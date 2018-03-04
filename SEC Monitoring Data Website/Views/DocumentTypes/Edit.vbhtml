@ModelType SEC_Monitoring_Data_Website.EditDocumentTypeViewModel

@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>DocumentType</legend>

        @Html.HiddenFor(Function(model) model.DocumentType.Id)

        @Using t = Html.JQueryUI().BeginTabs()

                t.Tab("Basic Details", "basic_details")
                t.Tab("User Access Levels", "user_access_levels")
                't.Tab("Parent Document Types", "parent_document_types")
                't.Tab("Child Document Types", "child_document_types")


                ' Basic Details Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_BasicDetails", Model)
            End Using

            ' User Access Levels Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_UserAccessLevels", Model)
            End Using

            @*' Parent Document Types Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_ParentDocumentTypes", Model)
            End Using

            ' Child Document Types Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_ChildDocumentTypes", Model)
            End Using*@

        End Using
    
        <p>
            @Html.JQueryUI.Button("Save")
        </p>

    </fieldset>
End Using


@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
