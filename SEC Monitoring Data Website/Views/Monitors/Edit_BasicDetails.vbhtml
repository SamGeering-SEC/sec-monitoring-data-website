@ModelType SEC_Monitoring_Data_Website.EditMonitorViewModel

<table class="edit-table">

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
                Error - Monitor Name "@ViewData("MonitorNameExists")" already exists for a different Monitor!
            </td>
        </tr>
    End If
    <tr>
        <th>
            Measurement Type
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.MeasurementTypeId, Model.MeasurementTypeList)
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
            @Html.DropDownListFor(Function(model) model.OwnerOrganisationId, Model.OwnerOrganisationList)
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

    @Html.HiddenFor(Function(model) model.Monitor.RequiresCalibration)

    @If Model.Monitor.RequiresCalibration = True Then


    @<tr>
        <th>
            Last Field Calibration
        </th>
        <td>
        @Html.JQueryUI.DatepickerFor(Function(model) model.Monitor.LastFieldCalibration)
        </td>
        <td>
        @Html.ValidationMessageFor(Function(model) model.Monitor.LastFieldCalibration)
        </td>
    </tr>
    @<tr>
        <th>
            Last Full Calibration
        </th>
        <td>
        @Html.JQueryUI.DatepickerFor(Function(model) model.Monitor.LastFullCalibration)
        </td>
        <td>
        @Html.ValidationMessageFor(Function(model) model.Monitor.LastFullCalibration)
        </td>
    </tr>
    @<tr>
        <th>
            Next Full Calibration
        </th>
        <td>
        @Html.JQueryUI.DatepickerFor(Function(model) model.Monitor.NextFullCalibration)
        </td>
        <td>
        @Html.ValidationMessageFor(Function(model) model.Monitor.NextFullCalibration)
        </td>
    </tr>

    Else

        @Html.HiddenFor(Function(model) model.Monitor.LastFieldCalibration)
        @Html.HiddenFor(Function(model) model.Monitor.LastFullCalibration)
        @Html.HiddenFor(Function(model) model.Monitor.NextFullCalibration)

    End If

</table>