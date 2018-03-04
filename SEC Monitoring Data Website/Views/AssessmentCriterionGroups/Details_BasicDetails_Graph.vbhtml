@ModelType SEC_Monitoring_Data_Website.AssessmentCriterionGroupDetailsViewModel

<table class="details-table">
    <tr>
        <th>
            Show Graph
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.ShowGraph)
        </td>
    </tr>
    <tr>
        <th>
            Graph Title
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.GraphTitle)
        </td>
    </tr>
    <tr>
        <th>
            Graph X-axis Label
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.GraphXAxisLabel)
        </td>
    </tr>
    <tr>
        <th>
            Graph Y-axis Label
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.GraphYAxisLabel)
        </td>
    </tr>
    <tr>
        <th>
            Graph Y-axis Min
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.GraphYAxisMin)
        </td>
    </tr>
    <tr>
        <th>
            Graph Y-axis Max
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.GraphYAxisMax)
        </td>
    </tr>
    <tr>
        <th>
            Graph Y-axis Tick Interval
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.AssessmentCriterionGroup.GraphYAxisTickInterval)
        </td>
    </tr>
</table>
