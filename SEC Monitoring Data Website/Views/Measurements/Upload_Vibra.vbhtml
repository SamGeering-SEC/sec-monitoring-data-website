@ModelType SEC_Monitoring_Data_Website.UploadMeasurementsViewModel

<h3>Metric Mappings</h3>

<table class="create-table">

    @Html.EditorFor(Function(model) model.UploadVibraViewModel.PPVXChannelMapping)
    @Html.EditorFor(Function(model) model.UploadVibraViewModel.PPVYChannelMapping)
    @Html.EditorFor(Function(model) model.UploadVibraViewModel.PPVZChannelMapping)
    @Html.EditorFor(Function(model) model.UploadVibraViewModel.DominantFrequencyComponentXChannelMapping)
    @Html.EditorFor(Function(model) model.UploadVibraViewModel.DominantFrequencyComponentYChannelMapping)
    @Html.EditorFor(Function(model) model.UploadVibraViewModel.DominantFrequencyComponentZChannelMapping)

</table>
