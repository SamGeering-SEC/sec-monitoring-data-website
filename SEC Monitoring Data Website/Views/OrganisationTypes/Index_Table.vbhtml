@ModelType List(Of SEC_Monitoring_Data_Website.OrganisationType)

@Code
    Dim showOrganisationLinks = DirectCast(ViewData("ShowOrganisationLinks"), Boolean)
    Dim showDeleteOrganisationLinks = DirectCast(ViewData("ShowDeleteOrganisationLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                Organisation Type Name
            </th>
            <th></th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showOrganisationLinks Then
                        @Html.RouteLink(currentItem.OrganisationTypeName,
                                    "OrganisationTypeDetailsRoute",
                                    New With {.OrganisationTypeRouteName = currentItem.getRouteName})
                    Else
                        @currentItem.OrganisationTypeName
                    End If
                </td>
                <td>
                    @If showDeleteOrganisationLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                         "OrganisationTypeDeleteByIdRoute",
                                         New With {.OrganisationTypeId = currentItem.Id},
                                         New With {.class = "DeleteOrganisationTypeLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>
            </tr>
        Next
    </table>

Else
    @<p>
        There are no Organisation Types available to view.
    </p>
End If
