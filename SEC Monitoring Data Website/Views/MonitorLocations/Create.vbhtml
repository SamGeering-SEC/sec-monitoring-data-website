@ModelType SEC_Monitoring_Data_Website.CreateMonitorLocationViewModel

@Code
    ViewData("Title") = "Create Monitor Location"
End Code

<script type="text/javascript">

    function ShowHideFacadeLocationRow() {
        var mti = $('#MeasurementTypeId').val();
        if (mti == '1') {
            $("#FacadeLocationRow").show();
        }
        else {
            $("#FacadeLocationRow").hide();
        }
    };

    $(document).ready(function () {

        ShowHideFacadeLocationRow();
        $("#MeasurementTypeId").change(function () { ShowHideFacadeLocationRow() });

    });

</script>

<h2>Create Monitor Location for @Model.Project.FullName</h2>

@Using Html.BeginForm()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>Monitor Location</legend>

    @Html.HiddenFor(Function(model) model.Project.Id)
    
         <table class="create-table">
             <tr>
                 <th>
                     Monitor Location Name
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MonitorLocation.MonitorLocationName)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MonitorLocation.MonitorLocationName)
                 </td>
             </tr>
             <tr>
                 <th>
                     Location
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MonitorLocation.MonitorLocationGeoCoords)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MonitorLocation.MonitorLocationGeoCoords)
                 </td>
             </tr>
             <tr>
                 <th>
                     Height Above Ground
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MonitorLocation.HeightAboveGround)m
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MonitorLocation.HeightAboveGround)
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
             <tr id="FacadeLocationRow">
                 <th>
                     Facade Location?
                 </th>
                 <td>
                     @Html.EditorFor(Function(model) model.MonitorLocation.IsAFacadeLocation)
                 </td>
                 <td>
                     @Html.ValidationMessageFor(Function(model) model.MonitorLocation.IsAFacadeLocation)
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
