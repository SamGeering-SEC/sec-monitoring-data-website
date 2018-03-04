@ModelType SEC_Monitoring_Data_Website.EditMonitorLocationViewModel

<script type="text/javascript">

    function update_assessmentcriteria() {
        $.ajax({
            type: 'GET',
            url: '@Url.RouteUrl("MonitorLocationEditUpdateAssessmentCriteriaRoute")',
            data: {
                MonitorLocationId: $('#MonitorLocation_Id').val(),
            },
            success: function (partialView) {
                $('#assessment_criteria').html(partialView);
                $('#assessment_criteria').jQueryUIHelpers();
                addButtonAnimations();
            },
            failure: function () {
                alert('Failed to update Assessment Criteria');
            }
        });
    };

    function deleteAssessmentCriteriaSuccess(data, textStatus, jqXHR) {
        update_assessmentcriteria();
        addButtonAnimations();
    }

    function deleteAssessmentCriteriaError(jqXHR, textStatus, errorThrown) {
        alert(textStatus);
    }

</script>

<table class="edit-table">
    <tr>
        <th>
            Assessment Group
        </th>
        <th>
            Description
        </th>
        <th style="text-align:center">
            Number of Criteria
        </th>
        <th></th>
        <th></th>
    </tr>
    @For Each acg In Model.MonitorLocation.getAssessmentCriterionGroups
        @<tr>
            <td>
                @acg.GroupName
            </td>
            <td>
                @acg.getDescription
            </td>
            <td style="text-align:center">
                @acg.AssessmentCriteria.Where(Function(ac) ac.MonitorLocationId = Model.MonitorLocation.Id).Count
            </td>
            <td>
                @Html.RouteLink("Edit Group", "AssessmentCriterionGroupEditRoute",
                                New With {.ProjectRouteName = Model.Project.getRouteName,
                                          .AssessmentCriterionGroupRouteName = acg.getRouteName},
                                New With {.class = "sitewide-button-16 edit-button-16"})
            </td>
            <td>
                @Html.RouteLink("Delete Group", "MonitorLocationAssessmentCriterionGroupDeleteRoute",
                                New With {.MonitorLocationId = Model.MonitorLocation.Id,
                                          .AssessmentCriterionGroupId = acg.Id},
                                New With {.class = "sitewide-button-16 delete-button-16 DeleteAssessmentCriteriaLink"})
            </td>
        </tr>
    Next

</table>

@Using (Html.JQueryUI().Begin(New Dialog().Title("Delete Assessment Criteria").AutoOpen(False).Modal(True).ConfirmAjax(".DeleteAssessmentCriteriaLink", "Yes", "No", New AjaxSettings With {.Method = HttpVerbs.Post,
.Success = "deleteAssessmentCriteriaSuccess",
.Error = "deleteAssessmentCriteriaError"})))
    @<p>
        @Html.Raw("Would you like to delete Assessment Criteria in this Group for the Monitor Location?")
    </p>
End Using