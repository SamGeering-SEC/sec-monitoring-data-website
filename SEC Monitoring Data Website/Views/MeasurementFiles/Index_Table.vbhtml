@ModelType List(Of SEC_Monitoring_Data_Website.MeasurementFile)

@Code
    Dim showMeasurementFileLinks = DirectCast(ViewData("ShowMeasurementFileLinks"), Boolean)
    Dim showMonitorLinks = DirectCast(ViewData("ShowMonitorLinks"), Boolean)
    Dim showMonitorLocationLinks = DirectCast(ViewData("ShowMonitorLocationLinks"), Boolean)
    Dim showContactLinks = DirectCast(ViewData("ShowContactLinks"), Boolean)
    Dim showDeleteMeasurementFileLinks = DirectCast(ViewData("ShowDeleteMeasurementFileLinks"), Boolean)
End Code


@If Model.Count > 0 Then

    @<table>
        <tr>
            <th>
                File Name
            </th>
            <th>
                Uploaded On
            </th>
            <th style="text-align:center">
                Success?
            </th>
            <th>
                Monitor
            </th>
            <th>
                Location
            </th>
            <th>
                Uploaded By
            </th>
            <th>
                File Type
            </th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showMeasurementFileLinks Then
                        @Html.RouteLink(
                            currentItem.MeasurementFileName,
                            "MeasurementFileDetailsRoute",
                            New With {.MeasurementFileId = currentItem.Id}
                        )
                    Else
                        @currentItem.MeasurementFileName
                    End If
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) currentItem.UploadDateTime)
                </td>
                <td style="text-align:center">
                    @Html.DisplayFor(Function(modelItem) currentItem.UploadSuccess)
                </td>
                <td>
                    @If showMonitorLinks Then
                        @Html.RouteLink(
                            currentItem.Monitor.MonitorName,
                            "MonitorDetailsRoute",
                            New With {
                                .MonitorRouteName = currentItem.Monitor.getRouteName
                            }
                        )
                    Else
                        @currentItem.Monitor.MonitorName
                    End If
                </td>
                <td>
                    @If showMonitorLocationLinks Then
                        @Html.RouteLink(
                            currentItem.MonitorLocation.MonitorLocationName,
                            "MonitorLocationDetailsRoute",
                            New With {
                                .ProjectRouteName = currentItem.MonitorLocation.Project.getRouteName,
                                .MonitorLocationRouteName = currentItem.MonitorLocation.getRouteName
                            }
                        )
                    Else
                        @currentItem.MonitorLocation.MonitorLocationName
                    End If
                </td>
                <td>
                    @If showContactLinks Then
                        @Html.RouteLink(
                            currentItem.Contact.ContactName,
                            "ContactDetailsRoute",
                            New With {.ContactRouteName = currentItem.Contact.getRouteName}
                        )
                    Else
                        @currentItem.Contact.ContactName
                    End If
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) currentItem.MeasurementFileType.FileTypeName)
                </td>
                <td></td>
            </tr>
        Next

    </table>

Else

    @<p>
        There are no Measurement Files to View
    </p>

End If