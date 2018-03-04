@ModelType SEC_Monitoring_Data_Website.Country

@Code
    ViewData("Title") = "Create Country"
End Code

<h2>Create Country</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Country</legend>

         <table class="create-table">
             <tr>
                 <th>
                     Country Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.CountryName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.CountryName)
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
