@ModelType MonitorLocationCriteriaDetailsViewModel

@Code
    Dim showProjectDetailsLink As Boolean = True
    Dim showMonitorLocationDetailsLink As Boolean = True
End Code

<table class="details-table">
    <tr>
        <th>
            Project
        </th>
        <td>
            @If showProjectDetailsLink Then
                @Html.RouteLink(Model.AssessmentCriterionGroup.Project.FullName,
                                "ProjectDetailsRoute",
                                New With {.ProjectRouteName = Model.AssessmentCriterionGroup.Project.getRouteName})
            Else
                @Model.AssessmentCriterionGroup.Project.FullName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Assessment Group
        </th>
        <td>
            @Html.RouteLink(
                        Model.AssessmentCriterionGroup.GroupName,
                        "AssessmentCriterionGroupDetailsRoute",
                        New With {.ProjectRouteName = Model.AssessmentCriterionGroup.Project.getRouteName,
                                  .AssessmentCriterionGroupRouteName = Model.AssessmentCriterionGroup.getRouteName}
                    )
        </td>
    </tr>
    <tr>
        <th>
            Monitor Location
        </th>
        <td>
            @If showMonitorLocationDetailsLink Then
                @Html.RouteLink(
                    Model.MonitorLocation.MonitorLocationName,
                    "MonitorLocationDetailsRoute",
                    New With {.ProjectRouteName = Model.MonitorLocation.Project.getRouteName,
                            .MonitorLocationRouteName = Model.MonitorLocation.getRouteName})
            Else
                @Model.MonitorLocation.MonitorLocationName
            End If
        </td>
    </tr>
</table>