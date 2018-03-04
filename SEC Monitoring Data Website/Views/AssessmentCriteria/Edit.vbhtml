@Code
    ViewData("Title") = "Edit Monitor Location Assessment Criterion"
End Code

<h2>Edit Monitor Location Assessment Criterion</h2>

@Using t = Html.JQueryUI().BeginTabs()

    t.Tab("Assessment", "assessment")
    t.Tab("Assessed Levels", "assessed_levels")
    t.Tab("Criterion Levels", "criterion_levels")


    ' Assessment Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_Assessment", Model)
    End Using

    ' Assessed Levels Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_AssessedLevels", Model)
    End Using

    ' Criterion Levels Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_CriterionLevels", Model)
    End Using

End Using
