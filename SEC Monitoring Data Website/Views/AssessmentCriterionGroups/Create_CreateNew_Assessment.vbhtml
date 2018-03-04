@ModelType SEC_Monitoring_Data_Website.CreateNewAssessmentCriterionGroupViewModel

<table class="create-table">
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
            @Html.DropDownListFor(Function(model) model.MeasurementTypeId, Model.MeasurementTypeList, "Please select a Measurement Type...")
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementTypeId)
        </td>
    </tr>
    <tr>
        <th>
            Threshold Aggregate Duration
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.ThresholdAggregateDurationId, Model.ThresholdAggregateDurationList, "Please select a Threshold Aggregate Duration...")
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
            @Html.DropDownListFor(Function(model) model.AssessmentPeriodDurationTypeId, Model.AssessmentPeriodDurationTypeList, "Please select a Assessment Period Duration Type...")
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
