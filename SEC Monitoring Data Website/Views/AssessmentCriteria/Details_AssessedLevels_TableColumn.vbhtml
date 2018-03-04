@ModelType AssessmentCriterion

<table class="details-table">
    <tr>
        <th>
            Tabulate Assessed Levels
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.TabulateAssessedLevels)
        </td>
    </tr>
    @If Model.TabulateAssessedLevels Then
        @<tr>
            <th>
                Merge Identically Named Columns
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MergeAssessedLevels)
            </td>
        </tr>
        @<tr>
            <th>
                Row 1 Header
            </th>
            <td>
                @Html.Raw(Model.AssessedLevelHeader1)
            </td>
        </tr>
        @<tr>
            <th>
                Row 2 Header
            </th>
            <td>
                @Html.Raw(Model.AssessedLevelHeader2)
            </td>
        </tr>
        @<tr>
            <th>
                Row 3 Header
            </th>
            <td>
                @Html.Raw(Model.AssessedLevelHeader3)
            </td>
        </tr>
    End If

</table>
