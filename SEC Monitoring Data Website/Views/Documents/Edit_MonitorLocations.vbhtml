@ModelType SEC_Monitoring_Data_Website.EditDocumentViewModel

<table class="edit-table">
    <tr>
        <th>
            Monitor Locations related to Document
        </th>
        <th>
        </th>
    </tr>
    @For Each m In Model.AllMonitorLocations
        Dim trIdMonitorLocationDocument = "trMonitorLocationDocument" + m.Id.ToString
        Dim trIdMonitorLocationNonDocument = "trMonitorLocationNonDocument" + m.Id.ToString
        @<tr id='@trIdMonitorLocationDocument'>
            <td>
                @Html.Raw(m.MonitorLocationName + If(m.CurrentMonitor IsNot Nothing, " (" + m.CurrentMonitor.MonitorName + ")", ""))
            </td>
            <td>
                @Ajax.RouteLink("Remove from Document", "DocumentRemoveMonitorLocationRoute", New With {.DocumentId = Model.Document.Id,
                                                                                              .MonitorLocationId = m.Id},
                                                                                    New AjaxOptions With {.HttpMethod = "DELETE",
                                                                                                          .OnComplete = "$('#" + trIdMonitorLocationDocument + "').hide();$('#" + trIdMonitorLocationNonDocument + "').show();"},
                                                                                                      New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getDocumentMonitorLocationIds.Contains(m.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdMonitorLocationDocument + "').hide(); </script>")
        End If
    Next
</table>

<table class="edit-table">
    <tr>
        <th>
            Other Monitor Locations
        </th>
        <th>
        </th>
    </tr>
    @For Each m In Model.AllMonitorLocations
        Dim trIdMonitorLocationDocument = "trMonitorLocationDocument" + m.Id.ToString
        Dim trIdMonitorLocationNonDocument = "trMonitorLocationNonDocument" + m.Id.ToString
        @<tr id='@trIdMonitorLocationNonDocument'>
            <td>
                @Html.Raw(m.MonitorLocationName + If(m.CurrentMonitor IsNot Nothing, " ("+m.CurrentMonitor.MonitorName+")", ""))
            </td>
            <td>
                @Ajax.RouteLink("Add to Document", "DocumentAddMonitorLocationRoute", New With {.DocumentId = Model.Document.Id,
                                                                                              .MonitorLocationId = m.Id},
                                                                                    New AjaxOptions With {.HttpMethod = "PUT",
                                                                                                          .OnComplete = "$('#" + trIdMonitorLocationDocument + "').show();$('#" + trIdMonitorLocationNonDocument + "').hide();"},
                                                                                                      New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getDocumentMonitorLocationIds.Contains(m.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdMonitorLocationNonDocument + "').hide(); </script>")
        End If
    Next
</table>

