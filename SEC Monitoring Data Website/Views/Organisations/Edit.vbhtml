@ModelType SEC_Monitoring_Data_Website.EditOrganisationViewModel

@Code
    ViewData("Title") = "Edit Organisation"
End Code

<h2>Edit Organisation</h2>

@Using Html.BeginForm()

    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Organisation</legend>

        @Using t = Html.JQueryUI().BeginTabs()

                t.Tab("Basic Details", "basic_details")
                t.Tab("Projects", "projects")

                ' Basic Details Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_BasicDetails", Model)
            End Using

            ' Projects Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_Projects", Model)
            End Using

        End Using

        <p>
            @Html.JQueryUI.Button("Save")
        </p>

    </fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
