Imports System.Data.Entity.Core
Imports libSEC

Public Class MeasurementViewGroupsController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Create"

    <HttpGet()> _
    Public Function Create(MeasurementViewRouteName As String) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim MeasurementView = MeasurementsDAL.GetMeasurementView(MeasurementViewRouteName.FromRouteName)
        If MeasurementView Is Nothing Then Return HttpNotFound()
        Dim nextIndex As Integer = MeasurementView.MeasurementViewGroups.Count + 1

        Dim vm As New CreateMeasurementViewGroupViewModel With {.MeasurementView = MeasurementView,
                                                                .MeasurementViewGroup = New MeasurementViewGroup With {.GroupIndex = nextIndex}}

        Return View(vm)

    End Function
    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ByVal ViewModel As CreateMeasurementViewGroupViewModel) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        ModelState.Remove("MeasurementView.ViewName")
        ModelState.Remove("MeasurementView.TableResultsHeader")
        ModelState.Remove("MeasurementView.MeasurementType")
        ModelState.Remove("MeasurementView.DisplayName")

        If ModelState.IsValid Then
            Dim MeasurementView = MeasurementsDAL.GetMeasurementView(ViewModel.MeasurementView.Id)
            ViewModel.MeasurementViewGroup.MeasurementView = MeasurementView
            ' Add Measurement View Group to database
            MeasurementsDAL.AddMeasurementViewGroup(ViewModel.MeasurementViewGroup)
            ' Redirect to Measurement View Details
            Return RedirectToRoute("MeasurementViewEditRoute", New With {.MeasurementViewRouteName = MeasurementView.getRouteName})
        End If

        Return View(ViewModel)

    End Function

#End Region

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(ByVal MeasurementViewRouteName As String, GroupIndex As Integer) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        Dim MeasurementViewGroup As MeasurementViewGroup = MeasurementsDAL.GetMeasurementViewGroup(MeasurementViewRouteName.FromRouteName, GroupIndex)
        If IsNothing(MeasurementViewGroup) Then
            Return HttpNotFound()
        End If

        Return View(getEditMeasurementViewGroupViewModel(MeasurementViewGroup))

    End Function

    Private Function getEditMeasurementViewGroupViewModel(ByVal MeasurementViewGroup As MeasurementViewGroup)

        Return New EditMeasurementViewGroupViewModel With {.MeasurementViewGroup = MeasurementViewGroup,
                                                           .MeasurementView = MeasurementViewGroup.MeasurementView}

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditMeasurementViewGroupViewModel) As ActionResult

        If Not UAL.CanEditMeasurementViews Then Return New HttpUnauthorizedResult()

        ModelState.Remove("MeasurementView.ViewName")
        ModelState.Remove("MeasurementView.TableResultsHeader")
        ModelState.Remove("MeasurementView.MeasurementType")
        ModelState.Remove("MeasurementView.DisplayName")

        If ModelState.IsValid Then
            Dim MeasurementView = MeasurementsDAL.GetMeasurementView(ViewModel.MeasurementView.Id)
            ' Update MeasurementViewGroup
            MeasurementsDAL.UpdateMeasurementViewGroup(ViewModel.MeasurementViewGroup)
            ' Redirect to Measurement View Details
            Return RedirectToRoute("MeasurementViewEditRoute", New With {.MeasurementViewRouteName = MeasurementView.getRouteName})
        End If

        Return View(ViewModel)

    End Function

#End Region

End Class