@ModelType SEC_Monitoring_Data_Website.CreateAssessmentCriterionViewModel

@Code
    ViewData("Title") = "Create Monitor Location Assessment Criterion"
End Code

<h2>Create Monitor Location Assessment Criterion</h2>

@Using Html.BeginForm()
    
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @Html.HiddenFor(Function(model) model.AssessmentCriterionGroupId)
    @Html.HiddenFor(Function(model) model.MonitorLocationId)

    @Using t = Html.JQueryUI().BeginTabs()

            t.Tab("Assessment", "assessment")
            t.Tab("Assessed Levels", "assessed_levels")
            t.Tab("Criterion Levels", "criterion_levels")


            ' Assessment Tab
        @Using t.BeginPanel
            @Html.Partial("Create_Assessment", Model)
        End Using

        ' Assessed Levels Tab
        @Using t.BeginPanel
            @Html.Partial("Create_AssessedLevels", Model)
        End Using

        ' Criterion Levels Tab
        @Using t.BeginPanel
            @Html.Partial("Create_CriterionLevels", Model)
        End Using

    End Using

    @<p>
        @Html.JQueryUI.Button("Create")
    </p>

End Using