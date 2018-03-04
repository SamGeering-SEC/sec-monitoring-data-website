' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
' visit http://go.microsoft.com/?LinkId=9394802
Imports System.Web.Http
Imports System.Web.Optimization
Imports libSEC.Dates


Public Class MvcApplication

    Inherits System.Web.HttpApplication

    Sub Application_Start()

        AreaRegistration.RegisterAllAreas()
        DependencyResolver.SetResolver(New NinjectDependencyResolver()) ' added for DI
        WebApiConfig.Register(GlobalConfiguration.Configuration)
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
        AuthConfig.RegisterAuth()

        ' Initial seed - comment out for production
        Dim Seed = New InitialSeed()


    End Sub


End Class
