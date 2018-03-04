Imports System.Data.Entity.Core
Imports libSEC

Namespace SEC_Monitoring_Data_Website
    Public Class UserAccessLevelsController
        Inherits ControllerBase

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)

        End Sub


#Region "Index"

        Function Index() As ActionResult

            If Not UAL.CanViewUserAccessLevelList Then Return New HttpUnauthorizedResult()

            Return View(getViewUserAccessLevelsViewModel)

        End Function
        Private Function getViewUserAccessLevelsViewModel(Optional searchText As String = "") As ViewUserAccessLevelsViewModel

            Dim userAccessLevels = MeasurementsDAL.GetUserAccessLevels

            Dim st = LCase(searchText)

            ' Filter by search text
            If searchText <> "" Then
                userAccessLevels = userAccessLevels.Where(
                    Function(ual) LCase(ual.AccessLevelName).Contains(st)
                )
            End If

            setIndexLinks()

            Return New ViewUserAccessLevelsViewModel With {
                .UserAccessLevels = userAccessLevels.ToList,
                .NavigationButtons = getIndexNavigationButtons(),
                .TableId = "useraccesslevels-table",
                .UpdateTableRouteName = "UserAccessLevelUpdateIndexTableRoute",
                .ObjectName = "UserAccessLevel",
                .ObjectDisplayName = "User Access Level"
            }

        End Function
        Private Sub setIndexLinks()

            ViewData("ShowUserAccessLevelLinks") = UAL.CanViewUserAccessLevelDetails
            ViewData("ShowDeleteUserAccessLevelLinks") = UAL.CanDeleteUserAccessLevels

        End Sub
        Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

            Dim buttons = New List(Of NavigationButtonViewModel)

            If UAL.CanCreateUserAccessLevels Then buttons.Add(GetCreateButton64("UserAccessLevel"))

            Return buttons

        End Function
        <HttpGet()> _
        Public Function UpdateIndexTable(SearchText As String) As PartialViewResult

            Return PartialView("Index_Table",
                               getViewUserAccessLevelsViewModel(searchText:=SearchText).UserAccessLevels)

        End Function

#End Region

#Region "Details"

        Public Function Details(UserAccessLevelRouteName As String) As ActionResult

            If Not UAL.CanViewUserAccessLevelDetails Then Return New HttpUnauthorizedResult()

            Dim UserAccessLevel = MeasurementsDAL.GetUserAccessLevel(UserAccessLevelRouteName.FromRouteName)
            If IsNothing(UserAccessLevel) Then
                Return HttpNotFound()
            End If

            setDetailsLinks()

            Dim vm As New UserAccessLevelDetailsViewModel With {
                .UserAccessLevel = UserAccessLevel,
                .NavigationButtons = getDetailsNavigationButtons(UserAccessLevel),
                .Tabs = getDetailsTabs(UserAccessLevel)
            }

            Return View(vm)

        End Function

        Private Function getDetailsNavigationButtons(UserAccessLevel As UserAccessLevel) As IEnumerable(Of NavigationButtonViewModel)

            Dim buttons As New List(Of NavigationButtonViewModel)

            If UAL.CanViewUserAccessLevelList Then buttons.Add(UserAccessLevel.getIndexRouteButton64)
            If UAL.CanEditUserAccessLevels Then buttons.Add(UserAccessLevel.getEditRouteButton64)
            If UserAccessLevel.canBeDeleted = True And UAL.CanDeleteUserAccessLevels Then buttons.Add(UserAccessLevel.getDeleteRouteButton64)

            Return buttons

        End Function

        Private Function getDetailsTabs(UserAccessLevel As UserAccessLevel) As IEnumerable(Of TabViewModel)

            Dim tabs As New List(Of TabViewModel)

            ' Basic Details
            tabs.Add(TabViewModel.getDetailsTab("Basic Details", "UserAccessLevels", UserAccessLevel))
            tabs.Add(TabViewModel.getDetailsTab("Users", "UserAccessLevels", UserAccessLevel))

            Return tabs

        End Function

        Private Sub setDetailsLinks()

            ViewData("ShowUserLinks") = UAL.CanViewUserDetails

        End Sub

#End Region

#Region "Edit"

        <HttpGet()> _
        Public Function Edit(ByVal UserAccessLevelRouteName As String) As ActionResult

            If Not UAL.CanEditUserAccessLevels Then Return New HttpUnauthorizedResult()

            Dim UserAccessLevel As UserAccessLevel = MeasurementsDAL.GetUserAccessLevel(UserAccessLevelRouteName.FromRouteName)
            If IsNothing(UserAccessLevel) Then
                Return HttpNotFound()
            End If

            Return View(getEditUserAccessLevelViewModel(UserAccessLevel))

        End Function
        Private Function getEditUserAccessLevelViewModel(ByVal UserAccessLevel As UserAccessLevel) As EditUserAccessLevelViewModel

            Return New EditUserAccessLevelViewModel With {
                .UserAccessLevel = UserAccessLevel,
                .ProjectPermissionList = New SelectList(
                    MeasurementsDAL.GetProjectPermissions, "Id", "PermissionName", UserAccessLevel.ProjectPermissionId
                )
            }

        End Function
        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Edit(ByVal ViewModel As EditUserAccessLevelViewModel) As ActionResult

            If Not UAL.CanEditUserAccessLevels Then Return New HttpUnauthorizedResult()

            ModelState.Remove("UserAccessLevel.ProjectPermission")

            If ModelState.IsValid Then
                ' Attach Relations
                ViewModel.UserAccessLevel.ProjectPermission = MeasurementsDAL.GetProjectPermission(ViewModel.ProjectPermissionId)
                ViewModel.UserAccessLevel.ProjectPermissionId = ViewModel.ProjectPermissionId
                ' Update UserAccessLevel
                MeasurementsDAL.UpdateUserAccessLevel(ViewModel.UserAccessLevel)
                ' Redirect to Details
                Return RedirectToAction(
                    "Details",
                    New With {.UserAccessLevelRouteName = ViewModel.UserAccessLevel.getRouteName}
                )
            End If

            Return View(getEditUserAccessLevelViewModel(ViewModel.UserAccessLevel))

        End Function

#End Region

#Region "Create"

        <HttpGet()> _
        Public Function Create() As ActionResult

            If Not UAL.CanCreateUserAccessLevels Then Return New HttpUnauthorizedResult()

            Return View(getCreateUserAccessLevelViewModel())

        End Function
        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Create(ByVal ViewModel As CreateUserAccessLevelViewModel) As ActionResult

            If Not UAL.CanCreateUserAccessLevels Then Return New HttpUnauthorizedResult()

            ' Check that UserAccessLevelName does not already exist
            Dim existingUserAccessLevelNames = MeasurementsDAL.GetUserAccessLevels().Select(
                Function(ual) ual.AccessLevelName.ToRouteName().ToLower()
            ).ToList()
            If existingUserAccessLevelNames.Contains(ViewModel.UserAccessLevel.AccessLevelName.ToRouteName().ToLower()) Then
                ModelState.AddModelError("UserAccessLevel.AccessLevelName", "User Access Level Name already exists!")
            End If

            ModelState.Remove("UserAccessLevel.ProjectPermission")

            If ModelState.IsValid Then
                ' Attach Relations
                ViewModel.UserAccessLevel.ProjectPermissionId = ViewModel.ProjectPermissionId
                ViewModel.UserAccessLevel.ProjectPermission = MeasurementsDAL.GetProjectPermission(ViewModel.ProjectPermissionId)
                ' Add UserAccessLevel to database
                MeasurementsDAL.AddUserAccessLevel(ViewModel.UserAccessLevel)
                ' Redirect to Details
                Return RedirectToAction("Details", New With {.UserAccessLevelRouteName = ViewModel.UserAccessLevel.getRouteName})
            End If

            Return View(getCreateUserAccessLevelViewModel(ViewModel))

        End Function
        Private Function getCreateUserAccessLevelViewModel(Optional viewModel As CreateUserAccessLevelViewModel = Nothing) As CreateUserAccessLevelViewModel

            Dim userAccessLevel As UserAccessLevel
            Dim projectPermissionId As Integer

            If viewModel Is Nothing Then
                userAccessLevel = New UserAccessLevel()
                projectPermissionId = Nothing
            Else
                userAccessLevel = viewModel.UserAccessLevel
                projectPermissionId = viewModel.ProjectPermissionId
            End If

            Return New CreateUserAccessLevelViewModel With {
                .UserAccessLevel = userAccessLevel,
                .ProjectPermissionList = New SelectList(
                    MeasurementsDAL.GetProjectPermissions, "Id", "PermissionName"
                ),
                .ProjectPermissionId = projectPermissionId
            }

        End Function

#End Region

#Region "Delete"

        <HttpPost()> _
        Public Function DeleteUserAccessLevel(UserAccessLevelId As Integer) As ActionResult

            If Not UAL.CanDeleteUserAccessLevels Then Return New HttpUnauthorizedResult()

            Dim UserAccessLevel = MeasurementsDAL.GetUserAccessLevel(UserAccessLevelId)
            If UserAccessLevel Is Nothing Then Return Nothing
            If Not UserAccessLevel.canBeDeleted Then Return Nothing
            MeasurementsDAL.DeleteUserAccessLevel(UserAccessLevelId)
            Return Json(New With {.redirectToUrl = Url.Action("Index")})

        End Function

#End Region


    End Class

End Namespace