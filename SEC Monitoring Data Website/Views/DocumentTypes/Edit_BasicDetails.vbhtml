@ModelType SEC_Monitoring_Data_Website.EditDocumentTypeViewModel

<table class="edit-table">
    <tr>
        <th>
            Document Type Name
        </th>
        <td>
            @Html.EditorFor(Function(model) model.DocumentType.DocumentTypeName)
        </td>
        <td>
            @Html.ValidationMessageFor(Function(model) model.DocumentType.DocumentTypeName)
        </td>
    </tr>
</table>