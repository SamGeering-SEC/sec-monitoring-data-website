@ModelType SEC_Monitoring_Data_Website.EditCalculationFilterViewModel

<script type="text/javascript">

    function ShowHideTimeWindowInputs() {
        if ($("#CalculationFilter_UseTimeWindow").is(':checked')) {
            $("#TimeWindowInputs").show();
        } else {
            $("#TimeWindowInputs").hide();
        }
    };

    $(document).ready(function () {
        ShowHideTimeWindowInputs();
        $("#CalculationFilter_UseTimeWindow").click(function () { ShowHideTimeWindowInputs() });
    });

</script>

<table class="edit-table">
    <tr>
        <th>
            Filter Name
        </th>
        <td>
            @Html.EditorFor(Function(model) model.CalculationFilter.FilterName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CalculationFilter.FilterName)
        </td>
    </tr>
    <tr>
        <th>
            Measurement Metric
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.MeasurementMetricId, Model.MeasurementMetricList)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementMetricId)
        </td>
    </tr>
    <tr>
        <th>
            Input Multiplier
        </th>
        <td>
            @Html.EditorFor(Function(model) model.CalculationFilter.InputMultiplier)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CalculationFilter.InputMultiplier)
        </td>
    </tr>
    <tr>
        <th>
            Calculation Aggregate Function
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.CalculationAggregateFunctionId, Model.CalculationAggregateFunctionList)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CalculationAggregateFunctionId)
        </td>
    </tr>
    <tr>
        <th>
            Time Base
        </th>
        <td>
            @Html.EditorFor(Function(model) model.TimeBase)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.TimeBase)
        </td>
    </tr>
    <tr>
        <th>
            Use Time Window
        </th>
        <td>
            @Html.EditorFor(Function(model) model.CalculationFilter.UseTimeWindow)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CalculationFilter.UseTimeWindow)
        </td>
    </tr>
    </table>
<table id="TimeWindowInputs" class="edit-table">
    <tr>
        <th>
            Start Time
        </th>
        <td>
            @Html.EditorFor(Function(model) model.TimeWindowStartTime)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.TimeWindowStartTime)
        </td>
    </tr>
    <tr>
        <th>
            End Time
        </th>
        <td>
            @Html.EditorFor(Function(model) model.TimeWindowEndTime)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.TimeWindowEndTime)
        </td>
    </tr>
    <tr>
        <th>
            Time Step
        </th>
        <td>
            @Html.EditorFor(Function(model) model.TimeStep)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.TimeStep)
        </td>
    </tr>
</table>