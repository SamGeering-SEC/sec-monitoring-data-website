@ModelType SEC_Monitoring_Data_Website.CreateMonitorDeploymentRecordViewModel

<table class="create-table">
    <tr>
        <th>
            Microphone Serial Number
        </th>
        <td>
            @Html.EditorFor(Function(model) model.NoiseSetting.MicrophoneSerialNumber)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.NoiseSetting.MicrophoneSerialNumber)
        </td>
    </tr>
    <tr>
        <th>
            Dynamic Range Lower Level
        </th>
        <td>
            @Html.EditorFor(Function(model) model.NoiseSetting.DynamicRangeLowerLevel)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.NoiseSetting.DynamicRangeLowerLevel)
        </td>
    </tr>
    <tr>
        <th>
            Dynamic Range Upper Level
        </th>
        <td>
            @Html.EditorFor(Function(model) model.NoiseSetting.DynamicRangeUpperLevel)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.NoiseSetting.DynamicRangeUpperLevel)
        </td>
    </tr>
    <tr>
        <th>
            Wind Screen Correction
        </th>
        <td>
            @Html.EditorFor(Function(model) model.NoiseSetting.WindScreenCorrection)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.NoiseSetting.WindScreenCorrection)
        </td>
    </tr>
    <tr>
        <th>
            Alarm Trigger Level
        </th>
        <td>
            @Html.EditorFor(Function(model) model.NoiseSetting.AlarmTriggerLevel)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.NoiseSetting.AlarmTriggerLevel)
        </td>
    </tr>
    <tr>
        <th>
            Frequency Weighting
        </th>
        <td>
            @Html.EditorFor(Function(model) model.NoiseSetting.FrequencyWeighting)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.NoiseSetting.FrequencyWeighting)
        </td>
    </tr>
    @If Model.ValidationErrors.Contains("FrequencyWeighting") Then
        @<tr>
            <td style="color:red">
                Please define the Frequency Weighting
            </td>
        </tr>
    End If
    <tr>
        <th>
            Time Weighting
        </th>
        <td>
            @Html.EditorFor(Function(model) model.NoiseSetting.TimeWeighting)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.NoiseSetting.TimeWeighting)
        </td>
    </tr>
    @If Model.ValidationErrors.Contains("TimeWeighting") Then
        @<tr>
            <td style="color:red">
                Please define the Time Weighting
            </td>
        </tr>
    End If
    <tr>
        <th>
            Sound Recording
        </th>
        <td>
            @Html.EditorFor(Function(model) model.NoiseSetting.SoundRecording)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.NoiseSetting.SoundRecording)
        </td>
    </tr>
</table>