Imports System.Data.Entity.Core
Imports libSEC
Imports DotNet.Highcharts
Imports DotNet.Highcharts.Options
Imports PagedList

Public Class MeasurementFilesController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Index"

    Public Function Index() As ActionResult

        If Not UAL.CanViewMeasurementFileList Then Return New HttpNotFoundResult()

        Return View(getViewMeasurementFilesViewModel)

    End Function
    Public Function IndexAfterDeleteSuccess() As ActionResult

        If Not UAL.CanViewMeasurementFileList Then Return New HttpNotFoundResult()

        Return View(getViewMeasurementFilesViewModel)

    End Function
    Public Function IndexAfterDeleteFailure() As ActionResult

        If Not UAL.CanViewMeasurementFileList Then Return New HttpNotFoundResult()

        Return View(getViewMeasurementFilesViewModel)

    End Function

    Private Function getViewMeasurementFilesViewModel(Optional searchText As String = "",
                                                      Optional measurementTypeId As Integer = 0,
                                                      Optional projectId As Integer = 0,
                                                      Optional monitorLocationId As Integer = 0,
                                                      Optional initialLoad As Boolean = True) As ViewMeasurementFilesViewModel

        Dim measurementFiles = MeasurementsDAL.GetMeasurementFiles

        If (searchText = "" And
            measurementTypeId <= 0 And projectId <= 0 And monitorLocationId <= 0 And
            initialLoad = True) Then

            measurementFiles = New List(Of MeasurementFile)

        Else

            ' Filter by search text
            Dim st = LCase(searchText)
            If searchText <> "" Then
                measurementFiles = measurementFiles.Where(
                    Function(mf) LCase(mf.Contact.ContactName).Contains(st) Or _
                                 LCase(mf.MeasurementFileName).Contains(st) Or _
                                 LCase(mf.MeasurementFileType.FileTypeName).Contains(st) Or _
                                 LCase(mf.Monitor.MonitorName).Contains(st) Or _
                                 LCase(mf.MonitorLocation.MonitorLocationName).Contains(st)
                )
            End If

            ' Filter by MeasurementType
            If measurementTypeId > 0 Then
                measurementFiles = measurementFiles.Where(
                    Function(mf) mf.MeasurementFileType.MeasurementTypeId = measurementTypeId
                )
            End If

            ' Filter by Project
            If projectId > 0 Then
                measurementFiles = measurementFiles.Where(
                    Function(mf) mf.MonitorLocation.ProjectId = projectId
                )
            End If

            ' Filter by Monitor Location
            If monitorLocationId > 0 Then
                measurementFiles = measurementFiles.Where(
                    Function(mf) mf.MonitorLocationId = monitorLocationId
                )
            End If

        End If

        setIndexLinks()

        ' Create SelectLists
        Dim projectList = MeasurementsDAL.GetProjectsSelectList(
            Include0AsAll:=True,
            MeasurementTypeId:=measurementTypeId,
            projectIds:=AllowedProjectIds
        )
        Dim monitorLocationsSelectList = MeasurementsDAL.GetMonitorLocationsSelectList(
            Include0AsAll:=True,
            MeasurementTypeId:=measurementTypeId,
            ProjectId:=projectId
        )

        Dim measurementFileList = measurementFiles.ToList()
        ' Filter by allowed projects
        If CurrentUser.UserAccessLevel.ProjectPermission.PermissionName <> "AllProjects" Then
            Dim newMeasurementFileList As New List(Of MeasurementFile)
            Dim projectIds = AllowedProjectIds()
            For Each measurementFile In measurementFileList
                If projectIds.Contains(measurementFile.MonitorLocation.ProjectId) Then
                    newMeasurementFileList.Add(measurementFile)
                End If
            Next
            measurementFileList = newMeasurementFileList
        End If

        Return New ViewMeasurementFilesViewModel With {
            .MeasurementFiles = measurementFileList,
            .TableId = "measurementfiles-table",
            .UpdateTableRouteName = "MeasurementFileUpdateIndexTableRoute",
            .ObjectName = "MeasurementFile",
            .ObjectDisplayName = "Measurement File",
            .MeasurementTypeId = measurementTypeId,
            .MeasurementTypeList = MeasurementsDAL.GetMeasurementTypesSelectList(True),
            .ProjectId = projectId,
            .ProjectList = projectList,
            .MonitorLocationId = monitorLocationId,
            .MonitorLocationList = monitorLocationsSelectList,
            .UpdateProjectsRouteName = "MeasurementFileIndexUpdateProjectsRoute",
            .UpdateMonitorLocationsRouteName = "MeasurementFileIndexUpdateMonitorLocationsRoute"
        }

    End Function

    Private Sub setIndexLinks()

        ViewData("ShowMeasurementFileLinks") = UAL.CanViewMeasurementFileDetails
        ViewData("ShowMonitorLinks") = UAL.CanViewMonitorDetails
        ViewData("ShowMonitorLocationLinks") = UAL.CanViewMonitorLocationDetails
        ViewData("ShowContactLinks") = UAL.CanViewContactDetails
        ViewData("ShowDeleteMeasurementFileLinks") = UAL.CanDeleteMeasurementFiles

    End Sub

    <HttpGet()> _
    Public Function UpdateIndexTable(SearchText As String, MeasurementTypeId As String,
                                     ProjectId As String, MonitorLocationId As String) As PartialViewResult

        Dim mtid As Integer = If(MeasurementTypeId <> "", CInt(MeasurementTypeId), 0)
        Dim pid As Integer = If(ProjectId <> "" And ProjectId <> "null", CInt(ProjectId), 0)
        Dim mlid As Integer = If(MonitorLocationId <> "" And MonitorLocationId <> "null", CInt(MonitorLocationId), 0)

        Return PartialView(
            "Index_Table",
            getViewMeasurementFilesViewModel(
                searchText:=SearchText,
                measurementTypeId:=mtid,
                projectId:=pid,
                monitorLocationId:=mlid,
                initialLoad:=False
            ).MeasurementFiles
        )

    End Function

    <HttpPost()> _
    Public Function GetProjectsSelectList(MeasurementTypeId As Integer) As JsonResult

        Return Json(
            MeasurementsDAL.GetProjectsSelectList(Include0AsAll:=True, MeasurementTypeId:=MeasurementTypeId, projectIds:=AllowedProjectIds)
        )

    End Function

    <HttpPost()> _
    Public Function GetMonitorLocationsSelectList(MeasurementTypeId As Integer, ProjectId As Integer) As JsonResult

        Return Json(
            MeasurementsDAL.GetMonitorLocationsSelectList(True, MeasurementTypeId, ProjectId)
        )

    End Function

#End Region

#Region "Details"

    <HttpGet()> _
    Public Function Details(MeasurementFileId As Integer) As ActionResult

        If Not UAL.CanViewMeasurementFileDetails Then Return New HttpUnauthorizedResult()

        Dim MeasurementFile = MeasurementsDAL.GetMeasurementFile(MeasurementFileId)
        If IsNothing(MeasurementFile) Then
            Return HttpNotFound()
        End If

        Dim vm = getMeasurementFileDetailsViewModel(MeasurementFile.Id, 1, 20, "Start Date Time")

        Return View(vm)

    End Function

    Private Function getMeasurementFileDetailsViewModel(MeasurementFileId As Integer, PageNumber As Integer, PageSize As Integer, SortBy As String) As MeasurementFileDetailsViewModel

        Dim measurementFile = MeasurementsDAL.GetMeasurementFile(MeasurementFileId)

        Dim numMeasurements As Integer = measurementFile.NumberOfMeasurements
        Dim numPages As Integer = If(
            CDbl(numMeasurements) / PageSize = CDbl(numMeasurements / PageSize),
            numMeasurements / PageSize,
            (numMeasurements / PageSize) + 1
        )
        Dim iFirst = (PageNumber - 1) * PageSize
        Dim iLast = Math.Min(PageNumber * PageSize, numMeasurements - 1)

        Dim measurements As New List(Of Measurement)
        Select Case SortBy
            Case "Start Date Time"
                measurements = MeasurementsDAL.GetObjectMeasurements("MeasurementFile", measurementFile.Id, "StartDateTime")
            Case "Duration"
                measurements = MeasurementsDAL.GetObjectMeasurements("MeasurementFile", measurementFile.Id, "Duration")
            Case "Level"
                measurements = MeasurementsDAL.GetObjectMeasurements("MeasurementFile", measurementFile.Id, "Level")
            Case "Metric"
                Dim metricIds = MeasurementsDAL.GetObjectMeasurementMetricIds("MeasurementFile", measurementFile.Id)
                Dim orderedMetricIds = MeasurementsDAL.GetMeasurementMetrics.Where(
                    Function(mm) metricIds.Contains(mm.Id)
                ).OrderBy(
                    Function(mm) mm.MetricName
                ).Select(Function(mm) mm.Id).ToList
                measurements = MeasurementsDAL.GetObjectMeasurements(
                    ObjectName:="MeasurementFile", ObjectId:=measurementFile.Id,
                    OrderBy:="MeasurementMetricId", SortOrder:=orderedMetricIds
                )
        End Select
        measurements = measurements.GetRange(iFirst, iLast - iFirst + 1)

        ' Attach Measurement Metrics
        Dim measurementMetrics = MeasurementsDAL.GetMeasurementMetrics.ToList()
        For Each m In measurements
            m.MeasurementMetric = measurementMetrics.Single(Function(mm) mm.Id = m.MeasurementMetricId)
        Next

        setDetailsLinks()

        Dim vm = New MeasurementFileDetailsViewModel With {
            .MeasurementFile = measurementFile,
            .NumPages = numPages,
            .PageNumber = PageNumber,
            .PageSize = PageSize,
            .Measurements = measurements,
            .NumMeasurements = numMeasurements,
            .SortBy = SortBy,
            .SortByList = New SelectList({"Start Date Time", "Duration", "Level", "Metric"}),
            .NavigationButtons = getDetailsNavigationButtons(measurementFile)
        }

        Return vm

    End Function

    Private Sub setDetailsLinks()

        ViewData("ShowMonitorLink") = UAL.CanViewMonitorDetails
        ViewData("ShowMonitorLocationLink") = UAL.CanViewMonitorLocationDetails
        ViewData("ShowContactLink") = UAL.CanViewContactDetails
        ViewData("ShowDeleteMeasurementLinks") = UAL.CanDeleteMeasurements

    End Sub
    Private Function getDetailsNavigationButtons(measurementFile As MeasurementFile) As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons As New List(Of NavigationButtonViewModel)

        If UAL.CanViewMeasurementFileList Then buttons.Add(measurementFile.getIndexRouteButton64)
        If measurementFile.canBeDeleted = True And UAL.CanDeleteMeasurementFiles Then buttons.Add(measurementFile.getDeleteRouteButton64)

        Return buttons

    End Function

    <HttpGet()> _
    <ChildActionOnly> _
    Public Function Details_Graph(MeasurementFileId As Integer) As PartialViewResult

        Dim measurementFile = MeasurementsDAL.GetMeasurementFile(MeasurementFileId)

        Dim measurements = MeasurementsDAL.GetObjectMeasurements("MeasurementFile", measurementFile.Id, "StartDateTime")
        Dim startDateTime = measurementFile.FirstMeasurementStartDateTime
        Dim endDateTime = measurementFile.LastMeasurementStartDateTime.AddDays(measurementFile.LastMeasurementDuration)

        Dim metricIds = measurements.Select(Function(m) m.MeasurementMetricId).Distinct.ToList

        ' Create view data for chart
        Dim chart As New Highcharts("MeasurementsChart")
        chart.InitChart(GetInitValues(Width:=800)) _
             .SetOptions(GetOptions) _
             .SetSubtitle(GetSubTitle) _
             .SetLegend(GetLegend) _
             .SetTooltip(GetToolTip) _
             .SetLabels(GetLabels) _
             .SetPlotOptions(GetPlotOptions)
        chart.SetTitle(GetTitle("Measurements imported from File: " + measurementFile.MeasurementFileName))
        Dim tickIntervalMillis = GetTickIntervalMillis(startDateTime, endDateTime)
        chart.SetXAxis(GetDateTimeAxis("Date and Time", startDateTime, endDateTime, tickIntervalMillis))
        chart.SetYAxis(GetYAxis("Level"))
        chart.SetCredits(GetCredits("", ""))

        Dim seriesList As New List(Of Series)
        For Each metricId In metricIds
            Dim metric = MeasurementsDAL.GetMeasurementMetric(metricId)
            Dim metricName = metric.MetricName
            Dim metricMeasurements = measurements.Where(Function(m) m.MeasurementMetricId = metricId).ToList
            seriesList.Add(
                New Series With {
                    .Data = metricMeasurements.GetStepLineSeriesData(metric.RoundingDecimalPlaces),
                    .Name = metricName,
                    .Type = Enums.ChartTypes.Line,
                    .PlotOptionsSeries = New PlotOptionsSeries With {.TurboThreshold = 0}
                }
            )
        Next

        chart.SetSeries(seriesList.ToArray)

        Return PartialView(chart)

    End Function

    Public Function UpdateDetailsTable(MeasurementFileId As Integer, PageNumber As Integer, PageSize As Integer, SortBy As String) As PartialViewResult

        Dim measurementFile = MeasurementsDAL.GetMeasurementFile(MeasurementFileId)
        Return PartialView(
            "Details_Table",
            getMeasurementFileDetailsViewModel(
                MeasurementFileId, PageNumber, PageSize, SortBy
            )
        )

    End Function

#End Region

#Region "Delete"

    <HttpPost()>
    Public Function DeleteMeasurementFile(MeasurementFileId As Integer) As ActionResult

        If Not UAL.CanDeleteMeasurementFiles Then Return New HttpUnauthorizedResult()

        Dim MeasurementFile = MeasurementsDAL.GetMeasurementFile(MeasurementFileId)
        If MeasurementFile Is Nothing Then Return Nothing
        Dim success = MeasurementsDAL.DeleteMeasurementFile(MeasurementFileId)
        If success = True Then
            Return Json(New With {.redirectToUrl = Url.Action("IndexAfterDeleteSuccess")})
        Else
            Return Json(New With {.redirectToUrl = Url.Action("IndexAfterDeleteFailure")})
        End If

    End Function

    <HttpPost()> _
    Public Function DeleteMeasurement(MeasurementFileId As Integer, MeasurementId As Integer) As ActionResult

        If Not UAL.CanDeleteMeasurements Then Return New HttpUnauthorizedResult()

        Dim result = MeasurementsDAL.DeleteMeasurement(MeasurementId)
        If result = False Then Return Nothing

        Dim measurementFile = MeasurementsDAL.GetMeasurementFile(MeasurementFileId:=MeasurementFileId)
        measurementFile.NumberOfMeasurements -= 1
        MeasurementsDAL.UpdateMeasurementFileMeasurementCount(measurementFile)

        Return Nothing

    End Function

#End Region



End Class