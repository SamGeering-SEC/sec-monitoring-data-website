@ModelType SEC_Monitoring_Data_Website.Monitor

@Code
    Dim currentDeployment = Model.getCurrentDeploymentRecord
    Dim previousDeployments = Model.getPreviousDeploymentRecords
    Dim showDeploymentRecordLinks = DirectCast(ViewData("ShowDeploymentRecordLinks"), Boolean)
End Code

@If Model.isDeployed = True Then
    @<h4>Current Deployment (started on @Format(currentDeployment.DeploymentStartDate, "dddd dd MMMM yyyy"))</h4>

    Select Case Model.MeasurementType.MeasurementTypeName
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
    New With {.MonitorRouteName = Model.getRouteName,
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
                Monitor Location
            </th>
            <th>

            </th>
        </tr>

        @For i As Integer = previousDeployments.Count - 1 To 0 Step -1
            @code
                Dim monitorLocation = previousDeployments(i).MonitorLocation
            End Code
            @<tr>
                <td>
                    @Format(previousDeployments(i).DeploymentStartDate, "dd-MMM-yyyy")
                </td>
                <td>
                    @Format(previousDeployments(i).DeploymentEndDate, "dd-MMM-yyyy")
                </td>
                <td>
                    @Html.RouteLink(
                        monitorLocation.MonitorLocationName,
                        "MonitorLocationDetailsRoute",
                        New With {.ProjectRouteName = monitorLocation.Project.ShortName.ToRouteName,
                                    .MonitorLocationRouteName = monitorLocation.MonitorLocationName.ToRouteName}
                    )
                </td>
                <td>
                    @If showDeploymentRecordLinks Then
                        @Html.RouteLink(
                            "View Details",
                            "MonitorDeploymentRecordDetailsRoute",
                            New With {.MonitorRouteName = Model.getRouteName, .DeploymentIndex = i + 1}
                        )
                    End If
                </td>
            </tr>
        Next

    </table>
End If