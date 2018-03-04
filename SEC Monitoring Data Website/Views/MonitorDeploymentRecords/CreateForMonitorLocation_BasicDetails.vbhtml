@ModelType SEC_Monitoring_Data_Website.CreateMonitorDeploymentRecordViewModel

@Code
    Dim showProjectLink = DirectCast(ViewData("ShowProjectLink"), Boolean)
    Dim showMonitorLocationLink = DirectCast(ViewData("ShowMonitorLocationLink"), Boolean)
End Code

<table class="create-table">
    <tr>
    <tr>
        <th>
            Monitor
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.MonitorId, Model.MonitorList, "Please select a Monitor to Deploy...")
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MonitorId)
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
        <td></td>
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
        <td></td>
    </tr>
    <tr>
        <th>
            Deployment Start Date
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MonitorDeploymentRecord.DeploymentStartDate)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MonitorDeploymentRecord.DeploymentStartDate)
        </td>
    </tr>
</table>