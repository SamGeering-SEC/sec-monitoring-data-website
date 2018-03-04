@ModelType SEC_Monitoring_Data_Website.MeasurementCommentsIndexViewModel

<script type="text/javascript">

    var monitorLocationId;
    var measurementRangeStartDate;
    var measurementRangeEndDate;

    function dateSelected() {
        measurementRangeStartDate = $('#MeasurementRangeStartDate').val();
        measurementRangeEndDate = $('#MeasurementRangeEndDate').val();
        updateGraph();
    };

    function setDateRange(startDate, endDate) {
        $('#MeasurementRangeStartDate').datepicker("setDate", startDate)
        $('#MeasurementRangeEndDate').datepicker("setDate", endDate)
        measurementRangeStartDate = startDate;
        measurementRangeEndDate = endDate;
        updateGraph();
    };

    function updateGraph() {
        $.ajax({
            type: "GET",
            url: '@Url.RouteUrl("MeasurementCommentUpdateViewRoute")',
            data: {
                MonitorLocationId: monitorLocationId,
                strStartDate: measurementRangeStartDate,
                strEndDate: measurementRangeEndDate
            },
            beforeSend: function () {
                $('#graph').empty();
                $("#divLoadingElement").css("display", "inline");
            },
            success: function (partialView) {
                $("#divLoadingElement").css("display", "none");
                $("#graph").html(partialView);
                $('#graph').jQueryUIHelpers();
            },
            failure: function () {
                alert('Failed to load Measurements');
            }
        });
    };

    function deleteMeasurementCommentSuccess(data, textStatus, jqXHR) {
        $('#comment-table').html(data);
        measurementRangeStartDate = $('#MeasurementRangeStartDate').val();
        measurementRangeEndDate = $('#MeasurementRangeEndDate').val();
        updateGraph();
    };

    function deleteMeasurementCommentError(jqXHR, textStatus, errorThrown) {
        alert(textStatus);
    };

    $(document).ready(function () {
        monitorLocationId = $('#MonitorLocation_Id').val();
    });

</script>

@Code
    ViewData("Title") = "Measurement Comments"
End Code

<h2>Measurement Comments for @Model.MonitorLocation.MonitorLocationName</h2>

@Html.HiddenFor(Function(model) model.MonitorLocation.Id)

<div id="graph">
    <p>
        @Model.Chart
    </p>
    <p>
        @Html.JQueryUI.ActionButton(
            "Add Comment", "Create", "MeasurementComments",
            New With {
                .ProjectRouteName = Model.MonitorLocation.Project.getRouteName,
                .MonitorLocationRouteName = Model.MonitorLocation.getRouteName,
                .StartDateCode = Format(Date.FromOADate(Model.intMeasurementRangeStartDate), "yyyyMMdd"),
                .EndDateCode = Format(Date.FromOADate(Model.intMeasurementRangeEndDate), "yyyyMMdd")
            },
            Nothing
        )
    </p>
</div>

<div id="divLoadingElement" style="display:none">
    <p>
        <img src="~/Images/loading.gif" />
    </p>
</div>

<div>
    <table>
        <tr>
            <th>
                Start Date
            </th>
            <td>
                @Html.JQueryUI.DatepickerFor(
                    Function(model) model.MeasurementRangeStartDate
                ).MinDate(Model.FirstMeasurementStartDate).MaxDate(Model.LastMeasurementEndDate).OnSelect("dateSelected").DateFormat("dd-MMM-yy")
            </td>
        </tr>
        <tr>
            <th>
                End Date
            </th>
            <td>
                @Html.JQueryUI.DatepickerFor(
                    Function(model) model.MeasurementRangeEndDate
                ).MinDate(Model.FirstMeasurementStartDate).MaxDate(Model.LastMeasurementEndDate).OnSelect("dateSelected").DateFormat("dd-MMM-yy")
            </td>
        </tr>
    </table>
</div>

<div id='comment-table'>
    @Html.Partial("Index_CommentsTable", Model.MeasurementComments)
</div>

@Html.Partial("NavigationButtons", Model.NavigationButtons)

@Using (Html.JQueryUI().Begin(
    New Dialog().Title(
        "Delete Measurement Comment"
    ).AutoOpen(False).Modal(True).ConfirmAjax(
        ".DeleteMeasurementCommentLink", "Yes", "No",
        New AjaxSettings With {
            .Method = HttpVerbs.Post,
            .Success = "deleteMeasurementCommentSuccess",
            .Error = "deleteMeasurementCommentError"
        }
    )
))
    @<p>
        Would you like to delete this Measurement Comment?
    </p>
End Using
