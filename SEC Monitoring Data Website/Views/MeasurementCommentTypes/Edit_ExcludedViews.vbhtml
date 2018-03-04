@ModelType SEC_Monitoring_Data_Website.EditMeasurementCommentTypeViewModel

<table class="edit-table">
    <tr>
        <th>
            Excluded Views
        </th>
        <th>
        </th>
    </tr>
    @For Each e In Model.AllExcludedMeasurementViews
        Dim trIdExcludedMeasurementViewMeasurementCommentType = "trExcludedMeasurementViewMeasurementCommentType" + e.Id.ToString
        Dim trIdExcludedMeasurementViewNonMeasurementCommentType = "trExcludedMeasurementViewNonMeasurementCommentType" + e.Id.ToString
        @<tr id='@trIdExcludedMeasurementViewMeasurementCommentType'>
            <td>
                @e.ViewName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Measurement Comment Type",
                                "MeasurementCommentTypeRemoveExcludedMeasurementViewRoute",
                                New With {.MeasurementCommentTypeShortName = Model.MeasurementCommentType.getRouteName,
                                          .ExcludedMeasurementViewId = e.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                      .OnComplete = "$('#" + trIdExcludedMeasurementViewMeasurementCommentType + "').hide();$('#" + trIdExcludedMeasurementViewNonMeasurementCommentType + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getMeasurementCommentTypeExcludedMeasurementViewIds.Contains(e.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdExcludedMeasurementViewMeasurementCommentType + "').hide(); </script>")
        End If
    Next
</table>

<table class="edit-table">
    <tr>
        <th>
            Other Views
        </th>
        <th>
        </th>
    </tr>
    @For Each e In Model.AllExcludedMeasurementViews
        Dim trIdExcludedMeasurementViewMeasurementCommentType = "trExcludedMeasurementViewMeasurementCommentType" + e.Id.ToString
        Dim trIdExcludedMeasurementViewNonMeasurementCommentType = "trExcludedMeasurementViewNonMeasurementCommentType" + e.Id.ToString
        @<tr id='@trIdExcludedMeasurementViewNonMeasurementCommentType'>
            <td>
                @e.ViewName
            </td>
            <td>
                @Ajax.RouteLink("Add to Measurement Comment Type",
                                "MeasurementCommentTypeAddExcludedMeasurementViewRoute",
                                New With {.MeasurementCommentTypeShortName = Model.MeasurementCommentType.getRouteName,
                                          .ExcludedMeasurementViewId = e.Id},
                                New AjaxOptions With {.HttpMethod = "PUT",
                                                      .OnComplete = "$('#" + trIdExcludedMeasurementViewMeasurementCommentType + "').show();$('#" + trIdExcludedMeasurementViewNonMeasurementCommentType + "').hide();"},
                                New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getMeasurementCommentTypeExcludedMeasurementViewIds.Contains(e.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdExcludedMeasurementViewNonMeasurementCommentType + "').hide(); </script>")
        End If
    Next
</table>

