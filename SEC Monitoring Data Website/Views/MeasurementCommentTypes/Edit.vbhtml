@ModelType SEC_Monitoring_Data_Website.EditMeasurementCommentTypeViewModel

@Code
    ViewData("Title") = "Edit Measurement Comment Type"
End Code

<h2>Edit Measurement Comment Type</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>MeasurementCommentType</legend>

        @Html.HiddenFor(Function(model) model.MeasurementCommentType.Id)

        @Using t = Html.JQueryUI().BeginTabs()

                t.Tab("Basic Details", "basic_details")
                t.Tab("Excluded Views", "excluded_views")
                t.Tab("Excluded Criteria", "excluded_criteria")


                ' Basic Details Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_BasicDetails", Model)
            End Using

            ' Excluded Views Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_ExcludedViews", Model)
            End Using

            ' Excluded Criteria Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_ExcludedAssessmentCriteria", Model)
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
