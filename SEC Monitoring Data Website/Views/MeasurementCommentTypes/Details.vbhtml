@ModelType SEC_Monitoring_Data_Website.MeasurementCommentTypeDetailsViewModel

@Code
    ViewData("Title") = "Measurement Comment Type Details"
End Code

<h2>Measurement Comment Type Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "MeasurementCommentType")