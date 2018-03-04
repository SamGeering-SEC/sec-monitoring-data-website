@ModelType SEC_Monitoring_Data_Website.Organisation

@Code
    Dim showAuthoredDocumentLinks = DirectCast(ViewData("ShowAuthoredDocumentLinks"), Boolean)
    Dim showAuthoredDocumentTypeLinks = DirectCast(ViewData("ShowAuthoredDocumentTypeLinks"), Boolean)
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
    @For Each ad In Model.AuthoredDocuments.OrderBy(Function(d) d.Title)
        @<tr>
            <td>
                @If showAuthoredDocumentLinks Then
                    @Html.RouteLink(ad.Title,
                                "DocumentDetailsRoute",
                                New With {.DocumentFileName = ad.getFileName,
                                          .DocumentUploadDate = ad.getUploadDate,
                                          .DocumentUploadTime = ad.getUploadTime})
                Else
                    @ad.Title
                End If
            </td>
            <td>
                @If showAuthoredDocumentTypeLinks Then
                    @Html.RouteLink(ad.DocumentType.DocumentTypeName,
                                "DocumentTypeDetailsRoute",
                                New With {.DocumentTypeRouteName = ad.DocumentType.getRouteName})
                Else
                    @ad.DocumentType.DocumentTypeName
                End If
            </td>
        </tr>
    Next

</table>