@ModelType List(Of SEC_Monitoring_Data_Website.MeasurementView)

@Code
    Dim showMeasurementViewLinks = DirectCast(ViewData("ShowMeasurementViewLinks"), Boolean)
    Dim showDeleteMeasurementViewLinks = DirectCast(ViewData("ShowDeleteMeasurementViewLinks"), Boolean)
End Code

@If Model.Count > 0 Then

    @<table class="index-table">
        <tr>
            <th>
                View Name
            </th>
            <th>
                Display Name
            </th>
            <th>
                Measurement Type
            </th>
            <th>
                Table Type
            </th>
            <th>
                # Groups
            </th>
            <th>
                # Sequences
            </th>
            <th></th>
        </tr>

        @For Each item In Model
                Dim currentItem = item
            @<tr>
                <td>
                    @If showMeasurementViewLinks Then
                        @Html.RouteLink(currentItem.ViewName,
                                "MeasurementViewDetailsRoute",
                                New With {.MeasurementViewRouteName = currentItem.getRouteName})
                    Else
                        @currentItem.ViewName
                    End If
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) currentItem.DisplayName)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) currentItem.MeasurementType.MeasurementTypeName)
                </td>
                <td>
                    @Html.DisplayFor(Function(modelItem) currentItem.MeasurementViewTableType.TableTypeName)
                </td>
                <td style="text-align:center">
                    @currentItem.getGroups.Count
                </td>
                <td style="text-align:center">
                    @currentItem.getSequenceSettings.Count
                </td>
                <td>
                    @If showDeleteMeasurementViewLinks Then
                        @If currentItem.canBeDeleted = True Then
                            @Html.RouteLink("Delete",
                                     "MeasurementViewDeleteByIdRoute",
                                     New With {.MeasurementViewId = currentItem.Id},
                                     New With {.class = "DeleteMeasurementViewLink sitewide-button-16 delete-button-16"})
                        End If
                    End If
                </td>
            </tr>
        Next

    </table>

Else
    @<p>
        There are no Measurement Views available to view.
    </p>
End If
