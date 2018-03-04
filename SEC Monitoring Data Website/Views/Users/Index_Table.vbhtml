@ModelType List(Of SEC_Monitoring_Data_Website.User)

@Code
    Dim showUserLinks = DirectCast(ViewData("ShowUserLinks"), Boolean)
    Dim showUserAccessLevelLinks = DirectCast(ViewData("ShowUserAccessLevelLinks"), Boolean)
    Dim showContactLinks = DirectCast(ViewData("ShowContactLinks"), Boolean)
    Dim showOrganisationLinks = DirectCast(ViewData("ShowOrganisationLinks"), Boolean)
    Dim showDeleteUserLinks = DirectCast(ViewData("ShowDeleteUserLinks"), Boolean)
    Dim currentUserName = DirectCast(ViewData("CurrentUserName"), String)
    
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                User Name
            </th>
            <th>
                User Access Level
            </th>
            <th>
                Contact Name
            </th>
            <th>
                Contact Organisation
            </th>
            <th></th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
                Dim userAccessLevel = currentItem.UserAccessLevel
                Dim contact = currentItem.Contact
                Dim organisation = contact.Organisation
            @<tr>
                <td>
                    @If showUserLinks Then
                        @Html.RouteLink(
                            currentItem.UserName, "UserDetailsRoute",
                            New With {.UserRouteName = currentItem.getRouteName}
                        )
                    Else
                        @currentItem.UserName
                    End If
                </td>
                <td>
                    @If showUserAccessLevelLinks Then
                        @Html.RouteLink(
                            userAccessLevel.AccessLevelName, "UserAccessLevelDetailsRoute",
                            New With {.UserAccessLevelRouteName = userAccessLevel.getRouteName}
                        )
                    Else
                        @userAccessLevel.AccessLevelName
                    End If
                </td>
                 <td>
                     @If showContactLinks Then
                         @Html.RouteLink(
                            contact.ContactName, "ContactDetailsRoute",
                            New With {.ContactRouteName = contact.getRouteName}
                        )
                     Else
                         @contact.ContactName
                     End If
                 </td>
                <td>
                    @If showOrganisationLinks Then
                        @Html.RouteLink(
                            organisation.FullName, "OrganisationDetailsRoute",
                            New With {.OrganisationRouteName = organisation.getRouteName}
                        )
                    Else
                        @organisation.FullName
                    End If
                </td>
                <td>
                    @If showDeleteUserLinks Then
                        @If currentItem.canBeDeleted = True And currentItem.UserName.ToLower() <> currentUserName.ToLower() Then
                            @Html.RouteLink(
                                "Delete",
                                "UserDeleteByIdRoute",
                                New With {.UserId = currentItem.Id},
                                New With {.class = "DeleteUserLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>
            </tr>
        Next

    </table>
Else
    @<p>
        There are no Users available to view.
    </p>
End If
