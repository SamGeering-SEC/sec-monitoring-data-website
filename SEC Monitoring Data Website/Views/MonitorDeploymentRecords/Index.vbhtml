@ModelType SEC_Monitoring_Data_Website.ViewMonitorDeploymentRecordsViewModel

@Code
    ViewData("Title") = "Monitor Deployment Record List"
End Code

@Html.AntiForgeryToken()


<h2>List of Monitor Deployment Records</h2>

@Html.Partial("SearchableIndexMTPrMLHeader")

<div id='@Model.TableId'>
    @Html.Partial("Index_Table", Model.MonitorDeploymentRecords)
</div>


