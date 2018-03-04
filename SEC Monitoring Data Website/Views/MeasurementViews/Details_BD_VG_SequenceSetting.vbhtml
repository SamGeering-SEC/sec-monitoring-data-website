@ModelType SEC_Monitoring_Data_Website.MeasurementViewSequenceSetting

@Code
    Dim showCalculationFilterLinks = DirectCast(ViewData("ShowCalculationFilterLinks"), Boolean)
End Code

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
                                       .type = "color", .disabled = "disabled"})
        </td>
    </tr>
    <tr>
        <th>
            Calculation Filter
        </th>
        <td>
            @If showCalculationFilterLinks Then
                @Html.RouteLink(Model.CalculationFilter.FilterName,
                            "CalculationFilterDetailsRoute",
                            New With {.CalculationFilterRouteName = Model.CalculationFilter.getRouteName})
            Else
                @Model.CalculationFilter.FilterName
            End If
        </td>
    </tr>
</table>