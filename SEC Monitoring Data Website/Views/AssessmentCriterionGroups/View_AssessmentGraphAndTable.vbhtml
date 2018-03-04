@ModelType ViewAssessmentDataViewModel

@Code
    Dim table = Model.Table
End Code

@If Model.AssessmentCriterionGroup.ShowGraph Then
    @Model.Chart
End If

<table class="assessment-table">
    <thead>
        @For Each row In table.Header.Rows
            @<tr>
                @For Each cell In row.Cells
                    @<th colspan="@cell.ColSpan" rowspan="@cell.RowSpan" style="width:100px">
    @Html.Raw(cell.Text)
</th>
            Next
            </tr>
        Next
    </thead>
    <tbody>
        @For Each row In table.Body.Rows
            @<tr>
                @*Date*@
                @For c = table.FirstDateColumnIndex() To table.LastDateColumnIndex()
                    @Code
                    Dim cell = row.Cells(c)
                    End Code
                    @<th colspan="@cell.ColSpan" rowspan="@cell.RowSpan">
                        @cell.Text
                    </th>
            Next
                @*Assessed Levels*@
                @For c = table.FirstLevelColumnIndex() To table.LastLevelColumnIndex()
                    @Code
                    Dim cell = row.Cells(c)
                    End Code
                    @<td colspan="@cell.ColSpan" rowspan="@cell.RowSpan">
                        @cell.Text
                    </td>
            Next
                @*Triggers*@
                @For c = table.FirstTriggerColumnIndex To table.LastTriggerColumnIndex
                    @Code
                    Dim cell = row.Cells(c)
                    End Code
                    @<td colspan="@cell.ColSpan" rowspan="@cell.RowSpan">
                        @cell.Text
                    </td>
            Next
                @*Daily Total*@
                @If Model.AssessmentCriterionGroup.SumExceedancesAcrossCriteria Then
                    @Code
                    Dim cell = row.Cells(table.DailySumColumnIndex)
                    End Code
                    @<th colspan="@cell.ColSpan" rowspan="@cell.RowSpan">
                        @row.Cells(table.DailySumColumnIndex).Text
                    </th>
            End If
            </tr>
        Next
    </tbody>
    <tfoot>
        @For Each row In table.Footer.Rows
            @<tr>
                @For Each cell In row.Cells
                    @<th colspan="@cell.ColSpan" rowspan="@cell.RowSpan">
                        @cell.Text
                    </th>
                Next
            </tr>
        Next
    </tfoot>
</table>

@Using Html.BeginRouteForm("AssessmentTableDownloadRoute",
                           New With {.MonitorLocationId = Model.MonitorLocation.Id,
                                     .AssessmentCriterionGroupId = Model.AssessmentCriterionGroup.Id,
                                     .strAssessmentDate = Format(Model.AssessmentDate, "dd-MMM-yyyy"),
                                     .StartOrEnd = Model.StartOrEnd})

    @Html.AntiForgeryToken
    @<p>
        @Html.JQueryUI.Button("Download Table")
    </p>

        End Using