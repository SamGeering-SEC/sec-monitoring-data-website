@ModelType SEC_Monitoring_Data_Website.CreateUserViewModel

@Code
    ViewData("Title") = "Create User"
End Code

<h2>Create User</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)
    @Html.HiddenFor(Function(model) model.User.Salt)

    @<fieldset>
        <legend>User</legend>

        <table class="create-table">
            <tr>
                <th>
                    User Name
                </th>
                <td>
                    @Html.EditorFor(Function(model) model.User.UserName)
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.User.UserName)
                </td>
            </tr>
            <tr>
                <th>
                    Password
                </th>
                <td>
                    @Html.PasswordFor(Function(model) model.User.Password)
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.User.Password)
                </td>
            </tr>
            <tr>
                <th>
                    Associated Contact
                </th>
                <td>
                    @Html.DropDownListFor(Function(model) model.ContactId, Model.ContactList, "Please select a Contact...")
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.ContactId)
                </td>
            </tr>
            <tr>
                <th>
                    Receives Locked User Email Notifications
                </th>
                <td>
                    @Html.EditorFor(Function(model) model.User.ReceivesLockNotifications)
                </td>
            </tr>
            <tr>
                <th>
                    User Access Level
                </th>
                <td>
                    @Html.DropDownListFor(Function(model) model.UserAccessLevelId, Model.UserAccessLevelList, "Please select a User Access Level...")
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.UserAccessLevelId)
                </td>
            </tr>
        </table>

        @Html.ValidationSummary(True)

        <p>
            @Html.JQueryUI.Button("Create")
        </p>
    </fieldset>
End Using


@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
