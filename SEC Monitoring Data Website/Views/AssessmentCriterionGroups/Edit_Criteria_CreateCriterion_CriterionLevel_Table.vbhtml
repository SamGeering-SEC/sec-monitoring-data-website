@ModelType SEC_Monitoring_Data_Website.CreateAssessmentCriterionPopUpViewModel

<table class="create-table">
    <tr>
        <th>
            Tabulate Criterion Triggers
        </th>
        <td>
            @Html.EditorFor(Function(model) model.TabulateCriterionTriggers)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.TabulateCriterionTriggers)
        </td>
    </tr>
    <tr>
        <th>
            Merge Criterion Triggers
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MergeCriterionTriggers)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MergeCriterionTriggers)
        </td>
    </tr>
    <tr>
        <th>
            Criterion Trigger Header 1
        </th>
        <td>
            @Html.EditorFor(Function(model) model.CriterionTriggerHeader1)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CriterionTriggerHeader1)
        </td>
    </tr>
    <tr>
        <th>
            Criterion Trigger Header 2
        </th>
        <td>
            @Html.EditorFor(Function(model) model.CriterionTriggerHeader2)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CriterionTriggerHeader2)
        </td>
    </tr>
    <tr>
        <th>
            Criterion Trigger Header 3
        </th>
        <td>
            @Html.EditorFor(Function(model) model.CriterionTriggerHeader3)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CriterionTriggerHeader3)
        </td>
    </tr>
</table>