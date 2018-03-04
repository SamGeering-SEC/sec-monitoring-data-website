@ModelType SEC_Monitoring_Data_Website.EditMeasurementViewViewModel

<h3>View</h3>

<table class="edit-table">
    <tr>
        <th>
            View Name
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementView.ViewName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementView.ViewName)
        </td>
    </tr>
    <tr>
        <th>
            Display Name
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementView.DisplayName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementView.DisplayName)
        </td>
    </tr>
    <tr>
        <th>
            Measurement Type
        </th>
        <td>
            @Html.HiddenFor(Function(model) model.MeasurementTypeId)
            @Html.DisplayFor(Function(model) model.MeasurementView.MeasurementType.MeasurementTypeName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementTypeId)
        </td>
    </tr>
    <tr>
        <th>
            Measurement View Table Type
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.MeasurementViewTableTypeId, Model.MeasurementViewTableTypeList)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementViewTableTypeId)
        </td>
    </tr>
    <tr>
        <th>
            Table Results Header
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementView.TableResultsHeader)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementView.TableResultsHeader)
        </td>
    </tr>
    <tr>
        <th>
            Y-axis Minimum Value
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementView.YAxisMinValue)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementView.YAxisMinValue)
        </td>
    </tr>
    <tr>
        <th>
            Y-axis Maximum Value
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementView.YAxisMaxValue)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementView.YAxisMaxValue)
        </td>
    </tr>
    <tr>
        <th>
            Y-axis Step Value
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementView.YAxisStepValue)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementView.YAxisStepValue)
        </td>
    </tr>

</table>

@If Model.MeasurementView.getGroups.Count > 0 Then

    @<h3>View Groups</h3>

    @Using t = Html.JQueryUI().BeginTabs()

        @For Each g In Model.MeasurementView.getGroups
                t.Tab(g.getTabName, g.getTabHtmlName)
            Next

        @For Each g In Model.MeasurementView.getGroups
            @Using t.BeginPanel
                @Html.Partial("Edit_BD_ViewGroup", g)
            End Using
        Next

    End Using

End If
<table>
    <tr>
        <td>
            @Html.RouteLink("Add New Group", "MeasurementViewGroupCreateRoute",
                     New With {.MeasurementViewRouteName = Model.MeasurementView.getRouteName},
                     New With {.class = "sitewide-button-64 create-button-64"})
        </td>
    </tr>
</table>

