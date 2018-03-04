@ModelType SEC_Monitoring_Data_Website.ViewCalculationFiltersViewModel

@Code
    ViewData("Title") = "Calculation Filter List"
End Code

<h2>List of Calculation Filters</h2>

@Html.Partial("SearchableIndexMTHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.CalculationFilters)
</div>


@Html.Partial("NavigationButtons", Model.NavigationButtons)
