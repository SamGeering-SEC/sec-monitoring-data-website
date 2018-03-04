@ModelType SEC_Monitoring_Data_Website.ViewMeasurementFilesViewModel

@Code
    ViewData("Title") = "Measurement File List"
End Code


<h3><span style="color: #00ff00;">File was deleted successfully!</span></h3>


<h2>List of Measurement Files</h2>

@Html.Partial("SearchableIndexMFHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.MeasurementFiles)
</div>


