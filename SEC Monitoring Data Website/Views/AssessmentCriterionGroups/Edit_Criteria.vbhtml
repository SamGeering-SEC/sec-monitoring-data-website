@ModelType SEC_Monitoring_Data_Website.EditAssessmentCriterionGroupViewModel

@code
    Dim monitorLocations = Model.AssessmentCriterionGroup.Project.MonitorLocations.Where(
        Function(m) m.MeasurementTypeId = Model.AssessmentCriterionGroup.MeasurementTypeId
            ).OrderBy(Function(l) l.MonitorLocationName).ToList
End Code

@Using Ajax.BeginRouteForm("AssessmentCriterionGroupGetMonitorLocationCriteriaForEditRoute",
                          Nothing,
                          New AjaxOptions With {.UpdateTargetId = "monitor_location_criteria",
                                                .InsertionMode = InsertionMode.Replace,
                                                .HttpMethod = "POST",
                                                .LoadingElementId = "divLoadingElement",
                                                .OnBegin = "emptyCriteria",
                                                .OnSuccess = "renderHelpers"},
                          New With {.id = "getMonitorLocationCriteriaForm"})
    

    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @Html.HiddenFor(Function(model) model.AssessmentCriterionGroupId)
    
    @<table class="edit-table">
        <tr>
            <th>
                Monitor Location:
            </th>
            <td>
                @Html.DropDownListFor(Function(model) model.MonitorLocationId,
                                      Model.MonitorLocationList,
                                      "Please select a Monitor Location...",
                                      New With {.onchange = "submitMonitorLocationForm();"})
            </td>
        </tr>
    </table>

End Using


<div id="monitor_location_criteria">

</div>

<div>
    @Using (Html.JQueryUI().Begin(New Dialog().Title("Delete Criterion").AutoOpen(False).Modal(True).ConfirmAjax(
                                  ".DeleteCriterionLink",
                                  "Yes",
                                  "No",
                                  New AjaxSettings With {.Method = HttpVerbs.Post,
                                                         .Success = "ajaxOpSuccess",
                                                         .BeforeSend = "displayLoadingDiv",
                                                         .Error = "ajaxOpError"})))
        @<p>
            Would you like to delete this Criterion?
        </p>
    End Using
</div>

