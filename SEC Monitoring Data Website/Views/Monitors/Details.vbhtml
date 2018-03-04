@ModelType SEC_Monitoring_Data_Website.MonitorDetailsViewModel

@Code
    ViewData("Title") = "Monitor Details"
End Code

<h2>@Model.Monitor.MonitorName Monitor Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "Monitor")