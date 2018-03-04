@ModelType SEC_Monitoring_Data_Website.MonitorDeploymentRecord

@Code
    Dim showProjectLink = DirectCast(ViewData("ShowProjectLink"), Boolean)
    Dim showMonitorLink = DirectCast(ViewData("ShowMonitorLink"), Boolean)
    Dim showMonitorLocationLink = DirectCast(ViewData("ShowMonitorLocationLink"), Boolean)
End Code

<table class="details-table">
    <tr>
        <th>
            Monitor
        </th>
        <td>
            @If showMonitorLink Then
                @Html.RouteLink(Model.Monitor.MonitorName,
                            "MonitorDetailsRoute",
                            New With {.MonitorRouteName = Model.Monitor.getRouteName})
            Else
                @Model.Monitor.MonitorName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Project
        </th>
        <td>
            @If showProjectLink Then
                @Html.RouteLink(Model.MonitorLocation.Project.FullName,
                            "ProjectDetailsRoute",
                            New With {.ProjectRouteName = Model.MonitorLocation.Project.getRouteName})
            Else
                @Model.MonitorLocation.Project.FullName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Monitor Location
        </th>
        <td>
            @If showMonitorLocationLink Then
                @Html.RouteLink(Model.MonitorLocation.MonitorLocationName,
                            "MonitorLocationDetailsRoute",
                            New With {.ProjectRouteName = Model.MonitorLocation.Project.getRouteName,
                                      .MonitorLocationRouteName = Model.MonitorLocation.getRouteName})
            Else
                @Model.MonitorLocation.MonitorLocationName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Deployment Start Date
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.DeploymentStartDate)
        </td>
    </tr>
    <tr>
        <th>
            @If Not Model.DeploymentEndDate Is Nothing Then
                @Html.Raw("Deployment End Date")
            End If
        </th>
        <td>
            @If Not Model.DeploymentEndDate Is Nothing Then
                @Html.DisplayFor(Function(model) model.DeploymentEndDate)
            End If
        </td>
    </tr>
</table>