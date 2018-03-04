@ModelType SEC_Monitoring_Data_Website.UploadMeasurementsViewModel

<h3>Metric Mappings</h3>

<table class="create-table">

    
    @Html.EditorFor(Function(model) model.UploadPPVLiveViewModel.XcvMapping)
    @Html.EditorFor(Function(model) model.UploadPPVLiveViewModel.YcvMapping)
    @Html.EditorFor(Function(model) model.UploadPPVLiveViewModel.ZcvMapping)
    @Html.EditorFor(Function(model) model.UploadPPVLiveViewModel.XcfMapping)
    @Html.EditorFor(Function(model) model.UploadPPVLiveViewModel.YcfMapping)
    @Html.EditorFor(Function(model) model.UploadPPVLiveViewModel.ZcfMapping)
    @Html.EditorFor(Function(model) model.UploadPPVLiveViewModel.XuMapping)
    @Html.EditorFor(Function(model) model.UploadPPVLiveViewModel.YuMapping)
    @Html.EditorFor(Function(model) model.UploadPPVLiveViewModel.ZuMapping)
    
</table>