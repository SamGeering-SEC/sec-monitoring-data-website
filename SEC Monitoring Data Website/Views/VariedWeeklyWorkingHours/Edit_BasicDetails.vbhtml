@ModelType SEC_Monitoring_Data_Website.EditVariedWeeklyWorkingHoursViewModel

<table class="edit-table">
    <tr>
        <th>
            Project
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.VariedWeeklyWorkingHours.Project.FullName)
        </td>
        <td>
            @Html.HiddenFor(Function(model) model.VariedWeeklyWorkingHours.ProjectId)
        </td>
    </tr>
    <tr>
        <th>
            Start Date
        </th>
        <td>
            @Html.JQueryUI.DatepickerFor(Function(model) model.VariedWeeklyWorkingHours.StartDate)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.VariedWeeklyWorkingHours.StartDate)
        </td>
    </tr>
    <tr>
        <th>
            End Date
        </th>
        <td>
            @Html.JQueryUI.DatepickerFor(Function(model) model.VariedWeeklyWorkingHours.EndDate)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.VariedWeeklyWorkingHours.EndDate)
        </td>
    </tr>
</table>

<h3> Working Hours</h3>
@Html.EditorFor(Function(model) model.WorkingWeekViewModel)