@ModelType SEC_Monitoring_Data_Website.CreateAssessmentCriterionGroupViewModel

@Code
    ViewData("Title") = "Create Assessment Criterion Group"
End Code

<script type="text/javascript">

    function updateAssessmentCriterionGroups() {
        $("#CreateFromExistingAssessmentCriterionGroupId").empty();
        $.ajax({
            type: 'POST',
            url: '@Url.RouteUrl("AssessmentCriterionGroupGetProjectAssessmentCriterionGroupsRoute")',
            dataType: 'json',
            data: { ProjectId: $("#CreateFromExistingProjectId").val() },
            success: function (assessmentcriteriongroups) {
                $.each(assessmentcriteriongroups, function (i, acg) {
                    $("#CreateFromExistingAssessmentCriterionGroupId").append('<option value="' + acg.Value + '">' + acg.Text + '</option>');
                });
                updateMonitorLocations();
            }            
        });
        return false;
    }

    function updateMonitorLocations() {
        $("#CreateFromExistingMonitorLocationId").empty();
        $.ajax({
            type: 'POST',
            url: '@Url.RouteUrl("AssessmentCriterionGroupGetAssessmentCriterionGroupMonitorLocationsRoute")',
            dataType: 'json',
            data: { AssessmentCriterionGroupId: $("#CreateFromExistingAssessmentCriterionGroupId").val() },
            success: function (monitorlocations) {
                $.each(monitorlocations, function (i, ml) {
                    $("#CreateFromExistingMonitorLocationId").append('<option value="' + ml.Value + '">' + ml.Text + '</option>');
                });
            }
        });
        return false;
    }

    $(document).ready(function () {

        $("#CreateFromExistingProjectId").change(function () {
            updateAssessmentCriterionGroups();
        });

        $("#CreateFromExistingAssessmentCriterionGroupId").change(function () {
            updateMonitorLocations();
        });

    });

</script>

<h2>Create Assessment Criterion Group</h2>

@Html.ValidationMessage("error")

@Using t = Html.JQueryUI().BeginTabs()

    t.Tab("Create New", "create_new")
    @If Model.CreateFromExistingCriterionGroupViewModel.CreateFromExistingProjectList.Count > 0 Then
            t.Tab("Copy Existing", "copy_existing")
        End If



    ' Create New Tab
    @Using t.BeginPanel
        @Html.Partial("Create_CreateNew", Model.CreateNewAssessmentCriterionGroupViewModel)
    End Using

    ' Copy Existing Tab
    @If Model.CreateFromExistingCriterionGroupViewModel.CreateFromExistingProjectList.Count > 0 Then
        @Using t.BeginPanel
            @Html.Partial("Create_CopyExisting", Model.CreateFromExistingCriterionGroupViewModel)
        End Using
    End If

End Using




