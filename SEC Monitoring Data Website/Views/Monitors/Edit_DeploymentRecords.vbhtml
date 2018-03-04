@ModelType SEC_Monitoring_Data_Website.EditMonitorViewModel

@code
    Dim currentDeployment = Model.Monitor.getCurrentDeploymentRecord
End Code

@If currentDeployment Is Nothing Then

    @Html.RouteLink(
        "Deploy Monitor",
        "MonitorDeploymentRecordCreateForMonitorRoute",
        New With {.MonitorRouteName = Model.Monitor.getRouteName}
    )

Else

    @<h4>Current Deployment (started on @Format(currentDeployment.DeploymentStartDate, "dddd dd MMMM yyyy"))</h4>

    Select Case Model.Monitor.MeasurementType.MeasurementTypeName
        Case "Noise"
            @Html.Partial("Details_DR_NoiseSetting", currentDeployment.MonitorSettings)
Case "Vibration"
            @Html.Partial("Details_DR_VibrationSetting", currentDeployment.MonitorSettings)
Case "Air Quality, Dust and Meteorological"
            @Html.Partial("Details_DR_AirQualitySetting", currentDeployment.MonitorSettings)
End Select

    @<p>
        @Html.RouteLink(
            "End Current Deployment",
            "MonitorDeploymentRecordEndRoute",
            New With {.MonitorRouteName = Model.Monitor.getRouteName}
        )
    </p>
    
End If