@ModelType SEC_Monitoring_Data_Website.IViewObjectsByMeasurementTypeViewModel

<script type="text/javascript">

    var SearchText;

    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            clearTimeout(timer);
            timer = setTimeout(callback, ms);
        };
    })();

    function getVariables() {
        SearchText = $('#SearchText').val();
        MeasurementTypeId = $('#MeasurementTypeId').val();
    };

    function update_table() {
        getVariables();
        $.ajax({
            type: 'GET',
            url: '@Url.RouteUrl(Model.UpdateTableRouteName)',
            data: {
                SearchText: SearchText,
                MeasurementTypeId: MeasurementTypeId
            },
            beforeSend: function () {
                $('#divLoadingElement').css('display", "inline');
            },
            success: function (partialView) {
                $('#divLoadingElement').css("display", "none");
                $('#@Model.TableId').html(partialView);
                addButtonAnimations();
                try {
                    filterRows();
                }
                catch (err) {}
            },
            error: function () {
                alert('Failed to update table');
            }
        });
    };

    function delayed_update_table() {
        delay(function () {
            update_table();
        }, 1000)
    }

    function deleteObjectSuccess(data, textStatus, jqXHR) {
        update_table();
        addButtonAnimations();
    }

    function deleteObjectError(jqXHR, textStatus, errorThrown) {
        alert(textStatus);
    }

</script>


<table id="search-bar" class="index-table">
    <tr>
        <th>
            Search
        </th>
        <td>
            @Html.TextBoxFor(Function(model) model.SearchText, 
                             New With {.onkeyup = "delayed_update_table();"})
        </td>
    </tr>
    <tr>
        <th>
            Measurement Type
        </th>
        <th>
            @Html.DropDownListFor(Function(model) model.MeasurementTypeId,
                                  Model.MeasurementTypeList,
                                  "Please select a Measurement Type...",
                                  New With {.onchange = "delayed_update_table();"})
        </th>
    </tr>
</table>

<div id="divLoadingElement" style="display:none">
    <p>
        <img src="~/Images/loading.gif" />
    </p>
</div>

@Using (Html.JQueryUI().Begin(New Dialog().Title("Delete " + Model.ObjectDisplayName).AutoOpen(False).Modal(True).ConfirmAjax(".Delete" + Model.ObjectName + "Link", "Yes", "No", New AjaxSettings With {.Method = HttpVerbs.Post,
.Success = "deleteObjectSuccess",
.Error = "deleteObjectError"})))
    @<p>
    @Html.Raw("Would you like to delete this " + Model.ObjectDisplayName + "?")
</p>
End Using