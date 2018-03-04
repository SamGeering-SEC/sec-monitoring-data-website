@ModelType SEC_Monitoring_Data_Website.OrganisationType

@Code
    ViewData("Title") = "Create Organisation Type"
End Code

<h2>Create Organisation Type</h2>

@Using Html.BeginForm()
    @Html.ValidationSummary(True)
    @Html.AntiForgeryToken()

    @<fieldset>
        <legend>OrganisationType</legend>

         <table class="create-table">
             <tr>
                 <th>
                     Organisation Type Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.OrganisationTypeName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.OrganisationTypeName)
                 </td>
             </tr>
         </table>

        <p>
            @Html.JQueryUI.Button("Create")
        </p>
    </fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
