@ModelType SEC_Monitoring_Data_Website.EditMeasurementCommentTypeViewModel

<table class="edit-table">
    <tr>
        <th>
            Comment Type Name
        </th>
        <td>
            @Html.EditorFor(Function(model) model.MeasurementCommentType.CommentTypeName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.MeasurementCommentType.CommentTypeName)
        </td>
    </tr>
</table>