@ModelType SEC_Monitoring_Data_Website.Document

@Code
    Dim showDocumentTypeLink = DirectCast(ViewData("ShowDocumentTypeLinks"), Boolean)
    Dim showAuthorOrganisationLink = DirectCast(ViewData("ShowAuthorOrganisationLinks"), Boolean)
End Code

<table class="details-table">
    <tr>
        <th>
            Title
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.Title)
        </td>
    </tr>
    @If Model.HasDateRange = True Then
        @<tr>
            <th>
                Start Date Time
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.StartDateTime)
            </td>
        </tr>
        @<tr>
            <th>
                End Date Time
            </th>
            <td>
                @Html.DisplayFor(Function(model) model.EndDateTime)
            </td>
        </tr>
    End If
    <tr>
        <th>
            Document Type
        </th>
        <td>
            @If showDocumentTypeLink Then
                @Html.RouteLink(Model.DocumentType.DocumentTypeName,
                            "DocumentTypeDetailsRoute",
                            New With {.DocumentTypeRouteName = Model.DocumentType.getRouteName})
            Else
                @Model.DocumentType.DocumentTypeName
            End If
        </td>
    </tr>
    <tr>
        <th>
            Author Organisation
        </th>
        <td>
            @If showAuthorOrganisationLink Then
                @Html.RouteLink(Model.AuthorOrganisation.FullName,
                            "OrganisationDetailsRoute",
                            New With {.OrganisationRouteName = Model.AuthorOrganisation.getRouteName})
            Else
                @Model.AuthorOrganisation.FullName
            End If
        </td>
    </tr>
</table>

@Using Html.BeginRouteForm("DocumentDownloadRoute",
                           New With {.DocumentId = Model.Id})

    @Html.AntiForgeryToken

    @<p>
        @Html.JQueryUI.Button("Download")
    </p>

End Using