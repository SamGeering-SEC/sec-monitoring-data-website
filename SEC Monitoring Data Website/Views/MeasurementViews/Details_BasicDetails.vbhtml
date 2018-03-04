@ModelType SEC_Monitoring_Data_Website.MeasurementView

<h3>View</h3>

<table class="details-table">
    <tr>
        <th>
            View Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.ViewName)
        </td>
    </tr>
    <tr>
        <th>
            Display Name
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.DisplayName)
        </td>
    </tr>
    <tr>
        <th>
            Measurement Type
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MeasurementType.MeasurementTypeName)
        </td>
    </tr>
    <tr>
        <th>
            Table Type
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.MeasurementViewTableType.TableTypeName)
        </td>
    </tr>
    <tr>
        <th>
            Table Results Header
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.TableResultsHeader)
        </td>
    </tr>
    <tr>
        <th>
            Y-axis Minimum Value
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.YAxisMinValue)
        </td>
    </tr>
    <tr>
        <th>
            Y-axis Maximum Value
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.YAxisMaxValue)
        </td>
    </tr>
    <tr>
        <th>
            Y-axis Step Value
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.YAxisStepValue)
        </td>
    </tr>
</table>


@If Model.getGroups.Count > 0 Then

    @<h3>View Groups</h3>

    @Using t = Html.JQueryUI().BeginTabs()

        @For Each g In Model.getGroups
                t.Tab(g.getTabName, g.getTabHtmlName)
            Next

        @For Each g In Model.getGroups
            @Using t.BeginPanel
                @Html.Partial("Details_BD_ViewGroup", g)
            End Using
        Next

    End Using

End If



