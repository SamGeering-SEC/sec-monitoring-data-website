@ModelType SEC_Monitoring_Data_Website.Organisation

@Code
    Dim showOwnedMonitorLinks = DirectCast(ViewData("ShowOwnedMonitorLinks"), Boolean)
End Code

<table class="scrolling-table normal-table">

    @*Header Row*@
    <thead>
        <tr>
            <th style="width:50%">
                Monitor Name
            </th>
            <th style="width:50%">
                Measurement Type
            </th>
        </tr>
    </thead>


    @*Item Rows*@
    <tbody>
        @For Each om In Model.OwnedMonitors.OrderBy(Function(mon) mon.MonitorName)
            @<tr>
                <td style="width:50%">
                    @If showOwnedMonitorLinks Then
                        @Html.RouteLink(om.MonitorName,
                                     "MonitorDetailsRoute",
                                     New With {.MonitorRouteName = om.getRouteName})
                    Else
                        @om.MonitorName
                     End If
                </td>
                <td style="width:50%">
                    @om.MeasurementType.MeasurementTypeName
                </td>
            </tr>
        Next
    </tbody>

</table>