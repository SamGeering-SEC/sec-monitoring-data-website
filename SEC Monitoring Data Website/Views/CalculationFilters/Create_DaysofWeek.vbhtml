@ModelType SEC_Monitoring_Data_Website.CreateCalculationFilterViewModel

<table class="create-table">
    <tr>
        <th>
            Applies on Mondays?
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AppliesOnMondays)
        </td>
    </tr>
    <tr>
        <th>
            Applies on Tuesdays?
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AppliesOnTuesdays)
        </td>
    </tr>
    <tr>
        <th>
            Applies on Wednesdays?
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AppliesOnWednesdays)
        </td>
    </tr>
    <tr>
        <th>
            Applies on Thursdays?
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AppliesOnThursdays)
        </td>
    </tr>
    <tr>
        <th>
            Applies on Fridays?
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AppliesOnFridays)
        </td>
    </tr>
    <tr>
        <th>
            Applies on Saturdays?
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AppliesOnSaturdays)
        </td>
    </tr>
    <tr>
        <th>
            Applies on Sundays?
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AppliesOnSundays)
        </td>
    </tr>
    <tr>
        <th>
            Applies on Public Holidays?
        </th>
        <td>
            @Html.EditorFor(Function(model) model.AppliesOnPublicHolidays)
        </td>
    </tr>
</table>