@ModelType SEC_Monitoring_Data_Website.UploadMeasurementsViewModel

@Code
    ViewData("Title") = "Upload Measurements"
End Code

<script type="text/javascript">

    function hideAllControls() {
        @For Each mft In Model.MeasurementFileTypes
            @Html.Raw("$('#upload_" + mft.getHtmlName + "').hide();" + vbCrLf)
        Next

    }
    function showControls(controlsIndex) {
        hideAllControls();
        switch (controlsIndex) {
            @For Each mft In Model.MeasurementFileTypes
                @Html.Raw("case '" + mft.Id.ToString + "':" + vbCrLf)
                @Html.Raw("$('#upload_" + mft.getHtmlName + "').show();" + vbCrLf)
                @Html.Raw("break;" + vbCrLf)
        Next

        }
    }
    function validateUploadButton() {
        if ($('#uploadFile').prop('value') != '') {
            $('#uploadButton').show();
        }
        else {
            $('#uploadButton').hide();
        }
    }
    function ShowHideRoundInputMeasurementsControls() {
        if ($("#RoundInputMeasurements").is(':checked')) {
            $("#RoundInputMeasurementsControls").show();
        } else {
            $("#RoundInputMeasurementsControls").hide();
        }
    };
    function ShowHideAddTimeOffsetControls() {
        if ($("#AddTimeOffset").is(':checked')) {
            $("#AddTimeOffsetControls").show();
        } else {
            $("#AddTimeOffsetControls").hide();
        }
    };
    function ShowHideAddDaylightSavingsTimeOffsetControls() {
        if ($("#AddDaylightSavingsTimeOffset").is(':checked')) {
            $("#AddDaylightSavingsTimeOffsetControls").show();
        } else {
            $("#AddDaylightSavingsTimeOffsetControls").hide();
        }
    };

    $(document).ready(function () {
        hideAllControls();
        validateUploadButton();
        ShowHideRoundInputMeasurementsControls();
        $("#RoundInputMeasurements").click(function () { ShowHideRoundInputMeasurementsControls() });
        ShowHideAddTimeOffsetControls();
        $("#AddTimeOffset").click(function () { ShowHideAddTimeOffsetControls() });
        ShowHideAddDaylightSavingsTimeOffsetControls();
        $("#AddDaylightSavingsTimeOffset").click(function () { ShowHideAddDaylightSavingsTimeOffsetControls() });
    });

</script>

<h2>Upload Measurements</h2>

@Using Html.BeginRouteForm("MeasurementPostUploadRoute", Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data"})

    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @Html.HiddenFor(Function(model) model.MonitorId)
    @Html.HiddenFor(Function(model) model.MonitorLocationId)
    @Html.HiddenFor(Function(model) model.ProjectId)

    @<table class="create-table">
        <tr>
            <th>
                Monitor Location
            </th>
            <td>
                @Model.MonitorLocation.MonitorLocationName
            </td>
        </tr>
        <tr>
            <th>
                Monitor
            </th>
            <td>
                @Model.Monitor.MonitorName
            </td>
        </tr>
        <tr>
            <th>
                Measurement File Type
            </th>
            <td>
                @Html.DropDownListFor(Function(model) model.MeasurementFileTypeId, Model.MeasurementFileTypeList,
                                      "Please select a Measurement File Type...",
                                      New With {.onchange = "showControls(MeasurementFileTypeId.value);"})
            </td>
        </tr>
        <tr>
            <th>
                Measurement File
            </th>
            <td>
                <input type="file" name="UploadFile" id="uploadFile" onchange="validateUploadButton();" />
            </td>
        </tr>
    </table>
    
    @<h3>
        Round Measurement Times? @Html.EditorFor(Function(model) model.RoundInputMeasurements)
    </h3>
    @<div id='RoundInputMeasurementsControls'>
        <table class="create-table">
            <tr>
                <td>
                    Round to nearest
                </td>
                <td>
                    @Html.EditorFor(Function(model) model.RoundInputCount)
                </td>
                <td>
                    @Html.DropDownListFor(Function(model) model.RoundInputDurationValue, Model.RoundInputDurationList)
                </td>
            </tr>
        </table>
    </div>
    
    @<h3>
        Add Global Time Offset? @Html.EditorFor(Function(model) model.AddTimeOffset)
    </h3>
    @<div id='AddTimeOffsetControls'>
        <table class="create-table">
            <tr>
                <td>
                    Add offset to each Measurement:
                </td>
                <td>
                    @Html.EditorFor(Function(model) model.AddTimeCount)
                </td>
                <td>
                    @Html.DropDownListFor(Function(model) model.AddTimeDurationValue, Model.AddTimeDurationList)
                </td>
            </tr>
        </table>
    </div>

@<h3>
    Add Daylight Savings Time Offset? @Html.EditorFor(Function(model) model.AddDaylightSavingsTimeOffset)
</h3>
@<div id='AddDaylightSavingsTimeOffsetControls'>
    <table class="create-table">
        <tr>
            <td>
                For Measurements starting at or after
            </td>
            <td>
                @Html.JQueryUI.DatepickerFor(Function(model) model.DaylightSavingsBorderDate)
                @Html.EditorFor(Function(model) model.DaylightSavingsBorderTime)
            </td>
        </tr>
        <tr>
            <td>
                add
            </td>
            <td>
                @Html.EditorFor(Function(model) model.AddDaylightSavingsHourCount) hours
            </td>
        </tr>

    </table>
</div>
    
    @For Each mft In Model.MeasurementFileTypes
        @Html.Raw("<div id='upload_" + mft.getHtmlName + "'>" + vbCrLf)
        @Html.Partial(mft.getUploadViewName, Model)
        @Html.Raw("</div>" + vbCrLf)
    Next

    @<p>
        @Html.JQueryUI.Button("Upload", New With {.id = "uploadButton"})
    </p>

End Using




