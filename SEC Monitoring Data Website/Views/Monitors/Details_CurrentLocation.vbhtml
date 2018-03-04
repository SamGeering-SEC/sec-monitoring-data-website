@ModelType SEC_Monitoring_Data_Website.Monitor

@Code
    Dim showCurrentLocationLink = DirectCast(ViewData("ShowCurrentLocationLink"), Boolean)
End Code

<table class="details-table">
    <tr>
        <th>
            Location Name
        </th>
        <td>
            @If showCurrentLocationLink Then
                @Html.RouteLink(Model.CurrentLocation.MonitorLocationName,
                            "MonitorLocationDetailsRoute",
                            New With {.ProjectRouteName = Model.CurrentLocation.Project.getRouteName,
                                      .MonitorLocationRouteName = Model.CurrentLocation.getRouteName})
            Else
                @Model.CurrentLocation.MonitorLocationName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Coordinates
        </th>
        <td>
            @Model.CurrentLocation.MonitorLocationGeoCoords.Latitude,@Model.CurrentLocation.MonitorLocationGeoCoords.Longitude
        </td>
    </tr>
    <tr>
        <th>
            Height Above Ground
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.CurrentLocation.HeightAboveGround)m
        </td>
    </tr>
    <tr>
        <th>
            Facade Location?
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.CurrentLocation.IsAFacadeLocation)
        </td>
    </tr>
</table>
