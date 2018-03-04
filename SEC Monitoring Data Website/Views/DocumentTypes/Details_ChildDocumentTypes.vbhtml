@ModelType SEC_Monitoring_Data_Website.DocumentType

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Document Type Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each cdt In Model.ChildDocumentTypes.OrderBy(Function(dt) dt.DocumentTypeName)
        @<tr>
            <td>
                @cdt.DocumentTypeName
            </td>
        </tr>
    Next

</table>