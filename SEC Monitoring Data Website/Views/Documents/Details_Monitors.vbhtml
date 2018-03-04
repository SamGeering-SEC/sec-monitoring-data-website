@ModelType SEC_Monitoring_Data_Website.Document

@Code
    Dim showMonitorLinks = DirectCast(ViewData("ShowMonitorLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Monitor Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each m In Model.Monitors
        @<tr>
            <td>
                @If showMonitorLinks Then
                    @Html.RouteLink(m.MonitorName,
                                "MonitorDetailsRoute",
                                New With {.MonitorRouteName = m.getRouteName})
                Else
                    @m.MonitorName
                End If
            </td>
        </tr>
    Next

</table>