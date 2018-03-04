@ModelType SEC_Monitoring_Data_Website.Contact

@Code
    Dim showExcludedDocumentLinks = DirectCast(ViewData("ShowExcludedDocumentLinks"), Boolean)
End Code

<table class="details-table">

    @*Header Row*@
    <tr>
        <th>
            Title
        </th>
        <th>
            Document Type
        </th>
    </tr>

    @*Item Rows*@
    @For Each ed In Model.ExcludedDocuments
        @<tr>
            <td>
                @If showExcludedDocumentLinks Then
                    @Html.RouteLink(ed.Title,
                                "DocumentDetailsRoute",
                                New With {.DocumentFileName = ed.getFileName,
                                          .DocumentUploadDate = ed.getUploadDate,
                                          .DocumentUploadTime = ed.getUploadTime})
                Else
                    @ed.Title
                End If
            </td>
            <td>
                @ed.DocumentType.DocumentTypeName
            </td>
        </tr>
    Next

</table>