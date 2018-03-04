@ModelType List(Of SEC_Monitoring_Data_Website.Organisation)

@Code
    Dim showOrganisationLinks = DirectCast(ViewData("ShowOrganisationLinks"), Boolean)
    Dim showDeleteOrganisationLinks = DirectCast(ViewData("ShowDeleteOrganisationLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                Organisation Name
            </th>
        </tr>
        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showOrganisationLinks Then
                        @Html.RouteLink(currentItem.FullName,
                                "OrganisationDetailsRoute",
                                New With {.OrganisationRouteName = currentItem.getRouteName})
                    Else
                        @currentItem.FullName
                    End If
                </td>
                <td>
                    @If showDeleteOrganisationLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                     "OrganisationDeleteByIdRoute",
                                     New With {.OrganisationId = currentItem.Id},
                                     New With {.class = "DeleteOrganisationLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>
            </tr>
        Next
    </table>

Else
    @<p>
        There are no Organisations available to view.
    </p>
End If
