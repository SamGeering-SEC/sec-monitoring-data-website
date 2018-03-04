@ModelType SEC_Monitoring_Data_Website.ViewMeasurementsViewModel

<table class="measurements-table">
    <tr>
        <th rowspan="4">
            Day
        </th>
        <th rowspan="4">
            Date
        </th>
        <th rowspan="4">
            Time
        </th>
        <th colspan="@Model.SelectedMeasurementView.getSequenceSettings.Count.ToString">
            @Model.SelectedMeasurementView.TableResultsHeader
        </th>
    </tr>
    <tr>
        @For g = 0 To Model.SelectedMeasurementView.MeasurementViewGroups.Count - 1
            @<th colspan="@Model.SelectedMeasurementView.MeasurementViewGroups(g).MeasurementViewSequenceSettings.Count">
                @Model.SelectedMeasurementView.MeasurementViewGroups(g).MainHeader
            </th>
        Next
    </tr>
    <tr>
        @For g = 0 To Model.SelectedMeasurementView.MeasurementViewGroups.Count - 1
            @<th colspan="@Model.SelectedMeasurementView.MeasurementViewGroups(g).MeasurementViewSequenceSettings.Count">
                @Model.SelectedMeasurementView.MeasurementViewGroups(g).SubHeader
            </th>
        Next
    </tr>
    <tr>
        @For Each mvg In Model.SelectedMeasurementView.MeasurementViewGroups
            @For Each mvss In mvg.MeasurementViewSequenceSettings
                @<th>
                    @mvss.TableHeader
                </th>
            Next
        Next
    </tr>

    @For Each startDateTime In Model.StartDateTimes
        @<tr>
            <td>
                @Format(startDateTime, "ddd")
            </td>
            <td>
                @Format(startDateTime, "dd/MM/yyyy")
            </td>
            <td>
                @Format(startDateTime, "HH:mm")
            </td>
            @For Each fms In Model.FilteredMeasurements
                @<td>
                    @If fms.hasMeasurementAtDateTime(startDateTime) = True Then
                        @Math.Round(
                            CDec(fms.getMeasurementAtDateTime(startDateTime).getFilteredLevel),
                            fms.getMetric.RoundingDecimalPlaces, MidpointRounding.AwayFromZero
                        ).ToString("F" + fms.getMetric.RoundingDecimalPlaces.ToString)
                    Else
                        @<text>N/A</text>
                    End If
                </td>
            Next
        </tr>
    Next
</table>