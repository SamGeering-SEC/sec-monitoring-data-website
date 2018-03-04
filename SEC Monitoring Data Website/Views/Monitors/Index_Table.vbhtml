@ModelType List(Of SEC_Monitoring_Data_Website.Monitor)

@Code
    Dim showMonitorLinks = DirectCast(ViewData("ShowMonitorLinks"), Boolean)
    Dim showDeleteMonitorLinks = DirectCast(ViewData("ShowDeleteMonitorLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">

        @*Header Row*@
        <tr>
            <th>
                Monitor Name
            </th>
            <th>
                Serial Number
            </th>
            <th>
                Measurement Type
            </th>
            <th>
                Online?
            </th>
            <th>
                Power Status Ok?
            </th>
            <th>

            </th>
        </tr>

        @*Item Rows*@
        @For Each m As SEC_Monitoring_Data_Website.Monitor In Model
            @<tr>
                <td>
                    @If showMonitorLinks Then
                        @Html.RouteLink(m.MonitorName,
                                        "MonitorDetailsRoute",
                                        New With {.MonitorRouteName = m.getRouteName})
                    End If
                </td>
                <td>
                    @m.SerialNumber
                </td>
                <td>
                    @m.MeasurementType.MeasurementTypeName
                </td>
                <td style="text-align:center">
                    @If m.CurrentStatus IsNot Nothing Then
                        @Html.DisplayFor(Function(modelItem) m.CurrentStatus.IsOnline)
                    Else
                        @Html.Raw("N/A")
                    End If
                </td>
                <td style="text-align:center">
                    @If m.CurrentStatus IsNot Nothing Then
                        @Html.DisplayFor(Function(modelItem) m.CurrentStatus.PowerStatusOk)
                    Else
                        @Html.Raw("N/A")
                    End If
                </td>
                <td>
                    @If showDeleteMonitorLinks Then
                        @If m.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                             "MonitorDeleteByIdRoute",
                                             New With {.MonitorId = m.Id},
                                             New With {.class = "DeleteMonitorLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>

            </tr>
        Next

    </table>


Else
    @<p>
        There are no Monitors available to view.
    </p>
End If
