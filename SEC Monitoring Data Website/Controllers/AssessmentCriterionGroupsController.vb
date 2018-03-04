Imports System.Data.Entity.Core
Imports libSEC
Imports System.Globalization
Imports DotNet.Highcharts
Imports DotNet.Highcharts.Options
Imports DotNet.Highcharts.Options.Point

Public Class AssessmentCriterionGroupsController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Criteria Groups"

#Region "Details"

    <HttpGet()> _
    Public Function Details(ProjectRouteName As String, AssessmentCriterionGroupRouteName As String) As ActionResult

        If (Not UAL.CanViewAssessmentCriteria Or
            Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

        Dim AssessmentCriterionGroup = MeasurementsDAL.GetAssessmentCriterionGroup(
            ProjectRouteName.FromRouteName,
            AssessmentCriterionGroupRouteName.FromRouteName
        )

        If IsNothing(AssessmentCriterionGroup) Then
            Return HttpNotFound()
        End If

        Dim MonitorLocationsWithCriteria = AssessmentCriterionGroup.AssessmentCriteria.Select(
            Function(ac) ac.MonitorLocation
            ).Distinct.OrderBy(
                Function(ml) ml.MonitorLocationName
            ).ToList

        Dim viewModel As New AssessmentCriterionGroupDetailsViewModel With {
            .AssessmentCriterionGroup = AssessmentCriterionGroup,
            .AssessmentCriterionGroupId = AssessmentCriterionGroup.Id,
            .MonitorLocationId = Nothing,
            .MonitorLocationList = New SelectList(MonitorLocationsWithCriteria, "Id", "MonitorLocationName"),
            .NavigationButtons = getAssessmentCriterionGroupDetailsNavigationButtons(AssessmentCriterionGroup)
            }

        setDetailsLinks(ProjectRouteName)

        Return View(viewModel)

    End Function

    Private Sub setDetailsLinks(ProjectRouteName As String)

        ViewData("ShowDetailsProjectLink") = (UAL.CanViewProjectDetails And CanAccessProject(ProjectRouteName))

    End Sub
    Private Function getAssessmentCriterionGroupDetailsNavigationButtons(assessmentCriterionGroup As AssessmentCriterionGroup) As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons As New List(Of NavigationButtonViewModel)

        If UAL.CanEditAssessmentCriteria Then
            buttons.Add(
                New NavigationButtonViewModel(
                    "Edit Group",
                    "AssessmentCriterionGroupEditRoute",
                    New With {.controller = "AssessmentCriterionGroups",
                            .ProjectRouteName = assessmentCriterionGroup.Project.getRouteName(),
                            .AssessmentCriterionGroupRouteName = assessmentCriterionGroup.getRouteName},
                    "sitewide-button-64 edit-button-64"
                )
            )
        End If

        Return buttons

    End Function

#End Region

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(ProjectRouteName As String, AssessmentCriterionGroupRouteName As String) As ActionResult

        If (Not UAL.CanEditAssessmentCriteria Or
            Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

        Dim AssessmentCriterionGroup = MeasurementsDAL.GetAssessmentCriterionGroup(
            ProjectRouteName.FromRouteName,
            AssessmentCriterionGroupRouteName.FromRouteName
        )
        If IsNothing(AssessmentCriterionGroup) Then
            Return HttpNotFound()
        End If
        Return View(getEditAssessmentCriterionGroupViewModel(AssessmentCriterionGroup))

    End Function
    Private Function getEditAssessmentCriterionGroupViewModel(ByVal AssessmentCriterionGroup As AssessmentCriterionGroup)

        Return New EditAssessmentCriterionGroupViewModel With {
                    .AssessmentCriterionGroup = AssessmentCriterionGroup,
                    .AssessmentCriterionGroupId = AssessmentCriterionGroup.Id,
                    .ThresholdAggregateDurationId = AssessmentCriterionGroup.ThresholdAggregateDurationId,
                    .ThresholdAggregateDurationList = New SelectList(
                        MeasurementsDAL.GetThresholdAggregateDurations,
                        "Id", "AggregateDurationName",
                        AssessmentCriterionGroup.ThresholdAggregateDurationId
                        ),
                    .AssessmentPeriodDurationTypeId = AssessmentCriterionGroup.AssessmentPeriodDurationTypeId,
                    .AssessmentPeriodDurationTypeList = New SelectList(
                        MeasurementsDAL.GetAssessmentPeriodDurationTypes,
                        "Id", "DurationTypeName",
                        AssessmentCriterionGroup.AssessmentPeriodDurationTypeId
                        ),
                    .NewCriterionCalculationFilterId = Nothing,
                    .NewCriterionCalculationFilterList = New SelectList(
                        MeasurementsDAL.GetCalculationFiltersForMeasurementType(AssessmentCriterionGroup.MeasurementTypeId),
                        "Id", "FilterName"
                        ),
                    .MonitorLocationId = Nothing,
                    .MonitorLocationList = New SelectList(
                        MeasurementsDAL.GetMonitorLocations.Where(
                            Function(l) l.ProjectId = AssessmentCriterionGroup.ProjectId AndAlso
                                        l.MeasurementTypeId = AssessmentCriterionGroup.MeasurementTypeId),
                            "Id", "MonitorLocationName"
                        ),
                    .ShowGraph = AssessmentCriterionGroup.ShowGraph,
                    .GraphTitle = AssessmentCriterionGroup.GraphTitle,
                    .GraphXAxisLabel = AssessmentCriterionGroup.GraphXAxisLabel,
                    .GraphYAxisLabel = AssessmentCriterionGroup.GraphYAxisLabel,
                    .GraphYAxisMin = AssessmentCriterionGroup.GraphYAxisMin,
                    .GraphYAxisMax = AssessmentCriterionGroup.GraphYAxisMax,
                    .GraphYAxisTickInterval = AssessmentCriterionGroup.GraphYAxisTickInterval,
                    .NumDateColumns = AssessmentCriterionGroup.NumDateColumns,
                    .DateColumn1Header = AssessmentCriterionGroup.DateColumn1Header,
                    .DateColumn1Format = AssessmentCriterionGroup.DateColumn1Format,
                    .DateColumn2Header = AssessmentCriterionGroup.DateColumn2Header,
                    .DateColumn2Format = AssessmentCriterionGroup.DateColumn2Format,
                    .MergeHeaderRow1 = AssessmentCriterionGroup.MergeHeaderRow1,
                    .MergeHeaderRow2 = AssessmentCriterionGroup.MergeHeaderRow2,
                    .MergeHeaderRow3 = AssessmentCriterionGroup.MergeHeaderRow3,
                    .ShowIndividualResults = AssessmentCriterionGroup.ShowIndividualResults,
                    .SumExceedancesAcrossCriteria = AssessmentCriterionGroup.SumExceedancesAcrossCriteria,
                    .SumPeriodExceedances = AssessmentCriterionGroup.SumPeriodExceedances,
                    .SumDaysWithExceedances = AssessmentCriterionGroup.SumDaysWithExceedances,
                    .SumDailyEvents = AssessmentCriterionGroup.SumDailyEvents,
                    .ShowSumTitles = AssessmentCriterionGroup.ShowSumTitles,
                    .NavigationButtons = {AssessmentCriterionGroup.Project.getDetailsRouteButton64("Back to Project")}.ToList
                }


    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditAssessmentCriterionGroupViewModel) As ActionResult

        If (Not UAL.CanEditAssessmentCriteria Or
            Not CanAccessProject(ViewModel.AssessmentCriterionGroup.ProjectId)) Then Return New HttpUnauthorizedResult()

        ModelState.Remove("AssessmentCriterionGroup.Project")
        ModelState.Remove("AssessmentCriterionGroup.MeasurementType")
        ModelState.Remove("AssessmentCriterionGroup.ThresholdAggregateDuration")
        ModelState.Remove("AssessmentCriterionGroup.AssessmentPeriodDurationType")

        If ModelState.IsValid Then
            ' Attach Relations
            ViewModel.AssessmentCriterionGroup.ThresholdAggregateDuration = MeasurementsDAL.GetThresholdAggregateDuration(
                ViewModel.ThresholdAggregateDurationId
            )
            ViewModel.AssessmentCriterionGroup.AssessmentPeriodDurationType = MeasurementsDAL.GetAssessmentPeriodDurationType(
                ViewModel.AssessmentPeriodDurationTypeId
            )
            ' Update AssessmentCriterionGroup
            MeasurementsDAL.UpdateAssessmentCriterionGroup(ViewModel.AssessmentCriterionGroup)
            ' Redirect to Details
            Dim Project = MeasurementsDAL.GetProject(ViewModel.AssessmentCriterionGroup.ProjectId)
            Return RedirectToAction("Details",
                                    New With {.ProjectRouteName = Project.getRouteName,
                                              .AssessmentCriterionGroupRouteName = ViewModel.AssessmentCriterionGroup.getRouteName})
        End If

        Return View(ViewModel)

    End Function
    Private Function getEditAssessmentCriteriaViewModel(MonitorLocationId As Integer, AssessmentCriterionGroupId As Integer) As EditAssessmentCriteriaViewModel

        Dim assessmentCriterionGroup = MeasurementsDAL.GetAssessmentCriterionGroup(AssessmentCriterionGroupId)
        Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(MonitorLocationId)

        ' Get assessment criteria directly from database so calculation filter is included
        Dim criteriaIds = monitorLocation.AssessmentCriteria.Where(
            Function(ac) ac.AssessmentCriterionGroupId = AssessmentCriterionGroupId
        ).Select(Function(c) c.Id).ToList
        Dim criteria = MeasurementsDAL.GetAssessmentCriteria(criteriaIds).ToList
        Dim newCriterionCalculationFilterList = New SelectList(
            MeasurementsDAL.GetCalculationFiltersForMeasurementType(
                assessmentCriterionGroup.MeasurementTypeId), "Id", "FilterName"
        )
        Dim thresholdTypeList = New SelectList(
            MeasurementsDAL.GetThresholdTypes, "Id", "ThresholdTypeName"
        )
        Dim seriesTypeList = New SelectList(
            MeasurementsDAL.GetMeasurementViewSeriesTypes, "Id", "SeriesTypeName"
        )
        Dim dashStyleList = New SelectList(
            MeasurementsDAL.GetSeriesDashStyles, "Id", "DashStyleName"
        )

        Dim vm = New EditAssessmentCriteriaViewModel With {
            .AssessmentCriterionGroupId = AssessmentCriterionGroupId,
            .Criteria = criteria,
            .DashStyleList = dashStyleList,
            .MonitorLocationId = MonitorLocationId,
            .NewCriterionCalculationFilterId = Nothing,
            .NewCriterionCalculationFilterList = newCriterionCalculationFilterList,
            .CopyFromMonitorLocationId = Nothing,
            .CopyFromMonitorLocationList = New SelectList(
                MeasurementsDAL.GetMonitorLocationsForProject(monitorLocation.ProjectId) _
                    .Where(Function(ml) ml.AssessmentCriteria.Count > 0 AndAlso
                                            ml.Id <> MonitorLocationId), "Id", "MonitorLocationName"
                ),
            .SeriesTypeList = seriesTypeList,
            .ThresholdTypeId = Nothing,
            .ThresholdTypeList = thresholdTypeList,
            .CreateAssessmentCriterionViewModel = New CreateAssessmentCriterionPopUpViewModel With {
                  .AssessmentCriterionGroupId = Nothing,
                  .CalculationFilterId = Nothing,
                  .CalculationFilterList = newCriterionCalculationFilterList,
                  .MonitorLocationId = MonitorLocationId,
                  .ThresholdTypeId = Nothing,
                  .ThresholdTypeList = thresholdTypeList,
                  .AssessedLevelDashStyleId = 1,
                  .AssessedLevelDashStyleList = dashStyleList,
                  .AssessedLevelSeriesTypeId = 1,
                  .AssessedLevelSeriesTypeList = seriesTypeList,
                  .AssessedLevelLineColour = "#000000",
                  .CriterionLevelDashStyleId = 1,
                  .CriterionLevelDashStyleList = dashStyleList,
                  .AssessedLevelSeriesName = "",
                  .CriterionLevelSeriesName = "",
                  .CriterionLevelLineColour = "#000000",
                  .AssessedLevelHeader1 = "",
                  .AssessedLevelHeader2 = "",
                  .AssessedLevelHeader3 = "",
                  .CriterionTriggerHeader1 = "",
                  .CriterionTriggerHeader2 = "",
                  .CriterionTriggerHeader3 = ""
                  }
            }

        Return vm

    End Function

#Region "Partial Views"

    <HttpGet()> _
    Public Function Edit_Criteria_MonitorLocationCriteria(MonitorLocationId As Integer, AssessmentCriterionGroupId As Integer) As PartialViewResult

        Return PartialView(getEditAssessmentCriteriaViewModel(MonitorLocationId, AssessmentCriterionGroupId))

    End Function
    <HttpPost()>
    <ValidateAntiForgeryToken()> _
    Public Function GetMonitorLocationCriteriaForEdit(MonitorLocationId As Integer, AssessmentCriterionGroupId As Integer) As PartialViewResult

        Dim ViewModel = getEditAssessmentCriteriaViewModel(MonitorLocationId, AssessmentCriterionGroupId)

        Return PartialView("Edit_Criteria_MonitorLocationCriteria", ViewModel)

    End Function
    <HttpGet()> _
    Public Function GetMonitorLocationCriteriaForDetails(MonitorLocationId As Integer, AssessmentCriterionGroupId As Integer) As PartialViewResult

        Dim MonitorLocation = MeasurementsDAL.GetMonitorLocation(MonitorLocationId)
        If MonitorLocation Is Nothing Then Return Nothing

        Dim ViewModel As New ViewMonitorLocationAssessmentCriteriaViewModel With {
            .MonitorLocation = MeasurementsDAL.GetMonitorLocation(MonitorLocationId),
            .AssessmentCriteria = MonitorLocation.AssessmentCriteria.Where(
                Function(ac) ac.AssessmentCriterionGroupId = AssessmentCriterionGroupId
            ).OrderBy(Function(ac) ac.CriterionIndex),
            .ShowCalculationFilterLinks = showMonitorLocationCriteriaCalculationFilterLinks()
        }

        setEditLinks()

        Return PartialView("Details_Criteria_MonitorLocationCriteria", ViewModel)

    End Function
    Private Sub setEditLinks()

        ViewData("ShowCalculationFilterLinks") = showMonitorLocationCriteriaCalculationFilterLinks()

    End Sub

    Private Function showMonitorLocationCriteriaCalculationFilterLinks() As Boolean

        Return UAL.CanViewCalculationFilterDetails

    End Function

#End Region


#Region "AssessmentCriteria"

    <HttpPost()>
    <ValidateAntiForgeryToken()> _
    Public Function DeleteAssessmentCriterion(DeleteAssessmentCriterionId As Integer) As PartialViewResult

        Dim AssessmentCriterion = MeasurementsDAL.GetAssessmentCriterion(DeleteAssessmentCriterionId)
        If AssessmentCriterion Is Nothing Then Return Nothing
        Dim AssessmentCriterionGroupId = AssessmentCriterion.AssessmentCriterionGroupId

        Dim MonitorLocation = AssessmentCriterion.MonitorLocation
        If MonitorLocation Is Nothing Then Return Nothing

        MeasurementsDAL.DeleteAssessmentCriterion(DeleteAssessmentCriterionId)

        Dim viewModel = getEditAssessmentCriteriaViewModel(MonitorLocation.Id, AssessmentCriterionGroupId)

        Return PartialView("Edit_Criteria_MonitorLocationCriteria", viewModel)

    End Function

    <HttpPost()>
    <ValidateAntiForgeryToken()> _
    Public Function CreateAssessmentCriterion(ViewModel As CreateAssessmentCriterionPopUpViewModel) As PartialViewResult

        ModelState.Remove("AssessmentCriterion.CalculationFilter")
        ModelState.Remove("AssessmentCriterion.ThresholdType")
        ModelState.Remove("AssessmentCriterion.AssessedLevelDashStyle")
        ModelState.Remove("AssessmentCriterion.AssessedLevelSeriesType")
        ModelState.Remove("AssessmentCriterion.CriterionLevelDashStyle")

        If ModelState.IsValid Then
            Dim acg = MeasurementsDAL.GetAssessmentCriterionGroup(ViewModel.AssessmentCriterionGroupId)
            Dim nextIndex = acg.AssessmentCriteria.Where(
                Function(ac) ac.MonitorLocationId = ViewModel.MonitorLocationId
            ).Count + 1
            Dim AssessmentCriterion = New AssessmentCriterion With {
                .AssessmentCriterionGroupId = ViewModel.AssessmentCriterionGroupId,
                .CalculationFilterId = ViewModel.CalculationFilterId,
                .MonitorLocationId = ViewModel.MonitorLocationId,
                .ThresholdRangeLower = ViewModel.ThresholdRangeLower,
                .ThresholdRangeUpper = ViewModel.ThresholdRangeUpper,
                .ThresholdTypeId = ViewModel.ThresholdTypeId,
                .RoundingDecimalPlaces = ViewModel.RoundingDecimalPlaces,
                .CriterionIndex = nextIndex,
                .PlotAssessedLevel = ViewModel.PlotAssessedLevel,
                .AssessedLevelSeriesName = ViewModel.AssessedLevelSeriesName,
                .AssessedLevelDashStyleId = ViewModel.AssessedLevelDashStyleId,
                .AssessedLevelLineWidth = ViewModel.AssessedLevelLineWidth,
                .AssessedLevelLineColour = ViewModel.AssessedLevelLineColour,
                .AssessedLevelSeriesTypeId = ViewModel.AssessedLevelSeriesTypeId,
                .AssessedLevelMarkersOn = ViewModel.AssessedLevelMarkersOn,
                .PlotCriterionLevel = ViewModel.PlotCriterionLevel,
                .CriterionLevelSeriesName = ViewModel.CriterionLevelSeriesName,
                .CriterionLevelDashStyleId = ViewModel.CriterionLevelDashStyleId,
                .CriterionLevelLineWidth = ViewModel.CriterionLevelLineWidth,
                .CriterionLevelLineColour = ViewModel.CriterionLevelLineColour,
                .TabulateAssessedLevels = ViewModel.TabulateAssessedLevels,
                .MergeAssessedLevels = ViewModel.MergeAssessedLevels,
                .AssessedLevelHeader1 = ViewModel.AssessedLevelHeader1,
                .AssessedLevelHeader2 = ViewModel.AssessedLevelHeader2,
                .AssessedLevelHeader3 = ViewModel.AssessedLevelHeader3,
                .TabulateCriterionTriggers = ViewModel.TabulateCriterionTriggers,
                .MergeCriterionTriggers = ViewModel.MergeCriterionTriggers,
                .CriterionTriggerHeader1 = ViewModel.CriterionTriggerHeader1,
                .CriterionTriggerHeader2 = ViewModel.CriterionTriggerHeader2,
                .CriterionTriggerHeader3 = ViewModel.CriterionTriggerHeader3
            }

            MeasurementsDAL.AddAssessmentCriterion(AssessmentCriterion)

        End If

        Dim vm = getEditAssessmentCriteriaViewModel(
            ViewModel.MonitorLocationId, ViewModel.AssessmentCriterionGroupId
            )

        Return PartialView("Edit_Criteria_MonitorLocationCriteria", vm)


    End Function

    <HttpPost()>
    <ValidateAntiForgeryToken()> _
    Public Function EditAssessmentCriterion(ViewModel As EditAssessmentCriterionPopUpViewModel) As PartialViewResult

        ModelState.Remove("AssessmentCriterion.CalculationFilter")
        ModelState.Remove("AssessmentCriterion.ThresholdType")
        ModelState.Remove("AssessmentCriterion.AssessedLevelDashStyle")
        ModelState.Remove("AssessmentCriterion.AssessedLevelSeriesType")
        ModelState.Remove("AssessmentCriterion.CriterionLevelDashStyle")

        If ModelState.IsValid Then
            Dim criterion = MeasurementsDAL.GetAssessmentCriterion(ViewModel.EditAssessmentCriterionId)
            ' Assessment
            criterion.CalculationFilterId = ViewModel.CalculationFilterId
            criterion.ThresholdRangeLower = ViewModel.ThresholdRangeLower
            criterion.ThresholdRangeUpper = ViewModel.ThresholdRangeUpper
            criterion.RoundingDecimalPlaces = ViewModel.RoundingDecimalPlaces
            criterion.ThresholdTypeId = ViewModel.ThresholdTypeId
            criterion.ThresholdType = MeasurementsDAL.GetThresholdType(ViewModel.ThresholdTypeId)
            ' Assessed Level Series
            criterion.PlotAssessedLevel = ViewModel.PlotAssessedLevel
            criterion.AssessedLevelSeriesName = ViewModel.AssessedLevelSeriesName
            criterion.AssessedLevelDashStyleId = ViewModel.AssessedLevelDashStyleId
            criterion.AssessedLevelLineWidth = ViewModel.AssessedLevelLineWidth
            criterion.AssessedLevelLineColour = ViewModel.AssessedLevelLineColour
            criterion.AssessedLevelSeriesTypeId = ViewModel.AssessedLevelSeriesTypeId
            criterion.AssessedLevelMarkersOn = ViewModel.AssessedLevelMarkersOn
            ' Criterion Level Series
            criterion.PlotCriterionLevel = ViewModel.PlotCriterionLevel
            criterion.CriterionLevelSeriesName = ViewModel.CriterionLevelSeriesName
            criterion.CriterionLevelDashStyleId = ViewModel.CriterionLevelDashStyleId
            criterion.CriterionLevelLineWidth = ViewModel.CriterionLevelLineWidth
            criterion.CriterionLevelLineColour = ViewModel.CriterionLevelLineColour
            ' Assessed Level Table
            criterion.TabulateAssessedLevels = ViewModel.TabulateAssessedLevels
            criterion.MergeAssessedLevels = ViewModel.MergeAssessedLevels
            criterion.AssessedLevelHeader1 = ViewModel.AssessedLevelHeader1
            criterion.AssessedLevelHeader2 = ViewModel.AssessedLevelHeader2
            criterion.AssessedLevelHeader3 = ViewModel.AssessedLevelHeader3
            ' Criterion Level Table
            criterion.TabulateCriterionTriggers = ViewModel.TabulateCriterionTriggers
            criterion.MergeCriterionTriggers = ViewModel.MergeCriterionTriggers
            criterion.CriterionTriggerHeader1 = ViewModel.CriterionTriggerHeader1
            criterion.CriterionTriggerHeader2 = ViewModel.CriterionTriggerHeader2
            criterion.CriterionTriggerHeader3 = ViewModel.CriterionTriggerHeader3
            ' Save criterion
            MeasurementsDAL.UpdateAssessmentCriterion(criterion)
        End If

        Dim vm = getEditAssessmentCriteriaViewModel(
            ViewModel.MonitorLocationId,
            ViewModel.AssessmentCriterionGroupId
        )

        Return PartialView("Edit_Criteria_MonitorLocationCriteria", vm)


    End Function
    <HttpGet()> _
    Public Function CopyCriteria(ToMonitorLocationId As Integer) As PartialViewResult

        Dim ToMonitorLocation = MeasurementsDAL.GetMonitorLocation(ToMonitorLocationId)
        If ToMonitorLocation Is Nothing Then Return Nothing

        Dim viewModel As New CopyAssessmentCriteriaViewModel With {
            .CopyToMonitorLocationId = ToMonitorLocationId,
            .CopyFromMonitorLocationId = Nothing,
            .CopyFromMonitorLocationList = MeasurementsDAL.GetMonitorLocationsForProject(
                ToMonitorLocation.ProjectId
            ).Where(
                Function(ml) ml.AssessmentCriteria.Count > 0 AndAlso
                             ml.Id <> ToMonitorLocationId AndAlso
                             ml.MeasurementTypeId = ToMonitorLocation.MeasurementTypeId
            )
        }

        Return PartialView(viewModel)

    End Function
    <HttpPost()>
    Public Function CopyCriteria(ViewModel As CopyAssessmentCriteriaViewModel) As PartialViewResult

        Dim srcLocation = MeasurementsDAL.GetMonitorLocation(ViewModel.CopyFromMonitorLocationId)

        For Each c In srcLocation.AssessmentCriteria.Where(
            Function(ac) ac.AssessmentCriterionGroupId = ViewModel.AssessmentCriterionGroupId
        )

            Dim AssessmentCriterion = New AssessmentCriterion With {
                .AssessmentCriterionGroupId = ViewModel.AssessmentCriterionGroupId,
                .MonitorLocationId = ViewModel.CopyToMonitorLocationId,
                .AssessedLevelDashStyleId = c.AssessedLevelDashStyleId,
                .AssessedLevelHeader1 = c.AssessedLevelHeader1,
                .AssessedLevelHeader2 = c.AssessedLevelHeader2,
                .AssessedLevelHeader3 = c.AssessedLevelHeader3,
                .AssessedLevelLineColour = c.AssessedLevelLineColour,
                .AssessedLevelLineWidth = c.AssessedLevelLineWidth,
                .AssessedLevelMarkersOn = c.AssessedLevelMarkersOn,
                .AssessedLevelSeriesName = c.AssessedLevelSeriesName,
                .AssessedLevelSeriesTypeId = c.AssessedLevelSeriesTypeId,
                .CalculationFilterId = c.CalculationFilterId,
                .CriterionIndex = c.CriterionIndex,
                .CriterionLevelDashStyleId = c.CriterionLevelDashStyleId,
                .CriterionLevelLineColour = c.CriterionLevelLineColour,
                .CriterionLevelLineWidth = c.CriterionLevelLineWidth,
                .CriterionLevelSeriesName = c.CriterionLevelSeriesName,
                .CriterionTriggerHeader1 = c.CriterionTriggerHeader1,
                .CriterionTriggerHeader2 = c.CriterionTriggerHeader2,
                .CriterionTriggerHeader3 = c.CriterionTriggerHeader3,
                .MergeAssessedLevels = c.MergeAssessedLevels,
                .MergeCriterionTriggers = c.MergeCriterionTriggers,
                .PlotAssessedLevel = c.PlotAssessedLevel,
                .PlotCriterionLevel = c.PlotCriterionLevel,
                .RoundingDecimalPlaces = c.RoundingDecimalPlaces,
                .TabulateAssessedLevels = c.TabulateAssessedLevels,
                .TabulateCriterionTriggers = c.TabulateCriterionTriggers,
                .ThresholdRangeLower = c.ThresholdRangeLower,
                .ThresholdRangeUpper = c.ThresholdRangeUpper,
                .ThresholdTypeId = c.ThresholdTypeId
            }

            MeasurementsDAL.AddAssessmentCriterion(AssessmentCriterion)

        Next

        Dim vm = getEditAssessmentCriteriaViewModel(ViewModel.CopyToMonitorLocationId,
                                                    ViewModel.AssessmentCriterionGroupId)

        Return PartialView("Edit_Criteria_MonitorLocationCriteria", vm)

    End Function
    <HttpPost()>
    Public Function MoveAssessmentCriterionUp(AssessmentCriterionId As Integer) As PartialViewResult

        Dim AssessmentCriterion = MeasurementsDAL.GetAssessmentCriterion(AssessmentCriterionId)
        If AssessmentCriterion Is Nothing Then Return Nothing
        Dim AssessmentCriterionGroupId = AssessmentCriterion.AssessmentCriterionGroupId

        Dim MonitorLocation = AssessmentCriterion.MonitorLocation
        If MonitorLocation Is Nothing Then Return Nothing

        MeasurementsDAL.DecreaseAssessmentCriterionIndex(AssessmentCriterionId)

        Dim viewModel = getEditAssessmentCriteriaViewModel(MonitorLocation.Id,
                                                           AssessmentCriterionGroupId)

        Return PartialView("Edit_Criteria_MonitorLocationCriteria",
                           viewModel)

    End Function
    <HttpPost()>
    Public Function MoveAssessmentCriterionDown(AssessmentCriterionId As Integer) As PartialViewResult

        Dim AssessmentCriterion = MeasurementsDAL.GetAssessmentCriterion(AssessmentCriterionId)
        If AssessmentCriterion Is Nothing Then Return Nothing
        Dim AssessmentCriterionGroupId = AssessmentCriterion.AssessmentCriterionGroupId

        Dim MonitorLocation = AssessmentCriterion.MonitorLocation
        If MonitorLocation Is Nothing Then Return Nothing

        MeasurementsDAL.IncreaseAssessmentCriterionIndex(AssessmentCriterionId)

        Dim viewModel = getEditAssessmentCriteriaViewModel(MonitorLocation.Id,
                                                           AssessmentCriterionGroupId)

        Return PartialView("Edit_Criteria_MonitorLocationCriteria",
                           viewModel)

    End Function

#End Region

#End Region

#Region "Create"

    <HttpGet()> _
    Public Function Create(ProjectRouteName As String) As ActionResult

        If (Not UAL.CanCreateAssessmentCriteria Or
            Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

        Dim Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)

        If Project Is Nothing Then Return HttpNotFound()

        Dim copyFromProjects = AllowedProjects.Where(
            Function(p) _
                p.Id <> Project.Id AndAlso
                p.AssessmentCriteria.Count > 0
        )

        Dim vm As New CreateAssessmentCriterionGroupViewModel With {
            .CreateNewAssessmentCriterionGroupViewModel = New CreateNewAssessmentCriterionGroupViewModel With {
                .AssessmentCriterionGroup = New AssessmentCriterionGroup With {.Project = Project, .ProjectId = Project.Id},
                .MeasurementTypeId = Nothing,
                .MeasurementTypeList = New SelectList(MeasurementsDAL.GetMeasurementTypes, "Id", "MeasurementTypeName"),
                .ThresholdAggregateDurationId = Nothing,
                .ThresholdAggregateDurationList = New SelectList(MeasurementsDAL.GetThresholdAggregateDurations, "Id", "AggregateDurationName"),
                .AssessmentPeriodDurationTypeId = Nothing,
                .AssessmentPeriodDurationTypeList = New SelectList(MeasurementsDAL.GetAssessmentPeriodDurationTypes, "Id", "DurationTypeName")
            },
            .CreateFromExistingCriterionGroupViewModel = New CreateFromExistingCriterionGroupViewModel With {
                .CopyToProjectId = Project.Id,
                .CreateFromExistingProjectId = Nothing,
                .CreateFromExistingProjectList = New SelectList(copyFromProjects, "Id", "FullName"),
                .CreateFromExistingAssessmentCriterionGroupId = Nothing,
                .CreateFromExistingAssessmentCriterionGroupList = New SelectList(String.Empty, "Id", "GroupName"),
                .CreateFromExistingMonitorLocationId = Nothing,
                .CreateFromExistingMonitorLocationList = New SelectList(String.Empty, "Id", "MonitorLocationName")
            }
        }

        Return View(vm)

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function CreateNew(ByVal ViewModel As CreateNewAssessmentCriterionGroupViewModel) As ActionResult

        If (Not UAL.CanCreateAssessmentCriteria Or
            Not CanAccessProject(ViewModel.AssessmentCriterionGroup.ProjectId)) Then Return New HttpUnauthorizedResult()

        ' Check that GroupName doesn't already exist in the Project
        Dim project = MeasurementsDAL.GetProject(ViewModel.AssessmentCriterionGroup.ProjectId)
        Dim existingGroupNames = project.AssessmentCriteria.Select(Function(acg) acg.GroupName).ToList
        If project.hasAssessmentGroupNamed(ViewModel.AssessmentCriterionGroup.GroupName) Then
            Return RedirectToAction("Create", New With {.ProjectRouteName = project.getRouteName})
        End If

        ModelState.Remove("AssessmentCriterionGroup.Project")
        ModelState.Remove("AssessmentCriterionGroup.MeasurementType")
        ModelState.Remove("AssessmentCriterionGroup.ThresholdAggregateDuration")
        ModelState.Remove("AssessmentCriterionGroup.AssessmentPeriodDurationType")

        If ModelState.IsValid Then
            ' Attach Relations
            ViewModel.AssessmentCriterionGroup.MeasurementType = MeasurementsDAL.GetMeasurementType(ViewModel.MeasurementTypeId)
            ViewModel.AssessmentCriterionGroup.ThresholdAggregateDuration = MeasurementsDAL.GetThresholdAggregateDuration(ViewModel.ThresholdAggregateDurationId)
            ViewModel.AssessmentCriterionGroup.AssessmentPeriodDurationType = MeasurementsDAL.GetAssessmentPeriodDurationType(ViewModel.AssessmentPeriodDurationTypeId)
            ViewModel.AssessmentCriterionGroup.Project = MeasurementsDAL.GetProject(ViewModel.AssessmentCriterionGroup.ProjectId)
            ' Add Assessment Criterion Group to database
            MeasurementsDAL.AddAssessmentCriterionGroup(ViewModel.AssessmentCriterionGroup)
            ' Redirect to Details
            Return RedirectToAction(
                "Details",
                New With {.ProjectRouteName = project.getRouteName,
                          .AssessmentCriterionGroupRouteName = ViewModel.AssessmentCriterionGroup.getRouteName}
            )
        End If

        Return Create(project.ShortName.ToRouteName)

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function CreateFromExisting(ByVal ViewModel As CreateFromExistingCriterionGroupViewModel) As ActionResult

        If (Not UAL.CanCreateAssessmentCriteria Or
            Not CanAccessProject(ViewModel.CopyToProjectId)) Then Return New HttpUnauthorizedResult()

        Dim ExistingGroup As AssessmentCriterionGroup = MeasurementsDAL.GetAssessmentCriterionGroup(
            ViewModel.CreateFromExistingAssessmentCriterionGroupId
        )
        Dim targetProject = MeasurementsDAL.GetProject(ViewModel.CopyToProjectId)
        ' Check that GroupName doesn't already exist in the Project
        If targetProject.hasAssessmentGroupNamed(ExistingGroup.GroupName) Then
            ModelState.AddModelError(
                "error", "A group named '" + ExistingGroup.GroupName + "' already exists in the Project. Please choose another name."
            )
            Return RedirectToAction("Create", New With {.ProjectRouteName = targetProject.getRouteName})
        End If

        If ModelState.IsValid Then
            ' Create Group
            Dim AssessmentCriterionGroup As New AssessmentCriterionGroup With {
                .AssessmentPeriodDurationCount = ExistingGroup.AssessmentPeriodDurationCount,
                .AssessmentPeriodDurationType = MeasurementsDAL.GetAssessmentPeriodDurationType(ExistingGroup.AssessmentPeriodDurationTypeId),
                .GroupName = ExistingGroup.GroupName,
                .MeasurementType = MeasurementsDAL.GetMeasurementType(ExistingGroup.MeasurementTypeId),
                .Project = MeasurementsDAL.GetProject(ViewModel.CopyToProjectId),
                .ThresholdAggregateDuration = MeasurementsDAL.GetThresholdAggregateDuration(ExistingGroup.ThresholdAggregateDurationId),
                .DateColumn1Format = ExistingGroup.DateColumn1Format,
                .DateColumn1Header = ExistingGroup.DateColumn1Header,
                .DateColumn2Format = ExistingGroup.DateColumn2Format,
                .DateColumn2Header = ExistingGroup.DateColumn2Header,
                .GraphTitle = ExistingGroup.GraphTitle,
                .GraphXAxisLabel = ExistingGroup.GraphXAxisLabel,
                .GraphYAxisLabel = ExistingGroup.GraphYAxisLabel,
                .GraphYAxisMax = ExistingGroup.GraphYAxisMax,
                .GraphYAxisMin = ExistingGroup.GraphYAxisMin,
                .GraphYAxisTickInterval = ExistingGroup.GraphYAxisTickInterval,
                .MergeHeaderRow1 = ExistingGroup.MergeHeaderRow1,
                .MergeHeaderRow2 = ExistingGroup.MergeHeaderRow2,
                .MergeHeaderRow3 = ExistingGroup.MergeHeaderRow3,
                .NumDateColumns = ExistingGroup.NumDateColumns,
                .ShowGraph = ExistingGroup.ShowGraph,
                .ShowIndividualResults = ExistingGroup.ShowIndividualResults,
                .ShowSumTitles = ExistingGroup.ShowSumTitles,
                .SumDailyEvents = ExistingGroup.SumDailyEvents,
                .SumDaysWithExceedances = ExistingGroup.SumDaysWithExceedances,
                .SumExceedancesAcrossCriteria = ExistingGroup.SumExceedancesAcrossCriteria,
                .SumPeriodExceedances = ExistingGroup.SumPeriodExceedances
                }
            ' Create copies of Assessment Criteria for each Monitor Location in the Project
            Dim sourceMonitorLocation = MeasurementsDAL.GetMonitorLocation(ViewModel.CreateFromExistingMonitorLocationId)
            Dim sourceAssessmentCriteria = sourceMonitorLocation.AssessmentCriteria.Where(
                Function(ac) ac.AssessmentCriterionGroupId = ViewModel.CreateFromExistingAssessmentCriterionGroupId
                )
            Dim targetMonitorLocations = MeasurementsDAL.GetMonitorLocationsForProject(targetProject.Id) _
                                                        .Where(Function(ml) ml.MeasurementTypeId = ExistingGroup.MeasurementTypeId)
            For Each monitorLocation In targetMonitorLocations
                For Each criterion In sourceMonitorLocation.AssessmentCriteria.Where(Function(ac) ac.AssessmentCriterionGroupId = ExistingGroup.Id)
                    AssessmentCriterionGroup.AssessmentCriteria.Add(
                        New AssessmentCriterion With {
                            .CalculationFilter = MeasurementsDAL.GetCalculationFilter(criterion.CalculationFilterId),
                            .MonitorLocation = MeasurementsDAL.GetMonitorLocation(monitorLocation.Id),
                            .ThresholdRangeLower = criterion.ThresholdRangeLower,
                            .ThresholdRangeUpper = criterion.ThresholdRangeUpper,
                            .RoundingDecimalPlaces = criterion.RoundingDecimalPlaces,
                            .ThresholdType = MeasurementsDAL.GetThresholdType(criterion.ThresholdTypeId),
                            .AssessedLevelDashStyle = MeasurementsDAL.GetSeriesDashStyle(criterion.AssessedLevelDashStyleId),
                            .AssessedLevelHeader1 = criterion.AssessedLevelHeader1,
                            .AssessedLevelHeader2 = criterion.AssessedLevelHeader2,
                            .AssessedLevelHeader3 = criterion.AssessedLevelHeader3,
                            .AssessedLevelLineColour = criterion.AssessedLevelLineColour,
                            .AssessedLevelLineWidth = criterion.AssessedLevelLineWidth,
                            .AssessedLevelMarkersOn = criterion.AssessedLevelMarkersOn,
                            .AssessedLevelSeriesName = criterion.AssessedLevelSeriesName,
                            .AssessedLevelSeriesType = MeasurementsDAL.GetMeasurementViewSeriesType(criterion.AssessedLevelSeriesTypeId),
                            .CriterionIndex = criterion.CriterionIndex,
                            .CriterionLevelDashStyle = MeasurementsDAL.GetSeriesDashStyle(criterion.CriterionLevelDashStyleId),
                            .CriterionLevelLineColour = criterion.CriterionLevelLineColour,
                            .CriterionLevelLineWidth = criterion.CriterionLevelLineWidth,
                            .CriterionLevelSeriesName = criterion.CriterionLevelSeriesName,
                            .CriterionTriggerHeader1 = criterion.CriterionTriggerHeader1,
                            .CriterionTriggerHeader2 = criterion.CriterionTriggerHeader2,
                            .CriterionTriggerHeader3 = criterion.CriterionTriggerHeader3,
                            .MergeAssessedLevels = criterion.MergeAssessedLevels,
                            .MergeCriterionTriggers = criterion.MergeCriterionTriggers,
                            .PlotAssessedLevel = criterion.PlotAssessedLevel,
                            .PlotCriterionLevel = criterion.PlotCriterionLevel,
                            .TabulateAssessedLevels = criterion.TabulateAssessedLevels,
                            .TabulateCriterionTriggers = criterion.TabulateCriterionTriggers
                            }
                        )
                Next
            Next

            ' Add Group to database
            MeasurementsDAL.AddAssessmentCriterionGroup(AssessmentCriterionGroup)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.ProjectRouteName = targetProject.getRouteName,
                                                         .AssessmentCriterionGroupRouteName = AssessmentCriterionGroup.getRouteName})

        End If

        Return RedirectToAction("Create", New With {.ProjectRouteName = targetProject.getRouteName})

    End Function

    <HttpPost()> _
    Public Function GetProjectAssessmentCriterionGroups(ProjectId As Integer) As JsonResult

        Dim ProjectAssessmentCriterionGroups = MeasurementsDAL.GetAssessmentCriterionGroupsForProject(ProjectId)
        Dim ProjectAssessmentCriterionGroupSelectListItems As New List(Of SelectListItem)

        For Each acg In ProjectAssessmentCriterionGroups
            ProjectAssessmentCriterionGroupSelectListItems.Add(New SelectListItem With {.Text = acg.GroupName, .Value = acg.Id.ToString})
        Next
        ProjectAssessmentCriterionGroupSelectListItems.Insert(
            0, New SelectListItem With {.Selected = True, .Text = "Please select an Assessment Group from the list", .Value = ""}
        )
        Return Json(New SelectList(ProjectAssessmentCriterionGroupSelectListItems, "Value", "Text"))

    End Function
    <HttpPost()> _
    Public Function GetAssessmentCriterionGroupMonitorLocations(AssessmentCriterionGroupId As Integer) As JsonResult

        Dim AssessmentCriterionGroupMonitorLocations = MeasurementsDAL.GetMonitorLocationsForAssessmentCriterionGroup(AssessmentCriterionGroupId)
        Dim AssessmentCriterionGroupMonitorLocationSelectListItems As New List(Of SelectListItem)

        For Each ml In AssessmentCriterionGroupMonitorLocations
            AssessmentCriterionGroupMonitorLocationSelectListItems.Add(New SelectListItem With {.Text = ml.MonitorLocationName, .Value = ml.Id.ToString})
        Next
        AssessmentCriterionGroupMonitorLocationSelectListItems.Insert(0, New SelectListItem With {.Selected = True, .Text = "Please select a Monitor Location from the list", .Value = ""})
        Return Json(New SelectList(AssessmentCriterionGroupMonitorLocationSelectListItems, "Value", "Text"))

    End Function

#End Region

#End Region

#Region "Assessment View"

    <ActionName("View")> _
    <HttpGet()> _
    Public Function Vue(ProjectRouteName As String, MonitorLocationRouteName As String,
                        Optional strAssessmentDate As String = "",
                        Optional startOrEnd As String = "Start",
                        Optional showComments As Boolean = True,
                        Optional showCriteriaTimePeriods As Boolean = True,
                        Optional showNonWorkingHours As Boolean = True,
                        Optional dynamicYMin As Boolean? = Nothing,
                        Optional dynamicYMax As Boolean? = Nothing) As ActionResult

        ' Check Permissions
        If Not CanAccessProject(ProjectRouteName) Then Return New HttpNotFoundResult()

        ' Validate inputs
        Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(
            ProjectRouteName.FromRouteName,
            MonitorLocationRouteName.FromRouteName
        )
        If monitorLocation Is Nothing Then Return HttpNotFound()

        ' Check for Assessment Criteria
        Dim monitorLocationAssessmentCriteria = monitorLocation.AssessmentCriteria
        If monitorLocationAssessmentCriteria.Count = 0 Then Return HttpNotFound()

        Dim assessmentCriterionGroups = monitorLocationAssessmentCriteria.Select(
            Function(acg) acg.AssessmentCriterionGroup
        ).Distinct.OrderBy(Function(acg) acg.GroupName).ToList()
        Dim selectedAssessmentCriterionGroup = assessmentCriterionGroups.First

        Dim assessmentCriterionGroup = MeasurementsDAL.GetAssessmentCriterionGroup(
            ProjectRouteName.FromRouteName,
            selectedAssessmentCriterionGroup.GroupName
        )
        If assessmentCriterionGroup Is Nothing Then Return HttpNotFound()

        Dim assessmentCriteria = assessmentCriterionGroup.AssessmentCriteria.Where(
            Function(ac) ac.MonitorLocationId = monitorLocation.Id
        ).Distinct.ToList

        ' Set Dynamic Y Axis Scaling
        If dynamicYMin Is Nothing Then
            Select Case monitorLocation.MeasurementType.MeasurementTypeName
                Case "Noise"
                    dynamicYMin = True
                Case Else
                    dynamicYMin = False
            End Select
        End If
        If dynamicYMax Is Nothing Then
            dynamicYMax = True
        End If


        ' Assessment Dates
        Dim firstMeasurementDate = monitorLocation.getFirstMeasurementStartDateTime.DateOnly
        Dim lastMeasurementDate = monitorLocation.getLastMeasurementStartDateTime.DateOnly
        Dim assessmentDate As Date
        If strAssessmentDate = "" Then
            assessmentDate = lastMeasurementDate
        Else
            Try
                assessmentDate = Date.ParseExact(
                    strAssessmentDate, "yyyy-MM-dd", CultureInfo.InvariantCulture
                )
            Catch ex As FormatException
                Return New HttpNotFoundResult(
                    "Error in specifying date for assessment. Please use 'yyyy-MM-dd'"
                )
            End Try
        End If
        assessmentDate = EarlierDate(assessmentDate, lastMeasurementDate)
        assessmentDate = LaterDate(assessmentDate, firstMeasurementDate)

        Dim vm As New ViewAssessmentViewModel With {
            .AssessmentCriteria = assessmentCriteria,
            .AssessmentCriterionGroup = selectedAssessmentCriterionGroup,
            .SelectedAssessmentCriterionGroup = selectedAssessmentCriterionGroup,
            .AssessmentCriterionGroups = assessmentCriterionGroups,
            .AssessmentDate = assessmentDate,
            .StartOrEnd = startOrEnd,
            .ShowComments = showComments,
            .ShowCriteriaTimePeriods = showCriteriaTimePeriods,
            .ShowNonWorkingHours = showNonWorkingHours,
            .DynamicYMin = CBool(dynamicYMin),
            .DynamicYMax = CBool(dynamicYMax),
            .MonitorLocation = monitorLocation,
            .MonitorLocationId = monitorLocation.Id,
            .NavigationButtons = getViewAssessmentCriteriaNavigationButtons(monitorLocation, assessmentDate),
            .FirstMeasurementDate = firstMeasurementDate,
            .LastMeasurementDate = lastMeasurementDate
        }

        Return View(vm)

    End Function

    <HttpGet()>
    Public Function ViewNavigationButtons(strMonitorLocationId As String, strAssessmentDate As String) As PartialViewResult

        Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(CInt(strMonitorLocationId))
        Dim assessmentViewDate = Date.ParseExact(strAssessmentDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture)
        Return PartialView("NavigationButtons", getViewAssessmentCriteriaNavigationButtons(monitorLocation, assessmentViewDate))

    End Function

    Private Function getViewAssessmentCriteriaNavigationButtons(monitorLocation As MonitorLocation,
                                                                Optional measurementViewDate As Nullable(Of Date) = Nothing) As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons As New List(Of NavigationButtonViewModel)

        buttons.Add(monitorLocation.getMeasurementsViewRouteButton64(measurementViewDate))
        buttons.Add(monitorLocation.getDetailsRouteButton("Back to Monitor Location"))
        buttons.Add(monitorLocation.Project.getDetailsRouteButton64("Back to Project"))

        Return buttons

    End Function

    <HttpGet()> _
    Public Function ViewTableAndGraph(MonitorLocationId As String, AssessmentCriterionGroupId As String,
                                      AssessmentDate As String, StartOrEnd As String,
                                      ShowComments As Boolean,
                                      ShowCriteriaTimePeriods As Boolean,
                                      ShowNonWorkingHours As Boolean,
                                      dynamicYMin As Boolean, dynamicYMax As Boolean) As PartialViewResult


        Dim mlId As Integer = CInt(Val(MonitorLocationId))
        Dim acgId As Integer = CInt(Val(AssessmentCriterionGroupId))
        Dim aDate = Date.ParseExact(AssessmentDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture)

        Dim vm = getViewAssessmentDataViewModel(
            mlId, acgId, AssessmentDate, StartOrEnd,
            ShowComments, ShowCriteriaTimePeriods, ShowNonWorkingHours,
            dynamicYMin, dynamicYMax
        )
        vm.Chart = AssessmentChart(vm)
        vm.Table = AssessmentTable(vm)

        Return PartialView("View_AssessmentGraphAndTable", vm)

    End Function
    <HttpGet()> _
    Public Function ViewTableAndGraphAjax(strMonitorLocationId As String,
                                          strAssessmentCriterionGroupId As String,
                                          strAssessmentDate As String, startOrEnd As String,
                                          showComments As Boolean,
                                          showCriteriaTimePeriods As Boolean,
                                          showNonWorkingHours As Boolean,
                                          dynamicYMin As Boolean, dynamicYMax As Boolean) As PartialViewResult

        If (strMonitorLocationId Is Nothing Or
            strAssessmentCriterionGroupId Is Nothing Or
            strAssessmentDate Is Nothing) Then Return Nothing

        Return ViewTableAndGraph(
            strMonitorLocationId, strAssessmentCriterionGroupId,
            strAssessmentDate, startOrEnd,
            showComments, showCriteriaTimePeriods, showNonWorkingHours,
            dynamicYMin, dynamicYMax
        )

    End Function

    Public Function getViewAssessmentDataViewModel(
        MonitorLocationId As Integer, AssessmentCriterionGroupId As Integer,
        AssessmentDate As Date, StartOrEnd As String,
        ShowComments As Boolean, ShowCriteriaTimePeriods As Boolean, ShowNonWorkingHours As Boolean,
        DynamicYMin As Boolean, DynamicYMax As Boolean
    ) As ViewAssessmentDataViewModel

        Dim startDate As Date, endDate As Date

        Dim debugEvents = New List(Of DebugEvent)

        ' Validate inputs
        debugEvents.Add(New DebugEvent("Validate inputs"))
        Dim assessmentCriterionGroup = MeasurementsDAL.GetAssessmentCriterionGroup(AssessmentCriterionGroupId)
        Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(MonitorLocationId)

        ' Set start and end dates for assessment
        debugEvents.Add(New DebugEvent("Set start and end dates"))
        If StartOrEnd = "Start" Then
            startDate = AssessmentDate
            endDate = assessmentCriterionGroup.getAssessmentEndDate(startDate)
        Else
            endDate = AssessmentDate
            startDate = assessmentCriterionGroup.getAssessmentStartDate(endDate)
        End If

        ' Get assessment criteria
        debugEvents.Add(New DebugEvent("Get assessment criteria"))
        Dim assessmentCriteriaIds = assessmentCriterionGroup.AssessmentCriteria.Where(
            Function(ac) ac.MonitorLocationId = MonitorLocationId
        ).Select(Function(ac) ac.Id).ToList
        Dim assessmentCriteria = MeasurementsDAL.GetAssessmentCriteria(assessmentCriteriaIds).ToList

        ' Get public holidays
        debugEvents.Add(New DebugEvent("Get public holidays"))
        Dim publicHolidays = MeasurementsDAL.GetPublicHolidays.ToList.Where(
            Function(ph) ph.CountryId = monitorLocation.Project.CountryId And
                         ph.HolidayDate.ToOADate >= startDate.ToOADate And
                         ph.HolidayDate.ToOADate <= endDate.ToOADate
        )

        ' Read all measurements in assessment period to minimise database calls
        debugEvents.Add(New DebugEvent("Read measurements in assessment period"))
        Dim assessmentMeasurements = MeasurementsDAL.GetMonitorLocationMeasurements(
            monitorLocation.Id, startDate, endDate.AddDays(2)
        )

        ' Create graphing results
        debugEvents.Add(New DebugEvent("Create graphing results"))
        Dim graphingMeasurements = assessmentMeasurements
        Dim graphingResults As New List(Of FilteredMeasurementsSequence)
        For Each ac In assessmentCriteria
            Dim calcFilter = MeasurementsDAL.GetCalculationFilter(ac.CalculationFilterId)
            Dim criterionGraphingMeasurements = graphingMeasurements.Where(
                Function(m) m.MeasurementMetricId = calcFilter.MeasurementMetricId
            ).ToList.ApplyCalculationFilter(calcFilter)
            graphingResults.Add(
                New FilteredMeasurementsSequence(criterionGraphingMeasurements, calcFilter)
            )
        Next

        ' Get assessment dates based on project working days
        debugEvents.Add(New DebugEvent("Get assessment dates based on working days"))
        Dim project = MeasurementsDAL.GetProject(monitorLocation.ProjectId)
        Dim aggregateDurationDates As List(Of Date)
        Select Case assessmentCriterionGroup.ThresholdAggregateDuration.AggregateDurationName
            Case "Working Day"
                aggregateDurationDates = project.getWorkingDaysBetween(startDate, endDate)
            Case "Day"
                aggregateDurationDates = DateList(startDate, endDate, TimeResolutionType.Day)
            Case Else
                Throw New NotSupportedException(
                    "Invalid Aggregate Duration Name - expected 'Working Day' or 'Day'"
                )
        End Select

        ' Remove measurements which took place outside of the project's working hours
        debugEvents.Add(New DebugEvent("Remove measurements outside working hours"))
        Dim assessmentPeriodDates As List(Of Date)
        assessmentPeriodDates = DateList(startDate, endDate, TimeResolutionType.Day)
        If assessmentCriterionGroup.ThresholdAggregateDuration.AggregateDurationName = "Working Day" Then
            Dim workingPeriods = project.getWorkingHours(assessmentPeriodDates)
            assessmentMeasurements = assessmentMeasurements.FilterDateTimeRanges(workingPeriods)
        End If

        ' Add comments if selected
        debugEvents.Add(New DebugEvent("Add comments if selected"))
        Dim measurementComments = New List(Of MeasurementComment)
        If ShowComments Then
            measurementComments = MeasurementsDAL.GetMeasurementComments.Where(
                Function(c) c.MonitorLocationId = MonitorLocationId
            ).ToList
        End If

        ' Add working periods
        debugEvents.Add(New DebugEvent("Add working periods"))
        Dim nonWorkingHours As New List(Of DateTimeRange)
        If ShowNonWorkingHours Then
            nonWorkingHours = project.getNonWorkingHours(assessmentPeriodDates)
        End If

        ' Remove measurements having a comment which is excluded from the current assessment criterion group
        debugEvents.Add(New DebugEvent("Remove measurements with excluding comments"))
        If ShowComments Then
            Dim excludingCommentTypeIds = assessmentCriterionGroup.ExcludingMeasurementCommentTypes.Select(Function(mct) mct.Id).ToList
            Dim excludingCommentIds = MeasurementsDAL.GetCommentTypeCommentIds(excludingCommentTypeIds)
            Dim excludedMeasurementIds = MeasurementsDAL.GetCommentMeasurementIds(excludingCommentIds)
            assessmentMeasurements = assessmentMeasurements.Where(
                Function(m) excludedMeasurementIds.Contains(m.Id) = False
            ).ToList
        End If

        ' Calculate assessment results
        debugEvents.Add(New DebugEvent("Calculate assessment results"))
        Dim assessmentResults As New List(Of List(Of AssessmentResultSingleDay))
        For Each ac In assessmentCriteria
            Dim calcFilter = MeasurementsDAL.GetCalculationFilter(ac.CalculationFilterId)
            Dim assessmentMetricMeasurements = assessmentMeasurements.Where(
                Function(m) m.MeasurementMetricId = calcFilter.MeasurementMetricId
            ).ToList
            Dim criterionAssessmentResults = GetThresholdExceedances(
                Measurements:=assessmentMetricMeasurements,
                CalculationFilter:=calcFilter,
                ThresholdRangeLower:=ac.ThresholdRangeLower,
                ThresholdRangeUpper:=ac.ThresholdRangeUpper,
                ThresholdTypeName:=ac.ThresholdType.ThresholdTypeName,
                AssessmentDates:=aggregateDurationDates,
                RoundingDecimalPlaces:=ac.RoundingDecimalPlaces
            )
            assessmentResults.Add(criterionAssessmentResults)
        Next

        debugEvents.Add(New DebugEvent("Create view model"))
        Dim vm As New ViewAssessmentDataViewModel With {
            .AssessmentCriteria = assessmentCriteria,
            .AssessmentCriterionGroup = assessmentCriterionGroup,
            .Dates = aggregateDurationDates,
            .AssessmentResults = assessmentResults,
            .GraphingResults = graphingResults,
            .AssessmentDate = AssessmentDate,
            .StartOrEnd = StartOrEnd,
            .MeasurementComments = measurementComments,
            .NumCriteria = assessmentCriteria.Count,
            .LevelRoundingDP = assessmentCriteria.First.CalculationFilter.MeasurementMetric.RoundingDecimalPlaces,
            .MonitorLocation = monitorLocation,
            .ShowCriteriaTimePeriods = ShowCriteriaTimePeriods,
            .ShowNonWorkingHours = ShowNonWorkingHours,
            .DynamicYMin = DynamicYMin,
            .DynamicYMax = DynamicYMax,
            .NonWorkingHours = nonWorkingHours
        }

        debugEvents.Add(New DebugEvent("Return view model"))

        'If UAL.AccessLevelName = "Super User" Then
        '    For i = 0 To debugEvents.Count - 1
        '        Response.Write(debugEvents(i).dt.ToString + ": " + debugEvents(i).name + "<br />")
        '    Next
        'End If

        Return vm

    End Function


    <HttpPost()> _
<ValidateAntiForgeryToken()> _
    Public Function DownloadTable(MonitorLocationId As Integer, AssessmentCriterionGroupId As Integer,
                                  strAssessmentDate As String, StartOrEnd As String) As FileResult

        'build the content for the dynamic Word document
        'in HTML alongwith some Office specific style properties. 
        Dim strBody As New System.Text.StringBuilder("")

        strBody.Append("<html xmlns:o='urn:schemas-microsoft-com:office:office' " & _
                       "xmlns:w='urn:schemas-microsoft-com:office:word'xmlns='http://www.w3.org/TR/REC-html40'><head><title>Time</title>")

        'The setting specifies document's view after it is downloaded as Print instead of the default Web Layout
        strBody.Append("<!--[if gte mso 9]><xml><w:WordDocument><w:View>Print</w:View><w:Zoom>90</w:Zoom><w:DoNotOptimizeForBrowser/></w:WordDocument></xml><![endif]-->")
        strBody.Append("<style>" & _
                       "<!-- /* Style Definitions */" & _
                       "@page Section1" & _
                       "   {size:8.27in 11.69in; " & _
                       "   margin:1.0in 1.25in 1.0in 1.25in ; " & _
                       "   mso-header-margin:.5in; " & _
                       "   mso-footer-margin:.5in; mso-paper-source:0;}" & _
                       " div.Section1" & _
                       "   {page:Section1;}" & _
                       "-->" & _
                       "</style></head>")

        Dim assessmentDate = Date.ParseExact(strAssessmentDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture)
        Dim vm = getViewAssessmentDataViewModel(
            MonitorLocationId, AssessmentCriterionGroupId, assessmentDate, StartOrEnd,
            False, False, False, False, False
        )
        Dim table = AssessmentTable(vm)
        Dim group = vm.AssessmentCriterionGroup

        strBody.Append("<body lang=EN-US style='tab-interval:.5in'><div class=Section1>")

        strBody.Append("<table style='border: solid 1px; padding: 5px; text-align:center; border-collapse: collapse; font-family: Arial;'>")
        ' table head
        strBody.Append("<thead>")
        For Each row In table.Header.Rows
            strBody.Append("<tr>")
            For Each cell In row.Cells
                strBody.Append(cell.ToTheadFootTh())
            Next
            strBody.Append("</tr>")
        Next
        strBody.Append("</thead>")
        'table body
        strBody.Append("<tbody>")
        For Each row In table.Body.Rows
            strBody.Append("<tr>")
            ' date
            For c = table.FirstDateColumnIndex() To table.LastDateColumnIndex()
                Dim cell = row.Cells(c)
                strBody.Append(cell.ToTbodyTh(cellWidth:="width: 20%;"))
            Next c
            ' assessed levels
            For c = table.FirstLevelColumnIndex() To table.LastLevelColumnIndex()
                Dim cell = row.Cells(c)
                strBody.Append(cell.ToTBodyTd())
            Next
            ' triggers
            For c = table.FirstTriggerColumnIndex To table.LastTriggerColumnIndex
                Dim cell = row.Cells(c)
                strBody.Append(cell.ToTBodyTd())
            Next
            ' daily total
            If group.SumExceedancesAcrossCriteria Then
                Dim cell = row.Cells(table.DailySumColumnIndex)
                strBody.Append(cell.ToTbodyTh())
            End If
            strBody.Append("</tr>")
        Next row
        strBody.Append("</tbody>")
        ' table foot
        strBody.Append("<tfoot>")
        For Each row In table.Footer.Rows
            strBody.Append("<tr>")
            For Each cell In row.Cells
                strBody.Append(cell.ToTheadFootTh())
            Next
            strBody.Append("</tr>")
        Next row
        strBody.Append("</tfoot>")
        strBody.Append("</table>")
        strBody.Append("</div></body></html>")

        'Force this content to be downloaded as a Word document with the name of your choice
        Dim fileName = String.Format("{0}_{1}_{2}_{3}", {group.GroupName, vm.MonitorLocation.MonitorLocationName, StartOrEnd, strAssessmentDate})
        Response.AppendHeader("Content-Type", "application/msword")
        Response.AppendHeader("Content-disposition",
                              String.Format("attachment; filename={0}.doc", {fileName}))
        Response.Write(strBody)

    End Function

    Private Function AssessmentTable(model As ViewAssessmentDataViewModel) As AssessmentTableViewModel

        ' Build Header
        Dim header = New AssessmentTableHeader(
            group:=model.AssessmentCriterionGroup,
            assessmentResults:=model.AssessmentResults,
            assessmentCriteria:=model.AssessmentCriteria
        )
        ' Build Body
        Dim body = New AssessmentTableBody(
            group:=model.AssessmentCriterionGroup,
            assessmentResults:=model.AssessmentResults,
            assessmentCriteria:=model.AssessmentCriteria,
            dates:=model.Dates
        )
        ' Build Footer
        Dim footer = New AssessmentTableFooter(
            group:=model.AssessmentCriterionGroup,
            body:=body,
            assessmentResults:=model.AssessmentResults,
            assessmentCriteria:=model.AssessmentCriteria,
            dates:=model.Dates
        )
        ' Build Table
        Dim tbl = New AssessmentTableViewModel(
            group:=model.AssessmentCriterionGroup,
            header:=header,
            body:=body,
            footer:=footer
        )

        Return tbl

    End Function
    Private Function replaceText(title As String, model As ViewAssessmentDataViewModel) As String

        Return title.Replace(
            "$MonitorLocationName$", model.MonitorLocation.MonitorLocationName
        ).Replace(
            "$Project.FullName$", model.MonitorLocation.Project.FullName
        ).Replace(
            "$Project.ShortName$", model.MonitorLocation.Project.ShortName
        ).Replace(
            "$Project.ProjectNumber$", model.MonitorLocation.Project.ProjectNumber
        )

    End Function
    Private Function AssessmentChart(model As ViewAssessmentDataViewModel) As Highcharts

        Dim startDate As Date
        Dim endDate As Date

        ' Calculate start and end dates
        Dim group = model.AssessmentCriterionGroup
        If model.StartOrEnd = "Start" Then
            startDate = model.AssessmentDate
            endDate = group.getAssessmentEndDate(startDate)
        Else
            endDate = model.AssessmentDate
            startDate = group.getAssessmentStartDate(endDate)
        End If

        ' Create view data for chart
        Dim chart As New Highcharts("AssessmentChart")
        chart.InitChart(GetInitValues) _
             .SetOptions(GetOptions) _
             .SetSubtitle(GetSubTitle) _
             .SetLegend(GetLegend) _
             .SetTooltip(GetToolTip) _
             .SetLabels(GetLabels) _
             .SetPlotOptions(GetPlotOptions)
        chart.SetExporting(GetExporting("View Assessment Chart"))
        chart.SetTitle(GetTitle(
            replaceText(group.GraphTitle, model)
        ))
        chart.SetCredits(GetCredits("", ""))

        ' Set axes
        Dim tickIntervalMillis = GetTickIntervalMillis(startDate, endDate.AddDays(1))

        chart.SetYAxis(GetYAxis(
            replaceText(group.GraphYAxisLabel, model),
            TickInterval:=group.GraphYAxisTickInterval,
            MinValue:=IIf(model.DynamicYMin = False, group.GraphYAxisMin, Nothing),
            MaxValue:=IIf(model.DynamicYMax = False, group.GraphYAxisMax, Nothing)
        ))

        chart.SetXAxis(GetDateTimeAxis(
            axisTitle:=replaceText(group.GraphXAxisLabel, model),
            startDateTime:=startDate,
            endDateTime:=endDate.AddDays(1),
            tickIntervalMillis:=tickIntervalMillis,
            comments:=model.MeasurementComments,
            nonWorkingHours:=model.NonWorkingHours
        ))

        ' Add Measurement Series
        Dim markersOn = New PlotOptionsSeriesMarker With {.Enabled = True}
        Dim markersOff = New PlotOptionsSeriesMarker With {.Enabled = False}
        Dim seriesList As New List(Of Series)
        For c = 0 To model.AssessmentResults.Count - 1
            ' Build a FilteredMeasurementsSequence
            Dim criterionResults = model.AssessmentResults(c)
            Dim criterion = model.AssessmentCriteria(c)
            Dim filteredMeasurements = New List(Of FilteredMeasurements)
            For Each arsd As AssessmentResultSingleDay In criterionResults
                filteredMeasurements.AddRange(arsd.getFilteredMeasurements())
            Next
            Dim fms = model.GraphingResults(c)
            If fms.Count > 0 And criterion.PlotAssessedLevel Then
                ' Set series plot options
                Dim seriesType = model.AssessmentCriteria(c).AssessedLevelSeriesType.SeriesTypeName
                Dim seriesName = FixHeader(criterion.AssessedLevelSeriesName, criterion)
                Dim seriesColour = Drawing.ColorTranslator.FromHtml(criterion.AssessedLevelLineColour)
                Dim lineWidth = criterion.AssessedLevelLineWidth
                Dim dashStyle = criterion.AssessedLevelDashStyle.DashStyleEnum
                Dim ndp = criterion.RoundingDecimalPlaces
                Dim marker As PlotOptionsSeriesMarker
                If criterion.AssessedLevelMarkersOn Then
                    marker = markersOn
                Else
                    marker = markersOff
                End If
                Dim poSeries = New PlotOptionsSeries With {
                    .TurboThreshold = 0,
                    .LineWidth = lineWidth,
                    .DashStyle = dashStyle,
                    .Marker = marker
                }
                ' Create series
                Dim data As DotNet.Highcharts.Helpers.Data = Nothing
                Dim chartType As DotNet.Highcharts.Enums.ChartTypes
                Select Case seriesType
                    Case "Line"
                        data = fms.GetLineSeriesData(ndp)
                        chartType = Enums.ChartTypes.Line
                    Case "Step Line"
                        data = fms.GetStepLineSeriesData(ndp)
                        chartType = Enums.ChartTypes.Line
                    Case "Summary Line"
                        data = fms.GetSummaryLineSeriesData(ndp)
                        chartType = Enums.ChartTypes.Line
                    Case "Area"
                        data = fms.GetAreaSeriesData(ndp)
                        chartType = Enums.ChartTypes.Area
                    Case "Column"
                        Dim calcFilter = fms.getFilter
                        Dim plotFilter = New CalculationFilter With {
                            .TimeBase = calcFilter.TimeBase,
                            .TimeStep = calcFilter.TimeStep,
                            .TimeWindowEndTime = calcFilter.TimeWindowEndTime,
                            .TimeWindowStartTime = calcFilter.TimeWindowStartTime,
                            .UseTimeWindow = calcFilter.UseTimeWindow
                        }
                        For Each dow In MeasurementsDAL.GetDaysOfWeek
                            plotFilter.ApplicableDaysOfWeek.Add(dow)
                        Next
                        Dim plotDateTimes As New List(Of Date)
                        For Each d As Date In DateList(startDate, endDate)
                            plotDateTimes.AddRange(plotFilter.getStartDateTimes(d))
                        Next
                        data = fms.GetCategorySeriesData(plotDateTimes, ndp)
                        chartType = Enums.ChartTypes.Column
                End Select
                seriesList.Add(
                    New Series With {
                        .Data = data,
                        .Name = seriesName,
                        .Type = chartType,
                        .Color = seriesColour,
                        .PlotOptionsSeries = poSeries
                    }
                )
            End If
        Next

        ' Add Criteria Series
        For c = 0 To model.AssessmentCriteria.Count - 1
            Dim criterion = model.AssessmentCriteria(c)
            If criterion.PlotCriterionLevel Then
                Dim points As New List(Of Point)
                If Not model.ShowCriteriaTimePeriods Then
                    points.Add(New Point With {.X = startDate.ToHighChartsTimestamp,
                                               .Y = criterion.ThresholdRangeLower})
                    points.Add(New Point With {.X = endDate.AddDays(1).ToHighChartsTimestamp,
                                               .Y = criterion.ThresholdRangeLower})
                Else
                    ' Add a broken line with the periods covered by the criterion
                    Dim applicableDateTimeRanges = criterion.GetApplicableDateTimeRanges(startDate, endDate)
                    For i = 0 To applicableDateTimeRanges.Count - 1
                        Dim dtRange = applicableDateTimeRanges(i)
                        points.Add(New Point With {.X = dtRange.StartDateTime.ToHighChartsTimestamp,
                                                   .Y = criterion.ThresholdRangeLower})
                        points.Add(New Point With {.X = dtRange.EndDateTime.ToHighChartsTimestamp,
                                                   .Y = criterion.ThresholdRangeLower})
                        If i < applicableDateTimeRanges.Count - 1 Then
                            points.Add(Nothing)
                        End If
                    Next
                End If

                seriesList.Add(
                    New Series With {
                        .Data = New Helpers.Data(points.ToArray),
                        .Name = FixHeader(criterion.CriterionLevelSeriesName, criterion),
                        .Type = Enums.ChartTypes.Line,
                        .Color = Drawing.ColorTranslator.FromHtml(criterion.CriterionLevelLineColour),
                        .PlotOptionsSeries = New PlotOptionsSeries With {
                            .LineWidth = criterion.CriterionLevelLineWidth,
                            .TurboThreshold = 0,
                            .DashStyle = criterion.CriterionLevelDashStyle.DashStyleEnum,
                            .Marker = New PlotOptionsSeriesMarker With {.Enabled = False}
                            }
                        }
                    )
            End If
        Next

        chart.SetSeries(seriesList.ToArray)

        Return chart

    End Function

#End Region

End Class