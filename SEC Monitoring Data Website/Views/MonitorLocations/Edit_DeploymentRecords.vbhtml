@ModelType SEC_Monitoring_Data_Website.EditMonitorLocationViewModel

@If Model.MonitorLocation.CurrentMonitor Is Nothing Then

    @Html.RouteLink(
        "Deploy a Monitor to this Location",
        "MonitorDeploymentRecordCreateForMonitorLocationRoute",
        New With {.ProjectRouteName = Model.Project.getRouteName,
                  .MonitorLocationRouteName = Model.MonitorLocation.getRouteName}
    )

Else

    @Html.RouteLink(
        "End Current Deployment",
        "MonitorDeploymentRecordEndRoute",
        New With {.MonitorRouteName = Model.MonitorLocation.CurrentMonitor.getRouteName}
    )

End If