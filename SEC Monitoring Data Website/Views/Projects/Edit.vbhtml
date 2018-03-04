@ModelType SEC_Monitoring_Data_Website.EditProjectViewModel

@Code
    ViewData("Title") = "Edit Project"
End Code

<script type="text/javascript">

    function deleteVariedWeeklyWorkingHoursSuccess(data, textStatus, jqXHR) {
        $('#VariationsTable').html(data);
    }

    function deleteVariedWeeklyWorkingHoursError(jqXHR, textStatus, errorThrown) {
        alert(textStatus);
    }
    function deleteAssessmentCriterionGroupSuccess(data, textStatus, jqXHR) {
        $('#AssessmentCriteriaTab').html(data);
    }

    function deleteAssessmentCriterionGroupError(jqXHR, textStatus, errorThrown) {
        alert(textStatus);
    }

</script>

<h2>Edit Project</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Project</legend>

        @Using t = Html.JQueryUI().Begin(New Tabs().OnLoad("addButtonAnimations"))

                t.Tab("Basic Details", "basic_details")
                t.AjaxTab("Organisations", Url.RouteUrl("ProjectEditOrganisationsRoute", New With {.ProjectRouteName = Model.Project.getRouteName}))
                t.AjaxTab("Contacts", Url.RouteUrl("ProjectEditContactsRoute", New With {.ProjectRouteName = Model.Project.getRouteName}))
                t.Tab("Monitor Locations", "monitor_locations")
                t.AjaxTab("Measurement Views", Url.RouteUrl("ProjectEditMeasurementViewsRoute", New With {.ProjectRouteName = Model.Project.getRouteName}))
                t.Tab("Assessment Criteria", "assessment_criteria")
                t.AjaxTab("Working Hours", Url.RouteUrl("ProjectEditWorkingHoursRoute", New With {.ProjectRouteName = Model.Project.getRouteName}))

                ' Basic Details Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_BasicDetails", Model)
            End Using

            ' Monitor Locations Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_MonitorLocations", Model)
            End Using

            ' Assessment Criteria Tab
            @Using t.BeginPanel
                @<div id="AssessmentCriteriaTab">
                    @Html.Partial("Edit_AssessmentCriteria", Model)
                </div>
            End Using

        End Using

    </fieldset>

    @<p>
        @Html.JQueryUI.Button("Save")
    </p>

End Using

@*Delete Varation Dialog*@
@Using (Html.JQueryUI().Begin(New Dialog().Title("Delete Variation").AutoOpen(False).Modal(True).ConfirmAjax(".DeleteVariedWeeklyWorkingHoursLink", "Yes", "No", New AjaxSettings With {.Method = HttpVerbs.Post,
.Success = "deleteVariedWeeklyWorkingHoursSuccess",
.Error = "deleteVariedWeeklyWorkingHoursError"})))
    @<p>
        Would you like to delete this Variation?
    </p>
End Using

@*Delete Assessment Criterion Group Dialog*@
@Using (Html.JQueryUI().Begin(New Dialog().Title("Delete Group").AutoOpen(False).Modal(True).ConfirmAjax(".DeleteAssessmentCriterionGroupLink", "Yes", "No", New AjaxSettings With {.Method = HttpVerbs.Post,
.Success = "deleteAssessmentCriterionGroupSuccess",
.Error = "deleteAssessmentCriterionGroupError"})))
    @<p>
        Would you like to delete this Assessment Criterion Group from the Project?
    </p>
End Using