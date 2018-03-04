@ModelType SEC_Monitoring_Data_Website.Document

@Code
    Dim showProjectLinks = DirectCast(ViewData("ShowProjectLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Project
        </th>
    </tr>

    @*Item Rows*@
    @For Each p In Model.Projects
        @<tr>
            <td>
                @If showProjectLinks Then
                    @Html.RouteLink(p.FullName,
                                "ProjectDetailsRoute",
                                New With {.ProjectRouteName = p.getRouteName})
                Else
                    @p.FullName
                End If
            </td>
        </tr>
    Next

</table>