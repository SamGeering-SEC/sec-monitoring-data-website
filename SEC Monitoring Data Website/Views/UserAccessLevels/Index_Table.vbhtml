@ModelType List(Of SEC_Monitoring_Data_Website.UserAccessLevel)

@Code
    Dim showUserAccessLevelLinks = DirectCast(ViewData("ShowUserAccessLevelLinks"), Boolean)
    Dim showDeleteUserAccessLevelLinks = DirectCast(ViewData("ShowDeleteUserAccessLevelLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
    <tr>
        <th>
            Access Level Name
        </th>
    </tr>
    @For Each item In Model
            Dim currentItem = item
        @<tr>
            <td>
                @If showUserAccessLevelLinks Then
                    @Html.RouteLink(
                                currentItem.AccessLevelName,
                                "UserAccessLevelDetailsRoute",
                                New With {.UserAccessLevelRouteName = currentItem.getRouteName}
                        )
                Else
                    @currentItem.AccessLevelName
                End If
            </td>
            <td>
                @If showDeleteUserAccessLevelLinks Then
                    @If currentItem.canBeDeleted = True Then
                        @Html.RouteLink(
                                "Delete",
                                "UserAccessLevelDeleteByIdRoute",
                                New With {.UserAccessLevelId = currentItem.Id},
                                New With {.class = "DeleteUserAccessLevelLink sitewide-button-16 delete-button-16"}
                            )
                    End If
                End If
            </td>
        </tr>
    Next
</table>

Else
    @<p>
        There are no User Access Levels available to view.
    </p>
End If
