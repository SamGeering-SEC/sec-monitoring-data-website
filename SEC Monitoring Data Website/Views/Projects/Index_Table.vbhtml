@ModelType IEnumerable(Of SEC_Monitoring_Data_Website.Project)

@Code
    Dim showProjectLinks = DirectCast(ViewData("ShowProjectLinks"), Boolean)
    Dim showClientOrganisationLinks = DirectCast(ViewData("ShowClientOrganisationLinks"), Boolean)
    Dim showMonitorLocationsLinks = DirectCast(ViewData("ShowMonitorLocationsLinks"), Boolean)
    Dim showDeleteProjectLinks = DirectCast(ViewData("ShowDeleteProjectLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class='index-table'>
        <tr>
            <th>
                @Html.Label("Project Name")
            </th>
            <th>
                @Html.Label("Client")
            </th>
            <td></td>
            <td></td>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showProjectLinks Then
                        @Html.RouteLink(currentItem.FullName,
                                    "ProjectDetailsRoute",
                                    New With {.ProjectRouteName = currentItem.getRouteName})
                    Else
                        @currentItem.FullName
                    End If
                </td>
                <td>
                    @If showClientOrganisationLinks Then
                        @Html.RouteLink(currentItem.ClientOrganisation.ShortName,
                                    "OrganisationDetailsRoute",
                                    New With {.controller = "Organisations",
                                              .OrganisationRouteName = currentItem.ClientOrganisation.getRouteName})
                    Else
                        @currentItem.ClientOrganisation.ShortName
                    End If
                </td>
                <td>
                    @If showMonitorLocationsLinks Then

                        @If currentItem.MonitorLocations.Count > 0 Then
                            @Html.JQueryUI.ActionButton(
                                "View Monitor Locations", "Select", "MonitorLocations",
                                New With {.ProjectRouteName = currentItem.getRouteName},
                                Nothing
                            )
                        End If
                    End If
                </td>
                <td>
                    @If showDeleteProjectLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                        "ProjectDeleteByIdRoute",
                                        New With {.ProjectId = currentItem.Id},
                                        New With {.class = "DeleteProjectLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>
            </tr>
        Next
    </table>

Else
    @<p>
        There are no Projects available to view.
    </p>
End If