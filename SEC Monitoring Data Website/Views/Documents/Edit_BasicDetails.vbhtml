@ModelType SEC_Monitoring_Data_Website.EditDocumentViewModel


    <script type="text/javascript">

        function ShowHideDateTimeRows() {
            if ($("#Document_HasDateRange").is(':checked')) {
                $("#StartDateTimeRow").show();
                $("#EndDateTimeRow").show();
            } else {
                $("#StartDateTimeRow").hide();
                $("#EndDateTimeRow").hide();
            }
        };

        $(document).ready(function () {

            ShowHideDateTimeRows();
            $("#Document_HasDateRange").click(function () { ShowHideDateTimeRows() });

        });

    </script>


@Html.HiddenFor(Function(model) model.Document.Id)

<table class="edit-table">
    <tr>
        <th>
            Title
        </th>
        <td>
            @Html.EditorFor(Function(model) model.Document.Title)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.Document.Title)
        </td>
    </tr>
    <tr>
        <th>
            Has Date Range?
        </th>
        <td>
            @Html.CheckBoxFor(Function(model) model.Document.HasDateRange)
        </td>
        <td></td>
    </tr>
    <tr id="StartDateTimeRow">
        <th>
            Start Date and Time
        </th>
        <td>
            @Html.JQueryUI.DatepickerFor(Function(model) model.StartDate)<br>@Html.EditorFor(Function(model) model.StartTime)
        </td>
        <td></td>
    </tr>
    <tr id="EndDateTimeRow">
        <th>
            End Date and Time
        </th>
        <td>
            @Html.JQueryUI.DatepickerFor(Function(model) model.EndDate)<br>@Html.EditorFor(Function(model) model.EndTime)
        </td>
        <td></td>
    </tr>
    <tr>
        <th>
            Document Type
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.DocumentTypeId, Model.DocumentTypeList, "Please select a Document Type...")
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.DocumentTypeId)
        </td>
    </tr>
    <tr>
        <th>
            Author Organisation
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.AuthorOrganisationId, Model.AuthorOrganisationList, "Please select an Author Organisation...")
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.AuthorOrganisationId)
        </td>
    </tr>
</table>