@ModelType SEC_Monitoring_Data_Website.AssessmentCriterionGroupDetailsViewModel


@Using Ajax.BeginRouteForm("AssessmentCriterionGroupGetMonitorLocationCriteriaForDetailsRoute",
                          Nothing,
                          New AjaxOptions With {.UpdateTargetId = "monitor_location_criteria",
                                                .InsertionMode = InsertionMode.Replace,
                                                .HttpMethod = "GET",
                                                .OnBegin = "emptyCriteria",
                                                .LoadingElementId = "divLoadingElement"},
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

<div id="divLoadingElement" style="display:none">
    <img src="~/Images/loading.gif" />
</div>
<div id="monitor_location_criteria">

</div>

