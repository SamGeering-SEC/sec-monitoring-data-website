@ModelType SEC_Monitoring_Data_Website.UploadMeasurementsViewModel

<h3>Metric Mappings</h3>

<table class="create-table">

    @Html.EditorFor(Function(model) model.UploadSPLTrackViewModel.PeriodLAeqMapping)
    @Html.EditorFor(Function(model) model.UploadSPLTrackViewModel.LAmaxMapping)
    @Html.EditorFor(Function(model) model.UploadSPLTrackViewModel.L10Mapping)
    @Html.EditorFor(Function(model) model.UploadSPLTrackViewModel.L90Mapping)

</table>