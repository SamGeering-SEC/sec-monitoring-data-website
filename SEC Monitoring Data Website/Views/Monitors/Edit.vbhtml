@ModelType SEC_Monitoring_Data_Website.EditMonitorViewModel

@Code
    ViewData("Title") = "Edit Monitor"
End Code

<h2>Edit Monitor</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Monitor</legend>

        @Html.HiddenFor(Function(model) model.Monitor.Id)

        @Using t = Html.JQueryUI().BeginTabs()

            t.Tab("Basic Details", "basic_details")
            @If Model.Monitor.CurrentLocation IsNot Nothing Then
                t.Tab("Status", "status")
            End If
            t.Tab("Deployment Records", "deployment_records")


            ' Basic Details Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_BasicDetails", Model)
            End Using

            ' Status Tab
            @If Model.Monitor.CurrentLocation IsNot Nothing Then
                @Using t.BeginPanel
                    @Html.Partial("Edit_Status", Model)
                End Using
            End If

            ' Deployment Records Tab
            @Using t.BeginPanel
                @Html.Partial("Edit_DeploymentRecords", Model)
            End Using

        End Using

        <p>
            @Html.JQueryUI.Button("Save")
        </p>
    </fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
