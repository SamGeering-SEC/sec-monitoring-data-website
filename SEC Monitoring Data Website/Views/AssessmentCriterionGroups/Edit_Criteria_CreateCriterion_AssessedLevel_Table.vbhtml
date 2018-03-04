@ModelType SEC_Monitoring_Data_Website.CreateAssessmentCriterionPopUpViewModel

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
            Merge Assessed Levels
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
            Assessed Level Header 1
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
            Assessed Level Header 2
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
            Assessed Level Header
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessedLevelHeader3)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessedLevelHeader3)
        </td>
    </tr>
</table>