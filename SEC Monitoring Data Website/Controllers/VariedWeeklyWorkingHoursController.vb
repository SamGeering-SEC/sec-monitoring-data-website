Imports System.Data.Entity.Core
Imports libSEC

Public Class VariedWeeklyWorkingHoursController
        Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub

#Region "Delete"

    <HttpGet()> _
    Public Function Details(VariedWeeklyWorkingHoursId As String) As ActionResult

        Dim VariedWeeklyWorkingHours = MeasurementsDAL.GetVariedWeeklyWorkingHours(VariedWeeklyWorkingHoursId)
        If IsNothing(VariedWeeklyWorkingHours) Then
            Return HttpNotFound()
        End If

        Dim projectId = VariedWeeklyWorkingHours.ProjectId
        If (Not CanAccessProject(projectId) Or
            Not UAL.CanEditProjects) Then Return New HttpUnauthorizedResult()

        Return View(VariedWeeklyWorkingHours)

    End Function

#End Region

#Region "Create"

    <HttpGet()>
    Public Function Create(ProjectRouteName As String) As ActionResult

        If (Not CanAccessProject(ProjectRouteName) Or
            Not UAL.CanEditProjects) Then Return New HttpUnauthorizedResult()

        ' Check Project Route Name is valid
        Dim Project = MeasurementsDAL.GetProject(ProjectRouteName.FromRouteName)
        If Project Is Nothing Then Return HttpNotFound()
        ' Check that the Project has Standard Weekly Working Hours
        Dim standardWorkingHours = Project.StandardWeeklyWorkingHours
        If standardWorkingHours Is Nothing Then Return HttpNotFound()
        ' Pre-populate the selected Measurement Views with those from the Standard Hours
        Dim selectableMeasurementViews As New List(Of SelectableMeasurementView)
        Dim standardMeasurementViewIds As List(Of Integer) = Project.StandardWeeklyWorkingHours.AvailableMeasurementViews.Select(Function(mv) mv.Id).ToList
        For Each mv In Project.MeasurementViews
            selectableMeasurementViews.Add(New SelectableMeasurementView With {.MeasurementView = mv, .IsSelected = standardMeasurementViewIds.Contains(mv.Id)})
        Next
        ' Create ViewModel
        'Dim viewModel = New CreateVariedWeeklyWorkingHoursViewModel With {
        '    .VariedWeeklyWorkingHours = New VariedWeeklyWorkingHours With {.Project = Project,
        '                                                                   .StartDate = Date.Today,
        '                                                                   .EndDate = Date.Today},
        '    .WorkingWeekViewModel = New WorkingWeekViewModel(Project.StandardWeeklyWorkingHours),
        '    .AllMeasurementViews = selectableMeasurementViews}
        Dim viewModel = New CreateVariedWeeklyWorkingHoursViewModel With {
    .VariedWeeklyWorkingHours = New VariedWeeklyWorkingHours With {.Project = Project,
                                                                   .ProjectId = Project.Id,
                                                                   .StartDate = Date.Today,
                                                                   .EndDate = Date.Today},
    .WorkingWeekViewModel = New WorkingWeekViewModel(Project.StandardWeeklyWorkingHours)}

        Return View(viewModel)

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Create(ViewModel As CreateVariedWeeklyWorkingHoursViewModel)

        ' Clean up ModelState
        'For i = 0 To ViewModel.AllMeasurementViews.Count - 1
        '    ModelState.Remove("AllMeasurementViews[" + i.ToString + "].MeasurementView.ViewName")
        '    ModelState.Remove("AllMeasurementViews[" + i.ToString + "].MeasurementView.DisplayName")
        '    ModelState.Remove("AllMeasurementViews[" + i.ToString + "].MeasurementView.TableResultsHeader")
        '    ModelState.Remove("AllMeasurementViews[" + i.ToString + "].MeasurementView.MeasurementType")
        'Next

        If (Not CanAccessProject(ViewModel.VariedWeeklyWorkingHours.ProjectId) Or
            Not UAL.CanEditProjects) Then Return New HttpUnauthorizedResult()

        If ModelState.IsValid = True Then

            Dim Project = MeasurementsDAL.GetProject(ViewModel.VariedWeeklyWorkingHours.ProjectId)
            ' Get Basic Properties from ViewModel
            Dim startDate = ViewModel.VariedWeeklyWorkingHours.StartDate
            Dim endDate = ViewModel.VariedWeeklyWorkingHours.EndDate
            ' Create a new VariedWeeklyWorkingHours object from ViewModel Working Hours and reassign Basic Properties
            Dim VariedWeeklyWorkingHours = ViewModel.WorkingWeekViewModel.GetVariedWeeklyWorkingHours
            VariedWeeklyWorkingHours.ProjectId = Project.Id
            VariedWeeklyWorkingHours.StartDate = ViewModel.VariedWeeklyWorkingHours.StartDate
            VariedWeeklyWorkingHours.EndDate = ViewModel.VariedWeeklyWorkingHours.EndDate
            ' Add VariedWeeklyWorkingHours to Database
            VariedWeeklyWorkingHours = MeasurementsDAL.AddProjectVariedWeeklyWorkingHours(Project.Id, VariedWeeklyWorkingHours)
            ' Add Selected Measurement Views
            'For Each mv In ViewModel.AllMeasurementViews
            '    If mv.IsSelected = True Then
            '        AddMeasurementView(VariedWeeklyWorkingHours.Id, mv.MeasurementView.Id)
            '    End If
            'Next
            ' Redirect to Project Edit View
            Return RedirectToRoute("ProjectEditRoute", New With {.ProjectRouteName = Project.getRouteName})
        End If

        Return View(ViewModel)

    End Function

#End Region

#Region "Edit"

    <HttpGet()> _
    Public Function Edit(ByVal VariedWeeklyWorkingHoursId As Integer) As ActionResult

        Dim VariedWeeklyWorkingHours As VariedWeeklyWorkingHours = MeasurementsDAL.GetVariedWeeklyWorkingHours(VariedWeeklyWorkingHoursId)
        If IsNothing(VariedWeeklyWorkingHours) Then
            Return HttpNotFound()
        End If

        Dim projectId = VariedWeeklyWorkingHours.ProjectId
        If (Not CanAccessProject(projectId) Or
            Not UAL.CanEditProjects) Then Return New HttpUnauthorizedResult()

        Return View(getEditVariedWeeklyWorkingHoursViewModel(VariedWeeklyWorkingHours))

    End Function

    Private Function getEditVariedWeeklyWorkingHoursViewModel(ByVal VariedWeeklyWorkingHours As VariedWeeklyWorkingHours)

        Dim selectableMeasurementViews As New List(Of SelectableMeasurementView)
        Dim Project = MeasurementsDAL.GetProject(VariedWeeklyWorkingHours.ProjectId)
        Dim selectedMeasurementViews = VariedWeeklyWorkingHours.AvailableMeasurementViews
        For Each mv In Project.MeasurementViews
            selectableMeasurementViews.Add(New SelectableMeasurementView With {.MeasurementView = mv,
                                                                               .IsSelected = selectedMeasurementViews.Select(Function(smv) smv.Id).ToList.Contains(mv.Id)})
        Next

        Return New EditVariedWeeklyWorkingHoursViewModel With {
            .VariedWeeklyWorkingHours = VariedWeeklyWorkingHours,
            .WorkingWeekViewModel = New WorkingWeekViewModel(VariedWeeklyWorkingHours)
        }

    End Function

    <HttpPost()> _
    <ValidateAntiForgeryToken()> _
    Public Function Edit(ByVal ViewModel As EditVariedWeeklyWorkingHoursViewModel) As ActionResult

        ' Clean up ModelState
        'For i = 0 To ViewModel.AllMeasurementViews.Count - 1
        '    ModelState.Remove("AllMeasurementViews[" + i.ToString + "].MeasurementView.ViewName")
        '    ModelState.Remove("AllMeasurementViews[" + i.ToString + "].MeasurementView.DisplayName")
        '    ModelState.Remove("AllMeasurementViews[" + i.ToString + "].MeasurementView.TableResultsHeader")
        '    ModelState.Remove("AllMeasurementViews[" + i.ToString + "].MeasurementView.MeasurementType")
        'Next

        If (Not CanAccessProject(ViewModel.VariedWeeklyWorkingHours.ProjectId) Or
            Not UAL.CanEditProjects) Then Return New HttpUnauthorizedResult()

        If ModelState.IsValid Then
            ' Create new CreateViewModel
            Dim Project = MeasurementsDAL.GetProject(ViewModel.VariedWeeklyWorkingHours.ProjectId)
            MeasurementsDAL.UpdateVariedWeeklyWorkingHours(ViewModel.VariedWeeklyWorkingHours)
            MeasurementsDAL.UpdateVariedWeeklyWorkingHoursVariedDailyWorkingHours(ViewModel.VariedWeeklyWorkingHours.Id, ViewModel.WorkingWeekViewModel.GetVariedDailyWorkingHours)
            ' Update Available Measurement Views
            'For Each mv In ViewModel.AllMeasurementViews
            '    If mv.IsSelected = True Then
            '        AddMeasurementView(ViewModel.VariedWeeklyWorkingHours.Id, mv.MeasurementView.Id)
            '    Else
            '        RemoveMeasurementView(ViewModel.VariedWeeklyWorkingHours.Id, mv.MeasurementView.Id)
            '    End If
            'Next
            ' Redirect to Details
            Return RedirectToRoute("ProjectEditRoute", New With {.ProjectRouteName = Project.getRouteName})
        End If

        Return View(ViewModel)

    End Function

    <HttpPut()> _
    Public Function AddMeasurementView(VariedWeeklyWorkingHoursId As Integer, MeasurementViewId As Integer) As ActionResult

        Dim VariedWeeklyWorkingHours As VariedWeeklyWorkingHours = MeasurementsDAL.GetVariedWeeklyWorkingHours(VariedWeeklyWorkingHoursId)
        If IsNothing(VariedWeeklyWorkingHours) Then
            Return HttpNotFound()
        End If

        Dim projectId = VariedWeeklyWorkingHours.ProjectId
        If (Not CanAccessProject(projectId) Or
            Not UAL.CanEditProjects) Then Return New HttpUnauthorizedResult()

        MeasurementsDAL.AddVariedWeeklyWorkingHoursMeasurementView(VariedWeeklyWorkingHoursId, MeasurementViewId)
        Return Nothing

    End Function
    <HttpDelete()> _
    Public Function RemoveMeasurementView(VariedWeeklyWorkingHoursId As Integer, MeasurementViewId As Integer) As ActionResult

        Dim VariedWeeklyWorkingHours As VariedWeeklyWorkingHours = MeasurementsDAL.GetVariedWeeklyWorkingHours(VariedWeeklyWorkingHoursId)
        If IsNothing(VariedWeeklyWorkingHours) Then
            Return HttpNotFound()
        End If

        Dim projectId = VariedWeeklyWorkingHours.ProjectId
        If (Not CanAccessProject(projectId) Or
            Not UAL.CanEditProjects) Then Return New HttpUnauthorizedResult()

        MeasurementsDAL.RemoveVariedWeeklyWorkingHoursMeasurementView(VariedWeeklyWorkingHoursId, MeasurementViewId)
        Return Nothing

    End Function


#End Region


End Class