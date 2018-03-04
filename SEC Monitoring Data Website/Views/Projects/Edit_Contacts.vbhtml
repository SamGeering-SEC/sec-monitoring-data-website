@ModelType SEC_Monitoring_Data_Website.EditProjectViewModel

<h3>Project Contacts</h3>
<table class="edit-table">
    <tr>
        <th>
            Contact
        </th>
        <th>
            Organisation
        </th>
        <th>
        </th>
    </tr>
    @For Each c In Model.AllContacts
        Dim trIdContactProject = "trContactProject" + c.Id.ToString
        Dim trIdContactNonProject = "trContactNonProject" + c.Id.ToString
        @<tr id='@trIdContactProject'>
            <td>
                @c.ContactName
            </td>
            <td>
                @c.Organisation.FullName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Project",
                                "ProjectRemoveContactRoute",
                                New With {.ProjectRouteName = Model.Project.getRouteName,
                                          .ContactId = c.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                        .OnComplete = "$('#" + trIdContactProject + "').hide();$('#" + trIdContactNonProject + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})
            </td>
        </tr>

        @If Model.getProjectContactIds.Contains(c.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdContactProject + "').hide(); </script>")
        End If
    Next
</table>

<h3>Non-Project Contacts</h3>
<table class="edit-table">
    <tr>
        <th>
            Contact
        </th>
        <th>
            Organisation
        </th>
        <th>
        </th>
    </tr>
    @For Each c In Model.AllContacts
        Dim trIdContactProject = "trContactProject" + c.Id.ToString
        Dim trIdContactNonProject = "trContactNonProject" + c.Id.ToString
        @<tr id='@trIdContactNonProject'>
            <td>
                @c.ContactName
            </td>
            <td>
                @c.Organisation.FullName
            </td>
            <td>
                @Ajax.RouteLink("Add to Project", "ProjectAddContactRoute", New With {.ProjectRouteName = Model.Project.getRouteName,
                                                                                              .ContactId = c.Id},
                                                                                    New AjaxOptions With {.HttpMethod = "PUT",
                                                                                                          .OnComplete = "$('#" + trIdContactProject + "').show();$('#" + trIdContactNonProject + "').hide();"},
                                                                            New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getProjectContactIds.Contains(c.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdContactNonProject + "').hide(); </script>")
        End If
    Next
</table>
