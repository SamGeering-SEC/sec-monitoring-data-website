@ModelType SEC_Monitoring_Data_Website.EditStandardWeeklyWorkingHoursViewModel

<script type="text/javascript">

    $(document).ready(function () {

        //Set initial state
        $("#WorkingWeekViewModel_StartTimeMondays").prop("disabled", !$("#chkWorkOnMondays").prop("checked")); $("#WorkingWeekViewModel_EndTimeMondays").prop("disabled", !$("#chkWorkOnMondays").prop("checked"));
        $("#WorkingWeekViewModel_StartTimeTuesdays").prop("disabled", !$("#chkWorkOnTuesdays").prop("checked")); $("#WorkingWeekViewModel_EndTimeTuesdays").prop("disabled", !$("#chkWorkOnTuesdays").prop("checked"));
        $("#WorkingWeekViewModel_StartTimeWednesdays").prop("disabled", !$("#chkWorkOnWednesdays").prop("checked")); $("#WorkingWeekViewModel_EndTimeWednesdays").prop("disabled", !$("#chkWorkOnWednesdays").prop("checked"));
        $("#WorkingWeekViewModel_StartTimeThursdays").prop("disabled", !$("#chkWorkOnThursdays").prop("checked")); $("#WorkingWeekViewModel_EndTimeThursdays").prop("disabled", !$("#chkWorkOnThursdays").prop("checked"));
        $("#WorkingWeekViewModel_StartTimeFridays").prop("disabled", !$("#chkWorkOnFridays").prop("checked")); $("#WorkingWeekViewModel_EndTimeFridays").prop("disabled", !$("#chkWorkOnFridays").prop("checked"));
        $("#WorkingWeekViewModel_StartTimeSaturdays").prop("disabled", !$("#chkWorkOnSaturdays").prop("checked")); $("#WorkingWeekViewModel_EndTimeSaturdays").prop("disabled", !$("#chkWorkOnSaturdays").prop("checked"));
        $("#WorkingWeekViewModel_StartTimeSundays").prop("disabled", !$("#chkWorkOnSundays").prop("checked")); $("#WorkingWeekViewModel_EndTimeSundays").prop("disabled", !$("#chkWorkOnSundays").prop("checked"));
        $("#WorkingWeekViewModel_StartTimePublicHolidays").prop("disabled", !$("#chkWorkOnPublicHolidays").prop("checked")); $("#WorkingWeekViewModel_EndTimePublicHolidays").prop("disabled", !$("#chkWorkOnPublicHolidays").prop("checked"));
        //Add event handlers
        $("#chkWorkOnMondays").click(function () { $("#WorkingWeekViewModel_StartTimeMondays").prop("disabled", !this.checked); $("#WorkingWeekViewModel_EndTimeMondays").prop("disabled", !this.checked); });
        $("#chkWorkOnTuesdays").click(function () { $("#WorkingWeekViewModel_StartTimeTuesdays").prop("disabled", !this.checked); $("#WorkingWeekViewModel_EndTimeTuesdays").prop("disabled", !this.checked); });
        $("#chkWorkOnWednesdays").click(function () { $("#WorkingWeekViewModel_StartTimeWednesdays").prop("disabled", !this.checked); $("#WorkingWeekViewModel_EndTimeWednesdays").prop("disabled", !this.checked); });
        $("#chkWorkOnThursdays").click(function () { $("#WorkingWeekViewModel_StartTimeThursdays").prop("disabled", !this.checked); $("#WorkingWeekViewModel_EndTimeThursdays").prop("disabled", !this.checked); });
        $("#chkWorkOnFridays").click(function () { $("#WorkingWeekViewModel_StartTimeFridays").prop("disabled", !this.checked); $("#WorkingWeekViewModel_EndTimeFridays").prop("disabled", !this.checked); });
        $("#chkWorkOnSaturdays").click(function () { $("#WorkingWeekViewModel_StartTimeSaturdays").prop("disabled", !this.checked); $("#WorkingWeekViewModel_EndTimeSaturdays").prop("disabled", !this.checked); });
        $("#chkWorkOnSundays").click(function () { $("#WorkingWeekViewModel_StartTimeSundays").prop("disabled", !this.checked); $("#WorkingWeekViewModel_EndTimeSundays").prop("disabled", !this.checked); });
        $("#chkWorkOnPublicHolidays").click(function () { $("#WorkingWeekViewModel_StartTimePublicHolidays").prop("disabled", !this.checked); $("WorkingWeekViewModel_#EndTimePublicHolidays").prop("disabled", !this.checked); });

    });

</script>

<h3>Standard Working Hours</h3>

<table class="edit-table">
    <tr>
        <th>
            Day of Week
        </th>
        <th>
            Working Day?
        </th>
        <th>
            Start Time
        </th>
        <th>
            End Time
        </th>
    </tr>
    <tr>
        <td>
            Monday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkingWeekViewModel.WorkOnMondays, New With {.id = "chkWorkOnMondays", .checked = Model.WorkingWeekViewModel.WorkOnMondays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.StartTimeMondays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnMondays), .name = "test.this"})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.EndTimeMondays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnMondays)})
        </td>
    </tr>
    <tr>
        <td>
            Tuesday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkingWeekViewModel.WorkOnTuesdays, New With {.id = "chkWorkOnTuesdays", .checked = Model.WorkingWeekViewModel.WorkOnTuesdays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.StartTimeTuesdays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnTuesdays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.EndTimeTuesdays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnTuesdays)})
        </td>
    </tr>
    <tr>
        <td>
            Wednesday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkingWeekViewModel.WorkOnWednesdays, New With {.id = "chkWorkOnWednesdays", .checked = Model.WorkingWeekViewModel.WorkOnWednesdays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.StartTimeWednesdays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnWednesdays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.EndTimeWednesdays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnWednesdays)})
        </td>
    </tr>
    <tr>
        <td>
            Thursday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkingWeekViewModel.WorkOnThursdays, New With {.id = "chkWorkOnThursdays", .checked = Model.WorkingWeekViewModel.WorkOnThursdays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.StartTimeThursdays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnThursdays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.EndTimeThursdays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnThursdays)})
        </td>
    </tr>
    <tr>
        <td>
            Friday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkingWeekViewModel.WorkOnFridays, New With {.id = "chkWorkOnFridays", .checked = Model.WorkingWeekViewModel.WorkOnFridays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.StartTimeFridays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnFridays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.EndTimeFridays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnFridays)})
        </td>
    </tr>
    <tr>
        <td>
            Saturday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkingWeekViewModel.WorkOnSaturdays, New With {.id = "chkWorkOnSaturdays", .checked = Model.WorkingWeekViewModel.WorkOnSaturdays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.StartTimeSaturdays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnSaturdays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.EndTimeSaturdays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnSaturdays)})
        </td>
    </tr>
    <tr>
        <td>
            Sunday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkingWeekViewModel.WorkOnSundays, New With {.id = "chkWorkOnSundays", .checked = Model.WorkingWeekViewModel.WorkOnSundays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.StartTimeSundays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnSundays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.EndTimeSundays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnSundays)})
        </td>
    </tr>
    <tr>
        <td>
            Public Holidays
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkingWeekViewModel.WorkOnPublicHolidays, New With {.id = "chkWorkOnPublicHolidays", .checked = Model.WorkingWeekViewModel.WorkOnPublicHolidays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.StartTimePublicHolidays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnPublicHolidays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.WorkingWeekViewModel.EndTimePublicHolidays, New With {.disabled = Not (Model.WorkingWeekViewModel.WorkOnPublicHolidays)})
        </td>
    </tr>

</table>