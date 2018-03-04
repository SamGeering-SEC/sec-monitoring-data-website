@modelType SEC_Monitoring_Data_Website.EditProjectViewModel

<h3>Project Organisations</h3>
<table class="edit-table">

    @For Each o In Model.AllOrganisations
        Dim trIdOrganisationProject = "trOrganisationProject" + o.Id.ToString
        Dim trIdOrganisationNonProject = "trOrganisationNonProject" + o.Id.ToString
        @<tr id='@trIdOrganisationProject'>
            <td>
                @o.FullName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Project", "ProjectRemoveOrganisationRoute", New With {.ProjectRouteName = Model.Project.getRouteName,
                                                                                              .OrganisationId = o.Id},
                                                                                    New AjaxOptions With {.HttpMethod = "DELETE",
                                                                                                          .OnComplete = "$('#" + trIdOrganisationProject + "').hide();$('#" + trIdOrganisationNonProject + "').show();"},
                                                                                    New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getProjectOrganisationIds.Contains(o.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdOrganisationProject + "').hide(); </script>")
        End If
    Next
</table>

<h3>Non-Project Organisations</h3>
<table class="edit-table">

    @For Each o In Model.AllOrganisations
        Dim trIdOrganisationProject = "trOrganisationProject" + o.Id.ToString
        Dim trIdOrganisationNonProject = "trOrganisationNonProject" + o.Id.ToString
        @<tr id='@trIdOrganisationNonProject'>
            <td>
                @o.FullName
            </td>
            <td>
                @Ajax.RouteLink("Add to Project", "ProjectAddOrganisationRoute", New With {.ProjectRouteName = Model.Project.getRouteName,
                                                                                              .OrganisationId = o.Id},
                                                                                    New AjaxOptions With {.HttpMethod = "PUT",
                                                                                                          .OnComplete = "$('#" + trIdOrganisationProject + "').show();$('#" + trIdOrganisationNonProject + "').hide();"},
                                                                                                      New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getProjectOrganisationIds.Contains(o.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdOrganisationNonProject + "').hide(); </script>")
        End If
    Next
</table>



