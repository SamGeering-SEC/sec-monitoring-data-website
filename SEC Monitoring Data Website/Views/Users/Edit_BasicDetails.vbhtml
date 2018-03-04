@ModelType SEC_Monitoring_Data_Website.EditUserViewModel


@Html.HiddenFor(Function(model) model.User.UserName)


<table class="edit-table">
    <tr>
        <th>
            User Name
        </th>
        <td>
            @Model.User.UserName
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <th>
            Associated Contact
        </th>
        <td>
            @Html.DropDownListFor(
                Function(model) model.ContactId,
                Model.ContactList
            )
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
            @Html.DropDownListFor(
                Function(model) model.UserAccessLevelId,
                Model.UserAccessLevelList
            )
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.UserAccessLevelId)
        </td>
    </tr>
</table>
