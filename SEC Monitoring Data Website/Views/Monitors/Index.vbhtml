@ModelType SEC_Monitoring_Data_Website.ViewMonitorsViewModel

@Code
    ViewData("Title") = "Monitor List"
End Code

<h2>List of Monitors</h2>

@Html.Partial("SearchableIndexMTHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.Monitors)
</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)

