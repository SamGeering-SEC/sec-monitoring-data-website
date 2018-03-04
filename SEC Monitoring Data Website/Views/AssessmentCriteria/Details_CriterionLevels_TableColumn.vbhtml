@ModelType AssessmentCriterion

<table class="details-table">
    <tr>
        <th>
            Tabulate Criterion Triggers
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.TabulateCriterionTriggers)
        </td>
    </tr>
    @If Model.TabulateCriterionTriggers Then
        @<tr>
            <th>
                Merge Identically Named Columns
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.MergeCriterionTriggers)
            </td>
        </tr>
        @<tr>
            <th>
                Row 1 Header
            </th>
            <td>
                @Html.Raw(Model.CriterionTriggerHeader1)
            </td>
        </tr>
        @<tr>
            <th>
                Row 2 Header
            </th>
            <td>
                @Html.Raw(Model.CriterionTriggerHeader2)
            </td>
        </tr>
        @<tr>
            <th>
                Row 3 Header
            </th>
            <td>
                @Html.Raw(Model.CriterionTriggerHeader3)
            </td>
        </tr>
    End If
</table>
