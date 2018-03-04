@ModelType SEC_Monitoring_Data_Website.CreateMonitorDeploymentRecordViewModel

<table class="create-table">
    <tr>
        <th>
            Alarm Trigger Level
        </th>
        <td>
            @Html.EditorFor(Function(model) model.VibrationSetting.AlarmTriggerLevel)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.VibrationSetting.AlarmTriggerLevel)
        </td>
    </tr>
    <tr>
        <th>
            XChannel Weighting
        </th>
        <td>
            @Html.EditorFor(Function(model) model.VibrationSetting.XChannelWeighting)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.VibrationSetting.XChannelWeighting)
        </td>
    </tr>
    <tr>
        <th>
            YChannel Weighting
        </th>
        <td>
            @Html.EditorFor(Function(model) model.VibrationSetting.YChannelWeighting)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.VibrationSetting.YChannelWeighting)
        </td>
    </tr>
    <tr>
        <th>
            ZChannel Weighting
        </th>
        <td>
            @Html.EditorFor(Function(model) model.VibrationSetting.ZChannelWeighting)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.VibrationSetting.ZChannelWeighting)
        </td>
    </tr>
</table>
