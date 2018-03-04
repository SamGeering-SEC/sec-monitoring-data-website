@ModelType SEC_Monitoring_Data_Website.Document

@Code
    Dim showMonitorLocationLinks = DirectCast(ViewData("ShowMonitorLocationLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Monitor Location Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each ml In Model.MonitorLocations
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
        </tr>
    Next

</table>