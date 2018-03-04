Imports System.Data.Entity.Core
Imports libSEC

Public Class MonitorDeploymentRecordsController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Index"

    Public Function Index() As ActionResult

        If Not UAL.CanViewMonitorDeploymentRecordList Then Return New HttpNotFoundResult()

        Return View(getViewMonitorDeploymentRecordsViewModel)

    End Function
    Private Function getViewMonitorDeploymentRecordsViewModel(Optional searchText As String = "",
                                                              Optional measurementTypeId As Integer = 0,
                                                              Optional projectId As Integer = 0,
                                                              Optional monitorLocationId As Integer = 0) As ViewMonitorDeploymentRecordsViewModel

        Dim projects = AllowedProjects()
        Dim projectIds = projects.Select(Function(p) p.Id).ToList
        Dim monitorlocations = MeasurementsDAL.GetMonitorLocations().Where(Function(ml) ml.ProjectId)


        ' Get Monitor Deployment Records from database
        Dim deploymentRecords = MeasurementsDAL.GetMonitorDeploymentRecords().Where(
            Function(mdr) projectIds.Contains(mdr.MonitorLocation.ProjectId)
                )
        Dim st = LCase(searchText)

        ' Filter by search text
        If searchText <> "" Then deploymentRecords = deploymentRecords.Where(
            Function(mdr) LCase(mdr.Monitor.MonitorName).Contains(st) Or
                          LCase(mdr.MonitorLocation.MonitorLocationName).Contains(st) Or
                          LCase(mdr.MonitorLocation.Project.FullName).Contains(st) Or
                          LCase(mdr.MonitorLocation.Project.ShortName).Contains(st)
            )

        ' Filter by MeasurementType
        If measurementTypeId > 0 Then
            deploymentRecords = deploymentRecords.Where(
                Function(mdr) mdr.MonitorLocation.MeasurementTypeId = measurementTypeId
                )
        End If

        ' Filter by Project
        If projectId > 0 Then
            deploymentRecords = deploymentRecords.Where(
                Function(mdr) mdr.MonitorLocation.ProjectId = projectId
                )
        End If

        ' Filter by Monitor Location
        If monitorLocationId > 0 Then
            deploymentRecords = deploymentRecords.Where(
                Function(mdr) mdr.MonitorLocationId = monitorLocationId
                )
        End If

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

        setIndexLinks()

        Dim vm As New ViewMonitorDeploymentRecordsViewModel With {
            .MonitorDeploymentRecords = deploymentRecords.ToList,
            .SearchText = searchText,
            .TableId = "monitor-deployment-records-table",
            .UpdateTableRouteName = "MonitorDeploymentRecordUpdateIndexTableRoute",
            .ObjectName = "MonitorDeploymentRecord",
            .ObjectDisplayName = "Monitor Deployment Record",
            .MeasurementTypeId = measurementTypeId,
            .MeasurementTypeList = MeasurementsDAL.GetMeasurementTypesSelectList(True),
            .ProjectId = projectId,
            .ProjectList = projectList,
            .MonitorLocationId = monitorLocationId,
            .MonitorLocationList = monitorLocationsSelectList,
            .UpdateProjectsRouteName = "MonitorDeploymentRecordIndexUpdateProjectsRoute",
            .UpdateMonitorLocationsRouteName = "MonitorDeploymentRecordIndexUpdateMonitorLocationsRoute"
        }

        Return vm

    End Function

    Private Sub setIndexLinks()

        ViewData("ShowProjectLinks") = UAL.CanViewProjectDetails
        ViewData("ShowMonitorLinks") = UAL.CanViewMonitorDetails
        ViewData("ShowMonitorLocationLinks") = UAL.CanViewMonitorLocationDetails
        ViewData("ShowDeleteDeploymentRecordLinks") = UAL.CanDeleteMonitorDeploymentRecords

    End Sub

    <HttpGet()> _
    Public Function UpdateIndexTable(SearchText As String, MeasurementTypeId As String,
                                     ProjectId As String, MonitorLocationId As String) As PartialViewResult

        Dim mtid As Integer = If(MeasurementTypeId <> "", CInt(MeasurementTypeId), 0)
        Dim pid As Integer = If(ProjectId <> "" And ProjectId <> "null", CInt(ProjectId), 0)
        Dim mlid As Integer = If(MonitorLocationId <> "" And MonitorLocationId <> "null", CInt(MonitorLocationId), 0)

        Return PartialView("Index_Table",
                           getViewMonitorDeploymentRecordsViewModel(
                               searchText:=SearchText,
                               measurementTypeId:=mtid,
                               projectId:=pid,
                               monitorLocationId:=mlid
                               ).MonitorDeploymentRecords)
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

    Public Function Details(MonitorRouteName As String, DeploymentIndex As Integer) As ActionResult

        If Not UAL.CanViewMonitorDeploymentRecordDetails Then Return New HttpUnauthorizedResult()

        Dim Monitor = MeasurementsDAL.GetMonitor(MonitorRouteName.FromRouteName)
        If Monitor Is Nothing Then Return HttpNotFound()

        Dim MonitorDeploymentRecords = MeasurementsDAL.GetMonitorDeploymentRecords(Monitor)
        If MonitorDeploymentRecords.Count = 0 Then Return HttpNotFound()

        If DeploymentIndex > MonitorDeploymentRecords.Count Then
            Return RedirectToAction(
                "Details", New With {.MonitorRouteName = MonitorRouteName,
                                     .DeploymentIndex = MonitorDeploymentRecords.Count}
            )
        End If

        If DeploymentIndex <= 0 Then
            Return RedirectToAction(
                "Details", New With {.MonitorRouteName = MonitorRouteName, .DeploymentIndex = 1}
            )
        End If

        setDetailsLinks()

        Dim monitorDeploymentRecord = MonitorDeploymentRecords(DeploymentIndex - 1)

        Dim vm = New MonitorDeploymentRecordDetailsViewModel With {
            .MonitorDeploymentRecord = monitorDeploymentRecord,
            .NavigationButtons = getDetailsNavigationButtons(monitorDeploymentRecord),
            .Tabs = getDetailsTabs(monitorDeploymentRecord)
        }

        Return View(vm)

    End Function

    Private Sub setDetailsLinks()

        ViewData("ShowProjectLink") = UAL.CanViewProjectDetails
        ViewData("ShowMonitorLink") = UAL.CanViewMonitorDetails
        ViewData("ShowMonitorLocationLink") = UAL.CanViewMonitorLocationDetails

    End Sub

    Private Function getDetailsNavigationButtons(monitorDeploymentRecord As MonitorDeploymentRecord) As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons = New List(Of NavigationButtonViewModel)

        If UAL.CanViewMonitorDeploymentRecordList Then buttons.Add(monitorDeploymentRecord.getIndexRouteButton64)
        If monitorDeploymentRecord.DeploymentEndDate Is Nothing And UAL.CanEndMonitorDeployments Then buttons.Add(monitorDeploymentRecord.getEndRouteButton64)
        If monitorDeploymentRecord.canBeDeleted = True And UAL.CanDeleteMonitorDeploymentRecords Then buttons.Add(monitorDeploymentRecord.getDeleteRouteButton64)

        Return buttons

    End Function

    Private Function getDetailsTabs(monitorDeploymentRecord As MonitorDeploymentRecord) As IEnumerable(Of TabViewModel)

        Dim tabs As New List(Of TabViewModel)

        ' Basic Details
        tabs.Add(TabViewModel.getDetailsTab("Basic Details", "MonitorDeploymentRecords", monitorDeploymentRecord))

        ' Monitor Settings
        tabs.Add(TabViewModel.getDetailsTab("Monitor Settings", "MonitorDeploymentRecords", monitorDeploymentRecord))

        Return tabs

    End Function

#End Region

#Region "Create"

    <HttpGet()> _
    Public Function CreateForMonitor(MonitorRouteName As String) As ActionResult

        If Not UAL.CanCreateDeploymentRecords Then Return New HttpUnauthorizedResult()

        Dim Monitor = MeasurementsDAL.GetMonitor(MonitorRouteName.FromRouteName)

        If Monitor.CurrentLocation IsNot Nothing Then Return RedirectToRoute(
            "MonitorDetailsRoute",
            New With {.MonitorRouteName = Monitor.getRouteName}
        )

        Dim vm = getCreateMonitorDeploymentRecordViewModel(Monitor)

        Return View(vm)

    End Function
    Public Function CreateForMonitorLocation(ProjectRouteName As String, MonitorLocationRouteName As String) As ActionResult

        If Not UAL.CanCreateDeploymentRecords Then Return New HttpUnauthorizedResult()

        Dim Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
        If Project Is Nothing Then Return HttpNotFound()

        Dim MonitorLocation As MonitorLocation = MeasurementsDAL.GetMonitorLocation(
            ProjectRouteName.FromRouteName, MonitorLocationRouteName.FromRouteName
        )
        If MonitorLocation Is Nothing Then Return HttpNotFound()

        If MonitorLocation.CurrentMonitor IsNot Nothing Then Return RedirectToRoute(
            "MonitorLocationDetailsRoute",
            New With {.MonitorLocationRouteName = MonitorLocation.getRouteName}
        )

        Dim vm = getCreateMonitorLocationDeploymentRecordViewModel(MonitorLocation)

        Return View(vm)

    End Function

    Private Function getCreateMonitorDeploymentRecordViewModel(Monitor As Monitor,
                                                               Optional validationErrors As List(Of String) = Nothing) As CreateMonitorDeploymentRecordViewModel

        Dim vm As New CreateMonitorDeploymentRecordViewModel With {
            .Monitor = Monitor,
            .MonitorId = Monitor.Id,
            .MonitorSettings = New MonitorSettings,
            .MonitorDeploymentRecord = New MonitorDeploymentRecord,
            .ProjectId = Nothing,
            .ProjectList = New SelectList(AllowedProjects(), "Id", "FullName"),
            .MonitorLocationId = Nothing,
            .MonitorLocationList = New SelectList(String.Empty, "Id", "MonitorLocationName"),
            .MeasurementPeriod = Date.Today
        }

        Select Case Monitor.MeasurementType.MeasurementTypeName
            Case "Noise"
                vm.NoiseSetting = New NoiseSetting
            Case "Vibration"
                vm.VibrationSetting = New VibrationSetting
            Case "Air Quality, Dust and Meteorological"
                vm.AirQualitySetting = New AirQualitySetting
        End Select

        Dim previousRecord As MonitorDeploymentRecord
        previousRecord = Monitor.DeploymentRecords.OrderByDescending(Function(mdr) mdr.DeploymentStartDate).FirstOrDefault()
        If previousRecord IsNot Nothing Then

            Dim previousSettings = previousRecord.MonitorSettings
            vm.MonitorSettings.AdditionalInfo1 = previousSettings.AdditionalInfo1
            vm.MonitorSettings.AdditionalInfo2 = previousSettings.AdditionalInfo2
            vm.MonitorSettings.MeasurementPeriod = previousSettings.MeasurementPeriod

            Select Case Monitor.MeasurementType.MeasurementTypeName
                Case "Noise"
                    Dim previousNoiseSetting = previousSettings.NoiseSetting
                    vm.NoiseSetting.AlarmTriggerLevel = previousNoiseSetting.AlarmTriggerLevel
                    vm.NoiseSetting.DynamicRangeLowerLevel = previousNoiseSetting.DynamicRangeLowerLevel
                    vm.NoiseSetting.DynamicRangeUpperLevel = previousNoiseSetting.DynamicRangeUpperLevel
                    vm.NoiseSetting.FrequencyWeighting = previousNoiseSetting.FrequencyWeighting
                    vm.NoiseSetting.MicrophoneSerialNumber = previousNoiseSetting.MicrophoneSerialNumber
                    vm.NoiseSetting.SoundRecording = previousNoiseSetting.SoundRecording
                    vm.NoiseSetting.TimeWeighting = previousNoiseSetting.TimeWeighting
                    vm.NoiseSetting.WindScreenCorrection = previousNoiseSetting.WindScreenCorrection
                Case "Vibration"
                    Dim previousVibrationSetting = previousSettings.VibrationSetting
                    vm.VibrationSetting.AlarmTriggerLevel = previousVibrationSetting.AlarmTriggerLevel
                    vm.VibrationSetting.XChannelWeighting = previousVibrationSetting.XChannelWeighting
                    vm.VibrationSetting.YChannelWeighting = previousVibrationSetting.YChannelWeighting
                    vm.VibrationSetting.ZChannelWeighting = previousVibrationSetting.ZChannelWeighting
                Case "Air Quality, Dust and Meteorological"
                    Dim previousAirQualitySetting = previousSettings.AirQualitySetting
                    vm.AirQualitySetting.AlarmTriggerLevel = previousAirQualitySetting.AlarmTriggerLevel
                    vm.AirQualitySetting.InletHeatingOn = previousAirQualitySetting.InletHeatingOn
                    vm.AirQualitySetting.NewDailySample = previousAirQualitySetting.NewDailySample
            End Select
        End If

        setCreateLinks()

        If Not validationErrors Is Nothing Then
            vm.ValidationErrors = validationErrors
        Else
            vm.ValidationErrors = New List(Of String)
        End If

        Return vm

    End Function
    Private Function getCreateMonitorLocationDeploymentRecordViewModel(MonitorLocation As MonitorLocation,
                                                                       Optional validationErrors As List(Of String) = Nothing) As CreateMonitorDeploymentRecordViewModel

        Dim vm As New CreateMonitorDeploymentRecordViewModel With {
            .MonitorLocation = MonitorLocation,
            .MonitorId = Nothing,
            .MonitorList = New SelectList(
                MeasurementsDAL.GetMonitors.Where(
                    Function(m) m.MeasurementTypeId = MonitorLocation.MeasurementTypeId AndAlso
                                m.CurrentLocation Is Nothing
                    ), "Id", "MonitorName"
                ),
            .MonitorSettings = New MonitorSettings,
            .MonitorDeploymentRecord = New MonitorDeploymentRecord,
            .ProjectId = Nothing,
            .ProjectList = New SelectList(AllowedProjects(), "Id", "FullName"),
            .MonitorLocationId = MonitorLocation.Id,
            .MonitorLocationList = Nothing,
            .MeasurementPeriod = Date.Today
            }

        Select Case MonitorLocation.MeasurementType.MeasurementTypeName
            Case "Noise"
                vm.NoiseSetting = New NoiseSetting
            Case "Vibration"
                vm.VibrationSetting = New VibrationSetting
            Case "Air Quality, Dust and Meteorological"
                vm.AirQualitySetting = New AirQualitySetting
        End Select

        setCreateLinks()

        If Not validationErrors Is Nothing Then
            vm.ValidationErrors = validationErrors
        Else
            vm.ValidationErrors = New List(Of String)
        End If

        Return vm

    End Function

    Private Sub setCreateLinks()

        ViewData("ShowProjectLink") = UAL.CanViewProjectDetails
        ViewData("ShowMonitorLink") = UAL.CanViewMonitorDetails
        ViewData("ShowMonitorLocationLink") = UAL.CanViewMonitorLocationDetails

    End Sub

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function CreateForMonitor(ByVal ViewModel As CreateMonitorDeploymentRecordViewModel) As ActionResult

        If Not UAL.CanCreateDeploymentRecords Then Return New HttpUnauthorizedResult()

        ModelState.Remove("MonitorDeploymentRecord.Monitor")
        ModelState.Remove("MonitorDeploymentRecord.MonitorLocation")
        ModelState.Remove("MonitorDeploymentRecord.MonitorSettings")
        ModelState.Remove("MonitorSettings.Monitor")
        ModelState.Remove("Monitor")
        ModelState.Remove("Monitor.MonitorName")
        ModelState.Remove("Monitor.SerialNumber")
        ModelState.Remove("Monitor.Manufacturer")
        ModelState.Remove("Monitor.Model")
        ModelState.Remove("Monitor.Category")
        ModelState.Remove("NoiseSetting.MonitorSettings")
        ModelState.Remove("VibrationSetting.MonitorSettings")
        ModelState.Remove("AirQualitySetting.MonitorSettings")

        Dim Monitor = MeasurementsDAL.GetMonitor(ViewModel.MonitorId)
        Dim MonitorLocation = MeasurementsDAL.GetMonitorLocation(ViewModel.MonitorLocationId)

        If ModelState.IsValid Then
            Dim validationErrors = tryCreateDeploymentRecord(ViewModel, Monitor, MonitorLocation)
            If validationErrors.Count() = 0 Then
                Return RedirectToRoute(
                    "MonitorLocationDetailsRoute",
                    New With {.ProjectRouteName = MonitorLocation.Project.getRouteName,
                              .MonitorLocationRouteName = MonitorLocation.getRouteName}
                )
            End If
            Return View(getCreateMonitorDeploymentRecordViewModel(Monitor, validationErrors))

        Else

            LogModelStateErrors(ModelState)
            Return View(getCreateMonitorDeploymentRecordViewModel(Monitor))

        End If

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function CreateForMonitorLocation(ByVal ViewModel As CreateMonitorDeploymentRecordViewModel) As ActionResult

        If Not UAL.CanCreateDeploymentRecords Then Return New HttpUnauthorizedResult()

        ModelState.Remove("MonitorDeploymentRecord.Monitor")
        ModelState.Remove("MonitorDeploymentRecord.MonitorLocation")
        ModelState.Remove("MonitorDeploymentRecord.MonitorSettings")
        ModelState.Remove("MonitorSettings.Monitor")
        ModelState.Remove("MonitorSettings.MonitorLocationId")

        Dim Monitor = MeasurementsDAL.GetMonitor(ViewModel.MonitorId)
        Dim MonitorLocation = MeasurementsDAL.GetMonitorLocation(ViewModel.MonitorLocationId)

        If ModelState.IsValid Then
            Dim validationErrors = tryCreateDeploymentRecord(ViewModel, Monitor, MonitorLocation)
            If validationErrors.Count() = 0 Then
                Return RedirectToRoute(
                    "MonitorLocationDetailsRoute",
                    New With {.ProjectRouteName = MonitorLocation.Project.getRouteName,
                              .MonitorLocationRouteName = MonitorLocation.getRouteName}
                )
            End If
            Return View(getCreateMonitorLocationDeploymentRecordViewModel(MonitorLocation, validationErrors))
        End If

        Return View(getCreateMonitorLocationDeploymentRecordViewModel(MonitorLocation))

    End Function

    Private Function tryCreateDeploymentRecord(ByRef viewModel As CreateMonitorDeploymentRecordViewModel,
                                               ByRef monitor As Monitor, ByRef monitorLocation As MonitorLocation) As List(Of String)

        Dim validationErrors As New List(Of String)
        Dim numErrors As Integer = 0
        If viewModel.MeasurementPeriod.Hour = 0 And viewModel.MeasurementPeriod.Minute = 0 Then
            validationErrors.Add("MeasurementPeriod")
        End If
        Select Case monitor.MeasurementType.MeasurementTypeName
            Case "Noise"
                If viewModel.NoiseSetting.FrequencyWeighting = "" Then
                    validationErrors.Add("FrequencyWeighting")
                End If
                If viewModel.NoiseSetting.TimeWeighting = "" Then
                    validationErrors.Add("TimeWeighting")
                End If
        End Select

        If validationErrors.Count > 0 Then Return validationErrors

        Try

            ' Attach Relations
            viewModel.MonitorDeploymentRecord.Monitor = monitor
            viewModel.MonitorDeploymentRecord.MonitorLocation = monitorLocation
            ' Attach Custom Properties
            viewModel.MonitorSettings.MeasurementPeriod = viewModel.MeasurementPeriod.TimeOnly.ToOADate
            ' Attach Specific Settings to Monitor Settings
            viewModel.MonitorSettings.NoiseSetting = viewModel.NoiseSetting
            viewModel.MonitorSettings.VibrationSetting = viewModel.VibrationSetting
            viewModel.MonitorSettings.AirQualitySetting = viewModel.AirQualitySetting
            ' Attach Monitor Settings and new Status to Monitor
            'monitor.CurrentSettings = viewModel.MonitorSettings
            monitor.CurrentStatus = New MonitorStatus With {.StatusComment = "No Comments"}
            ' Link Monitor Settings and Monitor Deployment Record
            viewModel.MonitorSettings.DeploymentRecord = viewModel.MonitorDeploymentRecord
            viewModel.MonitorDeploymentRecord.MonitorSettings = viewModel.MonitorSettings
            ' Add Monitor and Monitor Location to Monitor Deployment Record
            viewModel.MonitorDeploymentRecord.Monitor = monitor
            viewModel.MonitorDeploymentRecord.MonitorLocation = monitorLocation
            ' Link Monitor and Monitor Location
            monitor.CurrentLocation = monitorLocation
            monitorLocation.CurrentMonitor = monitor
            ' Add Monitor Deployment Record to database
            MeasurementsDAL.AddMonitorDeploymentRecord(viewModel.MonitorDeploymentRecord)
            MeasurementsDAL.UpdateMonitorLocationCurrentMonitor(monitorLocation)
            MeasurementsDAL.UpdateMonitorCurrentLocation(monitor)

        Catch ex As Exception

            Debug.WriteLine(
                String.Format(
                    "Error creating Monitor Deployment Record for Monitor {0} and Monitor Location {1}",
                    monitor.MonitorName, monitorLocation.MonitorLocationName
                )
            )

        End Try

        Return validationErrors

    End Function

    Public Function GetProjectMonitorLocations(ProjectId As Integer, MeasurementTypeId As Integer) As JsonResult

        Dim ProjectMonitorLocations = MeasurementsDAL.GetMonitorLocationsForProject(ProjectId).Where(Function(ml) ml.CurrentMonitor Is Nothing AndAlso ml.MeasurementTypeId = MeasurementTypeId)
        Dim ProjectMonitorLocationSelectListItems As New List(Of SelectListItem)

        For Each ml In ProjectMonitorLocations
            ProjectMonitorLocationSelectListItems.Add(
                New SelectListItem With {.Text = ml.MonitorLocationName, .Value = ml.Id.ToString}
            )
        Next

        Return Json(New SelectList(ProjectMonitorLocationSelectListItems, "Value", "Text"))

    End Function
    Public Function GetAssessmentCriterionGroupMonitorLocations(AssessmentCriterionGroupId As Integer) As JsonResult

        Dim AssessmentCriterionGroupMonitorLocations = MeasurementsDAL.GetMonitorLocationsForAssessmentCriterionGroup(AssessmentCriterionGroupId)
        Dim AssessmentCriterionGroupMonitorLocationSelectListItems As New List(Of SelectListItem)

        For Each ml In AssessmentCriterionGroupMonitorLocations
            AssessmentCriterionGroupMonitorLocationSelectListItems.Add(
                New SelectListItem With {.Text = ml.MonitorLocationName, .Value = ml.Id.ToString}
            )
        Next

        Return Json(New SelectList(AssessmentCriterionGroupMonitorLocationSelectListItems, "Value", "Text"))

    End Function

#End Region

#Region "End"

    Public Function EndDeployment(MonitorRouteName As String) As ActionResult

        If Not UAL.CanEndMonitorDeployments Then Return New HttpUnauthorizedResult()

        Dim Monitor = MeasurementsDAL.GetMonitor(MonitorRouteName.FromRouteName)
        Dim MonitorDeploymentRecord = Monitor.getCurrentDeploymentRecord
        MonitorDeploymentRecord.DeploymentEndDate = Date.Today
        If MonitorDeploymentRecord Is Nothing Then Return RedirectToRoute(
            "MonitorDetailsRoute", New With {.MonitorRouteName = MonitorRouteName}
        )

        Return View(MonitorDeploymentRecord)

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function EndDeployment(MonitorDeploymentRecord As MonitorDeploymentRecord) As ActionResult

        If Not UAL.CanEndMonitorDeployments Then Return New HttpUnauthorizedResult()

        ModelState.Remove("Monitor")
        ModelState.Remove("MonitorLocation")
        ModelState.Remove("MonitorSettings")

        If MonitorDeploymentRecord.DeploymentEndDate Is Nothing Then
            ModelState.AddModelError(
                "DeploymentEndDate", "Please Enter a Date for the Deployment End"
            )
        End If

        If ModelState.IsValid Then
            Dim Monitor = MeasurementsDAL.GetMonitor(MonitorDeploymentRecord.MonitorId)
            Dim MonitorLocation = MeasurementsDAL.GetMonitorLocation(MonitorDeploymentRecord.MonitorLocationId)
            'Monitor.CurrentSettings = Nothing
            Monitor.CurrentLocation = Nothing
            MonitorLocation.CurrentMonitor = Nothing
            MonitorDeploymentRecord = MeasurementsDAL.UpdateMonitorDeploymentRecord(MonitorDeploymentRecord)
            MonitorLocation = MeasurementsDAL.UpdateMonitorLocationCurrentMonitor(MonitorLocation)
            Monitor = MeasurementsDAL.UpdateMonitorCurrentLocation(Monitor)
            Return RedirectToAction(
                "Details",
                New With {.MonitorRouteName = Monitor.MonitorName.ToRouteName,
                          .DeploymentIndex = MonitorDeploymentRecord.getDeploymentIndex}
            )
        End If

        Return EndDeployment(MeasurementsDAL.GetMonitor(MonitorDeploymentRecord.MonitorId).getRouteName)

    End Function

#End Region

#Region "Delete"

    <HttpPost()> _
    Public Function DeleteMonitorDeploymentRecord(MonitorDeploymentRecordId As Integer) As ActionResult

        If Not UAL.CanDeleteMonitorDeploymentRecords Then Return New HttpUnauthorizedResult()

        Dim MonitorDeploymentRecord = MeasurementsDAL.GetMonitorDeploymentRecord(MonitorDeploymentRecordId)
        If MonitorDeploymentRecord Is Nothing Then Return Nothing
        MeasurementsDAL.DeleteMonitorDeploymentRecord(MonitorDeploymentRecordId)
        Return Json(New With {.redirectToUrl = Url.Action("Index")})

    End Function

#End Region

End Class