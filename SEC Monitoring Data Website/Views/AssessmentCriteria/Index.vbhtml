@ModelType MonitorLocationCriteriaDetailsViewModel

@Code
    ViewData("Title") = "Monitor Location Assessment Criteria Details"
End Code

<h2>Monitor Location Assessment Criteria Details</h2>

@Using t = Html.JQueryUI().BeginTabs()

    t.Tab("Basic Details", "basic_details")
    t.Tab("Assessment Criteria", "assessment_criteria")

    ' Basic Details Tab
    @Using t.BeginPanel
        @Html.Partial("Index_BasicDetails", Model)
    End Using

    ' Assessment Criteria Tab
    @Using t.BeginPanel
        @Html.Partial("Index_AssessmentCriteria", Model)
    End Using

End Using

@Html.Partial("NavigationButtons", Model.NavigationButtons)
