@ModelType SEC_Monitoring_Data_Website.Project

@Code
    Dim showAssessmentCriterionGroupLinks = DirectCast(ViewData("ShowAssessmentCriterionGroupLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Group Name
        </th>
        <th>
            Measurement Type
        </th>
        <th>
            Description
        </th>
    </tr>

    @*Item Rows*@
    @For Each ac In Model.AssessmentCriteria.OrderBy(Function(c) c.GroupName)
        @<tr>
            <td>
                @If showAssessmentCriterionGroupLinks Then
                    @Html.RouteLink(ac.GroupName,
                                "AssessmentCriterionGroupDetailsRoute",
                                New With {.ProjectRouteName = Model.getRouteName,
                                          .AssessmentCriterionGroupRouteName = ac.getRouteName})
                Else
                    @ac.GroupName
                End If
            </td>
            <td>
                @ac.MeasurementType.MeasurementTypeName
            </td>
            <td>
                @ac.getDescription
            </td>
        </tr>
    Next

</table>