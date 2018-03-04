Imports System.Data.Entity.Core
Imports libSEC

Public Class MeasurementViewsController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub


#Region "Index"

    Public Function Index() As ActionResult

        If Not UAL.CanViewMeasurementViewList Then Return New HttpUnauthorizedResult()

        Return View(getViewMeasurementViewsViewModel)

    End Function

    Private Function getViewMeasurementViewsViewModel(Optional searchText As String = "",
                                                      Optional measurementTypeId As Integer = 0) As ViewMeasurementViewsViewModel

        Dim measurementViews = MeasurementsDAL.GetMeasurementViews
        Dim st = LCase(searchText)

        ' Filter by search text
        If searchText <> "" Then
            measurementViews = measurementViews.Where(
                Function(mv) LCase(mv.ViewName).Contains(st) Or _
                             LCase(mv.DisplayName).Contains(st) Or _
                             LCase(mv.MeasurementType.MeasurementTypeName).Contains(st) Or _
                             LCase(mv.MeasurementViewTableType.TableTypeName).Contains(st) Or _
                             LCase(mv.TableResultsHeader).Contains(st)
            )
        End If

        ' Filter by MeasurementType
        If measurementTypeId > 0 Then
            measurementViews = measurementViews.Where(
                Function(mv) mv.MeasurementTypeId = measurementTypeId
                )
        End If

        setIndexLinks()

        Return New ViewMeasurementViewsViewModel With {
            .MeasurementViews = measurementViews.ToList,
            .TableId = "measurementviews-table",
            .UpdateTableRouteName = "MeasurementViewUpdateIndexTableRoute",
            .ObjectName = "MeasurementView",
            .ObjectDisplayName = "Measurement View",
            .NavigationButtons = getIndexNavigationButtons(),
            .MeasurementTypeId = 0,
            .MeasurementTypeList = MeasurementsDAL.GetMeasurementTypesSelectList(True)
            }

    End Function

    Private Sub setIndexLinks()

        ViewData("ShowMeasurementViewLinks") = UAL.CanViewMeasurementViewDetails
        ViewData("ShowDeleteMeasurementViewLinks") = UAL.CanDeleteMeasurementViews

    End Sub
    Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

        Dim buttons = New List(Of NavigationButtonViewModel)

        If UAL.CanCreateMeasurementViews Then buttons.Add(GetCreateButton64("MeasurementView"))

        Return buttons

    End Function

    <HttpGet()> _
    Public Function UpdateIndexTable(SearchText As String, MeasurementTypeId As String) As PartialViewResult

        Dim mtid As Integer = 0
        If MeasurementTypeId <> "" Then mtid = CInt(MeasurementTypeId)

        Return PartialView("Index_Table",
                           getViewMeasurementViewsViewModel(searchText:=SearchText,
                                                            measurementTypeId:=mtid).MeasurementViews)

    End Function

#End Region

#Region "Details"

    Public Function Details(MeasurementViewRouteName As String) As ActionResult

        If Not UAL.CanViewMeasurementViewDetails Then Return New HttpUnauthorizedResult()

        Dim MeasurementView = MeasurementsDAL.GetMeasurementView(MeasurementViewRouteName.FromRouteName)

        If IsNothing(MeasurementView) Then
            Return HttpNotFound()
        End If

        setDetailsLinks()

        Dim vm As New MeasurementViewDetailsViewModel With {
            .MeasurementView = MeasurementView,
            .Tabs = getDetailsTabs(MeasurementView),
            .NavigationButtons = getDetailsNavigationButtons(MeasurementView)
        }

        Return View(vm)

    End Function

    Private Sub setDetailsLinks()

        ViewData("ShowCalculationFilterLinks") = UAL.CanViewCalculationFilterDetails
        ViewData("ShowMeasurementCommentTypeLinks") = UAL.CanViewMeasurementCommentTypeDetails
        ViewData("ShowProjectLinks") = UAL.CanViewProjectDetails

    End Sub

    Private Function getDetailsNavigationButtons(measurementView As MeasurementView)

        Dim buttons As New List(Of NavigationButtonViewModel)

        If UAL.CanViewMeasurementViewList Then buttons.Add(measurementView.getIndexRouteButton64)
        If UAL.CanEditMeasurementViews Then buttons.Add(measurementView.getEditRouteButton64)
        If measurementView.canBeDeleted = True And UAL.CanDeleteMeasurementViews Then buttons.Add(measurementView.getDeleteRouteButton64)

        Return buttons

    End Function

    Private Function getDetailsTabs(measurementView As MeasurementView) As IEnumerable(Of TabViewModel)

        Dim tabs As New List(Of TabViewModel)

        ' Basic Details
        tabs.Add(TabViewModel.getDetailsTab("Basic Details", "MeasurementViews", measurementView))

        ' Excluding Measurement Comment Types
        If measurementView.ExcludingMeasurementCommentTypes.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Comment Types", "MeasurementViews", measurementView))
        End If

        ' Projects
        If measurementView.Projects.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Projects", "MeasurementViews", measurementView))
        End If

        Return tabs

    End Function

#End Region

#Region "Create"

    <HttpGet()> _
    Public Function Create() As ActionResult

        If Not UAL.CanCreateMeasurementViews Then Return New HttpUnauthorizedResult()

        Return View(getCreateMeasurementViewViewModel())

    End Function
    Private Function getCreateMeasurementViewViewModel(Optional viewModel As CreateMeasurementViewViewModel = Nothing) As CreateMeasurementViewViewModel

        Dim measurementView As MeasurementView
        Dim measurementTypeId As Integer
        Dim measurementViewTableTypeId As Integer

        If viewModel Is Nothing Then
            measurementView = New MeasurementView
            measurementTypeId = Nothing
            measurementViewTableTypeId = Nothing
        Else
            measurementView = viewModel.MeasurementView
            measurementTypeId = viewModel.MeasurementTypeId
            measurementViewTableTypeId = viewModel.MeasurementViewTableTypeId
        End If

        Return New CreateMeasurementViewViewModel With {
            .MeasurementView = measurementView,
            .MeasurementTypeId = measurementTypeId,
            .MeasurementTypeList = New SelectList(MeasurementsDAL.GetMeasurementTypes, "Id", "MeasurementTypeName"),
            .MeasurementViewTableTypeId = measurementViewTableTypeId,
            .MeasurementViewTableTypeList = New SelectList(MeasurementsDAL.GetMeasurementViewTableTypes, "Id", "TableTypeName")
        }

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ByVal ViewModel As CreateMeasurementViewViewModel) As ActionResult

        If Not UAL.CanCreateMeasurementViews Then Return New HttpUnauthorizedResult()

        ' Check that MeasurementViewName does not already exist
        Dim existingMeasurementViewNames = MeasurementsDAL.GetMeasurementViews().Select(
            Function(mm) mm.ViewName.ToRouteName().ToLower()
        ).ToList()
        If existingMeasurementViewNames.Contains(ViewModel.MeasurementView.ViewName.ToRouteName().ToLower()) Then
            ModelState.AddModelError("MeasurementView.ViewName", "Measurement View Name already exists!")
        End If

        ModelState.Remove("MeasurementView.MeasurementType")
        ModelState.Remove("MeasurementView.MeasurementViewTableType")

        If ModelState.IsValid Then
            ' Attach Relations
            Dim measurementType = MeasurementsDAL.GetMeasurementType(ViewModel.MeasurementTypeId)
            ViewModel.MeasurementView.MeasurementType = measurementType
            Dim measurementViewTableType = MeasurementsDAL.GetMeasurementViewTableType(ViewModel.MeasurementViewTableTypeId)
            ViewModel.MeasurementView.MeasurementViewTableType = measurementViewTableType
            ' Add Measurement View to database
            MeasurementsDAL.AddMeasurementView(ViewModel.MeasurementView)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.MeasurementViewRouteName = ViewModel.MeasurementView.getRouteName})
        End If

        Return View(getCreateMeasurementViewViewModel(ViewModel))

    End Function

#End Region

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(ByVal MeasurementViewRouteName As String) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim MeasurementView As MeasurementView = MeasurementsDAL.GetMeasurementView(MeasurementViewRouteName.FromRouteName)
        If IsNothing(MeasurementView) Then
            Return HttpNotFound()
        End If

        Return View(getEditMeasurementViewViewModel(MeasurementView))

    End Function

    Private Function getEditMeasurementViewViewModel(ByVal MeasurementView As MeasurementView)

        Return New EditMeasurementViewViewModel With {
            .MeasurementView = MeasurementView,
            .MeasurementTypeId = MeasurementView.MeasurementTypeId,
            .MeasurementTypeList = New SelectList(MeasurementsDAL.GetMeasurementTypes, "Id", "MeasurementTypeName", MeasurementView.MeasurementTypeId),
            .MeasurementViewTableTypeId = MeasurementView.MeasurementViewTableTypeId,
            .MeasurementViewTableTypeList = New SelectList(MeasurementsDAL.GetMeasurementViewTableTypes, "Id", "TableTypeName", MeasurementView.MeasurementViewTableTypeId),
            .AllUserAccessLevels = MeasurementsDAL.GetUserAccessLevels,
            .AllCommentTypes = MeasurementsDAL.GetMeasurementCommentTypes,
            .AllProjects = AllowedProjects()
        }

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditMeasurementViewViewModel) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        ModelState.Remove("MeasurementView.MeasurementType")
        ModelState.Remove("MeasurementView.MeasurementViewTableType")

        If ModelState.IsValid Then
            ' Attach Relations
            Dim measurementType = MeasurementsDAL.GetMeasurementType(ViewModel.MeasurementTypeId)
            ViewModel.MeasurementView.MeasurementType = measurementType
            Dim measurementViewTableType = MeasurementsDAL.GetMeasurementViewTableType(ViewModel.MeasurementViewTableTypeId)
            ViewModel.MeasurementView.MeasurementViewTableType = measurementViewTableType
            ' Update MeasurementView
            MeasurementsDAL.UpdateMeasurementView(ViewModel.MeasurementView)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.MeasurementViewRouteName = ViewModel.MeasurementView.getRouteName})
        End If

        Return View(ViewModel)

    End Function


    <HttpPut()> _
    Public Function AddUserAccessLevel(MeasurementViewRouteName As String, UserAccessLevelId As Integer) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim MeasurementView = MeasurementsDAL.GetMeasurementView(MeasurementViewRouteName.FromRouteName)
        MeasurementsDAL.AddMeasurementViewUserAccessLevel(MeasurementView.Id, UserAccessLevelId)
        Return Nothing

    End Function
    <HttpDelete()> _
    Public Function RemoveUserAccessLevel(MeasurementViewRouteName As String, UserAccessLevelId As Integer) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim MeasurementView = MeasurementsDAL.GetMeasurementView(MeasurementViewRouteName.FromRouteName)
        MeasurementsDAL.RemoveMeasurementViewUserAccessLevel(MeasurementView.Id, UserAccessLevelId)
        Return Nothing

    End Function

    <HttpPut()> _
    Public Function AddCommentType(MeasurementViewRouteName As String, CommentTypeId As Integer) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim MeasurementView = MeasurementsDAL.GetMeasurementView(MeasurementViewRouteName.FromRouteName)
        MeasurementsDAL.AddMeasurementViewCommentType(MeasurementView.Id, CommentTypeId)
        Return Nothing

    End Function
    <HttpDelete()> _
    Public Function RemoveCommentType(MeasurementViewRouteName As String, CommentTypeId As Integer) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim MeasurementView = MeasurementsDAL.GetMeasurementView(MeasurementViewRouteName.FromRouteName)
        MeasurementsDAL.RemoveMeasurementViewCommentType(MeasurementView.Id, CommentTypeId)
        Return Nothing

    End Function

    <HttpPut()> _
    Public Function AddProject(MeasurementViewId As Integer, ProjectId As Integer) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        MeasurementsDAL.AddProjectMeasurementView(ProjectId, MeasurementViewId)
        Return Nothing

    End Function
    Public Function RemoveProject(MeasurementViewId As Integer, ProjectId As Integer) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        MeasurementsDAL.RemoveProjectMeasurementView(ProjectId, MeasurementViewId)
        Return Nothing

    End Function

#End Region

#Region "Delete"

    <HttpPost()> _
    Public Function DeleteMeasurementView(MeasurementViewId As Integer) As ActionResult

        If Not UAL.CanDeleteMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim MeasurementView = MeasurementsDAL.GetMeasurementView(MeasurementViewId)
        If MeasurementView Is Nothing Then Return Nothing

        For Each group In MeasurementView.MeasurementViewGroups.ToList
            For Each sequence In group.MeasurementViewSequenceSettings.ToList
                MeasurementsDAL.DeleteMeasurementViewSequenceSetting(sequence.Id)
            Next
            MeasurementsDAL.DeleteMeasurementViewGroup(group.Id)
        Next
        MeasurementsDAL.DeleteMeasurementView(MeasurementViewId)

        Return Json(New With {.redirectToUrl = Url.Action("Index")})

    End Function

    <HttpPost()> _
    Public Function DeleteSequence(MeasurementViewRouteName As String, GroupIndex As Integer, SequenceIndex As Integer) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim measurementView = MeasurementsDAL.GetMeasurementView(MeasurementViewRouteName.FromRouteName)
        Dim measurementViewGroup = measurementView.MeasurementViewGroups.Single(Function(mvg) mvg.GroupIndex = GroupIndex)
        Dim measurementViewSequenceSetting = measurementViewGroup.MeasurementViewSequenceSettings.Single(Function(mvss) mvss.SequenceIndex = SequenceIndex)

        ' Delete Sequence
        MeasurementsDAL.DeleteMeasurementViewSequenceSetting(measurementViewSequenceSetting.Id)

        ' Decrement Index of each Sequence with .Index > SequenceIndex
        Dim sequenceSettingsToDecrement = measurementViewGroup.MeasurementViewSequenceSettings.Where(Function(mvss) mvss.SequenceIndex > SequenceIndex).ToList
        For Each sequenceSetting In sequenceSettingsToDecrement
            sequenceSetting.SequenceIndex -= 1
            MeasurementsDAL.UpdateMeasurementViewSequenceSetting(sequenceSetting)
        Next

        Return Json(New With {.redirectToUrl = Url.Action("Edit")})

    End Function

    <HttpPost()> _
    Public Function DeleteGroup(MeasurementViewRouteName As String, GroupIndex As Integer) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim measurementView = MeasurementsDAL.GetMeasurementView(MeasurementViewRouteName.FromRouteName)
        Dim measurementViewGroup = measurementView.MeasurementViewGroups.Single(Function(mvg) mvg.GroupIndex = GroupIndex)

        ' Delete Group's Sequence Settings
        Dim sequencesToDelete = measurementViewGroup.MeasurementViewSequenceSettings.ToList
        For Each sequence In sequencesToDelete
            MeasurementsDAL.DeleteMeasurementViewSequenceSetting(sequence.Id)
        Next

        ' Delete Group
        MeasurementsDAL.DeleteMeasurementViewGroup(measurementViewGroup.Id)

        ' Decrement Index of each Group with .Index > GroupIndex
        Dim groupsToDecrement = measurementView.MeasurementViewGroups.Where(Function(mvg) mvg.GroupIndex > GroupIndex).ToList
        For Each group In groupsToDecrement
            group.GroupIndex -= 1
            MeasurementsDAL.UpdateMeasurementViewGroup(group)
        Next

        Return Json(New With {.redirectToUrl = Url.Action("Edit")})

    End Function

#End Region

End Class