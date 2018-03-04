@ModelType SEC_Monitoring_Data_Website.UploadResultViewModel

@Code
    ViewData("Title") = "Measurement File Upload Result"
End Code
@Code
    Dim showMonitorLink = DirectCast(ViewData("ShowMonitorLink"), Boolean)
    Dim showMonitorLocationLink = DirectCast(ViewData("ShowMonitorLocationLink"), Boolean)
End Code

<h2>Measurement Upload: @IIf(Model.MeasurementFile.UploadSuccess,
                             Html.Raw("<span style='color: #00ff00'>Success</span>"),
                             Html.Raw("<span style='color: #ff0000'>Failure</span>"))</h2>

@Using t = Html.JQueryUI().BeginTabs()


    t.Tab("Basic Details", "basic_details")

    ' Basic Details Tab
    @Using t.BeginPanel
        @Html.Partial("UploadResult_BasicDetails", Model)
    End Using

End Using

@Html.Partial("NavigationButtons", Model.NavigationButtons)
