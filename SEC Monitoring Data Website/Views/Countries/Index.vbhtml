@ModelType SEC_Monitoring_Data_Website.ViewCountriesViewModel

@Code
    ViewData("Title") = "Country List"
End Code

<h2>List of Countries</h2>

@Html.Partial("SearchableIndexHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.Countries)
</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)