Imports System.Data.Entity.Core
Imports libSEC

Namespace SEC_Monitoring_Data_Website
    Public Class OrganisationTypesController
        Inherits ControllerBase

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)

        End Sub

#Region "Index"

        Public Function Index() As ActionResult

            If Not UAL.CanViewOrganisationTypeList Then Return New HttpUnauthorizedResult()

            Return View(getViewOrganisationTypesViewModel)

        End Function

        Private Function getViewOrganisationTypesViewModel(Optional searchText As String = "") As ViewOrganisationTypesViewModel

            Dim OrganisationTypes = MeasurementsDAL.GetOrganisationTypes
            Dim st = LCase(searchText)
            If searchText <> "" Then OrganisationTypes = OrganisationTypes.Where(Function(ot) LCase(ot.OrganisationTypeName).Contains(st))

            setIndexLinks()

            Return New ViewOrganisationTypesViewModel With {
                .OrganisationTypes = OrganisationTypes.ToList,
                .TableId = "organisationtypes-table",
                .UpdateTableRouteName = "OrganisationTypeUpdateIndexTableRoute",
                .ObjectName = "OrganisationType",
                .ObjectDisplayName = "Organisation Type",
                .NavigationButtons = getIndexNavigationButtons()
            }

        End Function

        Private Sub setIndexLinks()

            ViewData("ShowOrganisationLinks") = UAL.CanViewOrganisationDetails
            ViewData("ShowDeleteOrganisationLinks") = UAL.CanDeleteOrganisations

        End Sub
        Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

            Dim buttons = New List(Of NavigationButtonViewModel)

            If UAL.CanCreateOrganisationTypes Then buttons.Add(GetCreateButton64("OrganisationType"))
            If UAL.CanViewOrganisationList Then buttons.Add(GetIndexButton64("Organisation", "organisation-button-64"))

            Return buttons

        End Function

        <HttpGet()> _
        Public Function UpdateIndexTable(SearchText As String) As PartialViewResult

            Return PartialView("Index_Table", getViewOrganisationTypesViewModel(SearchText).OrganisationTypes)

        End Function

#End Region

#Region "Details"
        Public Function Details(OrganisationTypeRouteName As String) As ActionResult

            If Not UAL.CanViewOrganisationTypeDetails Then Return New HttpUnauthorizedResult()

            Dim OrganisationType = MeasurementsDAL.GetOrganisationType(OrganisationTypeRouteName.FromRouteName)
            If IsNothing(OrganisationType) Then
                Return HttpNotFound()
            End If

            Dim vm As New OrganisationTypeDetailsViewModel With {
                .OrganisationType = OrganisationType,
                .Tabs = getDetailsTabs(OrganisationType),
                .NavigationButtons = getDetailsNavigationButtons(OrganisationType)
            }
            setDetailsLinks()

            Return View(vm)

        End Function

        Private Sub setDetailsLinks()

            ViewData("ShowOrganisationLinks") = UAL.CanViewOrganisationDetails

        End Sub

        Private Function getDetailsNavigationButtons(organisationType As OrganisationType)

            Dim buttons As New List(Of NavigationButtonViewModel)

            If UAL.CanViewOrganisationTypeList Then buttons.Add(organisationType.getIndexRouteButton64)
            If UAL.CanEditOrganisationTypes Then buttons.Add(organisationType.getEditRouteButton64)
            If organisationType.canBeDeleted = True And UAL.CanDeleteOrganisationTypes Then buttons.Add(organisationType.getDeleteRouteButton64)

            Return buttons

        End Function

        Private Function getDetailsTabs(organisationType As OrganisationType) As IEnumerable(Of TabViewModel)

            Dim tabs As New List(Of TabViewModel)

            ' Basic Details
            tabs.Add(TabViewModel.getDetailsTab("Basic Details", "OrganisationTypes", organisationType))

            ' Organisations
            If organisationType.Organisations.Count > 0 Then
                tabs.Add(TabViewModel.getDetailsTab("Organisations", "OrganisationTypes", organisationType))
            End If

            Return tabs

        End Function

#End Region

#Region "Edit"

        Public Function Edit(OrganisationTypeRouteName As String) As ActionResult

            If Not UAL.CanEditOrganisationTypes Then Return New HttpUnauthorizedResult()

            Dim OrganisationType = MeasurementsDAL.GetOrganisationType(OrganisationTypeRouteName.FromRouteName)
            If IsNothing(OrganisationType) Then
                Return HttpNotFound()
            End If
            Return View(OrganisationType)

        End Function
        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Edit(OrganisationType As OrganisationType) As ActionResult

            If Not UAL.CanEditOrganisationTypes Then Return New HttpUnauthorizedResult()

            If ModelState.IsValid Then
                MeasurementsDAL.UpdateOrganisationType(OrganisationType)
                Return RedirectToAction("Details", New With {.OrganisationTypeRouteName = OrganisationType.getRouteName})
            End If

            Return View(OrganisationType)

        End Function

#End Region

#Region "Create"

        <HttpGet()> _
        Public Function Create() As ActionResult

            If Not UAL.CanEditOrganisationTypes Then Return New HttpUnauthorizedResult()

            Return View(New OrganisationType)

        End Function

        <HttpPost()> _
        <ValidateAntiForgeryToken()> _
        Public Function Create(ByVal OrganisationType As OrganisationType) As ActionResult

            If Not UAL.CanEditOrganisationTypes Then Return New HttpUnauthorizedResult()

            ' Check that OrganisationTypeName does not already exist
            Dim existingOrganisationTypeNames = MeasurementsDAL.GetOrganisationTypes().Select(
                Function(ot) ot.OrganisationTypeName.ToRouteName().ToLower()
            ).ToList()
            If existingOrganisationTypeNames.Contains(OrganisationType.OrganisationTypeName.ToRouteName().ToLower()) Then
                ModelState.AddModelError("OrganisationTypeName", "Organisation Type Name already exists!")
            End If

            If ModelState.IsValid Then
                ' Add Organisation Type to database
                MeasurementsDAL.AddOrganisationType(OrganisationType)
                ' Redirect to Details
                Return RedirectToAction("Details", New With {.OrganisationTypeRouteName = OrganisationType.getRouteName})
            End If

            Return View(OrganisationType)

        End Function

#End Region

#Region "Delete"

        <HttpPost()> _
        Public Function DeleteOrganisationType(OrganisationTypeId As Integer) As ActionResult

            If Not UAL.CanDeleteOrganisationTypes Then Return New HttpUnauthorizedResult()

            Dim OrganisationType = MeasurementsDAL.GetOrganisationType(OrganisationTypeId)
            If OrganisationType Is Nothing Then Return Nothing
            MeasurementsDAL.DeleteOrganisationType(OrganisationTypeId)
            Return Json(New With {.redirectToUrl = Url.Action("Index")})

        End Function

#End Region

    End Class

End Namespace