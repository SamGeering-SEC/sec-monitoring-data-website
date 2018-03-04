@ModelType IEnumerable(Of SEC_Monitoring_Data_Website.MonitorLocation)

@code
    Dim showMonitorLocationLinks = DirectCast(ViewData("ShowMonitorLocationLinks"), Boolean)
    Dim showMeasurementsLinks = DirectCast(ViewData("ShowMeasurementsLinks"), Boolean)
    Dim showAssessmentLinks = DirectCast(ViewData("ShowAssessmentLinks"), Boolean)
End Code

<table class="index-table">
    <tr>
        <th>
            Monitor Location Name
        </th>
        <th>
            Measurement Type
        </th>
        <th>
        </th>
        <th>
        </th>
    </tr>

    @For Each item In Model
        Dim currentItem = item
        @<tr>
            <td>
                @If showMonitorLocationLinks Then
                    @Html.RouteLink(currentItem.MonitorLocationName,
                                "MonitorLocationDetailsRoute",
                                New With {.ProjectRouteName = currentItem.Project.getRouteName,
                                          .MonitorLocationRouteName = currentItem.getRouteName})
                Else
                    @currentItem.MonitorLocationName
                End If
            </td>
            <td>
                @currentItem.MeasurementType.MeasurementTypeName
            </td>
            <td style="text-align:center">
                @If showMeasurementsLinks Then
                    @If currentItem.hasMeasurements = True Then
                        @Html.JQueryUI.ActionButton(
                            "View Measurements", "View", "Measurements",
                            New With {
                                .ProjectRouteName = currentItem.Project.getRouteName,
                                .MonitorLocationRouteName = currentItem.getRouteName,
                                .ViewRouteName = "Default",
                                .ViewDuration = "Week",
                                .strStartDate = Format(Date.Today, "dd-MMM-yyyy")
                            },
                            Nothing
                        )
                    End If
                End If
            </td>
            <td style="text-align:center">
                @If showAssessmentLinks AndAlso currentItem.hasMeasurements AndAlso currentItem.AssessmentCriteria.Count > 0 Then
                    @Html.JQueryUI.ActionButton(
                        "View Assessments", "View", "AssessmentCriterionGroups",
                        New With {
                            .ProjectRouteName = currentItem.Project.getRouteName,
                            .MonitorLocationRouteName = currentItem.getRouteName,
                            .strAssessmentDate = Format(Date.Today, "yyyy-MM-dd")
                        },
                        Nothing
                    )
                End If
            </td>
        </tr>
    Next
</table>