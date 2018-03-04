Imports System.Data.Entity.Core
Imports libSEC

Public Class CountriesController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Index"

    Public Function Index() As ActionResult

        If Not UAL.CanViewCountryList Then Return New HttpUnauthorizedResult()

        Return View(getViewCountriesViewModel)

    End Function

    Private Function getViewCountriesViewModel(Optional searchText As String = "") As ViewCountriesViewModel

        Dim countries = MeasurementsDAL.GetCountries
        Dim st = LCase(searchText)
        If searchText <> "" Then countries = countries.Where(Function(c) LCase(c.CountryName).Contains(st))

        setIndexLinks()

        Return New ViewCountriesViewModel With {
            .Countries = countries.ToList,
            .TableId = "countries-table",
            .UpdateTableRouteName = "CountryUpdateIndexTableRoute",
            .ObjectName = "Country",
            .ObjectDisplayName = "Country",
            .NavigationButtons = getIndexNavigationButtons()
        }

    End Function

    Private Sub setIndexLinks()

        ViewData("ShowCountryLinks") = UAL.CanViewCountryDetails
        ViewData("ShowDeleteCountryLinks") = UAL.CanDeleteCountries

    End Sub
    Private Function getIndexNavigationButtons() As List(Of NavigationButtonViewModel)

        Dim buttons = New List(Of NavigationButtonViewModel)

        If UAL.CanCreateContacts Then buttons.Add(GetCreateButton64("Country"))

        Return buttons

    End Function

    <HttpGet()> _
    Public Function UpdateIndexTable(SearchText As String) As PartialViewResult

        Return PartialView("Index_Table", getViewCountriesViewModel(SearchText).countries)

    End Function

#End Region

#Region "Details"

    Public Function Details(CountryRouteName As String) As ActionResult

        If Not UAL.CanViewCountryDetails Then Return New HttpUnauthorizedResult()

        Dim Country = MeasurementsDAL.GetCountry(CountryRouteName.FromRouteName)
        If IsNothing(Country) Then
            Return HttpNotFound()
        End If

        setDetailsLinks()

        Dim vm As New CountryDetailsViewModel With {
            .Country = Country,
            .Tabs = getDetailsTabs(Country),
            .NavigationButtons = getDetailsNavigationButtons(Country)
        }

        Return View(vm)

    End Function
    Private Sub setDetailsLinks()

        ViewData("ShowProjectLinks") = UAL.CanViewProjectDetails
        ViewData("ShowProjectClientOrganisationLinks") = UAL.CanViewOrganisationDetails

    End Sub
    Private Function getDetailsNavigationButtons(country As Country) As IEnumerable(Of NavigationButtonViewModel)

        Dim buttons As New List(Of NavigationButtonViewModel)

        If UAL.CanViewCountryList Then buttons.Add(country.getIndexRouteButton64)
        If UAL.CanEditCountries Then buttons.Add(country.getEditRouteButton64)
        If country.canBeDeleted = True And UAL.CanDeleteCountries Then buttons.Add(country.getDeleteRouteButton64)

        Return buttons

    End Function

    Private Function getDetailsTabs(country As Country) As IEnumerable(Of TabViewModel)

        Dim tabs As New List(Of TabViewModel)

        ' Basic Details
        tabs.Add(TabViewModel.getDetailsTab("Basic Details", "Countries", country))

        ' Public Holidays
        If country.PublicHolidays.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Public Holidays", "Countries", country))
        End If

        ' Projects
        If country.Projects.Count > 0 Then
            tabs.Add(TabViewModel.getDetailsTab("Projects", "Countries", country))
        End If

        Return tabs

    End Function

#End Region

#Region "Edit"

    Public Function Edit(CountryRouteName As String) As ActionResult

        If Not UAL.CanEditCountries Then Return New HttpUnauthorizedResult()

        Dim Country = MeasurementsDAL.GetCountry(CountryRouteName.FromRouteName)

        If IsNothing(Country) Then
            Return HttpNotFound()
        End If

        setEditLinks()

        Return View(Country)

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(Country As Country) As ActionResult

        If Not UAL.CanEditCountries Then Return New HttpUnauthorizedResult()

        If ModelState.IsValid Then
            MeasurementsDAL.UpdateCountry(Country)
            Return RedirectToAction("Details", New With {.CountryRouteName = Country.getRouteName})
        End If

        Return View(Country)

    End Function

    Private Sub setEditLinks()

        ViewData("ShowCreatePublicHolidayLink") = UAL.CanCreatePublicHolidays
        ViewData("ShowDeletePublicHolidayLinks") = UAL.CanDeletePublicHolidays

    End Sub

    <HttpDelete()> _
    Public Function DeletePublicHoliday(CountryRouteName As String, PublicHolidayId As Integer) As ActionResult

        If Not UAL.CanDeletePublicHolidays Then Return New HttpUnauthorizedResult()

        MeasurementsDAL.DeletePublicHoliday(PublicHolidayId)
        Return Nothing

    End Function

#End Region

#Region "Create"

    <HttpGet()> _
    Public Function Create() As ActionResult

        If Not UAL.CanCreateCountries Then Return New HttpUnauthorizedResult()
        Return View(New Country)

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ByVal Country As Country) As ActionResult

        If Not UAL.CanCreateCountries Then Return New HttpUnauthorizedResult()

        ' Check that CountryName does not already exist
        Dim existingCountryNames = MeasurementsDAL.GetCountries().Select(Function(c) c.CountryName.ToRouteName().ToLower()).ToList()
        If existingCountryNames.Contains(Country.CountryName.ToRouteName().ToLower()) Then
            ModelState.AddModelError("CountryName", "Country Name already exists!")
        End If

        If ModelState.IsValid Then
            ' Add Country to database
            MeasurementsDAL.AddCountry(Country)
            ' Redirect to Details
            Return RedirectToAction("Details", New With {.CountryRouteName = Country.getRouteName})
        End If

        Return View(Country)

    End Function

#End Region

#Region "Delete"

    <HttpPost()> _
    Public Function DeleteCountry(CountryId As Integer) As ActionResult

        If Not UAL.CanDeleteCountries Then Return New HttpUnauthorizedResult()

        Dim Country = MeasurementsDAL.GetCountry(CountryId)
        If Country Is Nothing Then Return Nothing
        MeasurementsDAL.DeleteCountry(CountryId)
        Return Json(New With {.redirectToUrl = Url.Action("Index")})

    End Function

#End Region


End Class