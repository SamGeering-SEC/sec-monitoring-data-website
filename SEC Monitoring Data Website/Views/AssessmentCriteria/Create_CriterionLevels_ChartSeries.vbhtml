
@ModelType SEC_Monitoring_Data_Website.CreateAssessmentCriterionViewModel

<table class="create-table">
    <tr>
        <th>
            Plot Criterion Level
        </th>
        <td>
            @Html.EditorFor(Function(model) model.PlotCriterionLevel)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.PlotCriterionLevel)
        </td>
    </tr>
    <tr>
        <th>
            Series Name
        </th>
        <td>
            @Html.EditorFor(Function(model) model.CriterionLevelSeriesName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CriterionLevelSeriesName)
        </td>
    </tr>
    <tr>
        <th>
            Dash Style
        </th>
        <td>
            @Html.DropDownListFor(
                Function(model) model.CriterionLevelDashStyleId,
                                Model.CriterionLevelDashStyleList,
                                "Please select a Dash Style..."
            )
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CriterionLevelDashStyleId)
        </td>
    </tr>
    <tr>
        <th>
            Line Width
        </th>
        <td>
            @Html.EditorFor(Function(model) model.CriterionLevelLineWidth)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CriterionLevelLineWidth)
        </td>
    </tr>
    <tr>
        <th>
            Line Colour
        </th>
        <td>
            @Html.TextBoxFor(
                Function(model) model.CriterionLevelLineColour,
                New With {.class = "text-box single-line", .type = "color"}
            )
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CriterionLevelLineColour)
        </td>
    </tr>
</table>