@ModelType SEC_Monitoring_Data_Website.EditMonitorLocationViewModel

<script type="text/javascript">

    function ShowHideFacadeLocationRow() {
        var mti = $('#MonitorLocation_MeasurementTypeId').val();
        if (mti == '1') {
            $("#FacadeLocationRow").show();
        }
        else {
            $("#FacadeLocationRow").hide();
        }
    };

    $(document).ready(function () {

        ShowHideFacadeLocationRow();

    });

</script>

<table class="edit-table">
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
            @Html.HiddenFor(Function(model) model.MonitorLocation.MeasurementTypeId)
            @Html.DisplayFor(Function(model) model.MonitorLocation.MeasurementType.MeasurementTypeName)
        </td>
        <td>
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