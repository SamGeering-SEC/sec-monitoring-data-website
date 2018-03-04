@ModelType MonitorLocationCriterionDetailsViewModel

@Code
    Dim showCalculationFilterDetailsLink As Boolean = True
    Dim calculationFilter = Model.AssessmentCriterion.CalculationFilter
    Dim assessmentCriterion = Model.AssessmentCriterion
End Code

<table class="details-table">
    <tr>
        <th>
            Calculation Filter
        </th>
        <td>
            @If showCalculationFilterDetailsLink Then
                @Html.RouteLink(calculationFilter.FilterName,
                                "CalculationFilterDetailsRoute",
                                New With {.CalculationFilterRouteName = calculationFilter.getRouteName})
            Else
                @calculationFilter.FilterName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Threshold Range Lower Bound
        </th>
        <td>
            @assessmentCriterion.ThresholdRangeLower
        </td>
    </tr>
    <tr>
        <th>
            Threshold Range Upper Bound
        </th>
        <td>
            @assessmentCriterion.ThresholdRangeUpper
        </td>
    </tr>
    <tr>
        <th>
            Threshold Range Lower Bound Type
        </th>
        <td>
            @assessmentCriterion.ThresholdType.ThresholdTypeName
        </td>
    </tr>
    <tr>
        <th>
            # Rounding Decimal Places
        </th>
        <td>
            @assessmentCriterion.RoundingDecimalPlaces
        </td>
    </tr>
</table>
