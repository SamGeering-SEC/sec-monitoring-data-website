@Using t = Html.JQueryUI().BeginTabs()

    t.Tab("Chart Series", "chart_series")
    t.Tab("Table Column", "table_column")


    ' Chart Series Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_AssessedLevels_ChartSeries", Model)
    End Using

    ' Table Column Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_AssessedLevels_TableColumn", Model)
    End Using

End Using
