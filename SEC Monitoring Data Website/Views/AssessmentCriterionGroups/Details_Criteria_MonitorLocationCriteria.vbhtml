@ModelType SEC_Monitoring_Data_Website.ViewMonitorLocationAssessmentCriteriaViewModel

@Code
    Dim showCalculationFilterLinks = DirectCast(ViewData("ShowCalculationFilterLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Calculation Filter
        </th>
        <th style="text-align:center">
            Lower Bound
        </th>
        <th style="text-align:center">
            Upper Bound
        </th>
        <th style="text-align:center">
            Lower Bound Type
        </th>
        <th style="text-align:center">
            Rounding Decimal Places
        </th>
        <th style="text-align:center">
            Plot Assessed Level
        </th>
        <th style="text-align:center">
            Plot Criterion Level
        </th>
        <th style="text-align:center">
            Tabulate Assessed Level
        </th>
        <th style="text-align:center">
            Tabulate Criterion Triggers
        </th>
    </tr>

    @*Item Rows*@
    @For Each ac In Model.AssessmentCriteria
        @<tr>
            <td>
                @If showCalculationFilterLinks Then
                    @Html.RouteLink(
                        ac.CalculationFilter.FilterName,
                        "CalculationFilterDetailsRoute",
                        New With {.CalculationFilterRouteName = ac.CalculationFilter.getRouteName}
                    )
                Else
                    @ac.CalculationFilter.FilterName
                End If
            </td>
            <td style="text-align:center">
                @ac.ThresholdRangeLower
            </td>
             <td style="text-align:center">
                 @ac.ThresholdRangeUpper
             </td>
             <td style="text-align:center">
                 @ac.ThresholdTypeSymbol()
             </td>
             <td style="text-align:center">
                 @ac.RoundingDecimalPlaces
             </td>
             <td style="text-align:center">
                 @Html.DisplayFor(Function(m) ac.PlotAssessedLevel)
             </td>
             <td style="text-align:center">
                 @Html.DisplayFor(Function(m) ac.PlotCriterionLevel)
             </td>
             <td style="text-align:center">
                 @Html.DisplayFor(Function(m) ac.TabulateAssessedLevels)
             </td>
             <td style="text-align:center">
                 @Html.DisplayFor(Function(m) ac.TabulateCriterionTriggers)
             </td>
        </tr>
    Next

</table>