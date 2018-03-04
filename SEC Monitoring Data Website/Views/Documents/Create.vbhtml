@ModelType SEC_Monitoring_Data_Website.CreateDocumentViewModel

<style type="text/css">
    label {
        display: inline-block !important;
        padding: 5px !important;
        font-style: normal;
    }
</style>

@section Scripts
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

        function hideIrrelevantItemsInTable(t) {

            var searchText = $('#searchTextBox').val().toLowerCase();
            var rows = t.getElementsByTagName('tr');
            for (r = 0; r < rows.length - 1; r++) { // an extra row is created for some reason
                var labelText = rows[r].getElementsByTagName('label')[0].textContent.toLowerCase();
                var isChecked = rows[r].getElementsByTagName('input')[0].checked;
                if (isChecked == true || (searchText != '' && labelText.search(searchText) != -1)) {
                    rows[r].style.display = '';
                }
                else {
                    rows[r].style.display = 'none';
                }
            }
        };

        function hideIrrelevantItems() {

            var tProjects = document.getElementById('projects-list');
            var tMonitors = document.getElementById('monitors-list');
            var tMonitorLocations = document.getElementById('monitor-locations-list');

            hideIrrelevantItemsInTable(tProjects);
            hideIrrelevantItemsInTable(tMonitors);
            hideIrrelevantItemsInTable(tMonitorLocations);

        };



        $(document).ready(function () {

            ShowHideDateTimeRows();
            $("#Document_HasDateRange").click(function () { ShowHideDateTimeRows() });
            hideIrrelevantItems();

        });



    </script>
End Section


@Code
    ViewData("Title") = "Create new Document"
End Code

<h2>Create new Document</h2>

@Using Html.BeginForm(Nothing, Nothing, FormMethod.Post, New With {.enctype = "multipart/form-data"})
    
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)
    
    @Html.HiddenFor(Function(model) model.ReturnToRouteName)
    @Html.HiddenFor(Function(model) model.ReturnToRouteValues)

    @<table class="create-table">
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
        <tr>
            <th>
                Search for related Items
            </th>
            <td>
                <input type="text" id="searchTextBox" onkeyup="hideIrrelevantItems();" />
            </td>
        </tr>
        <tr>
            <th>
                Related Projects:
            </th>
            <td id="projects-list">
                @Code
                    Dim htmlListInfo1 = New HtmlListInfo(HtmlTag.table, 1, Nothing, TextLayout.Default, TemplateIsUsed.No)
                @Html.CheckBoxListFor(Function(model) model.PostedProjects.ProjectIds,
                      Function(model) model.AvailableProjects,
                      Function(Project) Project.Id,
                      Function(Project) Project.Name,
                      Function(model) model.SelectedProjects,
                      htmlListInfo1)
                End Code
            </td>
        </tr>
        <tr>
            <th>
                Related Monitor Locations:
            </th>
            <td id="monitor-locations-list">
                @Code
                    Dim htmlListInfo3 = New HtmlListInfo(HtmlTag.table, 1, Nothing, TextLayout.Default, TemplateIsUsed.No)
                @Html.CheckBoxListFor(Function(model) model.PostedMonitorLocations.MonitorLocationIds,
                      Function(model) model.AvailableMonitorLocations,
                      Function(MonitorLocation) MonitorLocation.Id,
                      Function(MonitorLocation) MonitorLocation.Name,
                      Function(model) model.SelectedMonitorLocations,
                      htmlListInfo3)
                End Code
            </td>
        </tr>
        <tr>
            <th>
                Related Monitors:
            </th>
            <td id="monitors-list">
                @Code
                    Dim htmlListInfo2 = New HtmlListInfo(HtmlTag.table, 1, Nothing, TextLayout.Default, TemplateIsUsed.No)
                    @Html.CheckBoxListFor(Function(model) model.PostedMonitors.MonitorIds,
                      Function(model) model.AvailableMonitors,
                      Function(Monitor) Monitor.Id,
                      Function(Monitor) Monitor.Name,
                      Function(model) model.SelectedMonitors,
                      htmlListInfo2)
                End Code
            </td>
        </tr>

        <tr>
            <th>
                Select File:
            </th>
            <td>
                <input type="file" name="UploadFile" id="UploadFile" />
            </td>
            <td></td>
        </tr>
    </table>



    @<p>
        @Html.JQueryUI.Button("Create")
    </p>

        End Using

