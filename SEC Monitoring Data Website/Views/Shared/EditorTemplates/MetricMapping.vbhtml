@ModelType SEC_Monitoring_Data_Website.MetricMapping

<tr>
    <th>
        @Html.HiddenFor(Function(model) model.MappingName)
        @Model.MappingName
    </th>
    <td>
        @Html.DropDownListFor(Function(model) model.MetricId, Model.MetricList, "< No Mapping >")
    </td>
</tr>
