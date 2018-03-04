@ModelType SEC_Monitoring_Data_Website.Monitor

<table class="details-table">
    <tr>
        <th>
            Online?
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.CurrentStatus.IsOnline)
        </td>
    </tr>
    <tr>
        <th>
            Power Status Ok?
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.CurrentStatus.PowerStatusOk)
        </td>
    </tr>
    <tr>
        <th>
            Comment
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.CurrentStatus.StatusComment)
        </td>
    </tr>
</table>