@ModelType AssessmentCriterion

<table class="details-table">
    <tr>
        <th>
            Plot Assessed Level
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.PlotAssessedLevel)
        </td>
    </tr>
    @If Model.PlotAssessedLevel Then
        @<tr>
            <th>
                Series Name
            </th>
            <td>
                @Html.Raw(Model.AssessedLevelSeriesName)
            </td>
        </tr>
        @<tr>
            <th>
                Dash Style
            </th>
            <td>
                @Model.AssessedLevelDashStyle.DashStyleName
            </td>
        </tr>
        @<tr>
            <th>
                Line Width
            </th>
            <td>
                @Model.AssessedLevelLineWidth
            </td>
        </tr>
        @<tr>
            <th>
                Line Colour
            </th>
            <td>
                @Html.TextBoxFor(Function(model) model.AssessedLevelLineColour,
                             New With {.class = "text-box single-line",
                                       .type = "color", .disabled = "disabled"})
            </td>
        </tr>
        @<tr>
            <th>
                Series Type
            </th>
            <td>
                @Model.AssessedLevelSeriesType.SeriesTypeName
            </td>
        </tr>
        @<tr>
            <th>
                Show Markers
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.AssessedLevelMarkersOn)
            </td>
        </tr>
    End If

</table>
