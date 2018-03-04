@ModelType SEC_Monitoring_Data_Website.MonitorLocation

@Code
    Dim showAssessmentCriterionGroupLinks = DirectCast(ViewData("ShowAssessmentCriterionGroupLinks"), Boolean)
End Code

<table class="details-table">
    <tr>
        <th>
            Assessment Group
        </th>
        <th>
            Description
        </th>
        <th style="text-align:center">
            Number of Criteria
        </th>
    </tr>
    @For Each acg In Model.getAssessmentCriterionGroups
        @<tr>
            <td>
                @If showAssessmentCriterionGroupLinks Then
                    @Html.RouteLink(
                        acg.GroupName,
                        "AssessmentCriterionGroupDetailsRoute",
                        New With {.ProjectRouteName = Model.Project.getRouteName,
                                    .AssessmentCriterionGroupRouteName = acg.getRouteName}
                    )
                Else
                    @acg.GroupName
                End If
            </td>
            <td>
                @acg.getDescription
            </td>
            <td style="text-align:center">
                @acg.AssessmentCriteria.Where(Function(ac) ac.MonitorLocationId = Model.Id).Count
            </td>
        </tr>
    Next

</table>