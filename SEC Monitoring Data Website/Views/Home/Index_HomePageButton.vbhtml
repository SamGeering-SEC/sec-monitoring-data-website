@ModelType HomePageButtonViewModel

@Code

    Dim toolTipHolderName = Model.Text.Replace(" ", "") + "ToolTipHolder"
    
End Code

<div style="float:left; width:128px; height:200px; align-content:center; text-align:center; vertical-align:top">

    @Html.RouteLink(Model.Text,
                    Model.RouteName,
                    Nothing,
                    New With {.class = "sitewide-button-128 " + Model.ButtonClass,
                              .title = Model.HelpText})
    <h3>
        @Model.Text
    </h3>

</div>
