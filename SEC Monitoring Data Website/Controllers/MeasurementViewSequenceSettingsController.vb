Imports System.Data.Entity.Core
Imports libSEC

Public Class MeasurementViewSequenceSettingsController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Create"

    <HttpGet()> _
    Public Function Create(MeasurementViewRouteName As String, GroupIndex As Integer) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim MeasurementView = MeasurementsDAL.GetMeasurementView(MeasurementViewRouteName.FromRouteName)
        If MeasurementView Is Nothing Then Return HttpNotFound()
        If MeasurementView.getGroups.Count < GroupIndex Then Return HttpNotFound()

        Dim MeasurementViewGroup As MeasurementViewGroup = MeasurementView.getGroups.Single(Function(g) g.GroupIndex = GroupIndex)

        Dim nextIndex As Integer = MeasurementViewGroup.getSequenceSettings.Count + 1
        Dim calcFilters = MeasurementsDAL.GetCalculationFiltersForMeasurementType(MeasurementView.MeasurementTypeId)
        Dim seriesTypes = MeasurementsDAL.GetMeasurementViewSeriesTypes
        Dim dashStyles = MeasurementsDAL.GetSeriesDashStyles

        Dim vm As New CreateMeasurementViewSequenceSettingViewModel With {
            .MeasurementView = MeasurementView,
            .MeasurementViewGroup = MeasurementViewGroup,
            .MeasurementViewSequenceSetting = New MeasurementViewSequenceSetting With {.SequenceIndex = nextIndex},
            .CalculationFilterId = Nothing,
            .CalculationFilterList = New SelectList(calcFilters, "Id", "FilterName"),
            .DayViewSeriesDashStyleId = Nothing,
            .DayViewSeriesDashStyleList = New SelectList(dashStyles, "Id", "DashStyleName"),
            .DayViewSeriesTypeId = Nothing,
            .DayViewSeriesTypeList = New SelectList(seriesTypes, "Id", "SeriesTypeName"),
            .WeekViewSeriesDashStyleId = Nothing,
            .WeekViewSeriesDashStyleList = New SelectList(dashStyles, "Id", "DashStyleName"),
            .WeekViewSeriesTypeId = Nothing,
            .WeekViewSeriesTypeList = New SelectList(seriesTypes, "Id", "SeriesTypeName"),
            .MonthViewSeriesTypeId = Nothing,
            .MonthViewSeriesTypeList = New SelectList(seriesTypes, "Id", "SeriesTypeName"),
            .MonthViewSeriesDashStyleId = Nothing,
            .MonthViewSeriesDashStyleList = New SelectList(dashStyles, "Id", "DashStyleName")
        }

        Return View(vm)

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ByVal ViewModel As CreateMeasurementViewSequenceSettingViewModel) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        ' Remove Single Select Items
        ModelState.Remove("MeasurementViewSequenceSetting.CalculationFilter")
        ModelState.Remove("MeasurementViewSequenceSetting.DayViewSeriesType")
        ModelState.Remove("MeasurementViewSequenceSetting.WeekViewSeriesType")
        ModelState.Remove("MeasurementViewSequenceSetting.MonthViewSeriesType")
        ModelState.Remove("MeasurementViewSequenceSetting.DayViewDashStyle")
        ModelState.Remove("MeasurementViewSequenceSetting.WeekViewDashStyle")
        ModelState.Remove("MeasurementViewSequenceSetting.MonthViewDashStyle")
        ' Remove Parent Properties
        ModelState.Remove("MeasurementViewGroup.MainHeader")
        ModelState.Remove("MeasurementViewGroup.SubHeader")
        ModelState.Remove("MeasurementView.ViewName")
        ModelState.Remove("MeasurementView.TableResultsHeader")
        ModelState.Remove("MeasurementView.MeasurementType")
        ModelState.Remove("MeasurementView.DisplayName")

        If ModelState.IsValid Then
            ' Attach Relations
            ViewModel.MeasurementViewSequenceSetting.CalculationFilter = MeasurementsDAL.GetCalculationFilter(ViewModel.CalculationFilterId)
            ViewModel.MeasurementViewSequenceSetting.DayViewSeriesType = MeasurementsDAL.GetMeasurementViewSeriesType(ViewModel.DayViewSeriesTypeId)
            ViewModel.MeasurementViewSequenceSetting.WeekViewSeriesType = MeasurementsDAL.GetMeasurementViewSeriesType(ViewModel.WeekViewSeriesTypeId)
            ViewModel.MeasurementViewSequenceSetting.MonthViewSeriesType = MeasurementsDAL.GetMeasurementViewSeriesType(ViewModel.MonthViewSeriesTypeId)
            ViewModel.MeasurementViewSequenceSetting.DayViewDashStyle = MeasurementsDAL.GetSeriesDashStyle(ViewModel.DayViewSeriesDashStyleId)
            ViewModel.MeasurementViewSequenceSetting.WeekViewDashStyle = MeasurementsDAL.GetSeriesDashStyle(ViewModel.WeekViewSeriesDashStyleId)
            ViewModel.MeasurementViewSequenceSetting.MonthViewDashStyle = MeasurementsDAL.GetSeriesDashStyle(ViewModel.MonthViewSeriesDashStyleId)
            ' Attach Parent
            Dim MeasurementViewGroup = MeasurementsDAL.GetMeasurementViewGroup(ViewModel.MeasurementViewGroup.Id)
            ViewModel.MeasurementViewSequenceSetting.MeasurementViewGroup = MeasurementViewGroup
            ' Add Measurement View Sequence Setting to database
            MeasurementsDAL.AddMeasurementViewSequenceSetting(ViewModel.MeasurementViewSequenceSetting)
            ' Redirect to Measurement View Details
            Dim MeasurementView = MeasurementsDAL.GetMeasurementView(ViewModel.MeasurementView.Id)
            Return RedirectToRoute("MeasurementViewEditRoute",
                                   New With {.MeasurementViewRouteName = MeasurementView.getRouteName})
        End If

        Return View(ViewModel)

    End Function

#End Region

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(ByVal MeasurementViewRouteName As String, ByVal GroupIndex As Integer, ByVal SequenceIndex As Integer) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim MeasurementViewSequenceSetting As MeasurementViewSequenceSetting = MeasurementsDAL.GetMeasurementViewSequenceSetting(
            MeasurementViewRouteName.FromRouteName, GroupIndex, SequenceIndex
        )

        If IsNothing(MeasurementViewSequenceSetting) Then
            Return HttpNotFound()
        End If

        Return View(getEditMeasurementViewSequenceSettingViewModel(MeasurementViewSequenceSetting))

    End Function

    Private Function getEditMeasurementViewSequenceSettingViewModel(ByVal MeasurementViewSequenceSetting As MeasurementViewSequenceSetting)

        Dim measurementView = MeasurementViewSequenceSetting.MeasurementViewGroup.MeasurementView

        Dim calcFilters = MeasurementsDAL.GetCalculationFiltersForMeasurementType(
            measurementView.MeasurementTypeId
        )
        Dim seriesTypes = MeasurementsDAL.GetMeasurementViewSeriesTypes
        Dim dashStyles = MeasurementsDAL.GetSeriesDashStyles

        Return New EditMeasurementViewSequenceSettingViewModel With {
            .MeasurementViewSequenceSetting = MeasurementViewSequenceSetting,
            .MeasurementViewGroup = MeasurementViewSequenceSetting.MeasurementViewGroup,
            .MeasurementView = measurementView,
            .CalculationFilterId = MeasurementViewSequenceSetting.CalculationFilterId,
            .CalculationFilterList = New SelectList(
                calcFilters, "Id", "FilterName",
                MeasurementViewSequenceSetting.CalculationFilterId),
            .DayViewSeriesTypeId = MeasurementViewSequenceSetting.DayViewSeriesTypeId,
            .DayViewSeriesTypeList = New SelectList(
                seriesTypes, "Id", "SeriesTypeName",
                MeasurementViewSequenceSetting.DayViewSeriesTypeId),
            .WeekViewSeriesTypeId = MeasurementViewSequenceSetting.WeekViewSeriesTypeId,
            .WeekViewSeriesTypeList = New SelectList(
                seriesTypes, "Id", "SeriesTypeName",
                MeasurementViewSequenceSetting.WeekViewSeriesTypeId),
            .MonthViewSeriesTypeId = MeasurementViewSequenceSetting.MonthViewSeriesTypeId,
            .MonthViewSeriesTypeList = New SelectList(
                seriesTypes, "Id", "SeriesTypeName",
                MeasurementViewSequenceSetting.MonthViewSeriesTypeId),
            .DayViewSeriesDashStyleId = MeasurementViewSequenceSetting.DayViewDashStyleId,
            .DayViewSeriesDashStyleList = New SelectList(
                dashStyles, "Id", "DashStyleName",
                MeasurementViewSequenceSetting.DayViewDashStyleId),
            .WeekViewSeriesDashStyleId = MeasurementViewSequenceSetting.WeekViewDashStyleId,
            .WeekViewSeriesDashStyleList = New SelectList(
                dashStyles, "Id", "DashStyleName",
                MeasurementViewSequenceSetting.WeekViewDashStyleId),
            .MonthViewSeriesDashStyleId = MeasurementViewSequenceSetting.MonthViewDashStyleId,
            .MonthViewSeriesDashStyleList = New SelectList(
                dashStyles, "Id", "DashStyleName",
                MeasurementViewSequenceSetting.MonthViewDashStyleId)}

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditMeasurementViewSequenceSettingViewModel) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        ' Remove Single Select Items
        ModelState.Remove("MeasurementViewSequenceSetting.CalculationFilter")
        ModelState.Remove("MeasurementViewSequenceSetting.DayViewSeriesType")
        ModelState.Remove("MeasurementViewSequenceSetting.WeekViewSeriesType")
        ModelState.Remove("MeasurementViewSequenceSetting.MonthViewSeriesType")
        ModelState.Remove("MeasurementViewSequenceSetting.DayViewDashStyle")
        ModelState.Remove("MeasurementViewSequenceSetting.WeekViewDashStyle")
        ModelState.Remove("MeasurementViewSequenceSetting.MonthViewDashStyle")
        ' Remove Parent Properties
        ModelState.Remove("MeasurementViewGroup.MainHeader")
        ModelState.Remove("MeasurementViewGroup.SubHeader")
        ModelState.Remove("MeasurementView.ViewName")
        ModelState.Remove("MeasurementView.TableResultsHeader")
        ModelState.Remove("MeasurementView.MeasurementType")
        ModelState.Remove("MeasurementView.DisplayName")

        If ModelState.IsValid Then
            ' Attach Relations
            ViewModel.MeasurementViewSequenceSetting.CalculationFilter = MeasurementsDAL.GetCalculationFilter(ViewModel.CalculationFilterId)
            ViewModel.MeasurementViewSequenceSetting.DayViewSeriesType = MeasurementsDAL.GetMeasurementViewSeriesType(ViewModel.DayViewSeriesTypeId)
            ViewModel.MeasurementViewSequenceSetting.WeekViewSeriesType = MeasurementsDAL.GetMeasurementViewSeriesType(ViewModel.WeekViewSeriesTypeId)
            ViewModel.MeasurementViewSequenceSetting.MonthViewSeriesType = MeasurementsDAL.GetMeasurementViewSeriesType(ViewModel.MonthViewSeriesTypeId)
            ViewModel.MeasurementViewSequenceSetting.DayViewDashStyle = MeasurementsDAL.GetSeriesDashStyle(ViewModel.DayViewSeriesDashStyleId)
            ViewModel.MeasurementViewSequenceSetting.WeekViewDashStyle = MeasurementsDAL.GetSeriesDashStyle(ViewModel.WeekViewSeriesDashStyleId)
            ViewModel.MeasurementViewSequenceSetting.MonthViewDashStyle = MeasurementsDAL.GetSeriesDashStyle(ViewModel.MonthViewSeriesDashStyleId)
            ' Update MeasurementViewSequenceSetting
            MeasurementsDAL.UpdateMeasurementViewSequenceSetting(ViewModel.MeasurementViewSequenceSetting)
            ' Redirect to Measurement View Details
            Dim MeasurementView = MeasurementsDAL.GetMeasurementView(ViewModel.MeasurementView.Id)
            Return RedirectToRoute("MeasurementViewEditRoute", New With {.MeasurementViewRouteName = MeasurementView.getRouteName})
        End If

        Return View(getEditMeasurementViewSequenceSettingViewModel(ViewModel.MeasurementViewSequenceSetting))

    End Function


#End Region


End Class