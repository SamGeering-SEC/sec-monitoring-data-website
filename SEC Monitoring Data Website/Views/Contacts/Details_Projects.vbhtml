@ModelType SEC_Monitoring_Data_Website.Contact

@Code
    Dim showProjectLinks = DirectCast(ViewData("ShowProjectLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Project Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each p In Model.Projects.OrderBy(Function(proj) proj.FullName)
        @<tr>
            <td>
                @If showProjectLinks Then
                    @Html.RouteLink(
                        p.FullName,
                        "ProjectDetailsRoute",
                        New With {.ProjectRouteName = p.getRouteName}
                    )
                Else
                    @p.FullName
                End If
            </td>
        </tr>
    Next

</table>