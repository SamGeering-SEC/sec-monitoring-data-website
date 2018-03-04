@ModelType SEC_Monitoring_Data_Website.EditAssessmentCriterionGroupViewModel

<table class="edit-table">
    <tr>
        <th>
            Group Name
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.GroupName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.GroupName)
        </td>
    </tr>
    <tr>
        <th>
            Project
        </th>
        <td>
            @Model.AssessmentCriterionGroup.Project.FullName
        </td>
        <td>
            @Html.HiddenFor(Function(model) model.AssessmentCriterionGroup.ProjectId)
        </td>
    </tr>
    <tr>
        <th>
            Measurement Type
        </th>
        <td>
            @Model.AssessmentCriterionGroup.MeasurementType.MeasurementTypeName
        </td>
        <td>
            @Html.HiddenFor(Function(model) model.AssessmentCriterionGroup.MeasurementTypeId)
        </td>
    </tr>
    <tr>
        <th>
            Threshold Aggregate Duration
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.ThresholdAggregateDurationId, Model.ThresholdAggregateDurationList)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.ThresholdAggregateDurationId)
        </td>
    </tr>
    <tr>
        <th>
            Assessment Period Duration Type
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.AssessmentPeriodDurationTypeId, Model.AssessmentPeriodDurationTypeList)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentPeriodDurationTypeId)
        </td>
    </tr>
    <tr>
        <th>
            Assessment Period Duration Count
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.AssessmentPeriodDurationCount)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.AssessmentPeriodDurationCount)
        </td>
    </tr>
</table>
