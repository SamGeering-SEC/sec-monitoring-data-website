@ModelType SEC_Monitoring_Data_Website.CreateMonitorDeploymentRecordViewModel

<table class="create-table">

    <tr>
        <th>
            Measurement Period
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementPeriod)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementPeriod)
        </td>
        @If Model.ValidationErrors.Contains("MeasurementPeriod") Then
            @<tr>
                <td style="color:red">
                    Please define the Measurement Period
                </td>
            </tr>
        End If
    </tr>
    <tr>
        <th>
            Additional Info 1
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MonitorSettings.AdditionalInfo1)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MonitorSettings.AdditionalInfo1)
        </td>
    </tr>
    <tr>
        <th>
            Additional Info 2
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MonitorSettings.AdditionalInfo2)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MonitorSettings.AdditionalInfo2)
        </td>
    </tr>
</table>