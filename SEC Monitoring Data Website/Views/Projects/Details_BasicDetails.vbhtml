@ModelType Project

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
            Project Number
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.ProjectNumber)
        </td>
    </tr>
    <tr>
        <th>
            Location
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.ProjectGeoCoords)
        </td>
    </tr>
    @*@If Model.MapLink <> "" Then
        @<tr>
            <th>
                Map Link
            </th>
            <td>
                <a href='@Model.MapLink' target='_blank'>View Map</a>
            </td>
        </tr>
    End If*@
    <tr>
        <th>
            Client
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.ClientOrganisation.FullName)
        </td>
    </tr>
    <tr>
        <th>
            Country
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.Country.CountryName)
        </td>
    </tr>
</table>