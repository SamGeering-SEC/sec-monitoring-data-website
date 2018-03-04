@ModelType SEC_Monitoring_Data_Website.EditDocumentViewModel

<table class="edit-table">
    <tr>
        <th>
            Contacts excluded from viewing Document
        </th>
        <th>
        </th>
    </tr>
    @For Each e In Model.AllExcludedContacts
        Dim trIdExcludedContactDocument = "trExcludedContactDocument" + e.Id.ToString
        Dim trIdExcludedContactNonDocument = "trExcludedContactNonDocument" + e.Id.ToString
        @<tr id='@trIdExcludedContactDocument'>
            <td>
                @e.ContactName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Document",
                                "DocumentRemoveExcludedContactRoute",
                                New With {.DocumentId = Model.Document.Id,
                                          .ExcludedContactId = e.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                      .OnComplete = "$('#" + trIdExcludedContactDocument + "').hide();$('#" + trIdExcludedContactNonDocument + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getDocumentExcludedContactIds.Contains(e.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdExcludedContactDocument + "').hide(); </script>")
        End If
    Next
</table>

<table class="edit-table">
    <tr>
        <th>
            Other Contacts
        </th>
        <th>
        </th>
    </tr>
    @For Each e In Model.AllExcludedContacts
        Dim trIdExcludedContactDocument = "trExcludedContactDocument" + e.Id.ToString
        Dim trIdExcludedContactNonDocument = "trExcludedContactNonDocument" + e.Id.ToString
        @<tr id='@trIdExcludedContactNonDocument'>
            <td>
                @e.ContactName
            </td>
            <td>
                @Ajax.RouteLink("Add to Document",
                                "DocumentAddExcludedContactRoute",
                                New With {.DocumentId = Model.Document.Id,
                                          .ExcludedContactId = e.Id},
                                New AjaxOptions With {.HttpMethod = "PUT",
                                                      .OnComplete = "$('#" + trIdExcludedContactDocument + "').show();$('#" + trIdExcludedContactNonDocument + "').hide();"},
                                New With {.class = "sitewide-button-16 add-button-16"})
            </td>
        </tr>

        @If Model.getDocumentExcludedContactIds.Contains(e.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdExcludedContactNonDocument + "').hide(); </script>")
        End If
    Next
</table>

