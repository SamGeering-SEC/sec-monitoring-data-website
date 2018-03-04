@ModelType SEC_Monitoring_Data_Website.MeasurementMetricDetailsViewModel

@Code
    ViewData("Title") = "Measurement Metric Details"
End Code

<h2>Measurement Metric Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "MeasurementMetric")