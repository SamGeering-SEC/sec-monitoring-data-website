@ModelType List(Of SEC_Monitoring_Data_Website.MeasurementMetric)

@Code
    Dim showMeasurementMetricLinks = DirectCast(ViewData("ShowMeasurementMetricLinks"), Boolean)
    Dim showDeleteMeasurementMetricLinks = DirectCast(ViewData("ShowDeleteMeasurementMetricLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                Metric Name
            </th>
            <th>
                Display Name
            </th>
            <th>
                Measurement Type
            </th>
            <th>
                Fundamental Unit
            </th>
            <th>
                # Rounding Decimal Places
            </th>
            <th></th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showMeasurementMetricLinks Then
                        @Html.RouteLink(currentItem.MetricName,
                                "MeasurementMetricDetailsRoute",
                                New With {.MeasurementMetricRouteName = currentItem.getRouteName})
                    Else
                        @currentItem.MetricName
                    End If
                </td>
                <td>
                    @Html.Raw(currentItem.DisplayName)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) currentItem.MeasurementType.MeasurementTypeName)
                </td>
                <td>
                    @Html.Raw(currentItem.FundamentalUnit)
                </td>
                <td style="text-align:center">
                    @Html.DisplayFor(Function(modelItem) currentItem.RoundingDecimalPlaces)
                </td>
                <td>
                    @If showDeleteMeasurementMetricLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                     "MeasurementMetricDeleteByIdRoute",
                                     New With {.MeasurementMetricId = currentItem.Id},
                                     New With {.class = "DeleteMeasurementMetricLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>

            </tr>
        Next

    </table>

Else
    @<p>
        There are no Metrics available to view.
    </p>
End If
