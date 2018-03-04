@ModelType SEC_Monitoring_Data_Website.AssessmentCriterionGroupDetailsViewModel

@Code
    ViewData("Title") = "Assessment Criterion Group Details"
End Code

<h2>Assessment Criterion Group Details</h2>

<fieldset>
    <legend>AssessmentCriterionGroup</legend>

    @Using t = Html.JQueryUI().BeginTabs()

        t.Tab("Basic Details", "basic_details")
        @If Model.AssessmentCriterionGroup.AssessmentCriteria.Count > 0 Then
            t.Tab("Criteria", "criteria")
        End If
        

        ' Basic Details Tab
        @Using t.BeginPanel
            @Html.Partial("Details_BasicDetails", Model)
        End Using

        @If Model.AssessmentCriterionGroup.AssessmentCriteria.Count > 0 Then
            ' Criteria Tab
            @Using t.BeginPanel
                @Html.Partial("Details_Criteria", Model)
            End Using
        End If


    End Using

</fieldset>

@Html.Partial("NavigationButtons", Model.NavigationButtons)


@section Scripts

    <script type="text/javascript">

        function submitMonitorLocationForm() {
            $('#getMonitorLocationCriteriaForm').submit();
        }
        function emptyCriteria() {
            $('#monitor_location_criteria').empty();
        }

    </script>
End Section