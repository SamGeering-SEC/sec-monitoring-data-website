@ModelType SEC_Monitoring_Data_Website.MeasurementCommentType

@code
    Dim showExcludedCriteriaLinks = DirectCast(ViewData("ShowExcludedCriteriaLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Project
        </th>
        <th>
            Assessment Group Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each ac In Model.ExcludedAssessmentCriterionGroups.OrderBy(Function(acg) acg.GroupName).OrderBy(Function(acg) acg.Project.FullName)
        @<tr>
            <td>
                @If showExcludedCriteriaLinks Then
                    @Html.RouteLink(
                        ac.Project.FullName,
                        "ProjectDetailsRoute",
                        New With {.ProjectRouteName = ac.Project.getRouteName}
                    )
                Else
                    @ac.Project.FullName
                End If
            </td>
            <td>
                @If showExcludedCriteriaLinks Then
                    @Html.RouteLink(
                        ac.GroupName,
                        "AssessmentCriterionGroupDetailsRoute",
                        New With {.ProjectRouteName = ac.Project.getRouteName,
                                  .AssessmentCriterionGroupRouteName = ac.getRouteName}
                    )
                Else
                    @ac.GroupName
                End If
            </td>
        </tr>
    Next

</table>