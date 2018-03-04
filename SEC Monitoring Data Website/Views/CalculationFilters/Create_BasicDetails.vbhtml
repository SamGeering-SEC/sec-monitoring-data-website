@ModelType SEC_Monitoring_Data_Website.CreateCalculationFilterViewModel

<table class="create-table">
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
            @Html.DropDownListFor(Function(model) model.MeasurementMetricId, Model.MeasurementMetricList, "Please select a Measurement Metric...")
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
            @Html.DropDownListFor(Function(model) model.CalculationAggregateFunctionId, Model.CalculationAggregateFunctionList, "Please select a Calculation Aggregate Function...")
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CalculationAggregateFunctionId)
        </td>
    </tr>
</table>

<div id="AggregateParameters">

    <table class="details-table">
        <tr>
            <th>
                Time Base (hh:mm)
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
    <table id="TimeWindowControls" class="create-table">
        <tr>
            <th>
                Time Window Start Time
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
                Time Window End Time
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

</div>