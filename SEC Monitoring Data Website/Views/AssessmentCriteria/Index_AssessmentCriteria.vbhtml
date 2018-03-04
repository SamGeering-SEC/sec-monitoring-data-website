@ModelType MonitorLocationCriteriaDetailsViewModel

@Code
    Dim showCalculationFilterDetailsLinks = DirectCast(ViewData("ShowCalculationFilterDetailsLinks"), Boolean)
    Dim showAssessmentCriterionEditLinks = DirectCast(ViewData("ShowAssessmentCriterionEditLinks"), Boolean)
    Dim showCreateAssessmentCriterionLink = DirectCast(ViewData("ShowCreateAssessmentCriterionLink"), Boolean)
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
        <th></th>
        <th></th>
    </tr>

    @*Item Rows*@
    @For Each ac In Model.AssessmentCriteria
        @<tr>
            <td>
                @If showCalculationFilterDetailsLinks Then
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
            <td>
                @Html.RouteLink(
                    "View Assessment Criterion Details",
                    "AssessmentCriterionDetailsRoute",
                    New With {.ProjectRouteName = Model.AssessmentCriterionGroup.Project.getRouteName,
                              .AssessmentCriterionGroupRouteName = Model.AssessmentCriterionGroup.getRouteName,
                              .MonitorLocationRouteName = Model.MonitorLocation.getRouteName,
                              .CriterionIndex = ac.CriterionIndex},
                    New With {.class = "sitewide-button-16 details-button-16"}
                )
            </td>
            <td>
                @If showAssessmentCriterionEditLinks Then
                    @Html.RouteLink(
                        "Edit Assessment Criterion",
                        "AssessmentCriterionEditRoute",
                        New With {.ProjectRouteName = Model.AssessmentCriterionGroup.Project.getRouteName,
                                  .AssessmentCriterionGroupRouteName = Model.AssessmentCriterionGroup.getRouteName,
                                  .MonitorLocationRouteName = Model.MonitorLocation.getRouteName,
                                  .CriterionIndex = ac.CriterionIndex},
                        New With {.class = "sitewide-button-16 edit-button-16"}
                    )
                End If
            </td>
        </tr>
    Next
@If showCreateAssessmentCriterionLink Then
    @<tr>
        <td>
            @Html.RouteLink("Add new Assessment Criterion", "AssessmentCriterionCreateRoute",
                            New With {.ProjectRouteName = Model.AssessmentCriterionGroup.Project.getRouteName,
                                      .AssessmentCriterionGroupRouteName = Model.AssessmentCriterionGroup.getRouteName,
                                      .MonitorLocationRouteName = Model.MonitorLocation.getRouteName},
                            New With {.class = "sitewide-button-32 create-button-32"})
        </td>
    </tr>
End If        

</table>



