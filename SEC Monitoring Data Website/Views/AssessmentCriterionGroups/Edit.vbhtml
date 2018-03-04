@ModelType SEC_Monitoring_Data_Website.EditAssessmentCriterionGroupViewModel

@Code
    ViewData("Title") = "Edit Assessment Criterion Group"
End Code

<h2>Edit Assessment Criterion Group</h2>


@Using t = Html.JQueryUI().BeginTabs()

    t.Tab("Basic Details", "basic_details")
    t.Tab("Criteria", "criteria")


    ' Basic Details Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_BasicDetails", Model)

    End Using

    ' Criteria Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_Criteria", Model)
    End Using

End Using

@Html.Partial("NavigationButtons", Model.NavigationButtons)


@section Scripts

    <script type="text/javascript">

        function ajaxOpSuccess(data, textStatus, jqXHR) {
            $("#divLoadingElement").css("display", "none");
            $('#monitor_location_criteria').html(data);
            renderHelpers();
        }

        function ajaxOpError(jqXHR, textStatus, errorThrown) {
            alert(textStatus);
        }

        function renderHelpers() {
            $('#monitor_location_criteria').jQueryUIHelpers();
            $('.EditCriterionDialog').dialog('close');
            $('.CopyCriteriaDialog').dialog('close');
        }
        function emptyCriteria() {
            $('#monitor_location_criteria').empty();
        }
        function displayLoadingDiv() {
            emptyCriteria();
            $("#divLoadingElement").css("display", "inline");
        }
        function submitMonitorLocationForm() {
            $('#monitor_location_criteria').jQueryUIHelpers();
            $('.EditCriterionDialog').dialog('close');
            $('.CopyCriteriaDialog').dialog('close');
            $('#getMonitorLocationCriteriaForm').submit();
        }
        function editCommited() {
            submitMonitorLocationForm();
        }

    </script>
End Section

