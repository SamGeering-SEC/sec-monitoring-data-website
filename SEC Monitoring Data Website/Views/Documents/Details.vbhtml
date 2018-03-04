@ModelType SEC_Monitoring_Data_Website.DocumentDetailsViewModel

@Code
    ViewData("Title") = "Document Details"
End Code

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "Document")