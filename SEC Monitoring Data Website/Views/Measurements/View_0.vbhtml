@ModelType SEC_Monitoring_Data_Website.ViewMeasurementsViewModel

<script type="text/javascript">

    var currentDuration;
    var currentViewName;
    var currentStartDate;
    var projectRouteName;
    var monitorLocationRouteName;

    function showDayNavigators() {
        $('#DayNavigators').show();
        $('#WeekNavigators').hide();
        $('#MonthNavigators').hide();
    };
    function showWeekNavigators() {
        $('#DayNavigators').hide();
        $('#WeekNavigators').show();
        $('#MonthNavigators').hide();
    };
    function showMonthNavigators() {
        $('#DayNavigators').hide();
        $('#WeekNavigators').hide();
        $('#MonthNavigators').show();
    };
    function showNavigators() {
        switch (currentDuration) {
            case 'Day':
                showDayNavigators();
                break;
            case 'Week':
                showWeekNavigators();
                break;
            case 'Month':
                showMonthNavigators();
                break;
        }
    };
    function updateTheDate(withDate) {
        currentStartDate = withDate.toString("ddMMyyyy");
        $('#StartDate').datepicker("setDate", withDate);
    };
    function calendarDateChanged() {
        currentStartDate = Date.parseExact($('#StartDate').val(), "dd-MMM-yyyy").toString("ddMMyyyy");
        updateView();
    }
    function updateView() {
        $.ajax({
            type: "GET",
            url: '@Url.RouteUrl("MeasurementUpdateViewRoute")',
            data: {
                ProjectRouteName: projectRouteName,
                MonitorLocationRouteName: monitorLocationRouteName,
                ViewName: currentViewName,
                ViewDuration: currentDuration,
                strStartDate: currentStartDate
            },
            beforeSend: function(){
                $('#measurementsGraphAndTable').empty();
                $("#divLoadingElement").css("display", "inline");
            },
            success: function (partialView) {
                $("#divLoadingElement").css("display", "none");
                $("#measurementsGraphAndTable").html(partialView);
            },
            failure: function () {
                alert('Failed to load Measurement Data :(');
            }
        });
        $.ajax({
            type: "GET",
            url: '@Url.RouteUrl("MeasurementUpdateNavigationButtonsRoute")',
            data: {
                ProjectRouteName: projectRouteName,
                MonitorLocationRouteName: monitorLocationRouteName,
                strAssessmentDate: currentStartDate
            },
            success: function (partialView) {
                $("#navigationButtons").html(partialView);
            },
            failure: function () {
                alert('Failed to load Navigation Buttons :(');
            }
        });
    };


    $(document).ready(function () {

        currentDuration = '@Model.ViewDuration';
        currentViewName = '@Model.ViewName';
        currentStartDate = '@Model.StartDate.ToString("ddMMyyyy")';
        projectRouteName = '@Model.ProjectRouteName';
        monitorLocationRouteName = '@Model.MonitorLocationRouteName';
        showNavigators();

        $(':input[name="ViewDurationRadioButtons"]').change(function () {
            currentDuration = $(this).val();
            showNavigators();
            updateView();
        });

        $(':input[name="MeasurementViewButton"]').change(function () {
            currentViewName = $(this).val();
            updateView();
        });

        $('#PreviousDayButton').click(function () {
            var theDate = Date.parseExact(currentStartDate, "ddMMyyyy");
            theDate = theDate.addDays(-1);
            updateTheDate(theDate);
            updateView();
        });
        $('#FollowingDayButton').click(function () {
            var theDate = Date.parseExact(currentStartDate, "ddMMyyyy");
            theDate = theDate.addDays(1);
            updateTheDate(theDate);
            updateView();
        });
        $('#TodayButton').click(function () {
            theDate = Date.today();
            updateTheDate(theDate);
            updateView();
        });
        $('#PreviousWeekButton').click(function () {
            var theDate = Date.parseExact(currentStartDate, "ddMMyyyy");
            theDate = theDate.addWeeks(-1);
            updateTheDate(theDate);
            updateView();
        });
        $('#NextWeekButton').click(function () {
            var theDate = Date.parseExact(currentStartDate, "ddMMyyyy");
            theDate = theDate.addWeeks(1);
            updateTheDate(theDate);
            updateView();
        });
        $('#ThisWeekButton').click(function () {
            theDate = Date.today();
            updateTheDate(theDate);
            updateView();
        });
        $('#PreviousMonthButton').click(function () {
            var theDate = Date.parseExact(currentStartDate, "ddMMyyyy");
            theDate = theDate.addMonths(-1);
            updateTheDate(theDate);
            updateView();
        });
        $('#NextMonthButton').click(function () {
            var theDate = Date.parseExact(currentStartDate, "ddMMyyyy");
            theDate = theDate.addMonths(1);
            updateTheDate(theDate);
            updateView();
        });
        $('#ThisMonthButton').click(function () {
            theDate = Date.today();
            updateTheDate(theDate);
            updateView();
        });

    });

</script>


@Code
    ViewData("Title") = "View Measurements"
End Code

<h2>View Measurements</h2>

<h3>Measurement View</h3>

@Using Html.JQueryUI.BeginButtonSet()
    @For Each mv In Model.SelectableMeasurementViews
        @<label for='@mv.getHtmlName'>@mv.DisplayName</label>
        @If mv.ViewName = Model.SelectedMeasurementView.ViewName Then
            @<input type="radio" id='@mv.getHtmlName' name="MeasurementViewButton" value='@mv.ViewName' checked='checked' />
        Else
            @<input type="radio" id='@mv.getHtmlName' name="MeasurementViewButton" value='@mv.ViewName' />
        End If
    Next
End Using

<h3>View Duration</h3>

@Using Html.JQueryUI.BeginButtonSet()
    @<label for='ViewDurationDay'>Day</label>@<input type='radio' id='ViewDurationDay' name='ViewDurationRadioButtons' value='Day' @Html.Raw(IIf(Model.ViewDuration = "Day", "checked='checked'", "")) />
    @<label for='ViewDurationWeek'>Week</label>@<input type='radio' id='ViewDurationWeek' name='ViewDurationRadioButtons' value='Week' @Html.Raw(IIf(Model.ViewDuration = "Week", "checked='checked'", "")) />
    @<label for='ViewDurationMonth'>Month</label>@<input type='radio' id='ViewDurationMonth' name='ViewDurationRadioButtons' value='Month' @Html.Raw(IIf(Model.ViewDuration = "Month", "checked='checked'", "")) />
End Using

<h3>View Date</h3>
@Html.JQueryUI.DatepickerFor(Function(model) model.StartDate).OnSelect("calendarDateChanged").DateFormat("dd-MMM-yyyy").ConstrainInput(True).MinDate(Model.FirstMeasurementDate).MaxDate(Model.LastMeasurementDate).FirstDay(1)
<div id="DayNavigators">
    @Html.JQueryUI().Button("Previous Day", ButtonElement.Button, ButtonType.Button, New With {.id = "PreviousDayButton"})
    @Html.JQueryUI().Button("Following Day", ButtonElement.Button, ButtonType.Button, New With {.id = "FollowingDayButton"})
    @Html.JQueryUI().Button("Today", ButtonElement.Button, ButtonType.Button, New With {.id = "TodayButton"})
</div>
<div id="WeekNavigators">
    @Html.JQueryUI().Button("Previous Week", ButtonElement.Button, ButtonType.Button, New With {.id = "PreviousWeekButton"})
    @Html.JQueryUI().Button("Next Week", ButtonElement.Button, ButtonType.Button, New With {.id = "NextWeekButton"})
    @Html.JQueryUI().Button("This Week", ButtonElement.Button, ButtonType.Button, New With {.id = "ThisWeekButton"})
</div>
<div id="MonthNavigators">
    @Html.JQueryUI().Button("Previous Month", ButtonElement.Button, ButtonType.Button, New With {.id = "PreviousMonthButton"})
    @Html.JQueryUI().Button("Next Month", ButtonElement.Button, ButtonType.Button, New With {.id = "NextMonthButton"})
    @Html.JQueryUI().Button("This Month", ButtonElement.Button, ButtonType.Button, New With {.id = "ThisMonthButton"})
</div>

<div id="measurementsGraphAndTable">
    @Html.Partial("View_GraphAndTable", Model)
</div>
<div id="divLoadingElement" style="display:none">
    <p>
        <img src="~/Images/loading.gif" />
    </p>
</div>

<div id="navigationButtons">
    @Html.Partial("NavigationButtons", Model.NavigationButtons)
</div>



