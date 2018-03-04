@ModelType SEC_Monitoring_Data_Website.CreateNewAssessmentCriterionGroupViewModel

<table class="create-table">
    <tr>
        <th>
            # Date Columns
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.NumDateColumns)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.NumDateColumns)
        </td>
    </tr>
    <tr>
        <th>
            Date Column 1 Header
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.DateColumn1Header)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.DateColumn1Header)
        </td>
    </tr>
    <tr>
        <th>
            Date Column 1 Format
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.DateColumn1Format)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.DateColumn1Format)
        </td>
    </tr>
    <tr>
        <th>
            Date Column 2 Header
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.DateColumn2Header)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.DateColumn2Header)
        </td>
    </tr>
    <tr>
        <th>
            Date Column 2 Format
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.DateColumn2Format)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.DateColumn2Format)
        </td>
    </tr>
    <tr>
        <th>
            Merge Header Row 1
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.MergeHeaderRow1)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.MergeHeaderRow1)
        </td>
    </tr>
    <tr>
        <th>
            Merge Header Row 2
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.MergeHeaderRow2)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.MergeHeaderRow2)
        </td>
    </tr>
    <tr>
        <th>
            Merge Header Row 3
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.MergeHeaderRow3)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.MergeHeaderRow3)
        </td>
    </tr>
    <tr>
        <th>
            Show Individual Results
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.ShowIndividualResults)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.ShowIndividualResults)
        </td>
    </tr>
    <tr>
        <th>
            Sum Exceedances Across Criteria
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.SumExceedancesAcrossCriteria)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.SumExceedancesAcrossCriteria)
        </td>
    </tr>
    <tr>
        <th>
            Sum Period Exceedances
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.SumPeriodExceedances)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.SumPeriodExceedances)
        </td>
    </tr>
    <tr>
        <th>
            Sum Days With Exceedances
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.SumDaysWithExceedances)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.SumDaysWithExceedances)
        </td>
    </tr>
    <tr>
        <th>
            Sum Daily Events
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.SumDailyEvents)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.SumDailyEvents)
        </td>
    </tr>
    <tr>
        <th>
            Show Sum Titles
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessmentCriterionGroup.ShowSumTitles)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessmentCriterionGroup.ShowSumTitles)
        </td>
    </tr>
</table>