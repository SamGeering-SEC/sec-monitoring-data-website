@ModelType SEC_Monitoring_Data_Website.CreateAssessmentCriterionViewModel

<table class="create-table">
    <tr>
        <th>
            Plot Assessed Level
        </th>
        <td>
            @Html.EditorFor(Function(model) model.PlotAssessedLevel)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.PlotAssessedLevel)
        </td>
    </tr>
    <tr>
        <th>
            Series Name
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessedLevelSeriesName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessedLevelSeriesName)
        </td>
    </tr>
    <tr>
        <th>
            Dash Style
        </th>
        <td>
            @Html.DropDownListFor(
                Function(model) model.AssessedLevelDashStyleId,
                                Model.AssessedLevelDashStyleList,
                                "Please select a Dash Style..."
            )
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessedLevelDashStyleId)
        </td>
    </tr>
    <tr>
        <th>
            Line Width
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessedLevelLineWidth)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessedLevelLineWidth)
        </td>
    </tr>
    <tr>
        <th>
            Line Colour
        </th>
        <td>
            @Html.TextBoxFor(
                Function(model) model.AssessedLevelLineColour,
                New With {.class = "text-box single-line", .type = "color"}
            )
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessedLevelLineColour)
        </td>
    </tr>
    <tr>
        <th>
            Series Type
        </th>
        <td>
            @Html.DropDownListFor(
                Function(model) model.AssessedLevelSeriesTypeId,
                                Model.AssessedLevelSeriesTypeList,
                                "Please select a Series Type..."
            )
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessedLevelDashStyleId)
        </td>
    </tr>
    <tr>
        <th>
            Show Markers
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AssessedLevelMarkersOn)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AssessedLevelMarkersOn)
        </td>
    </tr>
</table>
