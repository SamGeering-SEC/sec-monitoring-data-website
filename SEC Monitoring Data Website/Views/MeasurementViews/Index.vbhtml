@ModelType SEC_Monitoring_Data_Website.ViewMeasurementViewsViewModel

@Code
    ViewData("Measurement Views List") = "Index"
End Code

<h2>List of Measurement Views</h2>

@Html.Partial("SearchableIndexMTHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.MeasurementViews)
</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)