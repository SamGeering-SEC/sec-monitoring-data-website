@ModelType SEC_Monitoring_Data_Website.CreateAssessmentCriterionViewModel

<table class="create-table">
    <tr>
        <th>
            Tabulate Assessed Levels
        </th>
        <td>
            @Html.EditorFor(Function(model) model.TabulateAssessedLevels)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.TabulateAssessedLevels)
        </td>
    </tr>
    <tr>
        <th>
            Merge Identically Named Columns
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MergeAssessedLevels)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MergeAssessedLevels)
        </td>
    </tr>
    <tr>
        <th>
            Row 1 Header
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessedLevelHeader1)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessedLevelHeader1)
        </td>
    </tr>
    <tr>
        <th>
            Row 2 Header
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessedLevelHeader2)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessedLevelHeader2)
        </td>
    </tr>
    <tr>
        <th>
            Row 3 Header
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessedLevelHeader3)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessedLevelHeader3)
        </td>
    </tr>
</table>