@ModelType SEC_Monitoring_Data_Website.EditDocumentViewModel

<table class="edit-table">
    <tr>
        <th>
            Projects related to Document
        </th>
        <th>
        </th>
    </tr>
    @For Each p In Model.AllProjects
        Dim trIdProjectDocument = "trProjectDocument" + p.Id.ToString
        Dim trIdProjectNonDocument = "trProjectNonDocument" + p.Id.ToString
        @<tr id='@trIdProjectDocument'>
            <td>
                @p.FullName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Document", "DocumentRemoveProjectRoute", New With {.DocumentId = Model.Document.Id,
                                                                                              .ProjectId = p.Id},
                                                                                    New AjaxOptions With {.HttpMethod = "DELETE",
                                                                                                          .OnComplete = "$('#" + trIdProjectDocument + "').hide();$('#" + trIdProjectNonDocument + "').show();"},
                                                                                                      New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getDocumentProjectIds.Contains(p.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdProjectDocument + "').hide(); </script>")
        End If
    Next
</table>

<table class="edit-table">
    <tr>
        <th>
            Other Projects
        </th>
        <th>
        </th>
    </tr>
    @For Each p In Model.AllProjects
        Dim trIdProjectDocument = "trProjectDocument" + p.Id.ToString
        Dim trIdProjectNonDocument = "trProjectNonDocument" + p.Id.ToString
        @<tr id='@trIdProjectNonDocument'>
            <td>
                @p.FullName
            </td>
            <td>
                @Ajax.RouteLink("Add to Document", "DocumentAddProjectRoute", New With {.DocumentId = Model.Document.Id,
                                                                                              .ProjectId = p.Id},
                                                                                    New AjaxOptions With {.HttpMethod = "PUT",
                                                                                                          .OnComplete = "$('#" + trIdProjectDocument + "').show();$('#" + trIdProjectNonDocument + "').hide();"},
                                                                                                      New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getDocumentProjectIds.Contains(p.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdProjectNonDocument + "').hide(); </script>")
        End If
    Next
</table>

