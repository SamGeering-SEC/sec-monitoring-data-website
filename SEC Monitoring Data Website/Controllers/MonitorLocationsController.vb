Imports System.Data.Entity.Core
Imports libSEC

Namespace SEC_Monitoring_Data_Website
    Public Class MonitorLocationsController
        Inherits ControllerBase

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)

        End Sub

#Region "Index"

        Function Index() As ActionResult

            If Not UAL.CanViewMonitorLocationList Then Return New HttpUnauthorizedResult()

            Return View(getViewMonitorLocationsViewModel)

        End Function

        Private Function getViewMonitorLocationsViewModel(Optional searchText As String = "",
                                                          Optional measurementTypeId As Integer = 0) As ViewMonitorLocationsViewModel

            Dim monitorlocations = MeasurementsDAL.GetMonitorLocations

            monitorlocations = monitorlocations.Where(Function(ml) AllowedProjectIds.Contains(ml.Project.Id))

            Dim st = LCase(searchText)

            ' Filter by search text
            If searchText <> "" Then
                monitorlocations = monitorlocations.Where(
                    Function(ml) LCase(ml.MeasurementType.MeasurementTypeName).Contains(st) Or
                                 LCase(ml.MonitorLocationName).Contains(st) Or
                                 LCase(ml.Project.FullName).Contains(st)
                )
            End If

            ' Filter by MeasurementType
            If measurementTypeId > 0 Then
                monitorlocations = monitorlocations.Where(
                    Function(ml) ml.MeasurementTypeId = measurementTypeId
                )
            End If

            setIndexLinks()

            Return New ViewMonitorLocationsViewModel With {
                .MonitorLocations = monitorlocations.ToList,
                .TableId = "monitorlocations-table",
                .UpdateTableRouteName = "MonitorLocationUpdateIndexTableRoute",
                .ObjectName = "MonitorLocation",
                .ObjectDisplayName = "Monitor Location",
                .MeasurementTypeId = 0,
                .MeasurementTypeList = MeasurementsDAL.GetMeasurementTypesSelectList(True)
            }

        End Function

        Private Sub setIndexLinks()

            ViewData("ShowMonitorLocationLinks") = UAL.CanViewMonitorLocationDetails
            ViewData("ShowDeleteMonitorLocationLinks") = UAL.CanDeleteMonitorLocations

        End Sub

        <HttpGet()> _
        Public Function UpdateIndexTable(SearchText As String, MeasurementTypeId As String) As PartialViewResult

            Dim mtid As Integer = 0
            If MeasurementTypeId <> "" Then mtid = CInt(MeasurementTypeId)

            Return PartialView("Index_Table",
                               getViewMonitorLocationsViewModel(searchText:=SearchText,
                                                                measurementTypeId:=mtid).MonitorLocations)

        End Function

        <ActionName("Select")> _
        Public Function Choose(ByVal ProjectRouteName As String) As ActionResult

            If (Not CanAccessProject(ProjectRouteName) Or
                Not UAL.CanViewSelectMonitorLocations) Then Return New HttpNotFoundResult()

            Dim project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            If project Is Nothing Then Return HttpNotFound()

            Dim monitorlocations As IEnumerable(Of MonitorLocation) = project.MonitorLocations.OrderBy(
                Function(ml) ml.MeasurementType.MeasurementTypeName
            ).OrderBy(
                Function(ml) ml.MonitorLocationName
            )

            Dim vm As New SelectMonitorLocationViewModel With {
                .MonitorLocations = monitorlocations,
                .NavigationButtons = getSelectNavigationButtons(project)
            }

            setSelectLinks()

            Return View(vm)

        End Function

        Private Sub setSelectLinks()

            ViewData("ShowMonitorLocationLinks") = UAL.CanViewMonitorLocationDetails
            ViewData("ShowMeasurementsLinks") = UAL.CanViewMeasurements
            ViewData("ShowAssessmentLinks") = UAL.CanViewAssessments

        End Sub
        Private Function getSelectNavigationButtons(project As Project) As List(Of NavigationButtonViewModel)

            Dim buttons = New List(Of NavigationButtonViewModel)

            If UAL.CanViewProjectList Then buttons.Add(GetIndexButton64("Project", "index-button-64"))
            If UAL.CanViewProjectDetails Then buttons.Add(project.getDetailsRouteButton64(ButtonText:="Back to Project"))

            Return buttons

        End Function

#End Region

#Region "Details"

        Public Function Details(ProjectRouteName As String, MonitorLocationRouteName As String) As ActionResult

            If (Not CanAccessProject(ProjectRouteName) Or
                Not UAL.CanViewMonitorLocationDetails) Then Return New HttpUnauthorizedResult()

            Dim ProjectShortName = ProjectRouteName.FromRouteName
            Dim MonitorLocationName = MonitorLocationRouteName.FromRouteName
            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(ProjectShortName, MonitorLocationName)
            If IsNothing(monitorLocation) Then
                Return HttpNotFound()
            End If
            Return View(getMonitorLocationDetailsViewModel(monitorLocation, ProjectRouteName))

        End Function

        Private Sub setDetailsLinks(monitorLocation As MonitorLocation)

            ViewData("ShowAssessmentCriterionGroupLinks") = UAL.CanViewAssessmentCriteria
            ViewData("ShowProjectLink") = UAL.CanViewProjectDetails
            ViewData("ShowCalibrationCertificateLinks") = UAL.CanViewDocumentDetails
            If Not IsNothing(monitorLocation.CurrentMonitor) Then
                ViewData("ShowCurrentMonitorLink") = UAL.CanViewMonitorDetails
                ViewData("ShowCurrentMonitorOwnerOrganisationLink") = UAL.CanViewOrganisationDetails
            End If
            ViewData("ShowDeploymentRecordLinks") = UAL.CanViewMonitorDeploymentRecordDetails

        End Sub

        Private Function getMonitorLocationDetailsViewModel(monitorLocation As MonitorLocation, ProjectRouteName As String)

            setDetailsLinks(monitorLocation)

            Return New MonitorLocationDetailsViewModel With {
                .MonitorLocation = monitorLocation,
                .Tabs = getDetailsTabs(monitorLocation),
                .NavigationButtons = getDetailsNavigationButtons(monitorLocation)
            }

        End Function

        Private Function getDetailsNavigationButtons(monitorLocation As MonitorLocation) As IEnumerable(Of NavigationButtonViewModel)

            Dim hasMeasurements = MeasurementsDAL.ObjectHasMeasurements("MonitorLocation", monitorLocation.Id)
            Dim hasMeasurementViews = monitorLocation.Project.MeasurementViews.Count > 0

            ' Create Navigation Buttons
            Dim buttons As New List(Of NavigationButtonViewModel)
            If hasMeasurements Then
                If hasMeasurementViews And UAL.CanViewMeasurementViewDetails Then buttons.Add(monitorLocation.getMeasurementsViewRouteButton64)
                If UAL.CanViewMeasurementCommentList Then buttons.Add(monitorLocation.getMeasurementCommentIndexRouteButton64)
                If monitorLocation.AssessmentCriteria.Count > 0 And UAL.CanViewAssessments Then buttons.Add(monitorLocation.getAssessmentViewRouteButton64)
            End If

            If monitorLocation.CurrentMonitor IsNot Nothing And UAL.CanUploadMeasurements Then buttons.Add(monitorLocation.getMeasurementUploadRouteButton64)
            If UAL.CanViewSelectMonitorLocations Then buttons.Add(
                New NavigationButtonViewModel(
                    Text:="View Project Monitor Locations",
                    RouteName:="MonitorLocationSelectRoute",
                    RouteValues:=New With {.ProjectRouteName = monitorLocation.Project.getRouteName},
                    ButtonClass:="sitewide-button-64 index-button-64"
                )
            )
            If UAL.CanEditMonitorLocations Then buttons.Add(monitorLocation.getEditRouteButton64)
            If monitorLocation.canBeDeleted = True And UAL.CanDeleteMonitorLocations Then buttons.Add(monitorLocation.getDeleteRouteButton64)

            Return buttons

        End Function

        Private Function getDetailsTabs(monitorLocation As MonitorLocation) As IEnumerable(Of TabViewModel)

            Dim tabs As New List(Of TabViewModel)

            ' Basic Details
            tabs.Add(TabViewModel.getDetailsTab("Basic Details", "MonitorLocations", monitorLocation))

            ' Monitor Locations
            If monitorLocation.getPhotos.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Photos", "MonitorLocations", monitorLocation))
            End If

            ' Current Monitor
            Dim currentMonitor = monitorLocation.CurrentMonitor
            If currentMonitor IsNot Nothing Then
                tabs.Add(TabViewModel.getDetailsTab("Monitor I.D.", "CurrentMonitor", "MonitorLocations", currentMonitor))
            End If

            ' Assessment Criteria
            If monitorLocation.getAssessmentCriterionGroups.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Assessment Criteria", "MonitorLocations", monitorLocation))
            End If

            ' Calibration Certificates
            If monitorLocation.getCalibrationCertificates.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Calibration Certificates", "MonitorLocations", monitorLocation))
            End If

            ' Deployment Records
            If monitorLocation.DeploymentRecords.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Deployment Records", "MonitorLocations", monitorLocation))
            End If

            Return tabs

        End Function

#End Region

#Region "Create"

        Public Function Create(ProjectRouteName As String) As ActionResult

            If (Not CanAccessProject(ProjectRouteName) Or
                Not UAL.CanCreateMonitorLocations) Then Return New HttpUnauthorizedResult()

            Dim Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            If Project Is Nothing Then Return HttpNotFound()

            Dim vm = getCreateMonitorLocationViewModel(project:=Project)

            Return View(vm)

        End Function
        <HttpPost()> _
        Public Function Create(ByVal ViewModel As CreateMonitorLocationViewModel) As ActionResult

            If (Not CanAccessProject(ViewModel.Project.Id) Or
                Not UAL.CanCreateMonitorLocations) Then Return New HttpUnauthorizedResult()

            ' Check that MonitorLocationName does not already exist in the Project
            Dim existingMonitorLocationNames = MeasurementsDAL.GetMonitorLocationsForProject(ViewModel.Project.Id).Select(
                Function(m) m.MonitorLocationName.ToRouteName().ToLower()
            ).ToList()
            If existingMonitorLocationNames.Contains(ViewModel.MonitorLocation.MonitorLocationName.ToRouteName().ToLower()) Then
                ModelState.AddModelError("MonitorLocation.MonitorLocationName", "Monitor Location Name already exists in this Project!")
            End If

            ' Clean up Model State
            ModelState.Remove("MonitorLocation.MeasurementType")
            ModelState.Remove("Project.FullName")
            ModelState.Remove("Project.ShortName")
            ModelState.Remove("Project.ProjectNumber")
            ModelState.Remove("Project.ClientOrganisation")
            ModelState.Remove("Project.Country")
            ModelState.Remove("Project.ProjectGeoCoords")
            ModelState.Remove("Project.StandardWeeklyWorkingHours")

            If ModelState.IsValid Then
                Dim Project = MeasurementsDAL.GetProject(ViewModel.Project.Id)
                ViewModel.MonitorLocation.Project = Project
                ViewModel.MonitorLocation.MeasurementTypeId = ViewModel.MeasurementTypeId
                MeasurementsDAL.AddMonitorLocation(ViewModel.MonitorLocation)
                Return RedirectToRoute("ProjectEditRoute", New With {.ProjectRouteName = Project.getRouteName})
            End If

            Dim vm = getCreateMonitorLocationViewModel(
                project:=ViewModel.Project,
                monitorLocation:=ViewModel.MonitorLocation,
                measurementTypeId:=ViewModel.MeasurementTypeId
            )
            Return View(vm)

        End Function

        Private Function getCreateMonitorLocationViewModel(project As Project,
                                                           Optional monitorLocation As MonitorLocation = Nothing,
                                                           Optional measurementTypeId As Integer = Nothing) As CreateMonitorLocationViewModel

            If monitorLocation Is Nothing Then
                monitorLocation = New MonitorLocation(project)
            End If

            Dim vm As New CreateMonitorLocationViewModel With {
                .MonitorLocation = monitorLocation,
                .MeasurementTypeId = measurementTypeId,
                .MeasurementTypeList = New SelectList(MeasurementsDAL.GetMeasurementTypes, "Id", "MeasurementTypeName"),
                .Project = project
            }

            Return vm

        End Function

#End Region

#Region "Edit"

        Public Function Edit(ProjectRouteName As String, MonitorLocationRouteName As String) As ActionResult

            If (Not CanAccessProject(ProjectRouteName) Or
                Not UAL.CanEditMonitorLocations) Then Return New HttpUnauthorizedResult()

            Dim Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            If Project Is Nothing Then Return HttpNotFound()
            Dim MonitorLocation = MeasurementsDAL.GetMonitorLocation(
                ProjectRouteName.FromRouteName, MonitorLocationRouteName.FromRouteName
            )
            If MonitorLocation Is Nothing Then Return HttpNotFound()

            Dim vm As New EditMonitorLocationViewModel With {
                .Project = Project, .MonitorLocation = MonitorLocation
            }

            Return View(vm)

        End Function
        <HttpPost()> _
        Public Function Edit(ByVal ViewModel As EditMonitorLocationViewModel) As ActionResult

            If (Not CanAccessProject(ViewModel.Project.Id) Or
                Not UAL.CanEditMonitorLocations) Then Return New HttpUnauthorizedResult()

            ModelState.Remove("Project.FullName")
            ModelState.Remove("Project.ShortName")
            ModelState.Remove("Project.ProjectNumber")
            ModelState.Remove("Project.ClientOrganisation")
            ModelState.Remove("Project.Country")
            ModelState.Remove("Project.ProjectGeoCoords")
            ModelState.Remove("Project.StandardWeeklyWorkingHours")
            ModelState.Remove("MonitorLocation.MeasurementType")

            Dim Project = MeasurementsDAL.GetProject(ViewModel.Project.Id)

            If ModelState.IsValid Then
                ViewModel.MonitorLocation.Project = Project
                MeasurementsDAL.UpdateMonitorLocation(ViewModel.MonitorLocation)
                Return RedirectToRoute(
                    "MonitorLocationDetailsRoute",
                    New With {.ProjectRouteName = Project.getRouteName,
                              .MonitorLocationRouteName = ViewModel.MonitorLocation.getRouteName}
                )
            End If

            ViewModel.Project = Project

            Return View(ViewModel)

        End Function

        Public Function UpdateEditAssessmentCriteria(MonitorLocationId As Integer) As PartialViewResult

            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(MonitorLocationId)
            Dim project = MeasurementsDAL.GetProject(monitorLocation.ProjectId)

            Dim vm As New EditMonitorLocationViewModel With {.Project = project,
                                                             .MonitorLocation = monitorLocation}

            Return PartialView("Edit_AssessmentCriteria", vm)

        End Function

#End Region

#Region "Delete"

        <HttpPost()> _
        Public Function DeleteMonitorLocation(MonitorLocationId As Integer) As ActionResult

            Dim MonitorLocation = MeasurementsDAL.GetMonitorLocation(MonitorLocationId)

            If (Not UAL.CanDeleteMonitorLocations Or
                Not AllowedProjectIds.Contains(MonitorLocation.ProjectId)) Then Return New HttpUnauthorizedResult()

            If MonitorLocation Is Nothing Then Return Nothing
            MeasurementsDAL.DeleteMonitorLocation(MonitorLocationId)
            Return Json(New With {.redirectToUrl = Url.Action("Index")})

        End Function
        <HttpPost()> _
        Public Function DeleteAssessmentCriterionGroup(MonitorLocationId As Integer, AssessmentCriterionGroupId As Integer) As ActionResult

            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(MonitorLocationId)

            If (Not UAL.CanDeleteAssessmentCriteria Or
                Not CanAccessProject(monitorLocation.ProjectId)) Then Return New HttpUnauthorizedResult()

            Dim assessmentCriteriaIds = MeasurementsDAL.GetAssessmentCriteria().Where(
                Function(ac) ac.AssessmentCriterionGroupId = AssessmentCriterionGroupId And
                             ac.MonitorLocationId = MonitorLocationId
            ).Select(Function(ac) ac.Id).ToList

            For Each id In assessmentCriteriaIds
                MeasurementsDAL.DeleteAssessmentCriterion(id)
            Next

            Return Json(New With {.redirectToUrl = Url.Action("Index")})

        End Function

#End Region

    End Class

End Namespace