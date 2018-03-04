@ModelType SEC_Monitoring_Data_Website.VariedWeeklyWorkingHours

@Code
    ViewData("Title") = "Variation Details"
End Code

<h2>Variation Details</h2>

@Using t = Html.JQueryUI().BeginTabs()

    t.Tab("Basic Details", "basic_details")
    't.Tab("Measurement Views", "measurement_views")


    ' Basic Details Tab
    @Using t.BeginPanel
        @Html.Partial("Details_BasicDetails", Model)
    End Using

    @*' Measurement Views Tab
    @Using t.BeginPanel
        @Html.Partial("Details_MeasurementViews", Model)
    End Using*@

End Using




