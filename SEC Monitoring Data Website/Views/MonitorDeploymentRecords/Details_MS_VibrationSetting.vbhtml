@ModelType SEC_Monitoring_Data_Website.MonitorDeploymentRecord

<table class="details-table">
    @If Model.MonitorSettings.VibrationSetting IsNot Nothing Then
        @<tr>
            <th>
                Alarm Trigger Level
            </th>
            <td>
                @Html.Raw(Model.MonitorSettings.VibrationSetting.AlarmTriggerLevel)
            </td>
        </tr>
        @<tr>
            <th>
                X Channel Weighting
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MonitorSettings.VibrationSetting.XChannelWeighting)
            </td>
        </tr>
        @<tr>
            <th>
                Y Channel Weighting
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MonitorSettings.VibrationSetting.YChannelWeighting)
            </td>
        </tr>
        @<tr>
            <th>
                Z Channel Weighting
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MonitorSettings.VibrationSetting.ZChannelWeighting)
            </td>
        </tr>
    End If
</table>