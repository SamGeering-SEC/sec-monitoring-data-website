@ModelType SEC_Monitoring_Data_Website.EditDocumentTypeViewModel

<table class="edit-table">
    <tr>
        <th>
            Parent Document Types
        </th>
        <th>
        </th>
    </tr>
    @For Each c In Model.AllParentDocumentTypes
        Dim trIdParentDocumentTypeDocumentType = "trParentDocumentTypeDocumentType" + c.Id.ToString
        Dim trIdParentDocumentTypeNonDocumentType = "trParentDocumentTypeNonDocumentType" + c.Id.ToString
        @<tr id='@trIdParentDocumentTypeDocumentType'>
            <td>
                @c.DocumentTypeName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Parent DocumentTypes", _
                                "DocumentTypeRemoveParentDocumentTypeRoute", _
                                New With {.DocumentTypeRouteName = Model.DocumentType.getRouteName,
                                          .ParentDocumentTypeId = c.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                      .OnComplete = "event.preventDefault();$('#" + trIdParentDocumentTypeDocumentType + "').hide();$('#" + trIdParentDocumentTypeNonDocumentType + "').show();return false;"})

            </td>
        </tr>

        @If Model.getDocumentTypeParentDocumentTypeIds.Contains(c.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdParentDocumentTypeDocumentType + "').hide(); </script>")
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
    @For Each c In Model.AllParentDocumentTypes
        Dim trIdParentDocumentTypeDocumentType = "trParentDocumentTypeDocumentType" + c.Id.ToString
        Dim trIdParentDocumentTypeNonDocumentType = "trParentDocumentTypeNonDocumentType" + c.Id.ToString
        @<tr id='@trIdParentDocumentTypeNonDocumentType'>
            <td>
                @c.DocumentTypeName
            </td>
            <td>
                @Ajax.RouteLink("Add to Parent Document Types",
                                "DocumentTypeAddParentDocumentTypeRoute",
                                New With {.DocumentTypeRouteName = Model.DocumentType.getRouteName,
                                          .ParentDocumentTypeId = c.Id},
                                New AjaxOptions With {.HttpMethod = "PUT",
                                                      .OnComplete = "$('#" + trIdParentDocumentTypeDocumentType + "').show();$('#" + trIdParentDocumentTypeNonDocumentType + "').hide();"})

            </td>
        </tr>

        @If Model.getDocumentTypeParentDocumentTypeIds.Contains(c.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdParentDocumentTypeNonDocumentType + "').hide(); </script>")
        End If
    Next
</table>



