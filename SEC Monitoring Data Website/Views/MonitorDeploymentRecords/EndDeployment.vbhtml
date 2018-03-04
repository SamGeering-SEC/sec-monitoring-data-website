@ModelType SEC_Monitoring_Data_Website.MonitorDeploymentRecord

@Code
    ViewData("Title") = "End Monitor Deployment"
End Code

<h2>End Monitor Deployment</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>MonitorDeploymentRecord</legend>

        @Html.HiddenFor(Function(model) model.Id)
        @Html.HiddenFor(Function(model) model.DeploymentStartDate)
        @Html.HiddenFor(Function(model) model.MonitorId)
        @Html.HiddenFor(Function(model) model.MonitorLocationId)

        @Using t = Html.JQueryUI().BeginTabs()

                t.Tab("Basic Details", "basic_details")
                t.Tab("Settings", "settings")


                ' Basic Details Tab
            @Using t.BeginPanel
                @Html.Partial("EndDeployment_BasicDetails", Model)
            End Using

            ' Settings Tab
            @Using t.BeginPanel
                @Html.Partial("Details_MonitorSettings", Model)
            End Using

        End Using

        <p>
            @Html.JQueryUI.Button("End Deployment")
        </p>
    </fieldset>
            End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
