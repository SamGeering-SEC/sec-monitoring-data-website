@ModelType SEC_Monitoring_Data_Website.VariedWeeklyWorkingHours

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            View Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each amv In Model.AvailableMeasurementViews
        @<tr>
            <td>
                @Html.RouteLink(amv.ViewName, "MeasurementViewDetailsRoute", New With {.MeasurementViewRouteName = amv.getRouteName})
            </td>
        </tr>
    Next

</table>