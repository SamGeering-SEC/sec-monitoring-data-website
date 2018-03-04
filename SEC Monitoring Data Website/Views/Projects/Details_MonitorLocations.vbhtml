@ModelType Project

@Code
    Dim showMonitorLocationLinks = DirectCast(ViewData("ShowMonitorLocationLinks"), Boolean)
    Dim showCurrentMonitorLinks = DirectCast(ViewData("ShowCurrentMonitorLinks"), Boolean)
    Dim showMonitorLocationMeasurementsLinks = DirectCast(ViewData("ShowMonitorLocationMeasurementsLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Monitor Location Name
        </th>
        <th>
            Measurement Type
        </th>
        <th>
            Monitor I.D.
        </th>
        <th>

        </th>
    </tr>

    @*Item Rows*@
    @For Each ml In Model.MonitorLocations.OrderBy(Function(l) l.MonitorLocationName)
        @<tr>
            <td>
                @If showMonitorLocationLinks Then
                    @Html.RouteLink(ml.MonitorLocationName,
                                "MonitorLocationDetailsRoute",
                                New With {.ProjectRouteName = ml.Project.getRouteName,
                                          .MonitorLocationRouteName = ml.getRouteName})
                Else
                    @ml.MonitorLocationName
                End If
            </td>
            <td>
                @ml.MeasurementType.MeasurementTypeName
            </td>

            <td style="text-align:center">
                @If showCurrentMonitorLinks Then
                    @If ml.CurrentMonitor IsNot Nothing Then
                        @Html.RouteLink(ml.CurrentMonitor.MonitorName,
                                    "MonitorDetailsRoute",
                                    New With {.MonitorRouteName = ml.CurrentMonitor.getRouteName})
                    Else
                        @Html.Raw("N/A")
                    End If
                Else
                    @If ml.CurrentMonitor IsNot Nothing Then
                        @ml.CurrentMonitor.MonitorName
                    Else
                        @Html.Raw("N/A")
                    End If
                End If
            </td>
            <td style="text-align:center">
                @If showMonitorLocationMeasurementsLinks Then
                    @If ml.hasMeasurements = True Then
                        @Html.RouteLink("View Measurements",
                                    "MeasurementViewRoute",
                                    New With {.ProjectRouteName = Model.getRouteName,
                                              .MonitorLocationRouteName = ml.getRouteName,
                                              .ViewRouteName = "Default",
                                              .ViewDuration = "Week",
                                              .strStartDate = Format(Date.Today, "dd-MMM-yyyy")})
                    End If
                End If
            </td>
        </tr>
    Next

</table>