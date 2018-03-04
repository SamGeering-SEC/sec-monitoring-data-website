@ModelType SEC_Monitoring_Data_Website.EditAssessmentCriterionPopUpViewModel

@Using Html.JQueryUI().Begin(New Dialog(New With {.class = "EditCriterionDialog"}).AutoOpen(False).Width(800).Height(600).Title("Edit Criterion").TriggerClick("#editCriterionButton" + Model.EditAssessmentCriterionId.ToString).Modal(True))

    @Using Ajax.BeginRouteForm("AssessmentCriterionPopUpEditRoute",
                              New AjaxOptions With {.UpdateTargetId = "monitor_location_criteria",
                                                    .InsertionMode = InsertionMode.Replace,
                                                    .HttpMethod = "POST",
                                                    .LoadingElementId = "divLoadingElement",
                                                    .OnBegin = "emptyCriteria",
                                                    .OnSuccess = "editCommited"})

        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True)

        @Html.HiddenFor(Function(model) model.EditAssessmentCriterionId)
        @Html.HiddenFor(Function(model) model.AssessmentCriterionGroupId)
        @Html.HiddenFor(Function(model) model.MonitorLocationId)
        @Html.HiddenFor(Function(model) model.CriterionIndex)

        @Using t = Html.JQueryUI().BeginTabs()

                t.Tab("Assessment", "assessment")
                t.Tab("Assessed Level", "assessed_level")
                t.Tab("Criterion Level", "criterion_level")


                ' Assessment Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_Criteria_EditCriterion_Assessment", Model)
            End Using

            ' Assessed Level Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_Criteria_EditCriterion_AssessedLevel", Model)
            End Using

            ' Criterion Level Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_Criteria_EditCriterion_CriterionLevel", Model)
            End Using

        End Using

        @<p>
            @Html.JQueryUI.Button("Save Criterion")
        </p>


            End Using

        End Using