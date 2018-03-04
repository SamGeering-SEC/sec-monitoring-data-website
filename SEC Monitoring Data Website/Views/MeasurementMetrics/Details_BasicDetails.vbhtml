@ModelType SEC_Monitoring_Data_Website.MeasurementMetric

<table class="details-table">
    <tr>
        <th>
            Metric Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MetricName)
        </td>
    </tr>
    <tr>
        <th>
            Display Name
        </th>
        <td>
            @Html.Raw(Model.DisplayName)
        </td>
    </tr>
    <tr>
        <th>
            Measurement Type
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MeasurementType.MeasurementTypeName)
        </td>
    </tr>
    <tr>
        <th>
            Fundamental Unit
        </th>
        <td>
            @Html.Raw(Model.FundamentalUnit)
        </td>
    </tr>
    <tr>
        <th>
            Rounding Decimal Places
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.RoundingDecimalPlaces)
        </td>
    </tr>
</table>