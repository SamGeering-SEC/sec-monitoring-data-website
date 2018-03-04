@ModelType UserAccessLevel

@Code
    Dim showUserLinks = DirectCast(ViewData("ShowUserLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            User Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each u In Model.Users.OrderBy(Function(user) user.UserName)
        @<tr>
            <td>
                @If showUserLinks Then
                    @Html.RouteLink(
                        u.UserName,
                        "UserDetailsRoute",
                        New With {.UserRouteName = u.getRouteName}
                    )
                Else
                    @u.UserName
                End If
            </td>
        </tr>
    Next

</table>