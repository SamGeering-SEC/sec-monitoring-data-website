Imports System.Data.Entity.Core
Imports libSEC
Imports DotNet.Highcharts
Imports DotNet.Highcharts.Options
Imports System.Globalization

Namespace Controllers
    Public Class MeasurementCommentsController

        Inherits ControllerBase

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)

        End Sub

#Region "Index"

        Public Function Index(ProjectRouteName As String, MonitorLocationRouteName As String) As ActionResult

            If Not UAL.CanViewMeasurementCommentList Then Return New HttpUnauthorizedResult()

            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(
                ProjectRouteName.FromRouteName, MonitorLocationRouteName.FromRouteName
            )

            If monitorLocation Is Nothing Then Return HttpNotFound()
            If monitorLocation.hasMeasurements = False Then Return HttpNotFound()

            ' Get Comments
            Dim measurementComments = MeasurementsDAL.GetMeasurementComments().Where(
                Function(mc) mc.MonitorLocationId = monitorLocation.Id
            )

            ' Get Measurement Ranges
            Dim firstMeasurementStartDateTime As Date = monitorLocation.getFirstMeasurementStartDateTime
            Dim lastMeasurementEndDateTime As Date = monitorLocation.getLastMeasurementEndDateTime

            Dim lastMeasurementEndOADateTime As Double = lastMeasurementEndDateTime.ToOADate
            Dim intMeasurementRangeEndDate As Integer = (
                Int(lastMeasurementEndOADateTime) +
                If(Int(lastMeasurementEndOADateTime) < lastMeasurementEndOADateTime, 1, 0)
            )
            Dim measurementRangeEndDate As Date = Date.FromOADate(intMeasurementRangeEndDate)

            Dim intMeasurementRangeStartDate As Integer = intMeasurementRangeEndDate - 1
            Dim measurementRangeStartDate As Date = Date.FromOADate(intMeasurementRangeStartDate)

            Dim intFirstMeasurementDate As Integer = Int(firstMeasurementStartDateTime.ToOADate)
            Dim firstMeasurementStartDate = Date.FromOADate(intFirstMeasurementDate)
            Dim intLastMeasurementDate As Integer = intMeasurementRangeEndDate
            Dim lastMeasurementEndDate = Date.FromOADate(intLastMeasurementDate)

            Dim metricIds = MeasurementsDAL.GetObjectMeasurementMetricIds(
                "MonitorLocation", monitorLocation.Id
            )
            Dim metrics = MeasurementsDAL.GetMeasurementMetrics.Where(
                Function(mm) metricIds.Contains(mm.Id)
            )

            ' Create Chart
            Dim chart = getChart(
                MonitorLocation:=monitorLocation,
                StartDate:=Date.FromOADate(CDbl(intMeasurementRangeStartDate)),
                EndDate:=Date.FromOADate(CDbl(intMeasurementRangeEndDate))
            )

            setIndexLinks()

            ' Create View Model
            Dim vm As New MeasurementCommentsIndexViewModel With {
                .Chart = chart,
                .FirstMeasurementStartDateTime = firstMeasurementStartDateTime,
                .LastMeasurementEndDateTime = lastMeasurementEndDateTime,
                .intFirstMeasurementStartDate = intFirstMeasurementDate,
                .intLastMeasurementEndDate = intLastMeasurementDate,
                .FirstMeasurementStartDate = firstMeasurementStartDate,
                .LastMeasurementEndDate = lastMeasurementEndDate,
                .intMeasurementRangeStartDate = intMeasurementRangeStartDate,
                .intMeasurementRangeEndDate = intMeasurementRangeEndDate,
                .MeasurementRangeStartDate = measurementRangeStartDate,
                .MeasurementRangeEndDate = measurementRangeEndDate,
                .Metrics = metrics,
                .MonitorLocation = monitorLocation,
                .MeasurementComments = measurementComments.ToList,
                .NavigationButtons = getIndexNavigationButtons(monitorLocation)
            }

            Return View(vm)

        End Function

        Private Sub setIndexLinks()

            ViewData("ShowMeasurementCommentTypeLinks") = UAL.CanViewMeasurementCommentTypeDetails
            ViewData("ShowContactLinks") = UAL.CanViewContactDetails
            ViewData("ShowDeleteCommentLinks") = UAL.CanDeleteMeasurementComments

        End Sub
        Private Function getIndexNavigationButtons(monitorLocation As MonitorLocation) As List(Of NavigationButtonViewModel)

            Dim buttons = New List(Of NavigationButtonViewModel)

            If UAL.CanViewMonitorLocationDetails Then buttons.Add(monitorLocation.getDetailsRouteButton("Back to Monitor Location"))
            If UAL.CanViewMonitorLocationDetails Then buttons.Add(monitorLocation.Project.getDetailsRouteButton64("Back to Project"))
            If UAL.CanViewMeasurementCommentTypeList Then buttons.Add(GetIndexButton64("MeasurementCommentType", "types-button-64"))

            Return buttons

        End Function

        Private Function getChart(MonitorLocation As MonitorLocation, StartDate As Date, EndDate As Date) As Highcharts

            ' Get Measurements
            Dim rmp As New ReadMeasurementParameters With {
                .StartDate = StartDate, .EndDate = EndDate,
                .MonitorLocation = MonitorLocation
            }
            Dim allMeasurements = MeasurementsDAL.ReadMeasurements(rmp).ToList

            If allMeasurements.Count = 0 Then Return Nothing

            Dim metrics = allMeasurements.Select(Function(m) m.MeasurementMetric).Distinct.ToList

            ' Create Chart
            Dim chart As New Highcharts("MeasurementsChart")
            chart.InitChart(GetInitValues) _
                 .SetOptions(GetOptions) _
                 .SetSubtitle(GetSubTitle) _
                 .SetLegend(GetLegend) _
                 .SetTooltip(GetToolTip) _
                 .SetLabels(GetLabels) _
                 .SetPlotOptions(GetPlotOptions)
            chart.SetExporting(GetExporting("Measurement Comments Chart"))
            chart.SetTitle(GetTitle(""))
            chart.SetYAxis(GetYAxis("Level"))
            chart.SetCredits(GetCredits("", ""))

            Dim comments = MeasurementsDAL.GetMeasurementComments.Where(
                Function(c) c.MonitorLocationId = MonitorLocation.Id
            ).ToList
            Dim tickIntervalMillis = GetTickIntervalMillis(StartDate, EndDate)
            chart.SetXAxis(
                GetDateTimeAxis("Date and Time", StartDate, EndDate, tickIntervalMillis, comments)
            )

            Dim seriesList As New List(Of Series)
            For Each metric In metrics
                Dim metricMeasurements = allMeasurements.Where(Function(m) m.MeasurementMetricId = metric.Id).ToList
                ' Downsample points so graph loads speedily
                metricMeasurements.DownSample(500)
                ' Can alternatively use New Series with {.PlotOptionsSeries = New PlotOptionsSeries With {.TurboThreshold = 0}}
                ' N.B. default turbo threshold is 1000 but converting to stepline doubles number of points
                seriesList.Add(
                    New Series With {
                        .Data = metricMeasurements.GetStepLineSeriesData(metric.RoundingDecimalPlaces),
                        .Name = metric.MetricName,
                        .Type = Enums.ChartTypes.Line
                    }
                )
            Next

            chart.SetSeries(seriesList.ToArray)

            Return chart

        End Function

        <HttpGet()> _
        Public Function UpdateCommentIndexView(ByVal MonitorLocationId As Integer,
                                        ByVal strStartDate As String, ByVal strEndDate As String) As PartialViewResult

            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(MonitorLocationId)
            Dim startDate = Date.ParseExact(strStartDate, "dd-MMM-yy", CultureInfo.InvariantCulture)
            Dim endDate = Date.ParseExact(strEndDate, "dd-MMM-yy", CultureInfo.InvariantCulture)
            Dim vm As New UpdateMeasurementCommentsIndexViewModel With {
                .chart = getChart(monitorLocation, startDate, endDate),
                .MonitorLocation = monitorLocation,
                .StartDate = startDate,
                .EndDate = endDate
            }

            Return PartialView(vm)

        End Function

#End Region

#Region "Create"

        <HttpGet()> _
        Public Function Create(ProjectRouteName As String,
                        MonitorLocationRouteName As String,
                        StartDateCode As String, EndDateCode As String) As ActionResult

            If Not UAL.CanCreateMeasurementComments Then Return New HttpUnauthorizedResult()

            ' Get Monitor Location
            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(
                ProjectRouteName.FromRouteName, MonitorLocationRouteName.FromRouteName
            )
            If monitorLocation Is Nothing Then Return HttpNotFound()

            ' Assign Dates and Times
            Dim startDate, endDate, startTime, endTime As Date
            Try
                startDate = Date.ParseExact(StartDateCode, "yyyyMMdd", CultureInfo.InvariantCulture)
                endDate = Date.ParseExact(EndDateCode, "yyyyMMdd", CultureInfo.InvariantCulture)
                startTime = New Date(startDate.Year, startDate.Month, startDate.Day).TimeOnly
                endTime = New Date(endDate.Year, endDate.Month, endDate.Day).TimeOnly
            Catch ex As Exception
                Return HttpNotFound()
            End Try

            ' Get Metrics
            Dim rmp As New ReadMeasurementParameters With {
                .StartDate = startDate.AddDays(-1),
                .EndDate = endDate.AddDays(1),
                .MonitorLocation = monitorLocation
            }
            Dim metrics = MeasurementsDAL.GetMeasurementMetrics(rmp)
            If metrics Is Nothing Then Return HttpNotFound()
            Dim availableMeasurementMetrics = metrics.Select(Function(m) New SelectableMeasurementMetric(m)).ToList
            Dim selectedMeasurementMetrics = metrics.Select(Function(m) New SelectableMeasurementMetric(m)).ToList

            Dim viewModel As New CreateMeasurementCommentViewModel With {
                .MeasurementComment = New MeasurementComment,
                .CommentTypeId = Nothing,
                .CommentTypeList = New SelectList(MeasurementsDAL.GetMeasurementCommentTypes, "Id", "CommentTypeName"),
                .MonitorLocationId = monitorLocation.Id,
                .CommentStartDate = startDate,
                .CommentStartTime = startTime,
                .CommentEndDate = endDate,
                .CommentEndTime = endTime,
                .AvailableMeasurementMetrics = availableMeasurementMetrics,
                .SelectedMeasurementMetrics = selectedMeasurementMetrics
            }

            Return View(viewModel)

        End Function
        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Create(ByVal ViewModel As CreateMeasurementCommentViewModel) As ActionResult

            If Not UAL.CanCreateMeasurementComments Then Return New HttpUnauthorizedResult()

            ModelState.Remove("MeasurementComment.CommentType")

            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(ViewModel.MonitorLocationId)

            If ModelState.IsValid Then
                ' Attach Relations
                ViewModel.MeasurementComment.CommentType = MeasurementsDAL.GetMeasurementCommentType(ViewModel.CommentTypeId)
                ' Add other properties
                ViewModel.MeasurementComment.MonitorLocationId = ViewModel.MonitorLocationId
                ' Add Associated Measurements
                Dim associatedMeasurementList As New List(Of Measurement)
                For Each metricId In ViewModel.PostedMeasurementMetrics.MeasurementMetricIds
                    Dim rmp As New ReadMeasurementParameters With {
                        .MonitorLocation = monitorLocation,
                        .StartDate = ViewModel.CommentStartDate.AddDays(ViewModel.CommentStartTime.TimeOnly.ToOADate),
                        .EndDate = ViewModel.CommentEndDate.AddDays(ViewModel.CommentEndTime.TimeOnly.ToOADate),
                        .Metric = MeasurementsDAL.GetMeasurementMetric(Convert.ToInt32(metricId))
                    }
                    associatedMeasurementList.AddRange(MeasurementsDAL.ReadMeasurements(rmp).ToList)
                Next
                ViewModel.MeasurementComment.Measurements = associatedMeasurementList

                ViewModel.MeasurementComment.CommentDateTime = DateTime.Now
                ViewModel.MeasurementComment.ContactId = CurrentContact.Id
                ViewModel.MeasurementComment.FirstMeasurementStartDateTime = ViewModel.CommentStartDate.AddDays(ViewModel.CommentStartTime.TimeOnly.ToOADate)
                ViewModel.MeasurementComment.LastMeasurementEndDateTime = ViewModel.CommentEndDate.AddDays(ViewModel.CommentEndTime.TimeOnly.ToOADate)
                ' Add Measurement Comment to database
                MeasurementsDAL.AddMeasurementComment(ViewModel.MeasurementComment)
                ' Redirect to Details
                Return RedirectToAction(
                    "Index",
                    New With {.ProjectRouteName = monitorLocation.Project.getRouteName,
                              .MonitorLocationRouteName = monitorLocation.getRouteName}
                )
            End If

            Return View(ViewModel)

        End Function

#End Region

#Region "Delete"

        <HttpPost()> _
        Public Function DeleteMeasurementComment(MeasurementCommentId As Integer,
                                                 ShowMeasurementCommentTypeLinks As Boolean,
                                                 ShowContactLinks As Boolean,
                                                 ShowDeleteCommentLinks As Boolean) As ActionResult

            If Not UAL.CanDeleteMeasurementComments Then Return New HttpUnauthorizedResult()

            Dim MeasurementComment = MeasurementsDAL.GetMeasurementComment(MeasurementCommentId)
            If MeasurementComment Is Nothing Then Return Nothing

            Dim MonitorLocationId = MeasurementComment.MonitorLocationId

            MeasurementsDAL.DeleteMeasurementComment(MeasurementCommentId)

            Dim comments = MeasurementsDAL.GetMeasurementComments.Where(
                Function(mc) mc.MonitorLocationId = MonitorLocationId
            ).ToList

            ViewData("ShowMeasurementCommentTypeLinks") = ShowMeasurementCommentTypeLinks
            ViewData("ShowContactLinks") = ShowContactLinks
            ViewData("ShowDeleteCommentLinks") = ShowDeleteCommentLinks

            Return PartialView("Index_CommentsTable", comments)

        End Function

#End Region


    End Class

End Namespace