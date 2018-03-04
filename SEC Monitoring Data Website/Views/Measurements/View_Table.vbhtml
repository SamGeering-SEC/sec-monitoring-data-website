@ModelType SEC_Monitoring_Data_Website.ViewMeasurementsViewModel

@If Model.FilteredMeasurements.ToList.Count > 0 Then

    @Select Case Model.SelectedMeasurementView.MeasurementViewTableType.TableTypeName
            Case "Daily"
        @Html.Partial("View_DailyMeasurementsTable", Model)
    Case "Dynamic"
        @Html.Partial("View_DynamicMeasurementsTable", Model)
End Select

Else

    @<h3>
        There are no Measurements to View for the Time Period @Format(Model.StartDate, "dd MMMM yyyy") - @Format(Model.EndDate, "dd MMMM yyyy").
    </h3>

End If

