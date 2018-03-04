@ModelType List(Of SEC_Monitoring_Data_Website.MonitorDeploymentRecord)

@Code
    Dim showProjectLinks = DirectCast(ViewData("ShowProjectLinks"), Boolean)
    Dim showMonitorLinks = DirectCast(ViewData("ShowMonitorLinks"), Boolean)
    Dim showMonitorLocationLinks = DirectCast(ViewData("ShowMonitorLocationLinks"), Boolean)
    Dim showDeleteDeploymentRecordLinks = DirectCast(ViewData("ShowDeleteDeploymentRecordLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                Monitor
            </th>
            <th>
                Project
            </th>
            <th>
                Monitor Location
            </th>
            <th style="text-align:center">
                Start Date
            </th>
            <th style="text-align:center">
                End Date
            </th>
            <th></th>
            <th></th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showMonitorLinks Then
                        @Html.RouteLink(currentItem.Monitor.MonitorName,
                                    "MonitorDetailsRoute",
                                    New With {.MonitorRouteName = currentItem.Monitor.getRouteName})
                    Else
                        @currentItem.Monitor.MonitorName
                    End If
                </td>
                <td>
                    @If showProjectLinks = True Then
                        @Html.RouteLink(currentItem.MonitorLocation.Project.FullName,
                                    "ProjectDetailsRoute",
                                    New With {.ProjectRouteName = currentItem.MonitorLocation.Project.getRouteName})
                    Else
                        @currentItem.MonitorLocation.Project.FullName
                    End If
                </td>
                <td>
                    @If showMonitorLocationLinks Then
                        @Html.RouteLink(currentItem.MonitorLocation.MonitorLocationName,
                                    "MonitorLocationDetailsRoute",
                                    New With {.ProjectRouteName = currentItem.MonitorLocation.Project.getRouteName,
                                              .MonitorLocationRouteName = currentItem.MonitorLocation.getRouteName})
                    Else
                        @currentItem.MonitorLocation.MonitorLocationName
                    End If
                </td>
                <td style="text-align:center">
                    @Format(currentItem.DeploymentStartDate, "ddd dd-MMM-yyyy")
                </td>
                <td style="text-align:center">
                    @If currentItem.DeploymentEndDate IsNot Nothing Then
                        @Format(currentItem.DeploymentEndDate, "ddd dd-MMM-yyyy")
                    Else
                        @Html.Raw("N/A")
                    End If

                </td>
                <td>
                    @Html.RouteLink("View Details",
                                    "MonitorDeploymentRecordDetailsRoute",
                                    New With {.MonitorRouteName = currentItem.Monitor.getRouteName,
                                              .DeploymentIndex = currentItem.getDeploymentIndex})
                </td>
                <td>
                    @If showDeleteDeploymentRecordLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                        "MonitorDeploymentRecordDeleteByIdRoute",
                                        New With {.MonitorDeploymentRecordId = currentItem.Id},
                                        New With {.class = "DeleteMonitorDeploymentRecordLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>
            </tr>
        Next
    </table>

Else
    @<p>
        There are no Monitor Deployment Records available to view.
    </p>
End If
