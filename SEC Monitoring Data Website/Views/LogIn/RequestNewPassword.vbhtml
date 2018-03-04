@modeltype SEC_Monitoring_Data_Website.RequestNewPasswordViewModel

@Code
    ViewData("Title") = "Request New Password"
End Code

<h2>Request New Password</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<table class="details-table">
        <tr>
            <th>
                Please enter the E-Mail address associated with your account.
            </th>
        </tr>
        <tr>
            <td>
                @Html.EditorFor(Function(model) model.AccountEmailAddress)
            </td>
        </tr>
    </table>

    @Html.ValidationSummary(True)

    @Html.JQueryUI().Button("Request Password")

End Using
