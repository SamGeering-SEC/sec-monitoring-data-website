@ModelType SEC_Monitoring_Data_Website.EditMeasurementViewViewModel

<table class="edit-table">
    <tr>
        <th>
            Projects related to MeasurementView
        </th>
        <th>
        </th>
    </tr>
    @For Each p In Model.AllProjects
        Dim trIdProjectMeasurementView = "trProjectMeasurementView" + p.Id.ToString
        Dim trIdProjectNonMeasurementView = "trProjectNonMeasurementView" + p.Id.ToString
        @<tr id='@trIdProjectMeasurementView'>
            <td>
                @p.FullName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Measurement View",
                                "MeasurementViewRemoveProjectRoute",
                                New With {.MeasurementViewId = Model.MeasurementView.Id,
                                          .ProjectId = p.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                      .OnComplete = "$('#" + trIdProjectMeasurementView + "').hide();$('#" + trIdProjectNonMeasurementView + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getMeasurementViewProjectIds.Contains(p.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdProjectMeasurementView + "').hide(); </script>")
        End If
    Next
</table>

<table class="edit-table">
    <tr>
        <th>
            Other Projects
        </th>
        <th>
        </th>
    </tr>
    @For Each p In Model.AllProjects
        Dim trIdProjectMeasurementView = "trProjectMeasurementView" + p.Id.ToString
        Dim trIdProjectNonMeasurementView = "trProjectNonMeasurementView" + p.Id.ToString
        @<tr id='@trIdProjectNonMeasurementView'>
            <td>
                @p.FullName
            </td>
            <td>
                @Ajax.RouteLink("Add to Measurement View",
                                "MeasurementViewAddProjectRoute",
                                New With {.MeasurementViewId = Model.MeasurementView.Id,
                                          .ProjectId = p.Id},
                                New AjaxOptions With {.HttpMethod = "PUT",
                                                      .OnComplete = "$('#" + trIdProjectMeasurementView + "').show();$('#" + trIdProjectNonMeasurementView + "').hide();"},
                                New With {.class = "sitewide-button-16 add-button-16"})
            </td>
        </tr>

        @If Model.getMeasurementViewProjectIds.Contains(p.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdProjectNonMeasurementView + "').hide(); </script>")
        End If
    Next
</table>

