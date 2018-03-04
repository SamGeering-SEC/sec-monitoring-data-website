@ModelType List(Of SEC_Monitoring_Data_Website.Contact)

@Code
    Dim showContactLinks = DirectCast(ViewData("ShowContactLinks"), Boolean)
    Dim showOrganisationLinks = DirectCast(ViewData("ShowOrganisationLinks"), Boolean)
    Dim showDeleteContactLinks = DirectCast(ViewData("ShowDeleteContactLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                Contact Name
            </th>
            <th>
                Email Address
            </th>
            <th>
                Telephone
            </th>
            <th>
                Organisation
            </th>
            <th></th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showContactLinks Then
                        @Html.RouteLink(currentItem.ContactName,
                                "ContactDetailsRoute",
                                New With {.ContactRouteName = currentItem.getRouteName})
                    Else
                        @currentItem.ContactName
                    End If
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) currentItem.EmailAddress)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) currentItem.PrimaryTelephoneNumber)
                </td>
                <td>
                    @If showOrganisationLinks Then
                        @Html.RouteLink(currentItem.Organisation.FullName,
                                "OrganisationDetailsRoute",
                                New With {.OrganisationRouteName = currentItem.Organisation.getRouteName})
                    Else
                        @currentItem.Organisation.FullName
                    End If
                </td>
                <td>
                    @If showDeleteContactLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                     "ContactDeleteByIdRoute",
                                     New With {.ContactId = currentItem.Id},
                                     New With {.class = "DeleteContactLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>
            </tr>
        Next

    </table>


Else
    @<p>
        There are no Contacts available to view.
    </p>
End If
