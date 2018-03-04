<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>@ViewData("Title") - Southdowns Monitoring Data Website</title>
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width" />
    @Styles.Render("~/Content/css")
    @Styles.Render("~/Content/themes/base/css")
    @Styles.Render("~/Content/themes/sec/css")
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/jqueryval")
    @Scripts.Render("~/bundles/jqueryui")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/HighChart")
    <script src="~/Scripts/Highcharts-4.0.1/js/themes/gray.js"></script>
    <script src="~/Scripts/date.js"></script>
    @*Leaflet*@
    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.0.3/dist/leaflet.css" />
    <script src="https://unpkg.com/leaflet@1.0.3/dist/leaflet.js"></script>
    @*End Leaflet*@
</head>
<body>
    <header>
        <div class="content-wrapper">
            <div class="float-left">
                @Html.RouteLink("Home", "HomeIndexRoute", Nothing, New With {.class = "site-logo"})
            </div>
            <div class="float-right">
                <section id="login">
                    @Html.Partial("_LoginPartial")
                </section>
            </div>
        </div>
    </header>
    <div id="body">
        @RenderSection("featured", required:=False)
        <section class="content-wrapper main-content clear-fix">
            @RenderBody()
        </section>
    </div>
    <footer>
        <div class="content-wrapper">
            <div class="float-left">
                <p>&copy; @DateTime.Now.Year - Southdowns Environmental Consultants Ltd</p>
            </div>
        </div>
    </footer>

    @RenderSection("scripts", required:=False)

</body>
</html>



<script type="text/javascript">

    function addButtonAnimations() {
        $('.sitewide-button-16').click(function (e) {
            $(this).css('background-image', 'url(@Url.Content("~/Images/px16/loading_16x16.gif"))');
        });
        $('.sitewide-button-32').click(function (e) {
            $(this).css('background-image', 'url(@Url.Content("~/Images/px32/loading_32x32.gif"))');
        });
        $('.sitewide-button-64').click(function (e) {
            $(this).css('background-image', 'url(@Url.Content("~/Images/px64/loading_64x64.gif"))');
        });
        $('.sitewide-button-128').click(function (e) {
            $(this).css('background-image', 'url(@Url.Content("~/Images/px128/loading_128x128.gif"))');
        });
    };
    
    function fixTooltips() {
        $(".tooltipClass").tooltip({

            open: function (event, ui) {
                ui.tooltip.css("max-width", "400px");
            },
            content: function () {
                return $(this).attr('title');
            },
            hide: { effect: "explode", duration: 1000 },
            show: { effect: "slide", duration: 500 }
        });
        $(".homePageTooltipClass").tooltip({

            open: function (event, ui) {
                ui.tooltip.css("max-width", "300px");
            },
            content: function () {
                return $(this).attr('title');
            },
            hide: { effect: "drop", duration: 1000 },
            show: { effect: "slide", duration: 500 }
        });
    };


    $(document).ready(function () {
        addButtonAnimations();
        fixTooltips();
    });

</script>