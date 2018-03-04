@ModelType SEC_Monitoring_Data_Website.EditContactViewModel
<h3>Excluded Documents</h3>
<table>
    <tr>
        <th>
            Title
        </th>
        <th>
            Document Type
        </th>
        <th>
        </th>
    </tr>
    @For Each e In Model.AllExcludedDocuments
        Dim trIdExcludedDocumentContact = "trExcludedDocumentContact" + e.Id.ToString
        Dim trIdExcludedDocumentNonContact = "trExcludedDocumentNonContact" + e.Id.ToString
        @<tr id='@trIdExcludedDocumentContact'>
            <td>
                @e.Title
            </td>
            <td>
                @e.DocumentType.DocumentTypeName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Contact",
                                "ContactRemoveExcludedDocumentRoute",
                                New With {.ContactShortName = Model.Contact.getRouteName,
                                          .ExcludedDocumentId = e.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                      .OnComplete = "$('#" + trIdExcludedDocumentContact + "').hide();$('#" + trIdExcludedDocumentNonContact + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getContactExcludedDocumentIds.Contains(e.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdExcludedDocumentContact + "').hide(); </script>")
        End If
    Next
</table>

<h3>Other Documents</h3>
<table>
    <tr>
        <th>
            Title
        </th>
        <th>
            Document Type
        </th>
        <th>
        </th>
    </tr>
    @For Each e In Model.AllExcludedDocuments
        Dim trIdExcludedDocumentContact = "trExcludedDocumentContact" + e.Id.ToString
        Dim trIdExcludedDocumentNonContact = "trExcludedDocumentNonContact" + e.Id.ToString
        @<tr id='@trIdExcludedDocumentNonContact'>
            <td>
                @e.Title
            </td>
            <td>
                @e.DocumentType.DocumentTypeName
            </td>
            <td>
                @Ajax.RouteLink("Add to Contact",
                                "ContactAddExcludedDocumentRoute",
                                New With {.ContactShortName = Model.Contact.getRouteName,
                                          .ExcludedDocumentId = e.Id},
                                New AjaxOptions With {.HttpMethod = "PUT",
                                                      .OnComplete = "$('#" + trIdExcludedDocumentContact + "').show();$('#" + trIdExcludedDocumentNonContact + "').hide();"},
                                New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getContactExcludedDocumentIds.Contains(e.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdExcludedDocumentNonContact + "').hide(); </script>")
        End If
    Next
</table>

