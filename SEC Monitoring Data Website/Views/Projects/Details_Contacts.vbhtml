@ModelType Project

@Code
    Dim showContactLinks = DirectCast(ViewData("ShowContactLinks"), Boolean)
    Dim showContactOrganisationLinks = DirectCast(ViewData("ShowContactOrganisationLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Contact Name
        </th>
        <th>
            Organisation
        </th>
    </tr>

    @*Item Rows*@
    @For Each c In Model.Contacts.OrderBy(Function(con) con.ContactName)
        @<tr>
            <td>
                @If showContactLinks Then
                    @Html.RouteLink(c.ContactName,
                                "ContactDetailsRoute",
                                New With {.ContactRouteName = c.getRouteName})
                Else
                    @c.ContactName
                End If
            </td>
            <td>
                @If showContactOrganisationLinks Then
                    @Html.RouteLink(c.Organisation.FullName,
                                "OrganisationDetailsRoute",
                                New With {.OrganisationRouteName = c.Organisation.getRouteName})
                Else
                    @c.Organisation.FullName
                End If
            </td>
        </tr>
    Next

</table>