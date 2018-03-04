@ModelType SEC_Monitoring_Data_Website.CreateMonitorDeploymentRecordViewModel

<h3>General Settings</h3>

@Html.Partial("Create_MS_GeneralSettings", Model)

<h3>Specific Settings</h3>

@Select Case Model.Monitor.MeasurementType.MeasurementTypeName

    Case "Noise"
        @Html.Partial("Create_MS_NoiseSetting", Model)
    Case "Vibration"
        @Html.Partial("Create_MS_VibrationSetting", Model)
Case "Air Quality, Dust and Meteorological"
        @Html.Partial("Create_MS_AirQualitySetting", Model)

End Select
