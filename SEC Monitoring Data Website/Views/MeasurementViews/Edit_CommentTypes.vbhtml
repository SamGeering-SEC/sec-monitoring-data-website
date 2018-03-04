@ModelType SEC_Monitoring_Data_Website.EditMeasurementViewViewModel



<table class="edit-table">
    <tr>
        <th>
            Excluding Comment Types
        </th>
        <th>
        </th>
    </tr>
    @For Each c In Model.AllCommentTypes
        Dim trIdCommentTypeMeasurementView = "trCommentTypeMeasurementView" + c.Id.ToString
        Dim trIdCommentTypeNonMeasurementView = "trCommentTypeNonMeasurementView" + c.Id.ToString
        @<tr id='@trIdCommentTypeMeasurementView'>
            <td>
                @c.CommentTypeName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Measurement View",
                                "MeasurementViewRemoveCommentTypeRoute",
                                New With {.MeasurementViewShortName = Model.MeasurementView.getRouteName, .CommentTypeId = c.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE", .OnComplete = "$('#" + trIdCommentTypeMeasurementView + "').hide();$('#" + trIdCommentTypeNonMeasurementView + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getMeasurementViewExcludingMeasurementCommentTypeIds.Contains(c.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdCommentTypeMeasurementView + "').hide(); </script>")
        End If
    Next
</table>




<table class="edit-table">
    <tr>
        <th>
            Non-Excluding Comment Types
        </th>
        <th>
        </th>
    </tr>
    @For Each c In Model.AllCommentTypes
        Dim trIdCommentTypeMeasurementView = "trCommentTypeMeasurementView" + c.Id.ToString
        Dim trIdCommentTypeNonMeasurementView = "trCommentTypeNonMeasurementView" + c.Id.ToString
        @<tr id='@trIdCommentTypeNonMeasurementView'>
            <td>
                @c.CommentTypeName
            </td>
            <td>
                @Ajax.RouteLink("Add to Measurement View",
                                "MeasurementViewAddCommentTypeRoute",
                                New With {.MeasurementViewShortName = Model.MeasurementView.getRouteName, .CommentTypeId = c.Id},
                                New AjaxOptions With {.HttpMethod = "PUT", .OnComplete = "$('#" + trIdCommentTypeMeasurementView + "').show();$('#" + trIdCommentTypeNonMeasurementView + "').hide();"},
                                New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getMeasurementViewExcludingMeasurementCommentTypeIds.Contains(c.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdCommentTypeNonMeasurementView + "').hide(); </script>")
        End If
    Next
</table>


