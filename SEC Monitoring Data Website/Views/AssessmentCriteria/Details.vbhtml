@ModelType MonitorLocationCriterionDetailsViewModel

@Code
    ViewData("Title") = "Monitor Location Assessment Criterion Details"
End Code

<h2>Monitor Location Assessment Criterion Details</h2>

@Using t = Html.JQueryUI().BeginTabs()

    t.Tab("Assessment", "assessment")
    t.Tab("Assessed Levels", "assessed_levels")
    t.Tab("Criterion Levels", "criterion_levels")


    ' Assessment Tab
    @Using t.BeginPanel
        @Html.Partial("Details_Assessment", Model)
    End Using

    ' Assessed Levels Tab
    @Using t.BeginPanel
        @Html.Partial("Details_AssessedLevels", Model)
    End Using

    ' Criterion Levels Tab
    @Using t.BeginPanel
        @Html.Partial("Details_CriterionLevels", Model)
    End Using

End Using

@Html.Partial("NavigationButtons", Model.NavigationButtons)
