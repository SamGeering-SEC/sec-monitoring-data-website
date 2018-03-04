Imports System.Data.Entity.Core
Imports libSEC

Namespace SEC_Monitoring_Data_Website
    Public Class ContactsController
        Inherits ControllerBase

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)

        End Sub

#Region "Index"

        Public Function Index() As ActionResult

            If Not UAL.CanViewContactList Then Return New HttpUnauthorizedResult()
            Return View(getViewContactsViewModel)

        End Function
        Private Function getViewContactsViewModel(Optional searchText As String = "") As ViewContactsViewModel

            Dim contacts = MeasurementsDAL.GetContacts
            Dim st = LCase(searchText)
            If searchText <> "" Then contacts = contacts.Where(
                Function(c) LCase(c.ContactName).Contains(st) Or _
                            LCase(c.EmailAddress).Contains(st) Or _
                            LCase(c.Organisation.FullName).Contains(st)
            )

            setIndexLinks()

            Return New ViewContactsViewModel With {
                .Contacts = contacts.ToList,
                .TableId = "contacts-table",
                .UpdateTableRouteName = "ContactUpdateIndexTableRoute",
                .ObjectName = "Contact",
                .ObjectDisplayName = "Contact",
                .NavigationButtons = getIndexNavigationButtons()
            }

        End Function
        Private Sub setIndexLinks()

            ViewData("ShowContactLinks") = UAL.CanViewContactList
            ViewData("ShowOrganisationLinks") = UAL.CanViewOrganisationDetails
            ViewData("ShowDeleteContactLinks") = UAL.CanDeleteContacts

        End Sub

        Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

            Dim buttons = New List(Of NavigationButtonViewModel)

            If UAL.CanCreateContacts Then buttons.Add(GetCreateButton64("Contact"))

            Return buttons

        End Function

        <HttpGet()> _
        Public Function UpdateIndexTable(SearchText As String) As PartialViewResult

            Return PartialView("Index_Table",
                               getViewContactsViewModel(SearchText).Contacts)

        End Function

#End Region

#Region "Details"

        Public Function Details(ContactRouteName As String) As ActionResult

            If Not UAL.CanViewContactDetails Then Return New HttpUnauthorizedResult()

            Dim Contact = MeasurementsDAL.GetContact(ContactRouteName.FromRouteName)
            If IsNothing(Contact) Then
                Return HttpNotFound()
            End If

            setDetailsLinks()

            Dim vm As New ContactDetailsViewModel With {.Tabs = getDetailsTabs(Contact),
                                                        .NavigationButtons = getNavigationButtons(Contact)}

            Return View(vm)

        End Function

        Private Function getNavigationButtons(contact As Contact) As IEnumerable(Of NavigationButtonViewModel)

            Dim buttons As New List(Of NavigationButtonViewModel)

            If UAL.CanViewContactList Then buttons.Add(contact.getIndexRouteButton64)
            If UAL.CanEditContacts Then buttons.Add(contact.getEditRouteButton64)
            If contact.canBeDeleted And UAL.CanDeleteContacts Then buttons.Add(contact.getDeleteRouteButton64)

            Return buttons

        End Function

        Private Function getDetailsTabs(contact As Contact) As IEnumerable(Of TabViewModel)

            Dim tabs As New List(Of TabViewModel)

            ' Basic Details
            tabs.Add(TabViewModel.getDetailsTab("Basic Details", "Contacts", contact))

            ' Projects
            If contact.Projects.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Projects", "Contacts", contact))
            End If

            ' Excluded Documents
            If contact.ExcludedDocuments.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Excluded Documents", "Contacts", contact))
            End If

            ' Measurement Files
            If contact.MeasurementFiles.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Measurement Files", "Contacts", contact))
            End If

            Return tabs

        End Function

        Private Sub setDetailsLinks()

            ' Basic Details
            ViewData("ShowOrganisationLink") = UAL.CanViewOrganisationDetails
            ViewData("ShowUserLink") = UAL.CanViewUserDetails

            ' Projects
            ViewData("ShowProjectLinks") = UAL.CanViewProjectDetails

            ' Excluded Documents
            ViewData("ShowExcludedDocumentLinks") = UAL.CanViewDocumentDetails

            ' Measurement Files
            ViewData("ShowMeasurementFileLinks") = UAL.CanViewMeasurementFileDetails
            ViewData("ShowMonitorLocationLinks") = UAL.CanViewMonitorLocationDetails
            ViewData("ShowMonitorLinks") = UAL.CanViewMonitorDetails

        End Sub

#End Region

#Region "Edit"

        <HttpGet()> _
        Public Function Edit(ByVal ContactRouteName As String) As ActionResult

            If Not UAL.CanEditContacts Then Return New HttpUnauthorizedResult()

            Dim Contact As Contact = MeasurementsDAL.GetContact(ContactRouteName.FromRouteName)
            If IsNothing(Contact) Then
                Return HttpNotFound()
            End If

            Return View(getEditContactViewModel(Contact))

        End Function
        Private Function getEditContactViewModel(ByVal Contact As Contact) As EditContactViewModel

            Return New EditContactViewModel With {
                .Contact = Contact,
                .OrganisationId = Contact.OrganisationId,
                .OrganisationList = New SelectList(MeasurementsDAL.GetOrganisations, "Id", "FullName", Contact.OrganisationId),
                .AllProjects = AllowedProjects(),
                .AllExcludedDocuments = MeasurementsDAL.GetDocuments
            }

        End Function
        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Edit(ByVal ViewModel As EditContactViewModel) As ActionResult

            If Not UAL.CanEditContacts Then Return New HttpUnauthorizedResult()

            ModelState.Remove("Contact.Organisation")

            If ModelState.IsValid Then
                ' Attach Relations
                ViewModel.Contact.Organisation = MeasurementsDAL.GetOrganisation(ViewModel.OrganisationId)
                ' Update Contact
                MeasurementsDAL.UpdateContact(ViewModel.Contact)
                ' Redirect to Details
                Return RedirectToAction("Details", New With {.ContactRouteName = ViewModel.Contact.getRouteName})
            End If

            Return View(ViewModel)

        End Function

        <HttpPut()> _
        Public Function AddProject(ContactRouteName As String, ProjectId As Integer) As ActionResult

            If Not UAL.CanEditContacts Then Return New HttpUnauthorizedResult()

            Dim Contact = MeasurementsDAL.GetContact(ContactRouteName.FromRouteName)
            MeasurementsDAL.AddContactProject(Contact.Id, ProjectId)
            Return Nothing

        End Function
        <HttpDelete()> _
        Public Function RemoveProject(ContactRouteName As String, ProjectId As Integer) As ActionResult

            If Not UAL.CanEditContacts Then Return New HttpUnauthorizedResult()

            Dim Contact = MeasurementsDAL.GetContact(ContactRouteName.FromRouteName)
            MeasurementsDAL.RemoveContactProject(Contact.Id, ProjectId)
            Return Nothing

        End Function

        <HttpPut()> _
        Public Function AddExcludedDocument(ContactRouteName As String, ExcludedDocumentId As Integer) As ActionResult

            If Not UAL.CanEditContacts Then Return New HttpUnauthorizedResult()

            Dim Contact = MeasurementsDAL.GetContact(ContactRouteName.FromRouteName)
            MeasurementsDAL.AddContactExcludedDocument(Contact.Id, ExcludedDocumentId)
            Return Nothing

        End Function
        <HttpDelete()> _
        Public Function RemoveExcludedDocument(ContactRouteName As String, ExcludedDocumentId As Integer) As ActionResult

            If Not UAL.CanEditContacts Then Return New HttpUnauthorizedResult()

            Dim Contact = MeasurementsDAL.GetContact(ContactRouteName.FromRouteName)
            MeasurementsDAL.RemoveContactExcludedDocument(Contact.Id, ExcludedDocumentId)
            Return Nothing

        End Function


#End Region

#Region "Create"

        <HttpGet()> _
        Public Function Create() As ActionResult

            If Not UAL.CanCreateContacts Then Return New HttpUnauthorizedResult()

            Return View(getCreateContactViewModel())

        End Function
        Private Function getCreateContactViewModel(Optional viewModel As CreateContactViewModel = Nothing) As CreateContactViewModel

            Dim contact As Contact
            Dim organisationId As Integer

            If viewModel Is Nothing Then
                contact = New Contact
                organisationId = Nothing
            Else
                contact = viewModel.Contact
                organisationId = viewModel.OrganisationId
            End If

            Return New CreateContactViewModel With {
                .Contact = contact,
                .OrganisationId = organisationId,
                .OrganisationList = New SelectList(MeasurementsDAL.GetOrganisations, "Id", "FullName")
            }

        End Function
        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Create(ByVal ViewModel As CreateContactViewModel) As ActionResult

            If Not UAL.CanCreateContacts Then Return New HttpUnauthorizedResult()

            ' Check that ContactName does not already exist
            Dim existingContactNames = MeasurementsDAL.GetContacts().Select(Function(c) c.ContactName.ToRouteName().ToLower()).ToList()
            If existingContactNames.Contains(ViewModel.Contact.ContactName.ToRouteName().ToLower()) Then
                ModelState.AddModelError("Contact.ContactName", "Contact Name already exists!")
            End If

            ModelState.Remove("Contact.Organisation")

            If ModelState.IsValid Then
                ' Attach Relations
                ViewModel.Contact.Organisation = MeasurementsDAL.GetOrganisation(ViewModel.OrganisationId)
                ' Add Contact to database
                MeasurementsDAL.AddContact(ViewModel.Contact)
                ' Redirect to Details
                Return RedirectToAction("Details", New With {.ContactRouteName = ViewModel.Contact.getRouteName})
            End If

            Return View(getCreateContactViewModel(ViewModel))

        End Function

#End Region

#Region "Delete"

        <HttpPost()> _
        Public Function DeleteContact(ContactId As Integer) As ActionResult

            If Not UAL.CanDeleteContacts Then Return New HttpUnauthorizedResult()

            Dim Contact = MeasurementsDAL.GetContact(ContactId)
            If Contact Is Nothing Then Return Nothing
            MeasurementsDAL.DeleteContact(ContactId)
            Return Json(New With {.redirectToUrl = Url.Action("Index")})

        End Function

#End Region

    End Class


End Namespace