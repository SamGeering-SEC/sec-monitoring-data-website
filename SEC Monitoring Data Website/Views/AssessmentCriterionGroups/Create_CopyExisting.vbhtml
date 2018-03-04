@ModelType SEC_Monitoring_Data_Website.CreateFromExistingCriterionGroupViewModel

@Using Html.BeginRouteForm("AssessmentCriterionGroupCreateFromExistingRoute")

    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @Html.HiddenFor(Function(model) model.CopyToProjectId)
    
    @<table class="create-table">
        <tr>
            <th>
                Project
            </th>
            <td>
                @Html.DropDownListFor(Function(model) model.CreateFromExistingProjectId,
                                      Model.CreateFromExistingProjectList,
                                      "Please select a Project...")
            </td>
            <td>
                @Html.ValidationMessageFor(Function(model) model.CreateFromExistingProjectId)
            </td>
        </tr>
        <tr>
            <th>
                Assessment Criterion Group
            </th>
            <td>
                @Html.DropDownListFor(Function(model) model.CreateFromExistingAssessmentCriterionGroupId, Model.CreateFromExistingAssessmentCriterionGroupList, "Please select an Assessment Group...")
            </td>
            <td>
                @Html.ValidationMessageFor(Function(model) model.CreateFromExistingAssessmentCriterionGroupId)
            </td>
        </tr>
        <tr>
            <th>
                Monitor Location
            </th>
            <td>
                @Html.DropDownListFor(Function(model) model.CreateFromExistingMonitorLocationId, Model.CreateFromExistingMonitorLocationList, "Please select a Monitor Location to Copy From...")
            </td>
            <td>
                @Html.ValidationMessageFor(Function(model) model.CreateFromExistingMonitorLocationId)
            </td>
        </tr>

    </table>

    @<p>
        @Html.JQueryUI.Button("Create")
    </p>

        End Using