@ModelType SEC_Monitoring_Data_Website.EditStandardWeeklyWorkingHoursViewModel

<table class="edit-table">
    <tr>
        <th>
            Avaiable Views
        </th>
        <th>
            Measurement Type
        </th>
        <th>
        </th>
    </tr>
    @For Each m In Model.AllMeasurementViews
        Dim trIdMeasurementViewStandardWeeklyWorkingHours = "trMeasurementViewStandardWeeklyWorkingHours" + m.Id.ToString
        Dim trIdMeasurementViewNonStandardWeeklyWorkingHours = "trMeasurementViewNonStandardWeeklyWorkingHours" + m.Id.ToString
        @<tr id='@trIdMeasurementViewStandardWeeklyWorkingHours'>
            <td>
                @m.ViewName
            </td>
             <td>
                 @m.MeasurementType.MeasurementTypeName
             </td>
            <td>
                @Ajax.RouteLink("Remove from Available Views",
                                "StandardWeeklyWorkingHoursRemoveMeasurementViewRoute",
                                New With {.ProjectRouteName = Model.StandardWeeklyWorkingHours.Project.getRouteName,
                                          .MeasurementViewId = m.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                      .OnComplete = "$('#" + trIdMeasurementViewStandardWeeklyWorkingHours + "').hide();$('#" + trIdMeasurementViewNonStandardWeeklyWorkingHours + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getStandardWeeklyWorkingHoursMeasurementViewIds.Contains(m.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdMeasurementViewStandardWeeklyWorkingHours + "').hide(); </script>")
        End If
    Next
</table>

<table class="edit-table">
    <tr>
        <th>
            Other Views
        </th>
        <th>
            Measurement Type
        </th>
        <th>
        </th>
    </tr>
    @For Each m In Model.AllMeasurementViews
        Dim trIdMeasurementViewStandardWeeklyWorkingHours = "trMeasurementViewStandardWeeklyWorkingHours" + m.Id.ToString
        Dim trIdMeasurementViewNonStandardWeeklyWorkingHours = "trMeasurementViewNonStandardWeeklyWorkingHours" + m.Id.ToString
        @<tr id='@trIdMeasurementViewNonStandardWeeklyWorkingHours'>
            <td>
                @m.ViewName
            </td>
             <td>
                 @m.MeasurementType.MeasurementTypeName
             </td>
            <td>
                @Ajax.RouteLink("Add to Available Views",
                                "StandardWeeklyWorkingHoursAddMeasurementViewRoute",
                                New With {.ProjectRouteName = Model.StandardWeeklyWorkingHours.Project.getRouteName,
                                                                                              .MeasurementViewId = m.Id},
                                New AjaxOptions With {.HttpMethod = "PUT",
                                                      .OnComplete = "$('#" + trIdMeasurementViewStandardWeeklyWorkingHours + "').show();$('#" + trIdMeasurementViewNonStandardWeeklyWorkingHours + "').hide();"},
                                New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getStandardWeeklyWorkingHoursMeasurementViewIds.Contains(m.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdMeasurementViewNonStandardWeeklyWorkingHours + "').hide(); </script>")
        End If
        
    Next
        
</table>
