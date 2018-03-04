Imports System.Data.Entity.Core
Imports libSEC
Imports System.ComponentModel.DataAnnotations

Public Class CalculationFiltersController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub


#Region "Index"

    Public Function Index() As ActionResult

        If Not UAL.CanViewCalculationFilterList Then Return New HttpUnauthorizedResult()
        Return View(getViewCalculationFiltersViewModel)

    End Function
    Private Function getViewCalculationFiltersViewModel(Optional searchText As String = "",
                                                        Optional measurementTypeId As Integer = 0) As ViewCalculationFiltersViewModel

        Dim calculationfilters = MeasurementsDAL.GetCalculationFilters
        Dim st = LCase(searchText)

        ' Filter by search text
        If searchText <> "" Then
            calculationfilters = calculationfilters.Where(
                Function(cf) LCase(cf.CalculationAggregateFunction.FunctionName).Contains(st) Or
                             LCase(cf.FilterName).Contains(st) Or
                             LCase(cf.MeasurementMetric.MetricName).Contains(st) Or
                             LCase(cf.MeasurementMetric.MeasurementType.MeasurementTypeName).Contains(st))
        End If

        ' Filter by MeasurementType
        If measurementTypeId > 0 Then
            calculationfilters = calculationfilters.Where(
                Function(cf) cf.MeasurementMetric.MeasurementTypeId = measurementTypeId
                )
        End If

        setIndexLinks()

        Return New ViewCalculationFiltersViewModel With {
            .CalculationFilters = calculationfilters.ToList,
            .TableId = "calculationfilters-table",
            .UpdateTableRouteName = "CalculationFilterUpdateIndexTableRoute",
            .ObjectName = "CalculationFilter",
            .ObjectDisplayName = "Calculation Filter",
            .NavigationButtons = getIndexNavigationButtons(),
            .MeasurementTypeId = 0,
            .MeasurementTypeList = MeasurementsDAL.GetMeasurementTypesSelectList(True)
        }

    End Function
    Private Function getIndexNavigationButtons() As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons As New List(Of NavigationButtonViewModel)

        If UAL.CanCreateCalculationFilters Then
            buttons.Add(GetCreateButton64("CalculationFilter"))
        End If

        Return buttons

    End Function
    <HttpGet()> _
    Public Function UpdateIndexTable(SearchText As String, MeasurementTypeId As String) As PartialViewResult

        Dim mtid As Integer = 0
        If MeasurementTypeId <> "" Then mtid = CInt(MeasurementTypeId)

        Return PartialView("Index_Table",
                           getViewCalculationFiltersViewModel(searchText:=SearchText,
                                                              measurementTypeId:=mtid).CalculationFilters)

    End Function

    Private Sub setIndexLinks()

        ViewData("ShowCalculationFilterLinks") = UAL.CanViewCalculationFilterDetails
        ViewData("ShowMeasurementMetricLinks") = UAL.CanViewMeasurementMetricDetails
        ViewData("ShowDeleteCalculationFilterLinks") = UAL.CanDeleteCalculationFilters

    End Sub

#End Region

#Region "Details"

    Public Function Details(CalculationFilterRouteName As String) As ActionResult

        If Not UAL.CanViewCalculationFilterDetails Then Return New HttpNotFoundResult()

        Dim CalculationFilter = MeasurementsDAL.GetCalculationFilter(CalculationFilterRouteName.FromRouteName)
        If IsNothing(CalculationFilter) Then
            Return HttpNotFound()
        End If

        setDetailsLinks()

        Return View(New CalculationFilterDetailsViewModel With {
                    .Tabs = getDetailsTabs(CalculationFilter),
                    .NavigationButtons = getDetailsNavigationButtons(CalculationFilter)
                }
        )

    End Function

    Private Function getDetailsNavigationButtons(calculationFilter As CalculationFilter) As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons As New List(Of NavigationButtonViewModel)

        If UAL.CanViewCalculationFilterList Then buttons.Add(calculationFilter.getIndexRouteButton64)
        If UAL.CanEditCalculationFilters Then buttons.Add(calculationFilter.getEditRouteButton64)
        If calculationFilter.canBeDeleted And UAL.CanDeleteCalculationFilters Then buttons.Add(calculationFilter.getDeleteRouteButton64)

        Return buttons

    End Function

    Private Function getDetailsTabs(calculationFilter As CalculationFilter) As IEnumerable(Of TabViewModel)

        Dim tabs As New List(Of TabViewModel)
        tabs.Add(TabViewModel.getDetailsTab("Basic Details", "CalculationFilters", calculationFilter))
        tabs.Add(TabViewModel.getDetailsTab("Days Of Week", "CalculationFilters", calculationFilter))

        Return tabs

    End Function

    Private Sub setDetailsLinks()

        ViewData("ShowMeasurementMetricLink") = UAL.CanViewMeasurementMetricDetails

    End Sub

#End Region

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(ByVal CalculationFilterRouteName As String) As ActionResult

        If Not UAL.CanEditCalculationFilters Then Return New HttpUnauthorizedResult()

        Dim CalculationFilter As CalculationFilter = MeasurementsDAL.GetCalculationFilter(CalculationFilterRouteName.FromRouteName)
        If IsNothing(CalculationFilter) Then
            Return HttpNotFound()
        End If

        Return View(getEditCalculationFilterViewModel(CalculationFilter))

    End Function

    Private Function getEditCalculationFilterViewModel(ByVal CalculationFilter As CalculationFilter)

        Return New EditCalculationFilterViewModel With {
            .CalculationFilter = CalculationFilter,
            .CalculationAggregateFunctionId = CalculationFilter.CalculationAggregateFunctionId,
            .CalculationAggregateFunctionList = New SelectList(MeasurementsDAL.GetCalculationAggregateFunctions, "Id", "FunctionName", CalculationFilter.CalculationAggregateFunctionId),
            .MeasurementMetricId = CalculationFilter.MeasurementMetricId,
            .MeasurementMetricList = New SelectList(MeasurementsDAL.GetMeasurementMetrics, "Id", "MetricName", CalculationFilter.MeasurementMetricId),
            .AllApplicableDaysOfWeek = MeasurementsDAL.GetDaysOfWeek,
            .TimeBase = Date.FromOADate(CalculationFilter.TimeBase),
            .TimeStep = Date.FromOADate(CalculationFilter.TimeStep),
            .TimeWindowStartTime = CalculationFilter.TimeWindowStartTime,
            .TimeWindowEndTime = CalculationFilter.TimeWindowEndTime
        }

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditCalculationFilterViewModel) As ActionResult

        If Not UAL.CanEditCalculationFilters Then Return New HttpUnauthorizedResult()

        ModelState.Remove("CalculationFilter.CalculationAggregateFunction")
        ModelState.Remove("CalculationFilter.MeasurementMetric")

        If ModelState.IsValid Then
            ' Attach Relations
            ViewModel.CalculationFilter.MeasurementMetric = MeasurementsDAL.GetMeasurementMetric(ViewModel.MeasurementMetricId)
            ViewModel.CalculationFilter.CalculationAggregateFunction = MeasurementsDAL.GetCalculationAggregateFunction(ViewModel.CalculationAggregateFunctionId)
            ' Attach Custom Properties
            ViewModel.CalculationFilter.TimeBase = ViewModel.TimeBase.TimeOnly.ToOADate
            ViewModel.CalculationFilter.TimeStep = ViewModel.TimeStep.TimeOnly.ToOADate
            ViewModel.CalculationFilter.TimeWindowStartTime = ViewModel.TimeWindowStartTime
            ViewModel.CalculationFilter.TimeWindowEndTime = ViewModel.TimeWindowEndTime
            ' Update CalculationFilter
            MeasurementsDAL.UpdateCalculationFilter(ViewModel.CalculationFilter)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.CalculationFilterRouteName = ViewModel.CalculationFilter.getRouteName})
        End If

        Return View(ViewModel)

    End Function


    <HttpPut()> _
    Public Function AddApplicableDayOfWeek(CalculationFilterRouteName As String, ApplicableDayOfWeekId As Integer) As ActionResult

        Dim CalculationFilter = MeasurementsDAL.GetCalculationFilter(CalculationFilterRouteName.FromRouteName)
        MeasurementsDAL.AddCalculationFilterApplicableDayOfWeek(CalculationFilter.Id, ApplicableDayOfWeekId)
        Return Nothing

    End Function
    <HttpDelete()> _
    Public Function RemoveApplicableDayOfWeek(CalculationFilterRouteName As String, ApplicableDayOfWeekId As Integer) As ActionResult

        Dim CalculationFilter = MeasurementsDAL.GetCalculationFilter(CalculationFilterRouteName.FromRouteName)
        MeasurementsDAL.RemoveCalculationFilterApplicableDayOfWeek(CalculationFilter.Id, ApplicableDayOfWeekId)
        Return Nothing

    End Function

#End Region

#Region "Create"

    <HttpGet()> _
    Public Function Create() As ActionResult

        If Not UAL.CanCreateCalculationFilters Then Return New HttpNotFoundResult()

        Return View(getCreateCalculationFilterViewModel())

    End Function

    Private Function getCreateCalculationFilterViewModel(Optional viewModel As CreateCalculationFilterViewModel = Nothing) As CreateCalculationFilterViewModel

        Dim calculationFilter As CalculationFilter
        Dim measurementMetricId As Integer
        Dim calculationAggregateFunctionId As Integer
        Dim timeBase As Date
        Dim timeStep As Date
        Dim timeWindowStartTime As Date
        Dim timeWindowEndTime As Date

        If viewModel Is Nothing Then
            calculationFilter = New CalculationFilter
            measurementMetricId = Nothing
            calculationAggregateFunctionId = Nothing
            timeBase = Date.FromOADate(0)
            timeStep = Date.FromOADate(0)
            timeWindowStartTime = Date.FromOADate(0)
            timeWindowEndTime = Date.FromOADate(0)
        Else
            calculationFilter = viewModel.CalculationFilter
            measurementMetricId = viewModel.MeasurementMetricId
            calculationAggregateFunctionId = viewModel.CalculationAggregateFunctionId
            timeBase = viewModel.TimeBase
            timeStep = viewModel.TimeStep
            timeWindowStartTime = viewModel.TimeWindowStartTime
            timeWindowEndTime = viewModel.TimeWindowEndTime
        End If

        Return New CreateCalculationFilterViewModel With {
            .CalculationFilter = calculationFilter,
            .MeasurementMetricId = measurementMetricId,
            .MeasurementMetricList = New SelectList(MeasurementsDAL.GetMeasurementMetrics, "Id", "MetricName"),
            .CalculationAggregateFunctionId = calculationAggregateFunctionId,
            .CalculationAggregateFunctionList = New SelectList(MeasurementsDAL.GetCalculationAggregateFunctions, "Id", "FunctionName"),
            .TimeBase = timeBase,
            .TimeStep = timeStep,
            .TimeWindowStartTime = timeWindowStartTime,
            .TimeWindowEndTime = timeWindowEndTime
        }

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ByVal ViewModel As CreateCalculationFilterViewModel) As ActionResult

        If Not UAL.CanCreateCalculationFilters Then Return New HttpNotFoundResult()

        ' Check that FilterName does not already exist
        Dim existingFilterNames = MeasurementsDAL.GetCalculationFilters().Select(Function(cf) cf.FilterName.ToRouteName().ToLower()).ToList()
        If existingFilterNames.Contains(ViewModel.CalculationFilter.FilterName.ToRouteName().ToLower()) Then
            ModelState.AddModelError("CalculationFilter.FilterName", "Calculation Filter Name already exists!")
        End If

        ModelState.Remove("CalculationFilter.MeasurementMetric")
        ModelState.Remove("CalculationFilter.CalculationAggregateFunction")

        If ModelState.IsValid Then
            ' Attach Relations
            ViewModel.CalculationFilter.MeasurementMetric = MeasurementsDAL.GetMeasurementMetric(ViewModel.MeasurementMetricId)
            ViewModel.CalculationFilter.CalculationAggregateFunction = MeasurementsDAL.GetCalculationAggregateFunction(ViewModel.CalculationAggregateFunctionId)
            ' Attach Custom Properties
            ViewModel.CalculationFilter.TimeBase = ViewModel.TimeBase.TimeOnly.ToOADate
            ViewModel.CalculationFilter.TimeStep = ViewModel.TimeStep.TimeOnly.ToOADate
            ViewModel.CalculationFilter.TimeWindowStartTime = ViewModel.TimeWindowStartTime
            ViewModel.CalculationFilter.TimeWindowEndTime = ViewModel.TimeWindowEndTime
            ' Add Applicable Days of Week
            Dim dbDaysofWeek = MeasurementsDAL.GetDaysOfWeek
            If ViewModel.AppliesOnMondays = True Then ViewModel.CalculationFilter.ApplicableDaysOfWeek.Add(dbDaysofWeek.Single(Function(d) d.DayName = "Monday"))
            If ViewModel.AppliesOnTuesdays = True Then ViewModel.CalculationFilter.ApplicableDaysOfWeek.Add(dbDaysofWeek.Single(Function(d) d.DayName = "Tuesday"))
            If ViewModel.AppliesOnWednesdays = True Then ViewModel.CalculationFilter.ApplicableDaysOfWeek.Add(dbDaysofWeek.Single(Function(d) d.DayName = "Wednesday"))
            If ViewModel.AppliesOnThursdays = True Then ViewModel.CalculationFilter.ApplicableDaysOfWeek.Add(dbDaysofWeek.Single(Function(d) d.DayName = "Thursday"))
            If ViewModel.AppliesOnFridays = True Then ViewModel.CalculationFilter.ApplicableDaysOfWeek.Add(dbDaysofWeek.Single(Function(d) d.DayName = "Friday"))
            If ViewModel.AppliesOnSaturdays = True Then ViewModel.CalculationFilter.ApplicableDaysOfWeek.Add(dbDaysofWeek.Single(Function(d) d.DayName = "Saturday"))
            If ViewModel.AppliesOnSundays = True Then ViewModel.CalculationFilter.ApplicableDaysOfWeek.Add(dbDaysofWeek.Single(Function(d) d.DayName = "Sunday"))
            If ViewModel.AppliesOnPublicHolidays = True Then ViewModel.CalculationFilter.ApplicableDaysOfWeek.Add(dbDaysofWeek.Single(Function(d) d.DayName = "Public Holiday"))
            ' Add Calculation Filter  to database
            MeasurementsDAL.AddCalculationFilter(ViewModel.CalculationFilter)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.CalculationFilterRouteName = ViewModel.CalculationFilter.getRouteName})
        End If

        Return View(getCreateCalculationFilterViewModel(ViewModel))

    End Function


#End Region

#Region "Delete"

    <HttpPost()> _
    Public Function DeleteCalculationFilter(CalculationFilterId As Integer) As ActionResult

        If Not UAL.CanDeleteCalculationFilters Then Return New HttpUnauthorizedResult()

        Dim CalculationFilter = MeasurementsDAL.GetCalculationFilter(CalculationFilterId)
        If CalculationFilter Is Nothing Then Return Nothing
        MeasurementsDAL.DeleteCalculationFilter(CalculationFilterId)
        Return Json(New With {.redirectToUrl = Url.Action("Index")})

    End Function

#End Region

End Class