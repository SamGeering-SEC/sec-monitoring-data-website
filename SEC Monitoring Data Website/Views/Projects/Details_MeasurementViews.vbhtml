@ModelType Project

@Code
    Dim showMeasurementViewLinks = DirectCast(ViewData("ShowMeasurementViewLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            View Name
        </th>
        <th>
            Measurement Type
        </th>
    </tr>

    @*Item Rows*@
    @For Each mv In Model.MeasurementViews.OrderBy(Function(v) v.ViewName)
        @<tr>
            <td>
                @If showMeasurementViewLinks Then
                    @Html.RouteLink(mv.ViewName,
                                "MeasurementViewDetailsRoute",
                                New With {.MeasurementViewRouteName = mv.getRouteName})
                Else
                    @mv.ViewName
                End If
            </td>
            <td>
                @mv.MeasurementType.MeasurementTypeName
            </td>
        </tr>
    Next

</table>