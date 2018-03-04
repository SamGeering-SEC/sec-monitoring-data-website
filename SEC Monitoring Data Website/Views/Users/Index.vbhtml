@ModelType SEC_Monitoring_Data_Website.ViewUsersViewModel

@Code
    ViewData("Title") = "Index"
End Code

<h2>Website Users</h2>

@Html.Partial("SearchableIndexHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.Users)
</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)
