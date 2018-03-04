@ModelType CalculationFilterDetailsViewModel

@Code
    ViewData("Title") = "Calculation Filter Details"
End Code

<h2>Calculation Filter Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "CalculationFilter")
