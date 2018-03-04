@ModelType SEC_Monitoring_Data_Website.AssessmentCriterionGroupDetailsViewModel

@Using t = Html.JQueryUI().BeginTabs()

    t.Tab("Assessment", "assessment")
    t.Tab("Graph", "graph")
    t.Tab("Table", "table")


    ' Assessment Tab
    @Using t.BeginPanel
        @Html.Partial("Details_BasicDetails_Assessment", Model)
    End Using

    ' Graph Tab
    @Using t.BeginPanel
        @Html.Partial("Details_BasicDetails_Graph", Model)
    End Using

    ' Table Tab
    @Using t.BeginPanel
        @Html.Partial("Details_BasicDetails_Table", Model)
    End Using

End Using

