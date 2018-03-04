@ModelType Project

@Code
    Dim showVariationLinks = DirectCast(ViewData("ShowVariationLinks"), Boolean)
End Code

<h3>Standard Hours</h3>
<table class="details-table">
    <tr>
        <th>
            Day
        </th>
        <th>
            Start Time
        </th>
        <th>
            End Time
        </th>
    </tr>

    @For Each dwh In Model.StandardWeeklyWorkingHours.StandardDailyWorkingHours.OrderBy(Function(d) d.DayOfWeekId)
        @<tr>
            <td>
                @dwh.DayOfWeek.DayName
            </td>
            <td style="text-align:center">
                @Format(dwh.StartTime, "HH:mm")
            </td>
            <td style="text-align:center">
                @Format(dwh.EndTime, "HH:mm")
            </td>
        </tr>
    Next

</table>

@*@If Model.StandardWeeklyWorkingHours.AvailableMeasurementViews.Count > 0 Then

        @<table class="details-table">
        <tr>
            <th>
                Measurement Views
            </th>
        </tr>
        @For Each mv In Model.StandardWeeklyWorkingHours.AvailableMeasurementViews
            @<tr>
                <td>
                    @Html.RouteLink(mv.ViewName, "MeasurementViewDetailsRoute", New With {.MeasurementViewRouteName = mv.getRouteName})
                </td>
            </tr>
        Next
    </table>

    End If*@




@If Model.VariedWeeklyWorkingHours.Count > 0 Then

    @<h3>Variations</h3>

    @<table class="details-table">
        <tr>
            <th>
                Start Date
            </th>
            <th>
                End Date
            </th>
            @*<th>
                    Measurement Views
                </th>*@
            <th></th>
        </tr>

        @For Each vwh In Model.VariedWeeklyWorkingHours
            @<tr>
                <td>
                    @Format(vwh.StartDate, "ddd dd-MMM-yy")
                </td>
                <td>
                    @Format(vwh.EndDate, "ddd dd-MMM-yy")
                </td>
                @*<td>
                        @For Each mv In vwh.AvailableMeasurementViews
                            @Html.RouteLink(mv.ViewName, "MeasurementViewDetailsRoute", New With {.MeasurementViewRouteName = mv.getRouteName})@<br>
                        Next
                    </td>*@
                <td>
                    @If showVariationLinks Then
                        @Html.RouteLink("Details",
                                    "VariedWeeklyWorkingHoursDetailsRoute",
                                    New With {.VariedWeeklyWorkingHoursId = vwh.Id},
                                    New With {.class = "sitewide-button-16 variations-button-16"})
                    End If
                </td>
            </tr>
        Next

    </table>

End If
