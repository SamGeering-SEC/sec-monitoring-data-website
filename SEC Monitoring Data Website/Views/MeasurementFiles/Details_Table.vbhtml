@ModelType MeasurementFileDetailsViewModel

@Code
    Dim showDeleteMeasurementLinks = DirectCast(ViewData("ShowDeleteMeasurementLinks"), Boolean)
End Code

@Html.HiddenFor(Function(model) model.NumPages)
@Html.HiddenFor(Function(model) model.PageNumber)

<table>
    <tr>
        <td>
            Page @Model.PageNumber of @Model.NumPages
        </td>
        <td>
            @Html.JQueryUI.Button("First", New With {.onclick = "firstPage()"}).Disabled(If(Model.PageNumber = 1, True, False))
        </td>
        <td>
            @Html.JQueryUI.Button("Previous", New With {.onclick = "previousPage()"}).Disabled(If(Model.PageNumber = 1, True, False))
        </td>
        <td>
            @Html.JQueryUI.Button("Next", New With {.onclick = "nextPage()"}).Disabled(If(Model.PageNumber = Model.NumPages, True, False))
        </td>
        <td>
            @Html.JQueryUI.Button("Last", New With {.onclick = "lastPage()"}).Disabled(If(Model.PageNumber = Model.NumPages, True, False))
        </td>
        <td>
            @Html.JQueryUI.SliderFor(Function(model) model.PageSize).Min(10).Max(100).Step(10).OnStop("changePageSize")
        </td>
        <td>
            Sort by: @Html.DropDownListFor(Function(model) model.SortBy, Model.SortByList, New With {.onchange = "changeSorting();"})
        </td>
    </tr>
</table>

<div id="measurementsTable">
    <table class="measurements-file-table">
        <thead>
            <tr>
                <th style="width:25%">
                    Start Date Time
                </th>
                <th style="width:12%">
                    Duration
                </th>
                <th style="width:12%">
                    Level
                </th>
                <th style="width:12%">
                    Overload
                </th>
                <th style="width:12%">
                    Underload
                </th>
                <th style="width:22%">
                    Metric
                </th>
                <th style="width:5%"></th>
            </tr>
        </thead>
        <tbody>
            @For Each measurement In Model.Measurements
                Dim m = measurement
                @<tr>
                    <td style="width:25%">
                        @m.StartDateTime
                    </td>
                    <td style="width:12%">
                        @Format(Date.FromOADate(m.Duration), "HH:mm:ss")
                    </td>
                    <td style="width:12%">
                        @Math.Round(
                            m.Level, m.MeasurementMetric.RoundingDecimalPlaces, MidpointRounding.AwayFromZero
                        ).ToString(
                            "F" + m.getMetric.RoundingDecimalPlaces.ToString
                        )
                    </td>
                    <td style="width:12%">
                        @m.Overload
                    </td>
                    <td style="width:12%">
                        @m.Underload
                    </td>
                    <td style="width:22%">
                        @m.MeasurementMetric.MetricName
                    </td>
                    <td style="width:5%">
                        @If showDeleteMeasurementLinks Then
                            @Html.RouteLink(
                                "Delete",
                                "MeasurementDeleteByIdRoute",
                                New With {.MeasurementId = m.Id},
                                New With {.class = "DeleteMeasurementLink sitewide-button-16 delete-button-16"}
                            )
                        End If
                    </td>
                </tr>
            Next
        </tbody>
    </table>
</div>

<div id="divLoadingElement" style="display:none">
    <p>
        <img src="~/Images/loading.gif" />
    </p>
</div>