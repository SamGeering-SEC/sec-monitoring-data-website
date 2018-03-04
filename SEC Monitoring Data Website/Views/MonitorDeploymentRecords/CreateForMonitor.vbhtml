@ModelType SEC_Monitoring_Data_Website.CreateMonitorDeploymentRecordViewModel

@Code
    ViewData("Title") = "Deploy Monitor"
End Code

<h2>Deploy Monitor</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>MonitorDeploymentRecord</legend>

    @Html.HiddenFor(Function(model) model.MonitorId)

    @If Model.ValidationErrors.Count > 0 Then
        @<h3 style="color:red;">
            Validation Errors, please check the individual tabs.
        </h3>
    End If

@Using t = Html.JQueryUI().BeginTabs()

        t.Tab("Basic Details", "basic_details")
        t.Tab("Monitor Settings", "monitor_settings")


        ' Basic Details Tab
        @Using t.BeginPanel
            @Html.Partial("CreateForMonitor_BasicDetails", Model)
        End Using

        ' Monitor Settings Tab
        @Using t.BeginPanel
            @Html.Partial("CreateForMonitor_MonitorSettings", Model)
        End Using

End Using

        <p>
            @Html.JQueryUI.Button("Deploy")
        </p>
    </fieldset>
            End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
