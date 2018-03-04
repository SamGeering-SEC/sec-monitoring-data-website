Imports System.Collections.Generic
Imports System.Web.Mvc
Imports Ninject
Imports Ninject.Parameters
Imports Ninject.Syntax
Imports System.Configuration
Imports libSEC

Public Class NinjectDependencyResolver

    Implements IDependencyResolver

    Private kernel As IKernel
    Public Sub New()
        kernel = New StandardKernel()
        AddBindings()
    End Sub
    Public Function GetService(serviceType As Type) As Object Implements IDependencyResolver.GetService
        Return kernel.TryGet(serviceType)
    End Function
    Public Function GetServices(serviceType As Type) As IEnumerable(Of Object) Implements IDependencyResolver.GetServices
        Return kernel.GetAll(serviceType)
    End Function
    Private Sub AddBindings()
        kernel.Bind(Of IMeasurementsDAL).To(Of EFMeasurementsDAL)()
    End Sub

End Class
