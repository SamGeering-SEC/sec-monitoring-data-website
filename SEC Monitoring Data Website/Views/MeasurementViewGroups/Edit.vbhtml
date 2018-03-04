@ModelType SEC_Monitoring_Data_Website.EditMeasurementViewGroupViewModel

@Code
    ViewData("Title") = "Edit Measurement View Group"
End Code

<h2>Edit Measurement View Group</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>MeasurementViewGroup</legend>

         @Html.HiddenFor(Function(model) model.MeasurementView.Id)
         @Html.HiddenFor(Function(model) model.MeasurementViewGroup.Id)

         <table class="create-table">
             <tr>
                 <th>
                     Measurement View
                 </th>
                 <th>
                     @Html.DisplayFor(Function(model) model.MeasurementView.ViewName)
                 </th>
             </tr>
             <tr>
                 <th>
                     Group Index
                 </th>
                 <td>
                     @Html.DisplayFor(Function(model) model.MeasurementViewGroup.GroupIndex)
                     @Html.HiddenFor(Function(model) model.MeasurementViewGroup.GroupIndex)
                 </td>
                 <td></td>
             </tr>
             <tr>
                 <th>
                     Main Header
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementViewGroup.MainHeader)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementViewGroup.MainHeader)
                 </td>
             </tr>
             <tr>
                 <th>
                     Sub Header
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementViewGroup.SubHeader)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementViewGroup.SubHeader)
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
