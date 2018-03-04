@ModelType SEC_Monitoring_Data_Website.User

@Code
    Dim contact = Model.Contact
    Dim userAccessLevel = Model.UserAccessLevel
End Code

<table class="details-table">
    <tr>
        <th>
            User Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.UserName)
        </td>
    </tr>
    <tr>
        <th>
            Contact
        </th>
        <td>
            @Html.RouteLink(
                contact.ContactName, "ContactDetailsRoute",
                New With {.ContactRouteName = contact.getRouteName}
            )
        </td>
    </tr>
    <tr>
        <th>
            Receives Locked User Email Notifications
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.ReceivesLockNotifications)
        </td>
    </tr>
    <tr>
        <th>
            User Access Level
        </th>
        <td>
            @Html.RouteLink(
                userAccessLevel.AccessLevelName, "UserAccessLevelDetailsRoute",
                New With {.UserAccessLevelRouteName = userAccessLevel.getRouteName}
            )
        </td>
    </tr>
</table>