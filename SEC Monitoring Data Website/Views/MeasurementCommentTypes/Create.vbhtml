@ModelType SEC_Monitoring_Data_Website.MeasurementCommentType

@Code
    ViewData("Title") = "Create Measurement Comment Type"
End Code

<h2>Create Measurement Comment Type</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>MeasurementCommentType</legend>

         <table class="create-table">
             <tr>
                 <th>
                     Comment Type Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.CommentTypeName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.CommentTypeName)
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
