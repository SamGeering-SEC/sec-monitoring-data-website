@ModelType SEC_Monitoring_Data_Website.UserAccessLevelDetailsViewModel

@Code
    ViewData("Title") = "User Access Level Details"
End Code

<h2>@Model.UserAccessLevel.AccessLevelName Access Level Details</h2>

@Html.Partial("TabsView", Model.Tabs)

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Html.Partial("DeleteObjectConfirmation", "UserAccessLevel")
