@ModelType SEC_Monitoring_Data_Website.DocumentType

@Code
    ViewData("Title") = "Create Document Type"
End Code

<h2>Create Document Type</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>DocumentType</legend>

         <table class="create-table">
             <tr>
                 <th>
                     Document Type Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.DocumentTypeName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.DocumentTypeName)
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
