@ModelType SEC_Monitoring_Data_Website.Country

@Code
    Dim showCreatePublicHolidayLink = DirectCast(ViewData("ShowCreatePublicHolidayLink"), Boolean)
    Dim showDeletePublicHolidayLinks = DirectCast(ViewData("ShowDeletePublicHolidayLinks"), Boolean)
End Code

<table class="edit-table">
    <tr>
        <th>
            Holiday Name
        </th>
        <th>
            Holiday Date
        </th>
        <th>
        </th>
    </tr>
    @For Each p In Model.PublicHolidays.OrderBy(Function(ph) ph.HolidayDate)
        Dim trIdPublicHolidayCountry = "trPublicHolidayCountry" + p.Id.ToString
        @<tr id='@trIdPublicHolidayCountry'>
            <td>
                @p.HolidayName
            </td>
            <td>
                @Format(p.HolidayDate, "dddd dd-MMM-yy")
            </td>
            <td>
                @If showDeletePublicHolidayLinks Then
                    @Ajax.RouteLink("Delete Holiday",
                                "CountryDeletePublicHolidayRoute",
                                New With {.CountryRouteName = Model.getRouteName,
                                          .PublicHolidayId = p.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                      .OnComplete = "$('#" + trIdPublicHolidayCountry + "').hide();"},
                                New With {.class = "sitewide-button-16 delete-button-16"})
                End If
            </td>
        </tr>
    Next
</table>

@If showCreatePublicHolidayLink Then
    @<table>
        <tr>
            <td>
                @Html.RouteLink("Add a new Public Holiday",
                                "CountryCreatePublicHolidayRoute",
                                New With {.CountryRouteName = Model.getRouteName},
                                New With {.class = "sitewide-button-64 create-button-64"})
            </td>
        </tr>
    </table>
End If

