@ModelType SEC_Monitoring_Data_Website.Organisation

@Code
    Dim showOrganisationTypeLink = DirectCast(ViewData("ShowOrganisationTypeLink"), Boolean)
End Code

<table class="details-table">
    <tr>
        <th>
            Full Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.FullName)
        </td>
    </tr>
    <tr>
        <th>
            Short Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.ShortName)
        </td>
    </tr>
    <tr>
        <th>
            Address
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.Address)
        </td>
    </tr>
    <tr>
        <th>
            Organisation Type
        </th>
        <td>
            @If showOrganisationTypeLink Then
                @Html.RouteLink(Model.OrganisationType.OrganisationTypeName,
                            "OrganisationTypeDetailsRoute",
                            New With {.OrganisationTypeRouteName = Model.OrganisationType.getRouteName})
            Else
                @Model.OrganisationType.OrganisationTypeName
            End If
        </td>
    </tr>
</table>