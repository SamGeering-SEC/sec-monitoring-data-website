@ModelType SEC_Monitoring_Data_Website.EditDocumentTypeViewModel

<table class="edit-table">
    <tr>
        <th>
            Child Document Types
        </th>
        <th>
        </th>
    </tr>
    @For Each c In Model.AllChildDocumentTypes
        Dim trIdChildDocumentTypeDocumentType = "trChildDocumentTypeDocumentType" + c.Id.ToString
        Dim trIdChildDocumentTypeNonDocumentType = "trChildDocumentTypeNonDocumentType" + c.Id.ToString
        @<tr id='@trIdChildDocumentTypeDocumentType'>
            <td>
                @c.DocumentTypeName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Child DocumentTypes", _
                                "DocumentTypeRemoveChildDocumentTypeRoute", _
                                New With {.DocumentTypeRouteName = Model.DocumentType.getRouteName,
                                          .ChildDocumentTypeId = c.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                      .OnComplete = "event.preventDefault();$('#" + trIdChildDocumentTypeDocumentType + "').hide();$('#" + trIdChildDocumentTypeNonDocumentType + "').show();return false;"})

            </td>
        </tr>

        @If Model.getDocumentTypeChildDocumentTypeIds.Contains(c.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdChildDocumentTypeDocumentType + "').hide(); </script>")
        End If
    Next
</table>

<table class="edit-table">
    <tr>
        <th>
            Other Document Types
        </th>
        <th>
        </th>
    </tr>
    @For Each c In Model.AllChildDocumentTypes
        Dim trIdChildDocumentTypeDocumentType = "trChildDocumentTypeDocumentType" + c.Id.ToString
        Dim trIdChildDocumentTypeNonDocumentType = "trChildDocumentTypeNonDocumentType" + c.Id.ToString
        @<tr id='@trIdChildDocumentTypeNonDocumentType'>
            <td>
                @c.DocumentTypeName
            </td>
            <td>
                @Ajax.RouteLink("Add to Child Document Types",
                                "DocumentTypeAddChildDocumentTypeRoute",
                                New With {.DocumentTypeRouteName = Model.DocumentType.getRouteName,
                                          .ChildDocumentTypeId = c.Id},
                                New AjaxOptions With {.HttpMethod = "PUT",
                                                      .OnComplete = "$('#" + trIdChildDocumentTypeDocumentType + "').show();$('#" + trIdChildDocumentTypeNonDocumentType + "').hide();"})

            </td>
        </tr>

        @If Model.getDocumentTypeChildDocumentTypeIds.Contains(c.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdChildDocumentTypeNonDocumentType + "').hide(); </script>")
        End If
    Next
</table>



