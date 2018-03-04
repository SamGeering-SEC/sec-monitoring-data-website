@ModelType List(Of SEC_Monitoring_Data_Website.DocumentType)

@Code
    Dim showDocumentTypeLinks = DirectCast(ViewData("ShowDocumentTypeLinks"), Boolean)
    Dim showDeleteDocumentTypeLinks = DirectCast(ViewData("ShowDeleteDocumentTypeLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showDocumentTypeLinks Then
                        @Html.RouteLink(currentItem.DocumentTypeName,
                                    "DocumentTypeDetailsRoute",
                                    New With {.DocumentTypeRouteName = currentItem.getRouteName})
                    Else
                        @currentItem.DocumentTypeName
                    End If
                </td>
                <td>
                    @If showDeleteDocumentTypeLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                 "DocumentTypeDeleteByIdRoute",
                                 New With {.DocumentTypeId = currentItem.Id},
                                 New With {.class = "DeleteDocumentTypeLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>
            </tr>
        Next

    </table>

Else
    @<p>
        There are no Document Types available to view.
    </p>
End If
