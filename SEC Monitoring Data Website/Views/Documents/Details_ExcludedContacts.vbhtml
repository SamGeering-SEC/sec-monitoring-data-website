@ModelType SEC_Monitoring_Data_Website.Document

@Code
    Dim showExcludedContactLinks = DirectCast(ViewData("ShowExcludedContactLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Contact Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each ec In Model.ExcludedContacts
        @<tr>
            <td>
                @If showExcludedContactLinks Then
                    @Html.RouteLink(ec.ContactName,
                                "ContactDetailsRoute",
                                New With {.ContactRouteName = ec.getRouteName})
                Else
                    @ec.ContactName
                End If
            </td>
        </tr>
    Next

</table>