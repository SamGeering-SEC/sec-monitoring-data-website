@ModelType SEC_Monitoring_Data_Website.Organisation

@Code
    Dim showContactLinks = DirectCast(ViewData("ShowContactLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Contact Name
        </th>
        <th>
            Email Address
        </th>
        <th>
            Telephone Number
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
                @c.EmailAddress
            </td>
            <td>
                @c.PrimaryTelephoneNumber
            </td>
        </tr>
    Next

</table>