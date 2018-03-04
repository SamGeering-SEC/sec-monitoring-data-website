@ModelType SEC_Monitoring_Data_Website.EditDocumentViewModel

@Code
    ViewData("Title") = "Edit Document"
End Code

<h2>Edit Document</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)


    @Using t = Html.JQueryUI().BeginTabs()

            t.Tab("Basic Details", "basic_details")
            t.Tab("Projects", "projects")
            t.Tab("Monitors", "monitors")
            t.Tab("Monitor Locations", "monitor_locations")
            t.Tab("Excluded Contacts", "excluded_contacts")


            ' Basic Details Tab
        @Using t.BeginPanel
            @Html.Partial("Edit_BasicDetails", Model)
        End Using

        ' Projects Tab
        @Using t.BeginPanel
            @Html.Partial("Edit_Projects", Model)
        End Using

        ' Monitors Tab
        @Using t.BeginPanel
            @Html.Partial("Edit_Monitors", Model)
        End Using

        ' Monitor Locations Tab
        @Using t.BeginPanel
            @Html.Partial("Edit_MonitorLocations", Model)
        End Using

        ' Excluded Contacts Tab
        @Using t.BeginPanel
            @Html.Partial("Edit_ExcludedContacts", Model)
        End Using

    End Using

    @<p>
        @Html.JQueryUI.Button("Save")
    </p>

End Using

