@ModelType SEC_Monitoring_Data_Website.Country

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Holiday Date
        </th>
        <th>
            Holiday Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each ph In Model.PublicHolidays.OrderBy(Function(hol) hol.HolidayDate)
        @<tr>
            <td>
                @Format(ph.HolidayDate,"dd MMMM yyyy")
            </td>
            <td>
                @ph.HolidayName
            </td>
        </tr>
    Next

</table>
