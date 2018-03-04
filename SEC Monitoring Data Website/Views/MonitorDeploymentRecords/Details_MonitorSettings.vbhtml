@ModelType SEC_Monitoring_Data_Website.MonitorDeploymentRecord

<h3>General Settings</h3>

<table class="details-table">
    <tr>
        <th>
            Measurement Period
        </th>
        <td>
            @Format(Date.FromOADate(Model.MonitorSettings.MeasurementPeriod), "HH:mm:ss")
        </td>
    </tr>
    <tr>
        <th>
            Additional Info 1
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MonitorSettings.AdditionalInfo1)
        </td>
    </tr>
    <tr>
        <th>
            Additional Info 2
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MonitorSettings.AdditionalInfo2)
        </td>
    </tr>
</table>

<h3>Specific Settings</h3>

@Select Case Model.Monitor.MeasurementType.MeasurementTypeName

    Case "Noise"
    @Html.Partial("Details_MS_NoiseSetting", Model)
Case "Vibration"
    @Html.Partial("Details_MS_VibrationSetting", Model)
Case "Air Quality, Dust and Meteorological"
    @Html.Partial("Details_MS_AirQualitySetting", Model)

End Select
