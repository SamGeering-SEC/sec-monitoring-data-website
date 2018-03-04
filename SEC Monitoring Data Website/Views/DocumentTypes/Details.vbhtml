@ModelType SEC_Monitoring_Data_Website.DocumentTypeDetailsViewModel

@Code
    ViewData("Title") = "Document Type Details"
End Code

<h2>Document Type Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "DocumentType")