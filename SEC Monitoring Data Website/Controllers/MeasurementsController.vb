Imports System.Data.Entity.Core
Imports libSEC
Imports System.Math
Imports System.Drawing
Imports DotNet.Highcharts
Imports DotNet.Highcharts.Helpers
Imports DotNet.Highcharts.Options
Imports System.IO

Namespace SEC_Monitoring_Data_Website

    Public Class MeasurementsController
        Inherits ControllerBase

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)

        End Sub

#Region "View"

        <ActionName("View")> _
        Public Function Vue(ByVal ProjectRouteName As String, ByVal MonitorLocationRouteName As String, ByVal ViewRouteName As String, ByVal ViewDuration As String, ByVal strStartDate As String) As ActionResult

            If Not UAL.CanViewMeasurements Then Return New HttpUnauthorizedResult()

            ' Redirect to Default View if startDate cannot be parsed
            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(
                ProjectRouteName.FromRouteName, MonitorLocationRouteName.FromRouteName
            )
            If monitorLocation Is Nothing Then Return HttpNotFound()

            If monitorLocation.MeasurementFiles.Where(
                Function(mf) mf.NumberOfMeasurements > 0
            ).Count = 0 Then Return RedirectToRoute(
                    "MonitorLocationDetailsRoute",
                    New With {.ProjectRouteName = monitorLocation.Project.getRouteName,
                              .MonitorLocationRouteName = monitorLocation.getRouteName}
                )

            Dim lastMeasurementDate = monitorLocation.getLastMeasurementEndDateTime()

            Dim startDate As Date

            If Date.TryParse(strStartDate, startDate) = False Then
                Return New RedirectToRouteResult(
                    "MeasurementViewRoute",
                    New RouteValueDictionary(
                        New With {
                            .ProjectRouteName = ProjectRouteName,
                            .MonitorLocationRouteName = MonitorLocationRouteName.FromRouteName,
                            .ViewRouteName = "Default",
                            .ViewDuration = "Day",
                            .strStartDate = Format(lastMeasurementDate, "dd-MMM-yyyy")
                        }
                    )
                )
            End If

            If startDate.ToOADate > lastMeasurementDate.ToOADate Then startDate = lastMeasurementDate.DateOnly

            Dim vm = getViewMeasurementsViewModel(
                ProjectRouteName, MonitorLocationRouteName, ViewRouteName, ViewDuration, startDate
            )
            If vm IsNot Nothing Then
                Return View(vm)
            Else
                Return RedirectToRoute(
                    "MonitorLocationDetailsRoute",
                    New With {.ProjectRouteName = ProjectRouteName,
                              .MonitorLocationRouteName = MonitorLocationRouteName}
                    )
            End If

        End Function

        Private Function getViewMeasurementsViewModel(
            ByVal ProjectRouteName As String, ByVal MonitorLocationRouteName As String, ByVal ViewRouteName As String,
            ByVal ViewDuration As String, ByVal StartDate As Date,
            Optional ByVal GraphYmin As Integer? = Nothing, Optional ByVal GraphYmax As Integer? = Nothing
        ) As ViewMeasurementsViewModel

            ' Set start and end view dates
            Dim modelStartDate As Date, modelEndDate As Date
            Select Case LCase(ViewDuration)
                Case "day"
                    modelStartDate = StartDate
                    modelEndDate = modelStartDate.AddDays(1)
                Case "week"
                    modelStartDate = StartDate
                    modelEndDate = modelStartDate.AddDays(7)
                Case "month"
                    modelStartDate = StartDate
                    modelEndDate = modelStartDate.AddMonths(1)
            End Select

            Dim Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            Dim MonitorLocation = MeasurementsDAL.GetMonitorLocation(
                ProjectRouteName.FromRouteName,
                MonitorLocationRouteName.FromRouteName
            )

            ' Settings
            Dim selectableMeasurementViews = Project.MeasurementViews.Where(
                Function(mv) mv.MeasurementTypeId = MonitorLocation.MeasurementTypeId
            )
            If selectableMeasurementViews.Count = 0 Then Return Nothing
            ViewRouteName = If(ViewRouteName = "Default",
                               selectableMeasurementViews(0).getRouteName,
                               ViewRouteName)
            Dim selectedMeasurementView = MeasurementsDAL.GetMeasurementView(ViewRouteName.FromRouteName)

            ' Get data from database and filter
            Dim measurementSequenceList As New List(Of FilteredMeasurementsSequence)
            For Each mvg In selectedMeasurementView.MeasurementViewGroups
                For Each mvss In mvg.MeasurementViewSequenceSettings
                    Dim calcFilter = mvss.CalculationFilter
                    Dim rmParams As New ReadMeasurementParameters With {
                        .Metric = calcFilter.MeasurementMetric,
                        .MonitorLocation = MonitorLocation,
                        .StartDate = modelStartDate,
                        .EndDate = modelEndDate
                        }
                    Dim measurementsSequence = ReadAndFilter(MeasurementsDAL, rmParams, calcFilter)
                    If measurementsSequence.Count > 0 Then measurementSequenceList.Add(measurementsSequence)
                Next
            Next


            Dim startDateTimeLists = measurementSequenceList.Select(
                Function(ms) ms.getMeasurementStartDateTimes
                )
            Dim startDateTimes = startDateTimeLists.MergeDateTimeLists.ToList

            ' Create Navigation Buttons
            Dim buttons = getNavigationButtons(MonitorLocation, modelStartDate)

            Dim firstMeasurementDate = MonitorLocation.getFirstMeasurementStartDateTime.DateOnly
            Dim lastMeasurementDate = MonitorLocation.getLastMeasurementStartDateTime.DateOnly

            ' Graph Y Range
            Dim graphMinY As Integer = 0
            If Not Nullable.Equals(GraphYmin, Nothing) Then graphMinY = CInt(GraphYmin)
            Dim graphMaxY As Integer = 0
            If Not Nullable.Equals(GraphYmax, Nothing) Then graphMaxY = CInt(GraphYmax)

            ' Create ViewModel
            Dim vm As New ViewMeasurementsViewModel With {
                .CurrentMonitor = MeasurementsDAL.GetCurrentMonitor(
                    ProjectRouteName.FromRouteName,
                    MonitorLocationRouteName.FromRouteName
                    ),
                .MonitorLocation = MonitorLocation,
                .MonitorLocationRouteName = MonitorLocation.getRouteName,
                .FilteredMeasurements = measurementSequenceList,
                .StartDate = modelStartDate, .EndDate = modelEndDate,
                .SelectedMeasurementView = selectedMeasurementView,
                .SelectableMeasurementViews = selectableMeasurementViews,
                .Project = MonitorLocation.Project,
                .ProjectRouteName = MonitorLocation.Project.getRouteName,
                .ViewDuration = ViewDuration,
                .ViewName = ViewRouteName.FromRouteName,
                .FirstMeasurementDate = firstMeasurementDate,
                .LastMeasurementDate = lastMeasurementDate,
                .VSliderMinValue = graphMinY, .VSliderMaxValue = graphMaxY,
                .VSliderMinLimit = selectedMeasurementView.YAxisMinValue,
                .VSliderMaxLimit = selectedMeasurementView.YAxisMaxValue,
                .VSliderStep = selectedMeasurementView.YAxisStepValue,
                .NavigationButtons = buttons
            }

            Select Case selectedMeasurementView.MeasurementViewTableType.TableTypeName
                Case "Daily"
                    vm.StartDateTimes = DateList(modelStartDate,
                                                 modelEndDate.AddDays(-1),
                                                 TimeResolutionType.Day)
                Case "Dynamic"
                    vm.StartDateTimes = startDateTimes
            End Select

            Return vm

        End Function

        Private Function getNavigationButtons(monitorLocation As MonitorLocation, Optional assessmentDate As Nullable(Of Date) = Nothing) As IEnumerable(Of NavigationButtonViewModel)

            Dim buttons As New List(Of NavigationButtonViewModel)
            If UAL.CanViewAssessments Then buttons.Add(monitorLocation.getAssessmentViewRouteButton64(assessmentDate))
            If UAL.CanViewMonitorLocationDetails Then buttons.Add(monitorLocation.getDetailsRouteButton("Back to Monitor Location"))
            If UAL.CanViewProjectDetails Then buttons.Add(monitorLocation.Project.getDetailsRouteButton64("Back to Project"))

            Return buttons

        End Function

        <HttpGet()> _
        Public Function UpdateView(ByVal ProjectRouteName As String, ByVal MonitorLocationRouteName As String,
                            ByVal ViewName As String, ByVal ViewDuration As String, ByVal strStartDate As String,
                            ByVal GraphMinY As Integer, GraphMaxY As Integer) As PartialViewResult

            Dim startDate As Date = New Date(
                CInt(Right(strStartDate, 4)),
                CInt(Mid(strStartDate, 3, 2)),
                CInt(Left(strStartDate, 2))
            )
            Return PartialView(
                getViewMeasurementsViewModel(
                    ProjectRouteName:=ProjectRouteName,
                    MonitorLocationRouteName:=MonitorLocationRouteName,
                    ViewRouteName:=ViewName.ToRouteName,
                    ViewDuration:=ViewDuration,
                    StartDate:=startDate,
                    GraphYmin:=GraphMinY,
                    GraphYmax:=GraphMaxY
                )
            )

        End Function

        <HttpGet()> _
        Public Function UpdateNavigationButtons(ByVal ProjectRouteName As String, ByVal MonitorLocationRouteName As String, ByVal strAssessmentDate As String) As PartialViewResult

            Dim startDate As Date = New Date(CInt(Right(strAssessmentDate, 4)), CInt(Mid(strAssessmentDate, 3, 2)), CInt(Left(strAssessmentDate, 2)))
            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(ProjectRouteName.FromRouteName, MonitorLocationRouteName.FromRouteName)
            Return PartialView("NavigationButtons", getNavigationButtons(monitorLocation, startDate))

        End Function

        <ChildActionOnly()> _
        Public Function View_MeasurementsChart(model As ViewMeasurementsViewModel) As ActionResult

            ' Create view data for chart
            Dim chart As New Highcharts("MeasurementsChart")
            chart.InitChart(GetInitValues(OnLoad:="setChartVariable")) _
                 .SetOptions(GetOptions) _
                 .SetSubtitle(GetSubTitle) _
                 .SetLegend(GetLegend) _
                 .SetTooltip(GetToolTip) _
                 .SetLabels(GetLabels) _
                 .SetPlotOptions(GetPlotOptions)
            chart.SetExporting(GetExporting("View Measurements Chart"))
            chart.SetTitle(GetTitle("Measurements for " + model.MonitorLocation.MonitorLocationName + _
                                    " from " + Format(model.StartDate, "dd-MMM-yy") + " to " + Format(model.EndDate, "dd-MMM-yy")))
            Dim tickIntervalMillis = GetTickIntervalMillis(model.StartDate, model.EndDate)
            chart.SetXAxis(GetDateTimeAxis("Date and Time", model.StartDate, model.EndDate, tickIntervalMillis))
            chart.SetYAxis(GetYAxis("Level"))
            chart.SetCredits(GetCredits("", ""))

            ' If all series are of the category type then set the x-axis to be a category axis
            Dim allCategoryTypes As Boolean = True
            For Each seq In model.SelectedMeasurementView.getSequenceSettings
                Select Case LCase(model.ViewDuration)
                    Case "day" : If seq.DayViewSeriesType.SeriesTypeName <> "Column" Then allCategoryTypes = False
                    Case "week" : If seq.WeekViewSeriesType.SeriesTypeName <> "Column" Then allCategoryTypes = False
                    Case "month" : If seq.MonthViewSeriesType.SeriesTypeName <> "Column" Then allCategoryTypes = False
                End Select
            Next
            If allCategoryTypes = True Then
                Dim distinctStartDateTimes As New List(Of Date)
                For Each m In model.FilteredMeasurements
                    distinctStartDateTimes.AddRange(m.getDistinctStartDateTimes)
                Next
                distinctStartDateTimes.Sort()
                distinctStartDateTimes = distinctStartDateTimes.Distinct.ToList
                chart.SetXAxis(GetCategoryAxis(distinctStartDateTimes, model.ViewDuration))
            End If

            Dim seriesList As New List(Of Series)
            Dim daysOfWeek = MeasurementsDAL.GetDaysOfWeek
            For Each s In model.FilteredMeasurements
                If s.Count > 0 Then
                    Dim sequenceSetting = model.SelectedMeasurementView.getSequenceSettings.Single(
                        Function(ss) ss.CalculationFilter.FilterName = s.getFilter.FilterName
                    )
                    Dim seriesType As String = ""
                    Select Case LCase(model.ViewDuration)
                        Case "day" : seriesType = sequenceSetting.DayViewSeriesType.SeriesTypeName
                        Case "week" : seriesType = sequenceSetting.WeekViewSeriesType.SeriesTypeName
                        Case "month" : seriesType = sequenceSetting.MonthViewSeriesType.SeriesTypeName
                    End Select
                    Dim ndp = sequenceSetting.CalculationFilter.MeasurementMetric.RoundingDecimalPlaces
                    Select Case seriesType
                        Case "Line"
                            seriesList.Add(
                                New Series With {
                                    .Data = s.GetLineSeriesData(ndp),
                                    .Name = sequenceSetting.SeriesName,
                                    .Type = Enums.ChartTypes.Line,
                                    .Color = ColorTranslator.FromHtml(sequenceSetting.SeriesColour),
                                    .PlotOptionsSeries = New PlotOptionsSeries With {.TurboThreshold = 0}
                                }
                            )
                        Case "Step Line"
                            seriesList.Add(
                                New Series With {
                                    .Data = s.GetStepLineSeriesData(ndp),
                                    .Name = sequenceSetting.SeriesName,
                                    .Type = Enums.ChartTypes.Line,
                                    .Color = ColorTranslator.FromHtml(sequenceSetting.SeriesColour),
                                    .PlotOptionsSeries = New PlotOptionsSeries With {.TurboThreshold = 0}
                                }
                            )
                        Case "Summary Line"
                            seriesList.Add(
                                New Series With {
                                    .Data = s.GetSummaryLineSeriesData(ndp),
                                    .Name = sequenceSetting.SeriesName,
                                    .Type = Enums.ChartTypes.Line,
                                    .Color = ColorTranslator.FromHtml(sequenceSetting.SeriesColour),
                                    .PlotOptionsSeries = New PlotOptionsSeries With {.TurboThreshold = 0}
                                }
                            )
                        Case "Area"
                            seriesList.Add(
                                New Series With {
                                    .Data = s.GetAreaSeriesData(ndp),
                                    .Name = sequenceSetting.SeriesName,
                                    .Type = Enums.ChartTypes.Area,
                                    .Color = ColorTranslator.FromHtml(sequenceSetting.SeriesColour),
                                    .PlotOptionsSeries = New PlotOptionsSeries With {.TurboThreshold = 0}
                                }
                            )
                        Case "Column"

                            ' Build a list of date times for empty columns
                            Dim calcFilter = s.getFilter
                            Dim plotFilter = New CalculationFilter With {
                                .TimeBase = calcFilter.TimeBase,
                                .TimeStep = calcFilter.TimeStep,
                                .TimeWindowEndTime = calcFilter.TimeWindowEndTime,
                                .TimeWindowStartTime = calcFilter.TimeWindowStartTime,
                                .UseTimeWindow = calcFilter.UseTimeWindow
                            }
                            For Each dow In daysOfWeek
                                plotFilter.ApplicableDaysOfWeek.Add(dow)
                            Next
                            Dim plotDateTimes As New List(Of Date)
                            For Each d As Date In DateList(model.StartDate, model.EndDate.AddDays(-1))
                                plotDateTimes.AddRange(plotFilter.getStartDateTimes(d))
                            Next
                            seriesList.Add(New Series With {
                                               .Data = s.GetCategorySeriesData(plotDateTimes, ndp),
                                               .Name = sequenceSetting.SeriesName,
                                               .Type = Enums.ChartTypes.Column,
                                               .Color = ColorTranslator.FromHtml(sequenceSetting.SeriesColour),
                                               .PlotOptionsSeries = New PlotOptionsSeries With {.TurboThreshold = 0}
                                           })
                    End Select
                End If
            Next

            chart.SetSeries(seriesList.ToArray)
            Dim yAxis As New YAxis With {.Title = New YAxisTitle With {.Text = "Level"},
                                         .TickInterval = model.VSliderStep}
            If model.VSliderMinValue <> 0 And model.VSliderMaxValue <> 0 Then
                yAxis.Min = model.VSliderMinValue
                yAxis.Max = model.VSliderMaxValue
            End If
            chart.SetYAxis(yAxis)


            Return PartialView(chart)

        End Function
        <ChildActionOnly()> _
        Public Function ViewDailyMeasurementsTable(model As ViewMeasurementsViewModel) As ActionResult

            Return PartialView(model)

        End Function
        <ChildActionOnly()> _
        Public Function ViewMeasurementsNavigator(model As ViewMeasurementsNavigatorViewModel) As ActionResult

            Return PartialView(model)

        End Function

#End Region

#Region "Upload"

        <HttpGet()> _
        Public Function Upload(ByVal ProjectRouteName As String, ByVal MonitorLocationRouteName As String) As ActionResult

            If (Not UAL.CanUploadMeasurements Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            Dim ProjectName = ProjectRouteName.FromRouteName
            Dim MonitorLocationName = MonitorLocationRouteName.FromRouteName

            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(ProjectName, MonitorLocationName)
            Dim monitor = MeasurementsDAL.GetCurrentMonitor(ProjectName, MonitorLocationName)
            Dim measurementFileTypes = MeasurementsDAL.GetMeasurementFileTypes(monitorLocation.MeasurementTypeId).ToList

            Dim roundInputDurationList As New SelectList(
                {New With {.Value = "Seconds", .Text = "Seconds"},
                 New With {.Value = "Minutes", .Text = "Minutes"},
                 New With {.Value = "Hours", .Text = "Hours"}},
                "Value", "Text"
            )

            Dim addTimeDurationList As New SelectList(
                {New With {.Value = "Seconds", .Text = "Seconds"},
                 New With {.Value = "Minutes", .Text = "Minutes"},
                 New With {.Value = "Hours", .Text = "Hours"}},
                "Value", "Text"
            )

            ' Metrics
            Dim mets = MeasurementsDAL.GetMeasurementMetrics(monitor.MeasurementType).ToList
            Dim umvm As New UploadMeasurementsViewModel With {
                .MeasurementFileTypeId = Nothing,
                .MeasurementFileTypeList = New SelectList(measurementFileTypes, "Id", "FileTypeName"),
                .MeasurementFileTypes = measurementFileTypes,
                .Monitor = monitor,
                .MonitorId = monitor.Id,
                .MonitorLocation = monitorLocation,
                .MonitorLocationId = monitorLocation.Id,
                .ProjectId = monitorLocation.ProjectId,
                .RoundInputDurationValue = "Hours",
                .RoundInputDurationList = roundInputDurationList,
                .AddTimeDurationValue = "Hours",
                .AddTimeDurationList = addTimeDurationList,
                .AddDaylightSavingsTimeOffset = False,
                .AddDaylightSavingsHourCount = 1,
                .DaylightSavingsBorderDate = Date.Today,
                .DaylightSavingsBorderTime = Date.Today.AddHours(1),
                .UploadOsirisViewModel = getUploadOsirisViewModel(mets),
                .UploadPPVLiveViewModel = getUploadPPVLiveViewModel(mets),
                .UploadRedboxViewModel = getUploadRedboxViewModel(mets),
                .UploadRionLeqLiveWebsystemViewModel = getUploadRionLeqLiveWebsystemViewModel(mets),
                .UploadRionRCDSViewModel = getUploadRionRCDSViewModel(mets),
                .UploadSigicomVibrationViewModel = getUploadSigicomVibrationViewModel(mets),
                .UploadSPLTrackViewModel = getUploadSPLTrackViewModel(mets),
                .UploadSpreadsheetTemplateViewModel = getUploadSpreadsheetTemplateViewModel(),
                .UploadVibraViewModel = getUploadVibraViewModel(mets)
            }

            Return View(umvm)

        End Function
        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Upload(ByVal ViewModel As UploadMeasurementsViewModel) As ActionResult

            If (Not UAL.CanUploadMeasurements Or
                Not CanAccessProject(ViewModel.ProjectId)) Then Return New HttpUnauthorizedResult()

            ModelState.Remove("MeasurementFile.MeasurementFileType")

            If ModelState.IsValid Then
                ' Add File to File System
                Dim FnWithoutExtension = Path.GetFileNameWithoutExtension(ViewModel.UploadFile.FileName).Replace("-", " ")
                Dim FnExtension = Path.GetExtension(ViewModel.UploadFile.FileName)
                Dim dateNow = Date.Now
                Dim ServerFn = FnWithoutExtension + Format(dateNow, "_yyyyMMddHHmmss") + FnExtension
                Dim tempFilePath = Path.Combine(Server.MapPath("~/Content/MeasurementFiles/TempFiles"), ServerFn)
                Dim permFilePath = Path.Combine(Server.MapPath("~/Content/MeasurementFiles"), ServerFn)
                ViewModel.UploadFile.SaveAs(tempFilePath)
                ' Add Measurement File Record to Database
                Dim currentUser = MeasurementsDAL.GetUser(User.Identity.Name)
                Dim contactId = currentUser.Contact.Id
                Dim measurementFile = New MeasurementFile With {
                    .ContactId = contactId,
                    .MeasurementFileName = FnWithoutExtension,
                    .ServerFileName = ServerFn,
                    .MeasurementFileTypeId = ViewModel.MeasurementFileTypeId,
                    .MonitorId = ViewModel.MonitorId,
                    .MonitorLocationId = ViewModel.MonitorLocationId,
                    .UploadDateTime = dateNow,
                    .UploadSuccess = False,
                    .FirstMeasurementStartDateTime = Date.Today(),
                    .LastMeasurementStartDateTime = Date.Today(),
                    .LastMeasurementDuration = 0
                }
                measurementFile = MeasurementsDAL.AddMeasurementFile(measurementFile)
                MeasurementsDAL.SaveChanges()
                ' Try to Upload Measurements
                Dim uploadedMeasurements = TryUploadMeasurements(ViewModel, tempFilePath, measurementFile.Id, dateNow)
                If uploadedMeasurements IsNot Nothing Then
                    'Copy file into permanent record store
                    FileCopy(tempFilePath, permFilePath)
                    ' System.IO.File.Delete(tempFilePath) ' causing an error, maybe because the file is too big so it is trying to delete it before it has been copied
                    ' Modify database measurement file record
                    measurementFile.UploadSuccess = True
                    measurementFile.FirstMeasurementStartDateTime = uploadedMeasurements.Select(Function(m) m.StartDateTime).Min()
                    Dim lastMeasurementSDT = uploadedMeasurements.Select(Function(m) m.StartDateTime).Max()
                    measurementFile.LastMeasurementStartDateTime = lastMeasurementSDT
                    measurementFile.LastMeasurementDuration = uploadedMeasurements.Where(
                        Function(m) m.StartDateTime = lastMeasurementSDT
                    ).First().Duration
                    measurementFile.NumberOfMeasurements = uploadedMeasurements.Count()
                    MeasurementsDAL.SaveChanges()
                End If
                ' Redirect to Result Page
                Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(ViewModel.MonitorLocationId)
                Dim project = monitorLocation.Project
                Return UploadResult(
                    ProjectRouteName:=project.getRouteName,
                    MonitorLocationRouteName:=monitorLocation.getRouteName,
                    MeasurementFileId:=measurementFile.Id
                )
            End If

        End Function

        Private Function TryUploadMeasurements(ViewModel As UploadMeasurementsViewModel, FilePath As String,
                                               MeasurementFileId As Integer, UploadDateTime As Date) As IEnumerable(Of Measurement)

            Dim MeasurementFileType = MeasurementsDAL.GetMeasurementFileType(ViewModel.MeasurementFileTypeId)
            Dim MappingDict As New Dictionary(Of String, MeasurementMetric)

            ' Measurement File Type
            Dim fs As New MeasurementFileSettings.MeasurementFileSettings
            Select Case MeasurementFileType.FileTypeName

                Case "Osiris"
                    fs = New MeasurementFileSettings.OsirisFileSettings(MeasurementsDAL)
                    fs.MetricMappings = ViewModel.UploadOsirisViewModel.getMappingDictionary(MeasurementsDAL)
                Case "PPV Live"
                    fs = New MeasurementFileSettings.PPVLiveFileSettings(MeasurementsDAL)
                    fs.MetricMappings = ViewModel.UploadPPVLiveViewModel.getMappingDictionary(MeasurementsDAL)
                Case "Redbox"
                    fs = New MeasurementFileSettings.RedboxFileSettings(MeasurementsDAL)
                    fs.MetricMappings = ViewModel.UploadRedboxViewModel.getMappingDictionary(MeasurementsDAL)
                Case "Rion Leq Live Websystem"
                    fs = New MeasurementFileSettings.RionLeqLiveWebsystemFileSettings(MeasurementsDAL)
                    fs.MetricMappings = ViewModel.UploadRionLeqLiveWebsystemViewModel.getMappingDictionary(MeasurementsDAL)
                Case "Rion RCDS"
                    fs = New MeasurementFileSettings.RionRCDSFileSettings(MeasurementsDAL)
                    fs.MetricMappings = ViewModel.UploadRionRCDSViewModel.getMappingDictionary(MeasurementsDAL)
                Case "Sigicom Vibration"
                    fs = New MeasurementFileSettings.SigicomVibrationFileSettings(MeasurementsDAL)
                    fs.MetricMappings = ViewModel.UploadSigicomVibrationViewModel.getMappingDictionary(MeasurementsDAL)
                Case "SPLTrack"
                    fs = New MeasurementFileSettings.SPLTrackFileSettings(MeasurementsDAL)
                    fs.MetricMappings = ViewModel.UploadSPLTrackViewModel.getMappingDictionary(MeasurementsDAL)
                Case "Spreadsheet Template (Noise)"
                    fs = New MeasurementFileSettings.NoiseSpreadsheetTemplateFileSettings(MeasurementsDAL)
                Case "Spreadsheet Template (Vibration)"
                    fs = New MeasurementFileSettings.VibrationSpreadsheetTemplateFileSettings(MeasurementsDAL)
                Case "Spreadsheet Template (Air Quality, Dust and Meteorological)"
                    fs = New MeasurementFileSettings.AirQualitySpreadsheetTemplateFileSettings(MeasurementsDAL)
                Case "Vibra"
                    fs = New MeasurementFileSettings.VibraFileSettings(MeasurementsDAL)
                    fs.MetricMappings = ViewModel.UploadVibraViewModel.getMappingDictionary(MeasurementsDAL)
                Case Else

                    Return Nothing

            End Select

            ' Measurement Rounding
            Dim roundingDuration As Double = 0, roundingMultiplier As Double = 0
            If ViewModel.RoundInputMeasurements = True Then
                Select Case ViewModel.RoundInputDurationValue
                    Case "Seconds"
                        roundingMultiplier = 1 / (24 * 60 * 60)
                    Case "Minutes"
                        roundingMultiplier = 1 / (24 * 60)
                    Case "Hours"
                        roundingMultiplier = 1 / 24
                End Select
                roundingDuration = ViewModel.RoundInputCount * roundingMultiplier
            End If

            ' Measurement Offset
            Dim offsetDuration As Double = 0, offsetMultiplier As Double = 0
            If ViewModel.AddTimeOffset = True Then
                Select Case ViewModel.AddTimeDurationValue
                    Case "Seconds"
                        offsetMultiplier = 1 / (24 * 60 * 60)
                    Case "Minutes"
                        offsetMultiplier = 1 / (24 * 60)
                    Case "Hours"
                        offsetMultiplier = 1 / 24
                End Select
                offsetDuration = ViewModel.AddTimeCount * offsetMultiplier
            End If

            ' Daylight Savings Offset
            Dim daylightSavingsOffsetStartDateTime As Date
            Dim daylightSavingsOffsetDuration As Double
            If ViewModel.AddDaylightSavingsTimeOffset = True Then
                daylightSavingsOffsetStartDateTime = ViewModel.DaylightSavingsBorderDate + ViewModel.DaylightSavingsBorderTime.ToTimeSpan
                daylightSavingsOffsetDuration = ViewModel.AddDaylightSavingsHourCount / 24
            End If

            ' Read measurements from file
            If fs.ReadFile(FilePath) = True Then
                Dim measurements = fs.getMeasurements(
                    MeasurementFileId,
                    ViewModel.MonitorId, ViewModel.MonitorLocationId, ViewModel.ProjectId, CurrentContact.Id, UploadDateTime,
                    roundingDuration, offsetDuration,
                    daylightSavingsOffsetStartDateTime, daylightSavingsOffsetDuration
                )
                If MeasurementsDAL.TryAddMeasurements(measurements) = True Then
                    Return measurements
                Else
                    Return Nothing
                End If
            End If

            Return Nothing

        End Function


        Public Function UploadResult(ByVal ProjectRouteName As String, ByVal MonitorLocationRouteName As String, ByVal MeasurementFileId As Integer)

            If Not UAL.CanUploadMeasurements Then Return New HttpUnauthorizedResult()

            Dim project = MeasurementsDAL.GetProject(ProjectShortName:=ProjectRouteName.FromRouteName)
            If project Is Nothing Then Return New HttpNotFoundResult()
            If Not CanAccessProject(project.Id) Then Return New HttpUnauthorizedResult()

            Dim measurementFile = MeasurementsDAL.GetMeasurementFile(MeasurementFileId)
            If measurementFile.MonitorLocation.getRouteName <> MonitorLocationRouteName Then Return New HttpNotFoundResult()
            If measurementFile.MonitorLocation.Project.getRouteName <> ProjectRouteName Then Return New HttpNotFoundResult()

            Dim viewModel As New UploadResultViewModel With {
                .MeasurementFile = measurementFile,
                .NavigationButtons = getResultNavigationButtons(measurementFile:=measurementFile)
            }
            setResultLinks()
            Return View("UploadResult", viewModel)

        End Function
        Private Sub setResultLinks()

            ViewData("ShowMonitorLink") = UAL.CanViewMonitorDetails
            ViewData("ShowMonitorLocationLink") = UAL.CanViewMonitorLocationDetails

        End Sub
        Private Function getResultNavigationButtons(measurementFile As MeasurementFile) As IEnumerable(Of NavigationButtonViewModel)

            Dim buttons As New List(Of NavigationButtonViewModel)

            buttons.Add(measurementFile.MonitorLocation.getDetailsRouteButton(ButtonText:="Back to Monitor Location"))
            buttons.Add(measurementFile.getDetailsRouteButton64(ButtonText:="View Uploaded File"))
            If UAL.CanViewMeasurementFileList Then buttons.Add(measurementFile.getIndexRouteButton64)

            Return buttons

        End Function

#Region "File Upload Control View Models"

        Private Function getNewSelectList(MeasurementMetrics As List(Of MeasurementMetric)) As SelectList

            Return New SelectList(MeasurementMetrics, "Id", "MetricName")

        End Function

#Region "Noise"

        Private Function getUploadRionLeqLiveWebsystemViewModel(MeasurementMetrics As List(Of MeasurementMetric))

            Return New UploadRionLeqLiveWebsystemViewModel With {
                .LpMapping = New MetricMapping("Lp", Nothing, MeasurementMetrics),
                .LeqMapping = New MetricMapping("Leq", Nothing, MeasurementMetrics),
                .LmaxMapping = New MetricMapping("Lmax", Nothing, MeasurementMetrics),
                .LminMapping = New MetricMapping("Lmin", Nothing, MeasurementMetrics),
                .ln1Mapping = New MetricMapping("ln1", Nothing, MeasurementMetrics),
                .ln2Mapping = New MetricMapping("ln2", Nothing, MeasurementMetrics),
                .ln3Mapping = New MetricMapping("ln3", Nothing, MeasurementMetrics),
                .ln4Mapping = New MetricMapping("ln4", Nothing, MeasurementMetrics),
                .ln5Mapping = New MetricMapping("ln5", Nothing, MeasurementMetrics)
            }

        End Function
        Private Function getUploadRionRCDSViewModel(MeasurementMetrics As List(Of MeasurementMetric))

            Dim LAeqMappingId, LAmaxMappingId, LAminMappingId, LA05MappingId, LA10MappingId, LA50MappingId, LA90MappingId, LA95MappingId As Integer

            Try
                LAeqMappingId = MeasurementsDAL.GetMeasurementMetric("LAeq, T").Id
            Catch ex As Exception
                LAeqMappingId = Nothing
            End Try
            Try
                LAmaxMappingId = MeasurementsDAL.GetMeasurementMetric("LAmax Fast, T").Id
            Catch ex As Exception
                LAmaxMappingId = Nothing
            End Try
            Try
                LAminMappingId = MeasurementsDAL.GetMeasurementMetric("LAmin Fast, T").Id
            Catch ex As Exception
                LAminMappingId = Nothing
            End Try
            Try
                LA05MappingId = MeasurementsDAL.GetMeasurementMetric("LA05 Fast, T").Id
            Catch ex As Exception
                LA05MappingId = Nothing
            End Try
            Try
                LA10MappingId = MeasurementsDAL.GetMeasurementMetric("LA10 Fast, T").Id
            Catch ex As Exception
                LA10MappingId = Nothing
            End Try
            Try
                LA50MappingId = MeasurementsDAL.GetMeasurementMetric("LA50 Fast, T").Id
            Catch ex As Exception
                LA50MappingId = Nothing
            End Try
            Try
                LA90MappingId = MeasurementsDAL.GetMeasurementMetric("LA90 Fast, T").Id
            Catch ex As Exception
                LA90MappingId = Nothing
            End Try
            Try
                LA95MappingId = MeasurementsDAL.GetMeasurementMetric("LA95 Fast, T").Id
            Catch ex As Exception
                LA95MappingId = Nothing
            End Try

            Return New UploadRionRCDSViewModel With {
                .LAeqMapping = New MetricMapping("LAeq", LAeqMappingId, MeasurementMetrics),
                .LAEMapping = New MetricMapping("LAE", Nothing, MeasurementMetrics),
                .LAmaxMapping = New MetricMapping("LAmax", LAmaxMappingId, MeasurementMetrics),
                .LAminMapping = New MetricMapping("LAmin", LAminMappingId, MeasurementMetrics),
                .LA05Mapping = New MetricMapping("LA05", LA05MappingId, MeasurementMetrics),
                .LA10Mapping = New MetricMapping("LA10", LA10MappingId, MeasurementMetrics),
                .LA50Mapping = New MetricMapping("LA50", LA50MappingId, MeasurementMetrics),
                .LA90Mapping = New MetricMapping("LA90", LA90MappingId, MeasurementMetrics),
                .LA95Mapping = New MetricMapping("LA95", LA95MappingId, MeasurementMetrics),
                .LCpkMapping = New MetricMapping("LCpk", Nothing, MeasurementMetrics)
            }

        End Function
        Private Function getUploadSPLTrackViewModel(MeasurementMetrics As List(Of MeasurementMetric))

            Return New UploadSPLTrackViewModel With {
                .PeriodLAeqMapping = New MetricMapping("Period LAeq", Nothing, MeasurementMetrics),
                .LAmaxMapping = New MetricMapping("LAmax", Nothing, MeasurementMetrics),
                .L10Mapping = New MetricMapping("L10", Nothing, MeasurementMetrics),
                .L90Mapping = New MetricMapping("L90", Nothing, MeasurementMetrics)
            }

        End Function

#End Region

#Region "Vibration"

        Private Function getUploadVibraViewModel(MeasurementMetrics As List(Of MeasurementMetric))

            Return New UploadVibraViewModel With {
                .PPVXChannelMapping = New MetricMapping("X [mm/s]", Nothing, MeasurementMetrics),
                .PPVYChannelMapping = New MetricMapping("Y [mm/s]", Nothing, MeasurementMetrics),
                .PPVZChannelMapping = New MetricMapping("Z [mm/s]", Nothing, MeasurementMetrics),
                .DominantFrequencyComponentXChannelMapping = New MetricMapping("X [Hz]", Nothing, MeasurementMetrics),
                .DominantFrequencyComponentYChannelMapping = New MetricMapping("Y [Hz]", Nothing, MeasurementMetrics),
                .DominantFrequencyComponentZChannelMapping = New MetricMapping("Z [Hz]", Nothing, MeasurementMetrics)
            }


        End Function
        Private Function getUploadSigicomVibrationViewModel(MeasurementMetrics As List(Of MeasurementMetric))

            Return New UploadSigicomVibrationViewModel With {
                .PPVXChannelMapping = New MetricMapping("PPV X Channel", Nothing, MeasurementMetrics),
                .PPVYChannelMapping = New MetricMapping("PPV Y Channel", Nothing, MeasurementMetrics),
                .PPVZChannelMapping = New MetricMapping("PPV Z Channel", Nothing, MeasurementMetrics)
            }

        End Function
        Private Function getUploadRedboxViewModel(MeasurementMetrics As List(Of MeasurementMetric))

            Return New UploadRedboxViewModel With {
                .XMapping = New MetricMapping("PPV X Channel", Nothing, MeasurementMetrics),
                .YMapping = New MetricMapping("PPV Y Channel", Nothing, MeasurementMetrics),
                .ZMapping = New MetricMapping("PPV Z Channel", Nothing, MeasurementMetrics)
            }

        End Function
        Private Function getUploadPPVLiveViewModel(MeasurementMetrics As List(Of MeasurementMetric))

            Return New UploadPPVLiveViewModel With {
                .XcvMapping = New MetricMapping("X_cv", Nothing, MeasurementMetrics),
                .YcvMapping = New MetricMapping("Y_cv", Nothing, MeasurementMetrics),
                .ZcvMapping = New MetricMapping("Z_cv", Nothing, MeasurementMetrics),
                .XcfMapping = New MetricMapping("X_cf", Nothing, MeasurementMetrics),
                .YcfMapping = New MetricMapping("Y_cf", Nothing, MeasurementMetrics),
                .ZcfMapping = New MetricMapping("Z_cf", Nothing, MeasurementMetrics),
                .XuMapping = New MetricMapping("X_u", Nothing, MeasurementMetrics),
                .YuMapping = New MetricMapping("Y_u", Nothing, MeasurementMetrics),
                .ZuMapping = New MetricMapping("Z_u", Nothing, MeasurementMetrics)
            }

        End Function

#End Region

#Region "Air Quality, Dust and Meteorological"

        Private Function getUploadOsirisViewModel(MeasurementMetrics As List(Of MeasurementMetric))

            Return New UploadOsirisViewModel With {
                .TotalParticlesMapping = New MetricMapping("Total Particles (ug/m^3)", Nothing, MeasurementMetrics),
                .PM10ParticlesMapping = New MetricMapping("PM10 particles (ug/m^3)", Nothing, MeasurementMetrics),
                .PM2point5ParticlesMapping = New MetricMapping("PM2.5 particles (ug/m^3)", Nothing, MeasurementMetrics),
                .PM1ParticlesMapping = New MetricMapping("PM1 particles (ug/m^3)", Nothing, MeasurementMetrics),
                .TemperatureMapping = New MetricMapping("Temperature (Celcius)", Nothing, MeasurementMetrics),
                .HumidityMapping = New MetricMapping("Humidity (% RH)", Nothing, MeasurementMetrics),
                .WindSpeedMapping = New MetricMapping("Wind Speed (mtr/sec)", Nothing, MeasurementMetrics),
                .WindDirectionMapping = New MetricMapping("Wind Direction (degrees)", Nothing, MeasurementMetrics)
            }

        End Function

#End Region

#Region "General"

        Private Function getUploadSpreadsheetTemplateViewModel()

            Dim durationFieldSettingList As New SelectList(
                {New With {.DurationFieldSettingValue = "Duration", .Text = "Duration"},
                 New With {.DurationFieldSettingValue = "EndDateTime", .Text = "End Date Time"}},
                "DurationFieldSettingValue", "Text"
            )

            Return New UploadSpreadsheetTemplateViewModel With {
                .DurationFieldSettingValue = "",
                .DurationFieldSettingList = durationFieldSettingList
            }

        End Function

#End Region

#End Region


#End Region


    End Class

End Namespace