@ModelType SEC_Monitoring_Data_Website.CreateCalculationFilterViewModel

<script type="text/javascript">

    function ShowHideTimeWindowControls() {
        if ($("#CalculationFilter_UseTimeWindow").is(':checked')) {
            $("#TimeWindowControls").show();
        } else {
            $("#TimeWindowControls").hide();
        }
    };
    function ShowHideAggregateParameters() {
        var f = $('#CalculationAggregateFunctionId').val();
        if (f != "1" && f != "") {
            $("#AggregateParameters").show();
        }
        else {
            $("#AggregateParameters").hide();
        }
    };

    $(document).ready(function () {

        ShowHideTimeWindowControls();
        $("#CalculationFilter_UseTimeWindow").click(function () {
            ShowHideTimeWindowControls();
        });

        ShowHideAggregateParameters();
        $('#CalculationAggregateFunctionId').change(function () { ShowHideAggregateParameters(); });

    });

</script>

@Code
    ViewData("Title") = "Create Calculation Filter"
End Code

<h2>Create Calculation Filter</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)


    @Using t = Html.JQueryUI().BeginTabs()

            t.Tab("Basic Details", "basic_details")
            t.Tab("Days of Week", "days_of_week")


            ' Basic Details Tab
        @Using t.BeginPanel
            @Html.Partial("Create_BasicDetails", Model)
        End Using

        ' Days of Week Tab
        @Using t.BeginPanel
            @Html.Partial("Create_DaysofWeek", Model)
        End Using

    End Using

    @<p>
        @Html.JQueryUI.Button("Create")
    </p>

End Using
