@ModelType SEC_Monitoring_Data_Website.MeasurementCommentType

@Code
    Dim showExcludedMeasurementViewLinks = DirectCast(ViewData("ShowExcludedMeasurementViewLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            View Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each emv In Model.ExcludedMeasurementViews.OrderBy(Function(mv) mv.ViewName)
        @<tr>
            <td>
                @If showExcludedMeasurementViewLinks Then
                    @Html.RouteLink(emv.ViewName, 
                                    "MeasurementViewDetailsRoute", 
                                    New With {.MeasurementViewRouteName = emv.getRouteName})
                Else
                    @emv.ViewName
                End If
            </td>
        </tr>
    Next

</table>