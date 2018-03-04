@ModelType SEC_Monitoring_Data_Website.ViewUserAccessLevelsViewModel

@Code
    ViewData("Title") = "User Access Levels"
End Code

<h2>List of User Access Levels</h2>

@Html.Partial("SearchableIndexHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.UserAccessLevels)
</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)