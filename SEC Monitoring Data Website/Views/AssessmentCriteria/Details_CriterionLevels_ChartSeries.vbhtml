@ModelType AssessmentCriterion

<table class="details-table">
    <tr>
        <th>
            Plot Criterion Level
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.PlotCriterionLevel)
        </td>
    </tr>
    @If Model.PlotCriterionLevel Then
        @<tr>
            <th>
                Series Name
            </th>
            <td>
                @Html.Raw(Model.CriterionLevelSeriesName)
            </td>
        </tr>
        @<tr>
            <th>
                Dash Style
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.CriterionLevelDashStyle.DashStyleName)
            </td>
        </tr>
        @<tr>
            <th>
                Line Width
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.CriterionLevelLineWidth)
            </td>
        </tr>
        @<tr>
            <th>
                Line Colour
            </th>
            <td>
                @Html.TextBoxFor(Function(model) model.CriterionLevelLineColour,
                             New With {.class = "text-box single-line",
                                       .type = "color", .disabled = "disabled"})
            </td>
        </tr>
    End If
</table>
