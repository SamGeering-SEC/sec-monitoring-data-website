Imports System.Data.Entity.Core
Imports libSEC

Public Class MeasurementCommentTypesController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Index"

    Public Function Index() As ActionResult

        If Not UAL.CanViewMeasurementCommentTypeList Then Return New HttpUnauthorizedResult()

        Return View(getViewMeasurementCommentTypesViewModel)

    End Function

    Private Function getViewMeasurementCommentTypesViewModel(Optional searchText As String = "") As ViewMeasurementCommentTypesViewModel

        Dim MeasurementCommentTypes = MeasurementsDAL.GetMeasurementCommentTypes
        Dim st = LCase(searchText)
        If searchText <> "" Then MeasurementCommentTypes = MeasurementCommentTypes.Where(Function(mct) LCase(mct.CommentTypeName).Contains(st))

        ' Create Navigation Buttons
        Dim buttons As New List(Of NavigationButtonViewModel)
        buttons.Add(GetCreateButton64("MeasurementCommentType"))

        setIndexLinks()

        Return New ViewMeasurementCommentTypesViewModel With {
            .MeasurementCommentTypes = MeasurementCommentTypes.ToList,
            .TableId = "measurementcommenttypes-table",
            .UpdateTableRouteName = "MeasurementCommentTypeUpdateIndexTableRoute",
            .ObjectName = "MeasurementCommentType",
            .ObjectDisplayName = "Measurement Comment Type",
            .NavigationButtons = getIndexNavigationButtons()
        }

    End Function

    Private Sub setIndexLinks()

        ViewData("ShowMeasurementCommentTypeLinks") = UAL.CanViewMeasurementCommentTypeDetails()
        ViewData("ShowDeleteMeasurementCommentTypeLinks") = UAL.CanDeleteMeasurementCommentTypes()

    End Sub
    Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

        Dim buttons = New List(Of NavigationButtonViewModel)

        If UAL.CanCreateMeasurementCommentTypes Then buttons.Add(GetCreateButton64("MeasurementCommentType"))

        Return buttons

    End Function

    <HttpGet()> _
    Public Function UpdateIndexTable(SearchText As String) As PartialViewResult

        Return PartialView("Index_Table", getViewMeasurementCommentTypesViewModel(SearchText).MeasurementCommentTypes)

    End Function

#End Region

#Region "Details"

    Public Function Details(MeasurementCommentTypeRouteName As String) As ActionResult

        If Not UAL.CanViewMeasurementCommentTypeDetails Then Return New HttpUnauthorizedResult()

        Dim MeasurementCommentType = MeasurementsDAL.GetMeasurementCommentType(MeasurementCommentTypeRouteName.FromRouteName)
        If IsNothing(MeasurementCommentType) Then
            Return HttpNotFound()
        End If


        Dim vm As New MeasurementCommentTypeDetailsViewModel With {
            .MeasurementCommentType = MeasurementCommentType,
            .NavigationButtons = getDetailsNavigationButtons(MeasurementCommentType),
            .Tabs = getDetailsTabs(MeasurementCommentType)
        }

        setDetailsLinks()

        Return View(vm)

    End Function

    Private Sub setDetailsLinks()

        ViewData("ShowExcludedMeasurementViewLinks") = UAL.CanViewMeasurementViewDetails
        ViewData("ShowExcludedCriteriaLinks") = UAL.CanViewAssessmentCriteria

    End Sub

    Private Function getDetailsNavigationButtons(measurementCommentType As MeasurementCommentType) As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons As New List(Of NavigationButtonViewModel)

        If UAL.CanViewMeasurementCommentTypeList Then buttons.Add(measurementCommentType.getIndexRouteButton64)
        If UAL.CanEditMeasurementCommentTypes Then buttons.Add(measurementCommentType.getEditRouteButton64)
        If measurementCommentType.canBeDeleted = True And UAL.CanDeleteMeasurementCommentTypes Then buttons.Add(measurementCommentType.getDeleteRouteButton64)

        Return buttons

    End Function

    Private Function getDetailsTabs(measurementCommentType As MeasurementCommentType) As IEnumerable(Of TabViewModel)

        Dim tabs As New List(Of TabViewModel)

        ' Basic Details
        tabs.Add(TabViewModel.getDetailsTab("Basic Details", "MeasurementCommentTypes", measurementCommentType))

        ' Excluded Measurement Views
        If measurementCommentType.ExcludedMeasurementViews.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Excluded Views", "MeasurementCommentTypes", measurementCommentType))
        End If

        ' Excluded Assessment Criterion Groups
        If measurementCommentType.ExcludedAssessmentCriterionGroups.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Excluded Criteria", "MeasurementCommentTypes", measurementCommentType))
        End If

        Return tabs

    End Function

#End Region

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(ByVal MeasurementCommentTypeRouteName As String) As ActionResult

        If Not UAL.CanEditMeasurementCommentTypes Then Return New HttpUnauthorizedResult()

        Dim MeasurementCommentType As MeasurementCommentType = MeasurementsDAL.GetMeasurementCommentType(MeasurementCommentTypeRouteName.FromRouteName)
        If IsNothing(MeasurementCommentType) Then
            Return HttpNotFound()
        End If

        Return View(getEditMeasurementCommentTypeViewModel(MeasurementCommentType))

    End Function

    Private Function getEditMeasurementCommentTypeViewModel(ByVal MeasurementCommentType As MeasurementCommentType)

        Return New EditMeasurementCommentTypeViewModel With {
            .MeasurementCommentType = MeasurementCommentType,
            .AllExcludedMeasurementViews = MeasurementsDAL.GetMeasurementViews,
            .AllExcludedAssessmentCriterionGroups = MeasurementsDAL.GetAssessmentCriterionGroups
        }

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditMeasurementCommentTypeViewModel) As ActionResult

        If Not UAL.CanEditMeasurementCommentTypes Then Return New HttpUnauthorizedResult()

        If ModelState.IsValid Then
            ' Update MeasurementCommentType
            MeasurementsDAL.UpdateMeasurementCommentType(ViewModel.MeasurementCommentType)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.MeasurementCommentTypeRouteName = ViewModel.MeasurementCommentType.getRouteName})
        End If

        Return View(ViewModel)

    End Function


    <HttpPut()> _
    Public Function AddExcludedMeasurementView(MeasurementCommentTypeRouteName As String, ExcludedMeasurementViewId As Integer) As ActionResult

        If Not UAL.CanEditMeasurementCommentTypes Then Return New HttpUnauthorizedResult()

        Dim MeasurementCommentType = MeasurementsDAL.GetMeasurementCommentType(MeasurementCommentTypeRouteName.FromRouteName)
        MeasurementsDAL.AddMeasurementCommentTypeExcludedMeasurementView(MeasurementCommentType.Id, ExcludedMeasurementViewId)
        Return Nothing

    End Function
    <HttpDelete()> _
    Public Function RemoveExcludedMeasurementView(MeasurementCommentTypeRouteName As String, ExcludedMeasurementViewId As Integer) As ActionResult

        If Not UAL.CanEditMeasurementCommentTypes Then Return New HttpUnauthorizedResult()

        Dim MeasurementCommentType = MeasurementsDAL.GetMeasurementCommentType(MeasurementCommentTypeRouteName.FromRouteName)
        MeasurementsDAL.RemoveMeasurementCommentTypeExcludedMeasurementView(MeasurementCommentType.Id, ExcludedMeasurementViewId)
        Return Nothing

    End Function

    <HttpPut()> _
    Public Function AddExcludedAssessmentCriterionGroup(MeasurementCommentTypeRouteName As String, ExcludedAssessmentCriterionGroupId As Integer) As ActionResult

        If Not UAL.CanEditMeasurementCommentTypes Then Return New HttpUnauthorizedResult()

        Dim MeasurementCommentType = MeasurementsDAL.GetMeasurementCommentType(MeasurementCommentTypeRouteName.FromRouteName)
        MeasurementsDAL.AddMeasurementCommentTypeExcludedAssessmentCriterionGroup(MeasurementCommentType.Id, ExcludedAssessmentCriterionGroupId)
        Return Nothing

    End Function
    <HttpDelete()> _
    Public Function RemoveExcludedAssessmentCriterionGroup(MeasurementCommentTypeRouteName As String, ExcludedAssessmentCriterionGroupId As Integer) As ActionResult

        If Not UAL.CanEditMeasurementCommentTypes Then Return New HttpUnauthorizedResult()

        Dim MeasurementCommentType = MeasurementsDAL.GetMeasurementCommentType(MeasurementCommentTypeRouteName.FromRouteName)
        MeasurementsDAL.RemoveMeasurementCommentTypeExcludedAssessmentCriterionGroup(MeasurementCommentType.Id, ExcludedAssessmentCriterionGroupId)
        Return Nothing

    End Function

#End Region

#Region "Create"

    <HttpGet()> _
    Public Function Create() As ActionResult

        If Not UAL.CanCreateMeasurementCommentTypes Then Return New HttpUnauthorizedResult()

        Return View(New MeasurementCommentType)

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ByVal MeasurementCommentType As MeasurementCommentType) As ActionResult

        If Not UAL.CanCreateMeasurementCommentTypes Then Return New HttpUnauthorizedResult()

        ' Check that MeasurementCommentTypeName does not already exist
        Dim existingMeasurementCommentTypeNames = MeasurementsDAL.GetMeasurementCommentTypes().Select(
            Function(mct) mct.CommentTypeName.ToRouteName().ToLower()
        ).ToList()
        If existingMeasurementCommentTypeNames.Contains(MeasurementCommentType.CommentTypeName.ToRouteName().ToLower()) Then
            ModelState.AddModelError("CommentTypeName", "Measurement Comment Type Name already exists!")
        End If

        If ModelState.IsValid Then
            ' Add Measurement Comment Type to database
            MeasurementsDAL.AddMeasurementCommentType(MeasurementCommentType)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.MeasurementCommentTypeRouteName = MeasurementCommentType.getRouteName})
        End If

        Return View(MeasurementCommentType)

    End Function

#End Region

#Region "Delete"

    <HttpPost()> _
    Public Function DeleteMeasurementCommentType(MeasurementCommentTypeId As Integer) As ActionResult

        If Not UAL.CanDeleteMeasurementCommentTypes Then Return New HttpUnauthorizedResult()

        Dim MeasurementCommentType = MeasurementsDAL.GetMeasurementCommentType(MeasurementCommentTypeId)
        If MeasurementCommentType Is Nothing Then Return Nothing
        MeasurementsDAL.DeleteMeasurementCommentType(MeasurementCommentTypeId)
        Return Json(New With {.redirectToUrl = Url.Action("Index")})

    End Function

#End Region

End Class