@ModelType SEC_Monitoring_Data_Website.OrganisationType

@Code
    Dim showOrganisationLinks = DirectCast(ViewData("ShowOrganisationLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Organisation
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