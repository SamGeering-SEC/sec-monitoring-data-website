@ModelType MonitorDeploymentRecordDetailsViewModel

@Code
    ViewData("Title") = "Monitor Deployment Record Details"
End Code

<h2>Monitor Deployment Record Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "MonitorDeploymentRecord")


