@ModelType SEC_Monitoring_Data_Website.DocumentType

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Document Type Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each pdt In Model.ParentDocumentTypes.OrderBy(Function(dt) dt.DocumentTypeName)
        @<tr>
            <td>
                @pdt.DocumentTypeName
            </td>
        </tr>
    Next

</table>