Imports System.Data.Entity.Core
Imports libSEC

Public Class MeasurementMetricsController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Index"

    Public Function Index() As ActionResult

        If Not UAL.CanViewMeasurementMetricList Then Return New HttpUnauthorizedResult()

        Return View(getViewMeasurementMetricsViewModel)

    End Function

    Private Function getViewMeasurementMetricsViewModel(Optional searchText As String = "",
                                                        Optional measurementTypeId As Integer = 0) As ViewMeasurementMetricsViewModel

        Dim measurementMetrics = MeasurementsDAL.GetMeasurementMetrics
        Dim st = LCase(searchText)

        ' Filter by search text
        If searchText <> "" Then
            measurementMetrics = measurementMetrics.Where(
                Function(mm) LCase(mm.MetricName).Contains(st) Or
                             LCase(mm.MeasurementType.MeasurementTypeName).Contains(st)
                )
        End If

        ' Filter by MeasurementType
        If measurementTypeId > 0 Then
            measurementMetrics = measurementMetrics.Where(
                Function(mm) mm.MeasurementTypeId = measurementTypeId
                )
        End If

        setIndexLinks()

        Return New ViewMeasurementMetricsViewModel With {
            .MeasurementMetrics = measurementMetrics.ToList,
            .TableId = "measurementmetrics-table",
            .UpdateTableRouteName = "MeasurementMetricUpdateIndexTableRoute",
            .ObjectName = "MeasurementMetric",
            .ObjectDisplayName = "Measurement Metric",
            .NavigationButtons = getIndexNavigationButtons(),
            .MeasurementTypeId = 0,
            .MeasurementTypeList = MeasurementsDAL.GetMeasurementTypesSelectList(True)
        }

    End Function

    Private Sub setIndexLinks()

        ViewData("ShowMeasurementMetricLinks") = UAL.CanViewMeasurementMetricDetails
        ViewData("ShowDeleteMeasurementMetricLinks") = UAL.CanDeleteMeasurementMetrics

    End Sub
    Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

        Dim buttons = New List(Of NavigationButtonViewModel)

        If UAL.CanCreateMeasurementMetrics Then buttons.Add(GetCreateButton64("MeasurementMetric"))

        Return buttons

    End Function

    <HttpGet()> _
    Public Function UpdateIndexTable(SearchText As String, MeasurementTypeId As String) As PartialViewResult

        Dim mtid As Integer = 0
        If MeasurementTypeId <> "" Then mtid = CInt(MeasurementTypeId)

        Return PartialView("Index_Table",
                           getViewMeasurementMetricsViewModel(searchText:=SearchText,
                                                              measurementTypeId:=mtid).MeasurementMetrics)

    End Function

#End Region

#Region "Details"

    Public Function Details(MeasurementMetricRouteName As String) As ActionResult

        If Not UAL.CanViewMeasurementMetricDetails Then Return New HttpUnauthorizedResult()

        Dim MeasurementMetric = MeasurementsDAL.GetMeasurementMetric(MeasurementMetricRouteName.FromRouteName)

        If IsNothing(MeasurementMetric) Then
            Return HttpNotFound()
        End If

        Dim vm As New MeasurementMetricDetailsViewModel With {
            .MeasurementMetric = MeasurementMetric,
            .Tabs = getDetailsTabs(MeasurementMetric),
            .NavigationButtons = getDetailsNavigationButtons(MeasurementMetric)
        }

        setDetailsLinks()

        Return View(vm)

    End Function

    Private Sub setDetailsLinks()

        ViewData("ShowMeasurementMetricLinks") = UAL.CanViewMeasurementMetricDetails

    End Sub

    Private Function getDetailsNavigationButtons(measurementMetric As MeasurementMetric) As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons As New List(Of NavigationButtonViewModel)

        If UAL.CanViewMeasurementMetricList Then buttons.Add(measurementMetric.getIndexRouteButton64)
        buttons.Add(measurementMetric.getEditRouteButton64)
        If measurementMetric.canBeDeleted = True And UAL.CanDeleteMeasurementMetrics Then buttons.Add(measurementMetric.getDeleteRouteButton64)

        Return buttons

    End Function

    Private Function getDetailsTabs(measurementMetric As MeasurementMetric) As IEnumerable(Of TabViewModel)

        Dim tabs As New List(Of TabViewModel)

        ' Basic Details
        tabs.Add(TabViewModel.getDetailsTab("Basic Details", "MeasurementMetrics", measurementMetric))

        ' Calculation Filters
        If measurementMetric.CalculationFilters.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Calculation Filters", "MeasurementMetrics", measurementMetric))
        End If

        Return tabs

    End Function

#End Region

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(ByVal MeasurementMetricRouteName As String) As ActionResult

        If Not UAL.CanEditMeasurementMetrics Then Return New HttpUnauthorizedResult()

        Dim MeasurementMetric As MeasurementMetric = MeasurementsDAL.GetMeasurementMetric(MeasurementMetricRouteName.FromRouteName)
        If IsNothing(MeasurementMetric) Then
            Return HttpNotFound()
        End If

        Return View(getEditMeasurementMetricViewModel(MeasurementMetric))

    End Function

    Private Function getEditMeasurementMetricViewModel(ByVal MeasurementMetric As MeasurementMetric)

        Return New EditMeasurementMetricViewModel With {
            .MeasurementMetric = MeasurementMetric,
            .MeasurementTypeId = MeasurementMetric.MeasurementTypeId,
            .MeasurementTypeList = New SelectList(
                MeasurementsDAL.GetMeasurementTypes,
                "Id", "MeasurementTypeName",
                MeasurementMetric.MeasurementTypeId
            )
        }

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditMeasurementMetricViewModel) As ActionResult

        If Not UAL.CanEditMeasurementMetrics Then Return New HttpUnauthorizedResult()

        ModelState.Remove("MeasurementMetric.MeasurementType")

        If ModelState.IsValid Then
            ' Attach Relations
            ViewModel.MeasurementMetric.MeasurementType = MeasurementsDAL.GetMeasurementType(ViewModel.MeasurementTypeId)
            ' Update MeasurementMetric
            MeasurementsDAL.UpdateMeasurementMetric(ViewModel.MeasurementMetric)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.MeasurementMetricRouteName = ViewModel.MeasurementMetric.getRouteName})
        End If

        Return View(ViewModel)

    End Function


#End Region

#Region "Create"

    <HttpGet()> _
    Public Function Create() As ActionResult

        If Not UAL.CanCreateMeasurementMetrics Then Return New HttpUnauthorizedResult()

        Dim vm As New CreateMeasurementMetricViewModel With {
            .MeasurementMetric = New MeasurementMetric,
            .MeasurementTypeId = Nothing,
            .MeasurementTypeList = New SelectList(
                MeasurementsDAL.GetMeasurementTypes,
                "Id", "MeasurementTypeName"
            )
        }

        Return View(vm)

    End Function
    Private Function getCreateMeasurementMetricViewModel(Optional viewModel As CreateMeasurementMetricViewModel = Nothing) As CreateMeasurementMetricViewModel

        Dim measurementMetric As MeasurementMetric
        Dim measurementTypeId As Integer

        If viewModel Is Nothing Then
            measurementMetric = New MeasurementMetric
            measurementTypeId = Nothing
        Else
            measurementMetric = viewModel.MeasurementMetric
            measurementTypeId = viewModel.MeasurementTypeId
        End If

        Return New CreateMeasurementMetricViewModel With {
            .MeasurementMetric = measurementMetric,
            .MeasurementTypeId = measurementTypeId,
            .MeasurementTypeList = New SelectList(
                MeasurementsDAL.GetMeasurementTypes,
                "Id", "MeasurementTypeName"
            )
        }

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ByVal ViewModel As CreateMeasurementMetricViewModel) As ActionResult

        If Not UAL.CanCreateMeasurementMetrics Then Return New HttpUnauthorizedResult()

        ' Check that MetricName does not already exist
        Dim existingMeasurementMetricNames = MeasurementsDAL.GetMeasurementMetrics().Select(
            Function(mm) mm.MetricName.ToRouteName().ToLower()
        ).ToList()
        If existingMeasurementMetricNames.Contains(ViewModel.MeasurementMetric.MetricName.ToRouteName().ToLower()) Then
            ModelState.AddModelError("MeasurementMetric.MetricName", "Measurement Metric Name already exists!")
        End If

        ModelState.Remove("MeasurementMetric.MeasurementType")

        If ModelState.IsValid Then
            ' Attach Relations
            ViewModel.MeasurementMetric.MeasurementType = MeasurementsDAL.GetMeasurementType(ViewModel.MeasurementTypeId)
            ' Add Measurement Metric to database
            MeasurementsDAL.AddMeasurementMetric(ViewModel.MeasurementMetric)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.MeasurementMetricRouteName = ViewModel.MeasurementMetric.getRouteName})
        End If

        Return View(getCreateMeasurementMetricViewModel(ViewModel))

    End Function

#End Region

#Region "Delete"

    <HttpPost()> _
    Public Function DeleteMeasurementMetric(MeasurementMetricId As Integer) As ActionResult

        If Not UAL.CanDeleteMeasurementMetrics Then Return New HttpUnauthorizedResult()

        Dim MeasurementMetric = MeasurementsDAL.GetMeasurementMetric(MeasurementMetricId)
        If MeasurementMetric Is Nothing Then Return Nothing
        MeasurementsDAL.DeleteMeasurementMetric(MeasurementMetricId)
        Return Json(New With {.redirectToUrl = Url.Action("Index")})

    End Function

#End Region


End Class