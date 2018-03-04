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
    @If Not IsNothing(Model.AirQualitySetting) Then
        @<tr>
            <th>
                Alarm Trigger Level
            </th>
            <td>
                @Html.Raw(Model.AirQualitySetting.AlarmTriggerLevel)
            </td>
        </tr>
        @<tr>
            <th>
                Inlet Heating On
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.AirQualitySetting.InletHeatingOn)
            </td>
        </tr>
        @<tr>
            <th>
                New Daily Sample
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.AirQualitySetting.NewDailySample)
            </td>
        </tr>
    End If
    <tr>
        <th>
            Additional Info1
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AdditionalInfo1)
        </td>
    </tr>
    <tr>
        <th>
            Additional Info2
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AdditionalInfo2)
        </td>
    </tr>
</table>