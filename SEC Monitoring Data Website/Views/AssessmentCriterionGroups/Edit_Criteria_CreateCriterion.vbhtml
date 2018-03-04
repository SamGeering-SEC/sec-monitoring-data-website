@ModelType SEC_Monitoring_Data_Website.CreateAssessmentCriterionPopUpViewModel

<p>@Html.JQueryUI().Button("Add", New With {.id = "addCriterionButton"})</p>

@Using Html.JQueryUI().Begin(New Dialog().AutoOpen(False).Width(800).Height(600).Title("Add new Criterion").TriggerClick("#addCriterionButton").Modal(True))

    @Using Ajax.BeginRouteForm("AssessmentCriterionPopUpCreateRoute",
                               New AjaxOptions With {.UpdateTargetId = "monitor_location_criteria",
                                                     .InsertionMode = InsertionMode.Replace,
                                                     .HttpMethod = "POST",
                                                     .LoadingElementId = "divLoadingElement",
                                                     .OnBegin = "emptyCriteria",
                                                     .OnSuccess = "renderHelpers"})

        @Html.AntiForgeryToken()
        @Html.ValidationSummary(True)

        @Html.HiddenFor(Function(model) model.AssessmentCriterionGroupId)
        @Html.HiddenFor(Function(model) model.MonitorLocationId)


        @Using t = Html.JQueryUI().BeginTabs()

                t.Tab("Assessment", "assessment")
                t.Tab("Assessed Level", "assessed_level")
                t.Tab("Criterion Level", "criterion_level")


                ' Assessment Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_Criteria_CreateCriterion_Assessment", Model)
            End Using

            ' Assessed Level Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_Criteria_CreateCriterion_AssessedLevel", Model)
            End Using

            ' Criterion Level Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_Criteria_CreateCriterion_CriterionLevel", Model)
            End Using

        End Using

        @<p>
            @Html.JQueryUI.Button("Create Criterion")
        </p>


            End Using

        End Using