@ModelType SEC_Monitoring_Data_Website.EditContactViewModel

@Code
    ViewData("Title") = "Edit Contact"
End Code

<h2>Edit Contact</h2>

@Using Html.BeginForm()

    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Contact</legend>

        @Html.HiddenFor(Function(model) model.Contact.Id)

        @Using t = Html.JQueryUI().BeginTabs()

                t.Tab("Basic Details", "basic_details")
                t.Tab("Projects", "projects")
                t.Tab("Excluded Documents", "excluded_documents")


                ' Basic Details Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_BasicDetails", Model)
            End Using

            ' Projects Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_Projects", Model)
            End Using

            ' Excluded Documents Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_ExcludedDocuments", Model)
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
