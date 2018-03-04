@ModelType SEC_Monitoring_Data_Website.EditCalculationFilterViewModel

<table class="edit-table">
    <tr>
        <th>
            Applies on ...
        </th>
        <th>
        </th>
    </tr>
    @For Each a In Model.AllApplicableDaysOfWeek.OrderBy(Function(DoW) DoW.Id)
        Dim trIdApplicableDayOfWeekCalculationFilter = "trApplicableDayOfWeekCalculationFilter" + a.Id.ToString
        Dim trIdApplicableDayOfWeekNonCalculationFilter = "trApplicableDayOfWeekNonCalculationFilter" + a.Id.ToString
        @<tr id='@trIdApplicableDayOfWeekCalculationFilter'>
            <td>
                @a.DayName
            </td>
            <td>
                @Ajax.RouteLink("Remove from Calculation Filter",
                                "CalculationFilterRemoveApplicableDayOfWeekRoute",
                                New With {.CalculationFilterShortName = Model.CalculationFilter.getRouteName,
                                          .ApplicableDayOfWeekId = a.Id},
                                New AjaxOptions With {.HttpMethod = "DELETE",
                                                      .OnComplete = "$('#" + trIdApplicableDayOfWeekCalculationFilter + "').hide();$('#" + trIdApplicableDayOfWeekNonCalculationFilter + "').show();"},
                                New With {.class = "sitewide-button-16 remove-button-16"})

            </td>
        </tr>

        @If Model.getCalculationFilterApplicableDaysOfWeekIds.Contains(a.Id) = False Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdApplicableDayOfWeekCalculationFilter + "').hide(); </script>")
        End If
    Next
</table>

<table class="edit-table">
    <tr>
        <th>
            Does not apply on...
        </th>
        <th>
        </th>
    </tr>
    @For Each a In Model.AllApplicableDaysOfWeek.OrderBy(Function(DoW) DoW.Id)
        Dim trIdApplicableDayOfWeekCalculationFilter = "trApplicableDayOfWeekCalculationFilter" + a.Id.ToString
        Dim trIdApplicableDayOfWeekNonCalculationFilter = "trApplicableDayOfWeekNonCalculationFilter" + a.Id.ToString
        @<tr id='@trIdApplicableDayOfWeekNonCalculationFilter'>
            <td>
                @a.DayName
            </td>
            <td>
                @Ajax.RouteLink("Add to Calculation Filter",
                                "CalculationFilterAddApplicableDayOfWeekRoute",
                                New With {.CalculationFilterShortName = Model.CalculationFilter.getRouteName,
                                          .ApplicableDayOfWeekId = a.Id},
                                New AjaxOptions With {.HttpMethod = "PUT",
                                                      .OnComplete = "$('#" + trIdApplicableDayOfWeekCalculationFilter + "').show();$('#" + trIdApplicableDayOfWeekNonCalculationFilter + "').hide();"},
                                New With {.class = "sitewide-button-16 add-button-16"})

            </td>
        </tr>

        @If Model.getCalculationFilterApplicableDaysOfWeekIds.Contains(a.Id) = True Then
            @Html.Raw("<script type = text/javascript> $('#" + trIdApplicableDayOfWeekNonCalculationFilter + "').hide(); </script>")
        End If
    Next
</table>

