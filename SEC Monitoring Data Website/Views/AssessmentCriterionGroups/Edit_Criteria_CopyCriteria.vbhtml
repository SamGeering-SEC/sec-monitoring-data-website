@ModelType SEC_Monitoring_Data_Website.CopyAssessmentCriteriaViewModel

<p>@Html.JQueryUI().Button("Copy From", New With {.id = "copyCriteriaButton"})</p>

@Using Html.JQueryUI().Begin(New Dialog(New With {.class = "CopyCriteriaDialog"}).AutoOpen(False).Width(800).Height(250).Title("Copy Criteria").TriggerClick("#copyCriteriaButton").Modal(True))

    @Using Ajax.BeginRouteForm("AssessmentCriteriaCopyRoute", New AjaxOptions With {.UpdateTargetId = "monitor_location_criteria",
                                                                                    .InsertionMode = InsertionMode.Replace,
                                                                                    .HttpMethod = "POST",
                                                                                    .LoadingElementId = "divLoadingElement",
                                                                                    .OnSuccess = "renderHelpers"})

        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True)

        @Html.HiddenFor(Function(model) model.AssessmentCriterionGroupId)
        @Html.HiddenFor(Function(model) model.CopyToMonitorLocationId)
                
        @<table class="create-table">
            <tr>
                <th>
                    Copy from Monitor Location:
                </th>
                <td>
                    @Html.DropDownListFor(Function(model) model.CopyFromMonitorLocationId, Model.CopyFromMonitorLocationList, "Please select a Monitor Location...")
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.CopyFromMonitorLocationId)
                </td>
            </tr>
        </table>

        @<p>
            @Html.JQueryUI.Button("Copy Criteria")
        </p>

            End Using

        End Using