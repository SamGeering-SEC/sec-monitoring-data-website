@ModelType SEC_Monitoring_Data_Website.CreateNewAssessmentCriterionGroupViewModel

@Using Html.BeginRouteForm("AssessmentCriterionGroupCreateNewRoute")

    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @Using t = Html.JQueryUI().BeginTabs()

            t.Tab("Assessment", "assessment")
            t.Tab("Graph", "graph")
            t.Tab("Table", "table")


            ' Assessment Tab
    @Using t.BeginPanel
        @Html.Partial("Create_CreateNew_Assessment", Model)
    End Using

        ' Graph Tab
    @Using t.BeginPanel
        @Html.Partial("Create_CreateNew_Graph", Model)
    End Using

        ' Table Tab
    @Using t.BeginPanel
        @Html.Partial("Create_CreateNew_Table", Model)
    End Using

    End Using


        @<p>
            @Html.JQueryUI.Button("Create")
        </p>

            End Using