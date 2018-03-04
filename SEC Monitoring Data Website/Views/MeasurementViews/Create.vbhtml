@ModelType SEC_Monitoring_Data_Website.CreateMeasurementViewViewModel

@Code
    ViewData("Title") = "Create Measurement View"
End Code

<h2>Create Measurement View</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>MeasurementView</legend>

         <table class="create-table">
             <tr>
                 <th>
                     View Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementView.ViewName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementView.ViewName)
                 </td>
             </tr>
             <tr>
                 <th>
                     Display Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementView.DisplayName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementView.DisplayName)
                 </td>
             </tr>
             <tr>
                 <th>
                     Measurement Type
                 </th>
                 <td>
                     @Html.DropDownListFor(Function(model) model.MeasurementTypeId, Model.MeasurementTypeList, "Please select a Measurement Type...")
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementTypeId)
                 </td>
             </tr>
             <tr>
                 <th>
                     Measurement View Table Type
                 </th>
                 <td>
                     @Html.DropDownListFor(Function(model) model.MeasurementViewTableTypeId, Model.MeasurementViewTableTypeList, "Please select a Measurement View Table Type...")
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementViewTableTypeId)
                 </td>
             </tr>
             <tr>
                 <th>
                     Table Results Header
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementView.TableResultsHeader)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementView.TableResultsHeader)
                 </td>
             </tr>
             <tr>
                 <th>
                     Y-axis Minimum Value
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementView.YAxisMinValue)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementView.YAxisMinValue)
                 </td>
             </tr>
             <tr>
                 <th>
                     Y-axis Maximum Value
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementView.YAxisMaxValue)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementView.YAxisMaxValue)
                 </td>
             </tr>
             <tr>
                 <th>
                     Y-axis Step Value
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementView.YAxisStepValue)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementView.YAxisStepValue)
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
