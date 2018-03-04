@ModelType SEC_Monitoring_Data_Website.Contact

@Code
    Dim showOrganisationLink = DirectCast(ViewData("ShowOrganisationLink"), Boolean)
    Dim showUserLink = DirectCast(ViewData("ShowUserLink"), Boolean)
End Code

<table class="details-table">
    <tr>
        <th>
            Contact Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.ContactName)
        </td>
    </tr>
    <tr>
        <th>
            Email Address
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.EmailAddress)
        </td>
    </tr>
    <tr>
        <th>
            Primary Telephone Number
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.PrimaryTelephoneNumber)
        </td>
    </tr>
    <tr>
        <th>
            Secondary Telephone Number
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.SecondaryTelephoneNumber)
        </td>
    </tr>
    <tr>
        <th>
            Organisation
        </th>
        <td>
            @If showOrganisationLink = True Then
            @Html.RouteLink(Model.Organisation.FullName, 
                            "OrganisationDetailsRoute", 
                            New With {.OrganisationRouteName = Model.Organisation.getRouteName})
            Else
                @Model.Organisation.FullName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Website User
        </th>
        <td>
            @If Model.User IsNot Nothing Then
                Dim user = Model.User
                If showUserLink = True Then
                           @Html.RouteLink(user.UserName, "UserDetailsRoute",
                                           New With {.UserRouteName = user.getRouteName})
                Else
                    @user.UserName
                End If
            End If
        </td>
    </tr>
</table>