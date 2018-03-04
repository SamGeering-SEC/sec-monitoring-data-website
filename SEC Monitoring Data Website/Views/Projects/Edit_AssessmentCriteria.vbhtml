@ModelType SEC_Monitoring_Data_Website.EditProjectViewModel

@Code
    Dim showCreateAssessmentCriterionGroupLink = DirectCast(ViewData("ShowCreateAssessmentCriterionGroupLink"), Boolean)
    Dim showEditAssessmentCriterionGroupLinks = DirectCast(ViewData("ShowEditAssessmentCriterionGroupLinks"), Boolean)
    Dim showDeleteAssessmentCriterionGroupLinks = DirectCast(ViewData("ShowDeleteAssessmentCriterionGroupLinks"), Boolean)
End Code

<table class="edit-table">

    @*Header Row*@
    <tr>
        <th>
            Group Name
        </th>
        <th>
            Description
        </th>
        <th></th>
        <th></th>
    </tr>

    @*Item Rows*@
    @For Each ac In Model.Project.AssessmentCriteria.OrderBy(Function(c) c.GroupName)
        @<tr>
            <td>
                @ac.GroupName
            </td>
            <td>
                @ac.getDescription
            </td>
            <td>
                @If showEditAssessmentCriterionGroupLinks Then
                    @Html.RouteLink("Edit Group",
                                "AssessmentCriterionGroupEditRoute",
                                New With {.ProjectRouteName = Model.Project.getRouteName, .AssessmentCriterionGroupRouteName = ac.getRouteName},
                                New With {.class = "sitewide-button-16 edit-button-16"})
                End If
            </td>
            <td>
                @If showDeleteAssessmentCriterionGroupLinks Then
                    @Html.RouteLink("Delete Group",
                                "AssessmentCriterionGroupDeleteFromProjectRoute",
                                New With {.ProjectId = Model.Project.Id, .AssessmentCriterionGroupId = ac.Id},
                                New With {.class = "sitewide-button-16 delete-button-16 DeleteAssessmentCriterionGroupLink"})
                End If
            </td>
        </tr>
    Next

    <tr>
        <td>
            @If showCreateAssessmentCriterionGroupLink Then
                @Html.RouteLink("Create new Assessment Group", "AssessmentCriterionGroupCreateRoute",
                     New With {.controller = "AssessmentCriterionGroups", .ProjectRouteName = Model.Project.getRouteName},
                     New With {.class = "sitewide-button-32 create-button-32"})
            End If
        </td>
    </tr>

</table>

