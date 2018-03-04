@ModelType SEC_Monitoring_Data_Website.EditAssessmentCriteriaViewModel

@If Model.Criteria.Count > 0 Then

    @<table class="edit-table">

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
            <th></th>
            <th></th>
            <th></th>
            <th></th>
        </tr>

        @*Item Rows*@
        @For Each ac In Model.Criteria.OrderBy(Function(a) a.CriterionIndex)
            @code
                Dim currentItem = ac
            End Code
            @<tr>
                <td>
                    @ac.CalculationFilter.FilterName
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
                <td>
                    @Html.JQueryUI().Button("Edit",
                                            New With {.id = "editCriterionButton" + ac.Id.ToString})
                </td>
                <td>
                    @If ac.CriterionIndex > 1 Then
                        @Ajax.RouteLink("Move Up",
                                        "AssessmentCriterionMoveUpRoute",
                                        New With {.AssessmentCriterionId = ac.Id},
                                        New AjaxOptions With {.HttpMethod = "POST",
                                                              .OnSuccess = "ajaxOpSuccess",
                                                              .OnBegin = "displayLoadingDiv",
                                                              .OnFailure = "ajaxOpError"},
                                        New With {.class = "sitewide-button-16 up-button-16"})
                    End If
                </td>
                <td>
                    @If ac.CriterionIndex < Model.Criteria.Count Then
                        @Ajax.RouteLink("Move Down",
                                        "AssessmentCriterionMoveDownRoute",
                                        New With {.AssessmentCriterionId = ac.Id},
                                        New AjaxOptions With {.HttpMethod = "POST",
                                                              .OnSuccess = "ajaxOpSuccess",
                                                              .OnBegin = "displayLoadingDiv",
                                                              .OnFailure = "ajaxOpError"},
                                        New With {.class = "sitewide-button-16 down-button-16"})
                    End If
                </td>
                <td>
                    @Html.RouteLink("Delete",
                                    "AssessmentCriterionDeleteRoute",
                                    New With {.DeleteAssessmentCriterionId = ac.Id},
                                    New With {.class = "DeleteCriterionLink sitewide-button-16 delete-button-16"})
                </td>
            </tr>
        Next

    </table>

Else

    @<p>
        No Criteria have been added for this Monitor Location.
    </p>


End If

@*Add new criterion button and dialog*@
@Html.Partial("Edit_Criteria_CreateCriterion",
              Model.CreateAssessmentCriterionViewModel)
              

@*Edit criteria dialogs*@
@For Each ac In Model.Criteria.OrderBy(Function(a) a.CalculationFilter.FilterName)

    @Html.Partial("Edit_Criteria_EditCriterion",
                  Model.getNewEditAssessmentCriterionViewModel(ac))
Next
   
@*Copy criteria dialog*@
@If Model.CopyFromMonitorLocationList.Count > 0 And Model.Criteria.Count = 0 Then
    @Html.Partial("Edit_Criteria_CopyCriteria",
                  New CopyAssessmentCriteriaViewModel With {
                      .AssessmentCriterionGroupId = Model.AssessmentCriterionGroupId,
                      .CopyToMonitorLocationId = Model.MonitorLocationId,
                      .CopyFromMonitorLocationId = Model.CopyFromMonitorLocationId,
                      .CopyFromMonitorLocationList = Model.CopyFromMonitorLocationList
                      })
End If

