@ModelType SEC_Monitoring_Data_Website.MeasurementView

@Code
    Dim showMeasurementCommentTypeLinks = DirectCast(ViewData("ShowMeasurementCommentTypeLinks"), Boolean)
End Code

<h3>Types of Comment which prevent the View from being displayed</h3>

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Comment Type
        </th>
    </tr>

    @*Item Rows*@
    @For Each ct In Model.ExcludingMeasurementCommentTypes
        @<tr>
            <td>
                @If showMeasurementCommentTypeLinks Then
                    @Html.RouteLink(ct.CommentTypeName,
                                "MeasurementCommentTypeDetailsRoute",
                                New With {.MeasurementCommentTypeRouteName = ct.getRouteName})
                Else
                    @ct.CommentTypeName
                End If
            </td>
        </tr>
    Next

</table>