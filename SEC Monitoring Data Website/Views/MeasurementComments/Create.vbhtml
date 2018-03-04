@ModelType SEC_Monitoring_Data_Website.CreateMeasurementCommentViewModel

<style type="text/css">
    label {
        display: inline-block !important;
        padding: 5px !important;
    }
</style>



@Using Html.BeginForm()
@Html.AntiForgeryToken
    
    @Html.HiddenFor(Function(model) model.MonitorLocationId)

    @<table class="create-table">
        <tr>
            <th>
                Comment Text
            </th>
            <td>
                @Html.EditorFor(Function(model) model.MeasurementComment.CommentText)
            </td>
            <td>
                @Html.ValidationMessageFor(Function(model) model.MeasurementComment.CommentText)
            </td>
        </tr>
        <tr>
            <th>
                Comment Type
            </th>
            <td>
                @Html.DropDownListFor(Function(model) model.CommentTypeId, Model.CommentTypeList, "Please select a Comment Type...")
            </td>
            <td>
                @Html.ValidationMessageFor(Function(model) model.CommentTypeId)
            </td>
        </tr>
         <tr>
             <th>
                 Start Date and Time
             </th>
             <td>
                 @Html.JQueryUI.DatepickerFor(Function(model) model.CommentStartDate)
                 @Html.EditorFor(Function(model) model.CommentStartTime)
             </td>
             <td>
                 @Html.ValidationMessageFor(Function(model) model.CommentStartDate)
             </td>
         </tr>
         <tr>
             <th>
                 End Date and Time
             </th>
             <td>
                 @Html.JQueryUI.DatepickerFor(Function(model) model.CommentEndDate)
                 @Html.EditorFor(Function(model) model.CommentEndTime)
             </td>
             <td>
                 @Html.ValidationMessageFor(Function(model) model.CommentEndDate)
             </td>
         </tr>
        <tr>
            <th>
                Measurement Metrics
            </th>
            <td>
                @Code
                    Dim htmlListInfo = New HtmlListInfo(HtmlTag.table, 1, Nothing, TextLayout.Default, TemplateIsUsed.No)
                    @Html.CheckBoxListFor(Function(model) model.PostedMeasurementMetrics.MeasurementMetricIds,
                                          Function(model) model.AvailableMeasurementMetrics,
                                          Function(metric) metric.Id,
                                          Function(metric) metric.Name,
                                          Function(model) model.SelectedMeasurementMetrics,
                                          htmlListInfo)
                End Code


            </td>
        </tr>
    </table>

    @Html.JQueryUI.Button("Add Comment")

    End Using