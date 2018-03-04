@Modeltype SEC_Monitoring_Data_Website.UpdateMeasurementCommentsIndexViewModel
<p>
    @Model.chart
</p>
<p>
    <button onclick="location.href='@Url.HttpRouteUrl("MeasurementCommentCreateRoute",
                                                      New With {.ProjectRouteName = Model.MonitorLocation.Project.getRouteName,
                                                                .MonitorLocationRouteName = Model.MonitorLocation.getRouteName,
                                                                .StartDateCode = Format(Model.StartDate, "yyyyMMdd"),
                                                                .EndDateCode = Format(Model.EndDate, "yyyyMMdd")})'">
        Add Comment
    </button>
</p>



