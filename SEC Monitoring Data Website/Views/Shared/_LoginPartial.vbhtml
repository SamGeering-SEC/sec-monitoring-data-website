@If Request.IsAuthenticated Then
    @<table>
        <tr>
            <td>
                <h4>
                    Hello, @User.Identity.Name!
                </h4>
            </td>
            <td>
                @Html.JQueryUI.ActionButton("Log Out", "LogOut", "LogIn")
            </td>
            <td>
                @Html.JQueryUI.ActionButton("Change Password", "ChangePassword", "LogIn")
            </td>
        </tr>
    </table>
End If
