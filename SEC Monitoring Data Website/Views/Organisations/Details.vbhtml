@ModelType SEC_Monitoring_Data_Website.OrganisationDetailsViewModel

@Code
    ViewData("Title") = "Organisation Details"
End Code

<h2>Organisation Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "Organisation")