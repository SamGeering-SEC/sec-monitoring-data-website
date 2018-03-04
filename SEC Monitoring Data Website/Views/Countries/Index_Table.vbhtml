@ModelType List(Of SEC_Monitoring_Data_Website.Country)

@Code
    Dim showCountryLinks = DirectCast(ViewData("ShowCountryLinks"), Boolean)
    Dim showDeleteCountryLinks = DirectCast(ViewData("ShowDeleteCountryLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                Country Name
            </th>
            <th></th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showCountryLinks Then
                        @Html.RouteLink(currentItem.CountryName,
                                "CountryDetailsRoute",
                                New With {.CountryRouteName = currentItem.getRouteName})
                    Else
                        @currentItem.CountryName
                    End If
                </td>
                <td>
                    @If showDeleteCountryLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                     "CountryDeleteByIdRoute",
                                     New With {.CountryId = currentItem.Id},
                                     New With {.class = "DeleteCountryLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>
            </tr>
        Next

    </table>

Else

    @<p>
        There are no Countries available to view.
    </p>

End If
