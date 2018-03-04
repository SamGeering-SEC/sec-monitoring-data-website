@ModelType MonitorLocationCriterionDetailsViewModel

@Using t = Html.JQueryUI().BeginTabs()

    t.Tab("Chart Series", "chart_series")
    t.Tab("Table Column", "table_column")


    ' Chart Series Tab
    @Using t.BeginPanel
        @Html.Partial("Details_CriterionLevels_ChartSeries", Model.AssessmentCriterion)
    End Using

    ' Table Column Tab
    @Using t.BeginPanel
        @Html.Partial("Details_CriterionLevels_TableColumn", Model.AssessmentCriterion)
    End Using

End Using
