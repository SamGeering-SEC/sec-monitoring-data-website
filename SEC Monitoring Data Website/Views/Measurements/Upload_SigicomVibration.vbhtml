@ModelType SEC_Monitoring_Data_Website.UploadMeasurementsViewModel

<h3>Metric Mappings</h3>

<table class="create-table">

    @Html.EditorFor(Function(model) model.UploadSigicomVibrationViewModel.PPVXChannelMapping)
    @Html.EditorFor(Function(model) model.UploadSigicomVibrationViewModel.PPVYChannelMapping)
    @Html.EditorFor(Function(model) model.UploadSigicomVibrationViewModel.PPVZChannelMapping)

</table>