@ModelType Project

@Code
    Dim showOrganisationLinks = DirectCast(ViewData("ShowOrganisationLinks"), Boolean)
End Code

<table class="details-table">

    <tr>
        <th>
            Organisation Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each o In Model.Organisations.OrderBy(Function(org) org.FullName)
        @<tr>
            <td>
                @If showOrganisationLinks Then
                    @Html.RouteLink(o.FullName,
                                    "OrganisationDetailsRoute",
                                    New With {.OrganisationRouteName = o.getRouteName})
                Else
                    @o.FullName
                End If
            </td>
        </tr>
    Next

</table>