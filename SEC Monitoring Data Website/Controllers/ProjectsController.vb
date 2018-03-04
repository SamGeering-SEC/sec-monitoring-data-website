Imports System.Data.Entity.Core
Imports libSEC

Namespace SEC_Monitoring_Data_Website

    Public Class ProjectsController
        Inherits ControllerBase

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)

        End Sub

#Region "Index"

        Public Function Index() As ActionResult

            If Not UAL.CanViewProjectList Then Return New HttpUnauthorizedResult()

            Dim vm = getViewProjectsViewModel()

            Return View(vm)

        End Function

        Private Function getViewProjectsViewModel(Optional searchText As String = "") As ViewProjectsViewModel

            Dim projects = AllowedProjects()

            Dim st = LCase(searchText)
            If searchText <> "" Then
                projects = projects.Where(
                    Function(p) LCase(p.FullName).Contains(st) Or _
                                LCase(p.ShortName).Contains(st)
                    ).ToList()
            End If

            Dim minLatitude = projects.Select(Function(p) p.ProjectGeoCoords.Latitude).ToList().Min()

            setIndexLinks()

            Return New ViewProjectsViewModel With {
                .Projects = projects,
                .NavigationButtons = getIndexNavigationButtons(),
                .TableId = "projects-table",
                .UpdateTableRouteName = "ProjectUpdateIndexTableRoute",
                .ObjectName = "Project",
                .ObjectDisplayName = "Project"
            }

        End Function

        <HttpGet()> _
        Public Function UpdateIndexTable(SearchText As String) As PartialViewResult

            Return PartialView("Index_Table",
                               getViewProjectsViewModel(SearchText).Projects)

        End Function

        Private Sub setIndexLinks()

            ViewData("ShowProjectLinks") = UAL.CanViewProjectDetails
            ViewData("ShowClientOrganisationLinks") = UAL.CanViewOrganisationDetails
            ViewData("ShowMonitorLocationsLinks") = UAL.CanViewMonitorLocationDetails
            ViewData("ShowDeleteProjectLinks") = UAL.CanDeleteProjects

        End Sub
        Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

            Dim buttons = New List(Of NavigationButtonViewModel)

            If UAL.CanCreateProjects Then buttons.Add(GetCreateButton64("Project"))

            Return buttons

        End Function

#End Region

#Region "Details"

        <HttpGet()> _
        Public Function Details(ByVal ProjectRouteName As String) As ActionResult

            If (Not CanAccessProject(ProjectRouteName) Or
                Not UAL.CanViewProjectDetails) Then Return New HttpUnauthorizedResult()

            Dim project As Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            If IsNothing(project) Then
                Return HttpNotFound()
            End If
            Return View(getProjectDetailsViewModel(project))

        End Function

        Function getProjectDetailsViewModel(ByVal project As Project) As ProjectDetailsViewModel

            setDetailsLinks(project)

            Return New ProjectDetailsViewModel With {
                .Project = project,
                .AssessmentCriteria = project.AssessmentCriteria.ToList,
                .Contacts = project.Contacts.ToList,
                .MeasurementViews = project.MeasurementViews.ToList,
                .MonitorLocations = project.MonitorLocations.ToList,
                .Organisations = project.Organisations.ToList,
                .StandardWorkingHours = project.StandardWeeklyWorkingHours,
                .Variations = project.VariedWeeklyWorkingHours.ToList,
                .NavigationButtons = getDetailsNavigationButtons(project),
                .Tabs = getDetailsTabs(project)
            }

        End Function


        Private Sub setDetailsLinks(project As Project)

            ViewData("ShowAssessmentCriterionGroupLinks") = UAL.CanViewAssessmentCriteria
            ViewData("ShowContactLinks") = UAL.CanViewContactDetails
            ViewData("ShowContactOrganisationLinks") = UAL.CanViewOrganisationDetails
            ViewData("ShowMeasurementViewLinks") = UAL.CanViewMeasurementViewDetails
            ViewData("ShowMonitorLocationLinks") = UAL.CanViewMonitorLocationDetails
            ViewData("ShowCurrentMonitorLinks") = UAL.CanViewMonitorDetails
            Dim hasMeasurementViews = project.MeasurementViews.Count > 0
            ViewData("ShowMonitorLocationMeasurementsLinks") = hasMeasurementViews And UAL.CanViewMonitorLocationDetails
            ViewData("ShowOrganisationLinks") = UAL.CanViewOrganisationDetails
            ViewData("ShowVariationLinks") = UAL.CanViewDocumentDetails

        End Sub

        Private Function getDetailsNavigationButtons(project As Project) As IEnumerable(Of NavigationButtonViewModel)

            Dim buttons As New List(Of NavigationButtonViewModel)

            If UAL.CanViewSelectMonitorLocations Then buttons.Add(
                New NavigationButtonViewModel(
                    "Select a Monitor Location",
                    "MonitorLocationSelectRoute",
                    New With {.controller = "MonitorLocations",
                            .action = "Select",
                            .ProjectRouteName = project.getRouteName},
                    "sitewide-button-64 monitorlocation-button-64"
                )
            )
            If UAL.CanViewProjectList Then buttons.Add(project.getIndexRouteButton64("Back to My Projects"))
            If UAL.CanEditProjects Then buttons.Add(project.getEditRouteButton64)
            If UAL.CanViewDocumentList Then buttons.Add(
                New NavigationButtonViewModel(
                    "View Project Documents",
                    "DocumentProjectIndexRoute",
                    New With {.ProjectShortName = project.ShortName},
                    "sitewide-button-64 documents-button-64"
                )
            )
            If project.canBeDeleted = True And UAL.CanDeleteProjects Then buttons.Add(project.getDeleteRouteButton64)

            Return buttons

        End Function

        Private Function getDetailsTabs(project As Project) As IEnumerable(Of TabViewModel)

            Dim tabs As New List(Of TabViewModel)

            ' Basic Details
            tabs.Add(TabViewModel.getDetailsTab("Basic Details", "Projects", project))

            ' Monitor Locations
            If project.MonitorLocations.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Monitor Locations", "Projects", project))
            End If

            ' Contacts
            If project.Contacts.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Contacts", "Projects", project))
            End If

            ' Working Hours
            tabs.Add(TabViewModel.getDetailsTab("Working Hours", "Projects", project))

            ' Organisations
            If project.Organisations.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Organisations", "Projects", project))
            End If

            ' Measurement Views
            If project.MeasurementViews.Count > 0 And UAL.CanViewMeasurementViewDetails Then
                tabs.Add(TabViewModel.getDetailsTab("Measurement Views", "Projects", project))
            End If

            ' Assessment Criteria
            If project.AssessmentCriteria.Count > 0 And UAL.CanViewAssessmentCriteria Then
                tabs.Add(TabViewModel.getDetailsTab("Assessment Criteria", "Projects", project))
            End If

            Return tabs

        End Function

#End Region

#Region "Create"

        <HttpGet()> _
        Public Function Create() As ActionResult

            If Not UAL.CanCreateProjects Then Return New HttpUnauthorizedResult()

            Return View(getCreateProjectViewModel())

        End Function
        Private Function getCreateProjectViewModel(Optional viewModel As CreateProjectViewModel = Nothing) As CreateProjectViewModel

            Dim project As Project
            Dim workingWeekViewModel As WorkingWeekViewModel
            Dim clientOrganisationId As Integer
            Dim countryId As Integer

            If viewModel Is Nothing Then
                project = New Project(DefaultLatitude, DefaultLongitude)
                workingWeekViewModel = New WorkingWeekViewModel
                clientOrganisationId = Nothing
                countryId = Nothing
            Else
                project = viewModel.Project
                workingWeekViewModel = viewModel.WorkingWeekViewModel
                clientOrganisationId = viewModel.ClientOrganisationId
                countryId = viewModel.CountryId
            End If

            Return New CreateProjectViewModel With {
                .Project = project,
                .ClientOrganisationId = clientOrganisationId,
                .ClientOrganisationList = New SelectList(MeasurementsDAL.GetOrganisations, "Id", "FullName"),
                .CountryId = countryId,
                .CountryList = New SelectList(MeasurementsDAL.GetCountries, "Id", "CountryName"),
                .WorkingWeekViewModel = workingWeekViewModel
            }

        End Function
        <HttpPost()> _
        Public Function Create(ByVal ViewModel As CreateProjectViewModel) As ActionResult

            If Not UAL.CanCreateProjects Then Return New HttpUnauthorizedResult()

            ' Check that ProjectName does not already exist
            Dim existingProjectNames = MeasurementsDAL.GetProjects().Select(
                Function(p) p.ShortName.ToRouteName().ToLower()
            ).ToList()
            If existingProjectNames.Contains(ViewModel.Project.ShortName.ToRouteName().ToLower()) Then
                ModelState.AddModelError("Project.ShortName", "Project Short Name already exists!")
            End If

            ModelState.Remove("Project.ClientOrganisation")
            ModelState.Remove("Project.Country")
            ModelState.Remove("Project.StandardWeeklyWorkingHours")

            If ModelState.IsValid Then
                ViewModel.Project.ClientOrganisation = MeasurementsDAL.GetOrganisation(ViewModel.ClientOrganisationId)
                ViewModel.Project.Country = MeasurementsDAL.GetCountry(ViewModel.CountryId)
                ViewModel.Project.StandardWeeklyWorkingHours = ViewModel.WorkingWeekViewModel.GetStandardWeeklyWorkingHours
                MeasurementsDAL.AddProject(ViewModel.Project)
                Return RedirectToAction("Index")
            End If

            Return View(getCreateProjectViewModel(ViewModel))

        End Function

#End Region

#Region "Edit"

        <HttpGet()> _
        Public Function Edit(ByVal ProjectRouteName As String) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            Dim project As Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            If IsNothing(project) Then
                Return HttpNotFound()
            End If

            Return View(getEditProjectViewModel(project))

        End Function
        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Edit(ByVal ViewModel As EditProjectViewModel) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ViewModel.Project.Id)) Then Return New HttpUnauthorizedResult()

            ModelState.Remove("Project.ClientOrganisation")
            ModelState.Remove("Project.Country")
            ModelState.Remove("Project.StandardWeeklyWorkingHours")

            If ModelState.IsValid Then
                ' Attach Relations
                ViewModel.Project.ClientOrganisation = MeasurementsDAL.GetOrganisation(ViewModel.ClientOrganisationId)
                ViewModel.Project.Country = MeasurementsDAL.GetCountry(ViewModel.CountryId)
                ' Update Project
                MeasurementsDAL.UpdateProject(ViewModel.Project)
                ' Redirect to Details
                Return RedirectToAction("Details", New With {.ProjectRouteName = ViewModel.Project.getRouteName})
            End If

            Return View(ViewModel)

        End Function

        Private Function getEditProjectViewModel(ByVal project As Project) As EditProjectViewModel

            setEditLinks()

            Return New EditProjectViewModel With {
                .Project = project,
                .AllContacts = MeasurementsDAL.GetContacts,
                .AllAssessmentCriteria = MeasurementsDAL.GetAssessmentCriterionGroups,
                .AllMeasurementViews = MeasurementsDAL.GetMeasurementViews,
                .AllMonitorLocations = MeasurementsDAL.GetMonitorLocations,
                .AllOrganisations = MeasurementsDAL.GetOrganisations,
                .ClientOrganisationId = project.ClientOrganisationId,
                .ClientOrganisationList = New SelectList(MeasurementsDAL.GetOrganisations, "Id", "FullName", project.ClientOrganisationId),
                .CountryId = project.CountryId,
                .CountryList = New SelectList(MeasurementsDAL.GetCountries, "Id", "CountryName", project.CountryId),
                .WorkingWeekViewModel = New WorkingWeekViewModel(project.StandardWeeklyWorkingHours)
            }

        End Function
        Private Sub setEditLinks()

            ViewData("ShowCreateAssessmentCriterionGroupLink") = UAL.CanViewAssessmentCriteria
            ViewData("ShowEditAssessmentCriterionGroupLinks") = UAL.CanEditAssessmentCriteria
            ViewData("ShowDeleteAssessmentCriterionGroupLinks") = UAL.CanDeleteAssessmentCriteria
            ViewData("ShowCreateMonitorLocationLink") = UAL.CanCreateMonitorLocations
            ViewData("ShowEditWorkingHoursLink") = UAL.CanEditProjects
            ViewData("ShowCreateVariationLink") = UAL.CanEditProjects
            ViewData("ShowEditVariationLinks") = UAL.CanEditProjects
            ViewData("ShowDeleteVariationLinks") = UAL.CanEditProjects

        End Sub

        <HttpPut()> _
        Public Function AddAssessmentCriterionGroup(ProjectRouteName As String, AssessmentCriterionGroupId As Integer) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            Dim proj = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            MeasurementsDAL.AddProjectAssessmentCriterionGroup(proj.Id, AssessmentCriterionGroupId)
            Return Nothing

        End Function
        <HttpDelete()> _
        Public Function RemoveAssessmentCriterionGroup(ProjectRouteName As String, AssessmentCriterionGroupId As Integer) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            Dim proj = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            MeasurementsDAL.RemoveProjectAssessmentCriterionGroup(proj.Id, AssessmentCriterionGroupId)
            Return Nothing

        End Function
        <HttpPost()> _
        Public Function DeleteAssessmentCriterionGroupFromProject(ProjectId As Integer, AssessmentCriterionGroupId As Integer) As PartialViewResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectId)) Then Return Nothing

            Dim assessmentCriterionGroup = MeasurementsDAL.GetAssessmentCriterionGroup(AssessmentCriterionGroupId)

            Dim criteriaIds = assessmentCriterionGroup.AssessmentCriteria.Where(Function(ac) ac.MonitorLocation.ProjectId = ProjectId).Select(Function(ac) ac.Id).ToList
            For Each criterionId In criteriaIds
                MeasurementsDAL.DeleteAssessmentCriterion(criterionId)
            Next

            MeasurementsDAL.DeleteAssessmentCriterionGroup(AssessmentCriterionGroupId)

            Dim project = MeasurementsDAL.GetProject(assessmentCriterionGroup.ProjectId)
            Dim vm = getEditProjectViewModel(project)

            Return PartialView("Edit_AssessmentCriteria", vm)

        End Function

        <HttpPut()> _
        Public Function AddContact(ProjectRouteName As String, ContactId As Integer) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            ' Add Contact to Project
            Dim proj = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            MeasurementsDAL.AddProjectContact(proj.Id, ContactId)
            ' If Project does not already contain Contact's Organisation then add Contact's Organisation to Project
            Dim con = MeasurementsDAL.GetContact(ContactId)
            If proj.Organisations.Select(Function(o) o.Id).ToList.Contains(con.OrganisationId) = False Then
                MeasurementsDAL.AddProjectOrganisation(proj.Id, con.OrganisationId)
            End If
            Return Nothing

        End Function
        <HttpDelete()> _
        Public Function RemoveContact(ProjectRouteName As String, ContactId As Integer) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            ' Remove Contact from Project
            Dim proj = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            MeasurementsDAL.RemoveProjectContact(proj.Id, ContactId)
            ' If Project has no more Contacts from Contact's Organisation then remove Contact's Organisation from Project
            Dim con = MeasurementsDAL.GetContact(ContactId)
            If proj.Contacts.Select(Function(c) c.OrganisationId).ToList.Contains(con.OrganisationId) = False Then
                MeasurementsDAL.RemoveProjectOrganisation(proj.Id, con.OrganisationId)
            End If

            Return Nothing

        End Function

        <HttpPut()> _
        Public Function AddMeasurementView(ProjectRouteName As String, MeasurementViewId As Integer) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            Dim proj = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            MeasurementsDAL.AddProjectMeasurementView(proj.Id, MeasurementViewId)
            Return Nothing

        End Function
        <HttpDelete()> _
        Public Function RemoveMeasurementView(ProjectRouteName As String, MeasurementViewId As Integer) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            Dim proj = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            MeasurementsDAL.RemoveProjectMeasurementView(proj.Id, MeasurementViewId)
            MeasurementsDAL.RemoveStandardWeeklyWorkingHoursMeasurementView(proj.StandardWeeklyWorkingHours.Id, MeasurementViewId)
            For Each variation In proj.VariedWeeklyWorkingHours
                MeasurementsDAL.RemoveVariedWeeklyWorkingHoursMeasurementView(variation.Id, MeasurementViewId)
            Next

            Return Nothing

        End Function

        <HttpPut()> _
        Public Function AddMonitorLocation(ProjectRouteName As String, MonitorLocationId As Integer) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            Dim proj = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            MeasurementsDAL.AddProjectMonitorLocation(proj.Id, MonitorLocationId)
            Return Nothing

        End Function
        <HttpDelete()> _
        Public Function RemoveMonitorLocation(ProjectRouteName As String, MonitorLocationId As Integer) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            Dim proj = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            MeasurementsDAL.RemoveProjectMonitorLocation(proj.Id, MonitorLocationId)
            Return Nothing

        End Function

        <HttpPut()> _
        Public Function AddOrganisation(ProjectRouteName As String, OrganisationId As Integer) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            Dim proj = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            MeasurementsDAL.AddProjectOrganisation(proj.Id, OrganisationId)
            Return Nothing

        End Function
        <HttpDelete()> _
        Public Function RemoveOrganisation(ProjectRouteName As String, OrganisationId As Integer) As ActionResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            ' Remove Organisation from Project
            Dim proj = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            MeasurementsDAL.RemoveProjectOrganisation(proj.Id, OrganisationId)
            ' Remove all Contacts of the Organisation from the Project
            Dim org = MeasurementsDAL.GetOrganisation(OrganisationId)
            For Each contactId In proj.Contacts.Where(Function(c) c.OrganisationId = OrganisationId).Select(Function(c) c.Id).ToList
                MeasurementsDAL.RemoveProjectContact(proj.Id, contactId)
            Next
            Return Nothing

        End Function

        Public Function Edit_AssessmentCriteria(ByVal ProjectRouteName As String) As PartialViewResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return Nothing

            Dim project As Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            Return PartialView(getEditProjectViewModel(project))

        End Function
        Public Function Edit_Contacts(ByVal ProjectRouteName As String) As PartialViewResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return Nothing

            Dim project As Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            Return PartialView(getEditProjectViewModel(project))

        End Function
        Public Function Edit_MeasurementViews(ByVal ProjectRouteName As String) As PartialViewResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return Nothing

            Dim project As Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            Return PartialView(getEditProjectViewModel(project))

        End Function
        Public Function Edit_MonitorLocations(ByVal ProjectRouteName As String) As PartialViewResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return Nothing

            Dim project As Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            Return PartialView(getEditProjectViewModel(project))

        End Function
        Public Function Edit_Organisations(ByVal ProjectRouteName As String) As PartialViewResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return Nothing

            Dim project As Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            Return PartialView(getEditProjectViewModel(project))

        End Function
        Public Function Edit_WorkingHours(ByVal ProjectRouteName As String) As PartialViewResult

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(ProjectRouteName)) Then Return Nothing

            Dim project As Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
            Return PartialView(getEditProjectViewModel(project))

        End Function

        <HttpPost()>
        <ValidateAntiForgeryToken()> _
        Public Function DeleteVariation(VariedWeeklyWorkingHoursId As Integer) As ActionResult

            Dim VariedWeeklyWorkingHours = MeasurementsDAL.GetVariedWeeklyWorkingHours(VariedWeeklyWorkingHoursId)
            If VariedWeeklyWorkingHours Is Nothing Then Return Nothing

            Dim Project = MeasurementsDAL.GetProject(VariedWeeklyWorkingHours.ProjectId)

            If (Not UAL.CanEditProjects Or
                Not CanAccessProject(Project.getRouteName)) Then Return Nothing

            MeasurementsDAL.DeleteVariedWeeklyWorkingHours(VariedWeeklyWorkingHoursId)

            Return PartialView("Edit_WH_Variations", Project.getVariations)

        End Function

#End Region

#Region "Delete"

        <HttpPost()>
        Public Function DeleteProject(ProjectId As Integer) As ActionResult

            If (Not CanAccessProject(ProjectId) Or
                Not UAL.CanDeleteProjects) Then Return New HttpUnauthorizedResult()

            Dim Project = MeasurementsDAL.GetProject(ProjectId)
            If Project Is Nothing Then Return Nothing
            MeasurementsDAL.DeleteProject(ProjectId)
            Return Json(New With {.redirectToUrl = Url.Action("Index")})

        End Function

#End Region

    End Class

End Namespace

