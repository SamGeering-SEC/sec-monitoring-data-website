@ModelType SEC_Monitoring_Data_Website.EditStandardWeeklyWorkingHoursViewModel

@Code
    ViewData("Title") = "Edit Standard Working Hours"
End Code

<h2>Edit Standard Working Hours</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @Html.HiddenFor(Function(model) model.ProjectId)
    
@Using t = Html.JQueryUI().BeginTabs()

        t.Tab("Working Hours", "working_hours")
        't.Tab("Measurement Views", "measurement_views")


        ' Working Hours Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_WorkingHours", Model)
    End Using

    @*' Measurement Views Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_MeasurementViews", Model)
    End Using*@

End Using



        @Html.HiddenFor(Function(model) model.StandardWeeklyWorkingHours.Id)

        @<p>
            @Html.JQueryUI.Button("Save")
        </p>

End Using

