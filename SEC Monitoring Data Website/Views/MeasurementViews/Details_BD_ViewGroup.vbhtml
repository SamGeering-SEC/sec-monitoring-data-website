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

@If Model.MeasurementViewSequenceSettings.Count > 0 Then

  @<h4>Sequence Settings</h4>
    @Using t = Html.JQueryUI().BeginTabs()

        @For Each s In Model.getSequenceSettings
                t.Tab(s.getTabName, s.getTabHtmlName)
            Next

        @For Each s In Model.getSequenceSettings
            @Using t.BeginPanel
                @Html.Partial("Details_BD_VG_SequenceSetting", s)
            End Using
        Next

    End Using

End If

