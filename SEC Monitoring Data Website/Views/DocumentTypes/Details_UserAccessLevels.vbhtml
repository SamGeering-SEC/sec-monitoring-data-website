@ModelType SEC_Monitoring_Data_Website.DocumentType

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Access Level Name
        </th>
    </tr>

    @*Item Rows*@
    @For Each aual In Model.AllowedUserAccessLevels.OrderBy(Function(al) al.AccessLevelName)
        @<tr>
            <td>
                @aual.AccessLevelName
            </td>
        </tr>
    Next

</table>