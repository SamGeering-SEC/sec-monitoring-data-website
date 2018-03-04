@ModelType SEC_Monitoring_Data_Website.DocumentType

@Code
    Dim showDocumentLinks = DirectCast(ViewData("ShowDocumentLinks"), Boolean)
End Code

<table>

    @*Header Row*@
    <tr>
        <th>
            Title
        </th>
    </tr>

    @*Item Rows*@
    @For Each d In Model.Documents
        @<tr>
            <td>
                @If showDocumentLinks Then
                    @Html.RouteLink(d.Title,
                                    "DocumentDetailsRoute",
                                    New With {.DocumentFileName = d.getFileName,
                                              .DocumentUploadDate = d.getUploadDate,
                                              .DocumentUploadTime = d.getUploadTime})
                Else
                    @d.Title
                End If
            </td>
        </tr>
    Next

</table>