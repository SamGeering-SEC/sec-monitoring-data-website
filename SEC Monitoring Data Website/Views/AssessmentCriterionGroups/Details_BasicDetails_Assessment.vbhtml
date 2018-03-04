@ModelType SEC_Monitoring_Data_Website.AssessmentCriterionGroupDetailsViewModel

<table class="details-table">
    <tr>
        <th>
            Group Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.GroupName)
        </td>
    </tr>
    <tr>
        <th>
            Project
        </th>
        <td>
            @If ViewData("ShowDetailsProjectLink") = True Then
                @Html.RouteLink(Model.AssessmentCriterionGroup.Project.FullName,
                                "ProjectDetailsRoute",
                                New With {.ProjectRouteName = Model.AssessmentCriterionGroup.Project.getRouteName})
            Else
                @Model.AssessmentCriterionGroup.Project.FullName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Measurement Type
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.MeasurementType.MeasurementTypeName)
        </td>
    </tr>
    <tr>
        <th>
            Threshold Aggregate Duration
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.ThresholdAggregateDuration.AggregateDurationName)
        </td>
    </tr>
    <tr>
        <th>
            Assessment Period Duration Type
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.AssessmentPeriodDurationType.DurationTypeName)
        </td>
    </tr>
    <tr>
        <th>
            Assessment Period Duration Count
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.AssessmentPeriodDurationCount)
        </td>
    </tr>
</table>
