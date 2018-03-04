@ModelType SEC_Monitoring_Data_Website.UploadMeasurementsViewModel

    <h3>Metric Mappings</h3>

    <table class="create-table">

        @Html.EditorFor(Function(model) model.UploadRionRCDSViewModel.LAeqMapping)
        @Html.EditorFor(Function(model) model.UploadRionRCDSViewModel.LAEMapping)
        @Html.EditorFor(Function(model) model.UploadRionRCDSViewModel.LAmaxMapping)
        @Html.EditorFor(Function(model) model.UploadRionRCDSViewModel.LAminMapping)
        @Html.EditorFor(Function(model) model.UploadRionRCDSViewModel.LA05Mapping)
        @Html.EditorFor(Function(model) model.UploadRionRCDSViewModel.LA10Mapping)
        @Html.EditorFor(Function(model) model.UploadRionRCDSViewModel.LA50Mapping)
        @Html.EditorFor(Function(model) model.UploadRionRCDSViewModel.LA90Mapping)
        @Html.EditorFor(Function(model) model.UploadRionRCDSViewModel.LA95Mapping)
        @Html.EditorFor(Function(model) model.UploadRionRCDSViewModel.LCpkMapping)

    </table>