@ModelType SEC_Monitoring_Data_Website.MonitorLocation

@Code
    Dim currentDeployment = Model.getCurrentDeploymentRecord
    Dim previousDeployments = Model.getPreviousDeploymentRecords
    Dim showDeploymentRecordLinks = DirectCast(ViewData("ShowDeploymentRecordLinks"), Boolean)
End Code

@If Model.CurrentMonitor IsNot Nothing Then
    @<h3>Current Deployment (started on @Format(currentDeployment.DeploymentStartDate, "dddd dd MMMM yyyy"))</h3>

    Select Case Model.CurrentMonitor.MeasurementType.MeasurementTypeName
        Case "Noise"
    @Html.Partial("Details_DR_NoiseSetting", currentDeployment.MonitorSettings)
Case "Vibration"
    @Html.Partial("Details_DR_VibrationSetting", currentDeployment.MonitorSettings)
Case "Air Quality, Dust and Meteorological"
    @Html.Partial("Details_DR_AirQualitySetting", currentDeployment.MonitorSettings)
End Select

@<br />
@Html.JQueryUI.ActionButton(
    "View Record Details", "Details", "MonitorDeploymentRecords",
    New With {.MonitorRouteName = Model.CurrentMonitor.getRouteName,
              .DeploymentIndex = currentDeployment.getDeploymentIndex},
    Nothing
)

End If

@If previousDeployments.Count > 0 Then

    @<h4>Previous Deployments</h4>
    @<table class="details-table">
        <tr>
            <th>
                Start Date
            </th>
            <th>
                End Date
            </th>
            <th>
                Monitor
            </th>
            <th>

            </th>
        </tr>
    
        @For i As Integer = previousDeployments.Count - 1 To 0 Step -1
            @code
                Dim deployment = previousDeployments(i)
            End Code
            @<tr>
                <td>
                    @Format(deployment.DeploymentStartDate, "dd-MMM-yyyy")
                </td>
                <td>
                    @Format(deployment.DeploymentEndDate, "dd-MMM-yyyy")
                </td>
                <td>
                    @Html.RouteLink(deployment.Monitor.MonitorName,
                                    "MonitorDetailsRoute",
                                    New With {.MonitorRouteName = deployment.Monitor.MonitorName.ToRouteName})
                </td>
                <td>
                    @If showDeploymentRecordLinks Then
                        @Html.RouteLink("View Details",
                                        "MonitorDeploymentRecordDetailsRoute",
                                        New With {.MonitorRouteName = previousDeployments(i).Monitor.getRouteName,
                                                  .DeploymentIndex = i + 1})
                    End If
                </td>
            </tr>
        Next

    </table>
End If