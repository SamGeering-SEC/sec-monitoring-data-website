@ModelType SEC_Monitoring_Data_Website.OrganisationType

@Code
    ViewData("Title") = "Edit"
End Code

<h2>Edit</h2>

@Using Html.BeginForm()

    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>OrganisationType</legend>

        @Html.HiddenFor(Function(model) model.Id)

         <table class="edit-table">
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
            @Html.JQueryUI.Button("Save")
        </p>
    </fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
