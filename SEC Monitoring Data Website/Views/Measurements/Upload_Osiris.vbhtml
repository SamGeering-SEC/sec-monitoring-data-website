@ModelType SEC_Monitoring_Data_Website.UploadMeasurementsViewModel

<h3>Metric Mappings</h3>

<table class="create-table">

    @Html.EditorFor(Function(model) model.UploadOsirisViewModel.TotalParticlesMapping)
    @Html.EditorFor(Function(model) model.UploadOsirisViewModel.PM10ParticlesMapping)
    @Html.EditorFor(Function(model) model.UploadOsirisViewModel.PM2point5ParticlesMapping)
    @Html.EditorFor(Function(model) model.UploadOsirisViewModel.PM1ParticlesMapping)
    @Html.EditorFor(Function(model) model.UploadOsirisViewModel.TemperatureMapping)
    @Html.EditorFor(Function(model) model.UploadOsirisViewModel.HumidityMapping)
    @Html.EditorFor(Function(model) model.UploadOsirisViewModel.WindSpeedMapping)
    @Html.EditorFor(Function(model) model.UploadOsirisViewModel.WindDirectionMapping)

</table>