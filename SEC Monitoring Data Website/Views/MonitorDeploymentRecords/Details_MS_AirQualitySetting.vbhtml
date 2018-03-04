@ModelType SEC_Monitoring_Data_Website.MonitorDeploymentRecord

<table class="details-table">
    @If Model.MonitorSettings.AirQualitySetting IsNot Nothing Then
    @<tr>
        <th>
            Alarm Trigger Level
        </th>
        <td>
            @Html.Raw(Model.MonitorSettings.AirQualitySetting.AlarmTriggerLevel)
        </td>
    </tr>
    @<tr>
        <th>
            Inlet Heating On
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MonitorSettings.AirQualitySetting.InletHeatingOn)
        </td>
    </tr>
    @<tr>
        <th>
            New Daily Sample
        </th>
         <td>
            @Html.DisplayFor(Function(model) model.MonitorSettings.AirQualitySetting.NewDailySample)
        </td>
    </tr>
    End If
</table>