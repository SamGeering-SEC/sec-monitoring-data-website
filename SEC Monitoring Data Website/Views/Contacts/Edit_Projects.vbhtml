@ModelType SEC_Monitoring_Data_Website.EditContactViewModel

<h3>Contact's Projects</h3>
<table>
    <tr>
        <th>
            Project Name
        </th>
        <th>
        </th>
    </tr>
    @For Each p In Model.AllProjects
        Dim trIdProjectContact = "trProjectContact" + p.Id.ToString
        Dim trIdProjectNonContact = "trProjectNonContact" + p.Id.ToString
        @<tr id='@trIdProjectContact'>
            <td>
                @p.FullName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Projects",
                                "ContactRemoveProjectRoute",
                                New With {.ContactShortName = Model.Contact.getRouteName,
                                          .ProjectId = p.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                      .OnComplete = "$('#" + trIdProjectContact + "').hide();$('#" + trIdProjectNonContact + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getContactProjectIds.Contains(p.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdProjectContact + "').hide(); </script>")
        End If
    Next
</table>

<h3>Other Projects</h3>
<table>
    <tr>
        <th>
            Project Name
        </th>
        <th>
        </th>
    </tr>
    @For Each p In Model.AllProjects
        Dim trIdProjectContact = "trProjectContact" + p.Id.ToString
        Dim trIdProjectNonContact = "trProjectNonContact" + p.Id.ToString
        @<tr id='@trIdProjectNonContact'>
            <td>
                @p.FullName
            </td>
            <td>
                @Ajax.RouteLink("Add to Projects",
                                "ContactAddProjectRoute",
                                New With {.ContactShortName = Model.Contact.getRouteName,
                                          .ProjectId = p.Id},
                                New AjaxOptions With {.HttpMethod = "PUT",
                                                      .OnComplete = "$('#" + trIdProjectContact + "').show();$('#" + trIdProjectNonContact + "').hide();"},
                                New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getContactProjectIds.Contains(p.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdProjectNonContact + "').hide(); </script>")
        End If
    Next
</table>

