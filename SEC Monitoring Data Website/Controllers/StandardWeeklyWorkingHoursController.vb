Imports System.Data.Entity.Core
Imports libSEC

Public Class StandardWeeklyWorkingHoursController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(ByVal ProjectRouteName As String) As ActionResult

        If (Not UAL.CanEditProjects Or
            Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

        Dim Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
        If Project Is Nothing Then Return HttpNotFound()
        Dim StandardWeeklyWorkingHours = MeasurementsDAL.GetStandardWeeklyWorkingHours(Project.StandardWeeklyWorkingHours.Id)

        Return View(getEditStandardWeeklyWorkingHoursViewModel(StandardWeeklyWorkingHours))

    End Function

    Private Function getEditStandardWeeklyWorkingHoursViewModel(ByVal StandardWeeklyWorkingHours As StandardWeeklyWorkingHours)

        Return New EditStandardWeeklyWorkingHoursViewModel With {
            .ProjectId = StandardWeeklyWorkingHours.Project.Id,
            .StandardWeeklyWorkingHours = StandardWeeklyWorkingHours,
            .AllMeasurementViews = MeasurementsDAL.GetMeasurementViews.Where(
                Function(mv) StandardWeeklyWorkingHours.Project.MeasurementViews.Contains(mv)
            ),
            .WorkingWeekViewModel = New WorkingWeekViewModel(StandardWeeklyWorkingHours)
        }

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditStandardWeeklyWorkingHoursViewModel) As ActionResult

        If (Not UAL.CanEditProjects Or
            Not CanAccessProject(ViewModel.ProjectId)) Then Return New HttpUnauthorizedResult()

        If ModelState.IsValid Then
            ' Update StandardWeeklyWorkingHours
            Dim Project = MeasurementsDAL.GetProject(ViewModel.ProjectId)
            Dim modelWeeklyWorkingHours = ViewModel.WorkingWeekViewModel.GetStandardWeeklyWorkingHours
            Dim dbWorkingHours = Project.StandardWeeklyWorkingHours
            Dim currentMeasurementViewIds As List(Of Integer) = dbWorkingHours.AvailableMeasurementViews.Select(Function(mv) mv.Id).ToList
            MeasurementsDAL.UpdateProjectWorkingHours(Project.Id, modelWeeklyWorkingHours, currentMeasurementViewIds)
            ' Redirect to Details
            Return RedirectToRoute("ProjectEditRoute", New With {.ProjectRouteName = Project.getRouteName})
        End If

        Return View(ViewModel)

    End Function

    <HttpPut()> _
    Public Function AddMeasurementView(ProjectRouteName As String, MeasurementViewId As Integer) As ActionResult

        If (Not UAL.CanEditProjects Or
            Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

        Dim Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
        Dim StandardWeeklyWorkingHours = Project.StandardWeeklyWorkingHours
        MeasurementsDAL.AddStandardWeeklyWorkingHoursMeasurementView(StandardWeeklyWorkingHours.Id, MeasurementViewId)
        Return Nothing

    End Function
    <HttpDelete()> _
    Public Function RemoveMeasurementView(ProjectRouteName As String, MeasurementViewId As Integer) As ActionResult

        If (Not UAL.CanEditProjects Or
            Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

        Dim Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
        Dim StandardWeeklyWorkingHours = Project.StandardWeeklyWorkingHours
        MeasurementsDAL.RemoveStandardWeeklyWorkingHoursMeasurementView(StandardWeeklyWorkingHours.Id, MeasurementViewId)
        Return Nothing

    End Function

#End Region



End Class