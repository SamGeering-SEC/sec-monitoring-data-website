@ModelType SEC_Monitoring_Data_Website.EditVariedWeeklyWorkingHoursViewModel

@Code
    ViewData("Title") = "Edit Variation"
End Code

<h2>Edit Variation</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)


        @Html.HiddenFor(Function(model) model.VariedWeeklyWorkingHours.Id)

         @Using t = Html.JQueryUI().BeginTabs()

                 t.Tab("Basic Details", "basic_details")
                 't.Tab("Measurement Views", "measurement_views")


                 ' Basic Details Tab
             @Using t.BeginPanel
                 @Html.Partial("Edit_BasicDetails", Model)
             End Using

             @*' Measurement Views Tab
             @Using t.BeginPanel
                 @Html.Partial("Edit_MeasurementViews", Model)
             End Using*@

         End Using

        @<p>
            @Html.JQueryUI.Button("Save")
        </p>

End Using


