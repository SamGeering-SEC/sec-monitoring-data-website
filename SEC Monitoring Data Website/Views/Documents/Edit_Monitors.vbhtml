@ModelType SEC_Monitoring_Data_Website.EditDocumentViewModel

<table class="edit-table">
    <tr>
        <th>
            Monitors related to Document
        </th>
        <th>
        </th>
    </tr>
    @For Each m In Model.AllMonitors
        Dim trIdMonitorDocument = "trMonitorDocument" + m.Id.ToString
        Dim trIdMonitorNonDocument = "trMonitorNonDocument" + m.Id.ToString
        @<tr id='@trIdMonitorDocument'>
            <td>
                @Html.Raw(m.MonitorName + If(m.CurrentLocation IsNot Nothing, " (" + m.CurrentLocation.MonitorLocationName + ")", ""))
            </td>
            <td>
                @Ajax.RouteLink("Remove from Document", "DocumentRemoveMonitorRoute", New With {.DocumentId = Model.Document.Id,
                                                                                              .MonitorId = m.Id},
                                                                                    New AjaxOptions With {.HttpMethod = "DELETE",
                                                                                                          .OnComplete = "$('#" + trIdMonitorDocument + "').hide();$('#" + trIdMonitorNonDocument + "').show();"},
                                                                                                      New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getDocumentMonitorIds.Contains(m.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdMonitorDocument + "').hide(); </script>")
        End If
    Next
</table>

<table class="edit-table">
    <tr>
        <th>
            Other Monitors
        </th>
        <th>
        </th>
    </tr>
    @For Each m In Model.AllMonitors
        Dim trIdMonitorDocument = "trMonitorDocument" + m.Id.ToString
        Dim trIdMonitorNonDocument = "trMonitorNonDocument" + m.Id.ToString
        @<tr id='@trIdMonitorNonDocument'>
            <td>
                @Html.Raw(m.MonitorName + If(m.CurrentLocation IsNot Nothing, " ("+m.CurrentLocation.MonitorLocationName+")", ""))
            </td>
            <td>
                @Ajax.RouteLink("Add to Document", "DocumentAddMonitorRoute", New With {.DocumentId = Model.Document.Id,
                                                                                              .MonitorId = m.Id},
                                                                                    New AjaxOptions With {.HttpMethod = "PUT",
                                                                                                          .OnComplete = "$('#" + trIdMonitorDocument + "').show();$('#" + trIdMonitorNonDocument + "').hide();"},
                                                                                                      New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getDocumentMonitorIds.Contains(m.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdMonitorNonDocument + "').hide(); </script>")
        End If
    Next
</table>

