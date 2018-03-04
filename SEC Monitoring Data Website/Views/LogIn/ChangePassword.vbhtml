@modeltype SEC_Monitoring_Data_Website.ChangePasswordViewModel

@Code
    ViewData("Title") = "Change Password"
End Code


@Using Html.BeginForm()
    
    @Html.AntiForgeryToken

    @Html.HiddenFor(Function(model) model.UserName)

    @<h2>Change Password</h2>

    @<table class="create-table">
        <tr>
            <th>
                Old Password
            </th>
            <td>
                @Html.PasswordFor(Function(model) model.OldPassword)
            </td>
            <td>
                @Html.ValidationMessageFor(Function(model) model.OldPassword)
            </td>
        </tr>
        <tr>
            <th>
                New Password
            </th>
            <td>
                @Html.PasswordFor(Function(model) model.NewPassword)
            </td>
            <td>
                @Html.ValidationMessageFor(Function(model) model.NewPassword)
            </td>
        </tr>
        <tr>
            <th>
                Confirm New Password
            </th>
            <td>
                @Html.PasswordFor(Function(model) model.ConfirmNewPassword)
            </td>
            <td>
                @Html.ValidationMessageFor(Function(model) model.ConfirmNewPassword)
            </td>
        </tr>
    </table>

    @Html.JQueryUI.Button("Change Password")

End Using