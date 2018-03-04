@ModelType SEC_Monitoring_Data_Website.ContactDetailsViewModel

@Code
    ViewData("Title") = "Contact Details"
End Code

<h2>Contact Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "Contact")