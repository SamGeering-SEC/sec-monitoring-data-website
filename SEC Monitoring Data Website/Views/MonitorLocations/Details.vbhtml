@ModelType SEC_Monitoring_Data_Website.MonitorLocationDetailsViewModel

@Code
    ViewData("Title") = "Monitor Location Details"
End Code


<h2>@Model.MonitorLocation.MonitorLocationName Monitor Location Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "MonitorLocation")

