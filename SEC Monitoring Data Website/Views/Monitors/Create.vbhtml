@ModelType SEC_Monitoring_Data_Website.CreateMonitorViewModel

<script type="text/javascript">

    function ShowHideCalibrationFields() {
        if ($("#Monitor_RequiresCalibration").is(':checked')) {
            $("#CalibrationFields").show();
        } else {
            $("#CalibrationFields").hide();
        }
    };


    $(document).ready(function () {
        ShowHideCalibrationFields();
        $("#Monitor_RequiresCalibration").click(function () { ShowHideCalibrationFields() });
    });

</script>

@Code
    ViewData("Title") = "Create Monitor"
End Code

<h2>Create Monitor</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Monitor</legend>
         <table class="create-table">
             <tr>
                 <th>
                     Monitor Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.Monitor.MonitorName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.Monitor.MonitorName)
                 </td>
             </tr>
             @If ViewData.ContainsKey("MonitorNameExists") Then
                @<tr>
                    <td></td>
                    <td style="color:red">
                        Error - Monitor Name "@ViewData("MonitorNameExists")" already exists!
                    </td>
                </tr>
             End If
             <tr>
                 <th>
                     Measurement Type
                 </th>
                 <td>
                     @Html.DropDownListFor(Function(model) model.MeasurementTypeId, model.MeasurementTypeList, "Please select a Measurement Type...")
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MeasurementTypeId)
                 </td>
             </tr>
             <tr>
                 <th>
                     Serial Number
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.Monitor.SerialNumber)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.Monitor.SerialNumber)
                 </td>
             </tr>
             <tr>
                 <th>
                     Manufacturer
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.Monitor.Manufacturer)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.Monitor.Manufacturer)
                 </td>
             </tr>
             <tr>
                 <th>
                     Model
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.Monitor.Model)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.Monitor.Model)
                 </td>
             </tr>
             <tr>
                 <th>
                     Category
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.Monitor.Category)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.Monitor.Category)
                 </td>
             </tr>
             <tr>
                 <th>
                     Owner Organisation
                 </th>
                 <td>
                     @Html.DropDownListFor(Function(model) model.OwnerOrganisationId, Model.OwnerOrganisationList, "Please select an Owner Organisation...")
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.OwnerOrganisationId)
                 </td>
             </tr>
             <tr>
                 <th>
                     Requires Calibration
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.Monitor.RequiresCalibration)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.Monitor.RequiresCalibration)
                 </td>
             </tr>
             </table>

         <div id="CalibrationFields">

             <table class="create-table">
                 <tr>
                     <th>
                         Last Field Calibration
                     </th>
                     <td>
                         @Html.EditorFor(Function(model) model.Monitor.LastFieldCalibration)
                     </td>
                     <td>
                         @Html.ValidationMessageFor(Function(model) model.Monitor.LastFieldCalibration)
                     </td>
                 </tr>
                 <tr>
                     <th>
                         Last Full Calibration
                     </th>
                     <td>
                         @Html.EditorFor(Function(model) model.Monitor.LastFullCalibration)
                     </td>
                     <td>
                         @Html.ValidationMessageFor(Function(model) model.Monitor.LastFullCalibration)
                     </td>
                 </tr>
                 <tr>
                     <th>
                         Next Full Calibration
                     </th>
                     <td>
                         @Html.EditorFor(Function(model) model.Monitor.NextFullCalibration)
                     </td>
                     <td>
                         @Html.ValidationMessageFor(Function(model) model.Monitor.NextFullCalibration)
                     </td>
                 </tr>
             </table>

         </div>

        <p>
            @Html.JQueryUI.Button("Create")
        </p>

    </fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
