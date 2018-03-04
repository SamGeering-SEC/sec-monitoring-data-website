@ModelType SEC_Monitoring_Data_Website.UploadResultViewModel

@Code
    Dim showMonitorLink = DirectCast(ViewData("ShowMonitorLink"), Boolean)
    Dim showMonitorLocationLink = DirectCast(ViewData("ShowMonitorLocationLink"), Boolean)
End Code

<table class="details-table">
    <tr>
        <th>
            Measurement File Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MeasurementFile.MeasurementFileName)
        </td>
    </tr>
    <tr>
        <th>
            Upload Date Time
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MeasurementFile.UploadDateTime)
        </td>
    </tr>
    <tr>
        <th>
            Monitor
        </th>
        <td>
            @If showMonitorLink Then
                @Html.RouteLink(Model.MeasurementFile.Monitor.MonitorName,
                            "MonitorDetailsRoute",
                            New With {.MonitorRouteName = Model.MeasurementFile.Monitor.getRouteName})
            Else
                @Model.MeasurementFile.Monitor.MonitorName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Monitor Location
        </th>
        <td>
            @If showMonitorLocationLink Then
                @Html.RouteLink(Model.MeasurementFile.MonitorLocation.MonitorLocationName,
                            "MonitorLocationDetailsRoute",
                            New With {.ProjectRouteName = Model.MeasurementFile.MonitorLocation.Project.getRouteName,
                            .MonitorLocationRouteName = Model.MeasurementFile.MonitorLocation.getRouteName})
            Else
                @Model.MeasurementFile.MonitorLocation.MonitorLocationName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Measurement File Type
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MeasurementFile.MeasurementFileType.FileTypeName)
        </td>
    </tr>
</table>