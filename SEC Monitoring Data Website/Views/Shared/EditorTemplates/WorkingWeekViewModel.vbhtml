@Modeltype SEC_Monitoring_Data_Website.WorkingWeekViewModel

@Code
    Dim name As String = ViewData.TemplateInfo.HtmlFieldPrefix
    Dim id As String = name.Replace(".", "_") + "_"
End Code

@*Commented out because was causing issue on Azure*@
@*<script type="text/javascript">

    $(document).ready(function () {
        //Set initial state
        $('#@Html.Raw(id+"StartTimeMondays")').prop("disabled", !$("#chkWorkOnMondays").prop("checked")); $('#@Html.Raw(id + "EndTimeMondays")').prop("disabled", !$("#chkWorkOnMondays").prop("checked"));
        $('#@Html.Raw(id+"StartTimeTuesdays")').prop("disabled", !$("#chkWorkOnTuesdays").prop("checked")); $('#@Html.Raw(id + "EndTimeTuesdays")').prop("disabled", !$("#chkWorkOnTuesdays").prop("checked"));
        $('#@Html.Raw(id+"StartTimeWednesdays")').prop("disabled", !$("#chkWorkOnWednesdays").prop("checked")); $('#@Html.Raw(id + "EndTimeWednesdays")').prop("disabled", !$("#chkWorkOnWednesdays").prop("checked"));
        $('#@Html.Raw(id+"StartTimeThursdays")').prop("disabled", !$("#chkWorkOnThursdays").prop("checked")); $('#@Html.Raw(id + "EndTimeThursdays")').prop("disabled", !$("#chkWorkOnThursdays").prop("checked"));
        $('#@Html.Raw(id+"StartTimeFridays")').prop("disabled", !$("#chkWorkOnFridays").prop("checked")); $('#@Html.Raw(id + "EndTimeFridays")').prop("disabled", !$("#chkWorkOnFridays").prop("checked"));
        $('#@Html.Raw(id+"StartTimeSaturdays")').prop("disabled", !$("#chkWorkOnSaturdays").prop("checked")); $('#@Html.Raw(id + "EndTimeSaturdays")').prop("disabled", !$("#chkWorkOnSaturdays").prop("checked"));
        $('#@Html.Raw(id+"StartTimeSundays")').prop("disabled", !$("#chkWorkOnSundays").prop("checked")); $('#@Html.Raw(id + "EndTimeSundays")').prop("disabled", !$("#chkWorkOnSundays").prop("checked"));

        //Add event handlers
        $("#chkWorkOnMondays").click(function () { $('#@Html.Raw(id+"StartTimeMondays")').prop("disabled", !this.checked); $('#@Html.Raw(id + "EndTimeMondays")').prop("disabled", !this.checked); });
        $("#chkWorkOnTuesdays").click(function () { $('#@Html.Raw(id+"StartTimeTuesdays")').prop("disabled", !this.checked); $('#@Html.Raw(id + "EndTimeTuesdays")').prop("disabled", !this.checked); });
        $("#chkWorkOnWednesdays").click(function () { $('#@Html.Raw(id+"StartTimeWednesdays")').prop("disabled", !this.checked); $('#@Html.Raw(id + "EndTimeWednesdays")').prop("disabled", !this.checked); });
        $("#chkWorkOnThursdays").click(function () { $('#@Html.Raw(id+"StartTimeThursdays")').prop("disabled", !this.checked); $('#@Html.Raw(id + "EndTimeThursdays")').prop("disabled", !this.checked); });
        $("#chkWorkOnFridays").click(function () { $('#@Html.Raw(id+"StartTimeFridays")').prop("disabled", !this.checked); $('#@Html.Raw(id + "EndTimeFridays")').prop("disabled", !this.checked); });
        $("#chkWorkOnSaturdays").click(function () { $('#@Html.Raw(id+"StartTimeSaturdays")').prop("disabled", !this.checked); $('#@Html.Raw(id + "EndTimeSaturdays")').prop("disabled", !this.checked); });
        $("#chkWorkOnSundays").click(function () { $('#@Html.Raw(id+"StartTimeSundays")').prop("disabled", !this.checked); $('#@Html.Raw(id + "EndTimeSundays")').prop("disabled", !this.checked); });

    });

</script>*@

<table class="create-table">
    <tr>
        <th>
            <h5>Day of Week</h5>
        </th>
        <th>
            <h5>Working Day?</h5>
        </th>
        <th>
            <h5>Start Time</h5>
        </th>
        <th>
            <h5>End Time</h5>
        </th>
    </tr>
    <tr>
        <td>
            Monday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkOnMondays, New With {.id = "chkWorkOnMondays", .checked = Model.WorkOnMondays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.StartTimeMondays, New With {.disabled = Not (Model.WorkOnMondays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.EndTimeMondays, New With {.disabled = Not (Model.WorkOnMondays)})
        </td>
    </tr>
    <tr>
        <td>
            Tuesday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkOnTuesdays, New With {.id = "chkWorkOnTuesdays", .checked = Model.WorkOnTuesdays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.StartTimeTuesdays, New With {.disabled = Not (Model.WorkOnTuesdays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.EndTimeTuesdays, New With {.disabled = Not (Model.WorkOnTuesdays)})
        </td>
    </tr>
    <tr>
        <td>
            Wednesday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkOnWednesdays, New With {.id = "chkWorkOnWednesdays", .checked = Model.WorkOnWednesdays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.StartTimeWednesdays, New With {.disabled = Not (Model.WorkOnWednesdays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.EndTimeWednesdays, New With {.disabled = Not (Model.WorkOnWednesdays)})
        </td>
    </tr>
    <tr>
        <td>
            Thursday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkOnThursdays, New With {.id = "chkWorkOnThursdays", .checked = Model.WorkOnThursdays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.StartTimeThursdays, New With {.disabled = Not (Model.WorkOnThursdays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.EndTimeThursdays, New With {.disabled = Not (Model.WorkOnThursdays)})
        </td>
    </tr>
    <tr>
        <td>
            Friday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkOnFridays, New With {.id = "chkWorkOnFridays", .checked = Model.WorkOnFridays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.StartTimeFridays, New With {.disabled = Not (Model.WorkOnFridays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.EndTimeFridays, New With {.disabled = Not (Model.WorkOnFridays)})
        </td>
    </tr>
    <tr>
        <td>
            Saturday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkOnSaturdays, New With {.id = "chkWorkOnSaturdays", .checked = Model.WorkOnSaturdays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.StartTimeSaturdays, New With {.disabled = Not (Model.WorkOnSaturdays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.EndTimeSaturdays, New With {.disabled = Not (Model.WorkOnSaturdays)})
        </td>
    </tr>
    <tr>
        <td>
            Sunday
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkOnSundays, New With {.id = "chkWorkOnSundays", .checked = Model.WorkOnSundays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.StartTimeSundays, New With {.disabled = Not (Model.WorkOnSundays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.EndTimeSundays, New With {.disabled = Not (Model.WorkOnSundays)})
        </td>
    </tr>
    <tr>
        <td>
            Public Holidays
        </td>
        <td style="text-align:center">
            @Html.CheckBoxFor(Function(model) model.WorkOnPublicHolidays, New With {.id = "chkWorkOnPublicHolidays", .checked = Model.WorkOnPublicHolidays})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.StartTimePublicHolidays, New With {.disabled = Not (Model.WorkOnPublicHolidays)})
        </td>
        <td>
            @Html.EditorFor(Function(model) model.EndTimePublicHolidays, New With {.disabled = Not (Model.WorkOnPublicHolidays)})
        </td>
    </tr>

</table>
