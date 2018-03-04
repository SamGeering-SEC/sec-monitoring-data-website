@ModelType SEC_Monitoring_Data_Website.UploadMeasurementsViewModel

@*<h3>File Settings</h3>

<table class="create-table">
        <tr>
        <th>
            Duration Field Setting
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.UploadSpreadsheetTemplateViewModel.DurationFieldSettingValue,
                                                  Model.UploadSpreadsheetTemplateViewModel.DurationFieldSettingList,
                                                  "Please specify how the Durations of each Measurement should be Calculated...")
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.UploadSpreadsheetTemplateViewModel.DurationFieldSettingValue)
        </td>
    </tr>
</table>*@
