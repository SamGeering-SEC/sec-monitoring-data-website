@ModelType SEC_Monitoring_Data_Website.ViewMeasurementsViewModel

@If Model.FilteredMeasurements.ToList.Count > 0 Then

    @Html.Action("View_MeasurementsChart", Model)

End If



@Section Scripts
    @Scripts.Render("~/bundles/HighChart")
End Section
