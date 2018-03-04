@ModelType SEC_Monitoring_Data_Website.EditMeasurementMetricViewModel

<table class="edit-table">
    <tr>
        <th>
            Metric Name
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementMetric.MetricName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementMetric.MetricName)
        </td>
    </tr>
    <tr>
        <th>
            Display Name
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementMetric.DisplayName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementMetric.DisplayName)
        </td>
    </tr>
    <tr>
        <th>
            Measurement Type
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.MeasurementTypeId, Model.MeasurementTypeList, String.Empty)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementTypeId)
        </td>
    </tr>
    <tr>
        <th>
            Fundamental Unit
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementMetric.FundamentalUnit)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementMetric.FundamentalUnit)
        </td>
    </tr>
    <tr>
        <th>
            Rounding Decimal Places
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementMetric.RoundingDecimalPlaces)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementMetric.RoundingDecimalPlaces)
        </td>
    </tr>
</table>