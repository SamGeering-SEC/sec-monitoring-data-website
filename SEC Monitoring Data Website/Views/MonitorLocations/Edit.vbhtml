@ModelType SEC_Monitoring_Data_Website.EditMonitorLocationViewModel

@Code
    ViewData("Title") = "Edit Monitor Location"
End Code

<h2>Edit Monitor Location</h2>

@Using Html.BeginForm()

    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>MonitorLocation</legend>

        @Html.HiddenFor(Function(model) model.MonitorLocation.Id)
        @Html.HiddenFor(Function(model) model.Project.Id)
        @Html.HiddenFor(Function(model) model.MonitorLocation.MonitorLocationGeoCoordsId)

        @Using t = Html.JQueryUI().BeginTabs()

                t.Tab("Basic Details", "basic_details")
                t.Tab("Photos", "photos")
                t.Tab("Assessment Criteria", "assessment_criteria")
                t.Tab("Deployment Records", "deployment_records")


                ' Basic Details Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_BasicDetails", Model)
            End Using

            ' Photos Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_Photos", Model)
            End Using

            ' Assessment Criteria Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_AssessmentCriteria", Model)
            End Using

            ' Deployment Records Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_DeploymentRecords", Model)
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
