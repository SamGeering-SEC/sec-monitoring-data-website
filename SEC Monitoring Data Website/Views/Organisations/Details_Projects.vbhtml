@ModelType SEC_Monitoring_Data_Website.Organisation

@Code
    Dim showProjectLinks = DirectCast(ViewData("ShowProjectLinks"), Boolean)
    Dim showProjectClientOrganisationLinks = DirectCast(ViewData("ShowProjectClientOrganisationLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Project
        </th>
        <th>
            Client
        </th>
    </tr>

    @*Item Rows*@
    @For Each p In Model.Projects.OrderBy(Function(proj) proj.FullName)
        @<tr>
            <td>
                @If showProjectLinks Then
                    @Html.RouteLink(p.FullName,
                                "ProjectDetailsRoute",
                                New With {.ProjectRouteName = p.getRouteName})
                Else
                    @p.FullName
                End If
            </td>
            <td>
                @If showProjectClientOrganisationLinks Then
                    @Html.RouteLink(p.ClientOrganisation.FullName,
                                "OrganisationDetailsRoute",
                                New With {.OrganisationRouteName = p.ClientOrganisation.getRouteName})
                Else
                    @p.ClientOrganisation.FullName
                End If
            </td>
        </tr>
    Next

</table>