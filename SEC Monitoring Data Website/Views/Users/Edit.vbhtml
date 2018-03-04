@ModelType SEC_Monitoring_Data_Website.EditUserViewModel

@Code
    ViewData("Title") = "Edit User"
End Code

<h2>Edit User</h2>

@Using Html.BeginForm()

    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>User</legend>

        @Html.HiddenFor(Function(model) model.User.Id)

        @Using t = Html.JQueryUI().BeginTabs()

                t.Tab("Basic Details", "basic_details")

                ' Basic Details Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_BasicDetails", Model)
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
