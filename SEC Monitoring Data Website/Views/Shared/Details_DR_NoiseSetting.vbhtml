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
    @If Not IsNothing(Model.NoiseSetting) Then

        @If Not IsNothing(Model.NoiseSetting.MicrophoneSerialNumber) Then
            @<tr>
                <th>
                    Microphone Serial Number
                </th>
                <td>
                    @Html.DisplayFor(Function(model) model.NoiseSetting.MicrophoneSerialNumber)
                </td>
            </tr>
        End If
        @<tr>
            <th>
                Dynamic Range Lower Level
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.NoiseSetting.DynamicRangeLowerLevel)
            </td>
        </tr>
        @<tr>
            <th>
                Dynamic Range Upper Level
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.NoiseSetting.DynamicRangeUpperLevel)
            </td>
        </tr>
        @<tr>
            <th>
                Wind Screen Correction
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.NoiseSetting.WindScreenCorrection)
            </td>
        </tr>
        @If Not IsNothing(Model.NoiseSetting.AlarmTriggerLevel) Then
            @<tr>
                <th>
                    Alarm Trigger Level
                </th>
                <td>
                    @Html.Raw(Model.NoiseSetting.AlarmTriggerLevel)
                </td>
            </tr>
        End If
        @If Not IsNothing(Model.NoiseSetting.FrequencyWeighting) Then
            @<tr>
                <th>
                    Frequency Weighting
                </th>
                <td>
                    @Html.DisplayFor(Function(model) model.NoiseSetting.FrequencyWeighting)
                </td>
            </tr>
        End If
        @If Not IsNothing(Model.NoiseSetting.TimeWeighting) Then
            @<tr>
                <th>
                    Time Weighting
                </th>
                <td>
                    @Html.DisplayFor(Function(model) model.NoiseSetting.TimeWeighting)
                </td>
            </tr>
        End If
        @<tr>
            <th>
                Sound Recording
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.NoiseSetting.SoundRecording)
            </td>
        </tr>


    End If

    @If Not IsNothing(Model.AdditionalInfo1) Then
        @<tr>
            <th>
                Additional Info 1
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.AdditionalInfo1)
            </td>
        </tr>
    End If
    @If Not IsNothing(Model.AdditionalInfo2) Then
        @<tr>
            <th>
                Additional Info 2
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.AdditionalInfo2)
            </td>
        </tr>
    End If

</table>