@ModelType SEC_Monitoring_Data_Website.MonitorDeploymentRecord

<table class="edit-table">
    <tr>
        <th>
            Monitor
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.Monitor.MonitorName)
        </td>
        <td></td>
    </tr>
    <tr>
        <th>
            Project
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MonitorLocation.Project.FullName)
        </td>
        <td></td>
    </tr>
    <tr>
        <th>
            Monitor Location
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MonitorLocation.MonitorLocationName)
        </td>
        <td></td>
    </tr>
    <tr>
        <th>
            Deployment Start Date
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.DeploymentStartDate)
        </td>
        <td></td>
    </tr>
    <tr>
        <th>
            Deployment End Date
        </th>
        <td>
            @Html.EditorFor(Function(model) model.DeploymentEndDate)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.DeploymentEndDate)
        </td>
    </tr>
</table>