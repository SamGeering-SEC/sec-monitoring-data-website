@ModelType SEC_Monitoring_Data_Website.CreateVariedWeeklyWorkingHoursViewModel

@Code
    ViewData("Title") = "Create Variation"
End Code

<h2>Create Variation</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @Using t = Html.JQueryUI().BeginTabs()

            t.Tab("Basic Details", "basic_details")
            't.Tab("Measurement Views", "measurement_views")


            ' Basic Details Tab
    @Using t.BeginPanel
        @Html.Partial("Create_BasicDetails", Model)
    End Using

        @*' Measurement Views Tab
    @Using t.BeginPanel
        @Html.Partial("Create_MeasurementViews", Model)
    End Using*@

    End Using

    @<p>
        @Html.JQueryUI.Button("Create")
    </p>

End Using

