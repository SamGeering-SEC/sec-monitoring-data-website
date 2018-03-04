@ModelType SEC_Monitoring_Data_Website.ViewMeasurementMetricsViewModel

@Code
    ViewData("Title") = "Measurement Metrics List"
End Code

<h2>List of Measurement Metrics</h2>

@Html.Partial("SearchableIndexMTHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.MeasurementMetrics)
</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)