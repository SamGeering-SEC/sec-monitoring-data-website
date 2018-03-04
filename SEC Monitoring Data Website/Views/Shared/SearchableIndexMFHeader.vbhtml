@ModelType SEC_Monitoring_Data_Website.IViewObjectsByMeasurementTypeProjectMonitorLocationViewModel

<script type="text/javascript">

    var SearchText;

    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();

    function getVariables() {
        SearchText = $('#SearchText').val();
        MeasurementTypeId = $('#MeasurementTypeId').val();
        ProjectId = $('#ProjectId').val();
        MonitorLocationId = $('#MonitorLocationId').val();
    };

    function updateProjects() {
        $("#ProjectId").empty();
        $.ajax({
            type: 'POST',
            url: '@Url.RouteUrl(Model.UpdateProjectsRouteName)',
            dataType: 'json',
            data: {
                MeasurementTypeId: $("#MeasurementTypeId").val()
            },
            success: function (assessmentcriteriongroups) {
                $.each(assessmentcriteriongroups, function (i, p) {
                    $("#ProjectId").append('<option value="' + p.Value + '">' + p.Text + '</option>');
                });
                updateMonitorLocations();
            }
        });
        return false;
    };

    function updateMonitorLocations() {
        $("#MonitorLocationId").empty();
        $.ajax({
            type: 'POST',
            url: '@Url.RouteUrl(Model.UpdateMonitorLocationsRouteName)',
            dataType: 'json',
            data: {
                MeasurementTypeId: $("#MeasurementTypeId").val(),
                ProjectId: $("#ProjectId").val()
            },
            success: function (monitorlocations) {
                $.each(monitorlocations, function (i, ml) {
                    $("#MonitorLocationId").append('<option value="' + ml.Value + '">' + ml.Text + '</option>');
                });
            }
        });

        return false;
    }

    function updateTable() {
        getVariables();
        $.ajax({
            type: 'GET',
            url: '@Url.RouteUrl(Model.UpdateTableRouteName)',
            data: {
                SearchText: SearchText,
                MeasurementTypeId: MeasurementTypeId,
                ProjectId: ProjectId,
                MonitorLocationId: MonitorLocationId
            },
            beforeSend: function () {
                $('#divLoadingElement').css('display", "inline');
            },
            success: function (partialView) {
                $('#divLoadingElement').css("display", "none");
                $('#@Model.TableId').html(partialView);
                addButtonAnimations();
                try {
                    filterRows();
                }
                catch (err) {}
            },
            error: function () {
                alert('Failed to update table');
            }
        });
    };

    function deleteObjectSuccess(data, textStatus, jqXHR) {
        update_table();
        addButtonAnimations();
    }

    function deleteObjectError(jqXHR, textStatus, errorThrown) {
        alert(textStatus);
    }

</script>


<table id="search-bar" class="index-table">
    <tr>
        <th>
            Search
        </th>
        <td>
            @Html.TextBoxFor(Function(model) model.SearchText)
        </td>
    </tr>
    <tr>
        <th>
            Measurement Type
        </th>
        <th>
            @Html.DropDownListFor(Function(model) model.MeasurementTypeId,
                                  Model.MeasurementTypeList,
                                  "Please select a Measurement Type...",
                                  New With {.onchange = "updateProjects();"})
        </th>
    </tr>
    <tr>
        <th>
            Project
        </th>
        <th>
            @Html.DropDownListFor(Function(model) model.ProjectId,
                                  Model.ProjectList,
                                  "Please select a Project...",
                                  New With {.onchange = "updateMonitorLocations();"})
        </th>
    </tr>
    <tr>
        <th>
            Monitor Location
        </th>
        <th>
            @Html.DropDownListFor(Function(model) model.MonitorLocationId,
                                  Model.MonitorLocationList,
                                  "Please select a Monitor Location...")
        </th>
    </tr>
    <tr>
        <td>
            @Html.JQueryUI.Button("Search", New With {.onclick = "updateTable();"})
        </td>
    </tr>
</table>

<div id="divLoadingElement" style="display:none">
    <p>
        <img src="~/Images/loading.gif" />
    </p>
</div>

@Using (Html.JQueryUI().Begin(New Dialog().Title("Delete " + Model.ObjectDisplayName).AutoOpen(False).Modal(True).ConfirmAjax(".Delete" + Model.ObjectName + "Link", "Yes", "No", New AjaxSettings With {.Method = HttpVerbs.Post,
.Success = "deleteObjectSuccess",
.Error = "deleteObjectError"})))
    @<p>
        @Html.Raw("Would you like to delete this " + Model.ObjectDisplayName + "?")
    </p>
End Using
