@ModelType SEC_Monitoring_Data_Website.MeasurementFileDetailsViewModel

@Code
    ViewData("Title") = "Measurement File Details"
End Code

<script type="text/javascript">

    var pageNumber;
    var pageSize;
    var sortBy;

    function getVariables() {
        pageNumber = parseInt($('#PageNumber').val());
        pageSize = parseInt($('#PageSize').val());
        sortBy = $('#SortBy').val();
    }

    function update_table() {
        $.ajax({
            type: 'GET',
            url: '@Url.RouteUrl("MeasurementFileUpdateDetailsTableRoute")',
            data: {
                MeasurementFileId: $('#MeasurementFile_Id').val(),
                PageNumber: pageNumber,
                PageSize: pageSize,
                SortBy: sortBy
            },
            beforeSend: function () {
                $('#measurementsTable').empty();
                $("#divLoadingElement").show();
            },
            success: function (partialView) {
                $('#tableAndControls').html(partialView);
                $('#tableAndControls').jQueryUIHelpers();
                addButtonAnimations();
            },
            failure: function () {
                alert('Failed to update table');
            }
        });
    };

    function deleteMeasurementSuccess(data, textStatus, jqXHR) {
        getVariables();
        update_table();
    }
    function deleteMeasurementError(jqXHR, textStatus, errorThrown) {
        alert(textStatus);
    }

    function previousPage() {
        getVariables();
        pageNumber -= 1;
        update_table();
    }
    function nextPage() {
        getVariables();
        pageNumber += 1;
        update_table();
    }
    function firstPage() {
        getVariables();
        pageNumber = 1;
        update_table();
    }
    function lastPage() {
        getVariables();
        pageNumber = $('#NumPages').val();
        update_table();
    }
    function changePageSize() {
        getVariables();
        pageNumber = 1;
        update_table();
    }
    function changeSorting() {
        getVariables();
        update_table();
    }

</script>

@Html.HiddenFor(Function(model) model.MeasurementFile.Id)

<h2>Measurement File Details</h2>


@Using t = Html.JQueryUI().BeginTabs()


    t.Tab("Basic Details", "basic_details")
    If Model.NumMeasurements > 0 Then
        t.Tab("Table", "table")
    End If

    If Model.NumMeasurements > 0 Then
        t.Tab("Graph", "graph")
    End If

    
    ' Basic Details Tab
    @If Model.NumMeasurements > 0 Then
        @Using t.BeginPanel
            @Html.Partial("Details_BasicDetails", Model)
        End Using
    End If

    ' Table Tab
    @If Model.NumMeasurements > 0 Then
        @Using t.BeginPanel
            @<div id="tableAndControls">
                @Html.Partial("Details_Table", Model)
            </div>
        End Using
    End If

    ' Graph Tab
    @If Model.NumMeasurements > 0 Then
        @Using t.BeginPanel
            @Html.Action("Details_Graph", "MeasurementFiles", Model.MeasurementFile.Id)
        End Using
    End If

End Using


@Using (Html.JQueryUI().Begin(New Dialog().Title("Delete Measurement") _
                       .AutoOpen(False).Modal(True) _
                       .ConfirmAjax(".DeleteMeasurementLink",
                                    "Yes",
                                    "No",
                                    New AjaxSettings With {
                                        .Method = HttpVerbs.Post,
                                        .Success = "deleteMeasurementSuccess",
                                        .Error = "deleteMeasurementError"
                                    }
                                )
                        ))
    
    @<p>
        @Html.Raw("Would you like to delete this Measurement?")
    </p>
    
End Using

@Html.Partial("NavigationButtons", Model.NavigationButtons)


@Html.Partial("DeleteObjectConfirmation", "MeasurementFile")