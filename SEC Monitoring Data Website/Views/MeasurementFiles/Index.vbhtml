@ModelType SEC_Monitoring_Data_Website.ViewMeasurementFilesViewModel

@Code
    ViewData("Title") = "Measurement File List"
End Code


<h2>List of Measurement Files</h2>


@Html.Partial("SearchableIndexMFHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.MeasurementFiles)
</div>


