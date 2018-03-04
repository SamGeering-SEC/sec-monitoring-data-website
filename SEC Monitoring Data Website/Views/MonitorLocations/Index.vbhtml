@ModelType SEC_Monitoring_Data_Website.ViewMonitorLocationsViewModel

@Code
    ViewData("Title") = "Select Monitor Location"
End Code

<h2>Select a Monitor Location</h2>

@Html.Partial("SearchableIndexMTHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.MonitorLocations)
</div>
