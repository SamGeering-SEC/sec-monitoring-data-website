@ModelType SEC_Monitoring_Data_Website.CalculationFilter

<table class="details-table">
    
    <tr>
        <th>
            Monday
        </th>
        <td>
            @If Model.ApplicableDaysOfWeek.Select(Function(DoW) DoW.DayName).Contains("Monday") = True Then
                @Html.CheckBox("chkMonday", True, New With {.disabled = "disabled"})
            Else
                @Html.CheckBox("chkMonday", False, New With {.disabled = "disabled"})
            End If
        </td>
    </tr>
    <tr>
        <th>
            Tuesday
        </th>
        <td>
            @If Model.ApplicableDaysOfWeek.Select(Function(DoW) DoW.DayName).Contains("Tuesday") = True Then
                @Html.CheckBox("chkTuesday", True, New With {.disabled = "disabled"})
            Else
                @Html.CheckBox("chkTuesday", False, New With {.disabled = "disabled"})
            End If
        </td>
    </tr>
    <tr>
        <th>
            Wednesday
        </th>
        <td>
            @If Model.ApplicableDaysOfWeek.Select(Function(DoW) DoW.DayName).Contains("Wednesday") = True Then
                @Html.CheckBox("chkWednesday", True, New With {.disabled = "disabled"})
            Else
                @Html.CheckBox("chkWednesday", False, New With {.disabled = "disabled"})
            End If
        </td>
    </tr>
    <tr>
        <th>
            Thursday
        </th>
        <td>
            @If Model.ApplicableDaysOfWeek.Select(Function(DoW) DoW.DayName).Contains("Thursday") = True Then
                @Html.CheckBox("chkThursday", True, New With {.disabled = "disabled"})
            Else
                @Html.CheckBox("chkThursday", False, New With {.disabled = "disabled"})
            End If
        </td>
    </tr>
    <tr>
        <th>
            Friday
        </th>
        <td>
            @If Model.ApplicableDaysOfWeek.Select(Function(DoW) DoW.DayName).Contains("Friday") = True Then
                @Html.CheckBox("chkFriday", True, New With {.disabled = "disabled"})
            Else
                @Html.CheckBox("chkFriday", False, New With {.disabled = "disabled"})
            End If
        </td>
    </tr>
    <tr>
        <th>
            Saturday
        </th>
        <td>
            @If Model.ApplicableDaysOfWeek.Select(Function(DoW) DoW.DayName).Contains("Saturday") = True Then
                @Html.CheckBox("chkSaturday", True, New With {.disabled = "disabled"})
            Else
                @Html.CheckBox("chkSaturday", False, New With {.disabled = "disabled"})
            End If
        </td>
    </tr>
    <tr>
        <th>
            Sunday
        </th>
        <td>
            @If Model.ApplicableDaysOfWeek.Select(Function(DoW) DoW.DayName).Contains("Sunday") = True Then
                @Html.CheckBox("chkSunday", True, New With {.disabled = "disabled"})
            Else
                @Html.CheckBox("chkSunday", False, New With {.disabled = "disabled"})
            End If
        </td>
    </tr>
    <tr>
        <th>
            Public Holiday
        </th>
        <td>
            @If Model.ApplicableDaysOfWeek.Select(Function(DoW) DoW.DayName).Contains("Public Holiday") = True Then
                @Html.CheckBox("chkPublicHoliday", True, New With {.disabled = "disabled"})
            Else
                @Html.CheckBox("chkPublicHoliday", False, New With {.disabled = "disabled"})
            End If
        </td>
    </tr>

</table>