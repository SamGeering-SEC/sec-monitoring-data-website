@ModelType SEC_Monitoring_Data_Website.MonitorLocation

@Code
    Dim showProjectLink = DirectCast(ViewData("ShowProjectLink"), Boolean)
End Code

<table class="details-table">
    <tr>
        <th>
            Project
        </th>
        <td>
            @If showProjectLink Then
                @Html.RouteLink(Model.Project.FullName,
                            "ProjectDetailsRoute",
                            New With {.ProjectRouteName = Model.Project.getRouteName})
            Else
                @Model.Project.FullName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Location Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MonitorLocationName)
        </td>
    </tr>
    <tr>
        <th>
            Coordinates
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MonitorLocationGeoCoords)
        </td>
    </tr>
    <tr>
        <th>
            Height Above Ground
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.HeightAboveGround)m
        </td>
    </tr>
    <tr>
        <th>
            Measurement Type
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MeasurementType.MeasurementTypeName)
        </td>
    </tr>
    @If Model.MeasurementType.MeasurementTypeName = "Noise" Then
        @<tr>
            <th>
                Facade Location?
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.IsAFacadeLocation)
            </td>
        </tr>
    End If

</table>