@ModelType SEC_Monitoring_Data_Website.CreateAssessmentCriterionPopUpViewModel

@Using t = Html.JQueryUI().BeginTabs()

    t.Tab("Series", "series")
    t.Tab("Table", "table")


    ' Series Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_Criteria_CreateCriterion_AssessedLevel_Series", Model)
    End Using

    ' Table Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_Criteria_CreateCriterion_AssessedLevel_Table", Model)
    End Using

End Using