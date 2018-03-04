Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports SEC_Monitoring_Data_Website
Imports libSEC


Namespace Controllers
    Public Class UsersController
        Inherits ControllerBase

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)

        End Sub

#Region "Index"

        Public Function Index() As ActionResult

            If Not UAL.CanViewUserList Then Return New HttpUnauthorizedResult()

            Return View(getViewUsersViewModel)

        End Function
        Private Function getViewUsersViewModel(Optional searchText As String = "") As ViewUsersViewModel

            Dim users = MeasurementsDAL.GetUsers
            Dim st = LCase(searchText)
            If searchText <> "" Then users = users.Where(
                Function(u) LCase(u.UserName).Contains(st) Or _
                LCase(u.UserAccessLevel.AccessLevelName).Contains(st) Or _
                LCase(u.Contact.ContactName).Contains(st) Or _
                LCase(u.Contact.EmailAddress).Contains(st) Or _
                LCase(u.Contact.Organisation.FullName).Contains(st)
            )

            setIndexLinks()

            Return New ViewUsersViewModel With {
                .Users = users.ToList,
                .TableId = "users-table",
                .UpdateTableRouteName = "UserUpdateIndexTableRoute",
                .ObjectName = "User",
                .ObjectDisplayName = "User",
                .NavigationButtons = getIndexNavigationButtons()
            }

        End Function

        Private Sub setIndexLinks()

            ViewData("ShowUserLinks") = UAL.CanViewUserDetails
            ViewData("ShowUserAccessLevelLinks") = UAL.CanViewUserAccessLevelDetails
            ViewData("ShowContactLinks") = UAL.CanViewContactDetails
            ViewData("ShowOrganisationLinks") = UAL.CanViewOrganisationDetails
            ViewData("ShowDeleteUserLinks") = UAL.CanDeleteUsers
            ViewData("CurrentUserName") = User.Identity.Name

        End Sub
        Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

            Dim buttons = New List(Of NavigationButtonViewModel)

            If UAL.CanCreateUsers Then buttons.Add(GetCreateButton64("User"))

            Return buttons

        End Function

        <HttpGet()> _
        Public Function UpdateIndexTable(SearchText As String) As PartialViewResult

            Return PartialView("Index_Table",
                               getViewUsersViewModel(SearchText).Users)

        End Function

#End Region

#Region "Details"

        Public Function Details(UserRouteName As String) As ActionResult

            If Not UAL.CanViewUserDetails Then Return New HttpUnauthorizedResult()

            Dim User = MeasurementsDAL.GetUser(UserRouteName.FromRouteName)
            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            setDetailsLinks()

            Dim vm As New UserDetailsViewModel With {
                .NavigationButtons = getDetailsNavigationButtons(User),
                .Tabs = getDetailsTabs(User)
            }

            Return View(vm)

        End Function

        Private Function getDetailsNavigationButtons(detailsUser As User) As IEnumerable(Of NavigationButtonViewModel)

            Dim buttons As New List(Of NavigationButtonViewModel)

            If UAL.CanViewUserList Then buttons.Add(detailsUser.getIndexRouteButton64)
            If UAL.CanEditUsers Then buttons.Add(detailsUser.getEditRouteButton64)
            If detailsUser.canBeDeleted = True And UAL.CanDeleteUsers And detailsUser.UserName.ToLower() <> User.Identity.Name Then buttons.Add(detailsUser.getDeleteRouteButton64)
            If UAL.CanInitiatePasswordResets = True Then buttons.Add(detailsUser.getResetPasswordButton64)

            Return buttons

        End Function

        Private Function getDetailsTabs(user As User) As IEnumerable(Of TabViewModel)

            Dim tabs As New List(Of TabViewModel)

            ' Basic Details
            tabs.Add(TabViewModel.getDetailsTab("Basic Details", "Users", user))

            Return tabs

        End Function

        Private Sub setDetailsLinks()

            ViewData("ShowOrganisationLink") = UAL.CanViewOrganisationDetails
            ViewData("ShowExcludedDocumentLinks") = UAL.CanViewDocumentDetails
            ViewData("ShowMeasurementFileLinks") = UAL.CanViewMeasurementFileDetails
            ViewData("ShowMonitorLocationLinks") = UAL.CanViewMonitorLocationDetails
            ViewData("ShowMonitorLinks") = UAL.CanViewMonitorDetails
            ViewData("ShowProjectLinks") = UAL.CanViewProjectDetails

        End Sub

#End Region

#Region "Edit"

        <HttpGet()> _
        Public Function Edit(ByVal UserRouteName As String) As ActionResult

            If Not UAL.CanEditUsers Then Return New HttpUnauthorizedResult()

            Dim User As User = MeasurementsDAL.GetUser(UserRouteName.FromRouteName)
            If IsNothing(User) Then
                Return HttpNotFound()
            End If

            Return View(getEditUserViewModel(User))

        End Function
        Private Function getEditUserViewModel(ByVal User As User) As EditUserViewModel

            Dim currentContact = User.Contact
            Dim currentUserAccessLevel = User.UserAccessLevel
            Dim contactList = New SelectList(
                MeasurementsDAL.GetContacts.Where(Function(c) c.User Is Nothing Or (c.User IsNot Nothing AndAlso c.User.Id = User.Id)),
                "Id", "ContactName", currentContact.Id
            )
            Dim userAccessLevelList = New SelectList(
                MeasurementsDAL.GetUserAccessLevels,
                "Id", "AccessLevelName", currentUserAccessLevel.Id
            )

            Return New EditUserViewModel With {
                .User = User,
                .ContactId = currentContact.Id,
                .ContactList = contactList,
                .UserAccessLevelId = currentUserAccessLevel.Id,
                .UserAccessLevelList = userAccessLevelList
            }

        End Function
        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Edit(ByVal ViewModel As EditUserViewModel) As ActionResult

            ' Check Permissions
            If Not UAL.CanEditUsers Then Return New HttpUnauthorizedResult()

            ModelState.Remove("User.Contact")
            ModelState.Remove("User.UserAccessLevel")

            If ModelState.IsValid Then
                ' Attach Relations
                ViewModel.User.Contact = MeasurementsDAL.GetContact(ViewModel.ContactId)
                ViewModel.User.UserAccessLevel = MeasurementsDAL.GetUserAccessLevel(ViewModel.UserAccessLevelId)
                ' Update User
                MeasurementsDAL.UpdateUser(ViewModel.User)
                ' Redirect to Details
                Return RedirectToAction(
                    "Details",
                    New With {.UserRouteName = ViewModel.User.getRouteName}
                )
            End If

            Return View(ViewModel)

        End Function

#End Region

#Region "Create"

        <HttpGet()> _
        Public Function Create() As ActionResult

            If Not UAL.CanCreateUsers Then Return New HttpUnauthorizedResult()

            Return View(getCreateUserViewModel())

        End Function
        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Create(ByVal ViewModel As CreateUserViewModel) As ActionResult

            If Not UAL.CanCreateUsers Then Return New HttpUnauthorizedResult()

            ' Check that UserName does not already exist
            Dim existingUserNames = MeasurementsDAL.GetUsers().Select(Function(u) u.UserName.ToRouteName().ToLower()).ToList()
            If existingUserNames.Contains(ViewModel.User.UserName.ToRouteName().ToLower()) Then
                ModelState.AddModelError("User.UserName", "User Name already exists!")
            End If

            ' Validation 
            ModelState.Remove("User.Contact")
            ModelState.Remove("User.UserAccessLevel")

            If ModelState.IsValid Then
                ' Attach Relations
                ViewModel.User.Contact = MeasurementsDAL.GetContact(ViewModel.ContactId)
                ViewModel.User.UserAccessLevel = MeasurementsDAL.GetUserAccessLevel(ViewModel.UserAccessLevelId)
                ' Encrypt Password
                ViewModel.User.Salt = CreateRandomSalt()
                ViewModel.User.Password = Hash512(ViewModel.User.Password, ViewModel.User.Salt)
                ' Add Contact to database
                MeasurementsDAL.AddUser(ViewModel.User)
                ' Redirect to Details
                Return RedirectToAction("Details", New With {.UserRouteName = ViewModel.User.getRouteName})
            End If

            Return View(getCreateUserViewModel())

        End Function
        Private Function getCreateUserViewModel(Optional viewModel As CreateUserViewModel = Nothing) As CreateUserViewModel

            Dim unassignedContacts = MeasurementsDAL.GetContacts.Where(Function(c) c.User Is Nothing)
            Dim contactList = New SelectList(unassignedContacts, "Id", "ContactName")
            Dim userAccessLevelList = New SelectList(MeasurementsDAL.GetUserAccessLevels, "Id", "AccessLevelName")

            Dim user As User
            Dim contactId As Integer
            Dim userAccessLevelId As Integer

            If viewModel Is Nothing Then
                user = New User
                contactId = Nothing
                userAccessLevelId = Nothing
            Else
                user = viewModel.User
                contactId = viewModel.ContactId
                userAccessLevelId = viewModel.UserAccessLevelId
            End If

            Return New CreateUserViewModel With {
                .User = user,
                .ContactId = contactId,
                .ContactList = contactList,
                .UserAccessLevelId = userAccessLevelId,
                .UserAccessLevelList = userAccessLevelList
            }

        End Function

#End Region

#Region "Delete"

        <HttpPost()> _
        Public Function DeleteUser(UserId As Integer) As ActionResult

            If Not UAL.CanDeleteUsers Then Return New HttpUnauthorizedResult()

            Dim User = MeasurementsDAL.GetUser(UserId)
            If User Is Nothing Then Return Nothing
            Dim userContact = User.Contact
            userContact.User = Nothing
            MeasurementsDAL.DeleteUser(UserId)
            Return Json(New With {.redirectToUrl = Url.Action("Index")})

        End Function

#End Region

#Region "Request Password Reset"

        <HttpPost()> _
        Public Function ResetPassword(ByVal UserId As Integer) As ActionResult

            If Not UAL.CanInitiatePasswordResets Then Return New HttpUnauthorizedResult()

            Dim user = MeasurementsDAL.GetUser(UserId)
            If user Is Nothing Then Return HttpNotFound()

            InitiatePasswordReset(user)

            Return Json(New With {.redirectToUrl = Url.Action("Index")})

        End Function

#End Region

    End Class
End Namespace
