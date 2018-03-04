Imports System.Web
Imports System.Web.Optimization

Public Class BundleConfig
    ' For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
    Public Shared Sub RegisterBundles(ByVal bundles As BundleCollection)
        bundles.Add(New ScriptBundle("~/bundles/jquery").Include(
                   "~/Scripts/jquery-{version}.js"))

        'bundles.Add(New ScriptBundle("~/bundles/jqueryui").Include(
        '            "~/Scripts/jquery-ui-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryui").Include(
                                     "~/Scripts/jquery-ui-{version}.js",
                                     "~/Scripts/jquery-ui.unobtrusive-{version}.js"))

        bundles.Add(New ScriptBundle("~/bundles/jqueryval").Include(
                                     "~/Scripts/jquery.unobtrusive*",
                                     "~/Scripts/jquery.validate*"))

        bundles.Add(New ScriptBundle("~/bundles/HighChart").Include(
                    "~/Scripts/Highcharts-4.0.1/js/highcharts.js",
                    "~/Scripts/Highcharts-4.0.1/js/modules/exporting.js")
                )

        ' Use the development version of Modernizr to develop with and learn from. Then, when you're
        ' ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
        bundles.Add(New ScriptBundle("~/bundles/modernizr").Include(
                    "~/Scripts/modernizr-*"))

        bundles.Add(New StyleBundle("~/Content/css").Include("~/Content/site.css", "~/Content/PagedList.css"))

        bundles.Add(New StyleBundle("~/Content/themes/base/css").Include(
                    "~/Content/themes/base/core.css",
                    "~/Content/themes/base/resizable.css",
                    "~/Content/themes/base/selectable.css",
                    "~/Content/themes/base/accordion.css",
                    "~/Content/themes/base/autocomplete.css",
                    "~/Content/themes/base/button.css",
                    "~/Content/themes/base/dialog.css",
                    "~/Content/themes/base/slider.css",
                    "~/Content/themes/base/tabs.css",
                    "~/Content/themes/base/datepicker.css",
                    "~/Content/themes/base/progressbar.css",
                    "~/Content/themes/base/theme.css"))
        bundles.Add(New StyleBundle("~/Content/themes/cupertino/css").Include(
            "~/Content/themes/cupertino/jquery-ui.cupertino.min.css"
        ))
        bundles.Add(New StyleBundle("~/Content/themes/sec/css").Include(
                    "~/Content/themes/sec/jquery-ui.structure.min.css",
                    "~/Content/themes/sec/jquery-ui.theme.min.css"
        ))

    End Sub
End Class