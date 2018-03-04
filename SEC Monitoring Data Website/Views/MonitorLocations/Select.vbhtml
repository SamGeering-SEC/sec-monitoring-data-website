@ModelType SelectMonitorLocationViewModel

@Code
    ViewData("Title") = "Select Monitor Location"
End Code

<h2>Select a Monitor Location</h2>

@Html.Partial("Select_Map", Model.MonitorLocations)


@Html.Partial("Select_Table", Model.MonitorLocations)


@Html.Partial("NavigationButtons", Model.NavigationButtons)