@modeltype IEnumerable(Of SEC_Monitoring_Data_Website.MeasurementComment)

@Code
    Dim showMeasurementCommentTypeLinks = DirectCast(ViewData("ShowMeasurementCommentTypeLinks"), Boolean)
    Dim showContactLinks = DirectCast(ViewData("ShowContactLinks"), Boolean)
    Dim showDeleteCommentLinks = DirectCast(ViewData("ShowDeleteCommentLinks"), Boolean)
End Code

@Using Html.BeginForm()

    @Html.AntiForgeryToken

    @If Model.ToList.Count = 0 Then

        @<p>
            No Comments have been added for this Monitor Location
        </p>

    Else


        @<table class="index-table">

            @*Header Row*@
            <tr>
                <th>
                    Comment Text
                </th>
                <th>
                    Comment Type
                </th>
                <th>
                    Added On
                </th>
                <th>
                    Added By
                </th>
                <th>
                    Start
                </th>
                <th>
                    End
                </th>
                <th></th>
                <th></th>
            </tr>

            @*Item Rows*@
            @For Each mc In Model
                    @code
                        Dim firstMeasurementStartDateTime = mc.FirstMeasurementStartDateTime
                        Dim lastMeasurementStartDateTime = mc.LastMeasurementEndDateTime
                    End Code
                @<tr>
                    <td>
                        @mc.CommentText
                    </td>
                    <td>
                        @If showMeasurementCommentTypeLinks Then
                            @Html.RouteLink(
                                mc.CommentType.CommentTypeName,
                                "MeasurementCommentTypeDetailsRoute",
                                New With {.MeasurementCommentTypeRouteName = mc.CommentType.getRouteName}
                            )
                        Else
                            @mc.CommentType.CommentTypeName
                        End If
                    </td>
                    <td>
                        @Format(mc.CommentDateTime, "ddd dd-MMM-yy HH:mm")
                    </td>
                    <td>
                        @If showContactLinks Then
                            @Html.RouteLink(
                                mc.Contact.ContactName,
                                "ContactDetailsRoute",
                                New With {.ContactRouteName = mc.Contact.getRouteName}
                            )
                        Else
                            @mc.Contact.ContactName
                        End If
                    </td>
                    <td>
                        @Format(firstMeasurementStartDateTime, "ddd dd-MMM-yy HH:mm")
                    </td>
                     <td>
                        @Format(lastMeasurementStartDateTime, "ddd dd-MMM-yy HH:mm")
                     </td>
                    <td>
                        <input type="button"
                               data-jqui-type="button"
                               value="Zoom To Comment"
                               onclick="setDateRange('@CDate(firstMeasurementStartDateTime).ToString("dd-MMM-yy")', '@CDate(lastMeasurementStartDateTime).AddDays(1).ToString("dd-MMM-yy")')" />
                    <td>
                        @If showDeleteCommentLinks Then
                            @Html.RouteLink(
                                "Delete Comment",
                                "MeasurementCommentDeleteRoute",
                                New With {.MeasurementCommentId = mc.Id,
                                            .ShowMeasurementCommentTypeLinks = showMeasurementCommentTypeLinks,
                                            .ShowContactLinks = showContactLinks,
                                            .ShowDeleteCommentLinks = showDeleteCommentLinks},
                                New With {.class = "DeleteMeasurementCommentLink sitewide-button-16 delete-button-16"}
                            )
                        End If
                    </td>
                </tr>
            Next

        </table>

    End If

End Using





