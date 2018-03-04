@ModelType SEC_Monitoring_Data_Website.MonitorSettings

<table class="details-table">
    <tr>
        <th>
            Measurement Period
        </th>
        <td>
            @Format(Date.FromOADate(Model.MeasurementPeriod), "HH:mm:ss")
        </td>
    </tr>
    @If Not IsNothing(Model.VibrationSetting) Then
        @<tr>
            <th>
                Alarm Trigger Level
            </th>
            <td>
                @Html.Raw(Model.VibrationSetting.AlarmTriggerLevel)
            </td>
        </tr>
        @<tr>
            <th>
                X Channel Weighting
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.VibrationSetting.XChannelWeighting)
            </td>
        </tr>
        @<tr>
            <th>
                Y Channel Weighting
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.VibrationSetting.YChannelWeighting)
            </td>
        </tr>
        @<tr>
            <th>
                Z Channel Weighting
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.VibrationSetting.ZChannelWeighting)
            </td>
        </tr>
    End If
    <tr>
        <th>
            Additional Info 1
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AdditionalInfo1)
        </td>
    </tr>
    <tr>
        <th>
            Additional Info 2
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AdditionalInfo2)
        </td>
    </tr>
</table>