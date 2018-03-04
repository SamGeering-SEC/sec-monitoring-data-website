@ModelType SEC_Monitoring_Data_Website.CreateMonitorDeploymentRecordViewModel

<table class="create-table">
    <tr>
        <th>
            Alarm Trigger Level
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AirQualitySetting.AlarmTriggerLevel)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AirQualitySetting.AlarmTriggerLevel)
        </td>
    </tr>
    <tr>
        <th>
            Inlet Heating On
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AirQualitySetting.InletHeatingOn)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AirQualitySetting.InletHeatingOn)
        </td>
    </tr>
    <tr>
        <th>
            New Daily Sample
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AirQualitySetting.NewDailySample)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AirQualitySetting.NewDailySample)
        </td>
    </tr>
</table>