@ModelType SEC_Monitoring_Data_Website.EditProjectViewModel

@Code
    Dim showCreateMonitorLocationLink = DirectCast(ViewData("ShowCreateMonitorLocationLink"), Boolean)
End Code

<h3>Project Monitor Locations</h3>

<table class="edit-table">

    @*Item Rows*@
    @For Each ml In Model.Project.MonitorLocations
        @<tr>
            <td>
                @ml.MonitorLocationName
            </td>
        </tr>
    Next

    <tr>
        <td>
            @If showCreateMonitorLocationLink Then
                @Html.RouteLink("Add new Monitor Location", "MonitorLocationCreateRoute",
                                New With {.ProjectRouteName = Model.Project.getRouteName},
                                New With {.class = "sitewide-button-32 create-button-32"})
            End If
        </td>
    </tr>

</table>

