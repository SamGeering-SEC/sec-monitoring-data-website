@ModelType SEC_Monitoring_Data_Website.EditOrganisationViewModel

<h3>Organisation's Projects</h3>
<table>
    <tr>
        <th>
            Project Name
        </th>
        <th>
        </th>
    </tr>
    @For Each p In Model.AllProjects
        Dim trIdOrganisationProject = "trOrganisationProject" + p.Id.ToString
        Dim trIdOrganisationNonProject = "trOrganisationNonProject" + p.Id.ToString
        @<tr id='@trIdOrganisationProject'>
            <td>
                @p.FullName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Projects",
                                "OrganisationRemoveProjectRoute",
                                New With {.OrganisationId = Model.Organisation.Id, .ProjectId = p.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                    .OnComplete = "$('#" + trIdOrganisationProject + "').hide();$('#" + trIdOrganisationNonProject + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getOrganisationProjectIds.Contains(p.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdOrganisationProject + "').hide(); </script>")
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
        Dim trIdOrganisationProject = "trOrganisationProject" + p.Id.ToString
        Dim trIdOrganisationNonProject = "trOrganisationNonProject" + p.Id.ToString
        @<tr id='@trIdOrganisationNonProject'>
            <td>
                @p.FullName
            </td>
            <td>
                @Ajax.RouteLink("Add to Projects",
                                "OrganisationAddProjectRoute",
                                New With {.OrganisationId = Model.Organisation.Id, .ProjectId = p.Id},
                                New AjaxOptions With {.HttpMethod = "PUT",
                                                    .OnComplete = "$('#" + trIdOrganisationProject + "').show();$('#" + trIdOrganisationNonProject + "').hide();"},
                                New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getOrganisationProjectIds.Contains(p.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdOrganisationNonProject + "').hide(); </script>")
        End If
    Next
</table>

