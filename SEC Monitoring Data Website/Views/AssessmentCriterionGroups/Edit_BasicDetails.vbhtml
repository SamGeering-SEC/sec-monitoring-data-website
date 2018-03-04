@ModelType SEC_Monitoring_Data_Website.EditAssessmentCriterionGroupViewModel

@Using Html.BeginForm()
@Html.AntiForgeryToken()
@Html.ValidationSummary(True)

@Html.HiddenFor(Function(model) model.AssessmentCriterionGroup.Id)

@Using t = Html.JQueryUI().BeginTabs()

        t.Tab("Assessment", "assessment")
        t.Tab("Graph", "graph")
        t.Tab("Table", "table")


        ' Assessment Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_BasicDetails_Assessment", Model)
    End Using

    ' Graph Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_BasicDetails_Graph", Model)
    End Using

    ' Table Tab
    @Using t.BeginPanel
        @Html.Partial("Edit_BasicDetails_Table", Model)
    End Using

End Using



@<p>
    @Html.JQueryUI.Button("Save Details")
</p>


    End Using