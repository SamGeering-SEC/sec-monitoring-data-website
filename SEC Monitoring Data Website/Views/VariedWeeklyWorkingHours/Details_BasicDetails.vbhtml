@ModelType SEC_Monitoring_Data_Website.VariedWeeklyWorkingHours

<table class="details-table">
    <tr>
        <th>
            Project
        </th>
        <td>
            @Html.RouteLink(Model.Project.FullName, "ProjectDetailsRoute", New With {.ProjectRouteName = Model.Project.getRouteName})
        </td>
    </tr>
    <tr>
        <th>
            Start Date
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.StartDate)
        </td>
    </tr>
    <tr>
        <th>
            End Date
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.EndDate)
        </td>
    </tr>
</table>

<h3>Working Hours</h3>
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

    @For Each dwh In Model.VariedDailyWorkingHours.OrderBy(Function(d) d.DayOfWeekId)
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