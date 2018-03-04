@ModelType List(Of SEC_Monitoring_Data_Website.MeasurementCommentType)

@Code
    Dim showMeasurementCommentTypeLinks = DirectCast(ViewData("ShowMeasurementCommentTypeLinks"), Boolean)
    Dim showDeleteMeasurementCommentTypeLinks = DirectCast(ViewData("ShowDeleteMeasurementCommentTypeLinks"), Boolean)
End Code


@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                Comment Type Name
            </th>
            <th></th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showMeasurementCommentTypeLinks Then
                        @Html.RouteLink(currentItem.CommentTypeName,
                                "MeasurementCommentTypeDetailsRoute",
                                New With {.MeasurementCommentTypeRouteName = currentItem.getRouteName})
                    Else
                        @currentItem.CommentTypeName
                    End If
                </td>
                <td>
                    @If showDeleteMeasurementCommentTypeLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                     "MeasurementCommentTypeDeleteByIdRoute",
                                     New With {.MeasurementCommentTypeId = currentItem.Id},
                                     New With {.class = "DeleteMeasurementCommentTypeLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>
            </tr>
        Next

    </table>

Else
    @<p>
        There are no Comment Types available to view.
    </p>
End If
