@ModelType SEC_Monitoring_Data_Website.CreateMonitorDeploymentRecordViewModel

<script type="text/javascript">
    $(document).ready(function () {
        $("#ProjectId").change(function () {
            $("#MonitorLocationId").empty();
            $.ajax({
                type: 'POST',
                url: '@Url.RouteUrl("MonitorDeploymentRecordsGetProjectMonitorLocationsRoute")',
                dataType: 'json',
                data: {
                    ProjectId: $("#ProjectId").val(),
                    MeasurementTypeId: $("#Monitor_MeasurementType_Id").val()
                },
                success: function (monitorlocations) {
                    $.each(monitorlocations, function (i, ml) {
                        $("#MonitorLocationId").append('<option value="' + ml.Value + '">' + ml.Text + '</option>');
                    });
                }
            });
            return false;
        })
    });
</script>

@Code
    Dim showMonitorLink = DirectCast(ViewData("ShowMonitorLink"), Boolean)
End Code

@Html.HiddenFor(Function(model) model.Monitor.MeasurementType.Id)

<table class="create-table">
    <tr>
        <th>
            Monitor
        </th>
        <td>
            @If showMonitorLink Then
                @Html.RouteLink(Model.Monitor.MonitorName,
                                "MonitorDetailsRoute",
                                New With {.MonitorRouteName = Model.Monitor.getRouteName})
            Else
                @Html.DisplayFor(Function(model) model.Monitor.MonitorName)
            End If
        </td>
        <td>

        </td>
    </tr>
    <tr>
        <th>
            Project
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.ProjectId,
                                  Model.ProjectList,
                                  "Please select a Project...")
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.ProjectId)
        </td>
    </tr>
    <tr>
        <th>
            Monitor Location
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.MonitorLocationId,
                                                  Model.MonitorLocationList,
                                                  "Please select a Monitor Location...")
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.monitorlocationid)
        </td>
    </tr>
    <tr>
        <th>
            Deployment Start Date
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MonitorDeploymentRecord.DeploymentStartDate)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MonitorDeploymentRecord.DeploymentStartDate)
        </td>
    </tr>
    
    @Html.HiddenFor(Function(model) model.MonitorDeploymentRecord.DeploymentEndDate)

</table>