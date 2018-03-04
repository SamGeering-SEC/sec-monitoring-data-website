@ModelType SEC_Monitoring_Data_Website.MeasurementViewGroup

<h4>Group</h4>
<table class="details-table">
    <tr>
        <th>
            Main Header
        </th>
        <td>
            @Model.MainHeader
        </td>
    </tr>
    <tr>
        <th>
            Sub Header
        </th>
        <td>
            @Model.SubHeader
        </td>
    </tr>
</table>

<table>
    <tr>
        <td>
            @Html.RouteLink("Edit Group", "MeasurementViewGroupEditRoute",
                     New With {.MeasurementViewRouteName = Model.MeasurementView.getRouteName,
                               .GroupIndex = Model.GroupIndex},
                     New With {.class = "sitewide-button-32 edit-button-32"})
            @Html.RouteLink("Delete Group",
                     "MeasurementViewGroupDeleteRoute",
                     New With {.MeasurementViewRouteName = Model.MeasurementView.getRouteName, .GroupIndex = Model.GroupIndex},
                     New With {.class = "sitewide-button-32 delete-button-32 DeleteGroupLink"})
        </td>
    </tr>
</table>

@If Model.MeasurementViewSequenceSettings.Count > 0 Then

    @<h4>Sequence Settings</h4>
    @Using t = Html.JQueryUI().BeginTabs()

        @For Each s In Model.getSequenceSettings
                t.Tab(s.getTabName, s.getTabHtmlName)
            Next

        @For Each s In Model.getSequenceSettings
            @Using t.BeginPanel
                @Html.Partial("Edit_BD_VG_SequenceSetting", s)
            End Using
        Next

    End Using

End If
<table>
    <tr>
        <td>
            @Html.RouteLink("Add New Sequence", "MeasurementViewSequenceSettingCreateRoute",
                     New With {.MeasurementViewRouteName = Model.MeasurementView.getRouteName, .GroupIndex = Model.GroupIndex},
                     New With {.class = "sitewide-button-32 create-button-32"})
        </td>
    </tr>
</table>

