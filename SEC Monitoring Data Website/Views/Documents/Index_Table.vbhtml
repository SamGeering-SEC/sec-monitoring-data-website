@ModelType List(Of SEC_Monitoring_Data_Website.Document)

@Code
    Dim showDocumentLinks = DirectCast(ViewData("ShowDocumentLinks"), Boolean)
    Dim showDocumentTypeLinks = DirectCast(ViewData("ShowDocumentTypeLinks"), Boolean)
    Dim showAuthorOrganisationLinks = DirectCast(ViewData("ShowAuthorOrganisationLinks"), Boolean)
    Dim showDeleteDocumentLinks = DirectCast(ViewData("ShowDeleteDocumentLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                Title
            </th>
            <th>
                Start
            </th>
            <th>
                End
            </th>
            <th>
                Type
            </th>
            <th>
                Organisation
            </th>
            <th></th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showDocumentLinks Then
                        @Html.RouteLink(
                            currentItem.Title, "DocumentDetailsRoute",
                            New With {.DocumentFileName = currentItem.getFileName,
                                      .DocumentUploadDate = currentItem.getUploadDate,
                                      .DocumentUploadTime = currentItem.getUploadTime}
                        )
                    Else
                        @currentItem.Title
                    End If
                </td>
                <td>
                    @If currentItem.HasDateRange = True Then
                        @Format(currentItem.StartDateTime, "ddd dd-MMM-yy HH:mm")
                    Else
                        @Html.Raw("N/A")
                    End If
                </td>
                <td>
                    @If currentItem.HasDateRange = True Then
                        @Format(currentItem.EndDateTime, "ddd dd-MMM-yy HH:mm")
                    Else
                        @Html.Raw("N/A")
                    End If
                </td>
                <td>
                    @If showDocumentTypeLinks Then
                        @Html.RouteLink(currentItem.DocumentType.DocumentTypeName,
                                    "DocumentTypeDetailsRoute",
                                    New With {.DocumentTypeRouteName = currentItem.DocumentType.getRouteName})
                    Else
                        @currentItem.DocumentType.DocumentTypeName
                    End If
                </td>
                <td>
                    @If showAuthorOrganisationLinks = True Then
                        @Html.RouteLink(
                            currentItem.AuthorOrganisation.ShortName, "OrganisationDetailsRoute",
                            New With {.OrganisationRouteName = currentItem.AuthorOrganisation.getRouteName}
                        )
                    Else
                        @currentItem.AuthorOrganisation.ShortName
                    End If
                </td>
            </tr>
        Next
    </table>

Else
    @<p>
        There are no Documents available to view.
    </p>
End If
