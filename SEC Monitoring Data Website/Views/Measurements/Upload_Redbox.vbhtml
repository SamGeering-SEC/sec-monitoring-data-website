@ModelType SEC_Monitoring_Data_Website.UploadMeasurementsViewModel

<h3>Metric Mappings</h3>

<table class="create-table">

    @Html.EditorFor(Function(model) model.UploadRedboxViewModel.XMapping)
    @Html.EditorFor(Function(model) model.UploadRedboxViewModel.YMapping)
    @Html.EditorFor(Function(model) model.UploadRedboxViewModel.ZMapping)

</table>