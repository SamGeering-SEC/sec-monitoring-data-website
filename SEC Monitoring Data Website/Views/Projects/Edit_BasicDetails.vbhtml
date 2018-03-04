@ModelType SEC_Monitoring_Data_Website.EditProjectViewModel

@Html.HiddenFor(Function(model) model.Project.Id)

<table class="edit-table">
    <tr>
        <th>
            @Html.LabelFor(Function(model) model.Project.FullName)
        </th>
        <td>
            @Html.EditorFor(Function(model) model.Project.FullName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.Project.FullName)
        </td>
    </tr>
    <tr>
        <th>
            @Html.LabelFor(Function(model) model.Project.ShortName)
        </th>
        <td>
            @Html.EditorFor(Function(model) model.Project.ShortName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.Project.ShortName)
        </td>
    </tr>
    <tr>
        <th>
            @Html.LabelFor(Function(model) model.Project.ProjectNumber)
        </th>
        <td>
            @Html.EditorFor(Function(model) model.Project.ProjectNumber)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.Project.ProjectNumber)
        </td>
    </tr>
    <tr>
        <th>
            @Html.LabelFor(Function(model) model.Project.ProjectGeoCoords)
        </th>
        <td>
            @Html.EditorFor(Function(model) model.Project.ProjectGeoCoords)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.Project.ProjectGeoCoords)
        </td>
    </tr>
    @Html.HiddenFor(Function(model) model.Project.MapLink)
    @*<tr>
        <th>
            @Html.LabelFor(Function(model) model.Project.MapLink)
        </th>
        <td>
            @Html.EditorFor(Function(model) model.Project.MapLink)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.Project.MapLink)
        </td>
    </tr>*@
    <tr>
        <th>
            @Html.LabelFor(Function(model) model.Project.ClientOrganisation)
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.ClientOrganisationId, Model.ClientOrganisationList, String.Empty)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.ClientOrganisationId)
        </td>
    </tr>
    <tr>
        <th>
            @Html.LabelFor(Function(model) model.Project.Country)
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.CountryId, Model.CountryList, String.Empty)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CountryId)
        </td>
    </tr>
</table>
