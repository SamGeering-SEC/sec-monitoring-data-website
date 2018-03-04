Imports System.Data.Entity.Core
Imports libSEC

Namespace SEC_Monitoring_Data_Website
    Public Class OrganisationsController
        Inherits ControllerBase

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)

        End Sub

#Region "Index"

        Public Function Index() As ActionResult

            If Not UAL.CanViewOrganisationList Then Return New HttpUnauthorizedResult()

            Return View(getViewOrganisationsViewModel)

        End Function

        Private Function getViewOrganisationsViewModel(Optional searchText As String = "") As ViewOrganisationsViewModel

            Dim organisations = MeasurementsDAL.GetOrganisations
            Dim st = LCase(searchText)
            If searchText <> "" Then organisations = organisations.Where(
                Function(o) LCase(o.FullName).Contains(st) Or
                            LCase(o.ShortName).Contains(st) Or
                            LCase(o.Address).Contains(st) Or
                            LCase(o.OrganisationType.OrganisationTypeName).Contains(st)
            )

            setIndexLinks()

            Return New ViewOrganisationsViewModel With {
                .Organisations = organisations.ToList,
                .TableId = "organisations-table",
                .UpdateTableRouteName = "OrganisationUpdateIndexTableRoute",
                .ObjectName = "Organisation",
                .ObjectDisplayName = "Organisation",
                .Names = organisations.Select(Function(o) o.FullName).ToList,
                .NavigationButtons = getIndexNavigationButtons()
            }

        End Function

        Private Sub setIndexLinks()

            ViewData("ShowOrganisationLinks") = UAL.CanViewOrganisationDetails
            ViewData("ShowDeleteOrganisationLinks") = UAL.CanDeleteOrganisations

        End Sub
        Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

            Dim buttons = New List(Of NavigationButtonViewModel)

            If UAL.CanCreateOrganisations Then buttons.Add(GetCreateButton64("Organisation"))
            If UAL.CanViewOrganisationTypeList Then buttons.Add(GetIndexButton64("OrganisationType", "types-button-64"))

            Return buttons

        End Function

        <HttpGet()> _
        Public Function UpdateIndexTable(SearchText As String) As PartialViewResult

            Return PartialView("Index_Table", getViewOrganisationsViewModel(SearchText).Organisations)

        End Function

#End Region

#Region "Details"

        Public Function Details(OrganisationRouteName As String) As ActionResult

            If Not UAL.CanViewOrganisationDetails Then Return New HttpUnauthorizedResult()

            Dim organisation = MeasurementsDAL.GetOrganisation(OrganisationRouteName.FromRouteName)

            If IsNothing(organisation) Then
                Return HttpNotFound()
            End If

            Dim vm As New OrganisationDetailsViewModel With {
                .Organisation = organisation,
                .Tabs = getDetailsTabs(organisation),
                .NavigationButtons = getDetailsNavigationButtons(organisation)
            }

            setDetailsLinks()

            Return View(vm)

        End Function

        Private Sub setDetailsLinks()

            ViewData("ShowAuthoredDocumentLinks") = UAL.CanViewDocumentDetails
            ViewData("ShowAuthoredDocumentTypeLinks") = UAL.CanViewDocumentTypeDetails
            ViewData("ShowOrganisationTypeLink") = UAL.CanViewOrganisationTypeDetails
            ViewData("ShowContactLinks") = UAL.CanViewContactDetails
            ViewData("ShowOwnedMonitorLinks") = UAL.CanViewMonitorDetails
            ViewData("ShowProjectLinks") = UAL.CanViewProjectDetails
            ViewData("ShowProjectClientOrganisationLinks") = UAL.CanViewOrganisationDetails

        End Sub

        Private Function getDetailsNavigationButtons(organisation As Organisation) As IEnumerable(Of NavigationButtonViewModel)

            Dim buttons As New List(Of NavigationButtonViewModel)

            If UAL.CanViewOrganisationDetails Then buttons.Add(organisation.getIndexRouteButton64)
            If UAL.CanEditOrganisations Then buttons.Add(organisation.getEditRouteButton64)
            If organisation.canBeDeleted = True And UAL.CanDeleteOrganisations Then buttons.Add(organisation.getDeleteRouteButton64)

            Return buttons

        End Function

        Private Function getDetailsTabs(organisation As Organisation) As IEnumerable(Of TabViewModel)

            Dim tabs As New List(Of TabViewModel)

            ' Basic Details
            tabs.Add(TabViewModel.getDetailsTab("Basic Details", "Organisations", organisation))

            ' Contacts
            If organisation.Contacts.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Contacts", "Organisations", organisation))
            End If

            ' Projects
            If organisation.Projects.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Projects", "Organisations", organisation))
            End If

            ' Owned Monitors
            If organisation.OwnedMonitors.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Owned Monitors", "Organisations", organisation))
            End If

            ' Authored Documents
            If organisation.AuthoredDocuments.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Authored Documents", "Organisations", organisation))
            End If

            Return tabs

        End Function

#End Region

#Region "Create"

        <HttpGet()> _
        Public Function Create() As ActionResult

            If Not UAL.CanCreateOrganisations Then Return New HttpUnauthorizedResult()

            Dim vm As New CreateOrganisationViewModel With {
                .Organisation = New Organisation,
                .OrganisationTypeList = New SelectList(MeasurementsDAL.GetOrganisationTypes, "Id", "OrganisationTypeName")
            }

            Return View(vm)

        End Function
        Private Function getCreateOrganisationViewModel(Optional viewModel As CreateOrganisationViewModel = Nothing) As CreateOrganisationViewModel

            Dim organisation As Organisation

            If viewModel Is Nothing Then
                organisation = New Organisation
            Else
                organisation = viewModel.Organisation
            End If

            Return New CreateOrganisationViewModel With {
                .Organisation = organisation,
                .OrganisationTypeList = New SelectList(MeasurementsDAL.GetOrganisationTypes, "Id", "OrganisationTypeName")
            }

        End Function
        <HttpPost()> _
        Public Function Create(ByVal ViewModel As CreateOrganisationViewModel) As ActionResult

            If Not UAL.CanCreateOrganisations Then Return New HttpUnauthorizedResult()

            ' Check that OrganisationName does not already exist
            Dim existingOrganisationNames = MeasurementsDAL.GetOrganisations().Select(
                Function(m) m.ShortName.ToRouteName().ToLower()
            ).ToList()
            If existingOrganisationNames.Contains(ViewModel.Organisation.ShortName.ToRouteName().ToLower()) Then
                ModelState.AddModelError("Organisation.ShortName", "Organisation Short Name already exists!")
            End If

            ModelState.Remove("Organisation.OrganisationType")

            If ModelState.IsValid Then
                ViewModel.Organisation.OrganisationType = MeasurementsDAL.GetOrganisationType(ViewModel.OrganisationTypeId)
                MeasurementsDAL.AddOrganisation(ViewModel.Organisation)
                Return RedirectToAction("Details", "Organisations", New With {.OrganisationRouteName = ViewModel.Organisation.getRouteName})
            End If

            Dim vm As New CreateOrganisationViewModel With {
                .Organisation = ViewModel.Organisation,
                .OrganisationTypeId = ViewModel.OrganisationTypeId,
                .OrganisationTypeList = New SelectList(MeasurementsDAL.GetOrganisationTypes, "Id", "DefaultProperty")
            }

            Return View(getCreateOrganisationViewModel(vm))

        End Function

#End Region

#Region "Edit"

        <HttpGet()> _
        Public Function Edit(ByVal OrganisationRouteName As String) As ActionResult

            If Not UAL.CanEditOrganisations Then Return New HttpUnauthorizedResult()

            Dim Organisation As Organisation = MeasurementsDAL.GetOrganisation(OrganisationRouteName.FromRouteName)
            If IsNothing(Organisation) Then
                Return HttpNotFound()
            End If

            Return View(getEditOrganisationViewModel(Organisation))

        End Function
        Private Function getEditOrganisationViewModel(ByVal Organisation As Organisation)

            Return New EditOrganisationViewModel With {
                .Organisation = Organisation,
                .OrganisationTypeId = Organisation.OrganisationTypeId,
                .OrganisationTypeList = New SelectList(
                    MeasurementsDAL.GetOrganisationTypes, "Id", "OrganisationTypeName", Organisation.OrganisationTypeId
                ),
                .AllProjects = AllowedProjects()
            }

        End Function
        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Edit(ByVal ViewModel As EditOrganisationViewModel) As ActionResult

            If Not UAL.CanEditOrganisations Then Return New HttpUnauthorizedResult()

            ModelState.Remove("Organisation.OrganisationType")

            If ModelState.IsValid Then
                ' Attach Relations
                ViewModel.Organisation.OrganisationType = MeasurementsDAL.GetOrganisationType(ViewModel.OrganisationTypeId)
                ' Update Organisation
                MeasurementsDAL.UpdateOrganisation(ViewModel.Organisation)
                ' Redirect to Details
                Return RedirectToAction("Details", New With {.OrganisationRouteName = ViewModel.Organisation.getRouteName})
            End If

            Return View(ViewModel)

        End Function

        <HttpPut()> _
        Public Function AddProject(OrganisationId As Integer, ProjectId As Integer) As ActionResult

            If Not UAL.CanEditOrganisations Then Return New HttpUnauthorizedResult()

            MeasurementsDAL.AddProjectOrganisation(ProjectId, OrganisationId)
            Return Nothing

        End Function
        <HttpDelete()> _
        Public Function RemoveProject(OrganisationId As Integer, ProjectId As Integer) As ActionResult

            If Not UAL.CanEditOrganisations Then Return New HttpUnauthorizedResult()

            MeasurementsDAL.RemoveProjectOrganisation(ProjectId, OrganisationId)
            Return Nothing

        End Function

#End Region

#Region "Delete"

        <HttpPost()> _
        Public Function DeleteOrganisation(OrganisationId As Integer) As ActionResult

            If Not UAL.CanDeleteOrganisations Then Return New HttpUnauthorizedResult()

            Dim Organisation = MeasurementsDAL.GetOrganisation(OrganisationId)
            If Organisation Is Nothing Then Return Nothing
            MeasurementsDAL.DeleteOrganisation(OrganisationId)
            Return Json(New With {.redirectToUrl = Url.Action("Index")})

        End Function

#End Region


    End Class

End Namespace