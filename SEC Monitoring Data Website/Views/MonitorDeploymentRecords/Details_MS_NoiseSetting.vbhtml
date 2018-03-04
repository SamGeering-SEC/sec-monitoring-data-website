@ModelType SEC_Monitoring_Data_Website.MonitorDeploymentRecord

<table class="details-table">
    @If Model.MonitorSettings.NoiseSetting IsNot Nothing Then
        @<tr>
            <th>
                Microphone Serial Number
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MonitorSettings.NoiseSetting.MicrophoneSerialNumber)
            </td>
        </tr>
        @<tr>
            <th>
                Dynamic Range Lower Level
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MonitorSettings.NoiseSetting.DynamicRangeLowerLevel)
            </td>
        </tr>
        @<tr>
            <th>
                Dynamic Range Upper Level
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MonitorSettings.NoiseSetting.DynamicRangeUpperLevel)
            </td>
        </tr>
        @<tr>
            <th>
                Wind Screen Correction
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MonitorSettings.NoiseSetting.WindScreenCorrection)
            </td>
        </tr>
        @<tr>
            <th>
                Alarm Trigger Level
            </th>
            <td>
                @Html.Raw(model.MonitorSettings.NoiseSetting.AlarmTriggerLevel)
            </td>
        </tr>
        @<tr>
            <th>
                Frequency Weighting
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MonitorSettings.NoiseSetting.FrequencyWeighting)
            </td>
        </tr>
        @<tr>
            <th>
                Time Weighting
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MonitorSettings.NoiseSetting.TimeWeighting)
            </td>
        </tr>
        @<tr>
            <th>
                Sound Recording
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MonitorSettings.NoiseSetting.SoundRecording)
            </td>
        </tr>
    End If
</table>