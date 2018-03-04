@ModelType IEnumerable(Of SEC_Monitoring_Data_Website.VariedWeeklyWorkingHours)

@Code
    Dim showEditVariationLinks = DirectCast(ViewData("ShowEditVariationLinks"), Boolean)
    Dim showDeleteVariationLinks = DirectCast(ViewData("ShowDeleteVariationLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="edit-table">
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
            <th></th>
        </tr>
        @For Each variation In Model
            @<tr>
                <td>
                    @Format(variation.StartDate, "ddd dd-MMM-yy")
                </td>
                <td>
                    @Format(variation.EndDate, "ddd dd-MMM-yy")
                </td>
                @*<td>
                        @For Each mv In variation.AvailableMeasurementViews
                            @mv.ViewName@<br>
                        Next
                    </td>*@
                <td>
                    @If showEditVariationLinks Then
                        @Html.RouteLink("Edit",
                                        "VariedWeeklyWorkingHoursEditRoute",
                                        New With {.VariedWeeklyWorkingHoursId = variation.Id},
                                        New With {.class = "sitewide-button-16 edit-button-16"})
                    End If
                </td>
                <td>
                    @If showDeleteVariationLinks Then
                        @Html.RouteLink("Delete",
                                        "VariedWeeklyWorkingHoursDeleteRoute",
                                        New With {.VariedWeeklyWorkingHoursId = variation.Id},
                                        New With {.class = "DeleteVariedWeeklyWorkingHoursLink sitewide-button-16 delete-button-16"})
                    End If
                </td>
            </tr>
        Next
    </table>

End If

