@ModelType SEC_Monitoring_Data_Website.CreateVariedWeeklyWorkingHoursViewModel

@code
    Dim count As Integer = 0
End Code

<table class="create-table">
    @For Each mv In Model.AllMeasurementViews
        @<tr>
            <td>
                @Html.EditorFor(Function(model) model.AllMeasurementViews(count))
            </td>
        </tr>
        count = count + 1
    Next
</table>