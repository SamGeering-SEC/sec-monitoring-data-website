@ModelType List(Of SEC_Monitoring_Data_Website.CalculationFilter)

@Code
    Dim showCalculationFilterLinks = DirectCast(ViewData("ShowCalculationFilterLinks"), Boolean)
    Dim showMeasurementMetricLinks = DirectCast(ViewData("ShowMeasurementMetricLinks"), Boolean)
    Dim showDeleteCalculationFilterLinks = DirectCast(ViewData("ShowDeleteCalculationFilterLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                Filter Name
            </th>
            <th>
                Metric
            </th>
            <th>
                Aggregate Function
            </th>
            <th>
            </th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td width="30%">
                    @If showCalculationFilterLinks Then
                        @Html.RouteLink(currentItem.FilterName,
                                    "CalculationFilterDetailsRoute",
                                    New With {.CalculationFilterRouteName = currentItem.getRouteName})
                    Else
                        @currentItem.FilterName
                    End If
                </td>
                <td width="30%">
                    @If showMeasurementMetricLinks Then
                        @Html.RouteLink(currentItem.MeasurementMetric.MetricName,
                                        "MeasurementMetricDetailsRoute",
                                        New With {.MeasurementMetricRouteName = currentItem.MeasurementMetric.getRouteName})
                    Else
                        @currentItem.MeasurementMetric.MetricName
                    End If
                </td>
                <td width="30%">
                    @Html.DisplayFor(Function(modelItem) currentItem.CalculationAggregateFunction.FunctionName)
                </td>
                <td>
                    @If showDeleteCalculationFilterLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                         "CalculationFilterDeleteByIdRoute",
                                         New With {.CalculationFilterId = currentItem.Id},
                                         New With {.class = "DeleteCalculationFilterLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>
            </tr>
        Next

    </table>


Else
    @<p>
        There are no Calculation Filters available to view.
    </p>
End If
