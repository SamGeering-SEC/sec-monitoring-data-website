@ModelType SEC_Monitoring_Data_Website.MeasurementViewSequenceSetting

<table class="details-table">
    <tr>
        <th>
            Table Header
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.TableHeader)
        </td>
    </tr>
    <tr>
        <th>
            Series Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.SeriesName)
        </td>
    </tr>
    <tr>
        <th>
            Day View Series Type
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.DayViewSeriesType.SeriesTypeName)
        </td>
    </tr>
    <tr>
        <th>
            Week View Series Type
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.WeekViewSeriesType.SeriesTypeName)
        </td>
    </tr>
    <tr>
        <th>
            Month View Series Type
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MonthViewSeriesType.SeriesTypeName)
        </td>
    </tr>
    <tr>
        <th>
            Series Colour
        </th>
        <td>
            @Html.TextBoxFor(Function(model) model.SeriesColour,
                             New With {.class = "text-box single-line",
                                       .type = "color",
                                       .disabled = "disabled"})
        </td>
    </tr>
    <tr>
        <th>
            Calculation Filter
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.CalculationFilter.FilterName)
        </td>
    </tr>
</table>
<table>
    <tr>
        <td>
            @Html.RouteLink("Edit Sequence", "MeasurementViewSequenceSettingEditRoute",
                    New With {.MeasurementViewRouteName = Model.MeasurementViewGroup.MeasurementView.getRouteName,
                                .GroupIndex = Model.MeasurementViewGroup.GroupIndex,
                                .SequenceIndex = Model.SequenceIndex},
                    New With {.class = "sitewide-button-16 edit-button-16"})
            @Html.RouteLink("Delete Sequence",
                    "MeasurementViewSequenceSettingDeleteRoute",
                    New With {.MeasurementViewRouteName = Model.MeasurementViewGroup.MeasurementView.getRouteName,
                              .GroupIndex = Model.MeasurementViewGroup.GroupIndex,
                              .SequenceIndex = Model.SequenceIndex},
                    New With {.class = "sitewide-button-16 delete-button-16 DeleteSequenceLink"})
        </td>
    </tr>
</table>
