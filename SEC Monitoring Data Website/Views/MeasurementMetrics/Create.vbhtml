@ModelType SEC_Monitoring_Data_Website.CreateMeasurementMetricViewModel

@Code
    ViewData("Title") = "Create Measurement Metric"
End Code

<h2>Create Measurement Metric</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>MeasurementMetric</legend>

         <table class="create-table">
             <tr>
                 <th>
                     Metric Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementMetric.MetricName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementMetric.MetricName)
                 </td>
             </tr>
             <tr>
                 <th>
                     Display Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementMetric.DisplayName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementMetric.DisplayName)
                 </td>
             </tr>
             <tr>
                 <th>
                     Measurement Type
                 </th>
                 <td>
                     @Html.DropDownListFor(Function(model) model.MeasurementTypeId, 
                                           Model.MeasurementTypeList, 
                                           "Please select a Measurement Type...")
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementTypeId)
                 </td>
             </tr>
             <tr>
                 <th>
                     Fundamental Unit
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementMetric.FundamentalUnit)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementMetric.FundamentalUnit)
                 </td>
             </tr>
             <tr>
                 <th>
                     Rounding Decimal Places
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MeasurementMetric.RoundingDecimalPlaces)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementMetric.RoundingDecimalPlaces)
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
