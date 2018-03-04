Imports System.Data.Entity.Core
Imports libSEC
Public Class MonitorsController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Index"

    Function Index() As ActionResult

        If Not UAL.CanViewMonitorList Then Return New HttpUnauthorizedResult()

        Return View(getViewMonitorsViewModel)

    End Function

    Private Function getViewMonitorsViewModel(Optional searchText As String = "",
                                              Optional measurementTypeId As Integer = 0) As ViewMonitorsViewModel

        Dim monitors = MeasurementsDAL.GetMonitors
        Dim st = LCase(searchText)

        ' Filter by search text
        If searchText <> "" Then
            monitors = monitors.Where(
                Function(m) LCase(m.Category).Contains(st) Or
                            LCase(m.Manufacturer).Contains(st) Or
                            LCase(m.MeasurementType.MeasurementTypeName).Contains(st) Or
                            LCase(m.Model).Contains(st) Or
                            LCase(m.MonitorName).Contains(st) Or
                            LCase(m.OwnerOrganisation.FullName).Contains(st) Or
                            LCase(m.SerialNumber).Contains(st)
                )
        End If

        ' Filter by MeasurementType
        If measurementTypeId > 0 Then
            monitors = monitors.Where(
                Function(m) m.MeasurementTypeId = measurementTypeId
            )
        End If


        setIndexLinks()

        Return New ViewMonitorsViewModel With {
            .Monitors = monitors.ToList,
            .TableId = "monitors-table",
            .UpdateTableRouteName = "MonitorUpdateIndexTableRoute",
            .ObjectName = "Monitor",
            .ObjectDisplayName = "Monitor",
            .NavigationButtons = getIndexNavigationButtons(),
            .MeasurementTypeId = 0,
            .MeasurementTypeList = MeasurementsDAL.GetMeasurementTypesSelectList(True)
            }

    End Function

    Private Sub setIndexLinks()

        ViewData("ShowMonitorLinks") = UAL.CanViewMonitorDetails
        ViewData("ShowDeleteMonitorLinks") = UAL.CanDeleteMonitors

    End Sub
    Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

        Dim buttons = New List(Of NavigationButtonViewModel)

        If UAL.CanCreateMonitors Then buttons.Add(GetCreateButton64("Monitor"))

        Return buttons

    End Function

    <HttpGet()> _
    Public Function UpdateIndexTable(SearchText As String, MeasurementTypeId As String) As PartialViewResult

        Dim mtid As Integer = 0
        If MeasurementTypeId <> "" Then mtid = CInt(MeasurementTypeId)

        Return PartialView("Index_Table",
                           getViewMonitorsViewModel(searchText:=SearchText,
                                                    measurementTypeId:=mtid).Monitors)

    End Function

#End Region

#Region "Details"

    Function Details(ByVal MonitorRouteName As String) As ActionResult

        If Not UAL.CanViewMonitorDetails Then Return New HttpUnauthorizedResult()

        Dim monitor As Monitor = MeasurementsDAL.GetMonitor(MonitorRouteName.FromRouteName)

        If IsNothing(monitor) Then
            Return HttpNotFound()
        End If

        Dim vm As New MonitorDetailsViewModel With {
            .Monitor = monitor,
            .Tabs = getDetailsTabs(monitor),
            .NavigationButtons = getDetailsNavigationButtons(monitor)
        }

        setDetailsLinks()

        Return View(vm)

    End Function

    Private Sub setDetailsLinks()

        ViewData("ShowOwnerOrganisationLink") = UAL.CanViewOrganisationDetails
        ViewData("ShowCalibrationCertificateLinks") = UAL.CanViewDocumentDetails
        ViewData("ShowCurrentLocationLink") = UAL.CanViewMonitorLocationDetails
        ViewData("ShowDeploymentRecordLinks") = UAL.CanViewMonitorDeploymentRecordDetails

    End Sub

    Private Function getDetailsNavigationButtons(monitor As Monitor) As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons = New List(Of NavigationButtonViewModel)

        If UAL.CanViewMonitorList Then buttons.Add(monitor.getIndexRouteButton64)
        If UAL.CanEditMonitors Then buttons.Add(monitor.getEditRouteButton64)
        If monitor.canBeDeleted = True And UAL.CanDeleteMonitors Then buttons.Add(monitor.getDeleteRouteButton64)

        Return buttons

    End Function

    Private Function getDetailsTabs(monitor As Monitor) As IEnumerable(Of TabViewModel)

        Dim tabs As New List(Of TabViewModel)

        ' Basic Details
        tabs.Add(TabViewModel.getDetailsTab("Basic Details", "Monitors", monitor))

        ' Current Status
        If monitor.CurrentStatus IsNot Nothing Then
            tabs.Add(TabViewModel.getDetailsTab("Current Status", "Monitors", monitor))
        End If

        ' Current Location
        If monitor.CurrentLocation IsNot Nothing Then
            tabs.Add(TabViewModel.getDetailsTab("Current Location", "Monitors", monitor))
        End If

        ' Calibration Certificates
        If monitor.getCalibrationCertificates.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Calibration Certificates", "Monitors", monitor))
        End If

        ' Deployment Records
        If monitor.DeploymentRecords.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Deployment Records", "Monitors", monitor))
        End If

        ' Photos
        If monitor.getPhotos.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Photos", "Monitors", monitor))
        End If

        Return tabs

    End Function

#End Region

#Region "Create"

    <HttpGet()> _
    Public Function Create() As ActionResult

        If Not UAL.CanCreateMonitors Then Return New HttpUnauthorizedResult()

        Return View(getCreateMonitorViewModel)

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ByVal ViewModel As CreateMonitorViewModel) As ActionResult

        If Not UAL.CanCreateMonitors Then Return New HttpUnauthorizedResult()

        ' Check that MonitorName does not already exist
        Dim existingMonitorNames = MeasurementsDAL.GetMonitors().Select(
            Function(m) m.MonitorName.ToRouteName().ToLower()
        ).ToList()
        If existingMonitorNames.Contains(ViewModel.Monitor.MonitorName.ToRouteName().ToLower()) Then
            ModelState.AddModelError("Monitor.MonitorName", "Monitor Name already exists!")
        End If

        ' Clean up model state
        ModelState.Remove("Monitor.MeasurementType")
        ModelState.Remove("Monitor.OwnerOrganisation")

        If ModelState.IsValid Then
            ' Attach Relations
            ViewModel.Monitor.MeasurementType = MeasurementsDAL.GetMeasurementType(ViewModel.MeasurementTypeId)
            ViewModel.Monitor.OwnerOrganisation = MeasurementsDAL.GetOrganisation(ViewModel.OwnerOrganisationId)
            ' Add a new Status to the Monitor
            Dim monStatus = New MonitorStatus With {
                .IsOnline = False, .PowerStatusOk = True, .StatusComment = ""
            }
            ViewModel.Monitor.CurrentStatus = monStatus
            ' Add Monitor to database
            MeasurementsDAL.AddMonitor(ViewModel.Monitor)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.MonitorRouteName = ViewModel.Monitor.getRouteName})
        End If

        Dim vm = getCreateMonitorViewModel(
                monitor:=ViewModel.Monitor,
                measurementTypeId:=ViewModel.MeasurementTypeId,
                ownerOrganisationId:=ViewModel.OwnerOrganisationId
            )
        Return View(vm)

    End Function
    Private Function getCreateMonitorViewModel(Optional monitor As Monitor = Nothing,
                                       Optional measurementTypeId As Integer = Nothing,
                                       Optional ownerOrganisationId As Integer = Nothing) As CreateMonitorViewModel

        If monitor Is Nothing Then
            monitor = New Monitor()
        End If

        Dim vm As New CreateMonitorViewModel With {
            .Monitor = monitor,
            .MeasurementTypeId = measurementTypeId,
            .MeasurementTypeList = New SelectList(MeasurementsDAL.GetMeasurementTypes, "Id", "MeasurementTypeName"),
            .OwnerOrganisationId = ownerOrganisationId,
            .OwnerOrganisationList = New SelectList(MeasurementsDAL.GetOrganisations, "Id", "FullName")
        }

        Return vm

    End Function

#End Region

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(ByVal MonitorRouteName As String) As ActionResult

        If Not UAL.CanEditMonitors Then Return New HttpUnauthorizedResult()

        Dim Monitor As Monitor = MeasurementsDAL.GetMonitor(MonitorRouteName.FromRouteName)
        If IsNothing(Monitor) Then
            Return HttpNotFound()
        End If

        Return View(getEditMonitorViewModel(Monitor))

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditMonitorViewModel) As ActionResult

        ' Check Permissions
        If Not UAL.CanEditMonitors Then Return New HttpUnauthorizedResult()

        ' Check the monitor name, if it already exists, only exists for the monitor being edited
        Dim monitorWithName = MeasurementsDAL.GetMonitor(ViewModel.Monitor.MonitorName)
        If monitorWithName IsNot Nothing AndAlso monitorWithName.Id <> ViewModel.Monitor.Id Then
            ViewData("MonitorNameExists") = ViewModel.Monitor.MonitorName
            Dim vm = getEditMonitorViewModel(
                Monitor:=ViewModel.Monitor,
                measurementTypeId:=ViewModel.MeasurementTypeId,
                ownerOrganisationId:=ViewModel.OwnerOrganisationId
            )
            Return View(vm)
        End If

        ModelState.Remove("Monitor.MeasurementType")
        ModelState.Remove("Monitor.OwnerOrganisation")
        ModelState.Remove("CurrentStatus.Monitor")

        If ModelState.IsValid Then
            ' Attach Relations
            ViewModel.Monitor.MeasurementType = MeasurementsDAL.GetMeasurementType(ViewModel.MeasurementTypeId)
            ViewModel.Monitor.OwnerOrganisation = MeasurementsDAL.GetOrganisation(ViewModel.OwnerOrganisationId)
            ' Update Monitor
            MeasurementsDAL.UpdateMonitor(ViewModel.Monitor)
            ' Update Status
            If ViewModel.CurrentStatus IsNot Nothing Then
                ViewModel.CurrentStatus.Monitor = ViewModel.Monitor
                MeasurementsDAL.UpdateMonitorStatus(ViewModel.CurrentStatus)
            End If
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.MonitorRouteName = ViewModel.Monitor.getRouteName})
        End If

        Return View(getEditMonitorViewModel(ViewModel.Monitor))

    End Function

    Private Function getEditMonitorViewModel(ByVal Monitor As Monitor,
                                             Optional measurementTypeId As Integer = 0,
                                             Optional ownerOrganisationId As Integer = 0)

        If measurementTypeId = 0 Then
            measurementTypeId = Monitor.MeasurementTypeId
        End If
        If ownerOrganisationId = 0 Then
            ownerOrganisationId = Monitor.OwnerOrganisationId
        End If

        Return New EditMonitorViewModel With {
            .Monitor = Monitor,
            .CurrentStatus = Monitor.CurrentStatus,
            .MeasurementTypeId = measurementTypeId,
            .MeasurementTypeList = New SelectList(MeasurementsDAL.GetMeasurementTypes, "Id", "MeasurementTypeName", measurementTypeId),
            .OwnerOrganisationId = ownerOrganisationId,
            .OwnerOrganisationList = New SelectList(MeasurementsDAL.GetOrganisations, "Id", "FullName", ownerOrganisationId)
        }

    End Function

#End Region

#Region "Delete"

    <HttpPost()> _
    Public Function DeleteMonitor(MonitorId As Integer) As ActionResult

        If Not UAL.CanDeleteMonitors Then Return New HttpUnauthorizedResult()

        Dim Monitor = MeasurementsDAL.GetMonitor(MonitorId)
        If Monitor Is Nothing Then Return Nothing
        MeasurementsDAL.DeleteMonitor(MonitorId)
        Return Json(New With {.redirectToUrl = Url.Action("Index")})

    End Function

#End Region

End Class