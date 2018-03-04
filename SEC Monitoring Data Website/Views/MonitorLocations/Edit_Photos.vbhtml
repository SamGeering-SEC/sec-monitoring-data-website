@ModelType SEC_Monitoring_Data_Website.EditMonitorLocationViewModel

@Html.RouteLink(
    "Add a new Photo",
    "MonitorLocationDocumentCreateRoute",
    New With {.MonitorLocationId = Model.MonitorLocation.Id,
    .DocumentTypeName = "Photo"}
)
