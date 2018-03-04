@ModelType SEC_Monitoring_Data_Website.Country

@Html.HiddenFor(Function(model) model.Id)

<table class="edit-table">
    <tr>
        <th>
            Country Name
        </th>
    </tr>
    <tr>
        <td>
            @Html.EditorFor(Function(model) model.CountryName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CountryName)
        </td>
    </tr>
</table>

<p>
    @Html.JQueryUI.Button("Save")
</p>