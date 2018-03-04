@ModelType SEC_Monitoring_Data_Website.CreateAssessmentCriterionPopUpViewModel

<table class="create-table">
    <tr>
        <th>
            Calculation Filter
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.CalculationFilterId,
                                          Model.CalculationFilterList,
                                          "Please select a Calculation Filter...")
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.CalculationFilterId)
        </td>
    </tr>
    <tr>
        <th>
            Threshold Range Lower Bound
        </th>
        <td>
            @Html.EditorFor(Function(model) model.ThresholdRangeLower)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.ThresholdRangeLower)
        </td>
    </tr>
    <tr>
        <th>
            Threshold Range Upper Bound
        </th>
        <td>
            @Html.EditorFor(Function(model) model.ThresholdRangeUpper)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.ThresholdRangeUpper)
        </td>
    </tr>
    <tr>
        <th>
            Threshold Range Lower Bound Type
        </th>
        <td>
            @Html.DropDownListFor(Function(model) model.ThresholdTypeId,
                                          Model.ThresholdTypeList,
                                          "Please select a Threshold Type...")
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.ThresholdTypeId)
        </td>
    </tr>
    <tr>
        <th>
            # Rounding Decimal Places
        </th>
        <td>
            @Html.EditorFor(Function(model) model.RoundingDecimalPlaces)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.RoundingDecimalPlaces)
        </td>
    </tr>
</table>