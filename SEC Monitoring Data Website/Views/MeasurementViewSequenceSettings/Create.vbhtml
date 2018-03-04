@ModelType SEC_Monitoring_Data_Website.CreateMeasurementViewSequenceSettingViewModel

@Code
    ViewData("Title") = "Create Measurement View Sequence"
End Code

<h2>Create Measurement View Sequence</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>MeasurementViewSequenceSetting</legend>

        @Html.HiddenFor(Function(model) model.MeasurementView.Id)
        @Html.HiddenFor(Function(model) model.MeasurementViewGroup.Id)


        <table class="create-table">
            <tr>
                <th>
                    Measurement View
                </th>
                <th>
                    @Html.DisplayFor(Function(model) model.MeasurementView.ViewName)
                </th>
            </tr>
            <tr>
                <th>
                    Group Index
                </th>
                <th>
                    @Html.DisplayFor(Function(model) model.MeasurementViewGroup.GroupIndex)
                </th>
            </tr>
            <tr>
                <th>
                    Sequence Index
                </th>
                <th>
                    @Html.HiddenFor(Function(model) model.MeasurementViewSequenceSetting.SequenceIndex)
                    @Html.DisplayFor(Function(model) model.MeasurementViewSequenceSetting.SequenceIndex)
                </th>
            </tr>
            <tr>
                <th>
                    Table Header
                </th>
                <td>
                    @Html.EditorFor(Function(model) model.MeasurementViewSequenceSetting.TableHeader)
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.MeasurementViewSequenceSetting.TableHeader)
                </td>
            </tr>
            <tr>
                <th>
                    Series Name
                </th>
                <td>
                    @Html.EditorFor(Function(model) model.MeasurementViewSequenceSetting.SeriesName)
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.MeasurementViewSequenceSetting.SeriesName)
                </td>
            </tr>
            <tr>
                <th>
                    Day View Series Type
                </th>
                <td>
                    @Html.DropDownListFor(Function(model) model.DayViewSeriesTypeId, Model.DayViewSeriesTypeList,
                                          "Please select a Day View Series Type...")
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.DayViewSeriesTypeId)
                </td>
            </tr>
            <tr>
                <th>
                    Day View Series Dash Style
                </th>
                <td>
                    @Html.DropDownListFor(Function(model) model.DayViewSeriesDashStyleId, Model.DayViewSeriesDashStyleList,
                                          "Please select a Day View Series Dash Style...")
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.DayViewSeriesDashStyleId)
                </td>
            </tr>
            <tr>
                <th>
                    Week View Series Type
                </th>
                <td>
                    @Html.DropDownListFor(Function(model) model.WeekViewSeriesTypeId, Model.WeekViewSeriesTypeList,
                                          "Please select a Week View Series Type...")
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.WeekViewSeriesTypeId)
                </td>
            </tr>
            <tr>
                <th>
                    Week View Series Dash Style
                </th>
                <td>
                    @Html.DropDownListFor(Function(model) model.WeekViewSeriesDashStyleId, Model.WeekViewSeriesDashStyleList,
                                          "Please select a Week View Series Dash Style...")
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.WeekViewSeriesDashStyleId)
                </td>
            </tr>
            <tr>
                <th>
                    Month View Series Type
                </th>
                <td>
                    @Html.DropDownListFor(Function(model) model.MonthViewSeriesTypeId, Model.MonthViewSeriesTypeList,
                                          "Please select a Month View Series Type...")
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.MonthViewSeriesTypeId)
                </td>
            </tr>
            <tr>
                <th>
                    Month View Series Dash Style
                </th>
                <td>
                    @Html.DropDownListFor(Function(model) model.MonthViewSeriesDashStyleId, Model.MonthViewSeriesDashStyleList,
                                          "Please select a Month View Series Dash Style...")
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.MonthViewSeriesDashStyleId)
                </td>
            </tr>
            <tr>
                <th>
                    Series Colour
                </th>
                <td>
                    @Html.TextBoxFor(Function(model) model.MeasurementViewSequenceSetting.SeriesColour, New With {.class = "text-box single-line", .type = "color"})
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.MeasurementViewSequenceSetting.SeriesColour)
                </td>
            </tr>
            <tr>
                <th>
                    Calculation Filter
                </th>
                <td>
                    @Html.DropDownListFor(Function(model) model.CalculationFilterId, Model.CalculationFilterList, "Please select a Calculation Filter...")
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.CalculationFilterId)
                </td>
            </tr>
        </table>

        <p>
            @Html.JQueryUI.Button("Create")
        </p>
    </fieldset>
End Using

@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
