@ModelType SEC_Monitoring_Data_Website.CountryDetailsViewModel

@Code
    ViewData("Title") = "Country Details"
End Code

<h2>Details for @Html.DisplayFor(Function(model) model.Country.CountryName)</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "Country")