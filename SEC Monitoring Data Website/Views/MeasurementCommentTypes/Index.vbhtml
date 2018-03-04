@ModelType SEC_Monitoring_Data_Website.ViewMeasurementCommentTypesViewModel

@Code
    ViewData("Title") = "Measurement Comment Type List"
End Code

<h2>List of Measurement Comment Types</h2>

@Html.Partial("SearchableIndexHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.MeasurementCommentTypes)
</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)