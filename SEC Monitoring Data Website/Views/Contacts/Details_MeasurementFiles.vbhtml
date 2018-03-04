@ModelType SEC_Monitoring_Data_Website.Contact

@Code
    Dim showMeasurementFileLinks = DirectCast(ViewData("ShowMeasurementFileLinks"), Boolean)
    Dim showMonitorLocationLinks = DirectCast(ViewData("ShowMonitorLocationLinks"), Boolean)
    Dim showMonitorLinks = DirectCast(ViewData("ShowMonitorLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            File Name
        </th>
        <th>
            Uploaded
        </th>
        <th>
            Upload Success?
        </th>
        <th>
            Monitor Location
        </th>
        <th>
            Monitor
        </th>
    </tr>

    @*Item Rows*@
    @For Each umf In Model.MeasurementFiles
        @<tr>
            <td>
                @If showMeasurementFileLinks Then
                    @Html.RouteLink(
                        umf.MeasurementFileName,
                        "MeasurementFileDetailsRoute",
                        New With {.MeasurementFileId = umf.Id}
                    )
                Else
                    @umf.MeasurementFileName
                End If
            </td>
            <td>
                @umf.UploadDateTime
            </td>
            <td>
                @umf.UploadSuccess
            </td>
            <td>
                @If showMonitorLocationLinks Then
                    @Html.RouteLink(
                        umf.MonitorLocation.MonitorLocationName,
                        "MonitorLocationDetailsRoute",
                        New With {.ProjectRouteName = umf.MonitorLocation.Project.getRouteName,
                                  .MonitorLocationRouteName = umf.MonitorLocation.getRouteName}
                    )
                Else
                    @umf.MonitorLocation.MonitorLocationName
                End If
            </td>
            <td>
                @If showMonitorLinks Then
                    @Html.RouteLink(
                        umf.Monitor.MonitorName,
                        "MonitorDetailsRoute",
                        New With {.MonitorRouteName = umf.Monitor.getRouteName}
                    )
                Else
                    @umf.Monitor.MonitorName
                End If
            </td>
        </tr>
    Next

</table>