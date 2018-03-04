@ModelType List(Of SEC_Monitoring_Data_Website.MonitorLocation)

@Code
    Dim showMonitorLocationLinks = DirectCast(ViewData("ShowMonitorLocationLinks"), Boolean)
    Dim showDeleteMonitorLocationLinks = DirectCast(ViewData("ShowDeleteMonitorLocationLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                Monitor Location Name
            </th>
            <th>
                Measurement Type
            </th>
            <th>
                Height Above Ground (m)
            </th>
            <th>
                Facade Location?
            </th>
            <th>

            </th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showMonitorLocationLinks Then
                        @Html.RouteLink(currentItem.MonitorLocationName,
                                "MonitorLocationDetailsRoute",
                                New With {.ProjectRouteName = currentItem.Project.getRouteName,
                                          .MonitorLocationRouteName = currentItem.getRouteName})
                    Else
                        @currentItem.MonitorLocationName
                    End If
                </td>
                <td>
                    @currentItem.MeasurementType.MeasurementTypeName
                </td>
                <td style="text-align:center">
                    @Html.DisplayFor(Function(modelItem) currentItem.HeightAboveGround)m
                </td>
                <td style="text-align:center">
                    @If currentItem.MeasurementType.MeasurementTypeName = "Noise" Then
                        @Html.DisplayFor(Function(modelItem) currentItem.IsAFacadeLocation)
                    End If
                </td>
                <td>
                    @If showDeleteMonitorLocationLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                        "MonitorLocationDeleteByIdRoute",
                                        New With {.MonitorLocationId = currentItem.Id},
                                        New With {.class = "DeleteMonitorLocationLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>

            </tr>
        Next
    </table>


Else
    @<p>
        There are no Monitor Locations available to view.
    </p>
End If
