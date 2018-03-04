@ModelType SEC_Monitoring_Data_Website.EditMeasurementCommentTypeViewModel

<table class="edit-table">
    <tr>
        <th>
            Excluded Criteria
        </th>
        <th>
        </th>
    </tr>
    @For Each e In Model.AllExcludedAssessmentCriterionGroups
        Dim trIdExcludedAssessmentCriterionGroupMeasurementCommentType = "trExcludedAssessmentCriterionGroupMeasurementCommentType" + e.Id.ToString
        Dim trIdExcludedAssessmentCriterionGroupNonMeasurementCommentType = "trExcludedAssessmentCriterionGroupNonMeasurementCommentType" + e.Id.ToString
        @<tr id='@trIdExcludedAssessmentCriterionGroupMeasurementCommentType'>
            <td>
                @e.GroupName (@e.Project.FullName)
            </td>
            <td>
                @Ajax.RouteLink("Remove from Measurement Comment Type",
                                "MeasurementCommentTypeRemoveExcludedAssessmentCriterionGroupRoute",
                                New With {.MeasurementCommentTypeShortName = Model.MeasurementCommentType.getRouteName,
                                          .ExcludedAssessmentCriterionGroupId = e.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                      .OnComplete = "$('#" + trIdExcludedAssessmentCriterionGroupMeasurementCommentType + "').hide();$('#" + trIdExcludedAssessmentCriterionGroupNonMeasurementCommentType + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getMeasurementCommentTypeExcludedAssessmentCriterionGroupIds.Contains(e.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdExcludedAssessmentCriterionGroupMeasurementCommentType + "').hide(); </script>")
        End If
    Next
</table>

<table class="edit-table">
    <tr>
        <th>
            Other Criteria
        </th>
        <th>
        </th>
    </tr>
    @For Each e In Model.AllExcludedAssessmentCriterionGroups
        Dim trIdExcludedAssessmentCriterionGroupMeasurementCommentType = "trExcludedAssessmentCriterionGroupMeasurementCommentType" + e.Id.ToString
        Dim trIdExcludedAssessmentCriterionGroupNonMeasurementCommentType = "trExcludedAssessmentCriterionGroupNonMeasurementCommentType" + e.Id.ToString
        @<tr id='@trIdExcludedAssessmentCriterionGroupNonMeasurementCommentType'>
            <td>
                @e.GroupName (@e.Project.FullName)
            </td>
            <td>
                @Ajax.RouteLink("Add to Measurement Comment Type",
                                "MeasurementCommentTypeAddExcludedAssessmentCriterionGroupRoute",
                                New With {.MeasurementCommentTypeShortName = Model.MeasurementCommentType.getRouteName,
                                          .ExcludedAssessmentCriterionGroupId = e.Id},
                                New AjaxOptions With {.HttpMethod = "PUT",
                                                      .OnComplete = "$('#" + trIdExcludedAssessmentCriterionGroupMeasurementCommentType + "').show();$('#" + trIdExcludedAssessmentCriterionGroupNonMeasurementCommentType + "').hide();"},
                                New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getMeasurementCommentTypeExcludedAssessmentCriterionGroupIds.Contains(e.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdExcludedAssessmentCriterionGroupNonMeasurementCommentType + "').hide(); </script>")
        End If
    Next
</table>

