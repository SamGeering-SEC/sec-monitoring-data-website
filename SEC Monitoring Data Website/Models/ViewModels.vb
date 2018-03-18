Imports libSEC
Imports DotNet.Highcharts
Imports DotNet.Highcharts.Options
Imports DotNet.Highcharts.Helpers
Imports System.Math

Imports System.Runtime.CompilerServices
Imports System.ComponentModel.DataAnnotations

#Region "Interfaces"

Public Interface IViewObjectsViewModel

    Property SearchText As String
    Property TableId As String
    Property UpdateTableRouteName As String
    Property ObjectName As String
    Property ObjectDisplayName As String

End Interface

Public Interface IViewObjectsByMeasurementTypeViewModel

    Property SearchText As String
    Property TableId As String
    Property UpdateTableRouteName As String
    Property ObjectName As String
    Property ObjectDisplayName As String
    Property MeasurementTypeId As String
    Property MeasurementTypeList As SelectList


End Interface

Public Interface IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel

    Property SearchText As String
    Property TableId As String
    Property UpdateTableRouteName As String
    Property ObjectName As String
    Property ObjectDisplayName As String
    Property MeasurementTypeId As String
    Property MeasurementTypeList As SelectList
    Property ProjectId As String
    Property ProjectList As SelectList
    Property MonitorLocationId As String
    Property MonitorLocationList As SelectList
    Property UpdateProjectsRouteName As String
    Property UpdateMonitorLocationsRouteName As String


End Interface

Public Interface IViewObjectsByDocumentTypeViewModel

    Property ProjectIds As String
    Property SearchText As String
    Property TableId As String
    Property UpdateTableRouteName As String
    Property ObjectName As String
    Property ObjectDisplayName As String
    Property DocumentTypeId As String
    Property DocumentTypeList As SelectList


End Interface

Public Interface IAlphabeticalListViewModel

    Property Names As List(Of String)
    Property TableId As String

End Interface


#End Region

Public Class NavigationButtonViewModel

    Public Sub New(Text As String, RouteName As String, RouteValues As Object, ButtonClass As String)

        Me.Text = Text
        Me.RouteName = RouteName
        Me.RouteValues = RouteValues
        Me.ButtonClass = ButtonClass

    End Sub

    Public Property Text As String
    Public Property RouteName As String
    Public Property RouteValues As Object
    Public Property ButtonClass As String

End Class

Public Class TabViewModel

#Region "Factory Methods"

    Public Shared Function getDetailsTab(TabLabel As String, controllerName As String, modelObject As Object) As TabViewModel

        Dim tabId = LCase(TabLabel).Replace(" ", "_")
        Dim viewName = "Details_" + TabLabel.Replace(" ", "")
        Dim isAjax = False
        Return New TabViewModel(TabLabel, tabId, viewName, controllerName, modelObject)

    End Function
    Public Shared Function getDetailsTab(TabLabel As String, PartialName As String, controllerName As String, modelObject As Object) As TabViewModel

        ' Use this one when the TabLabel is not html-friendly e.g. Monitor I.D.

        Dim tabId = LCase(PartialName).Replace(" ", "_")
        Dim viewName = "Details_" + PartialName
        Dim isAjax = False
        Return New TabViewModel(TabLabel, tabId, viewName, controllerName, modelObject)

    End Function

    Public Shared Function getEditTab(TabLabel As String, controllerName As String, modelObject As Object) As TabViewModel

        Dim tabId = LCase(TabLabel).Replace(" ", "_")
        Dim viewName = "Edit_" + TabLabel.Replace(" ", "")
        Dim isAjax = False
        Return New TabViewModel(TabLabel, tabId, viewName, controllerName, modelObject)

    End Function

#End Region

    Public Sub New(tabLabel As String, tabId As String, viewName As String, controllerName As String, modelObject As Object,
                   Optional isAjax As Boolean = False, Optional tabDiv As String = "")

        Me.tabLabel = tabLabel
        Me.tabId = tabId
        Me.modelObject = modelObject
        Me.isAjax = isAjax
        Me.tabDiv = tabDiv
        Me.viewName = viewName
        Me.controllerName = controllerName

    End Sub

    Public Property tabLabel As String
    Public Property tabId As String
    Public Property isAjax As Boolean
    Public Property modelObject As Object
    Public Property tabDiv As String
    Public Property viewName As String
    Public Property controllerName As String

End Class

#Region "Assessment Criteria"

#Region "Groups"

Public Class CreateAssessmentCriterionGroupViewModel

    Public Property CreateNewAssessmentCriterionGroupViewModel As CreateNewAssessmentCriterionGroupViewModel
    Public Property CreateFromExistingCriterionGroupViewModel As CreateFromExistingCriterionGroupViewModel

End Class
Public Class CreateFromExistingCriterionGroupViewModel

    Public Property CopyToProjectId As Integer

    <Required(ErrorMessage:="Please select a Project.")> _
    Public Property CreateFromExistingProjectId As Integer
    Public Property CreateFromExistingProjectList As SelectList

    <Required(ErrorMessage:="Please select a Group.")> _
    Public Property CreateFromExistingAssessmentCriterionGroupId As Integer
    Public Property CreateFromExistingAssessmentCriterionGroupList As SelectList

    <Required(ErrorMessage:="Please select a Monitor Location.")> _
    Public Property CreateFromExistingMonitorLocationId As Integer
    Public Property CreateFromExistingMonitorLocationList As SelectList

End Class
Public Class CreateNewAssessmentCriterionGroupViewModel

    ' Main Model
    Public Property AssessmentCriterionGroup As AssessmentCriterionGroup

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Measurement Type from the list.")> _
    Public Property MeasurementTypeId As Integer
    Public Property MeasurementTypeList As SelectList
    <Required(ErrorMessage:="Please select the Threshold Aggregate Duration from the list.")> _
    Public Property ThresholdAggregateDurationId As Integer
    Public Property ThresholdAggregateDurationList As SelectList
    <Required(ErrorMessage:="Please select the Assessment Period Duration Type from the list.")> _
    Public Property AssessmentPeriodDurationTypeId As Integer
    Public Property AssessmentPeriodDurationTypeList As SelectList

End Class
Public Class EditAssessmentCriterionGroupViewModel

    ' Main Model
    Public Property AssessmentCriterionGroup As AssessmentCriterionGroup

    ' ASSESSMENT
    ' Single Select Items
    <Required(ErrorMessage:="Please select the Threshold Aggregate Duration from the list.")> _
    Public Property ThresholdAggregateDurationId As Integer
    Public Property ThresholdAggregateDurationList As SelectList
    <Required(ErrorMessage:="Please select the Assessment Period Duration Type from the list.")> _
    Public Property AssessmentPeriodDurationTypeId As Integer
    Public Property AssessmentPeriodDurationTypeList As SelectList

    ' Items for Create / Edit Assessment Criterion
    Public Property AssessmentCriterionGroupId As Integer
    Public Property MonitorLocationId As Integer
    Public Property MonitorLocationList As SelectList
    <Required(ErrorMessage:="Please select the Calculation Filter from the list.")> _
    Public Property NewCriterionCalculationFilterId As Integer
    Public Property NewCriterionCalculationFilterList As SelectList

    ' GRAPH
    Public Property ShowGraph As Boolean
    Public Property GraphTitle As String
    Public Property GraphXAxisLabel As String
    Public Property GraphYAxisLabel As String
    Public Property GraphYAxisMin As Double
    Public Property GraphYAxisMax As Double
    Public Property GraphYAxisTickInterval As Double

    ' TABLE
    Public Property NumDateColumns As Integer
    Public Property DateColumn1Header As String
    Public Property DateColumn1Format As String
    Public Property DateColumn2Header As String
    Public Property DateColumn2Format As String
    Public Property MergeHeaderRow1 As Boolean
    Public Property MergeHeaderRow2 As Boolean
    Public Property MergeHeaderRow3 As Boolean
    Public Property ShowIndividualResults As Boolean
    Public Property SumExceedancesAcrossCriteria As Boolean
    Public Property SumPeriodExceedances As Boolean
    Public Property SumDaysWithExceedances As Boolean
    Public Property SumDailyEvents As Boolean
    Public Property ShowSumTitles As Boolean

    ' BUTTONS
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class
Public Class AssessmentCriterionGroupDetailsViewModel

    Public Property AssessmentCriterionGroup As AssessmentCriterionGroup
    Public Property AssessmentCriterionGroupId As Integer
    Public Property MonitorLocationId As Integer
    Public Property MonitorLocationList As SelectList
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

#End Region

#Region "Criteria"

Public Class ViewMonitorLocationAssessmentCriteriaViewModel

    Public Property MonitorLocation As MonitorLocation
    Public Property AssessmentCriteria As IEnumerable(Of AssessmentCriterion)

    ' Link Display
    Public Property ShowCalculationFilterLinks As Boolean

End Class
Public Class MonitorLocationCriteriaDetailsViewModel

    Public Property MonitorLocation As MonitorLocation
    Public Property AssessmentCriterionGroup As AssessmentCriterionGroup
    Public Property AssessmentCriteria As IEnumerable(Of AssessmentCriterion)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class
Public Class MonitorLocationCriterionDetailsViewModel

    Public Property MonitorLocation As MonitorLocation
    Public Property AssessmentCriterionGroup As AssessmentCriterionGroup
    Public Property AssessmentCriterion As AssessmentCriterion
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class CopyAssessmentCriteriaViewModel

    Public Property AssessmentCriterionGroupId As Integer
    Public Property CopyFromMonitorLocationId As Integer
    Public Property CopyFromMonitorLocationList As SelectList
    Public Property CopyToMonitorLocationId As Integer

End Class

Public Class CreateAssessmentCriterionPopUpViewModel

    Public Property AssessmentCriterionGroupId As Integer
    Public Property MonitorLocationId As Integer

    ' ASSESSMENT
    <Required(ErrorMessage:="Please select the Calculation Filter from the list.")> _
    Public Property CalculationFilterId As Integer
    Public Property CalculationFilterList As SelectList

    <Required(ErrorMessage:="Please enter the Level for the Lower Bound of the Threshold Range.")> _
    Public Property ThresholdRangeLower As Double

    <Required(ErrorMessage:="Please enter the Level for the Upper Bound of the Threshold Range.")> _
    Public Property ThresholdRangeUpper As Double

    <Required(ErrorMessage:="Please select the Type of Threshold for the Lower Bound of the Threshold Range.")> _
    Public Property ThresholdTypeId As Integer
    Public Property ThresholdTypeList As SelectList

    <Required(ErrorMessage:="Please select the Number of Decimal Places to Round the Assessment Levels to.")> _
    Public Property RoundingDecimalPlaces As Integer

    ' ASSESSED LEVEL SERIES
    Public Property PlotAssessedLevel As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelSeriesName As String
    <Required(ErrorMessage:="Please select the Dash Style for the Assessed Level Series.")> _
    Public Property AssessedLevelDashStyleId As Integer
    Public Property AssessedLevelDashStyleList As SelectList
    Public Property AssessedLevelLineWidth As Double
    Public Property AssessedLevelLineColour As String
    <Required(ErrorMessage:="Please select the Series Type for the Assessed Level Series.")> _
    Public Property AssessedLevelSeriesTypeId As Integer
    Public Property AssessedLevelSeriesTypeList As SelectList
    Public Property AssessedLevelMarkersOn As Boolean

    ' ASSESSED LEVEL TABLE
    Public Property TabulateAssessedLevels As Boolean
    Public Property MergeAssessedLevels As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelHeader1 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelHeader2 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelHeader3 As String

    ' CRITERION LEVEL SERIES
    Public Property PlotCriterionLevel As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionLevelSeriesName As String
    <Required(ErrorMessage:="Please select the Dash Style for the Criterion Level Series.")> _
    Public Property CriterionLevelDashStyleId As Integer
    Public Property CriterionLevelDashStyleList As SelectList
    Public Property CriterionLevelLineWidth As Double
    Public Property CriterionLevelLineColour As String

    ' CRITERION LEVEL TABLE
    Public Property TabulateCriterionTriggers As Boolean
    Public Property MergeCriterionTriggers As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionTriggerHeader1 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionTriggerHeader2 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionTriggerHeader3 As String

End Class

Public Class CreateAssessmentCriterionViewModel

    Public Property AssessmentCriterionGroupId As Integer
    Public Property MonitorLocationId As Integer

    ' ASSESSMENT
    <Required(ErrorMessage:="Please select the Calculation Filter from the list.")> _
    Public Property CalculationFilterId As Integer
    Public Property CalculationFilterList As SelectList

    <Required(ErrorMessage:="Please enter the Level for the Default Lower Bound of the Threshold Range.")> _
    Public Property ThresholdRangeLower As Double

    <Required(ErrorMessage:="Please enter the Level for the Default Upper Bound of the Threshold Range.")> _
    Public Property ThresholdRangeUpper As Double

    <Required(ErrorMessage:="Please select the Type of Threshold for the Lower Bound of the Threshold Range.")> _
    Public Property ThresholdTypeId As Integer
    Public Property ThresholdTypeList As SelectList

    <Required(ErrorMessage:="Please select the Number of Decimal Places to Round the Assessment Levels to.")> _
    Public Property RoundingDecimalPlaces As Integer

    ' ASSESSED LEVEL SERIES
    Public Property PlotAssessedLevel As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelSeriesName As String
    <Required(ErrorMessage:="Please select the Dash Style for the Assessed Level Series.")> _
    Public Property AssessedLevelDashStyleId As Integer
    Public Property AssessedLevelDashStyleList As SelectList
    Public Property AssessedLevelLineWidth As Double
    Public Property AssessedLevelLineColour As String
    <Required(ErrorMessage:="Please select the Series Type for the Assessed Level Series.")> _
    Public Property AssessedLevelSeriesTypeId As Integer
    Public Property AssessedLevelSeriesTypeList As SelectList
    Public Property AssessedLevelMarkersOn As Boolean

    ' ASSESSED LEVEL TABLE
    Public Property TabulateAssessedLevels As Boolean
    Public Property MergeAssessedLevels As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelHeader1 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelHeader2 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelHeader3 As String

    ' CRITERION LEVEL SERIES
    Public Property PlotCriterionLevel As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionLevelSeriesName As String
    <Required(ErrorMessage:="Please select the Dash Style for the Criterion Level Series.")> _
    Public Property CriterionLevelDashStyleId As Integer
    Public Property CriterionLevelDashStyleList As SelectList
    Public Property CriterionLevelLineWidth As Double
    Public Property CriterionLevelLineColour As String

    ' CRITERION LEVEL TABLE
    Public Property TabulateCriterionTriggers As Boolean
    Public Property MergeCriterionTriggers As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionTriggerHeader1 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionTriggerHeader2 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionTriggerHeader3 As String

End Class

Public Class EditAssessmentCriterionPopUpViewModel

    Public Property EditAssessmentCriterionId As Integer
    Public Property AssessmentCriterionGroupId As Integer
    Public Property MonitorLocationId As Integer

    ' ASSESSMENT
    <Required(ErrorMessage:="Please select the Calculation Filter from the list.")> _
    Public Property CalculationFilterId As Integer
    Public Property CalculationFilterList As SelectList
    <Required(ErrorMessage:="Please enter the Level for the Lower Bound of the Threshold Range.")> _
    Public Property ThresholdRangeLower As Double
    <Required(ErrorMessage:="Please enter the Level for the Upper Bound of the Threshold Range.")> _
    Public Property ThresholdRangeUpper As Double
    <Required(ErrorMessage:="Please select the Type of Threshold for the Lower Bound of the Threshold Range.")> _
    Public Property ThresholdTypeId As Integer
    Public Property ThresholdTypeList As SelectList
    <Required(ErrorMessage:="Please select the Number of Decimal Places to Round the Assessment Levels to.")> _
    Public Property RoundingDecimalPlaces As Integer
    Public Property CriterionIndex As Integer

    ' ASSESSED LEVEL SERIES
    Public Property PlotAssessedLevel As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelSeriesName As String
    <Required(ErrorMessage:="Please select the Dash Style for the Assessed Level Series.")> _
    Public Property AssessedLevelDashStyleId As Integer
    Public Property AssessedLevelDashStyleList As SelectList
    Public Property AssessedLevelLineWidth As Double
    Public Property AssessedLevelLineColour As String
    <Required(ErrorMessage:="Please select the Series Type for the Assessed Level Series.")> _
    Public Property AssessedLevelSeriesTypeId As Integer
    Public Property AssessedLevelSeriesTypeList As SelectList
    Public Property AssessedLevelMarkersOn As Boolean

    ' ASSESSED LEVEL TABLE
    Public Property TabulateAssessedLevels As Boolean
    Public Property MergeAssessedLevels As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelHeader1 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelHeader2 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AssessedLevelHeader3 As String

    ' CRITERION LEVEL SERIES
    Public Property PlotCriterionLevel As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionLevelSeriesName As String
    <Required(ErrorMessage:="Please select the Dash Style for the Criterion Level Series.")> _
    Public Property CriterionLevelDashStyleId As Integer
    Public Property CriterionLevelDashStyleList As SelectList
    Public Property CriterionLevelLineWidth As Double
    Public Property CriterionLevelLineColour As String

    ' CRITERION LEVEL TABLE
    Public Property TabulateCriterionTriggers As Boolean
    Public Property MergeCriterionTriggers As Boolean
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionTriggerHeader1 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionTriggerHeader2 As String
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property CriterionTriggerHeader3 As String

End Class

Public Class EditAssessmentCriteriaViewModel

    Public Property Criteria As IEnumerable(Of AssessmentCriterion)
    Public Property AssessmentCriterionGroupId As Integer
    Public Property MonitorLocationId As Integer

    <Required(ErrorMessage:="Please select the Calculation Filter from the list.")> _
    Public Property NewCriterionCalculationFilterId As Integer
    Public Property NewCriterionCalculationFilterList As SelectList

    Public Property CopyFromMonitorLocationId As Integer
    Public Property CopyFromMonitorLocationList As SelectList

    Public Property ThresholdTypeId As Integer
    Public Property ThresholdTypeList As SelectList

    Public Property DashStyleList As SelectList
    Public Property SeriesTypeList As SelectList

    Public Property CreateAssessmentCriterionViewModel As CreateAssessmentCriterionPopUpViewModel

    Public Function getNewEditAssessmentCriterionViewModel(criterion As AssessmentCriterion) As EditAssessmentCriterionPopUpViewModel

        Return New EditAssessmentCriterionPopUpViewModel With {
            .AssessedLevelDashStyleId = criterion.AssessedLevelDashStyleId,
            .AssessedLevelDashStyleList = DashStyleList,
            .AssessedLevelHeader1 = criterion.AssessedLevelHeader1,
            .AssessedLevelHeader2 = criterion.AssessedLevelHeader2,
            .AssessedLevelHeader3 = criterion.AssessedLevelHeader3,
            .AssessedLevelLineColour = criterion.AssessedLevelLineColour,
            .AssessedLevelLineWidth = criterion.AssessedLevelLineWidth,
            .AssessedLevelMarkersOn = criterion.AssessedLevelMarkersOn,
            .AssessedLevelSeriesName = criterion.AssessedLevelSeriesName,
            .AssessedLevelSeriesTypeId = criterion.AssessedLevelSeriesTypeId,
            .AssessedLevelSeriesTypeList = SeriesTypeList,
            .AssessmentCriterionGroupId = criterion.AssessmentCriterionGroupId,
            .CalculationFilterId = criterion.CalculationFilterId,
            .CalculationFilterList = NewCriterionCalculationFilterList,
            .CriterionLevelDashStyleId = criterion.CriterionLevelDashStyleId,
            .CriterionLevelDashStyleList = DashStyleList,
            .CriterionLevelLineColour = criterion.CriterionLevelLineColour,
            .CriterionLevelLineWidth = criterion.CriterionLevelLineWidth,
            .CriterionLevelSeriesName = criterion.CriterionLevelSeriesName,
            .CriterionTriggerHeader1 = criterion.CriterionTriggerHeader1,
            .CriterionTriggerHeader2 = criterion.CriterionTriggerHeader2,
            .CriterionTriggerHeader3 = criterion.CriterionTriggerHeader3,
            .EditAssessmentCriterionId = criterion.Id,
            .MergeAssessedLevels = criterion.MergeAssessedLevels,
            .MergeCriterionTriggers = criterion.MergeCriterionTriggers,
            .MonitorLocationId = criterion.MonitorLocationId,
            .PlotAssessedLevel = criterion.PlotAssessedLevel,
            .PlotCriterionLevel = criterion.PlotCriterionLevel,
            .RoundingDecimalPlaces = criterion.RoundingDecimalPlaces,
            .TabulateAssessedLevels = criterion.TabulateAssessedLevels,
            .TabulateCriterionTriggers = criterion.TabulateCriterionTriggers,
            .ThresholdRangeLower = criterion.ThresholdRangeLower,
            .ThresholdRangeUpper = criterion.ThresholdRangeUpper,
            .ThresholdTypeId = criterion.ThresholdTypeId,
            .ThresholdTypeList = ThresholdTypeList
        }

    End Function


End Class

#End Region

#Region "Assessments"

Module AssessmentHelpers

    Public Function FixHeader(headerText As String, criterion As AssessmentCriterion) As String

        ' Get durations
        Dim durationDays = criterion.CalculationFilter.getDuration
        Dim durationHours = durationDays * 24
        Dim durationMinutes = durationHours * 60
        Dim durationSeconds = durationMinutes * 60
        Dim timeBaseDays = criterion.CalculationFilter.TimeBase
        Dim timeBaseHours = timeBaseDays * 24
        Dim timeBaseMinutes = timeBaseHours * 60
        Dim timeBaseSeconds = timeBaseMinutes * 60
        Dim timeStepDays = criterion.CalculationFilter.TimeStep
        Dim timeStepHours = timeStepDays * 24
        Dim timeStepMinutes = timeStepHours * 60
        Dim timeStepSeconds = timeStepMinutes * 60
        ' Get times
        Dim startTime = criterion.CalculationFilter.TimeWindowStartTime
        Dim endTime = criterion.CalculationFilter.TimeWindowEndTime
        ' Get days
        Dim firstDayName = criterion.CalculationFilter.ApplicableDaysOfWeek.First.DayName
        Dim lastDayName = criterion.CalculationFilter.ApplicableDaysOfWeek.Last.DayName
        Dim firstDayAbbreviation = firstDayName.Substring(0, 3)
        Dim lastDayAbbreviation = lastDayName.Substring(0, 3)

        Dim fixedText = headerText.Replace(
            "$CalculationFilter.Duration.Days$", Round(durationDays).ToString("0")
        ).Replace(
            "$CalculationFilter.Duration.Hours$", Round(durationHours).ToString("0")
        ).Replace(
            "$CalculationFilter.Duration.Minutes$", Round(durationMinutes).ToString("0")
        ).Replace(
            "$CalculationFilter.Duration.Seconds$", Round(durationSeconds).ToString("0")
        ).Replace(
            "$CalculationFilter.TimeWindowStartTime$", startTime.ToString("HH:mm")
        ).Replace(
            "$CalculationFilter.TimeWindowEndTime$", endTime.ToString("HH:mm")
        ).Replace(
            "$CalculationFilter.TimeBase.Days$", Round(timeBaseDays).ToString("0")
        ).Replace(
            "$CalculationFilter.TimeBase.Hours$", Round(timeBaseHours).ToString("0")
        ).Replace(
            "$CalculationFilter.TimeBase.Minutes$", Round(timeBaseMinutes).ToString("0")
        ).Replace(
            "$CalculationFilter.TimeBase.Seconds$", Round(timeBaseSeconds).ToString("0")
        ).Replace(
            "$CalculationFilter.TimeStep.Days$", Round(timeStepDays).ToString("0")
        ).Replace(
            "$CalculationFilter.TimeStep.Hours$", Round(timeStepHours).ToString("0")
        ).Replace(
            "$CalculationFilter.TimeStep.Minutes$", Round(timeStepMinutes).ToString("0")
        ).Replace(
            "$CalculationFilter.TimeStep.Seconds$", Round(timeStepSeconds).ToString("0")
        ).Replace(
            "$ThresholdRangeLower$", criterion.ThresholdRangeLower.ToString
        ).Replace(
            "$ThresholdRangeUpper$", criterion.ThresholdRangeUpper.ToString
        ).Replace(
            "$ThresholdRangeLower(0)$", Round(criterion.ThresholdRangeLower, 0).ToString("0")
        ).Replace(
            "$ThresholdRangeUpper(0)$", Round(criterion.ThresholdRangeUpper, 0).ToString("0")
        ).Replace(
            "$ThresholdRangeLower(1)$", Round(criterion.ThresholdRangeLower, 1).ToString("0.0")
        ).Replace(
            "$ThresholdRangeUpper(1)$", Round(criterion.ThresholdRangeUpper, 1).ToString("0.0")
        ).Replace(
            "$CalculationFilter.FirstDay.Name$", firstDayName
        ).Replace(
            "$CalculationFilter.LastDay.Name$", lastDayName
        ).Replace(
            "$CalculationFilter.FirstDay.Abbreviation$", firstDayAbbreviation
        ).Replace(
            "$CalculationFilter.LastDay.Abbreviation$", lastDayAbbreviation
        )

        Return fixedText

    End Function

End Module


Public Class ViewAssessmentViewModel

    Public Property MonitorLocation As MonitorLocation
    Public Property MonitorLocationId As Integer

    ' All Groups
    Public Property AssessmentCriterionGroup As AssessmentCriterionGroup

    ' Selected Group
    Public Property AssessmentCriterionGroups As IEnumerable(Of AssessmentCriterionGroup)
    Public Property SelectedAssessmentCriterionGroup As AssessmentCriterionGroup
    'Public Property AssessmentCriterionGroupId As Integer
    'Public Property AssessmentCriterionGroupList As SelectList
    Public Property AssessmentCriteria As IEnumerable(Of AssessmentCriterion)

    ' Dates
    Public Property AssessmentDate As Date
    Public Property StartOrEnd As String
    Public Property FirstMeasurementDate As Date
    Public Property LastMeasurementDate As Date

    ' Chart Options
    Public Property ShowComments As Boolean
    Public Property ShowCriteriaTimePeriods As Boolean
    Public Property ShowNonWorkingHours As Boolean
    Public Property DynamicYMin As Boolean
    Public Property DynamicYMax As Boolean

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class ViewAssessmentDataViewModel

    Public Property AssessmentCriterionGroup As AssessmentCriterionGroup
    Public Property AssessmentCriteria As List(Of AssessmentCriterion)
    Public Property NumCriteria As Integer
    Public Property Dates As List(Of Date)
    Public Property AssessmentDate As Date
    Public Property StartOrEnd As String
    Public Property AssessmentResults As List(Of List(Of AssessmentResultSingleDay))
    Public Property GraphingResults As List(Of FilteredMeasurementsSequence)
    Public Property LevelRoundingDP As Integer
    Public Property MeasurementComments As List(Of MeasurementComment)
    Public Property MonitorLocation As MonitorLocation
    Public Property Chart As Highcharts
    Public Property Table As AssessmentTableViewModel
    Public Property ShowCriteriaTimePeriods As Boolean
    Public Property ShowNonWorkingHours As Boolean
    Public Property NonWorkingHours As List(Of DateTimeRange)
    Public Property DynamicYMin As Boolean
    Public Property DynamicYMax As Boolean

End Class

#Region "Assessment Table Classes"

Module AssessmentTableHelpers

    Public Function SumExceedancesByCriterion(CriteriaResultsByDate As List(Of List(Of AssessmentResultSingleDay))) As List(Of Integer)

        ' Inner lists are by date, outer lists are by criterion - each inner list is the for the same criterion over different dates.
        Dim criterionExceedanceSums As New List(Of Integer)

        If CriteriaResultsByDate.Count = 0 Then Return criterionExceedanceSums

        Dim numDates = CriteriaResultsByDate(0).Count
        Dim numCriteria = CriteriaResultsByDate.Count

        For Each criterionResultsByDate In CriteriaResultsByDate
            criterionExceedanceSums.Add(criterionResultsByDate.SumOfExceedances)
        Next

        Return criterionExceedanceSums

    End Function

    Public Function SumExceedanceEventsByCriterion(CriterionResultsByDate As List(Of List(Of AssessmentResultSingleDay))) As List(Of Integer)

        ' Inner lists are by date, outer lists are by criterion - each inner list is the for the same criterion over different dates.
        Dim criterionExceedanceEventSums As New List(Of Integer)

        If CriterionResultsByDate.Count = 0 Then Return criterionExceedanceEventSums

        For Each criterionResults In CriterionResultsByDate
            criterionExceedanceEventSums.Add(
                GetDailyThresholdExceedanceEvents(criterionResults).Count
            )
        Next

        Return criterionExceedanceEventSums

    End Function
    Public Function SumExceedanceEvents(CriterionResults As List(Of List(Of AssessmentResultSingleDay))) As Integer

        Dim sums = SumExceedanceEventsByCriterion(CriterionResults)
        Return sums.Sum


    End Function


    Public Function SumExceedancesByDate(CriteriaResultsByDate As List(Of List(Of AssessmentResultSingleDay))) As List(Of Integer)

        ' Inner lists are by date, outer lists are by criterion - each inner list is the for the same criterion over different dates.
        Dim numDates = CriteriaResultsByDate(0).Count
        Dim numCriteria = CriteriaResultsByDate.Count
        Dim dateExceedanceSums As New List(Of Integer)

        For d = 0 To numDates - 1
            Dim dateExceedances = 0
            For c = 0 To numCriteria - 1
                Dim dailyCriterionResults = CriteriaResultsByDate(c)(d)
                Dim dateCriterionExceendances = dailyCriterionResults.getNumberOfExceedances
                If Not IsNothing(dateCriterionExceendances) Then
                    dateExceedances += CInt(dateCriterionExceendances)
                End If
            Next
            dateExceedanceSums.Add(dateExceedances)
        Next

        Return dateExceedanceSums

    End Function

    Public Function GetDaysWithExceedancesByCriterion(CriteriaResultsByDate As List(Of List(Of AssessmentResultSingleDay))) As List(Of Integer)

        ' Inner lists are by date, outer lists are by criterion - each inner list is the for the same criterion over different dates.
        Dim criteriaExceedanceDays As New List(Of Integer)

        For Each criterionResultsByDate In CriteriaResultsByDate
            criteriaExceedanceDays.Add(criterionResultsByDate.CountDaysOfExceedances)
        Next

        Return criteriaExceedanceDays

    End Function


End Module

Public Class AssessmentTableCell

    Public Property Text As String
    Public Property NoResult As Boolean
    Public Property ColSpan As Integer = 1
    Public Property RowSpan As Integer = 1

    Public Function ToTheadFootTh() As String

        Return String.Format("<th colspan='{0}' rowspan='{1}' style='border: solid 1px; padding: 5px; text-align:center;'>{2}</th>", {ColSpan, RowSpan, Text})

    End Function

    Public Function ToTbodyTh(Optional cellWidth As String = "") As String

        Return String.Format(
            "<th colspan='{0}' rowspan='{1}' style='border-right: solid 1px; border-left: solid 1px; padding: 5px; text-align:center;{2}'>{3}</th>",
            {ColSpan, RowSpan, cellWidth, Text}
        )

    End Function

    Public Function ToTBodyTd() As String

        Return String.Format("<td colspan='{0}' rowspan='{1}' style='border-right: solid 1px; border-left: solid 1px; padding: 5px; text-align:center;'>{2}</td>", {ColSpan, RowSpan, Text})

    End Function

End Class

Public Class AssessmentTableRow

    Public Property Cells As List(Of AssessmentTableCell)

    Public Sub New()

        Cells = New List(Of AssessmentTableCell)

    End Sub

    Public Sub AddCell(Cell As AssessmentTableCell)

        Cells.Add(Cell)

    End Sub

    Public Sub AppendRow(Row As AssessmentTableRow)

        Cells.AddRange(Row.Cells)

    End Sub

    Public Sub MergeIdenticalCells()

        Dim newCells = New List(Of AssessmentTableCell)

        newCells.Add(Me.Cells(0))
        For c = 1 To Me.Cells.Count - 1
            If Cells(c).Text = newCells(newCells.Count - 1).Text Then
                newCells(newCells.Count - 1).ColSpan += 1
            Else
                newCells.Add(Cells(c))
            End If
        Next

        Cells = newCells

    End Sub

    Public Function ColumnsSpanned() As Integer

        Dim colsSpanned As Integer = 0
        For Each cell In Cells
            colsSpanned += cell.ColSpan
        Next

        Return colsSpanned

    End Function

    Public Function GetCellAtColumn(ColumnIndex) As AssessmentTableCell

        Dim currentColIndex As Integer = 0
        For Each cell In Cells
            If currentColIndex = ColumnIndex Then
                Return cell
            End If
            currentColIndex += cell.ColSpan
        Next
        Return Nothing

    End Function
    Public Function IndexOfCellAtColumn(ColumnIndex) As Integer

        Dim currentColIndex As Integer = 0
        For Each cell In Cells
            If currentColIndex = ColumnIndex Then
                Return Cells.IndexOf(cell)
            End If
            currentColIndex += cell.ColSpan
        Next
        Return -1

    End Function

End Class

Public Class AssessmentTableHeader

    Public Property Rows As List(Of AssessmentTableRow)
    Public Property NumDateColumns As Integer
    Public Property NumLevelColumns As Integer
    Public Property NumTriggerColumns As Integer
    Public Property HasSumColumn As Boolean
    Public Property MergeHeaderRow1 As Boolean
    Public Property MergeHeaderRow2 As Boolean
    Public Property MergeHeaderRow3 As Boolean
    Public Property SumExceedancesAcrossCriteria As Boolean

    

    Public Sub New(group As AssessmentCriterionGroup,
                   assessmentResults As List(Of List(Of AssessmentResultSingleDay)),
                   assessmentCriteria As List(Of AssessmentCriterion))

        Rows = New List(Of AssessmentTableRow)

        HasSumColumn = group.SumExceedancesAcrossCriteria
        MergeHeaderRow1 = group.MergeHeaderRow1
        MergeHeaderRow2 = group.MergeHeaderRow2
        MergeHeaderRow3 = group.MergeHeaderRow3
        SumExceedancesAcrossCriteria = group.SumExceedancesAcrossCriteria

        Dim numLevelColumns As Integer = 0
        Dim numTriggerColumns As Integer = 0
        Dim headerRow1 = New AssessmentTableRow
        Dim headerRow2 = New AssessmentTableRow
        Dim headerRow3 = New AssessmentTableRow

        ' dates
        If group.NumDateColumns > 0 Then
            headerRow1.AddCell(New AssessmentTableCell With {.Text = group.DateColumn1Header})
            headerRow2.AddCell(New AssessmentTableCell With {.Text = group.DateColumn1Header})
            headerRow3.AddCell(New AssessmentTableCell With {.Text = group.DateColumn1Header})
        End If
        If group.NumDateColumns = 2 Then
            headerRow1.AddCell(New AssessmentTableCell With {.Text = group.DateColumn2Header})
            headerRow2.AddCell(New AssessmentTableCell With {.Text = group.DateColumn2Header})
            headerRow3.AddCell(New AssessmentTableCell With {.Text = group.DateColumn2Header})
        End If
        ' assessed levels
        For c = 0 To assessmentResults.Count - 1
            Dim criterionResults = assessmentResults(c)
            Dim criterion = assessmentCriteria(c)
            If criterion.TabulateAssessedLevels Then
                headerRow1.AddCell(
                    New AssessmentTableCell With {
                        .Text = FixHeader(criterion.AssessedLevelHeader1, criterion)
                    }
                )
                headerRow2.AddCell(
                    New AssessmentTableCell With {
                        .Text = FixHeader(criterion.AssessedLevelHeader2, criterion)
                    }
                )
                headerRow3.AddCell(
                    New AssessmentTableCell With {
                        .Text = FixHeader(criterion.AssessedLevelHeader3, criterion)
                    }
                )
                numLevelColumns += 1
            End If
        Next
        ' triggers
        For c = 0 To assessmentResults.Count - 1
            Dim criterionResults = assessmentResults(c)
            Dim criterion = assessmentCriteria(c)
            If criterion.TabulateCriterionTriggers Then
                headerRow1.AddCell(
                    New AssessmentTableCell With {
                        .Text = FixHeader(criterion.CriterionTriggerHeader1, criterion)
                    }
                )
                headerRow2.AddCell(
                    New AssessmentTableCell With {
                        .Text = FixHeader(criterion.CriterionTriggerHeader2, criterion)
                    }
                )
                headerRow3.AddCell(
                    New AssessmentTableCell With {
                        .Text = FixHeader(criterion.CriterionTriggerHeader3, criterion)
                    }
                )
                numTriggerColumns += 1
            End If
        Next
        ' trigger total
        If group.SumExceedancesAcrossCriteria Then
            headerRow1.AddCell(New AssessmentTableCell With {.Text = "TOTAL"})
            headerRow2.AddCell(New AssessmentTableCell With {.Text = "TOTAL"})
            headerRow3.AddCell(New AssessmentTableCell With {.Text = "TOTAL"})
        End If

        AddRow(headerRow1)
        AddRow(headerRow2)
        AddRow(headerRow3)

        Me.NumDateColumns = group.NumDateColumns
        Me.NumLevelColumns = numLevelColumns
        Me.NumTriggerColumns = numTriggerColumns

    End Sub

    Public Sub AddRow(Row As AssessmentTableRow)

        Rows.Add(Row)

    End Sub

    Public Sub MergeCells(FirstLevelColumnIndex, LastTriggerColumnIndex)

        ' Merge header columns in each row where applicable
        If MergeHeaderRow1 Then Rows(0).MergeIdenticalCells()
        If MergeHeaderRow2 Then Rows(1).MergeIdenticalCells()
        If MergeHeaderRow3 Then Rows(2).MergeIdenticalCells()

        Dim numColsSpanned = Rows(0).ColumnsSpanned()
        For c = numColsSpanned - 1 To 0 Step -1
            Dim cell1 = Rows(0).GetCellAtColumn(c)
            Dim cell2 = Rows(1).GetCellAtColumn(c)
            Dim cell3 = Rows(2).GetCellAtColumn(c)
            Dim cell1index = Rows(0).IndexOfCellAtColumn(c)
            Dim cell2index = Rows(1).IndexOfCellAtColumn(c)
            Dim cell3index = Rows(2).IndexOfCellAtColumn(c)
            If Not cell1 Is Nothing And Not cell2 Is Nothing And Not cell3 Is Nothing Then
                If cell1.Text = cell2.Text And cell2.Text = cell3.Text Then
                    Rows(0).Cells(cell1index).RowSpan = 3
                    Rows(1).Cells.RemoveAt(cell2index)
                    Rows(2).Cells.RemoveAt(cell3index)
                ElseIf cell1.Text = cell2.Text Then
                    Rows(0).Cells(cell1index).RowSpan = 2
                    Rows(1).Cells.RemoveAt(cell2index)
                ElseIf cell2.Text = cell3.Text Then
                    Rows(1).Cells(cell2index).RowSpan = 2
                    Rows(2).Cells.RemoveAt(cell3index)
                End If
            ElseIf Not cell1 Is Nothing And Not cell2 Is Nothing Then
                If cell1.Text = cell2.Text Then
                    Rows(0).Cells(cell1index).RowSpan = 2
                    Rows(1).Cells.RemoveAt(cell2index)
                End If
            ElseIf Not cell2 Is Nothing And Not cell3 Is Nothing Then
                If cell2.Text = cell3.Text Then
                    Rows(1).Cells(cell2index).RowSpan = 2
                    Rows(2).Cells.RemoveAt(cell3index)
                End If
            End If
        Next

    End Sub

    Public Function ColumnsMatch(ColumnIndex1 As Integer, ColumnIndex2 As Integer) As Boolean

        For Each row In Rows
            If row.Cells(ColumnIndex1).Text <> row.Cells(ColumnIndex2).Text Then Return False
        Next
        Return True

    End Function

    Public Function MatchingColumnGroups(StartColumnIndex As Integer, EndColumnIndex As Integer) As List(Of List(Of Integer))

        ' N.B. don't use this after any merging

        Dim matchedIndices As New List(Of Integer)
        Dim groups As New List(Of List(Of Integer))

        For colIndex = StartColumnIndex To EndColumnIndex
            If Not matchedIndices.Contains(colIndex) Then
                Dim colGroup As New List(Of Integer)
                colGroup.Add(colIndex)
                For matchIndex = colIndex + 1 To EndColumnIndex
                    ' Only match adjacent groups of columns
                    If ColumnsMatch(colIndex, matchIndex) And matchIndex = colGroup(colGroup.Count - 1) + 1 Then
                        colGroup.Add(matchIndex)
                    End If
                Next
                For Each index In colGroup
                    matchedIndices.Add(index)
                Next
                groups.Add(colGroup)
            End If
        Next

        Return groups

    End Function

    Public Function GetDateColumns() As List(Of AssessmentTableRow)

        Dim newRows As New List(Of AssessmentTableRow)

        For Each row In Rows
            Dim newRow As New AssessmentTableRow()
            For c = 0 To NumDateColumns - 1
                newRow.Cells.Add(row.Cells(c))
            Next
            newRows.Add(newRow)
        Next

        Return newRows

    End Function
    Public Function GetSumColumn() As List(Of AssessmentTableRow)

        Dim newRows As New List(Of AssessmentTableRow)

        For Each row In Rows
            Dim newRow As New AssessmentTableRow()
            If HasSumColumn Then
                newRow.Cells.Add(row.Cells(row.Cells.Count - 1))
            End If
            newRows.Add(newRow)
        Next

        Return newRows

    End Function

    Public Function CompressColumnGroups(columnGroups As List(Of List(Of Integer))) As List(Of AssessmentTableRow)

        ' N.B. don't use this after any merging

        Dim newRows As New List(Of AssessmentTableRow)
        For Each row In Rows
            Dim newRow As New AssessmentTableRow()
            For Each columnGroup In columnGroups
                newRow.AddCell(row.Cells(columnGroup(0)))
            Next
            newRows.Add(newRow)
        Next

        Return newRows

    End Function

End Class

Public Class AssessmentTableBody

    Public Property Rows As List(Of AssessmentTableRow)
    Public Property NumDateColumns As Integer
    Public Property NumLevelColumns As Integer
    Public Property TotalExceedances As Integer
    Public Property PeriodExceedances As List(Of Integer)
    Public Property DaysOfExceedances As List(Of Integer)
    Public Property TriggerEvents As List(Of Integer)
    Public Property HasSumColumn As Boolean


    Public Sub New(group As AssessmentCriterionGroup,
                   assessmentResults As List(Of List(Of AssessmentResultSingleDay)),
                   assessmentCriteria As List(Of AssessmentCriterion),
                   dates As List(Of Date))

        NumDateColumns = group.NumDateColumns
        NumLevelColumns = 0
        Rows = New List(Of AssessmentTableRow)
        PeriodExceedances = New List(Of Integer)
        DaysOfExceedances = New List(Of Integer)
        TriggerEvents = New List(Of Integer)
        HasSumColumn = group.SumExceedancesAcrossCriteria

        TotalExceedances = 0
        For Each criterion In assessmentCriteria
            PeriodExceedances.Add(0)
            DaysOfExceedances.Add(0)
            TriggerEvents.Add(0)
        Next

        For d = 0 To dates.Count - 1
            Dim row As New AssessmentTableRow()
            ' date cell(s)
            Dim rowDate = dates(d)
            If group.NumDateColumns > 0 Then
                row.AddCell(
                    New AssessmentTableCell With {.Text = Format(rowDate, group.DateColumn1Format)}
                )
            End If
            If group.NumDateColumns = 2 Then
                row.AddCell(
                    New AssessmentTableCell With {.Text = Format(rowDate, group.DateColumn2Format)}
                )
            End If
            ' assessed level cells
            For c = 0 To assessmentResults.Count - 1
                Dim criterion = assessmentCriteria(c)
                Dim assessmentResult = assessmentResults(c)(d)
                If criterion.TabulateAssessedLevels Then
                    Dim level = assessmentResult.getMaxLevel()
                    If group.ShowIndividualResults Then
                        If d = 0 Then
                            NumLevelColumns += 1
                        End If
                        If level Is Nothing Then
                            row.AddCell(New AssessmentTableCell With {.NoResult = True})
                        Else
                            row.AddCell(
                                New AssessmentTableCell With {
                                    .Text = Math.Round(
                                        CDec(level), criterion.RoundingDecimalPlaces,
                                        MidpointRounding.AwayFromZero
                                    ).ToString("F" + criterion.RoundingDecimalPlaces.ToString)
                                }
                            )
                        End If
                    End If
                End If
            Next
            ' trigger cells
            Dim dailyExceedances As Integer = 0
            For c = 0 To assessmentResults.Count - 1
                Dim criterion = assessmentCriteria(c)
                Dim assessmentResult = assessmentResults(c)(d)
                If criterion.TabulateCriterionTriggers Then
                    Dim numExceedances = assessmentResult.getNumberOfExceedances()
                    If numExceedances Is Nothing Then
                        If group.ShowIndividualResults Then
                            row.AddCell(New AssessmentTableCell With {.NoResult = True})
                        End If
                    Else
                        dailyExceedances += numExceedances
                        TotalExceedances += numExceedances
                        PeriodExceedances(c) += numExceedances
                        If CInt(numExceedances) > 0 Then DaysOfExceedances(c) += 1
                        If group.ShowIndividualResults Then
                            row.AddCell(
                                New AssessmentTableCell With {
                                    .Text = CInt(numExceedances).ToString
                                }
                            )
                        End If
                    End If
                End If
            Next
            ' trigger sum cell
            If group.SumExceedancesAcrossCriteria Then
                row.AddCell(
                    New AssessmentTableCell With {.Text = dailyExceedances.ToString}
                )
            End If
            If row.Cells.Count > 0 Then AddRow(row)
            ' trigger events
            '  select the assessment results for the day
            Dim id = d
            Dim dailyResults = assessmentResults.Select(Function(ar) ar(id)).ToList()
            '  calculate the number of events for each trigger
            Dim dailyEvents = GetDailyThresholdExceedanceEvents(dailyResults)
            '  add them to TriggerEvents 
            For i = 0 To TriggerEvents.Count - 1
                TriggerEvents(i) += dailyEvents(i)
            Next
        Next

    End Sub

    Public Sub AddRow(Row As AssessmentTableRow)

        Rows.Add(Row)

    End Sub

    Public Function GetDateColumns() As List(Of AssessmentTableRow)

        Dim newRows As New List(Of AssessmentTableRow)

        For Each row In Rows
            Dim newRow As New AssessmentTableRow()
            For c = 0 To NumDateColumns - 1
                newRow.Cells.Add(row.Cells(c))
            Next
            newRows.Add(newRow)
        Next

        Return newRows

    End Function

    Public Function GetSumColumn() As List(Of AssessmentTableRow)

        Dim newRows As New List(Of AssessmentTableRow)

        For Each row In Rows
            Dim newRow As New AssessmentTableRow()
            If HasSumColumn Then
                newRow.Cells.Add(row.Cells(row.Cells.Count - 1))
            End If
            newRows.Add(newRow)
        Next

        Return newRows

    End Function

    Public Function CompressLevelGroups(columnGroups As List(Of List(Of Integer))) As List(Of AssessmentTableRow)

        ' N.B. don't use this after any merging

        Dim newRows As New List(Of AssessmentTableRow)
        For Each row In Rows
            Dim newRow As New AssessmentTableRow()
            For Each columnGroup In columnGroups
                Dim newCell As New AssessmentTableCell With {.NoResult = True}
                For Each columnIndex In columnGroup
                    If row.Cells.Count > columnIndex Then
                        If row.Cells(columnIndex).Text <> "" Then
                            newCell = row.Cells(columnIndex)
                        End If
                    End If
                Next
                newRow.AddCell(newCell)
            Next
            newRows.Add(newRow)
        Next

        Return newRows

    End Function
    Public Function CompressTriggerGroups(columnGroups As List(Of List(Of Integer))) As List(Of AssessmentTableRow)

        ' N.B. don't use this after any merging

        Dim newRows As New List(Of AssessmentTableRow)

        For Each row In Rows
            Dim newRow As New AssessmentTableRow()
            For Each columnGroup In columnGroups
                Dim newCell As New AssessmentTableCell With {.NoResult = True}
                Dim triggerSum = 0
                For Each columnIndex In columnGroup
                    If row.Cells.Count > columnIndex Then
                        If row.Cells(columnIndex).Text <> "" Then
                            triggerSum += CInt(row.Cells(columnIndex).Text)
                        End If
                    End If
                Next
                newRow.AddCell(New AssessmentTableCell With {.Text = triggerSum.ToString})
            Next
            newRows.Add(newRow)
        Next

        Return newRows

    End Function


End Class

Public Class AssessmentTableFooter

    Public Property Rows As List(Of AssessmentTableRow)
    Public Property ShowTitles As Boolean
    Public Property HasSumColumn As Boolean
    Public Property HasPeriodExceedances As Boolean
    Public Property HasDaysWithExceedances As Boolean
    Public Property HasDailyEvents As Boolean
    Private Property Results As List(Of List(Of AssessmentResultSingleDay))
    Private Property Criteria As List(Of AssessmentCriterion)


    Public Sub New(group As AssessmentCriterionGroup,
                   body As AssessmentTableBody,
                   assessmentResults As List(Of List(Of AssessmentResultSingleDay)),
                   assessmentCriteria As List(Of AssessmentCriterion),
                   dates As List(Of Date))

        Rows = New List(Of AssessmentTableRow)
        ShowTitles = group.ShowSumTitles
        HasSumColumn = group.SumExceedancesAcrossCriteria
        HasPeriodExceedances = group.SumPeriodExceedances
        HasDaysWithExceedances = group.SumDaysWithExceedances
        HasDailyEvents = group.SumDailyEvents

        Dim numDateAndLevelColumns = group.NumDateColumns + body.NumLevelColumns
        Dim titleColumnSpan As Integer
        If numDateAndLevelColumns = 0 Then
            titleColumnSpan = 1
        Else
            titleColumnSpan = numDateAndLevelColumns
        End If

        ' Filter out assessment criteria that should not be tabulated
        For c = assessmentCriteria.Count - 1 To 0 Step -1
            If assessmentCriteria(c).TabulateCriterionTriggers = False Then
                assessmentCriteria.RemoveAt(c)
                assessmentResults.RemoveAt(c)
            End If
        Next
        Criteria = assessmentCriteria
        Results = assessmentResults

        ' Total exceedances over period
        If HasPeriodExceedances Then
            Dim triggersSumRow = New AssessmentTableRow
            ' Title cell
            If ShowTitles Then
                triggersSumRow.AddCell(
                    New AssessmentTableCell With {
                        .Text = "Total Exceedances for Criterion Period",
                        .ColSpan = titleColumnSpan
                    }
                )
            End If
            ' Sum exceedances
            Dim sumsOfExceedancesByCriterion = SumExceedancesByCriterion(assessmentResults)
            For Each exceedanceSum In sumsOfExceedancesByCriterion
                triggersSumRow.AddCell(
                    New AssessmentTableCell With {.Text = exceedanceSum.ToString}
                )
            Next
            If group.SumExceedancesAcrossCriteria Then
                triggersSumRow.AddCell(
                    New AssessmentTableCell With {
                        .Text = sumsOfExceedancesByCriterion.Sum().ToString
                    }
                )
            End If
            Rows.Add(triggersSumRow)
        End If

        ' Days with exceedances in period
        If HasDaysWithExceedances Then
            Dim triggerDaysSumRow = New AssessmentTableRow
            ' Title cell
            If ShowTitles Then
                triggerDaysSumRow.AddCell(
                    New AssessmentTableCell With {
                        .Text = "# Days with Exceedances for Criterion Period",
                        .ColSpan = titleColumnSpan
                    }
                )
            End If
            ' Sum exceedances
            Dim daysWithExceedancesByCriterion = GetDaysWithExceedancesByCriterion(assessmentResults)
            For Each criterionDays In daysWithExceedancesByCriterion
                triggerDaysSumRow.AddCell(
                    New AssessmentTableCell With {.Text = criterionDays.ToString}
                )
            Next
            If group.SumExceedancesAcrossCriteria Then
                triggerDaysSumRow.AddCell(
                    New AssessmentTableCell With {
                        .Text = daysWithExceedancesByCriterion.Sum().ToString
                    }
                )
            End If
            Rows.Add(triggerDaysSumRow)
        End If

        ' Events
        If HasDailyEvents Then
            Dim eventsSumRow = New AssessmentTableRow
            ' Title cell
            If ShowTitles Then
                eventsSumRow.AddCell(
                    New AssessmentTableCell With {
                        .Text = "# Exceedance Events for Criterion Period",
                        .ColSpan = titleColumnSpan
                    }
                )
            End If
            ' Sum cells
            Dim sumsOfEventsByCriterion = SumExceedanceEventsByCriterion(assessmentResults)
            For Each criterionSumOfEvents In sumsOfEventsByCriterion
                eventsSumRow.AddCell(
                    New AssessmentTableCell With {
                        .Text = criterionSumOfEvents.ToString
                    }
                )
            Next
            If group.SumExceedancesAcrossCriteria Then
                eventsSumRow.AddCell(
                    New AssessmentTableCell With {
                        .Text = sumsOfEventsByCriterion.Sum().ToString
                    }
                )
            End If
            Rows.Add(eventsSumRow)
        End If

    End Sub

    Public Function GetTitleColumn() As List(Of AssessmentTableRow)

        Dim newRows As New List(Of AssessmentTableRow)

        For Each row In Rows
            Dim newRow As New AssessmentTableRow()
            If ShowTitles Then newRow.Cells.Add(row.Cells(0))
            newRows.Add(newRow)
        Next

        Return newRows

    End Function
    Public Function CompressTriggerGroups(
        columnGroups As List(Of List(Of Integer)), numDateAndLevelColumns As Integer
    ) As List(Of AssessmentTableRow)

        Dim newRows As New List(Of AssessmentTableRow)
        Dim rowIndex As Integer = 0

        Dim subtractFromIndex As Integer = numDateAndLevelColumns

        ' Period Exceedances
        If HasPeriodExceedances Then
            Dim row = Rows(rowIndex)
            Dim newRow As New AssessmentTableRow()
            For Each columnGroup In columnGroups
                Dim groupResults = New List(Of List(Of AssessmentResultSingleDay))
                For Each columnIndex In columnGroup
                    groupResults.Add(Results(columnIndex - subtractFromIndex))
                Next
                Dim triggerSum = groupResults.SumOfExceedances
                newRow.AddCell(
                    New AssessmentTableCell With {.Text = triggerSum.ToString}
                )
            Next
            newRows.Add(newRow)
            rowIndex += 1
        End If

        ' Days with exceedances
        If HasDaysWithExceedances Then
            Dim row = Rows(rowIndex)
            Dim newRow As New AssessmentTableRow()
            For Each columnGroup In columnGroups
                Dim groupResults = New List(Of List(Of AssessmentResultSingleDay))
                For Each columnIndex In columnGroup
                    groupResults.Add(Results(columnIndex - subtractFromIndex))
                Next
                Dim daysWithExceedances = groupResults.CountDaysOfExceedances
                newRow.AddCell(
                    New AssessmentTableCell With {.Text = daysWithExceedances.ToString}
                )
            Next
            newRows.Add(newRow)
            rowIndex += 1
        End If

        ' Trigger events
        If HasDailyEvents Then
            Dim row = Rows(rowIndex)
            Dim newRow As New AssessmentTableRow()
            For Each columnGroup In columnGroups
                Dim groupResults = New List(Of List(Of AssessmentResultSingleDay))
                For Each columnIndex In columnGroup
                    groupResults.Add(Results(columnIndex - subtractFromIndex))
                Next
                Dim eventsSum = SumExceedanceEvents(groupResults)
                newRow.AddCell(
                    New AssessmentTableCell With {.Text = eventsSum.ToString}
                )
            Next
            newRows.Add(newRow)
        End If

        Return newRows

    End Function

    Public Function GetSumColumn() As List(Of AssessmentTableRow)

        Dim newRows As New List(Of AssessmentTableRow)

        For Each row In Rows
            Dim newRow As New AssessmentTableRow()
            If HasSumColumn Then
                newRow.Cells.Add(row.Cells(row.Cells.Count - 1))
            End If
            newRows.Add(newRow)
        Next

        Return newRows

    End Function


End Class

Public Class AssessmentTableViewModel

    Public Property Header As AssessmentTableHeader
    Public Property Body As AssessmentTableBody
    Public Property Footer As AssessmentTableFooter
    Public Property NumDateColumns As Integer
    Public Property NumLevelColumns As Integer
    Public Property NumTriggerColumns As Integer

    Public Sub New(group As AssessmentCriterionGroup,
                   header As AssessmentTableHeader,
                   body As AssessmentTableBody,
                   footer As AssessmentTableFooter)

        ' Initial setup
        Dim origNumDateColumns = header.NumDateColumns
        Dim origNumLevelColumns = header.NumLevelColumns
        Dim origNumTriggerColumns = header.NumTriggerColumns

        NumDateColumns = header.NumDateColumns
        NumLevelColumns = header.NumLevelColumns
        NumTriggerColumns = header.NumTriggerColumns

        ' Get Level and Trigger Groups
        Dim levelGroups = header.MatchingColumnGroups(FirstLevelColumnIndex(), LastLevelColumnIndex())
        Dim triggerGroups = header.MatchingColumnGroups(FirstTriggerColumnIndex(), LastTriggerColumnIndex())

        ' Compress Level Column Groups
        Dim dateHeaderCols = header.GetDateColumns()
        Dim levelHeaderCols = header.CompressColumnGroups(levelGroups)
        Dim triggerHeaderCols = header.CompressColumnGroups(triggerGroups)
        Dim triggerSumHeaderCol = header.GetSumColumn()
        Dim newHeaderRows = ConcatenateColumns(
            {dateHeaderCols,
             levelHeaderCols,
             triggerHeaderCols,
             triggerSumHeaderCol}.ToList()
         )

        ' Compress Trigger Column Groups
        Dim dateBodyCols = body.GetDateColumns()
        Dim levelBodyCols = body.CompressLevelGroups(levelGroups)
        NumLevelColumns = levelGroups.Count
        Dim triggerBodyCols = body.CompressTriggerGroups(triggerGroups)
        NumTriggerColumns = triggerGroups.Count
        Dim triggerSumBodyCol = body.GetSumColumn()
        Dim newBodyRows = ConcatenateColumns({dateBodyCols, levelBodyCols, triggerBodyCols, triggerSumBodyCol}.ToList())
        Dim titleFooterCol = footer.GetTitleColumn()
        Dim triggerFooterCols = footer.CompressTriggerGroups(triggerGroups, origNumDateColumns + origNumLevelColumns)
        Dim triggerSumFooterCol = footer.GetSumColumn()
        Dim newFooterRows = ConcatenateColumns(
            {titleFooterCol, triggerFooterCols, triggerSumFooterCol}.ToList())
        For Each footerColRow In newFooterRows
            footerColRow.Cells(0).ColSpan = NumDateColumns + NumLevelColumns
        Next

        ' Add a new blank cell to the start of each header row if there are no date or level columns and ShowExceedanceSumTitles is True
        If NumDateColumns = 0 And NumLevelColumns = 0 And group.ShowSumTitles Then
            newHeaderRows(0).Cells.Insert(
                0, New AssessmentTableCell With {
                    .RowSpan = header.Rows.Count + body.Rows.Count
                }
            )
        End If

        ' Update self, header and body
        Me.NumDateColumns = group.NumDateColumns
        Me.NumLevelColumns = levelGroups.Count
        Me.NumTriggerColumns = triggerGroups.Count
        header.Rows = newHeaderRows
        body.Rows = newBodyRows
        footer.Rows = newFooterRows

        header.MergeCells(FirstLevelColumnIndex, LastTriggerColumnIndex)

        ' Assign header, body and footer
        Me.Header = header
        Me.Body = body
        Me.Footer = footer

    End Sub

    Public Function ConcatenateColumns(ColumnGroups As List(Of List(Of AssessmentTableRow))) As List(Of AssessmentTableRow)

        ' N.B. All column groups (i.e. lists of rows) must have the same number of rows

        Dim numRows = ColumnGroups(0).Count
        Dim newRows As New List(Of AssessmentTableRow)

        For r = 0 To numRows - 1
            Dim newRow As New AssessmentTableRow
            For Each columnGroup In ColumnGroups
                newRow.AppendRow(columnGroup(r))
            Next
            newRows.Add(newRow)
        Next

        Return newRows

    End Function

    Public Function FirstDateColumnIndex() As Integer

        Return 0

    End Function
    Public Function LastDateColumnIndex() As Integer

        Return NumDateColumns - 1

    End Function
    Public Function FirstLevelColumnIndex() As Integer

        Return NumDateColumns

    End Function
    Public Function LastLevelColumnIndex() As Integer

        Return NumDateColumns + NumLevelColumns - 1

    End Function
    Public Function FirstTriggerColumnIndex() As Integer

        Return NumDateColumns + NumLevelColumns

    End Function
    Public Function LastTriggerColumnIndex() As Integer

        Return NumDateColumns + NumLevelColumns + NumTriggerColumns - 1

    End Function

    Public Function DailySumColumnIndex() As Integer

        Return LastTriggerColumnIndex() + 1

    End Function


End Class

#End Region

#End Region

#End Region

#Region "Calculation Filters"

Public Class CalculationFilterDetailsViewModel

    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class ViewCalculationFiltersViewModel

    Implements IViewObjectsByMeasurementTypeViewModel

    Public Property CalculationFilters As List(Of CalculationFilter)
    Public Property ObjectDisplayName As String Implements IViewObjectsByMeasurementTypeViewModel.ObjectDisplayName
    Public Property ObjectName As String Implements IViewObjectsByMeasurementTypeViewModel.ObjectName
    Public Property SearchText As String Implements IViewObjectsByMeasurementTypeViewModel.SearchText
    Public Property TableId As String Implements IViewObjectsByMeasurementTypeViewModel.TableId
    Public Property UpdateTableRouteName As String Implements IViewObjectsByMeasurementTypeViewModel.UpdateTableRouteName
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)
    Public Property MeasurementTypeId As String Implements IViewObjectsByMeasurementTypeViewModel.MeasurementTypeId
    Public Property MeasurementTypeList As SelectList Implements IViewObjectsByMeasurementTypeViewModel.MeasurementTypeList

End Class

Public Class CreateCalculationFilterViewModel

    ' Main Model
    Public Property CalculationFilter As CalculationFilter

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Measurement Metric from the list.")> _
    Public Property MeasurementMetricId As Integer
    Public Property MeasurementMetricList As SelectList
    <Required(ErrorMessage:="Please select the Calculation Aggregate Function from the list.")> _
    Public Property CalculationAggregateFunctionId As Integer
    Public Property CalculationAggregateFunctionList As SelectList

    ' Special Items
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property TimeBase As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property TimeStep As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
<DataType(DataType.Time)> _
    Public Property TimeWindowStartTime As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
<DataType(DataType.Time)> _
    Public Property TimeWindowEndTime As Date

    Public Property AppliesOnMondays As Boolean = True
    Public Property AppliesOnTuesdays As Boolean = True
    Public Property AppliesOnWednesdays As Boolean = True
    Public Property AppliesOnThursdays As Boolean = True
    Public Property AppliesOnFridays As Boolean = True
    Public Property AppliesOnSaturdays As Boolean = True
    Public Property AppliesOnSundays As Boolean = True
    Public Property AppliesOnPublicHolidays As Boolean = False


End Class

Public Class EditCalculationFilterViewModel

    ' Main Model
    Public Property CalculationFilter As CalculationFilter

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Calculation Aggregate Function from the list.")> _
    Public Property CalculationAggregateFunctionId As Integer
    Public Property CalculationAggregateFunctionList As SelectList
    <Required(ErrorMessage:="Please select the Measurement Metric from the list.")> _
    Public Property MeasurementMetricId As Integer
    Public Property MeasurementMetricList As SelectList
    ' Multiple Select Items
    Public Property AllApplicableDaysOfWeek As IEnumerable(Of DayOfWeek)

    ' Special Items
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property TimeBase As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property TimeStep As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property TimeWindowStartTime As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
<DataType(DataType.Time)> _
    Public Property TimeWindowEndTime As Date

    ' Getters
    Public ReadOnly Property getCalculationFilterApplicableDaysOfWeekIds As IEnumerable(Of Integer)
        Get
            Return CalculationFilter.ApplicableDaysOfWeek.Select(Function(c) c.Id).ToList
        End Get
    End Property

End Class

#End Region

#Region "Contacts"

Public Class ContactDetailsViewModel

    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)


End Class

Public Class ViewContactsViewModel

    Implements IViewObjectsViewModel

    Public Property Contacts As List(Of Contact)

    Public Property ObjectDisplayName As String Implements IViewObjectsViewModel.ObjectDisplayName

    Public Property ObjectName As String Implements IViewObjectsViewModel.ObjectName

    Public Property SearchText As String Implements IViewObjectsViewModel.SearchText

    Public Property TableId As String Implements IViewObjectsViewModel.TableId

    Public Property UpdateTableRouteName As String Implements IViewObjectsViewModel.UpdateTableRouteName

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class
Public Class CreateContactViewModel

    ' Main Model
    Public Property Contact As Contact

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Organisation from the list.")> _
    Public Property OrganisationId As Integer
    Public Property OrganisationList As SelectList
    '<Required(ErrorMessage:="Please select the User Access Level from the list.")> _
    'Public Property UserAccessLevelId As Integer
    'Public Property UserAccessLevelList As SelectList

End Class
Public Class EditContactViewModel

    ' Main Model
    Public Property Contact As Contact

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Organisation from the list.")> _
    Public Property OrganisationId As Integer
    Public Property OrganisationList As SelectList

    'Public Property UserAccessLevelId As Nullable(Of Integer)
    'Public Property UserAccessLevelList As SelectList
    ' Multiple Select Items
    Public Property AllProjects As IEnumerable(Of Project)
    Public Property AllExcludedDocuments As IEnumerable(Of Document)

    ' Getters
    Public ReadOnly Property getContactProjectIds As IEnumerable(Of Integer)
        Get
            Return Contact.Projects.Select(Function(c) c.Id).ToList
        End Get
    End Property
    Public ReadOnly Property getContactExcludedDocumentIds As IEnumerable(Of Integer)
        Get
            Return Contact.ExcludedDocuments.Select(Function(c) c.Id).ToList
        End Get
    End Property

End Class

#End Region

#Region "Countries"

Public Class CountryDetailsViewModel

    Public Property Country As Country
    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)


End Class

Public Class ViewCountriesViewModel

    Implements IViewObjectsViewModel

    Public Property Countries As List(Of Country)

    Public Property ObjectDisplayName As String Implements IViewObjectsViewModel.ObjectDisplayName

    Public Property ObjectName As String Implements IViewObjectsViewModel.ObjectName

    Public Property SearchText As String Implements IViewObjectsViewModel.SearchText

    Public Property TableId As String Implements IViewObjectsViewModel.TableId

    Public Property UpdateTableRouteName As String Implements IViewObjectsViewModel.UpdateTableRouteName

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

#End Region

#Region "Documents"

Public Class ViewDocumentsViewModel

    Implements IViewObjectsByDocumentTypeViewModel

    Public Property Documents As List(Of Document)
    Public Property Projects As List(Of Project)

    Public Property MonitorLocations As List(Of MonitorLocation)
    Public Property Monitors As List(Of Monitor)
    Public Property AuthorOrganisations As List(Of Organisation)

    Public Property ProjectIds As String Implements IViewObjectsByDocumentTypeViewModel.ProjectIds
    Public Property SearchText As String Implements IViewObjectsByDocumentTypeViewModel.SearchText
    Public Property TableId As String Implements IViewObjectsByDocumentTypeViewModel.TableId
    Public Property UpdateTableRouteName As String Implements IViewObjectsByDocumentTypeViewModel.UpdateTableRouteName
    Public Property ObjectName As String Implements IViewObjectsByDocumentTypeViewModel.ObjectName
    Public Property ObjectDisplayName As String Implements IViewObjectsByDocumentTypeViewModel.ObjectDisplayName

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)
    Public Property DocumentTypeId As String Implements IViewObjectsByDocumentTypeViewModel.DocumentTypeId
    Public Property DocumentTypeList As SelectList Implements IViewObjectsByDocumentTypeViewModel.DocumentTypeList

End Class

Public Class DocumentDetailsViewModel

    Public Property Document As Document
    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)


End Class

Public Class CreateDocumentViewModel

    ' Main Model
    Public Property Document As Document

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Document Type from the list.")> _
    Public Property DocumentTypeId As Integer
    Public Property DocumentTypeList As SelectList
    <Required(ErrorMessage:="Please select the Author Organisation from the list.")> _
    Public Property AuthorOrganisationId As Integer
    Public Property AuthorOrganisationList As SelectList

    'Date and Time Items
    <DataType(DataType.Date)> _
    Public Property StartDate As Date

    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property StartTime As Date

    <DataType(DataType.Date)> _
    Public Property EndDate As Date

    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property EndTime As Date

    ' Special Items
    Public Property UploadFile As HttpPostedFileBase

    ' Selectable Projects
    Public Property AvailableProjects As IEnumerable(Of SelectableProject)
    Public Property SelectedProjects As IEnumerable(Of SelectableProject)
    Public Property PostedProjects As PostedProjects

    ' Selectable Monitors
    Public Property AvailableMonitors As IEnumerable(Of SelectableMonitor)
    Public Property SelectedMonitors As IEnumerable(Of SelectableMonitor)
    Public Property PostedMonitors As PostedMonitors

    ' Selectable MonitorLocations
    Public Property AvailableMonitorLocations As IEnumerable(Of SelectableMonitorLocation)
    Public Property SelectedMonitorLocations As IEnumerable(Of SelectableMonitorLocation)
    Public Property PostedMonitorLocations As PostedMonitorLocations

    ' Return to page parameters
    Public Property ReturnToRouteName As String
    Public Property ReturnToRouteValues As Object

End Class

Public Class SelectableProject

    Public Sub New(Project As Project)

        Id = Project.Id
        Name = Project.FullName

    End Sub

    Public Property Id As Integer
    Public Property Name As String
    Public Property IsSelected As Boolean
    Public Property Tags As Object

End Class
Public Class PostedProjects

    Public Property ProjectIds As String()

End Class
Public Class SelectableMonitor

    Public Sub New(Monitor As Monitor)

        Id = Monitor.Id
        Name = Monitor.MonitorName + If(Monitor.CurrentLocation IsNot Nothing, " (" + Monitor.CurrentLocation.MonitorLocationName + ") - " + Monitor.CurrentLocation.Project.FullName, "")

    End Sub

    Public Property Id As Integer
    Public Property Name As String
    Public Property IsSelected As Boolean
    Public Property Tags As Object

End Class
Public Class PostedMonitors

    Public Property MonitorIds As String()

End Class
Public Class SelectableMonitorLocation

    Public Sub New(MonitorLocation As MonitorLocation)

        Id = MonitorLocation.Id
        Name = MonitorLocation.MonitorLocationName + If(MonitorLocation.CurrentMonitor IsNot Nothing,
                                                        " (" + MonitorLocation.CurrentMonitor.MonitorName + ")",
                                                        "") + " - " + MonitorLocation.Project.FullName

    End Sub

    Public Property Id As Integer
    Public Property Name As String
    Public Property IsSelected As Boolean
    Public Property Tags As Object

End Class
Public Class PostedMonitorLocations

    Public Property MonitorLocationIds As String()

End Class

Public Class EditDocumentViewModel

    ' Main Model
    Public Property Document As Document

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Document Type from the list.")> _
    Public Property DocumentTypeId As Integer
    Public Property DocumentTypeList As SelectList
    <Required(ErrorMessage:="Please select the Author Organisation from the list.")> _
    Public Property AuthorOrganisationId As Integer
    Public Property AuthorOrganisationList As SelectList

    ' Multiple Select Items
    Public Property AllMonitors As IEnumerable(Of Monitor)
    Public Property AllMonitorLocations As IEnumerable(Of MonitorLocation)
    Public Property AllExcludedContacts As IEnumerable(Of Contact)
    Public Property AllProjects As IEnumerable(Of Project)

    'Date and Time Items
    <DataType(DataType.Date)> _
    Public Property StartDate As Date

    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property StartTime As Date

    <DataType(DataType.Date)> _
    Public Property EndDate As Date

    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property EndTime As Date

    Public ReadOnly Property getDocumentProjectIds As IEnumerable(Of Integer)
        Get
            Return Document.Projects.Select(Function(p) p.Id).ToList
        End Get
    End Property
    Public ReadOnly Property getDocumentMonitorIds As IEnumerable(Of Integer)
        Get
            Return Document.Monitors.Select(Function(m) m.Id).ToList
        End Get
    End Property
    Public ReadOnly Property getDocumentMonitorLocationIds As IEnumerable(Of Integer)
        Get
            Return Document.MonitorLocations.Select(Function(ml) ml.Id).ToList
        End Get
    End Property
    Public ReadOnly Property getDocumentExcludedContactIds As IEnumerable(Of Integer)
        Get
            Return Document.ExcludedContacts.Select(Function(ec) ec.Id).ToList
        End Get
    End Property

End Class



#End Region

#Region "Document Types"

Public Class DocumentTypeDetailsViewModel

    Public Property DocumentType As DocumentType
    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)


End Class

Public Class ViewDocumentTypesViewModel

    Implements IViewObjectsViewModel

    Public Property DocumentTypes As List(Of DocumentType)

    Public Property ObjectDisplayName As String Implements IViewObjectsViewModel.ObjectDisplayName

    Public Property ObjectName As String Implements IViewObjectsViewModel.ObjectName

    Public Property SearchText As String Implements IViewObjectsViewModel.SearchText

    Public Property TableId As String Implements IViewObjectsViewModel.TableId

    Public Property UpdateTableRouteName As String Implements IViewObjectsViewModel.UpdateTableRouteName

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class EditDocumentTypeViewModel

    ' Main Model
    Public Property DocumentType As DocumentType

    '' Multiple Select Items
    'Public Property AllChildDocumentTypes As IEnumerable(Of DocumentType)
    'Public Property AllParentDocumentTypes As IEnumerable(Of DocumentType)

    '' Getters
    'Public ReadOnly Property getDocumentTypeChildDocumentTypeIds As IEnumerable(Of Integer)
    '    Get
    '        Return DocumentType.ChildDocumentTypes.Select(Function(c) c.Id).ToList
    '    End Get
    'End Property
    'Public ReadOnly Property getDocumentTypeParentDocumentTypeIds As IEnumerable(Of Integer)
    '    Get
    '        Return DocumentType.ParentDocumentTypes.Select(Function(c) c.Id).ToList
    '    End Get
    'End Property

End Class


#End Region

#Region "Home"

Public Class HomePageViewModel

    Public Property Message As String
    Public Property Buttons As List(Of HomePageButtonViewModel)
    Public Property SystemMessages As List(Of SystemMessage)

End Class

Public Class HomePageButtonViewModel

    Public Sub New(Text As String, RouteName As String, ButtonClass As String, HelpText As String)

        Me.Text = Text
        Me.RouteName = RouteName
        Me.ButtonClass = ButtonClass
        Me.HelpText = HelpText


    End Sub

    Public Property Text As String
    Public Property RouteName As String
    Public Property HelpText As String
    Public Property ButtonClass As String

End Class

#End Region

#Region "LogIn"

Public Class LogInViewModel

    Public Property UserName As String

    Public Property Password As String

    Public Property ReturnUrl As String

    <DataType(DataType.EmailAddress)> _
    Public Property ResetPasswordEmailAddress As String

    Public Property EnterNewPassword As String
    Public Property ReEnterNewPassword As String
    Public Property PasswordResetUserId As Integer
    Public Property CurrentAction As String

End Class

Public Class ChangePasswordViewModel

    <Required()> _
    Public Property UserName As String

    <Required()> _
    Public Property OldPassword As String

    <Required()> _
    Public Property NewPassword As String

    <Required()> _
    Public Property ConfirmNewPassword As String

End Class

Public Class RequestNewPasswordViewModel

    <Required()> _
    <DataType(DataType.EmailAddress)> _
    Public Property AccountEmailAddress As String

End Class

#End Region

#Region "Measurements"

Public Class ViewMeasurementsViewModel

    Property CurrentMonitor As Monitor
    Property MonitorLocation As MonitorLocation
    Property MonitorLocationRouteName As String
    Property FilteredMeasurements As IEnumerable(Of FilteredMeasurementsSequence) ' For dynamic tables
    Property SummaryLevels As List(Of List(Of LevelsSummarySingleDay))
    Property Filters As IEnumerable(Of CalculationFilter)
    Property StartDateTimes As IEnumerable(Of Date)
    Property StartDate As Date
    Property EndDate As Date
    Property Project As Project
    Property ProjectRouteName As String
    Property ViewDuration As String
    Property ViewName As String

    Property SelectedMeasurementView As MeasurementView
    Property SelectableMeasurementViews As IEnumerable(Of MeasurementView)

    Public Property FirstMeasurementDate As Date
    Public Property LastMeasurementDate As Date

    Public Property VSliderMinValue As Integer
    Public Property VSliderMaxValue As Integer
    Public Property VSliderMinLimit As Integer
    Public Property VSliderMaxLimit As Integer
    Public Property VSliderStep As Integer


    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class
Public Class ViewMeasurementsChartViewModel

    Public Property Chart As Highcharts
    Public Property RangeYmin As Double
    Public Property RangeYmax As Double
    Public Property RangeYstep As Double

End Class
Public Class ViewMeasurementsNavigatorViewModel

    Property Project As Project
    Property MonitorLocation As MonitorLocation
    Property View As MeasurementView
    Property ViewDuration As String
    Property CurrentViewDate As Date

End Class
Public Class UploadMeasurementsViewModel

    ' Main Model
    Public Property MeasurementFile As MeasurementFile
    Public Property MeasurementFileTypes As List(Of MeasurementFileType)


    ' Single Select Items
    <Required(ErrorMessage:="Please select the Measurement File Type from the list.")> _
    Public Property MeasurementFileTypeId As Integer
    Public Property MeasurementFileTypeList As SelectList

    ' Display Properties
    Property Monitor As Monitor
    Property MonitorLocation As MonitorLocation

    ' Properties for Measurement File Record
    Property MonitorId As Integer
    Property MonitorLocationId As Integer
    Property ProjectId As Integer

    ' Sub-ViewModels
    Public Property UploadOsirisViewModel As UploadOsirisViewModel
    Public Property UploadPPVLiveViewModel As UploadPPVLiveViewModel
    Public Property UploadRedboxViewModel As UploadRedboxViewModel
    Public Property UploadRionLeqLiveWebsystemViewModel As UploadRionLeqLiveWebsystemViewModel
    Public Property UploadRionRCDSViewModel As UploadRionRCDSViewModel
    Public Property UploadSpreadsheetTemplateViewModel As UploadSpreadsheetTemplateViewModel
    Public Property UploadSigicomVibrationViewModel As UploadSigicomVibrationViewModel
    Public Property UploadSPLTrackViewModel As UploadSPLTrackViewModel
    Public Property UploadVibraViewModel As UploadVibraViewModel


    ' Special Items
    Public Property UploadFile As HttpPostedFileBase

    ' Measurement Rounding
    Public Property RoundInputMeasurements As Boolean
    Public Property RoundInputCount As Integer = 1
    Public Property RoundInputDurationValue As String
    Public Property RoundInputDurationList As SelectList

    ' Measurement Time Offset
    Public Property AddTimeOffset As Boolean
    Public Property AddTimeCount As Integer = 1
    Public Property AddTimeDurationValue As String
    Public Property AddTimeDurationList As SelectList

    ' Daylight Savings Offset
    Public Property AddDaylightSavingsTimeOffset As Boolean
    Public Property AddDaylightSavingsHourCount As Integer = 1

    <DataType(DataType.Date)> _
    Public Property DaylightSavingsBorderDate As Date

    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property DaylightSavingsBorderTime As DateTime

End Class

Public Class UploadResultViewModel

    Property MeasurementFile As MeasurementFile
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

#Region "Upload File View Models"

#Region "Air Quality"

Public Class UploadOsirisViewModel

    Public Property TotalParticlesMapping As MetricMapping
    Public Property PM10ParticlesMapping As MetricMapping
    Public Property PM2point5ParticlesMapping As MetricMapping
    Public Property PM1ParticlesMapping As MetricMapping
    Public Property TemperatureMapping As MetricMapping
    Public Property HumidityMapping As MetricMapping
    Public Property WindSpeedMapping As MetricMapping
    Public Property WindDirectionMapping As MetricMapping


    Public Function getMappingDictionary(MeasurementsDAL As IMeasurementsDAL) As Dictionary(Of String, MeasurementMetric)

        Dim mapDict As New Dictionary(Of String, MeasurementMetric)

        If TotalParticlesMapping.MetricId IsNot Nothing Then mapDict.Add(TotalParticlesMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(TotalParticlesMapping.MetricId)))
        If PM10ParticlesMapping.MetricId IsNot Nothing Then mapDict.Add(PM10ParticlesMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(PM10ParticlesMapping.MetricId)))
        If PM2point5ParticlesMapping.MetricId IsNot Nothing Then mapDict.Add(PM2point5ParticlesMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(PM2point5ParticlesMapping.MetricId)))
        If PM1ParticlesMapping.MetricId IsNot Nothing Then mapDict.Add(PM1ParticlesMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(PM1ParticlesMapping.MetricId)))
        If TemperatureMapping.MetricId IsNot Nothing Then mapDict.Add(TemperatureMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(TemperatureMapping.MetricId)))
        If HumidityMapping.MetricId IsNot Nothing Then mapDict.Add(HumidityMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(HumidityMapping.MetricId)))
        If WindSpeedMapping.MetricId IsNot Nothing Then mapDict.Add(WindSpeedMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(WindSpeedMapping.MetricId)))
        If WindDirectionMapping.MetricId IsNot Nothing Then mapDict.Add(WindDirectionMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(WindDirectionMapping.MetricId)))

        Return mapDict

    End Function

End Class

#End Region

#Region "Noise"

Public Class UploadRionLeqLiveWebsystemViewModel

    Public Property LpMapping As MetricMapping
    Public Property LeqMapping As MetricMapping
    Public Property LmaxMapping As MetricMapping
    Public Property LminMapping As MetricMapping

    Public Property ln1Mapping As MetricMapping
    Public Property ln2Mapping As MetricMapping
    Public Property ln3Mapping As MetricMapping
    Public Property ln4Mapping As MetricMapping
    Public Property ln5Mapping As MetricMapping

    Public Function getMappingDictionary(MeasurementsDAL As IMeasurementsDAL) As Dictionary(Of String, MeasurementMetric)

        Dim mapDict As New Dictionary(Of String, MeasurementMetric)

        If LpMapping.MetricId IsNot Nothing Then mapDict.Add(LpMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LpMapping.MetricId)))
        If LeqMapping.MetricId IsNot Nothing Then mapDict.Add(LeqMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LeqMapping.MetricId)))
        If LmaxMapping.MetricId IsNot Nothing Then mapDict.Add(LmaxMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LmaxMapping.MetricId)))
        If LminMapping.MetricId IsNot Nothing Then mapDict.Add(LminMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LminMapping.MetricId)))
        If ln1Mapping.MetricId IsNot Nothing Then mapDict.Add(ln1Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(ln1Mapping.MetricId)))
        If ln2Mapping.MetricId IsNot Nothing Then mapDict.Add(ln2Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(ln2Mapping.MetricId)))
        If ln3Mapping.MetricId IsNot Nothing Then mapDict.Add(ln3Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(ln3Mapping.MetricId)))
        If ln4Mapping.MetricId IsNot Nothing Then mapDict.Add(ln4Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(ln4Mapping.MetricId)))
        If ln5Mapping.MetricId IsNot Nothing Then mapDict.Add(ln5Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(ln5Mapping.MetricId)))

        Return mapDict

    End Function

End Class
Public Class UploadRionRCDSViewModel

    Public Property LAeqMapping As MetricMapping
    Public Property LAEMapping As MetricMapping
    Public Property LAmaxMapping As MetricMapping
    Public Property LAminMapping As MetricMapping
    Public Property LA05Mapping As MetricMapping
    Public Property LA10Mapping As MetricMapping
    Public Property LA50Mapping As MetricMapping
    Public Property LA90Mapping As MetricMapping
    Public Property LA95Mapping As MetricMapping
    Public Property LCpkMapping As MetricMapping


    Public Function getMappingDictionary(MeasurementsDAL As IMeasurementsDAL) As Dictionary(Of String, MeasurementMetric)

        Dim mapDict As New Dictionary(Of String, MeasurementMetric)

        If LAeqMapping.MetricId IsNot Nothing Then mapDict.Add(LAeqMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LAeqMapping.MetricId)))
        If LAEMapping.MetricId IsNot Nothing Then mapDict.Add(LAEMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LAEMapping.MetricId)))
        If LAmaxMapping.MetricId IsNot Nothing Then mapDict.Add(LAmaxMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LAmaxMapping.MetricId)))
        If LAminMapping.MetricId IsNot Nothing Then mapDict.Add(LAminMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LAminMapping.MetricId)))
        If LA05Mapping.MetricId IsNot Nothing Then mapDict.Add(LA05Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LA05Mapping.MetricId)))
        If LA10Mapping.MetricId IsNot Nothing Then mapDict.Add(LA10Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LA10Mapping.MetricId)))
        If LA50Mapping.MetricId IsNot Nothing Then mapDict.Add(LA50Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LA50Mapping.MetricId)))
        If LA90Mapping.MetricId IsNot Nothing Then mapDict.Add(LA90Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LA90Mapping.MetricId)))
        If LA95Mapping.MetricId IsNot Nothing Then mapDict.Add(LA95Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LA95Mapping.MetricId)))
        If LCpkMapping.MetricId IsNot Nothing Then mapDict.Add(LCpkMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LCpkMapping.MetricId)))

        Return mapDict

    End Function

End Class
Public Class UploadSPLTrackViewModel

    Public Property PeriodLAeqMapping As MetricMapping
    Public Property LAmaxMapping As MetricMapping
    Public Property L10Mapping As MetricMapping
    Public Property L90Mapping As MetricMapping


    Public Function getMappingDictionary(MeasurementsDAL As IMeasurementsDAL) As Dictionary(Of String, MeasurementMetric)

        Dim mapDict As New Dictionary(Of String, MeasurementMetric)

        If PeriodLAeqMapping.MetricId IsNot Nothing Then mapDict.Add(PeriodLAeqMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(PeriodLAeqMapping.MetricId)))
        If LAmaxMapping.MetricId IsNot Nothing Then mapDict.Add(LAmaxMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(LAmaxMapping.MetricId)))
        If L10Mapping.MetricId IsNot Nothing Then mapDict.Add(L10Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(L10Mapping.MetricId)))
        If L90Mapping.MetricId IsNot Nothing Then mapDict.Add(L90Mapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(L90Mapping.MetricId)))

        Return mapDict

    End Function

End Class

#End Region

#Region "Vibration"

Public Class UploadPPVLiveViewModel

    Public Property XcvMapping As MetricMapping
    Public Property YcvMapping As MetricMapping
    Public Property ZcvMapping As MetricMapping
    Public Property XcfMapping As MetricMapping
    Public Property YcfMapping As MetricMapping
    Public Property ZcfMapping As MetricMapping
    Public Property XuMapping As MetricMapping
    Public Property YuMapping As MetricMapping
    Public Property ZuMapping As MetricMapping

    Public Function getMappingDictionary(MeasurementsDAL As IMeasurementsDAL) As Dictionary(Of String, MeasurementMetric)

        Dim mapDict As New Dictionary(Of String, MeasurementMetric)

        If XcvMapping.MetricId IsNot Nothing Then mapDict.Add(XcvMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(XcvMapping.MetricId)))
        If YcvMapping.MetricId IsNot Nothing Then mapDict.Add(YcvMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(YcvMapping.MetricId)))
        If ZcvMapping.MetricId IsNot Nothing Then mapDict.Add(ZcvMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(ZcvMapping.MetricId)))
        If XcfMapping.MetricId IsNot Nothing Then mapDict.Add(XcfMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(XcfMapping.MetricId)))
        If YcfMapping.MetricId IsNot Nothing Then mapDict.Add(YcfMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(YcfMapping.MetricId)))
        If ZcfMapping.MetricId IsNot Nothing Then mapDict.Add(ZcfMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(ZcfMapping.MetricId)))
        If XuMapping.MetricId IsNot Nothing Then mapDict.Add(XuMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(XuMapping.MetricId)))
        If YuMapping.MetricId IsNot Nothing Then mapDict.Add(YuMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(YuMapping.MetricId)))
        If ZuMapping.MetricId IsNot Nothing Then mapDict.Add(ZuMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(ZuMapping.MetricId)))

        Return mapDict

    End Function


End Class
Public Class UploadRedboxViewModel

    Public Property XMapping As MetricMapping
    Public Property YMapping As MetricMapping
    Public Property ZMapping As MetricMapping

    Public Function getMappingDictionary(MeasurementsDAL As IMeasurementsDAL) As Dictionary(Of String, MeasurementMetric)

        Dim mapDict As New Dictionary(Of String, MeasurementMetric)

        If XMapping.MetricId IsNot Nothing Then mapDict.Add(XMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(XMapping.MetricId)))
        If YMapping.MetricId IsNot Nothing Then mapDict.Add(YMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(YMapping.MetricId)))
        If ZMapping.MetricId IsNot Nothing Then mapDict.Add(ZMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(ZMapping.MetricId)))

        Return mapDict

    End Function



End Class
Public Class UploadSigicomVibrationViewModel

    Public Property PPVXChannelMapping As MetricMapping
    Public Property PPVYChannelMapping As MetricMapping
    Public Property PPVZChannelMapping As MetricMapping

    Public Function getMappingDictionary(MeasurementsDAL As IMeasurementsDAL) As Dictionary(Of String, MeasurementMetric)

        Dim mapDict As New Dictionary(Of String, MeasurementMetric)

        If PPVXChannelMapping.MetricId IsNot Nothing Then mapDict.Add(PPVXChannelMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(PPVXChannelMapping.MetricId)))
        If PPVYChannelMapping.MetricId IsNot Nothing Then mapDict.Add(PPVYChannelMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(PPVYChannelMapping.MetricId)))
        If PPVZChannelMapping.MetricId IsNot Nothing Then mapDict.Add(PPVZChannelMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(PPVZChannelMapping.MetricId)))

        Return mapDict

    End Function

End Class
Public Class UploadVibraViewModel

    Public Property PPVXChannelMapping As MetricMapping
    Public Property PPVYChannelMapping As MetricMapping
    Public Property PPVZChannelMapping As MetricMapping
    Public Property DominantFrequencyComponentXChannelMapping As MetricMapping
    Public Property DominantFrequencyComponentYChannelMapping As MetricMapping
    Public Property DominantFrequencyComponentZChannelMapping As MetricMapping

    Public Function getMappingDictionary(MeasurementsDAL As IMeasurementsDAL) As Dictionary(Of String, MeasurementMetric)

        Dim mapDict As New Dictionary(Of String, MeasurementMetric)

        If PPVXChannelMapping.MetricId IsNot Nothing Then mapDict.Add(PPVXChannelMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(PPVXChannelMapping.MetricId)))
        If PPVYChannelMapping.MetricId IsNot Nothing Then mapDict.Add(PPVYChannelMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(PPVYChannelMapping.MetricId)))
        If PPVZChannelMapping.MetricId IsNot Nothing Then mapDict.Add(PPVZChannelMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(PPVZChannelMapping.MetricId)))
        If DominantFrequencyComponentXChannelMapping.MetricId IsNot Nothing Then mapDict.Add(DominantFrequencyComponentXChannelMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(DominantFrequencyComponentXChannelMapping.MetricId)))
        If DominantFrequencyComponentYChannelMapping.MetricId IsNot Nothing Then mapDict.Add(DominantFrequencyComponentYChannelMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(DominantFrequencyComponentYChannelMapping.MetricId)))
        If DominantFrequencyComponentZChannelMapping.MetricId IsNot Nothing Then mapDict.Add(DominantFrequencyComponentZChannelMapping.MappingName, MeasurementsDAL.GetMeasurementMetric(CInt(DominantFrequencyComponentZChannelMapping.MetricId)))

        Return mapDict

    End Function

End Class

#End Region

#Region "General"

Public Class UploadSpreadsheetTemplateViewModel

    Public Property DurationFieldSettingValue As String
    Public Property DurationFieldSettingList As SelectList

End Class

#End Region

#End Region

#End Region

#Region "Measurement Comments"

Public Class MeasurementCommentsIndexViewModel

    Public Property MonitorLocation As MonitorLocation
    Public Property MeasurementComments As IEnumerable(Of MeasurementComment)

    ' For Chart
    Public Property Chart As Highcharts
    Public Property Metrics As IEnumerable(Of MeasurementMetric)

    ' Dates for Slider
    Public Property FirstMeasurementStartDateTime As Date
    Public Property LastMeasurementEndDateTime As Date
    Public Property intFirstMeasurementStartDate As Integer
    Public Property intLastMeasurementEndDate As Integer

    Public Property intMeasurementRangeStartDate As Integer
    Public Property intMeasurementRangeEndDate As Integer
    Public Property FirstMeasurementStartDate As Date
    Public Property LastMeasurementEndDate As Date

    Public Property MeasurementRangeStartDate As Date
    Public Property MeasurementRangeEndDate As Date

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)


End Class

Public Class CreateMeasurementCommentViewModel

    ' Main Model
    Public Property MeasurementComment As MeasurementComment

    Public Property MonitorLocationId As Integer

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Comment Type from the list.")> _
    Public Property CommentTypeId As Integer
    Public Property CommentTypeList As SelectList

    ' Dates and Times
    <DataType(DataType.Date)> _
    Public Property CommentStartDate As Date

    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property CommentStartTime As Date
    <DataType(DataType.Date)> _
    Public Property CommentEndDate As Date

    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property CommentEndTime As Date

    ' Selectable MeasurementMetrics
    Public Property AvailableMeasurementMetrics As IEnumerable(Of SelectableMeasurementMetric)
    Public Property SelectedMeasurementMetrics As IEnumerable(Of SelectableMeasurementMetric)
    Public Property PostedMeasurementMetrics As PostedMeasurementMetrics




End Class
Public Class SelectableMeasurementMetric

    Public Sub New(MeasurementMetric As MeasurementMetric)

        Id = MeasurementMetric.Id
        Name = MeasurementMetric.MetricName

    End Sub

    Public Property Id As Integer
    Public Property Name As String
    Public Property IsSelected As Boolean
    Public Property Tags As Object

End Class
Public Class PostedMeasurementMetrics

    Public Property MeasurementMetricIds As String()

End Class

Public Class UpdateMeasurementCommentsIndexViewModel

    Public Property chart As Highcharts
    Public Property MonitorLocation As MonitorLocation

    Public Property StartDate As Date
    Public Property EndDate As Date

End Class

#End Region

#Region "Measurement Comment Types"

Public Class MeasurementCommentTypeDetailsViewModel

    Public Property MeasurementCommentType As MeasurementCommentType
    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class ViewMeasurementCommentTypesViewModel

    Implements IViewObjectsViewModel

    Public Property MeasurementCommentTypes As List(Of MeasurementCommentType)

    Public Property ObjectDisplayName As String Implements IViewObjectsViewModel.ObjectDisplayName

    Public Property ObjectName As String Implements IViewObjectsViewModel.ObjectName

    Public Property SearchText As String Implements IViewObjectsViewModel.SearchText

    Public Property TableId As String Implements IViewObjectsViewModel.TableId

    Public Property UpdateTableRouteName As String Implements IViewObjectsViewModel.UpdateTableRouteName

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class
Public Class EditMeasurementCommentTypeViewModel

    ' Main Model
    Public Property MeasurementCommentType As MeasurementCommentType

    ' Multiple Select Items
    Public Property AllExcludedMeasurementViews As IEnumerable(Of MeasurementView)
    Public Property AllExcludedAssessmentCriterionGroups As IEnumerable(Of AssessmentCriterionGroup)

    ' Getters
    Public ReadOnly Property getMeasurementCommentTypeExcludedMeasurementViewIds As IEnumerable(Of Integer)
        Get
            Return MeasurementCommentType.ExcludedMeasurementViews.Select(Function(mv) mv.Id).ToList
        End Get
    End Property
    Public ReadOnly Property getMeasurementCommentTypeExcludedAssessmentCriterionGroupIds As IEnumerable(Of Integer)
        Get
            Return MeasurementCommentType.ExcludedAssessmentCriterionGroups.Select(Function(acg) acg.Id).ToList
        End Get
    End Property

End Class

#End Region

#Region "Measurement Files"

Public Class MeasurementFileDetailsViewModel

    Public Property MeasurementFile As MeasurementFile

    Public Property PageNumber As Integer
    Public Property PageSize As Integer
    Public Property NumPages As Integer

    Public Property Measurements As List(Of Measurement)
    Public Property NumMeasurements As Integer

    Public Property SortByList As SelectList
    Public Property SortBy As String

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class ViewMeasurementFilesViewModel

    Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel

    Public Property MeasurementFiles As List(Of MeasurementFile)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)
    Public Property MeasurementTypeId As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.MeasurementTypeId
    Public Property MeasurementTypeList As SelectList Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.MeasurementTypeList
    Public Property MonitorLocationId As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.MonitorLocationId
    Public Property MonitorLocationList As SelectList Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.MonitorLocationList
    Public Property ObjectDisplayName As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.ObjectDisplayName
    Public Property ObjectName As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.ObjectName
    Public Property ProjectId As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.ProjectId
    Public Property ProjectList As SelectList Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.ProjectList
    Public Property SearchText As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.SearchText
    Public Property TableId As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.TableId
    Public Property UpdateTableRouteName As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.UpdateTableRouteName
    Public Property UpdateMonitorLocationsRouteName As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.UpdateMonitorLocationsRouteName
    Public Property UpdateProjectsRouteName As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.UpdateProjectsRouteName

End Class

<MetadataType(GetType(MeasurementFileMetadata))> _
Partial Public Class MeasurementFile
End Class
Public Class MeasurementFileMetadata

    <Display(Name:="Measurement File Name")> _
    Public Property MeasurementFileName As String

    <Display(Name:="Monitor")> _
    Public Property Monitor As Monitor

    Public Property MonitorId As Int32

    <Display(Name:="Upload Date Time")> _
    Public Property UploadDateTime As DateTime

    <Display(Name:="Upload Success")> _
    Public Property UploadSuccess As Boolean

    Public Property ContactId As Int32

    Public Property MonitorLocationId As Int32

    <Display(Name:="Contact")> _
    Public Property Contact As Contact

    <Display(Name:="Monitor Location")> _
    Public Property MonitorLocation As MonitorLocation

End Class

#End Region

#Region "Measurement Metrics"

Public Class MeasurementMetricDetailsViewModel

    Public Property MeasurementMetric As MeasurementMetric
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)
    Public Property Tabs As IEnumerable(Of TabViewModel)

End Class

Public Class ViewMeasurementMetricsViewModel

    Implements IViewObjectsByMeasurementTypeViewModel

    Public Property MeasurementMetrics As List(Of MeasurementMetric)
    Public Property ObjectDisplayName As String Implements IViewObjectsByMeasurementTypeViewModel.ObjectDisplayName
    Public Property ObjectName As String Implements IViewObjectsByMeasurementTypeViewModel.ObjectName
    Public Property SearchText As String Implements IViewObjectsByMeasurementTypeViewModel.SearchText
    Public Property TableId As String Implements IViewObjectsByMeasurementTypeViewModel.TableId
    Public Property UpdateTableRouteName As String Implements IViewObjectsByMeasurementTypeViewModel.UpdateTableRouteName
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)
    Public Property MeasurementTypeId As String Implements IViewObjectsByMeasurementTypeViewModel.MeasurementTypeId
    Public Property MeasurementTypeList As SelectList Implements IViewObjectsByMeasurementTypeViewModel.MeasurementTypeList

End Class

Public Class CreateMeasurementMetricViewModel

    ' Main Model
    Public Property MeasurementMetric As MeasurementMetric

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Measurement Type from the list.")> _
    Public Property MeasurementTypeId As Integer
    Public Property MeasurementTypeList As SelectList

End Class

Public Class EditMeasurementMetricViewModel

    ' Main Model
    Public Property MeasurementMetric As MeasurementMetric

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Measurement Type from the list.")> _
    Public Property MeasurementTypeId As Integer
    Public Property MeasurementTypeList As SelectList

End Class

#End Region

#Region "Measurement Views"

Public Class MeasurementViewDetailsViewModel

    Public Property MeasurementView As MeasurementView
    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class ViewMeasurementViewsViewModel

    Implements IViewObjectsByMeasurementTypeViewModel

    Public Property MeasurementViews As List(Of MeasurementView)


    Public Property ObjectDisplayName As String Implements IViewObjectsByMeasurementTypeViewModel.ObjectDisplayName

    Public Property ObjectName As String Implements IViewObjectsByMeasurementTypeViewModel.ObjectName

    Public Property SearchText As String Implements IViewObjectsByMeasurementTypeViewModel.SearchText

    Public Property TableId As String Implements IViewObjectsByMeasurementTypeViewModel.TableId

    Public Property UpdateTableRouteName As String Implements IViewObjectsByMeasurementTypeViewModel.UpdateTableRouteName

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)
    Public Property MeasurementTypeId As String Implements IViewObjectsByMeasurementTypeViewModel.MeasurementTypeId
    Public Property MeasurementTypeList As SelectList Implements IViewObjectsByMeasurementTypeViewModel.MeasurementTypeList

End Class

Public Class CreateMeasurementViewViewModel

    ' Main Model
    Public Property MeasurementView As MeasurementView

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Measurement Type from the list.")> _
    Public Property MeasurementTypeId As Integer
    Public Property MeasurementTypeList As SelectList
    <Required(ErrorMessage:="Please select the Measurement View Table Type from the list.")> _
    Public Property MeasurementViewTableTypeId As Integer
    Public Property MeasurementViewTableTypeList As SelectList

End Class
Public Class EditMeasurementViewViewModel

    ' Main Model
    Public Property MeasurementView As MeasurementView

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Measurement Type from the list.")> _
    Public Property MeasurementTypeId As Integer
    Public Property MeasurementTypeList As SelectList
    <Required(ErrorMessage:="Please select the Measurement View Table Type from the list.")> _
    Public Property MeasurementViewTableTypeId As Integer
    Public Property MeasurementViewTableTypeList As SelectList
    ' Multiple Select Items
    Public Property AllUserAccessLevels As IEnumerable(Of UserAccessLevel)
    Public Property AllCommentTypes As IEnumerable(Of MeasurementCommentType)
    Public Property AllProjects As IEnumerable(Of Project)

    ' Getters
    'Public ReadOnly Property getMeasurementViewAllowedUserAccessLevelIds As IEnumerable(Of Integer)
    '    Get
    '        Return MeasurementView.AllowedUserAccessLevels.Select(Function(c) c.Id).ToList
    '    End Get
    'End Property
    Public ReadOnly Property getMeasurementViewExcludingMeasurementCommentTypeIds As IEnumerable(Of Integer)
        Get
            Return MeasurementView.ExcludingMeasurementCommentTypes.Select(Function(c) c.Id).ToList
        End Get
    End Property
    Public ReadOnly Property getMeasurementViewProjectIds As IEnumerable(Of Integer)
        Get
            Return MeasurementView.Projects.Select(Function(c) c.Id).ToList
        End Get
    End Property

End Class

Public Class SelectableMeasurementView

    Public Property MeasurementView As MeasurementView
    Public Property IsSelected As Boolean

End Class

#End Region

#Region "Measurement View Groups"

Public Class CreateMeasurementViewGroupViewModel

    ' Main Model
    Public Property MeasurementViewGroup As MeasurementViewGroup

    ' Parent Items
    Public Property MeasurementView As MeasurementView

End Class

Public Class EditMeasurementViewGroupViewModel

    ' Main Model
    Public Property MeasurementViewGroup As MeasurementViewGroup

    ' Parent Items
    Public Property MeasurementView As MeasurementView

End Class

#End Region

#Region "MeasurementViewSequenceSettings"

Public Class CreateMeasurementViewSequenceSettingViewModel

    ' Main Model
    Public Property MeasurementViewSequenceSetting As MeasurementViewSequenceSetting

    ' Parent Items
    Public Property MeasurementViewGroup As MeasurementViewGroup
    Public Property MeasurementView As MeasurementView


    ' Single Select Items
    <Required(ErrorMessage:="Please select the Calculation Filter from the list.")> _
    Public Property CalculationFilterId As Integer
    Public Property CalculationFilterList As SelectList
    <Required(ErrorMessage:="Please select the Day View Series Type from the list.")> _
    Public Property DayViewSeriesTypeId As Integer
    Public Property DayViewSeriesTypeList As SelectList
    <Required(ErrorMessage:="Please select the Week View Series Type from the list.")> _
    Public Property WeekViewSeriesTypeId As Integer
    Public Property WeekViewSeriesTypeList As SelectList
    <Required(ErrorMessage:="Please select the Month View Series Type from the list.")> _
    Public Property MonthViewSeriesTypeId As Integer
    Public Property MonthViewSeriesTypeList As SelectList
    <Required(ErrorMessage:="Please select the Day View Series Dash Style from the list.")> _
    Public Property DayViewSeriesDashStyleId As Integer
    Public Property DayViewSeriesDashStyleList As SelectList
    <Required(ErrorMessage:="Please select the Week View Series Dash Style from the list.")> _
    Public Property WeekViewSeriesDashStyleId As Integer
    Public Property WeekViewSeriesDashStyleList As SelectList
    <Required(ErrorMessage:="Please select the Month View Series Dash Style from the list.")> _
    Public Property MonthViewSeriesDashStyleId As Integer
    Public Property MonthViewSeriesDashStyleList As SelectList

End Class

Public Class EditMeasurementViewSequenceSettingViewModel

    ' Main Model
    Public Property MeasurementViewSequenceSetting As MeasurementViewSequenceSetting

    ' Parent Items
    Public Property MeasurementViewGroup As MeasurementViewGroup
    Public Property MeasurementView As MeasurementView

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Calculation Filter from the list.")> _
    Public Property CalculationFilterId As Integer
    Public Property CalculationFilterList As SelectList
    <Required(ErrorMessage:="Please select the Day View Series Type from the list.")> _
    Public Property DayViewSeriesTypeId As Integer
    Public Property DayViewSeriesTypeList As SelectList
    <Required(ErrorMessage:="Please select the Week View Series Type from the list.")> _
    Public Property WeekViewSeriesTypeId As Integer
    Public Property WeekViewSeriesTypeList As SelectList
    <Required(ErrorMessage:="Please select the Month View Series Type from the list.")> _
    Public Property MonthViewSeriesTypeId As Integer
    Public Property MonthViewSeriesTypeList As SelectList
    <Required(ErrorMessage:="Please select the Day View Series Dash Style from the list.")> _
    Public Property DayViewSeriesDashStyleId As Integer
    Public Property DayViewSeriesDashStyleList As SelectList
    <Required(ErrorMessage:="Please select the Week View Series Dash Style from the list.")> _
    Public Property WeekViewSeriesDashStyleId As Integer
    Public Property WeekViewSeriesDashStyleList As SelectList
    <Required(ErrorMessage:="Please select the Month View Series Dash Style from the list.")> _
    Public Property MonthViewSeriesDashStyleId As Integer
    Public Property MonthViewSeriesDashStyleList As SelectList

End Class



#End Region

#Region "Monitors"

Public Class MonitorDetailsViewModel

    Public Property Monitor As Monitor
    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class ViewMonitorsViewModel

    Implements IViewObjectsByMeasurementTypeViewModel

    Public Property Monitors As List(Of Monitor)
    Public Property ObjectDisplayName As String Implements IViewObjectsByMeasurementTypeViewModel.ObjectDisplayName
    Public Property ObjectName As String Implements IViewObjectsByMeasurementTypeViewModel.ObjectName
    Public Property SearchText As String Implements IViewObjectsByMeasurementTypeViewModel.SearchText
    Public Property TableId As String Implements IViewObjectsByMeasurementTypeViewModel.TableId
    Public Property UpdateTableRouteName As String Implements IViewObjectsByMeasurementTypeViewModel.UpdateTableRouteName
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)
    Public Property MeasurementTypeId As String Implements IViewObjectsByMeasurementTypeViewModel.MeasurementTypeId
    Public Property MeasurementTypeList As SelectList Implements IViewObjectsByMeasurementTypeViewModel.MeasurementTypeList

End Class

Public Class CreateMonitorViewModel

    ' Main Model
    Public Property Monitor As Monitor

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Measurement Type from the list.")> _
    Public Property MeasurementTypeId As Integer
    Public Property MeasurementTypeList As SelectList
    <Required(ErrorMessage:="Please select the Owner Organisation from the list.")> _
    Public Property OwnerOrganisationId As Integer
    Public Property OwnerOrganisationList As SelectList



End Class

Public Class EditMonitorViewModel

    ' Main Model
    Public Property Monitor As Monitor
    Public Property CurrentStatus As MonitorStatus

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Measurement Type from the list.")> _
    Public Property MeasurementTypeId As Integer
    Public Property MeasurementTypeList As SelectList
    <Required(ErrorMessage:="Please select the Owner Organisation from the list.")> _
    Public Property OwnerOrganisationId As Integer
    Public Property OwnerOrganisationList As SelectList

End Class

#End Region

#Region "Monitor Deployment Records"

Public Class ViewMonitorDeploymentRecordsViewModel

    Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel


    Public Property MonitorDeploymentRecords As List(Of MonitorDeploymentRecord)
    Public Property SearchText As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.SearchText
    Public Property TableId As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.TableId
    Public Property UpdateTableRouteName As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.UpdateTableRouteName
    Public Property ObjectName As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.ObjectName
    Public Property ObjectDisplayName As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.ObjectDisplayName
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)
    Public Property MeasurementTypeId As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.MeasurementTypeId
    Public Property MeasurementTypeList As SelectList Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.MeasurementTypeList
    Public Property MonitorLocationId As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.MonitorLocationId
    Public Property MonitorLocationList As SelectList Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.MonitorLocationList
    Public Property ProjectId As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.ProjectId
    Public Property ProjectList As SelectList Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.ProjectList
    Public Property UpdateMonitorLocationsRouteName As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.UpdateMonitorLocationsRouteName
    Public Property UpdateProjectsRouteName As String Implements IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel.UpdateProjectsRouteName

End Class

Public Class CreateMonitorDeploymentRecordViewModel

    ' Main Model
    Public Property MonitorDeploymentRecord As MonitorDeploymentRecord
    Public Property Monitor As Monitor
    Public Property MonitorLocation As MonitorLocation
    Public Property MonitorSettings As MonitorSettings

    ' Hidden Properties
    Public Property MonitorId As Integer
    Public Property MonitorList As SelectList

    ' Optional Items
    Public Property NoiseSetting As NoiseSetting
    Public Property VibrationSetting As VibrationSetting
    Public Property AirQualitySetting As AirQualitySetting

    ' Single Select Items
    <Required(ErrorMessage:="Please select a Project.")> _
    Public Property ProjectId As Integer
    Public Property ProjectList As SelectList
    <Required(ErrorMessage:="Please select a Monitor Location to Deploy to.")> _
    Public Property MonitorLocationId As Integer
    Public Property MonitorLocationList As SelectList

    ' Special Items
    <DataType(DataType.Time)> _
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    Public Property MeasurementPeriod As Date
    Public Property ValidationErrors As New List(Of String)

End Class

Public Class CreateNoiseMonitorDeploymentRecordViewModel



End Class

Public Class MonitorDeploymentRecordDetailsViewModel

    Public Property MonitorDeploymentRecord As MonitorDeploymentRecord
    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

#End Region

#Region "Monitor Locations"

Public Class MonitorLocationDetailsViewModel

    Public Property MonitorLocation As MonitorLocation
    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class ViewMonitorLocationsViewModel

    Implements IViewObjectsByMeasurementTypeViewModel

    Public Property MonitorLocations As List(Of MonitorLocation)
    Public Property ObjectDisplayName As String Implements IViewObjectsByMeasurementTypeViewModel.ObjectDisplayName
    Public Property ObjectName As String Implements IViewObjectsByMeasurementTypeViewModel.ObjectName
    Public Property SearchText As String Implements IViewObjectsByMeasurementTypeViewModel.SearchText
    Public Property TableId As String Implements IViewObjectsByMeasurementTypeViewModel.TableId
    Public Property UpdateTableRouteName As String Implements IViewObjectsByMeasurementTypeViewModel.UpdateTableRouteName
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)
    Public Property MeasurementTypeId As String Implements IViewObjectsByMeasurementTypeViewModel.MeasurementTypeId
    Public Property MeasurementTypeList As SelectList Implements IViewObjectsByMeasurementTypeViewModel.MeasurementTypeList

End Class

Public Class CreateMonitorLocationViewModel

    ' Main Model
    Public Property MonitorLocation As MonitorLocation
    Public Property Project As Project

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Measurement Type from the list.")> _
    Public Property MeasurementTypeId As Integer
    Public Property MeasurementTypeList As SelectList

End Class

Public Class EditMonitorLocationViewModel

    ' Main Model
    Public Property MonitorLocation As MonitorLocation
    Public Property Project As Project

End Class

Public Class SelectMonitorLocationViewModel

    Public Property MonitorLocations As IEnumerable(Of MonitorLocation)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

#End Region

#Region "Organisations"

Public Class ViewOrganisationsViewModel

    Implements IViewObjectsViewModel, IAlphabeticalListViewModel


    Public Property Organisations As List(Of Organisation)

    Public Property ObjectDisplayName As String Implements IViewObjectsViewModel.ObjectDisplayName

    Public Property ObjectName As String Implements IViewObjectsViewModel.ObjectName

    Public Property SearchText As String Implements IViewObjectsViewModel.SearchText

    Public Property TableId As String Implements IViewObjectsViewModel.TableId, IAlphabeticalListViewModel.TableId

    Public Property UpdateTableRouteName As String Implements IViewObjectsViewModel.UpdateTableRouteName

    Public Property Names As List(Of String) Implements IAlphabeticalListViewModel.Names

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class CreateOrganisationViewModel

    Public Property Organisation As Organisation
    <Required(ErrorMessage:="Please select the Type of Organisation from the list.")> _
    Public Property OrganisationTypeId As Integer
    Public Property OrganisationTypeList As SelectList

End Class

Public Class EditOrganisationViewModel

    ' Main Model
    Public Property Organisation As Organisation

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Organisation Type from the list.")> _
    Public Property OrganisationTypeId As Integer
    Public Property OrganisationTypeList As SelectList

    ' Multiple Select Items
    Public Property AllProjects As IEnumerable(Of Project)

    Public ReadOnly Property getOrganisationProjectIds As IEnumerable(Of Integer)
        Get
            Return Organisation.Projects.Select(Function(p) p.Id).ToList
        End Get
    End Property

End Class

Public Class OrganisationDetailsViewModel

    Public Property Organisation As Organisation
    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

    'Public Property ShowAuthoredDocumentLink As Boolean = True
    'Public Property ShowAuthoredDocumentTypeLink As Boolean = True
    'Public Property ShowOrganisationTypeLink As Boolean = True
    'Public Property ShowContactLinks As Boolean = True
    'Public Property ShowOwnedMonitorLink As Boolean = True
    'Public Property ShowProjectLinks As Boolean = True
    'Public Property ShowProjectClientOrganisationLink As Boolean = True

End Class

#End Region

#Region "Organisation Types"

Public Class ViewOrganisationTypesViewModel

    Implements IViewObjectsViewModel

    Public Property OrganisationTypes As List(Of OrganisationType)

    Public Property ObjectDisplayName As String Implements IViewObjectsViewModel.ObjectDisplayName

    Public Property ObjectName As String Implements IViewObjectsViewModel.ObjectName

    Public Property SearchText As String Implements IViewObjectsViewModel.SearchText

    Public Property TableId As String Implements IViewObjectsViewModel.TableId

    Public Property UpdateTableRouteName As String Implements IViewObjectsViewModel.UpdateTableRouteName

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class OrganisationTypeDetailsViewModel

    Public Property OrganisationType As OrganisationType
    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

#End Region

#Region "Projects"

Public Class ViewProjectsViewModel

    Implements IViewObjectsViewModel

    Public Property Projects As IEnumerable(Of Project)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)
    Public Property ObjectDisplayName As String Implements IViewObjectsViewModel.ObjectDisplayName
    Public Property ObjectName As String Implements IViewObjectsViewModel.ObjectName
    Public Property SearchText As String Implements IViewObjectsViewModel.SearchText
    Public Property TableId As String Implements IViewObjectsViewModel.TableId
    Public Property UpdateTableRouteName As String Implements IViewObjectsViewModel.UpdateTableRouteName

End Class

Public Class ProjectDetailsViewModel

    Public Property Project As Project
    Public Property MonitorLocations As List(Of MonitorLocation)
    Public Property Organisations As List(Of Organisation)
    Public Property Contacts As List(Of Contact)
    Public Property MeasurementViews As List(Of MeasurementView)
    Public Property AssessmentCriteria As List(Of AssessmentCriterionGroup)
    Public Property StandardWorkingHours As StandardWeeklyWorkingHours
    Public Property Variations As List(Of VariedWeeklyWorkingHours)

    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)


End Class

Public Class CreateProjectViewModel

    ' Main Model
    Public Property Project As Project
    ' Single Select Items
    <Required(ErrorMessage:="Please select the Client Organisation from the list.")> _
    Public Property ClientOrganisationId As Integer
    Public Property ClientOrganisationList As SelectList
    <Required(ErrorMessage:="Please select a Country from the list.")> _
    Public Property CountryId As Integer
    Public Property CountryList As SelectList
    ' Custom Items
    Public Property WorkingWeekViewModel As WorkingWeekViewModel

End Class

Public Class EditProjectViewModel

    ' Main Model
    Public Property Project As Project

    ' Single Select Items
    Public Property ClientOrganisationId As Integer
    Public Property ClientOrganisationList As SelectList
    Public Property CountryId As Integer
    Public Property CountryList As SelectList

    ' Multiple Select Items
    Public Property AllAssessmentCriteria As IEnumerable(Of AssessmentCriterionGroup)
    Public Property AllContacts As IEnumerable(Of Contact)
    Public Property AllMeasurementViews As IEnumerable(Of MeasurementView)
    Public Property AllMonitorLocations As IEnumerable(Of MonitorLocation)
    Public Property AllOrganisations As IEnumerable(Of Organisation)

    ' Special Items
    Public Property WorkingWeekViewModel As WorkingWeekViewModel

    ' Getters
    Public ReadOnly Property getProjectAssessmentCriteriaIds As IEnumerable(Of Integer)
        Get
            Return Project.AssessmentCriteria.Select(Function(ac) ac.Id).ToList
        End Get
    End Property
    Public ReadOnly Property getProjectContactIds As IEnumerable(Of Integer)
        Get
            Return Project.Contacts.Select(Function(c) c.Id).ToList
        End Get
    End Property
    Public ReadOnly Property getProjectMeasurementViewIds As IEnumerable(Of Integer)
        Get
            Return Project.MeasurementViews.Select(Function(mv) mv.Id).ToList
        End Get
    End Property
    Public ReadOnly Property getProjectMonitorLocationIds As IEnumerable(Of Integer)
        Get
            Return Project.MonitorLocations.Select(Function(ml) ml.Id).ToList
        End Get
    End Property
    Public ReadOnly Property getProjectOrganisationIds As IEnumerable(Of Integer)
        Get
            Return Project.Organisations.Select(Function(o) o.Id).ToList
        End Get
    End Property

End Class

Public Class ProjectMonitorLocationsViewModel

    Property Project As Project
    Property MonitorLocations As IEnumerable(Of MonitorLocation)

End Class


#End Region

#Region "Public Holidays"

Public Class CreatePublicHolidayViewModel

    Public Property PublicHoliday As PublicHoliday
    Public Property CountryId As Integer
    Public Property CountryName As String

End Class

#End Region

#Region "Standard Weekly Working Hours"

Public Class EditStandardWeeklyWorkingHoursViewModel

    ' Main Model
    Public Property WorkingWeekViewModel As WorkingWeekViewModel
    Public Property StandardWeeklyWorkingHours As StandardWeeklyWorkingHours

    ' Parent Items
    Public Property ProjectId As Integer

    ' Multiple Select Items
    Public Property AllMeasurementViews As IEnumerable(Of MeasurementView)

    ' Getters
    Public ReadOnly Property getStandardWeeklyWorkingHoursMeasurementViewIds As IEnumerable(Of Integer)
        Get
            Return StandardWeeklyWorkingHours.AvailableMeasurementViews.Select(Function(mv) mv.Id).ToList
        End Get
    End Property

End Class

#End Region

#Region "Users"

Public Class UserDetailsViewModel

    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class
Public Class ViewUsersViewModel

    Implements IViewObjectsViewModel

    Public Property Users As List(Of User)

    Public Property ObjectDisplayName As String Implements IViewObjectsViewModel.ObjectDisplayName

    Public Property ObjectName As String Implements IViewObjectsViewModel.ObjectName

    Public Property SearchText As String Implements IViewObjectsViewModel.SearchText

    Public Property TableId As String Implements IViewObjectsViewModel.TableId

    Public Property UpdateTableRouteName As String Implements IViewObjectsViewModel.UpdateTableRouteName

    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class
Public Class EditUserViewModel

    ' Main Model
    Public Property User As User

    ' Single Select Items
    <Required(ErrorMessage:="Please select the associated Contact from the list.")> _
    Public Property ContactId As Integer
    Public Property ContactList As SelectList

    <Required(ErrorMessage:="Please select the User's Access Level from the list.")> _
    Public Property UserAccessLevelId As Integer
    Public Property UserAccessLevelList As SelectList

End Class
Public Class CreateUserViewModel

    ' Main Model
    Public Property User As User

    ' Single Select Items
    <Required(ErrorMessage:="Please select the associated Contact from the list.")> _
    Public Property ContactId As Integer
    Public Property ContactList As SelectList

    <Required(ErrorMessage:="Please select the User's Access Level from the list.")> _
    Public Property UserAccessLevelId As Integer
    Public Property UserAccessLevelList As SelectList

End Class

#End Region

#Region "User Access Levels"

Public Class ViewUserAccessLevelsViewModel

    Implements IViewObjectsViewModel

    Public Property UserAccessLevels As List(Of UserAccessLevel)
    Public Property ObjectDisplayName As String Implements IViewObjectsViewModel.ObjectDisplayName
    Public Property ObjectName As String Implements IViewObjectsViewModel.ObjectName
    Public Property SearchText As String Implements IViewObjectsViewModel.SearchText
    Public Property TableId As String Implements IViewObjectsViewModel.TableId
    Public Property UpdateTableRouteName As String Implements IViewObjectsViewModel.UpdateTableRouteName
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)

End Class

Public Class UserAccessLevelDetailsViewModel

    Public Property UserAccessLevel As UserAccessLevel

    Public Property Tabs As IEnumerable(Of TabViewModel)
    Public Property NavigationButtons As IEnumerable(Of NavigationButtonViewModel)


End Class

Public Class EditUserAccessLevelViewModel

    ' Main Model
    Public Property UserAccessLevel As UserAccessLevel

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Project Permission from the list.")> _
    Public Property ProjectPermissionId As Integer
    Public Property ProjectPermissionList As SelectList

End Class

Public Class CreateUserAccessLevelViewModel

    ' Main Model
    Public Property UserAccessLevel As UserAccessLevel

    ' Single Select Items
    <Required(ErrorMessage:="Please select the Project Permission from the list.")> _
    Public Property ProjectPermissionId As Integer
    Public Property ProjectPermissionList As SelectList

End Class


#End Region

#Region "Varied Weekly Working Hours"

Public Class CreateVariedWeeklyWorkingHoursViewModel

#Region "Properties"

    Public Property VariedWeeklyWorkingHours As VariedWeeklyWorkingHours
    'Public Property AllMeasurementViews As IList(Of SelectableMeasurementView)

    Public Property WorkingWeekViewModel As WorkingWeekViewModel


#End Region

End Class

Public Class EditVariedWeeklyWorkingHoursViewModel

    ' Main Model
    Public Property VariedWeeklyWorkingHours As VariedWeeklyWorkingHours

    ' Multiple Select Items
    'Public Property AllMeasurementViews As IList(Of SelectableMeasurementView)

    Public Property WorkingWeekViewModel As WorkingWeekViewModel

    ' Getters
    Public ReadOnly Property getVariedWeeklyWorkingHoursAvailableMeasurementViewIds As IEnumerable(Of Integer)
        Get
            Return VariedWeeklyWorkingHours.AvailableMeasurementViews.Select(Function(amv) amv.Id).ToList
        End Get
    End Property

End Class

#End Region

Public Class WorkingWeekViewModel

#Region "Constructors"

    Public Sub New()

        WorkOnMondays = False : StartTimeMondays = Date.FromOADate(8 / 24) : EndTimeMondays = Date.FromOADate(18 / 24)
        WorkOnTuesdays = False : StartTimeTuesdays = Date.FromOADate(8 / 24) : EndTimeTuesdays = Date.FromOADate(18 / 24)
        WorkOnWednesdays = False : StartTimeWednesdays = Date.FromOADate(8 / 24) : EndTimeWednesdays = Date.FromOADate(18 / 24)
        WorkOnThursdays = False : StartTimeThursdays = Date.FromOADate(8 / 24) : EndTimeThursdays = Date.FromOADate(18 / 24)
        WorkOnFridays = False : StartTimeFridays = Date.FromOADate(8 / 24) : EndTimeFridays = Date.FromOADate(18 / 24)
        WorkOnSaturdays = False : StartTimeSaturdays = Date.FromOADate(8 / 24) : EndTimeSaturdays = Date.FromOADate(18 / 24)
        WorkOnSundays = False : StartTimeSundays = Date.FromOADate(8 / 24) : EndTimeSundays = Date.FromOADate(18 / 24)
        WorkOnPublicHolidays = False : StartTimePublicHolidays = Date.FromOADate(8 / 24) : EndTimePublicHolidays = Date.FromOADate(18 / 24)

    End Sub
    Public Sub New(StandardWeeklyWorkingHours As StandardWeeklyWorkingHours)

        For Each sdwh As StandardDailyWorkingHours In StandardWeeklyWorkingHours.StandardDailyWorkingHours
            If sdwh.DayOfWeek.DayName = "Monday" Then
                WorkOnMondays = True
                StartTimeMondays = sdwh.StartTime
                EndTimeMondays = sdwh.EndTime
            End If
            If sdwh.DayOfWeek.DayName = "Tuesday" Then
                WorkOnTuesdays = True
                StartTimeTuesdays = sdwh.StartTime
                EndTimeTuesdays = sdwh.EndTime
            End If
            If sdwh.DayOfWeek.DayName = "Wednesday" Then
                WorkOnWednesdays = True
                StartTimeWednesdays = sdwh.StartTime
                EndTimeWednesdays = sdwh.EndTime
            End If
            If sdwh.DayOfWeek.DayName = "Thursday" Then
                WorkOnThursdays = True
                StartTimeThursdays = sdwh.StartTime
                EndTimeThursdays = sdwh.EndTime
            End If
            If sdwh.DayOfWeek.DayName = "Friday" Then
                WorkOnFridays = True
                StartTimeFridays = sdwh.StartTime
                EndTimeFridays = sdwh.EndTime
            End If
            If sdwh.DayOfWeek.DayName = "Saturday" Then
                WorkOnSaturdays = True
                StartTimeSaturdays = sdwh.StartTime
                EndTimeSaturdays = sdwh.EndTime
            End If
            If sdwh.DayOfWeek.DayName = "Sunday" Then
                WorkOnSundays = True
                StartTimeSundays = sdwh.StartTime
                EndTimeSundays = sdwh.EndTime
            End If
            If sdwh.DayOfWeek.DayName = "Public Holiday" Then
                WorkOnPublicHolidays = True
                StartTimePublicHolidays = sdwh.StartTime
                EndTimePublicHolidays = sdwh.EndTime
            End If
        Next

    End Sub
    Public Sub New(VariedWeeklyWorkingHours As VariedWeeklyWorkingHours)

        For Each vdwh As VariedDailyWorkingHours In VariedWeeklyWorkingHours.VariedDailyWorkingHours
            If vdwh.DayOfWeek.DayName = "Monday" Then
                WorkOnMondays = True
                StartTimeMondays = vdwh.StartTime
                EndTimeMondays = vdwh.EndTime
            End If
            If vdwh.DayOfWeek.DayName = "Tuesday" Then
                WorkOnTuesdays = True
                StartTimeTuesdays = vdwh.StartTime
                EndTimeTuesdays = vdwh.EndTime
            End If
            If vdwh.DayOfWeek.DayName = "Wednesday" Then
                WorkOnWednesdays = True
                StartTimeWednesdays = vdwh.StartTime
                EndTimeWednesdays = vdwh.EndTime
            End If
            If vdwh.DayOfWeek.DayName = "Thursday" Then
                WorkOnThursdays = True
                StartTimeThursdays = vdwh.StartTime
                EndTimeThursdays = vdwh.EndTime
            End If
            If vdwh.DayOfWeek.DayName = "Friday" Then
                WorkOnFridays = True
                StartTimeFridays = vdwh.StartTime
                EndTimeFridays = vdwh.EndTime
            End If
            If vdwh.DayOfWeek.DayName = "Saturday" Then
                WorkOnSaturdays = True
                StartTimeSaturdays = vdwh.StartTime
                EndTimeSaturdays = vdwh.EndTime
            End If
            If vdwh.DayOfWeek.DayName = "Sunday" Then
                WorkOnSundays = True
                StartTimeSundays = vdwh.StartTime
                EndTimeSundays = vdwh.EndTime
            End If
            If vdwh.DayOfWeek.DayName = "Public Holiday" Then
                WorkOnPublicHolidays = True
                StartTimePublicHolidays = vdwh.StartTime
                EndTimePublicHolidays = vdwh.EndTime
            End If
        Next

    End Sub

#End Region

#Region "Properties"

    Public Property WorkOnMondays As Boolean
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property StartTimeMondays As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property EndTimeMondays As Date

    Public Property WorkOnTuesdays As Boolean
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property StartTimeTuesdays As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property EndTimeTuesdays As Date

    Public Property WorkOnWednesdays As Boolean
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property StartTimeWednesdays As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property EndTimeWednesdays As Date

    Public Property WorkOnThursdays As Boolean
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property StartTimeThursdays As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property EndTimeThursdays As Date

    Public Property WorkOnFridays As Boolean
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property StartTimeFridays As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property EndTimeFridays As Date

    Public Property WorkOnSaturdays As Boolean
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property StartTimeSaturdays As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property EndTimeSaturdays As Date

    Public Property WorkOnSundays As Boolean
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property StartTimeSundays As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property EndTimeSundays As Date

    Public Property WorkOnPublicHolidays As Boolean
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property StartTimePublicHolidays As Date
    <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
    Public Property EndTimePublicHolidays As Date

#End Region

#Region "Methods"

    Public Function GetStandardWeeklyWorkingHours() As StandardWeeklyWorkingHours

        Dim swh As New StandardWeeklyWorkingHours

        If WorkOnMondays = True Then swh.StandardDailyWorkingHours.Add(New StandardDailyWorkingHours With {.DayOfWeekId = 1, .StartTime = StartTimeMondays, .EndTime = EndTimeMondays})
        If WorkOnTuesdays = True Then swh.StandardDailyWorkingHours.Add(New StandardDailyWorkingHours With {.DayOfWeekId = 2, .StartTime = StartTimeTuesdays, .EndTime = EndTimeTuesdays})
        If WorkOnWednesdays = True Then swh.StandardDailyWorkingHours.Add(New StandardDailyWorkingHours With {.DayOfWeekId = 3, .StartTime = StartTimeWednesdays, .EndTime = EndTimeWednesdays})
        If WorkOnThursdays = True Then swh.StandardDailyWorkingHours.Add(New StandardDailyWorkingHours With {.DayOfWeekId = 4, .StartTime = StartTimeThursdays, .EndTime = EndTimeThursdays})
        If WorkOnFridays = True Then swh.StandardDailyWorkingHours.Add(New StandardDailyWorkingHours With {.DayOfWeekId = 5, .StartTime = StartTimeFridays, .EndTime = EndTimeFridays})
        If WorkOnSaturdays = True Then swh.StandardDailyWorkingHours.Add(New StandardDailyWorkingHours With {.DayOfWeekId = 6, .StartTime = StartTimeSaturdays, .EndTime = EndTimeSaturdays})
        If WorkOnSundays = True Then swh.StandardDailyWorkingHours.Add(New StandardDailyWorkingHours With {.DayOfWeekId = 7, .StartTime = StartTimeSundays, .EndTime = EndTimeSundays})
        If WorkOnPublicHolidays = True Then swh.StandardDailyWorkingHours.Add(New StandardDailyWorkingHours With {.DayOfWeekId = 8, .StartTime = StartTimePublicHolidays, .EndTime = EndTimePublicHolidays})

        Return swh

    End Function

    Public Function GetVariedWeeklyWorkingHours() As VariedWeeklyWorkingHours

        Dim vwh As New VariedWeeklyWorkingHours

        If WorkOnMondays = True Then vwh.VariedDailyWorkingHours.Add(New VariedDailyWorkingHours With {.DayOfWeekId = 1, .StartTime = StartTimeMondays, .EndTime = EndTimeMondays})
        If WorkOnTuesdays = True Then vwh.VariedDailyWorkingHours.Add(New VariedDailyWorkingHours With {.DayOfWeekId = 2, .StartTime = StartTimeTuesdays, .EndTime = EndTimeTuesdays})
        If WorkOnWednesdays = True Then vwh.VariedDailyWorkingHours.Add(New VariedDailyWorkingHours With {.DayOfWeekId = 3, .StartTime = StartTimeWednesdays, .EndTime = EndTimeWednesdays})
        If WorkOnThursdays = True Then vwh.VariedDailyWorkingHours.Add(New VariedDailyWorkingHours With {.DayOfWeekId = 4, .StartTime = StartTimeThursdays, .EndTime = EndTimeThursdays})
        If WorkOnFridays = True Then vwh.VariedDailyWorkingHours.Add(New VariedDailyWorkingHours With {.DayOfWeekId = 5, .StartTime = StartTimeFridays, .EndTime = EndTimeFridays})
        If WorkOnSaturdays = True Then vwh.VariedDailyWorkingHours.Add(New VariedDailyWorkingHours With {.DayOfWeekId = 6, .StartTime = StartTimeSaturdays, .EndTime = EndTimeSaturdays})
        If WorkOnSundays = True Then vwh.VariedDailyWorkingHours.Add(New VariedDailyWorkingHours With {.DayOfWeekId = 7, .StartTime = StartTimeSundays, .EndTime = EndTimeSundays})
        If WorkOnPublicHolidays = True Then vwh.VariedDailyWorkingHours.Add(New VariedDailyWorkingHours With {.DayOfWeekId = 8, .StartTime = StartTimePublicHolidays, .EndTime = EndTimePublicHolidays})

        Return vwh

    End Function

    Public Function GetVariedDailyWorkingHours() As IEnumerable(Of VariedDailyWorkingHours)

        Dim vdwhList As New List(Of VariedDailyWorkingHours)

        For Each vdwh In GetVariedWeeklyWorkingHours.VariedDailyWorkingHours
            vdwhList.Add(vdwh)
        Next

        Return vdwhList

    End Function

#End Region

#Region "Metadata"

    <MetadataType(GetType(WorkingWeekViewModelMetadata))> _
    Partial Public Class WorkingWeekViewModel
    End Class
    Public Class WorkingWeekViewModelMetadata

        <Required> _
    <Display(Name:="Work On Mondays")> _
        Public Property WorkOnMondays As Boolean

        <Display(Name:="Start Time Mondays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
    <DataType(DataType.Time)> _
        Public Property StartTimeMondays As Date


        <Display(Name:="End Time Mondays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property EndTimeMondays As Date

        <Required> _
    <Display(Name:="Work On Tuesdays")> _
        Public Property WorkOnTuesdays As Boolean


        <Display(Name:="Start Time Tuesdays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property StartTimeTuesdays As Date


        <Display(Name:="End Time Tuesdays")> _
        <DataType(DataType.Time)> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        Public Property EndTimeTuesdays As Date

        <Required> _
        <Display(Name:="Work On Wednesdays")> _
        Public Property WorkOnWednesdays As Boolean


        <Display(Name:="Start Time Wednesdays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property StartTimeWednesdays As Date


        <Display(Name:="End Time Wednesdays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property EndTimeWednesdays As Date

        <Required> _
    <Display(Name:="Work On Thursdays")> _
        Public Property WorkOnThursdays As Boolean


        <Display(Name:="Start Time Thursdays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property StartTimeThursdays As Date


        <Display(Name:="End Time Thursdays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property EndTimeThursdays As Date

        <Required> _
    <Display(Name:="Work On Fridays")> _
        Public Property WorkOnFridays As Boolean


        <Display(Name:="Start Time Fridays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property StartTimeFridays As Date


        <Display(Name:="End Time Fridays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property EndTimeFridays As Date

        <Required> _
    <Display(Name:="Work On Saturdays")> _
        Public Property WorkOnSaturdays As Boolean

        <Display(Name:="Start Time Saturdays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property StartTimeSaturdays As Date


        <Display(Name:="End Time Saturdays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property EndTimeSaturdays As Date

        <Required> _
    <Display(Name:="Work On Sundays")> _
        Public Property WorkOnSundays As Boolean


        <Display(Name:="Start Time Sundays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property StartTimeSundays As Date


        <Display(Name:="End Time Sundays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property EndTimeSundays As Date

        <Required> _
    <Display(Name:="Work On PublicHolidays")> _
        Public Property WorkOnPublicHolidays As Boolean


        <Display(Name:="Start Time PublicHolidays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property StartTimePublicHolidays As Date


        <Display(Name:="End Time PublicHolidays")> _
        <DisplayFormat(ApplyFormatInEditMode:=True, DataFormatString:="{0:HH:mm}")> _
        <DataType(DataType.Time)> _
        Public Property EndTimePublicHolidays As Date



    End Class

#End Region

End Class

Module ExtensionFunctions

    <Extension()> Public Function ToObjectList(ByRef PointList As IEnumerable(Of Point)) As List(Of Object)

        Return PointList.Select(Function(d) CType(d, Object)).ToList

    End Function
    <Extension()> Public Function ToObjectArray(ByRef PointList As IEnumerable(Of Point)) As Object()

        Return PointList.ToObjectList.ToArray

    End Function

End Module

Module HighChartExtensions

    ''' <summary>
    ''' Return a point with the marker and hover events disabled.
    ''' </summary>
    ''' <param name="PointX"></param>
    ''' <param name="PointY"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' N.B. Can't find an easy way to hide the tooltip for a specific point.
    ''' </remarks>
    Private Function DisabledPoint(PointX As Double, PointY As Double) As Point

        Return New Point With {
            .X = PointX,
            .Y = PointY,
            .Marker = New PlotOptionsSeriesMarker With {
                .Enabled = False,
                .States = New PlotOptionsSeriesMarkerStates With {
                    .Hover = New PlotOptionsSeriesMarkerStatesHover With {.Enabled = False}
                    }
                },
            .Events = New PlotOptionsSeriesPointEvents With {
                .MouseOver = Nothing,
                .MouseOut = Nothing}
                }


    End Function

    <Extension()> Public Function ToHighChartsTimestamp(ByRef FromDateTime As Date) As Double

        Return DotNet.Highcharts.Helpers.Tools.GetTotalMilliseconds(FromDateTime)

    End Function


#Region "Get Series Data"


    <Extension()> Public Function GetCategorySeriesData(ByRef FilteredMeasurementsSequence As IFilteredMeasurementsSequence, ndp As Integer) As Helpers.Data

        Return New Helpers.Data(FilteredMeasurementsSequence.getMeasurementLevels.Select(Function(l) Round(l, ndp, MidpointRounding.AwayFromZero)).ToObjectArray)

    End Function
    <Extension()> Public Function GetCategorySeriesData(ByRef FilteredMeasurementsSequence As IFilteredMeasurementsSequence,
                                                        PlotDateTimes As List(Of Date), ndp As Integer) As Helpers.Data

        Dim levelsList As New List(Of Double)
        For Each d In PlotDateTimes
            If FilteredMeasurementsSequence.hasMeasurementAtDateTime(d) Then
                levelsList.Add(
                    Round(FilteredMeasurementsSequence.getMeasurementAtDateTime(d).getFilteredLevel,
                          ndp, MidpointRounding.AwayFromZero)
                    )
            Else
                levelsList.Add(0)
            End If
        Next

        Return New Helpers.Data(levelsList.ToObjectArray)

    End Function
    <Extension()> Public Function GetScatterSeriesData(FilteredMeasurementsSequence As IFilteredMeasurementsSequence, ndp As Integer) As Helpers.Data

        Dim series As New Series
        Dim points As New List(Of Point)

        Dim fms = FilteredMeasurementsSequence.getFilteredMeasurementsList
        For i = 0 To fms.Count - 1
            Dim fm = fms(i)
            points.Add(New Point With {.X = fm.getStartDateTime.ToHighChartsTimestamp,
                                       .Y = fm.getFilteredLevel})
        Next

        Return New Helpers.Data(points.ToArray)

    End Function
    <Extension()> Public Function GetLineSeriesData(FilteredMeasurementsSequence As IFilteredMeasurementsSequence, ndp As Integer) As Helpers.Data

        Dim series As New Series
        Dim points As New List(Of Point)

        Dim fms = FilteredMeasurementsSequence.getFilteredMeasurementsList
        For i = 0 To fms.Count - 1
            Dim fm = fms(i)
            points.Add(New Point With {.X = fm.getStartDateTime.ToHighChartsTimestamp,
                                       .Y = Round(fm.getFilteredLevel, ndp, MidpointRounding.AwayFromZero)})
            If i < fms.Count - 1 Then
                If fms(i).getEndDateTime.ToOADate < fms(i + 1).getStartDateTime.ToOADate Then
                    points.Add(Nothing)
                End If
            End If
        Next

        Return New Helpers.Data(points.ToArray)

    End Function
    <Extension()> Public Function GetStepLineSeriesData(FilteredMeasurementsSequence As IFilteredMeasurementsSequence, ndp As Integer) As Helpers.Data

        Dim series As New Series
        Dim points As New List(Of Point)

        Dim fms = FilteredMeasurementsSequence.getFilteredMeasurementsList
        For i = 0 To fms.Count - 1
            Dim fm = fms(i)
            ' Add a break point if there is a gap
            If i > 0 Then
                Dim fmLast = fms(i - 1)
                If fmLast.getEndDateTime <> fm.getStartDateTime Then
                    points.Add(Nothing)
                End If
            End If
            ' Add the start point of the step
            points.Add(
                New Point With {
                    .X = fm.getStartDateTime.ToHighChartsTimestamp,
                    .Y = Round(fm.getFilteredLevel, ndp, MidpointRounding.AwayFromZero)
                }
            )
            ' Add the end point of the step
            points.Add(DisabledPoint(
                fm.getEndDateTime.ToHighChartsTimestamp,
                Round(fm.getFilteredLevel, ndp, MidpointRounding.AwayFromZero)
            ))
        Next

        Return New Helpers.Data(points.ToArray)

    End Function
    <Extension()> Public Function GetStepLineSeriesData(MeasurementsList As List(Of Measurement), NumRoundingDecimalPlaces As Integer) As Helpers.Data

        Dim series As New Series
        Dim points As New List(Of Point)
        ' TODO: Uncomment this if there are errors in getting stepline series data
        Dim ml = MeasurementsList '.OrderBy(Function(m) m.StartDateTime).ToList

        For i = 0 To ml.Count - 1
            Dim m = ml(i)
            ' Add a break point if there is a gap (allow 1 hour tolerance in case measurements have been downsampled for display)
            If i > 0 Then
                Dim mLast = ml(i - 1)
                If m.getStartDateTime <> mLast.getEndDateTime Then
                    points.Add(Nothing)
                End If
            End If
            ' Add the start point of the step
            points.Add(
                New Point With {
                    .X = m.getStartDateTime.ToHighChartsTimestamp,
                    .Y = Round(m.getLevel, NumRoundingDecimalPlaces, MidpointRounding.AwayFromZero)
                }
            )
            ' Add the end point of the step
            points.Add(DisabledPoint(
                m.getEndDateTime.ToHighChartsTimestamp,
                Round(m.getLevel, NumRoundingDecimalPlaces, MidpointRounding.AwayFromZero)
            ))
        Next

        Return New Helpers.Data(points.ToArray)

    End Function
    <Extension()> Public Function GetSummaryLineSeriesData(FilteredMeasurementsSequence As IFilteredMeasurementsSequence, ndp As Integer) As Helpers.Data

        Dim series As New Series
        Dim points As New List(Of Point)

        Dim fms = FilteredMeasurementsSequence.getFilteredMeasurementsList
        For i = 0 To fms.Count - 1
            Dim fm = fms(i)
            points.Add(
                New Point With {.X = fm.getStartDateTime.ToHighChartsTimestamp,
                                .Y = Round(fm.getFilteredLevel, ndp, MidpointRounding.AwayFromZero)})
            points.Add(
                DisabledPoint(fm.getEndDateTime.ToHighChartsTimestamp,
                              Round(fm.getFilteredLevel, ndp, MidpointRounding.AwayFromZero))
                )
            If i < fms.Count - 1 Then
                points.Add(Nothing)
            End If
        Next

        Dim pointArray = points.ToArray
        Dim data = New Helpers.Data(pointArray)
        Return New Helpers.Data(points.ToArray)

    End Function
    '<Extension()> Public Function GetColumnSeriesData(FilteredMeasurementsSequence As IFilteredMeasurementsSequence) As Helpers.Data

    '    Dim series As New Series
    '    Dim heights As New List(Of Double)
    '    Dim points As New List(Of Point)
    '    Dim ndp = FilteredMeasurementsSequence.getMetric.RoundingDecimalPlaces

    '    Dim fms = FilteredMeasurementsSequence.getFilteredMeasurementsList
    '    For i = 0 To fms.Count - 1
    '        Dim fm = fms(i)
    '        points.Add(New Point With {.X = fm.getStartDateTime.ToHighChartsTimestamp,
    '                                   .Y = fm.getFilteredLevel})
    '    Next

    '    Return New Helpers.Data(points.ToArray)

    'End Function
    <Extension()> Public Function GetAreaSeriesData(FilteredMeasurementsSequence As IFilteredMeasurementsSequence, ndp As Integer) As Helpers.Data

        Dim series As New Series
        Dim points As New List(Of Point)

        Dim fms = FilteredMeasurementsSequence.getFilteredMeasurementsList
        For i = 0 To fms.Count - 1
            Dim fm = fms(i)
            points.Add(DisabledPoint(fm.getStartDateTime.ToHighChartsTimestamp, 0))
            points.Add(New Point With {.X = fm.getStartDateTime.ToHighChartsTimestamp,
                                       .Y = Round(fm.getFilteredLevel, fm.getMetric.RoundingDecimalPlaces, MidpointRounding.AwayFromZero)})
            points.Add(DisabledPoint(fm.getEndDateTime.ToHighChartsTimestamp,
                                     Round(fm.getFilteredLevel, fm.getMetric.RoundingDecimalPlaces, MidpointRounding.AwayFromZero)))
            points.Add(DisabledPoint(fm.getEndDateTime.ToHighChartsTimestamp, 0))
        Next

        Return New Helpers.Data(points.ToArray)

    End Function

#End Region


    Public Function GetInitValues(Optional OnLoad As String = "", Optional Width As Integer? = Nothing) As Chart

        Dim events As ChartEvents

        If OnLoad <> "" Then
            events = New ChartEvents With {.Load = OnLoad}
        Else
            events = New ChartEvents
        End If

        Dim chart = New Chart() With {
            .Height = 600,
            .Events = events,
            .BorderWidth = 1,
            .BorderColor = Drawing.Color.Gray,
            .BorderRadius = 0,
            .PlotBackgroundColor = Nothing,
            .PlotShadow = False,
            .PlotBorderWidth = 1,
            .PlotBorderColor = Drawing.Color.Black,
            .BackgroundColor = New BackColorOrGradient(
                New Gradient() With {
                    .LinearGradient = New Integer() {0, 0, 1000, 500}, _
                    .Stops = New Object(,) {{0, Drawing.Color.FromArgb(255, 255, 255, 255)},
                                            {1, Drawing.Color.FromArgb(255, 255, 255, 255)}}}),
            .ZoomType = Enums.ZoomTypes.X
        }

        If Not Width Is Nothing Then
            chart.Width = CInt(Width)
        End If

        Return chart

    End Function
    Public Function GetOptions() As GlobalOptions

        Return New GlobalOptions() With {
            .Colors = New Drawing.Color() {
                Drawing.ColorTranslator.FromHtml("#DDDF0D"),
                Drawing.ColorTranslator.FromHtml("#7798BF"),
                Drawing.ColorTranslator.FromHtml("#55BF3B"),
                Drawing.ColorTranslator.FromHtml("#DF5353"),
                Drawing.ColorTranslator.FromHtml("#DDDF0D"),
                Drawing.ColorTranslator.FromHtml("#aaeeee"),
                Drawing.ColorTranslator.FromHtml("#ff0066"),
                Drawing.ColorTranslator.FromHtml("#eeaaee")
            },
            .Global = New Options.Global With {.UseUTC = True}
        }

    End Function
    Public Function GetExporting(Optional FileName As String = "MyChart") As Exporting

        'Dim uri = HttpContext.Current.Request.Url
        'Dim root = uri.GetLeftPart(UriPartial.Authority)
        'Return New Exporting With {.Url = root + "/Content/HighchartsExport.axd",
        '                           .Filename = "MyChart",
        '                           .Width = 1200}

        Return New Exporting With {.Filename = FileName,
                                   .SourceHeight = 1000, .SourceWidth = 1530}

    End Function

    Public Function GetTitle(ByVal ChartTitle As String)

        Return New Title() With {.Text = ChartTitle, _
                                 .Style = "color: '#262261', font: '16px Helvetica 45 light, Helvetica, Arial, Sans-Serif'"}

    End Function
    Public Function GetSubTitle(Optional ByVal ChartSubTitle As String = "") As Subtitle

        Return New Subtitle() With {.Text = If(ChartSubTitle <> "", ChartSubTitle, Nothing),
                                    .Style = "color: '#DDD', font: '12px Helvetica 45 light, Helvetica, Arial, Sans-Serif'"}

    End Function
    Public Function GetPlotOptions() As PlotOptions

        Return New PlotOptions() With {
                   .Line = New PlotOptionsLine() With {
                               .DataLabels = New PlotOptionsLineDataLabels() With {
                                                   .Color = Drawing.ColorTranslator.FromHtml("#CCC")},
                                                   .Marker = New PlotOptionsLineMarker() With {
                                                   .LineColor = Drawing.ColorTranslator.FromHtml("#333")}},
                   .Spline = New PlotOptionsSpline() With {
                                   .Marker = New PlotOptionsSplineMarker() With {
                                   .LineColor = Drawing.ColorTranslator.FromHtml("#333")}},
                   .Scatter = New PlotOptionsScatter() With {
                                   .Marker = New PlotOptionsScatterMarker() With {
                                   .LineColor = Drawing.ColorTranslator.FromHtml("#333")}}}

    End Function


    Public Function GetLegend() As Legend

        '.Align = Enums.HorizontalAligns.Right,
        '.VerticalAlign = Enums.VerticalAligns.Top,
        '.Floating = True,

        Return New Legend() With {
            .BorderColor = System.Drawing.ColorTranslator.FromHtml("#262261"), .BorderWidth = 1, .BorderRadius = 3,
            .ItemStyle = "color: '#262261'",
            .ItemHoverStyle = "color: '#00B0D8'",
            .ItemHiddenStyle = "color: '#AFDFE5'",
            .UseHTML = True
        }

    End Function
    Public Function GetCredits(LinkText As String, LinkUrl As String) As Credits

        If LinkText <> "" Then
            Return New Credits With {.Text = LinkText, .Href = LinkUrl}
        Else
            Return New Credits With {.Text = "", .Href = ""}
        End If


    End Function
    Public Function GetToolTip() As Tooltip

        Return (New Tooltip() With {
                    .BorderWidth = 0, _
                    .Style = "color: '#262261'", _
                    .BackgroundColor = New BackColorOrGradient(New Gradient() With {.LinearGradient = New Integer() {0, 0, 600, 400}, _
                                                                                    .Stops = New Object(,) {{0, Drawing.Color.FromArgb(255, 255, 255, 255)},
                                                                                                            {1, Drawing.Color.FromArgb(255, 175, 223, 229)}}})})

    End Function
    Public Function GetLabels() As Labels

        Return New Labels() With {.Style = "color: '#262261'"}

    End Function


#Region "Get Axes"

    Public Function GetTickIntervalMillis(ByVal StartDateTime As DateTime, ByVal EndDateTime As DateTime) As Integer

        ' Set axes
        Dim tickIntervalMillis As Integer
        If EndDateTime.Subtract(StartDateTime) <= New TimeSpan(1, 0, 0, 0) Then
            tickIntervalMillis = 1 * 3600 * 1000
        ElseIf EndDateTime.Subtract(StartDateTime) <= New TimeSpan(7, 0, 0, 0) Then
            tickIntervalMillis = 4 * 3600 * 1000
        Else
            tickIntervalMillis = 24 * 3600 * 1000
        End If

        Return tickIntervalMillis

    End Function

    Public Function GetCategoryAxis(DistinctStartDateTimes As List(Of Date), ByVal ViewDuration As String) As XAxis

        Dim Categories As String()
        Dim lstCategories As New List(Of String)
        Dim axisTitle As String = "X"

        Select Case (LCase(ViewDuration))
            Case "day"
                axisTitle = "Start Time"
                For Each dsd In DistinctStartDateTimes
                    lstCategories.Add(Format(dsd, "HH:mm"))
                Next
            Case "week"
                axisTitle = "Day of Week"
                Dim dateOnMonday = MondayInTheWeekOf(DistinctStartDateTimes.First.Date).DateOnly
                For d = 0 To 6
                    lstCategories.Add(Format(dateOnMonday.AddDays(d), "ddd"))
                Next
            Case "month"
                axisTitle = "Day of Month"
                Dim dsd1 = DistinctStartDateTimes.First.Date
                Dim currentDate = New Date(dsd1.Year, dsd1.Month, 1)
                While currentDate.Month = dsd1.Month
                    lstCategories.Add(Format(currentDate, "dd"))
                    currentDate = currentDate.AddDays(1)
                End While
        End Select
        Categories = lstCategories.ToArray

        '.StartOnTick = True
        Return New XAxis With {.Type = Enums.AxisTypes.Category,
                               .Categories = Categories,
                               .GridLineWidth = 1,
                               .LineColor = System.Drawing.ColorTranslator.FromHtml("#262261"),
                               .TickColor = System.Drawing.ColorTranslator.FromHtml("#262261"),
                               .Labels = New XAxisLabels() With {.Style = "color: '#262261', fontWeight: 'bold'"},
                               .Title = New XAxisTitle() With {.Style = "color: '#262261', font: 'bold 12px Helvetica 45 light, Helvetica, Arial, Sans-Serif'",
                                                               .Text = axisTitle}}


    End Function
    Public Function GetDateTimeAxis(ByVal AxisTitle As String,
                                    ByVal StartDateTime As Date, ByRef EndDateTime As Date,
                                    ByVal TickIntervalMillis As Integer) As XAxis

        '.StartOnTick = True
        Return New XAxis With {
            .Type = Enums.AxisTypes.Datetime,
            .Min = StartDateTime.ToHighChartsTimestamp,
            .Max = EndDateTime.ToHighChartsTimestamp,
            .GridLineWidth = 0.5,
            .GridLineColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF"),
            .LineColor = System.Drawing.ColorTranslator.FromHtml("#262261"),
            .TickColor = System.Drawing.ColorTranslator.FromHtml("#262261"),
            .TickInterval = CDbl(TickIntervalMillis),
            .Labels = New XAxisLabels() With {
                .Style = "color: '#262261'",
                .Format = "{value:%H:%M %a %d %b}",
                .Rotation = -90
            },
            .Title = New XAxisTitle() With {
                .Style = "color: '#262261', font: 'bold 12px Helvetica 45 light, Helvetica, Arial, Sans-Serif'",
                .Text = AxisTitle
            }
        }

    End Function

    Public Function GetDateTimeAxis(ByVal axisTitle As String,
                                    ByVal startDateTime As Date, ByRef endDateTime As Date,
                                    ByVal tickIntervalMillis As Integer,
                                    ByVal comments As IEnumerable(Of MeasurementComment),
                                    Optional ByVal nonWorkingHours As IEnumerable(Of DateTimeRange) = Nothing) As XAxis

        Dim xAxis = GetDateTimeAxis(axisTitle, startDateTime, endDateTime, tickIntervalMillis)
        Dim allPlotBands As New List(Of XAxisPlotBands)

        ' Add plot bands for non-working hours
        If nonWorkingHours IsNot Nothing Then
            For Each nonWorkingPeriod In nonWorkingHours
                allPlotBands.Add(
                        New XAxisPlotBands With {
                            .From = nonWorkingPeriod.StartDateTime.ToHighChartsTimestamp,
                            .To = nonWorkingPeriod.EndDateTime.ToHighChartsTimestamp,
                            .Color = Drawing.Color.FromArgb(206, 227, 252)
                        }
                    )
            Next
        End If

        ' Add plot bands for comments
        For Each comment In comments
            Try
                Dim bandStart = comment.FirstMeasurementStartDateTime
                Dim bandEnd = comment.LastMeasurementEndDateTime
                allPlotBands.Add(
                    New XAxisPlotBands With {
                        .From = bandStart.ToHighChartsTimestamp,
                        .To = bandEnd.ToHighChartsTimestamp,
                        .Color = Drawing.Color.LightGray,
                        .Label = New XAxisPlotBandsLabel With {
                            .Text = comment.CommentText,
                            .Rotation = -90,
                            .TextAlign = Enums.HorizontalAligns.Right
                        }
                    }
                )
            Catch ex As Exception
                ' An error will be thrown if the comment does not contain any measurements
                Debug.WriteLine(String.Format("Error getting measurements from comment with Id={0}", comment.Id))
            End Try
        Next

        If allPlotBands.Count > 0 Then
            xAxis.PlotBands = allPlotBands.ToArray()
        End If

        xAxis.TickInterval = Nothing
        xAxis.TickPixelInterval = 50


        Return xAxis

    End Function

    Public Function GetYAxis(ByVal AxisTitle As String,
                             Optional ByVal TickInterval As Double? = Nothing,
                             Optional ByVal MinValue As Double? = Nothing,
                             Optional ByVal MaxValue As Double? = Nothing) As YAxis

        '.MinorTickInterval = Nothing,
        '.GridLineColor = System.Drawing.Color.FromArgb(255, 255, 255, 255),
        '.LineWidth = 0,
        '.TickWidth = 0,
        '.AlternateGridColor = Nothing,
        '.Labels = New YAxisLabels() With {.Style = "color: '#262261',fontWeight: 'bold'"},
        Dim yAxis = New YAxis() With {
            .GridLineWidth = 0.5,
            .GridLineColor = System.Drawing.ColorTranslator.FromHtml("#DFDFDF"),
            .LineColor = System.Drawing.ColorTranslator.FromHtml("#262261"),
            .TickColor = System.Drawing.ColorTranslator.FromHtml("#262261"),
            .TickInterval = TickInterval,
            .Labels = New YAxisLabels() With {.Style = "color: '#262261'"},
            .Title = New YAxisTitle() With {
                .Style = "color: '#262261',font: 'bold 12px Helvetica 45 light, Helvetica, Arial, Sans-Serif'",
                .Text = AxisTitle
            }
        }

        If Not MinValue Is Nothing Then yAxis.Min = CDbl(MinValue)
        If Not MaxValue Is Nothing Then yAxis.Max = CDbl(MaxValue)

        Return yAxis

    End Function

#End Region


End Module

