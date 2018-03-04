Imports System.Web.Mvc
Imports libSEC

Namespace Controllers
    Public Class AssessmentCriteriaController
        Inherits ControllerBase

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)

        End Sub

#Region "Index"

        Public Function Index(
        ProjectRouteName As String, AssessmentCriterionGroupRouteName As String, MonitorLocationRouteName As String
    ) As ActionResult

            ' Check Permissions
            If (Not UAL.CanViewAssessmentCriteria Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            ' Get Assessment Criterion Group
            Dim assessmentCriterionGroup = MeasurementsDAL.GetAssessmentCriterionGroup(
                ProjectRouteName.FromRouteName,
                AssessmentCriterionGroupRouteName.FromRouteName
            )
            If IsNothing(assessmentCriterionGroup) Then Return HttpNotFound()
            ' Get Monitor Location
            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(
                ProjectRouteName.FromRouteName, MonitorLocationRouteName.FromRouteName
            )
            If IsNothing(monitorLocation) Then Return HttpNotFound()
            ' Get Assessment Criteria
            Dim assessmentCriteria = monitorLocation.AssessmentCriteria.Where(
                Function(ac) ac.AssessmentCriterionGroupId = assessmentCriterionGroup.Id
            ).OrderBy(Function(ac) ac.CriterionIndex)

            Dim viewModel As New MonitorLocationCriteriaDetailsViewModel With {
                .AssessmentCriteria = assessmentCriteria,
                .AssessmentCriterionGroup = assessmentCriterionGroup,
                .MonitorLocation = monitorLocation,
                .NavigationButtons = getIndexNavigationButtons(assessmentCriterionGroup)
            }

            SetIndexLinks()

            Return View(viewModel)

        End Function
        Public Sub SetIndexLinks()

            ViewData("ShowCalculationFilterDetailsLinks") = UAL.CanViewCalculationFilterDetails
            ViewData("ShowAssessmentCriterionEditLinks") = UAL.CanEditAssessmentCriteria
            ViewData("ShowCreateAssessmentCriterionLink") = UAL.CanCreateAssessmentCriteria

        End Sub
        Private Function getIndexNavigationButtons(assessmentCriterionGroup As AssessmentCriterionGroup) As IEnumerable(Of NavigationButtonViewModel)

            Dim buttons As New List(Of NavigationButtonViewModel)

            buttons.Add(
                New NavigationButtonViewModel(
                    "Back to Assessment Group",
                    "AssessmentCriterionGroupDetailsRoute",
                    New With {.controller = "AssessmentCriterionGroups",
                              .ProjectRouteName = assessmentCriterionGroup.Project.getRouteName(),
                              .AssessmentCriterionGroupRouteName = assessmentCriterionGroup.getRouteName},
                    "sitewide-button-64 assessments-button-64"
                )
            )

            Return buttons

        End Function

#End Region

#Region "Details"

        Public Function Details(
            ProjectRouteName As String, AssessmentCriterionGroupRouteName As String, MonitorLocationRouteName As String,
            CriterionIndex As Integer
        )

            ' Check Permissions
            If (Not UAL.CanViewAssessmentCriteria Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            ' Get Assessment Criterion Group
            Dim assessmentCriterionGroup = MeasurementsDAL.GetAssessmentCriterionGroup(
                ProjectRouteName.FromRouteName,
                AssessmentCriterionGroupRouteName.FromRouteName
            )
            If IsNothing(assessmentCriterionGroup) Then Return HttpNotFound()
            ' Get Monitor Location
            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(
                ProjectRouteName.FromRouteName, MonitorLocationRouteName.FromRouteName
            )
            If IsNothing(monitorLocation) Then Return HttpNotFound()
            ' Get Assessment Criterion
            Dim assessmentCriteria = monitorLocation.AssessmentCriteria.Where(
                Function(ac) ac.AssessmentCriterionGroupId = assessmentCriterionGroup.Id
            ).OrderBy(Function(ac) ac.CriterionIndex)
            Dim criterionIndices = assessmentCriteria.Select(Function(ac) ac.CriterionIndex).ToList
            If Not criterionIndices.Contains(CriterionIndex) Then Return HttpNotFound()
            Dim assessmentCriterion = assessmentCriteria.Single(Function(ac) ac.CriterionIndex = CriterionIndex)

            Dim viewModel As New MonitorLocationCriterionDetailsViewModel With {
                .AssessmentCriterionGroup = assessmentCriterionGroup,
                .MonitorLocation = monitorLocation,
                .AssessmentCriterion = assessmentCriterion,
                .NavigationButtons = getDetailsNavigationButtons(assessmentCriterion)
            }

            Return View(viewModel)

        End Function
        Private Function getDetailsNavigationButtons(assessmentCriterion As AssessmentCriterion) As IEnumerable(Of NavigationButtonViewModel)

            Dim buttons As New List(Of NavigationButtonViewModel)

            Dim project = assessmentCriterion.AssessmentCriterionGroup.Project
            Dim group = assessmentCriterion.AssessmentCriterionGroup
            Dim monitorLocation = assessmentCriterion.MonitorLocation
            Dim numCriteria = group.AssessmentCriteria.Where(Function(ac) ac.MonitorLocation.Id = monitorLocation.Id).Count

            buttons.Add(
                New NavigationButtonViewModel(
                    "Back to Assessment Criteria List",
                    "AssessmentCriterionIndexRoute",
                    New With {.ProjectRouteName = project.getRouteName(),
                              .AssessmentCriterionGroupRouteName = group.getRouteName,
                              .MonitorLocationRouteName = monitorLocation.getRouteName},
                    "sitewide-button-64 index-button-64"
                )
            )
            If assessmentCriterion.CriterionIndex > 1 Then
                buttons.Add(
                    New NavigationButtonViewModel(
                        "Previous Criterion",
                        "AssessmentCriterionDetailsRoute",
                        New With {.ProjectRouteName = project.getRouteName,
                                  .AssessmentCriterionGroupRouteName = group.getRouteName,
                                  .MonitorLocationRouteName = monitorLocation.getRouteName,
                                  .CriterionIndex = assessmentCriterion.CriterionIndex - 1},
                        "sitewide-button-64 left-button-64"
                        )
                    )
            End If
            If assessmentCriterion.CriterionIndex < numCriteria Then
                buttons.Add(
                    New NavigationButtonViewModel(
                        "Next Criterion",
                        "AssessmentCriterionDetailsRoute",
                        New With {.ProjectRouteName = project.getRouteName,
                                  .AssessmentCriterionGroupRouteName = group.getRouteName,
                                  .MonitorLocationRouteName = monitorLocation.getRouteName,
                                  .CriterionIndex = assessmentCriterion.CriterionIndex + 1},
                        "sitewide-button-64 right-button-64"
                        )
                    )
            End If

            Return buttons

        End Function


#End Region

#Region "Edit"

        Public Function Edit(
                   ProjectRouteName As String, AssessmentCriterionGroupRouteName As String, MonitorLocationRouteName As String,
                   CriterionIndex As Integer
               )

            ' Check Permissions
            If (Not UAL.CanEditAssessmentCriteria Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            ' Get Assessment Criterion Group
            Dim assessmentCriterionGroup = MeasurementsDAL.GetAssessmentCriterionGroup(
                ProjectRouteName.FromRouteName,
                AssessmentCriterionGroupRouteName.FromRouteName
            )
            If IsNothing(assessmentCriterionGroup) Then Return HttpNotFound()
            ' Get Monitor Location
            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(
                ProjectRouteName.FromRouteName, MonitorLocationRouteName.FromRouteName
            )
            If IsNothing(monitorLocation) Then Return HttpNotFound()
            ' Get Assessment Criterion
            Dim assessmentCriteria = monitorLocation.AssessmentCriteria.Where(
                Function(ac) ac.AssessmentCriterionGroupId = assessmentCriterionGroup.Id
            ).OrderBy(Function(ac) ac.CriterionIndex)
            Dim criterionIndices = assessmentCriteria.Select(Function(ac) ac.CriterionIndex).ToList
            If Not criterionIndices.Contains(CriterionIndex) Then Return HttpNotFound()
            Dim assessmentCriterion = assessmentCriteria.Single(Function(ac) ac.CriterionIndex = CriterionIndex)

            'Dim viewModel As New MonitorLocationCriterionDetailsViewModel With {
            '    .AssessmentCriterionGroup = assessmentCriterionGroup,
            '    .MonitorLocation = monitorLocation,
            '    .AssessmentCriterion = assessmentCriterion,
            '    .NavigationButtons = getDetailsNavigationButtons(assessmentCriterion)
            '}

            'Return View(viewModel)
            Return View(assessmentCriterion.CriterionIndex)

        End Function


#End Region

#Region "Create"

        Public Function Create(
           ProjectRouteName As String, AssessmentCriterionGroupRouteName As String, MonitorLocationRouteName As String
       )

            ' Check Permissions
            If (Not UAL.CanCreateAssessmentCriteria Or
                Not CanAccessProject(ProjectRouteName)) Then Return New HttpUnauthorizedResult()

            ' Get Assessment Criterion Group
            Dim assessmentCriterionGroup = MeasurementsDAL.GetAssessmentCriterionGroup(
                ProjectRouteName.FromRouteName,
                AssessmentCriterionGroupRouteName.FromRouteName
            )
            If IsNothing(assessmentCriterionGroup) Then Return HttpNotFound()
            ' Get Monitor Location
            Dim monitorLocation = MeasurementsDAL.GetMonitorLocation(
                ProjectRouteName.FromRouteName, MonitorLocationRouteName.FromRouteName
            )
            If IsNothing(monitorLocation) Then Return HttpNotFound()

            'Dim viewModel As New MonitorLocationCriterionDetailsViewModel With {
            '    .AssessmentCriterionGroup = assessmentCriterionGroup,
            '    .MonitorLocation = monitorLocation,
            '    .AssessmentCriterion = assessmentCriterion,
            '    .NavigationButtons = getDetailsNavigationButtons(assessmentCriterion)
            '}

            'Return View(viewModel)

            Return View(7)

        End Function

#End Region



    End Class

End Namespace