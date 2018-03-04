Imports System.Data.Entity.Core
Imports libSEC

Public Class DocumentTypesController
         Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Details"

    Public Function Details(DocumentTypeRouteName As String) As ActionResult

        If Not UAL.CanViewDocumentTypeDetails Then Return New HttpUnauthorizedResult()

        Dim DocumentType = MeasurementsDAL.GetDocumentType(DocumentTypeRouteName.FromRouteName)
        If IsNothing(DocumentType) Then
            Return HttpNotFound()
        End If

        setDetailsLinks()

        Dim vm As New DocumentTypeDetailsViewModel With {
            .DocumentType = DocumentType,
            .Tabs = getDetailsTabs(DocumentType),
            .NavigationButtons = getDetailsNavigationButtons(DocumentType)
        }

        Return View(vm)

    End Function

    Private Sub setDetailsLinks()

        If UAL.CanViewDocumentDetails Then ViewData("ShowDocumentLinks") = True

    End Sub
    Private Function getDetailsNavigationButtons(documentType As DocumentType) As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons As New List(Of NavigationButtonViewModel)

        If UAL.CanViewDocumentTypeList Then buttons.Add(documentType.getIndexRouteButton64)
        If documentType.DocumentTypeName <> "Photo" And UAL.CanEditDocumentTypes Then buttons.Add(documentType.getEditRouteButton64)
        If documentType.canBeDeleted = True And UAL.CanDeleteDocumentTypes Then buttons.Add(documentType.getDeleteRouteButton64)

        Return buttons

    End Function

    Private Function getDetailsTabs(documentType As DocumentType) As IEnumerable(Of TabViewModel)

        Dim tabs As New List(Of TabViewModel)

        tabs.Add(TabViewModel.getDetailsTab("Basic Details", "DocumentTypes", documentType))

        If documentType.Documents.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Documents", "DocumentTypes", documentType))
        End If

        'If documentType.AllowedUserAccessLevels.Count > 0 Then
        '    tabs.Add(TabViewModel.getDetailsTab("User Access Levels", "DocumentTypes", documentType))
        'End If

        Return tabs

    End Function

#End Region

#Region "Index"

    Public Function Index() As ActionResult

        If Not UAL.CanViewDocumentList Then Return New HttpUnauthorizedResult()

        Return View(getViewDocumentTypesViewModel)

    End Function

    Private Function getViewDocumentTypesViewModel(Optional searchText As String = "") As ViewDocumentTypesViewModel

        Dim documentTypes = MeasurementsDAL.GetDocumentTypes
        Dim st = LCase(searchText)
        If searchText <> "" Then documentTypes = documentTypes.Where(Function(dt) LCase(dt.DocumentTypeName).Contains(st))

        Dim buttons As New List(Of NavigationButtonViewModel)
        buttons.Add(GetCreateButton64("DocumentType"))
        buttons.Add(GetIndexButton64("Document", "documents-button-64"))

        setIndexLinks()

        Return New ViewDocumentTypesViewModel With {
            .DocumentTypes = documentTypes.ToList,
            .TableId = "documenttypes-table",
            .UpdateTableRouteName = "DocumentTypeUpdateIndexTableRoute",
            .ObjectName = "DocumentType",
            .ObjectDisplayName = "Document Type",
            .NavigationButtons = getIndexNavigationButtons()
        }

    End Function

    Private Sub setIndexLinks()

        ViewData("ShowDocumentTypeLinks") = UAL.CanViewDocumentTypeDetails
        ViewData("ShowDeleteDocumentTypeLinks") = UAL.CanDeleteDocumentTypes

    End Sub
    Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

        Dim buttons = New List(Of NavigationButtonViewModel)

        If UAL.CanCreateDocumentTypes Then buttons.Add(GetCreateButton64("DocumentType"))
        If UAL.CanViewDocumentTypeList Then buttons.Add(GetIndexButton64("Document", "documents-button-64"))

        Return buttons

    End Function

    <HttpGet()> _
    Public Function UpdateIndexTable(SearchText As String) As PartialViewResult

        Return PartialView("Index_Table", getViewDocumentTypesViewModel(SearchText).DocumentTypes)

    End Function

#End Region

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(ByVal DocumentTypeRouteName As String) As ActionResult

        If Not UAL.CanEditDocumentTypes Then Return New HttpUnauthorizedResult()

        Dim DocumentType As DocumentType = MeasurementsDAL.GetDocumentType(DocumentTypeRouteName.FromRouteName)
        If IsNothing(DocumentType) Then Return HttpNotFound()
        If DocumentType.DocumentTypeName = "Photo" Then Return HttpNotFound()

        Return View(getEditDocumentTypeViewModel(DocumentType))

    End Function
    Private Function getEditDocumentTypeViewModel(ByVal DocumentType As DocumentType)

        'Return New EditDocumentTypeViewModel With {.DocumentType = DocumentType,
        '.AllChildDocumentTypes = MeasurementsDAL.GetDocumentTypes.Where(Function(dt) dt.Id <> DocumentType.Id),
        '.AllParentDocumentTypes = MeasurementsDAL.GetDocumentTypes.Where(Function(dt) dt.Id <> DocumentType.Id)}

        Return New EditDocumentTypeViewModel With {.DocumentType = DocumentType}

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditDocumentTypeViewModel) As ActionResult

        If Not UAL.CanEditDocumentTypes Then Return New HttpUnauthorizedResult()

        If ModelState.IsValid Then
            ' Update DocumentType
            MeasurementsDAL.UpdateDocumentType(ViewModel.DocumentType)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.DocumentTypeRouteName = ViewModel.DocumentType.getRouteName})
        End If

        Return View(ViewModel)

    End Function

    '<HttpPut()> _
    'Function AddChildDocumentType(DocumentTypeRouteName As String, ChildDocumentTypeId As Integer) As ActionResult

    '    Dim DocumentType = MeasurementsDAL.GetDocumentType(DocumentTypeRouteName.FromRouteName)
    '    MeasurementsDAL.AddDocumentTypeChildDocumentType(DocumentType.Id, ChildDocumentTypeId)
    '    Return Nothing

    'End Function
    '<HttpDelete()> _
    'Function RemoveChildDocumentType(DocumentTypeRouteName As String, ChildDocumentTypeId As Integer) As ActionResult

    '    Dim DocumentType = MeasurementsDAL.GetDocumentType(DocumentTypeRouteName.FromRouteName)
    '    MeasurementsDAL.RemoveDocumentTypeChildDocumentType(DocumentType.Id, ChildDocumentTypeId)
    '    Return Nothing

    'End Function

    '<HttpPut()> _
    'Function AddParentDocumentType(DocumentTypeRouteName As String, ParentDocumentTypeId As Integer) As ActionResult

    '    Dim DocumentType = MeasurementsDAL.GetDocumentType(DocumentTypeRouteName.FromRouteName)
    '    MeasurementsDAL.AddDocumentTypeParentDocumentType(DocumentType.Id, ParentDocumentTypeId)
    '    Return Nothing

    'End Function
    '<HttpDelete()> _
    'Function RemoveParentDocumentType(DocumentTypeRouteName As String, ParentDocumentTypeId As Integer) As ActionResult

    '    Dim DocumentType = MeasurementsDAL.GetDocumentType(DocumentTypeRouteName.FromRouteName)
    '    MeasurementsDAL.RemoveDocumentTypeParentDocumentType(DocumentType.Id, ParentDocumentTypeId)
    '    Return Nothing

    'End Function

#End Region

#Region "Create"

    <HttpGet()> _
    Public Function Create() As ActionResult

        If Not UAL.CanCreateDocumentTypes Then Return New HttpUnauthorizedResult()

        Return View(New DocumentType)

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ByVal DocumentType As DocumentType) As ActionResult

        If Not UAL.CanCreateDocumentTypes Then Return New HttpUnauthorizedResult()

        ' Check that DocumentTypeName does not already exist
        Dim existingDocumentTypeNames = MeasurementsDAL.GetDocumentTypes().Select(Function(dt) dt.DocumentTypeName.ToRouteName().ToLower()).ToList()
        If existingDocumentTypeNames.Contains(DocumentType.DocumentTypeName.ToRouteName().ToLower()) Then
            ModelState.AddModelError("DocumentTypeName", "Document Type Name already exists!")
        End If

        If ModelState.IsValid Then
            ' Add Document Type to database
            MeasurementsDAL.AddDocumentType(DocumentType)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.DocumentTypeRouteName = DocumentType.getRouteName})
        End If

        Return View(DocumentType)

    End Function

#End Region

#Region "Delete"

    <HttpPost()> _
    Public Function DeleteDocumentType(DocumentTypeId As Integer) As ActionResult

        If Not UAL.CanDeleteDocumentTypes Then Return New HttpUnauthorizedResult()

        Dim DocumentType = MeasurementsDAL.GetDocumentType(DocumentTypeId)
        If DocumentType Is Nothing Then Return Nothing
        MeasurementsDAL.DeleteDocumentType(DocumentTypeId)
        Return Json(New With {.redirectToUrl = Url.Action("Index")})

    End Function

#End Region



End Class