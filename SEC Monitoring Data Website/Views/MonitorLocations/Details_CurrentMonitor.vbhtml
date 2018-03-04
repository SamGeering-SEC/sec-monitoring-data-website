@ModelType SEC_Monitoring_Data_Website.Monitor

@Code
    Dim showCurrentMonitorLink = DirectCast(ViewData("ShowCurrentMonitorLink"), Boolean)
    Dim showCurrentMonitorOwnerOrganisationLink = DirectCast(ViewData("ShowCurrentMonitorOwnerOrganisationLink"), Boolean)
End Code

<table class="details-table">
    <tr>
        <th>
            Monitor Name
        </th>
        <td>
            @If showCurrentMonitorLink Then
                @Html.RouteLink(Model.MonitorName,
                            "MonitorDetailsRoute",
                            New With {.MonitorRouteName = Model.getRouteName})
            Else
                @Model.MonitorName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Manufacturer
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.Manufacturer)
        </td>
    </tr>
    <tr>
        <th>
            Model
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.Model)
        </td>
    </tr>
    <tr>
        <th>
            Serial Number
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.SerialNumber)
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
    <tr>
        <th>
            Last Field Calibration
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.LastFieldCalibration)
        </td>
    </tr>
    <tr>
        <th>
            Last Full Calibration
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.LastFullCalibration)
        </td>
    </tr>
    <tr>
        <th>
            Next Full Calibration
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.NextFullCalibration)
        </td>
    </tr>
    <tr>
        <th>
            Owner Organisation
        </th>
        <td>
            @If showCurrentMonitorOwnerOrganisationLink Then
                @Html.RouteLink(Model.OwnerOrganisation.FullName,
                            "OrganisationDetailsRoute",
                            New With {.OrganisationRouteName = Model.OwnerOrganisation.getRouteName})
            Else
                @Model.OwnerOrganisation.FullName
            End If
        </td>
    </tr>
</table>

<p>

</p>
