@ModelType SEC_Monitoring_Data_Website.EditProjectViewModel

<h3>Project Measurement Views</h3>
<table class="edit-table">
    <tr>
        <th>
            View Name
        </th>
        <th>
            Measurement Type
        </th>
        <th>
        </th>
    </tr>
    @For Each m In Model.AllMeasurementViews
        Dim trIdMeasurementViewProject = "trMeasurementViewProject" + m.Id.ToString
        Dim trIdMeasurementViewNonProject = "trMeasurementViewNonProject" + m.Id.ToString
        @<tr id='@trIdMeasurementViewProject'>
            <td>
                @m.ViewName
            </td>
            <td>
                @m.MeasurementType.MeasurementTypeName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Project",
                                "ProjectRemoveMeasurementViewRoute",
                                New With {.ProjectRouteName = Model.Project.getRouteName, .MeasurementViewId = m.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE", .OnComplete = "$('#" + trIdMeasurementViewProject + "').hide();$('#" + trIdMeasurementViewNonProject + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getProjectMeasurementViewIds.Contains(m.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdMeasurementViewProject + "').hide(); </script>")
        End If
    Next
</table>

<h3>Non-Project Measurement Views</h3>
<table class="edit-table">
    <tr>
        <th>
            View Name
        </th>
        <th>
            Measurement Type
        </th>
        <th>
        </th>
    </tr>
    @For Each m In Model.AllMeasurementViews
        Dim trIdMeasurementViewProject = "trMeasurementViewProject" + m.Id.ToString
        Dim trIdMeasurementViewNonProject = "trMeasurementViewNonProject" + m.Id.ToString
        @<tr id='@trIdMeasurementViewNonProject'>
            <td>
                @m.ViewName
            </td>
            <td>
                @m.MeasurementType.MeasurementTypeName
            </td>
            <td>
                @Ajax.RouteLink("Add to Project",
                                "ProjectAddMeasurementViewRoute",
                                New With {.ProjectRouteName = Model.Project.getRouteName, .MeasurementViewId = m.Id},
                                New AjaxOptions With {.HttpMethod = "PUT", .OnComplete = "$('#" + trIdMeasurementViewProject + "').show();$('#" + trIdMeasurementViewNonProject + "').hide();"},
                                New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getProjectMeasurementViewIds.Contains(m.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdMeasurementViewNonProject + "').hide(); </script>")
        End If
    Next
</table>










