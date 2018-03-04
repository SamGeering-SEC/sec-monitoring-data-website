@ModelType IEnumerable(Of NavigationButtonViewModel)

<table>
    <tr>
        @For Each button In Model
            @<td style="width:64px">
                @Html.RouteLink(button.Text,
                                button.RouteName,
                                button.RouteValues,
                                New With {.class = button.ButtonClass})
            </td>
        Next
    </tr>
    <tr style="text-align:center">
        @For Each Button In Model
            @<td>
                @Button.Text
            </td>
        Next
    </tr>
</table>