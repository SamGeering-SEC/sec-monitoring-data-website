@ModelType SEC_Monitoring_Data_Website.OrganisationTypeDetailsViewModel

@Code
    ViewData("Title") = "Organisation Type Details"
End Code

<h2>Organisation Type Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "OrganisationType")