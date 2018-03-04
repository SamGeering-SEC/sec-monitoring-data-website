@ModelType SEC_Monitoring_Data_Website.EditMonitorViewModel

@Html.HiddenFor(Function(model) model.CurrentStatus.Id)

<table class="edit-table">
    <tr>
        <th>
            Is Online
        </th>
        <td>
            @Html.EditorFor(Function(model) model.CurrentStatus.IsOnline)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CurrentStatus.IsOnline)
        </td>
    </tr>
    <tr>
        <th>
            Power Status Ok
        </th>
        <td>
            @Html.EditorFor(Function(model) model.CurrentStatus.PowerStatusOk)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CurrentStatus.PowerStatusOk)
        </td>
    </tr>
    <tr>
        <th>
            Status Comment
        </th>
        <td>
            @Html.EditorFor(Function(model) model.CurrentStatus.StatusComment)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CurrentStatus.StatusComment)
        </td>
    </tr>
</table>