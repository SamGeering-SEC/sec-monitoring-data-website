@Modeltype SEC_Monitoring_Data_Website.EditProjectViewModel

@Code
    Dim showEditWorkingHoursLink = DirectCast(ViewData("ShowEditWorkingHoursLink"), Boolean)
    Dim showCreateVariationLink = DirectCast(ViewData("ShowCreateVariationLink"), Boolean)
End Code

<h3>Standard Working Hours</h3>
<table>
    <tr>
        <td>
            @If showEditWorkingHoursLink Then
                @Html.RouteLink("Edit Working Hours",
                                "StandardWeeklyWorkingHoursEditRoute",
                                New With {.ProjectRouteName = Model.Project.getRouteName},
                                New With {.class = "sitewide-button-32 edit-button-32"})
            End If
        </td>
    </tr>
</table>
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

    @For Each dwh In Model.Project.StandardWeeklyWorkingHours.StandardDailyWorkingHours.OrderBy(Function(d) d.DayOfWeekId)
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
@*<table class="edit-table">
        <tr>
            <th>
                Measurement Views
            </th>
        </tr>
        @For Each mv In Model.Project.StandardWeeklyWorkingHours.AvailableMeasurementViews
            @<tr>
                <td>
                    @mv.ViewName
                </td>
            </tr>
        Next
    </table>*@

<h3>Variations</h3>
<div id="VariationsTable">
    @Html.Partial("Edit_WH_Variations", Model.Project.getVariations)
</div>

<table>
    <tr>
        <td>
            @If showCreateVariationLink Then
                @Html.RouteLink("Add a new Variation",
                            "VariedWeeklyWorkingHoursCreateRoute",
                            New With {.ProjectRouteName = Model.Project.getRouteName},
                            New With {.class = "sitewide-button-32 create-button-32"})
            End If
        </td>
    </tr>
</table>


