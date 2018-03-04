@ModelType SEC_Monitoring_Data_Website.ViewAssessmentViewModel

@Code
    ViewData("Title") = "View Assessment"
End Code


<script type="text/javascript">

    var assessmentGroupId;
    var assessmentDate;
    var monitorLocationId;
    var showComments;
    var showCriteriaTimePeriods;
    var showNonWorkingHours;
    var dynamicYMin;
    var dynamicYMax;
    var startOrEnd;
    var initialised = false;

    function getVariables() {
        assessmentCriterionGroupId = $("input[name='AssessmentCriterionGroupButton']:checked").val();
        monitorLocationId = $('#MonitorLocationId').val();
        showComments = $('#ShowComments').prop('checked');
        showCriteriaTimePeriods = $('#ShowCriteriaTimePeriods').prop('checked');
        showNonWorkingHours = $('#ShowNonWorkingHours').prop('checked');
        dynamicYMin = $('#DynamicYMin').prop('checked');
        dynamicYMax = $('#DynamicYMax').prop('checked');
        assessmentDate = $('#AssessmentDate').val();
        startOrEnd = $('#StartOrEnd').val();
        endDate = $('#AssessmentEndDate').val();
    };

    function assessmentGroupChanged() {
        if (initialised) {
            getVariables();
            updateTable();
        }
    };

    function updateTable() {
        $('#assessment-table').empty();
        $("#divLoadingElement").css("display", "inline");
        $.ajax({
            type: "GET",
            url: '@Url.RouteUrl("ViewTableAndGraphAjaxRoute")',
            data: {
                strMonitorLocationId: monitorLocationId,
                strAssessmentCriterionGroupId: assessmentCriterionGroupId,
                strAssessmentDate: assessmentDate,
                startOrEnd: startOrEnd,
                showComments: showComments,
                showCriteriaTimePeriods: showCriteriaTimePeriods,
                showNonWorkingHours: showNonWorkingHours,
                dynamicYMin: dynamicYMin,
                dynamicYMax: dynamicYMax
            },
            success: function (partialView) {
                $("#divLoadingElement").css("display", "none");
                $('#assessment-table').html(partialView);
            },
            error: function () {
                alert('Failed to load Assessment Data :(');
                alert(textStatus);
            }
        });
        $.ajax({
            type: "GET",
            url: '@Url.RouteUrl("AssessmentViewNavigationButtonsRoute")',
            data: {
                strMonitorLocationId: monitorLocationId,
                strAssessmentDate: assessmentDate
            },
            success: function (partialView) {
                $('#navigationButtons').html(partialView);
            },
            error: function () {
                alert('Failed to load Navigation Buttons :(');
            }
        });
    };
    function refreshView() {
        if (initialised) {
            getVariables();
            updateTable();
        }
    }
    $(document).ready(function () {

        initialised = true;
        $(':input[name="AssessmentCriterionGroupButton"]').change(function () {
            getVariables();
            updateTable();
        });

    });

</script>

<h2>Assessments for Monitor Location @Model.MonitorLocation.MonitorLocationName</h2>

@Html.HiddenFor(Function(model) model.MonitorLocationId)

<div>
    <h3>
        Assessment Criterion
    </h3>
    @Using Html.JQueryUI.BeginButtonSet()
        @For Each acg In Model.AssessmentCriterionGroups
            @<label for='@acg.getHtmlName'>@acg.GroupName</label>
            @If acg.GroupName = Model.SelectedAssessmentCriterionGroup.GroupName Then
                @<input type="radio" 
                        id='@acg.getHtmlName' 
                        name="AssessmentCriterionGroupButton" 
                        value='@acg.Id' checked='checked'/>
            Else
                @<input type="radio" 
                        id='@acg.getHtmlName' 
                        name="AssessmentCriterionGroupButton" 
                        value='@acg.Id'/>
            End If
        Next
            End Using
   
    <h3>
        Assessment Date
    </h3>
    <select id="StartOrEnd" onchange="refreshView();">
        <option value="Start" selected>Start</option>
        <option value="End">End</option>
    </select> on @Html.JQueryUI.DatepickerFor(Function(model) model.AssessmentDate).OnSelect("refreshView").DateFormat("dd-MMM-yyyy").ConstrainInput(True).MinDate(Model.FirstMeasurementDate).MaxDate(Model.LastMeasurementDate).FirstDay(1)

    <h3>
        Chart Options
    </h3>
    <table>
        <tr>
            <td>
                <input type="checkbox"
                       id="ShowComments"
                       name="ShowComments"
                       checked=@Model.ShowComments
                       onclick="refreshView()" />
                Comments
            </td>
            <td>
                <input type="checkbox"
                       id="ShowCriteriaTimePeriods"
                       name="ShowCriteriaTimePeriods"
                       checked=@Model.ShowCriteriaTimePeriods
                       onclick="refreshView()" />
                Criteria Time Periods
            </td>
            <td>
                <input type="checkbox"
                       id="ShowNonWorkingHours"
                       name="ShowNonWorkingHours"
                       checked=@Model.ShowNonWorkingHours
                       onclick="refreshView()" />
                Non-Working Hours
            </td>
            <td>
                <input type="checkbox"
                       id="DynamicYMin"
                       name="DynamicYMin"
                       checked=@Model.DynamicYMin
                       onclick="refreshView()" />
                Dynamic Y-Min
            </td>
            <td>
                <input type="checkbox"
                       id="DynamicYMax"
                       name="DynamicYMax"
                       checked=@Model.DynamicYMax
                       onclick="refreshView()" />
                Dynamic Y-Max
            </td>
        </tr>
    </table>
</div>

<div id="divLoadingElement" style="display:none;">
    <img src="~/Images/loading.gif" />
</div>

<div id="assessment-table">

    @Html.Action("ViewTableAndGraph", "AssessmentCriterionGroups",
                     New With {
                         .MonitorLocationId = Model.MonitorLocation.Id.ToString,
                         .AssessmentCriterionGroupId = Model.SelectedAssessmentCriterionGroup.Id.ToString,
                         .AssessmentDate = Model.AssessmentDate.ToString("dd-MMM-yyyy"),
                         .StartOrEnd = Model.StartOrEnd,
                         .showComments = Model.ShowComments,
                         .showCriteriaTimePeriods = Model.ShowCriteriaTimePeriods,
                         .showNonWorkingHours = Model.ShowNonWorkingHours,
                         .dynamicYMin = Model.DynamicYMin, .dynamicYMax = Model.DynamicYMax
                     }
                 )

</div>

<div id="navigationButtons">
    @Html.Partial("NavigationButtons", Model.NavigationButtons)
</div>


