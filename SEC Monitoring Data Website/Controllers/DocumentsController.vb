Imports System.Data.Entity.Core
Imports libSEC
Imports System.IO
Imports libSEC.Strings

Public Class DocumentsController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Index"

    Public Function Index() As ActionResult

        If Not UAL.CanViewDocumentList Then Return New HttpUnauthorizedResult()

        Return View(getViewDocumentsViewModel)

    End Function
    Public Function ProjectIndex(ProjectShortName As String) As ActionResult

        ' Load the project, if it exists
        Dim project = MeasurementsDAL.GetProject(ProjectShortName)
        If project Is Nothing Then
            Return HttpNotFound()
        End If
        Dim projectIds = New List(Of Integer) From {project.Id}

        Return View("Index", getViewDocumentsViewModel(projectIds:=projectIds))

    End Function
    Private Function getViewDocumentsViewModel(Optional searchText As String = "",
                                               Optional documentTypeId As Integer = 0,
                                               Optional projectIds As List(Of Integer) = Nothing) As ViewDocumentsViewModel

        Dim userProjects As New List(Of Project)

        userProjects = AllowedProjects()
        Dim monitorLocations = MeasurementsDAL.GetMonitorLocations().ToList
        Dim documents = MeasurementsDAL.GetDocuments().ToList

        ' Filter out documents which are assigned to a project the user does not have access to.
        Dim allowedDocuments As New List(Of Document)
        For Each doc In documents
            If doc.Projects.Count = 0 Then
                allowedDocuments.Add(doc)
            Else
                For Each documentProject In doc.Projects
                    If userProjects.Contains(documentProject) Then
                        allowedDocuments.Add(doc)
                    End If
                Next
            End If
        Next
        documents = allowedDocuments

        If Not projectIds Is Nothing AndAlso projectIds.Count > 0 Then
            monitorLocations = monitorLocations.Where(
                Function(ml) projectIds.Contains(ml.ProjectId)
            ).ToList
            documents = documents.Where(
                Function(doc) doc.Projects.Any(Function(p) projectIds.Contains(p.Id))
            ).ToList
        Else
            projectIds = New List(Of Integer)
        End If

        ' Get Monitor Locations and Monitors for the given Projects
        Dim monitors = monitorLocations.Where(
                Function(ml) ml.CurrentMonitor IsNot Nothing
            ).Select(Function(ml) ml.CurrentMonitor).ToList

        ' Filter by search text
        Dim st = LCase(searchText)
        If searchText <> "" Then
            documents = documents.Where(
                Function(d) LCase(d.AuthorOrganisation.FullName).Contains(st) Or
                            LCase(d.AuthorOrganisation.ShortName).Contains(st) Or
                            LCase(d.DocumentType.DocumentTypeName).Contains(st) Or
                            LCase(d.Title).Contains(st)
            )
        End If

        ' Filter by Document Type
        If documentTypeId > 0 Then
            documents = documents.Where(
                Function(d) d.DocumentTypeId = documentTypeId
            )
        End If

        ' Exclude any documents where the current Contact is an excluded Contact
        Dim documentList As New List(Of Document)
        For Each doc In documents.ToList
            If Not doc.ExcludedContacts.Contains(CurrentContact) Then
                documentList.Add(doc)
            End If
        Next

        Dim organisationIds = documents.Select(Function(d) d.AuthorOrganisationId).ToList
        Dim authorOrganisations = MeasurementsDAL.GetOrganisations.Where(
            Function(o) organisationIds.Contains(o.Id)
        ).ToList

        Dim vm As New ViewDocumentsViewModel With {
            .AuthorOrganisations = authorOrganisations,
            .Documents = documentList,
            .MonitorLocations = monitorLocations,
            .Monitors = monitors,
            .Projects = userProjects,
            .ProjectIds = String.Join(",", projectIds.Select(Function(i) i.ToString()).ToArray),
            .SearchText = searchText,
            .TableId = "documents-table",
            .UpdateTableRouteName = "DocumentUpdateIndexTableRoute",
            .ObjectName = "Document",
            .ObjectDisplayName = "Document",
            .NavigationButtons = getIndexNavigationButtons(projectIds),
            .DocumentTypeId = 0,
            .DocumentTypeList = MeasurementsDAL.GetDocumentTypesSelectList(True)
        }
        setIndexLinks()

        Return vm

    End Function
    Private Sub setIndexLinks()

        ViewData("ShowDocumentLinks") = UAL.CanViewDocumentDetails
        ViewData("ShowDocumentTypeLinks") = UAL.CanViewDocumentTypeDetails
        ViewData("ShowAuthorOrganisationLinks") = UAL.CanViewOrganisationDetails
        ViewData("ShowDeleteDocumentLinks") = UAL.CanDeleteDocuments

    End Sub
    Private Function getIndexNavigationButtons(projectIds As List(Of Integer)) As List(Of NavigationButtonViewModel)

        Dim buttons = New List(Of NavigationButtonViewModel)

        If UAL.CanCreateDocuments Then buttons.Add(GetCreateButton64("Document"))
        If UAL.CanViewDocumentTypeList Then buttons.Add(GetIndexButton64("DocumentType", "types-button-64"))
        If projectIds IsNot Nothing AndAlso projectIds.Count > 0 Then
            Dim project = MeasurementsDAL.GetProject(projectIds.First)
            If projectIds.Count = 1 And CanAccessProject(project.Id) And UAL.CanViewProjectDetails Then
                buttons.Add(project.getDetailsRouteButton64("Back to Project"))
            End If
        End If

        Return buttons

    End Function
    <HttpGet()> _
    Public Function UpdateIndexTable(SearchText As String, DocumentTypeId As String, ProjectIds As String) As PartialViewResult

        Dim dtid As Integer = 0
        If DocumentTypeId <> "" Then dtid = CInt(DocumentTypeId)
        Dim projectIdsList = ProjectIds.Split(",").Select(Function(id) CInt(id)).ToList

        Return PartialView(
            "Index_Table",
            getViewDocumentsViewModel(
                searchText:=SearchText,
                documentTypeId:=dtid,
                projectIds:=projectIdsList
            ).Documents
        )

    End Function

#End Region

#Region "Details"

    <HttpGet()> _
    Public Function Details(DocumentTitle As String, DocumentFileName As String, DocumentUploadDate As String, DocumentUploadTime As String) As ActionResult

        If Not UAL.CanViewDocumentDetails Then Return New HttpUnauthorizedResult()

        Dim documentPath = getPartialFileName(DocumentFileName, DocumentUploadDate, DocumentUploadTime)
        Dim Document = MeasurementsDAL.GetDocument(documentPath)

        If IsNothing(Document) Then
            Return HttpNotFound()
        End If

        setDetailsLinks()

        Dim vm As New DocumentDetailsViewModel With {
            .Document = Document,
            .NavigationButtons = getDetailsNavigationButtons(Document),
            .Tabs = getDetailsTabs(Document)
        }

        Return View(vm)

    End Function

    Private Sub setDetailsLinks()

        ViewData("ShowDocumentTypeLinks") = UAL.CanViewDocumentTypeDetails
        ViewData("ShowAuthorOrganisationLinks") = UAL.CanViewOrganisationDetails
        ViewData("ShowExcludedContactLinks") = UAL.CanViewContactDetails
        ViewData("ShowMonitorLocationLinks") = UAL.CanViewMonitorLocationDetails
        ViewData("ShowMonitorLinks") = UAL.CanViewMonitorDetails
        ViewData("ShowProjectLinks") = UAL.CanViewProjectDetails

    End Sub
    Private Function getDetailsNavigationButtons(document As Document) As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons As New List(Of NavigationButtonViewModel)

        If UAL.CanViewDocumentList Then buttons.Add(document.getIndexRouteButton64)
        If UAL.CanEditDocuments Then buttons.Add(document.getEditRouteButton64)
        If document.canBeDeleted = True And UAL.CanDeleteDocuments Then buttons.Add(document.getDeleteRouteButton64)

        Return buttons

    End Function

    Private Function getDetailsTabs(document As Document) As IEnumerable(Of TabViewModel)

        Dim tabs As New List(Of TabViewModel)

        ' Basic Details
        tabs.Add(TabViewModel.getDetailsTab("Basic Details", "Documents", document))

        ' Projects
        If document.Projects.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Projects", "Documents", document))
        End If

        ' Monitors
        If document.Monitors.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Monitors", "Documents", document))
        End If

        ' Monitor Locations
        If document.MonitorLocations.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Monitor Locations", "Documents", document))
        End If

        ' Excluded Contacts
        If document.ExcludedContacts.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Excluded Contacts", "Documents", document))
        End If

        Return tabs

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Download(DocumentId As Integer) As FileResult

        If Not UAL.CanViewDocumentDetails Then Return Nothing
        Dim document = MeasurementsDAL.GetDocument(DocumentId)
        Dim contentType As String = "application/" + Mid(Path.GetExtension(document.FilePath), 2)
        Dim currentFileName = Path.Combine(Server.MapPath("~/Content/Documents"), document.FilePath)

        Return File(currentFileName, contentType, Path.GetFileName(currentFileName))

    End Function

#End Region

#Region "Create"

    <HttpGet()> _
    Public Function Create(Optional MonitorLocationId As Nullable(Of Integer) = Nothing,
                           Optional DocumentTypeName As String = Nothing) As ActionResult

        If Not UAL.CanCreateDocuments Then Return New HttpUnauthorizedResult()

        If MonitorLocationId IsNot Nothing And DocumentTypeName IsNot Nothing Then
            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(MonitorLocationId)
            If monitorLocation Is Nothing Then Return HttpNotFound()
            Dim documentType = MeasurementsDAL.GetDocumentType(DocumentTypeName)
            If documentType Is Nothing Then Return HttpNotFound()
            Return View(getCreateDocumentViewModel(monitorLocation, documentType))
        End If

        Return View(getCreateDocumentViewModel)

    End Function

    Private Function getCreateDocumentViewModel(Optional MonitorLocation As MonitorLocation = Nothing, Optional DocumentType As DocumentType = Nothing) As CreateDocumentViewModel

        ' Create selected item lists
        Dim selectedProjects = New List(Of SelectableProject)
        Dim selectedMonitors = New List(Of SelectableMonitor)
        Dim selectedMonitorLocations = New List(Of SelectableMonitorLocation)
        Dim returnToRouteName = ""
        Dim returnToRouteValues = Nothing
        Dim documentTypeId = Nothing

        ' Adding a photo from the monitor location edit page
        If MonitorLocation IsNot Nothing And DocumentType IsNot Nothing Then
            selectedProjects.Add(New SelectableProject(MonitorLocation.Project))
            selectedMonitorLocations.Add(New SelectableMonitorLocation(MonitorLocation))
            If MonitorLocation.CurrentMonitor IsNot Nothing Then selectedMonitors.Add(New SelectableMonitor(MonitorLocation.CurrentMonitor))
            documentTypeId = DocumentType.Id
            returnToRouteName = "MonitorLocationEditRoute"
            returnToRouteValues = New With {.ProjectRouteName = MonitorLocation.Project.getRouteName,
                                            .MonitorLocationRouteName = MonitorLocation.getRouteName}
        End If

        ' Create available item lists
        Dim Projects = AllowedProjects()
        Dim availableProjects = Projects.Select(Function(p) New SelectableProject(p)).ToList
        Dim monitors = AllowedMonitors()
        Dim availableMonitors = monitors.Select(Function(m) New SelectableMonitor(m)).ToList
        Dim monitorLocations = AllowedMonitorLocations()
        Dim availableMonitorLocations = monitorLocations.Select(Function(ml) New SelectableMonitorLocation(ml)).ToList

        ' Create viewmodel
        Dim viewModel As New CreateDocumentViewModel With {
            .Document = New Document,
            .DocumentTypeId = documentTypeId,
            .DocumentTypeList = New SelectList(MeasurementsDAL.GetDocumentTypes, "Id", "DocumentTypeName"),
            .AuthorOrganisationId = Nothing,
            .AuthorOrganisationList = New SelectList(MeasurementsDAL.GetOrganisations, "Id", "FullName"),
            .StartDate = Date.Today,
            .EndDate = Date.Today,
            .StartTime = Date.Today,
            .EndTime = Date.Today,
            .AvailableProjects = availableProjects,
            .SelectedProjects = selectedProjects,
            .AvailableMonitors = availableMonitors,
            .SelectedMonitors = selectedMonitors,
            .AvailableMonitorLocations = availableMonitorLocations,
            .SelectedMonitorLocations = selectedMonitorLocations,
            .ReturnToRouteName = returnToRouteName,
            .ReturnToRouteValues = returnToRouteValues}

        Return viewModel


    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ByVal ViewModel As CreateDocumentViewModel) As ActionResult

        If Not UAL.CanCreateDocuments Then Return New HttpUnauthorizedResult()

        ModelState.Remove("Document.DocumentType")
        ModelState.Remove("Document.AuthorOrganisation")
        ModelState.Remove("Document.StartDateTime")
        ModelState.Remove("Document.EndDateTime")

        If ModelState.IsValid And ViewModel.UploadFile IsNot Nothing Then

            ' Attach Relations
            ViewModel.Document.DocumentType = MeasurementsDAL.GetDocumentType(ViewModel.DocumentTypeId)
            ViewModel.Document.AuthorOrganisation = MeasurementsDAL.GetOrganisation(ViewModel.AuthorOrganisationId)

            ' Assign selected Projects
            If Not ViewModel.PostedProjects Is Nothing Then
                Dim intPostedProjectIds = ViewModel.PostedProjects.ProjectIds.Select(Function(Id) Convert.ToInt32(Id)).ToList
                Dim Projects = AllowedProjects().Where(Function(Project) intPostedProjectIds.Contains(Project.Id)).ToList
                ViewModel.Document.Projects = Projects
            End If
            ' Assign selected Monitors
            If Not ViewModel.PostedMonitors Is Nothing Then
                Dim intPostedMonitorIds = ViewModel.PostedMonitors.MonitorIds.Select(Function(Id) Convert.ToInt32(Id)).ToList
                Dim Monitors = MeasurementsDAL.GetMonitors().Where(Function(Monitor) intPostedMonitorIds.Contains(Monitor.Id)).ToList
                ViewModel.Document.Monitors = Monitors
            End If
            ' Assign selected MonitorLocations
            If Not ViewModel.PostedMonitorLocations Is Nothing Then
                Dim intPostedMonitorLocationIds = ViewModel.PostedMonitorLocations.MonitorLocationIds.Select(Function(Id) Convert.ToInt32(Id)).ToList
                Dim MonitorLocations = MeasurementsDAL.GetMonitorLocations().Where(Function(MonitorLocation) intPostedMonitorLocationIds.Contains(MonitorLocation.Id)).ToList
                ViewModel.Document.MonitorLocations = MonitorLocations
            End If
            ' Attach DateTimes
            Try
                ViewModel.Document.StartDateTime = ViewModel.StartDate.AddDays(ViewModel.StartTime.TimeOnly.ToOADate)
                ViewModel.Document.EndDateTime = ViewModel.EndDate.AddDays(ViewModel.EndTime.TimeOnly.ToOADate)
            Catch ex As Exception
                ViewModel.Document.StartDateTime = Date.Now
                ViewModel.Document.EndDateTime = Date.Now
            End Try
            ' File was posted
            If ViewModel.UploadFile.ContentLength > 0 Then
                ' Add File to File System
                Dim FnWithoutExtension = Path.GetFileNameWithoutExtension(ViewModel.UploadFile.FileName)
                Dim FnExtension = Path.GetExtension(ViewModel.UploadFile.FileName)
                Dim Fn = FnWithoutExtension + Format(Date.Now(), "@yyyy-MM-dd@HH-mm-ss") + FnExtension
                Dim filePath = Path.Combine(Server.MapPath("~/Content/Documents"), Fn)
                ViewModel.UploadFile.SaveAs(filePath)
                ViewModel.Document.FilePath = Fn
                ' Add Document to database
                MeasurementsDAL.AddDocument(ViewModel.Document)
                ' Redirect to Appropriate Page
                If ViewModel.ReturnToRouteName = "" Then
                    Return RedirectToAction(
                        "Details",
                        New With {
                            .DocumentFileName = ViewModel.Document.getFileName,
                            .DocumentUploadDate = ViewModel.Document.getUploadDate,
                            .DocumentUploadTime = ViewModel.Document.getUploadTime
                        }
                    )
                Else
                    Dim rvDict = ViewModel.ReturnToRouteValues(0).ToString.ToDict
                    Return RedirectToRoute(ViewModel.ReturnToRouteName, New RouteValueDictionary(rvDict))
                End If

            End If
        End If

        ViewModel.DocumentTypeId = Nothing
        ViewModel.DocumentTypeList = New SelectList(MeasurementsDAL.GetDocumentTypes, "Id", "DocumentTypeName")
        ViewModel.AuthorOrganisationId = Nothing
        ViewModel.AuthorOrganisationList = New SelectList(MeasurementsDAL.GetOrganisations, "Id", "FullName")

        Return View(ViewModel)

    End Function

#End Region

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(DocumentTitle As String, DocumentFileName As String, DocumentUploadDate As String, DocumentUploadTime As String) As ActionResult

        If Not UAL.CanEditDocuments Then Return New HttpUnauthorizedResult()

        Dim documentPath = getPartialFileName(DocumentFileName, DocumentUploadDate, DocumentUploadTime)

        Dim Document As Document = MeasurementsDAL.GetDocument(documentPath)
        If IsNothing(Document) Then
            Return HttpNotFound()
        End If

        Return View(getEditDocumentViewModel(Document))

    End Function
    Private Function getEditDocumentViewModel(ByVal Document As Document)

        Return New EditDocumentViewModel With {
            .Document = Document,
            .DocumentTypeId = Document.DocumentTypeId,
            .DocumentTypeList = New SelectList(MeasurementsDAL.GetDocumentTypes, "Id", "DocumentTypeName", Document.DocumentTypeId),
            .AuthorOrganisationId = Document.AuthorOrganisationId,
            .AuthorOrganisationList = New SelectList(MeasurementsDAL.GetOrganisations, "Id", "FullName", Document.AuthorOrganisationId),
            .AllProjects = AllowedProjects(),
            .AllMonitors = MeasurementsDAL.GetMonitors,
            .AllMonitorLocations = MeasurementsDAL.GetMonitorLocations,
            .AllExcludedContacts = MeasurementsDAL.GetContacts,
            .StartDate = Document.StartDateTime.DateOnly,
            .StartTime = Document.StartDateTime.TimeOnly,
            .EndDate = Document.EndDateTime.DateOnly,
            .EndTime = Document.EndDateTime.TimeOnly
        }

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditDocumentViewModel) As ActionResult

        If Not UAL.CanEditDocuments Then Return New HttpUnauthorizedResult()

        ModelState.Remove("Document.DocumentType")
        ModelState.Remove("Document.AuthorOrganisation")
        ModelState.Remove("Document.StartDateTime")
        ModelState.Remove("Document.EndDateTime")

        If ModelState.IsValid Then
            ' Attach Relations
            ViewModel.Document.DocumentType = MeasurementsDAL.GetDocumentType(ViewModel.DocumentTypeId)
            ViewModel.Document.AuthorOrganisation = MeasurementsDAL.GetOrganisation(ViewModel.AuthorOrganisationId)
            ' Attach DateTimes
            Try
                ViewModel.Document.StartDateTime = ViewModel.StartDate.AddDays(ViewModel.StartTime.TimeOnly.ToOADate)
                ViewModel.Document.EndDateTime = ViewModel.EndDate.AddDays(ViewModel.EndTime.TimeOnly.ToOADate)
            Catch ex As Exception
                ViewModel.Document.StartDateTime = Date.Now
                ViewModel.Document.EndDateTime = Date.Now
            End Try
            ' Update Document
            Dim updatedDocument = MeasurementsDAL.UpdateDocument(ViewModel.Document)
            ' Redirect to Details
            Return RedirectToAction(
                "Details",
                New With {.DocumentFileName = updatedDocument.getFileName,
                          .DocumentUploadDate = updatedDocument.getUploadDate,
                          .DocumentUploadTime = updatedDocument.getUploadTime}
            )
        End If

        Return View(ViewModel)

    End Function

    <HttpPut()> _
    Public Function AddProject(DocumentId As Integer, ProjectId As Integer) As ActionResult

        If Not UAL.CanEditDocuments Then Return New HttpUnauthorizedResult()
        Dim Document = MeasurementsDAL.GetDocument(DocumentId)
        MeasurementsDAL.AddDocumentProject(Document.Id, ProjectId)
        Return Nothing

    End Function
    Public Function RemoveProject(DocumentId As Integer, ProjectId As Integer) As ActionResult

        If Not UAL.CanEditDocuments Then Return New HttpUnauthorizedResult()
        Dim Document = MeasurementsDAL.GetDocument(DocumentId)
        MeasurementsDAL.RemoveDocumentProject(Document.Id, ProjectId)
        Return Nothing

    End Function

    <HttpPut()> _
    Public Function AddMonitor(DocumentId As Integer, MonitorId As Integer) As ActionResult

        If Not UAL.CanEditDocuments Then Return New HttpUnauthorizedResult()
        Dim Document = MeasurementsDAL.GetDocument(DocumentId)
        MeasurementsDAL.AddDocumentMonitor(Document.Id, MonitorId)
        Return Nothing

    End Function
    Public Function RemoveMonitor(DocumentId As Integer, MonitorId As Integer) As ActionResult

        If Not UAL.CanEditDocuments Then Return New HttpUnauthorizedResult()
        Dim Document = MeasurementsDAL.GetDocument(DocumentId)
        MeasurementsDAL.RemoveDocumentMonitor(Document.Id, MonitorId)
        Return Nothing

    End Function

    <HttpPut()> _
    Public Function AddMonitorLocation(DocumentId As Integer, MonitorLocationId As Integer) As ActionResult

        If Not UAL.CanEditDocuments Then Return New HttpUnauthorizedResult()
        Dim Document = MeasurementsDAL.GetDocument(DocumentId)
        MeasurementsDAL.AddDocumentMonitorLocation(Document.Id, MonitorLocationId)
        Return Nothing

    End Function
    Public Function RemoveMonitorLocation(DocumentId As Integer, MonitorLocationId As Integer) As ActionResult

        If Not UAL.CanEditDocuments Then Return New HttpUnauthorizedResult()
        Dim Document = MeasurementsDAL.GetDocument(DocumentId)
        MeasurementsDAL.RemoveDocumentMonitorLocation(Document.Id, MonitorLocationId)
        Return Nothing

    End Function

    <HttpPut()> _
    Public Function AddExcludedContact(DocumentId As Integer, ExcludedContactId As Integer) As ActionResult

        If Not UAL.CanEditDocuments Then Return New HttpUnauthorizedResult()
        Dim Document = MeasurementsDAL.GetDocument(DocumentId)
        MeasurementsDAL.AddDocumentExcludedContact(Document.Id, ExcludedContactId)
        Return Nothing

    End Function
    Public Function RemoveExcludedContact(DocumentId As Integer, ExcludedContactId As Integer) As ActionResult

        If Not UAL.CanEditDocuments Then Return New HttpUnauthorizedResult()
        Dim Document = MeasurementsDAL.GetDocument(DocumentId)
        MeasurementsDAL.RemoveDocumentExcludedContact(Document.Id, ExcludedContactId)
        Return Nothing

    End Function

#End Region

#Region "Delete"

    <HttpPost()> _
    Public Function DeleteDocument(DocumentId As Integer) As ActionResult

        If Not UAL.CanDeleteDocuments Then Return New HttpUnauthorizedResult()
        Dim Document = MeasurementsDAL.GetDocument(DocumentId)
        If Document Is Nothing Then Return Nothing
        MeasurementsDAL.DeleteDocument(DocumentId)
        Return Json(New With {.redirectToUrl = Url.Action("Index")})

    End Function

#End Region

#Region "Shared"

    Private Function getPartialFileName(DocumentFileName As String, DocumentUploadDate As String, DocumentUploadTime As String) As String

        Return DocumentFileName + "@" + DocumentUploadDate + "@" + DocumentUploadTime

    End Function

#End Region


End Class