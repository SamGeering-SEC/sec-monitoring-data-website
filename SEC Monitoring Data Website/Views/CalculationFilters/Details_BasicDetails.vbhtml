@ModelType SEC_Monitoring_Data_Website.CalculationFilter

<table class="details-table">
    <tr>
        <th>
            Filter Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.FilterName)
        </td>
    </tr>
    <tr>
        <th>
            Measurement Metric
        </th>
        <td>
            @If ViewData("ShowMeasurementMetricLink") = True Then
                @Html.RouteLink(Model.MeasurementMetric.MetricName,
                                "MeasurementMetricDetailsRoute",
                                New With {.MeasurementMetricRouteName = Model.MeasurementMetric.getRouteName})
            Else
                @Model.MeasurementMetric.MetricName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Input Multiplier
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.InputMultiplier)
        </td>
    </tr>
    <tr>
        <th>
            Aggregate Function
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.CalculationAggregateFunction.FunctionName)
        </td>
    </tr>
    @If Model.CalculationAggregateFunction.FunctionName <> "Nothing" Then
        @<tr>
            <th>
                Time Base (hh:mm)
            </th>
            <td>
                @Format(Date.FromOADate(Model.TimeBase), "HH:mm")
            </td>
        </tr>
        @<tr>
            <th>
                Use Time Window
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.UseTimeWindow)
            </td>
        </tr>
        @If Model.UseTimeWindow = True Then
            @<tr>
                <th>
                    Time Window Start Time
                </th>
                <td>
                    @Html.DisplayFor(Function(model) model.TimeWindowStartTime)
                </td>
            </tr>
            @<tr>
                <th>
                    Time Window End Time
                </th>
                <td>
                    @Html.DisplayFor(Function(model) model.TimeWindowEndTime)
                </td>
            </tr>
            @<tr>
                <th>
                    Time Step
                </th>
                <td>
                    @Format(Date.FromOADate(Model.TimeStep), "HH:mm")
                </td>
            </tr>
        End If
    End If

</table>