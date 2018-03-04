@ModelType SEC_Monitoring_Data_Website.MeasurementMetric

@Code
    Dim showMeasurementMetricLinks = DirectCast(ViewData("ShowMeasurementMetricLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Filter Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each cf In Model.CalculationFilters.OrderBy(Function(f) f.FilterName)
        @<tr>
            <td>
                @If showMeasurementMetricLinks Then
                    @Html.RouteLink(cf.FilterName, 
                                    "CalculationFilterDetailsRoute", 
                                    New With {.CalculationFilterRouteName = cf.getRouteName})
                Else
                    @cf.FilterName
                End If
            </td>
        </tr>
    Next

</table>