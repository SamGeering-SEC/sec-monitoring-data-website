Imports libSEC
Imports System.Data.Entity.Core
Imports System.Data.Entity
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports libSEC.Lists
Imports System.Data.Entity.Validation
Imports System.Data.Entity.Infrastructure


Partial Public Class EFMeasurementsDAL

    Implements IMeasurementsDAL

    Dim db As New SECMonitoringDbContext

    Sub SaveChanges(context As SECMonitoringDbContext)

        Try
            context.SaveChanges()
        Catch eValidation As DbEntityValidationException
            For Each eve As DbEntityValidationResult In eValidation.EntityValidationErrors
                Debug.WriteLine(vbCrLf + "DbEntityValidationException!!!")
                Debug.WriteLine("Entity of type " + eve.Entry.Entity.GetType().Name +
                             " in state " + eve.Entry.State.ToString + " has the following validation errors:")
                For Each ve As DbValidationError In eve.ValidationErrors
                    Debug.WriteLine("- Property: " + ve.PropertyName +
                                ", Value: " + eve.Entry.CurrentValues.GetValue(Of Object)(ve.PropertyName) +
                                ", Error: " + ve.ErrorMessage)
                Next
            Next
            'Throw
        Catch eUpdate As DbUpdateException
            Debug.WriteLine(vbCrLf + "DbUpdateException!!!")
            Debug.WriteLine(eUpdate.InnerException.InnerException.ToString)
            'Throw
        End Try

    End Sub

#Region "Assessment Criteria"

#Region "Groups"

    Public Function GetAssessmentCriterionGroup(AssessmentCriterionGroupId As Integer, Optional db As SECMonitoringDbContext = Nothing) As AssessmentCriterionGroup Implements IMeasurementsDAL.GetAssessmentCriterionGroup

        If db Is Nothing Then db = Me.db
        Return GetAssessmentCriterionGroups(db).Single(Function(acg) acg.Id = AssessmentCriterionGroupId)

    End Function
    Public Function GetAssessmentCriterionGroups(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of AssessmentCriterionGroup) Implements IMeasurementsDAL.GetAssessmentCriterionGroups

        If db Is Nothing Then db = Me.db

        Return db.AssessmentCriterionGroups.Include(Function(acg) acg.AssessmentCriteria) _
                                           .Include(Function(acg) acg.MeasurementType).OrderBy(Function(acg) acg.GroupName)

    End Function

    Public Function GetAssessmentCriterionGroup(ProjectShortName As String, GroupName As String) As AssessmentCriterionGroup Implements IMeasurementsDAL.GetAssessmentCriterionGroup

        Try

            Return GetAssessmentCriterionGroups().Single(Function(acg) LCase(acg.GroupName) = LCase(GroupName) AndAlso
                                                                       LCase(acg.Project.ShortName) = LCase(ProjectShortName))

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function UpdateAssessmentCriterionGroup(AssessmentCriterionGroup As AssessmentCriterionGroup) As AssessmentCriterionGroup Implements IMeasurementsDAL.UpdateAssessmentCriterionGroup

        Using db = New SECMonitoringDbContext

            Dim dbAssessmentCriterionGroup = GetAssessmentCriterionGroups(db).Single(Function(acg) acg.Id = AssessmentCriterionGroup.Id)

            ' Assessment
            dbAssessmentCriterionGroup.GroupName = AssessmentCriterionGroup.GroupName
            dbAssessmentCriterionGroup.ThresholdAggregateDuration = GetThresholdAggregateDuration(AssessmentCriterionGroup.ThresholdAggregateDuration.Id, db)
            dbAssessmentCriterionGroup.AssessmentPeriodDurationType = GetAssessmentPeriodDurationType(AssessmentCriterionGroup.AssessmentPeriodDurationType.Id, db)
            dbAssessmentCriterionGroup.AssessmentPeriodDurationCount = AssessmentCriterionGroup.AssessmentPeriodDurationCount

            ' Graph
            dbAssessmentCriterionGroup.ShowGraph = AssessmentCriterionGroup.ShowGraph
            dbAssessmentCriterionGroup.GraphTitle = AssessmentCriterionGroup.GraphTitle
            dbAssessmentCriterionGroup.GraphXAxisLabel = AssessmentCriterionGroup.GraphXAxisLabel
            dbAssessmentCriterionGroup.GraphYAxisLabel = AssessmentCriterionGroup.GraphYAxisLabel
            dbAssessmentCriterionGroup.GraphYAxisMin = AssessmentCriterionGroup.GraphYAxisMin
            dbAssessmentCriterionGroup.GraphYAxisMax = AssessmentCriterionGroup.GraphYAxisMax
            dbAssessmentCriterionGroup.GraphYAxisTickInterval = AssessmentCriterionGroup.GraphYAxisTickInterval

            ' Table
            dbAssessmentCriterionGroup.NumDateColumns = AssessmentCriterionGroup.NumDateColumns
            dbAssessmentCriterionGroup.DateColumn1Header = AssessmentCriterionGroup.DateColumn1Header
            dbAssessmentCriterionGroup.DateColumn1Format = AssessmentCriterionGroup.DateColumn1Format
            dbAssessmentCriterionGroup.DateColumn2Header = AssessmentCriterionGroup.DateColumn2Header
            dbAssessmentCriterionGroup.DateColumn2Format = AssessmentCriterionGroup.DateColumn2Format
            dbAssessmentCriterionGroup.MergeHeaderRow1 = AssessmentCriterionGroup.MergeHeaderRow1
            dbAssessmentCriterionGroup.MergeHeaderRow2 = AssessmentCriterionGroup.MergeHeaderRow2
            dbAssessmentCriterionGroup.MergeHeaderRow3 = AssessmentCriterionGroup.MergeHeaderRow3
            dbAssessmentCriterionGroup.ShowIndividualResults = AssessmentCriterionGroup.ShowIndividualResults
            dbAssessmentCriterionGroup.SumExceedancesAcrossCriteria = AssessmentCriterionGroup.SumExceedancesAcrossCriteria
            dbAssessmentCriterionGroup.SumPeriodExceedances = AssessmentCriterionGroup.SumPeriodExceedances
            dbAssessmentCriterionGroup.SumDaysWithExceedances = AssessmentCriterionGroup.SumDaysWithExceedances
            dbAssessmentCriterionGroup.SumDailyEvents = AssessmentCriterionGroup.SumDailyEvents
            dbAssessmentCriterionGroup.ShowSumTitles = AssessmentCriterionGroup.ShowSumTitles

            db.Entry(dbAssessmentCriterionGroup).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbAssessmentCriterionGroup

        End Using

    End Function

    Public Function GetThresholdAggregateDurations(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of ThresholdAggregateDuration) Implements IMeasurementsDAL.GetThresholdAggregateDurations

        If db Is Nothing Then db = Me.db

        Try

            Return db.ThresholdAggregateDurations _
           .Include(Function(tad) tad.AssessmentCriterionGroups).OrderBy(Function(tad) tad.AggregateDurationName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetThresholdAggregateDuration(ThresholdAggregateDurationId As Integer, Optional db As SECMonitoringDbContext = Nothing) As ThresholdAggregateDuration Implements IMeasurementsDAL.GetThresholdAggregateDuration

        If db Is Nothing Then db = Me.db

        Try

            Return GetThresholdAggregateDurations(db).Single(Function(tad) tad.Id = ThresholdAggregateDurationId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetAssessmentPeriodDurationTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of AssessmentPeriodDurationType) Implements IMeasurementsDAL.GetAssessmentPeriodDurationTypes

        If db Is Nothing Then db = Me.db

        Try

            Return db.AssessmentPeriodDurationTypes _
           .Include(Function(apdt) apdt.AssessmentCriterionGroups).OrderBy(Function(apdt) apdt.DurationTypeName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetAssessmentPeriodDurationType(AssessmentPeriodDurationTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As AssessmentPeriodDurationType Implements IMeasurementsDAL.GetAssessmentPeriodDurationType

        If db Is Nothing Then db = Me.db

        Try

            Return GetAssessmentPeriodDurationTypes(db).Single(Function(apdt) apdt.Id = AssessmentPeriodDurationTypeId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function AddAssessmentCriterionGroup(AssessmentCriterionGroup As AssessmentCriterionGroup) As AssessmentCriterionGroup Implements IMeasurementsDAL.AddAssessmentCriterionGroup

        Try

            db.AssessmentCriterionGroups.Add(AssessmentCriterionGroup)
            SaveChanges(db)
            Return AssessmentCriterionGroup

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetAssessmentCriterionGroupsForProject(ProjectId As Integer) As IEnumerable(Of AssessmentCriterionGroup) Implements IMeasurementsDAL.GetAssessmentCriterionGroupsForProject

        Try
            Return GetAssessmentCriterionGroups().Where(Function(acg) acg.Project.Id = ProjectId)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function DeleteAssessmentCriterionGroup(AssessmentCriterionGroupId As Integer) As Boolean Implements IMeasurementsDAL.DeleteAssessmentCriterionGroup

        Try

            Dim AssessmentCriterionGroupToDelete As AssessmentCriterionGroup = db.AssessmentCriterionGroups.Single(Function(ac) ac.Id = AssessmentCriterionGroupId)
            AssessmentCriterionGroupToDelete.AssessmentPeriodDurationType = Nothing
            AssessmentCriterionGroupToDelete.Project = Nothing
            AssessmentCriterionGroupToDelete.ExcludingMeasurementCommentTypes = Nothing
            AssessmentCriterionGroupToDelete.MeasurementType = Nothing
            AssessmentCriterionGroupToDelete.ThresholdAggregateDuration = Nothing
            db.AssessmentCriterionGroups.Remove(AssessmentCriterionGroupToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "Individual Criteria"

    Public Function AddAssessmentCriterion(AssessmentCriterion As AssessmentCriterion) As AssessmentCriterion Implements IMeasurementsDAL.AddAssessmentCriterion

        Try

            db.AssessmentCriteria.Add(AssessmentCriterion)
            SaveChanges(db)
            Return AssessmentCriterion

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetAssessmentCriteria(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of AssessmentCriterion) Implements IMeasurementsDAL.GetAssessmentCriteria

        If db Is Nothing Then db = Me.db

        Try

            Return db.AssessmentCriteria _
           .Include(Function(ac) ac.AssessmentCriterionGroup) _
           .Include(Function(ac) ac.CalculationFilter) _
           .Include(Function(ac) ac.MonitorLocation) _
           .Include(Function(ac) ac.ThresholdType).OrderBy(Function(ac) ac.CriterionIndex)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetAssessmentCriteria(CriteriaIds As IEnumerable(Of Integer), Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of AssessmentCriterion) Implements IMeasurementsDAL.GetAssessmentCriteria

        If db Is Nothing Then db = Me.db

        Try

            Return GetAssessmentCriteria(db).Where(Function(c) CriteriaIds.Contains(c.Id))

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetAssessmentCriterion(AssessmentCriterionId As Integer, Optional db As SECMonitoringDbContext = Nothing) As AssessmentCriterion Implements IMeasurementsDAL.GetAssessmentCriterion

        If db Is Nothing Then db = Me.db

        Try

            Return GetAssessmentCriteria(db).Single(Function(ac) ac.Id = AssessmentCriterionId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function DeleteAssessmentCriterion(AssessmentCriterionId As Integer) As Boolean Implements IMeasurementsDAL.DeleteAssessmentCriterion

        Try

            Dim AssessmentCriterionToDelete As AssessmentCriterion = db.AssessmentCriteria.Single(Function(ac) ac.Id = AssessmentCriterionId)
            Dim assessmentCriterionIndex = AssessmentCriterionToDelete.CriterionIndex
            Dim assessmentCriterionGroupId = AssessmentCriterionToDelete.AssessmentCriterionGroupId
            ' Remove references and delete
            AssessmentCriterionToDelete.AssessmentCriterionGroup = Nothing
            AssessmentCriterionToDelete.CalculationFilter = Nothing
            AssessmentCriterionToDelete.MonitorLocation = Nothing
            db.AssessmentCriteria.Remove(AssessmentCriterionToDelete)
            ' Reindex other criteria
            Dim criteriaToReindex = db.AssessmentCriteria.Where(
                Function(ac) ac.AssessmentCriterionGroupId = assessmentCriterionGroupId And
                             ac.CriterionIndex > assessmentCriterionIndex
                ).ToList()
            For Each criterion In criteriaToReindex
                criterion.CriterionIndex -= 1
            Next

            SaveChanges(db)

            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function
    Public Function UpdateAssessmentCriterion(AssessmentCriterion As AssessmentCriterion) As AssessmentCriterion Implements IMeasurementsDAL.UpdateAssessmentCriterion

        Using db = New SECMonitoringDbContext

            Dim dbAC = GetAssessmentCriteria(db).Single(Function(ac) ac.Id = AssessmentCriterion.Id)
            Dim c = AssessmentCriterion
            dbAC.AssessedLevelDashStyle = GetSeriesDashStyle(c.AssessedLevelDashStyleId, db)
            dbAC.AssessedLevelHeader1 = c.AssessedLevelHeader1
            dbAC.AssessedLevelHeader2 = c.AssessedLevelHeader2
            dbAC.AssessedLevelHeader3 = c.AssessedLevelHeader3
            dbAC.AssessedLevelLineColour = c.AssessedLevelLineColour
            dbAC.AssessedLevelLineWidth = c.AssessedLevelLineWidth
            dbAC.AssessedLevelMarkersOn = c.AssessedLevelMarkersOn
            dbAC.AssessedLevelSeriesName = c.AssessedLevelSeriesName
            dbAC.AssessedLevelSeriesType = GetMeasurementViewSeriesType(c.AssessedLevelSeriesTypeId, db)
            dbAC.CalculationFilter = GetCalculationFilter(c.CalculationFilterId, db)
            dbAC.CriterionLevelDashStyle = GetSeriesDashStyle(c.CriterionLevelDashStyleId, db)
            dbAC.CriterionLevelLineColour = c.CriterionLevelLineColour
            dbAC.CriterionLevelLineWidth = c.CriterionLevelLineWidth
            dbAC.CriterionLevelSeriesName = c.CriterionLevelSeriesName
            dbAC.CriterionTriggerHeader1 = c.CriterionTriggerHeader1
            dbAC.CriterionTriggerHeader2 = c.CriterionTriggerHeader2
            dbAC.CriterionTriggerHeader3 = c.CriterionTriggerHeader3
            dbAC.MergeAssessedLevels = c.MergeAssessedLevels
            dbAC.MergeCriterionTriggers = c.MergeCriterionTriggers
            dbAC.PlotAssessedLevel = c.PlotAssessedLevel
            dbAC.PlotCriterionLevel = c.PlotCriterionLevel
            dbAC.RoundingDecimalPlaces = c.RoundingDecimalPlaces
            dbAC.TabulateAssessedLevels = c.TabulateAssessedLevels
            dbAC.TabulateCriterionTriggers = c.TabulateCriterionTriggers
            dbAC.ThresholdRangeLower = c.ThresholdRangeLower
            dbAC.ThresholdRangeUpper = c.ThresholdRangeUpper
            dbAC.ThresholdType = GetThresholdType(c.ThresholdType.Id, db)

            db.Entry(dbAC).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbAC

        End Using

    End Function
    Public Function DecreaseAssessmentCriterionIndex(
        AssessmentCriterionId As Integer, Optional db As SECMonitoringDbContext = Nothing
    ) As Boolean Implements IMeasurementsDAL.DecreaseAssessmentCriterionIndex

        If db Is Nothing Then db = Me.db

        Try

            Dim assessmentCriterion = GetAssessmentCriterion(AssessmentCriterionId)
            Dim assessmentCriterionGroup = assessmentCriterion.AssessmentCriterionGroup
            If assessmentCriterion.CriterionIndex = 1 Then Return False
            Dim belowAssessmentCriterion = assessmentCriterionGroup.AssessmentCriteria.Single(
                Function(ac) ac.CriterionIndex = assessmentCriterion.CriterionIndex - 1 And ac.MonitorLocationId = assessmentCriterion.MonitorLocationId
            )
            assessmentCriterion.CriterionIndex -= 1
            belowAssessmentCriterion.CriterionIndex += 1
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function
    Public Function IncreaseAssessmentCriterionIndex(
        AssessmentCriterionId As Integer, Optional db As SECMonitoringDbContext = Nothing
    ) As Boolean Implements IMeasurementsDAL.IncreaseAssessmentCriterionIndex

        If db Is Nothing Then db = Me.db

        Try

            Dim assessmentCriterion = GetAssessmentCriterion(AssessmentCriterionId)
            Dim assessmentCriterionGroup = assessmentCriterion.AssessmentCriterionGroup
            If assessmentCriterion.CriterionIndex = assessmentCriterionGroup.AssessmentCriteria.Where(
                Function(ac) ac.MonitorLocationId = assessmentCriterion.MonitorLocationId
            ).Count Then Return False
            Dim aboveAssessmentCriterion = assessmentCriterionGroup.AssessmentCriteria.Single(
                Function(ac) ac.CriterionIndex = assessmentCriterion.CriterionIndex + 1 And ac.MonitorLocationId = assessmentCriterion.MonitorLocationId
            )
            assessmentCriterion.CriterionIndex += 1
            aboveAssessmentCriterion.CriterionIndex -= 1
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function


#End Region


#End Region

#Region "Calculation Aggregate Functions"

    Public Function GetCalculationAggregateFunctions(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of CalculationAggregateFunction) Implements IMeasurementsDAL.GetCalculationAggregateFunctions

        If db Is Nothing Then db = Me.db

        Try

            Return db.CalculationAggregateFunctions _
           .Include(Function(caf) caf.CalculationFilters).OrderBy(Function(caf) caf.Id)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetCalculationAggregateFunction(CalculationAggregateFunctionId As Integer, Optional db As SECMonitoringDbContext = Nothing) As CalculationAggregateFunction Implements IMeasurementsDAL.GetCalculationAggregateFunction

        If db Is Nothing Then db = Me.db

        Try

            Return GetCalculationAggregateFunctions(db).Single(Function(caf) caf.Id = CalculationAggregateFunctionId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

#End Region

#Region "Calculation Filters"

    Public Function DeleteCalculationFilter(CalculationFilterId As Integer) As Boolean Implements IMeasurementsDAL.DeleteCalculationFilter

        Try

            Dim CalculationFilterToDelete = db.CalculationFilters.Single(Function(cf) cf.Id = CalculationFilterId)
            db.CalculationFilters.Remove(CalculationFilterToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function
    Public Function GetCalculationFilters(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of CalculationFilter) Implements IMeasurementsDAL.GetCalculationFilters

        If db Is Nothing Then db = Me.db

        Try

            Return db.CalculationFilters _
           .Include(Function(cf) cf.ApplicableDaysOfWeek) _
           .Include(Function(cf) cf.CalculationAggregateFunction) _
           .Include(Function(cf) cf.MeasurementMetric).OrderBy(Function(cf) cf.FilterName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetCalculationFilter(FilterName As String) As CalculationFilter Implements IMeasurementsDAL.GetCalculationFilter

        Try

            Return GetCalculationFilters().Single(
                Function(cf) LCase(cf.FilterName) = LCase(FilterName)
            )

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetCalculationFilter(CalculationFilterId As Integer, Optional db As SECMonitoringDbContext = Nothing) As CalculationFilter Implements IMeasurementsDAL.GetCalculationFilter

        If db Is Nothing Then db = Me.db

        Try

            Return GetCalculationFilters(db).Single(
                Function(cf) cf.Id = CalculationFilterId
            )

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetCalculationFiltersForMeasurementType(MeasurementTypeId As Integer) As IEnumerable(Of CalculationFilter) Implements IMeasurementsDAL.GetCalculationFiltersForMeasurementType

        Try
            Return GetCalculationFilters().Where(Function(cf) cf.MeasurementMetric.MeasurementTypeId = MeasurementTypeId)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function UpdateCalculationFilter(CalculationFilter As CalculationFilter) As CalculationFilter Implements IMeasurementsDAL.UpdateCalculationFilter

        Using db = New SECMonitoringDbContext

            Dim dbCalculationFilter = GetCalculationFilters(db).Single(Function(cf) cf.Id = CalculationFilter.Id)

            dbCalculationFilter.FilterName = CalculationFilter.FilterName
            dbCalculationFilter.InputMultiplier = CalculationFilter.InputMultiplier
            dbCalculationFilter.CalculationAggregateFunction = GetCalculationAggregateFunction(CalculationFilter.CalculationAggregateFunction.Id, db)
            dbCalculationFilter.MeasurementMetric = GetMeasurementMetric(CalculationFilter.MeasurementMetric.Id, db)
            dbCalculationFilter.TimeBase = CalculationFilter.TimeBase
            dbCalculationFilter.UseTimeWindow = CalculationFilter.UseTimeWindow
            dbCalculationFilter.TimeWindowStartTime = CalculationFilter.TimeWindowStartTime
            dbCalculationFilter.TimeWindowEndTime = CalculationFilter.TimeWindowEndTime
            dbCalculationFilter.TimeStep = CalculationFilter.TimeStep

            db.Entry(dbCalculationFilter).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbCalculationFilter

        End Using

    End Function
    Public Function AddCalculationFilterApplicableDayOfWeek(CalculationFilterId As Integer, ApplicableDayOfWeekId As Integer) As Boolean Implements IMeasurementsDAL.AddCalculationFilterApplicableDayOfWeek

        Try
            Dim obj = GetCalculationFilter(CalculationFilterId)
            obj.ApplicableDaysOfWeek.Add(GetDayOfWeek(ApplicableDayOfWeekId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveCalculationFilterApplicableDayOfWeek(CalculationFilterId As Integer, ApplicableDayOfWeekId As Integer) As Boolean Implements IMeasurementsDAL.RemoveCalculationFilterApplicableDayOfWeek

        Try
            Dim obj = GetCalculationFilter(CalculationFilterId)
            obj.ApplicableDaysOfWeek.Remove(GetDayOfWeek(ApplicableDayOfWeekId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function AddCalculationFilter(CalculationFilter As CalculationFilter) As CalculationFilter Implements IMeasurementsDAL.AddCalculationFilter

        Try

            db.CalculationFilters.Add(CalculationFilter)
            SaveChanges(db)
            Return CalculationFilter

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

#End Region

#Region "Contacts"

    Public Function GetContacts(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Contact) Implements IMeasurementsDAL.GetContacts

        If db Is Nothing Then db = Me.db

        Return db.Contacts.Include(Function(c) c.Organisation) _
                          .Include(Function(c) c.Projects).OrderBy(Function(c) c.ContactName)

    End Function
    Public Function GetContact(ContactName As String) As Contact Implements IMeasurementsDAL.GetContact

        Try

            Return GetContacts().Single(Function(c) LCase(c.ContactName) = LCase(ContactName))

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetContact(ContactId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Contact Implements IMeasurementsDAL.GetContact

        If db Is Nothing Then db = Me.db

        Try
            Return GetContacts(db).Single(Function(c) c.Id = ContactId)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function AddContact(Contact As Contact) As Contact Implements IMeasurementsDAL.AddContact

        Try

            db.Contacts.Add(Contact)
            SaveChanges(db)
            Return Contact

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetContacts(Project As Project) As IEnumerable(Of Contact) Implements IMeasurementsDAL.GetContacts

        Return GetContacts().Where(Function(c) c.Projects.Contains(Project))

    End Function
    Public Function UpdateContact(Contact As Contact) As Contact Implements IMeasurementsDAL.UpdateContact

        Using db = New SECMonitoringDbContext

            Dim dbContact = GetContacts(db).Single(Function(c) c.Id = Contact.Id)

            dbContact.ContactName = Contact.ContactName
            dbContact.EmailAddress = Contact.EmailAddress
            dbContact.PrimaryTelephoneNumber = Contact.PrimaryTelephoneNumber
            dbContact.SecondaryTelephoneNumber = Contact.SecondaryTelephoneNumber
            dbContact.Organisation = GetOrganisation(Contact.Organisation.Id, db)

            db.Entry(dbContact).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbContact

        End Using

    End Function
    Public Function AddContactProject(ContactId As Integer, ProjectId As Integer) As Boolean Implements IMeasurementsDAL.AddContactProject

        Try
            Dim obj = GetContact(ContactId)
            obj.Projects.Add(GetProject(ProjectId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function AddContactExcludedDocument(ContactId As Integer, ExcludedDocumentId As Integer) As Boolean Implements IMeasurementsDAL.AddContactExcludedDocument

        Try
            Dim obj = GetContact(ContactId)
            obj.ExcludedDocuments.Add(GetDocument(ExcludedDocumentId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveContactProject(ContactId As Integer, ProjectId As Integer) As Boolean Implements IMeasurementsDAL.RemoveContactProject

        Try
            Dim obj = GetContact(ContactId)
            obj.Projects.Remove(GetProject(ProjectId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveContactExcludedDocument(ContactId As Integer, ExcludedDocumentId As Integer) As Boolean Implements IMeasurementsDAL.RemoveContactExcludedDocument

        Try
            Dim obj = GetContact(ContactId)
            obj.ExcludedDocuments.Remove(GetDocument(ExcludedDocumentId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function DeleteContact(ContactId As Integer) As Boolean Implements IMeasurementsDAL.DeleteContact

        Try

            Dim ContactToDelete As Contact = db.Contacts.Single(Function(cf) cf.Id = ContactId)
            db.Contacts.Remove(ContactToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "Countries"

    Public Function GetCountries(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Country) Implements IMeasurementsDAL.GetCountries

        If db Is Nothing Then db = Me.db

        Try

            Return db.Countries _
           .Include(Function(c) c.PublicHolidays) _
           .Include(Function(c) c.Projects).OrderBy(Function(c) c.CountryName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetCountry(CountryId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Country Implements IMeasurementsDAL.GetCountry

        If db Is Nothing Then db = Me.db

        Try

            Return GetCountries(db).Single(Function(c) c.Id = CountryId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetCountry(CountryName As String) As Country Implements IMeasurementsDAL.GetCountry

        Try

            Return GetCountries().Single(Function(c) LCase(c.CountryName) = LCase(CountryName))

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function AddCountry(Country As Country) As Country Implements IMeasurementsDAL.AddCountry

        Try

            db.Countries.Add(Country)
            SaveChanges(db)
            Return Country

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function UpdateCountry(Country As Country) As Country Implements IMeasurementsDAL.UpdateCountry

        Try
            db.Countries.Attach(Country)
            db.Entry(Country).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return Country
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function DeleteCountry(CountryId As Integer) As Boolean Implements IMeasurementsDAL.DeleteCountry

        Try

            Dim CountryToDelete As Country = db.Countries.Single(Function(cf) cf.Id = CountryId)
            db.Countries.Remove(CountryToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "Days of Week"

    Public Function GetDaysOfWeek(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of DayOfWeek) Implements IMeasurementsDAL.GetDaysOfWeek

        If db Is Nothing Then db = Me.db

        Try

            Return db.DaysOfWeek.OrderBy(Function(DoW) DoW.Id)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetDayOfWeek(DayOfWeekId As Integer, Optional db As SECMonitoringDbContext = Nothing) As DayOfWeek Implements IMeasurementsDAL.GetDayOfWeek

        If db Is Nothing Then db = Me.db

        Try

            Return GetDaysOfWeek(db).Single(Function(dow) dow.Id = DayOfWeekId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

#End Region

#Region "Documents"

    Public Function GetDocuments(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Document) Implements IMeasurementsDAL.GetDocuments

        If db Is Nothing Then db = Me.db

        Try

            Return db.Documents _
           .Include(Function(d) d.AuthorOrganisation) _
           .Include(Function(d) d.DocumentType) _
           .Include(Function(d) d.ExcludedContacts) _
           .Include(Function(d) d.MonitorLocations) _
           .Include(Function(d) d.Monitors).OrderBy(Function(d) d.Title)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetDocuments(ProjectId As Integer, Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Document) Implements IMeasurementsDAL.GetDocuments

        If db Is Nothing Then db = Me.db

        Try
            Return GetDocuments().Where(Function(d) d.Projects.Select(Function(p) p.Id).ToList.Contains(ProjectId))
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function GetDocument(DocumentId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Document Implements IMeasurementsDAL.GetDocument

        If db Is Nothing Then db = Me.db

        Try

            Return GetDocuments(db).Single(Function(d) d.Id = DocumentId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetDocument(PartialFilePath As String) As Document Implements IMeasurementsDAL.GetDocument

        Try

            Return db.Documents.Single(Function(d) LCase(d.FilePath).Contains(LCase(PartialFilePath)))

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function AddDocument(Document As Document) As Document Implements IMeasurementsDAL.AddDocument

        Try

            Dim dbDocument = db.Documents.Add(Document)
            SaveChanges(db)
            Return dbDocument

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function UpdateDocument(Document As Document) As Document Implements IMeasurementsDAL.UpdateDocument

        Using db = New SECMonitoringDbContext

            Dim dbDocument = GetDocuments(db).Single(Function(d) d.Id = Document.Id)

            dbDocument.Title = Document.Title
            dbDocument.StartDateTime = Document.StartDateTime
            dbDocument.EndDateTime = Document.EndDateTime
            dbDocument.DocumentType = GetDocumentType(Document.DocumentType.Id, db)
            dbDocument.AuthorOrganisation = GetOrganisation(Document.AuthorOrganisation.Id, db)

            db.Entry(dbDocument).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbDocument

        End Using

    End Function

    Public Function DeleteDocument(DocumentId As Integer) As Boolean Implements IMeasurementsDAL.DeleteDocument

        Try

            Dim DocumentToDelete As Document = db.Documents.Single(Function(d) d.Id = DocumentId)
            DocumentToDelete.Projects = Nothing
            DocumentToDelete.MonitorLocations = Nothing
            DocumentToDelete.Monitors = Nothing
            db.Documents.Remove(DocumentToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function



    Public Function AddDocumentProject(DocumentId As Integer, ProjectId As Integer) As Boolean Implements IMeasurementsDAL.AddDocumentProject

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = GetDocument(DocumentId, db)
                obj.Projects.Add(GetProject(ProjectId, db))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function AddDocumentMonitor(DocumentId As Integer, MonitorId As Integer) As Boolean Implements IMeasurementsDAL.AddDocumentMonitor

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = GetDocument(DocumentId, db)
                obj.Monitors.Add(GetMonitor(MonitorId, db))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function AddDocumentMonitorLocation(DocumentId As Integer, MonitorLocationId As Integer) As Boolean Implements IMeasurementsDAL.AddDocumentMonitorLocation

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = GetDocument(DocumentId, db)
                obj.MonitorLocations.Add(GetMonitorLocation(MonitorLocationId, db))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function AddDocumentExcludedContact(DocumentId As Integer, ExcludedContactId As Integer) As Boolean Implements IMeasurementsDAL.AddDocumentExcludedContact

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = GetDocument(DocumentId, db)
                obj.ExcludedContacts.Add(GetContact(ExcludedContactId, db))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function RemoveDocumentProject(DocumentId As Integer, ProjectId As Integer) As Boolean Implements IMeasurementsDAL.RemoveDocumentProject

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = GetDocument(DocumentId, db)
                obj.Projects.Remove(GetProject(ProjectId, db))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function RemoveDocumentMonitor(DocumentId As Integer, MonitorId As Integer) As Boolean Implements IMeasurementsDAL.RemoveDocumentMonitor

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = GetDocument(DocumentId, db)
                obj.Monitors.Remove(GetMonitor(MonitorId, db))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function RemoveDocumentMonitorLocation(DocumentId As Integer, MonitorLocationId As Integer) As Boolean Implements IMeasurementsDAL.RemoveDocumentMonitorLocation

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = GetDocument(DocumentId, db)
                obj.MonitorLocations.Remove(GetMonitorLocation(MonitorLocationId, db))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function RemoveDocumentExcludedContact(DocumentId As Integer, ExcludedContactId As Integer) As Boolean Implements IMeasurementsDAL.RemoveDocumentExcludedContact

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = GetDocument(DocumentId, db)
                obj.ExcludedContacts.Remove(GetContact(ExcludedContactId, db))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "Document Types"

    Public Function GetDocumentTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of DocumentType) Implements IMeasurementsDAL.GetDocumentTypes

        If db Is Nothing Then db = Me.db

        Try

            Return db.DocumentTypes _
           .Include(Function(dt) dt.Documents) _
           .OrderBy(Function(dt) dt.DocumentTypeName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetDocumentType(DocumentTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As DocumentType Implements IMeasurementsDAL.GetDocumentType

        If db Is Nothing Then db = Me.db

        Try

            Return GetDocumentTypes(db).Single(Function(dt) dt.Id = DocumentTypeId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetDocumentType(DocumentTypeName As String) As DocumentType Implements IMeasurementsDAL.GetDocumentType

        Try

            Return GetDocumentTypes().Single(Function(dt) LCase(dt.DocumentTypeName) = LCase(DocumentTypeName))

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetDocumentTypesSelectList(Include0AsAll As Boolean,
                                               Optional db As SECMonitoringDbContext = Nothing) As SelectList Implements IMeasurementsDAL.GetDocumentTypesSelectList

        If db Is Nothing Then db = Me.db

        Dim documentTypes = Me.GetDocumentTypes()

        Dim dtList As New List(Of SelectListItem)
        If Include0AsAll Then
            dtList.Add(New SelectListItem With {.Value = 0,
                                                .Text = "All"})
        End If
        For Each dt In documentTypes
            dtList.Add(New SelectListItem With {.Value = dt.Id,
                                                .Text = dt.DocumentTypeName})
        Next

        Dim selItem As String = ""
        If Include0AsAll Then
            selItem = "All"
        End If
        Return New SelectList(dtList, "Value", "Text", selItem)

    End Function
    Public Function AddDocumentType(DocumentType As DocumentType) As DocumentType Implements IMeasurementsDAL.AddDocumentType

        Try

            db.DocumentTypes.Add(DocumentType)
            SaveChanges(db)
            Return DocumentType

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function UpdateDocumentType(DocumentType As DocumentType) As DocumentType Implements IMeasurementsDAL.UpdateDocumentType

        Using db = New SECMonitoringDbContext

            Dim dbDocumentType = GetDocumentTypes(db).Single(Function(dt) dt.Id = DocumentType.Id)

            dbDocumentType.DocumentTypeName = DocumentType.DocumentTypeName

            db.Entry(dbDocumentType).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbDocumentType

        End Using

    End Function
    Public Function DeleteDocumentType(DocumentTypeId As Integer) As Boolean Implements IMeasurementsDAL.DeleteDocumentType

        Try

            Dim DocumentTypeToDelete As DocumentType = db.DocumentTypes.Single(Function(cf) cf.Id = DocumentTypeId)
            db.DocumentTypes.Remove(DocumentTypeToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function


#End Region

#Region "Measurements"

    Public Function ReadMeasurements(RMP As IReadMeasurementParameters) As IEnumerable(Of Measurement) Implements IMeasurementsDAL.ReadMeasurements

        Dim measurements = db.Measurements.Include(Function(m) m.Monitor) _
                                           .Include(Function(m) m.MonitorLocation) _
                                           .Include(Function(m) m.MeasurementMetric) _
                                           .Include(Function(m) m.Project).OrderBy(Function(m) m.StartDateTime)

        If RMP.FilterText <> "" Then measurements = measurements.Where(Function(data) data.Monitor.MonitorName.Contains(RMP.FilterText) Or
                                                                                      data.MeasurementMetric.MetricName.Contains(RMP.FilterText))
        If RMP.MeasurementType IsNot Nothing Then measurements = measurements.Where(Function(data) data.MeasurementMetric.MeasurementType.MeasurementTypeName = RMP.MeasurementType.MeasurementTypeName)
        If RMP.StartDate.CompareTo(Date.MinValue) > 0 Then measurements = measurements.Where(Function(data) data.StartDateTime >= RMP.StartDate)
        If RMP.EndDate.CompareTo(Date.MinValue) > 0 Then measurements = measurements.Where(Function(data) data.StartDateTime < RMP.EndDate)
        If RMP.Metric IsNot Nothing Then measurements = measurements.Where(Function(data) data.MeasurementMetric.MetricName = RMP.Metric.MetricName)
        If RMP.Monitor IsNot Nothing Then measurements = measurements.Where(Function(data) data.Monitor.MonitorName = RMP.Monitor.MonitorName)
        If RMP.MonitorLocation IsNot Nothing Then measurements = measurements.Where(Function(data) data.MonitorLocation.MonitorLocationName = RMP.MonitorLocation.MonitorLocationName AndAlso _
                                                                                                   data.MonitorLocation.Project.FullName = RMP.MonitorLocation.Project.FullName)
        Return measurements.ToList

    End Function
    Public Function AddMeasurement(Measurement As Measurement) As Measurement Implements IMeasurementsDAL.AddMeasurement

        Try

            db.Measurements.Add(Measurement)
            SaveChanges(db)
            Return Measurement

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurements(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Measurement) Implements IMeasurementsDAL.GetMeasurements

        If db Is Nothing Then db = Me.db

        Try

            Return db.Measurements _
           .Include(Function(m) m.MeasurementComments) _
           .Include(Function(m) m.MeasurementFile) _
           .Include(Function(m) m.MeasurementMetric) _
           .Include(Function(m) m.Monitor) _
           .Include(Function(m) m.MonitorLocation) _
           .Include(Function(m) m.Project).OrderBy(Function(m) m.StartDateTime)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurement(MeasurementId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Measurement Implements IMeasurementsDAL.GetMeasurement

        If db Is Nothing Then db = Me.db

        Try

            Return GetMeasurements(db).Single(Function(m) m.Id = MeasurementId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function DeleteMeasurement(MeasurementId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMeasurement

        Try

            Dim sql As String = "DELETE FROM Measurements WHERE Id =" + MeasurementId.ToString
            Using conn As New SqlConnection(MeasurementsConnectionString)
                Dim cmd As New SqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                    Return False
                End Try
            End Using
            Return True

        Catch ex As Exception

            Debug.WriteLine(ex.Message)
            Return False

        End Try

    End Function
    Public Function DeleteMeasurementsForFile(MeasurementFileId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMeasurementsForFile

        Try

            Dim sql As String = "DELETE FROM Measurements WHERE MeasurementFileId =" + MeasurementFileId.ToString
            Using conn As New SqlConnection(MeasurementsConnectionString)
                Dim cmd As New SqlCommand(sql, conn)
                Try
                    conn.Open()
                    cmd.ExecuteNonQuery()
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                    Return False
                End Try
            End Using
            Return True

        Catch ex As Exception

            Debug.WriteLine(ex.Message)
            Return False

        End Try

    End Function


#Region "SQL Queries"

    Public Function TryAddMeasurements(Measurements As IEnumerable(Of Measurement)) As Boolean Implements IMeasurementsDAL.TryAddMeasurements

        Try

            Dim connectionString = MeasurementsConnectionString
            Dim numMeasurements = Measurements.Count

            For mFirst = 0 To numMeasurements - 1 Step 100

                Dim iterationMeasurements = Measurements.Skip(mFirst).Take(100)

                Using connection As New SqlConnection(connectionString)
                    connection.Open()
                    Using sqlBulkCopy As New SqlBulkCopy(connection)

                        sqlBulkCopy.ColumnMappings.Add("Id", "Id")
                        sqlBulkCopy.ColumnMappings.Add("StartDateTime", "StartDateTime")
                        sqlBulkCopy.ColumnMappings.Add("Duration", "Duration")
                        sqlBulkCopy.ColumnMappings.Add("Level", "Level")
                        sqlBulkCopy.ColumnMappings.Add("Overload", "Overload")
                        sqlBulkCopy.ColumnMappings.Add("Underload", "Underload")
                        sqlBulkCopy.ColumnMappings.Add("MeasurementMetricId", "MeasurementMetricId")
                        sqlBulkCopy.ColumnMappings.Add("MonitorId", "MonitorId")
                        sqlBulkCopy.ColumnMappings.Add("MonitorLocationId", "MonitorLocationId")
                        sqlBulkCopy.ColumnMappings.Add("ProjectId", "ProjectId")
                        sqlBulkCopy.ColumnMappings.Add("DateTimeUploaded", "DateTimeUploaded")
                        sqlBulkCopy.ColumnMappings.Add("UploadContactId", "UploadContactId")
                        sqlBulkCopy.ColumnMappings.Add("MeasurementFileId", "MeasurementFileId")

                        sqlBulkCopy.DestinationTableName = "Measurements"

                        Dim orderedCols = {"Id", "StartDateTime", "Duration", "Level", "Overload", "Underload",
                                           "MeasurementMetricId", "MonitorId", "MonitorLocationId", "ProjectId",
                                           "DateTimeUploaded", "UploadContactId", "MeasurementFileId"}

                        sqlBulkCopy.WriteToServer(ConvertToDataTable(iterationMeasurements.ToList, orderedCols))

                    End Using
                End Using

            Next

            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    Public Function GetObjectFirstMeasurementStartDateTime(ObjectName As String, ObjectId As Integer) As Date Implements IMeasurementsDAL.GetObjectFirstMeasurementStartDateTime

        Dim objectFirstMeasurementStartDateTime As Date
        Dim sql As String = "SELECT MIN(StartDateTime) as FirstStartDateTime FROM Measurements WHERE " + ObjectName + "Id = " + ObjectId.ToString
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                objectFirstMeasurementStartDateTime = Convert.ToDateTime(cmd.ExecuteScalar())
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return objectFirstMeasurementStartDateTime

    End Function
    Public Function GetObjectLastMeasurementStartDateTime(ObjectName As String, ObjectId As Integer) As Date Implements IMeasurementsDAL.GetObjectLastMeasurementStartDateTime

        Dim ObjectLastMeasurementStartDateTime As Date
        Dim sql As String = "SELECT MAX(StartDateTime) as LastStartDateTime FROM Measurements WHERE " + ObjectName + "Id = " + ObjectId.ToString
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                ObjectLastMeasurementStartDateTime = Convert.ToDateTime(cmd.ExecuteScalar())
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return ObjectLastMeasurementStartDateTime

    End Function
    Public Function GetObjectLastMeasurementDuration(ObjectName As String, ObjectId As Integer) As Double Implements IMeasurementsDAL.GetObjectLastMeasurementDuration

        Dim ObjectLastMeasurementDuration As Double
        Dim sql As String = "SELECT MAX(Duration) FROM Measurements WHERE " + ObjectName + "Id = " + ObjectId.ToString + " AND StartDateTime = (SELECT MAX(StartDateTime) FROM Measurements WHERE " + ObjectName + "Id = " + ObjectId.ToString + ")"
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                ObjectLastMeasurementDuration = Convert.ToDouble(cmd.ExecuteScalar())
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return ObjectLastMeasurementDuration

    End Function
    Public Function GetObjectLastMeasurementEndDateTime(ObjectName As String, ObjectId As Integer) As Date Implements IMeasurementsDAL.GetObjectLastMeasurementEndDateTime

        Return GetObjectLastMeasurementStartDateTime(ObjectName, ObjectId).AddDays(GetObjectLastMeasurementDuration(ObjectName, ObjectId))

    End Function
    Public Function GetObjectMeasurementMetricIds(ObjectName As String, ObjectId As Integer) As List(Of Integer) Implements IMeasurementsDAL.GetObjectMeasurementMetricIds

        Dim ObjectMeasurementMetricIds As New List(Of Integer)
        Dim sql As String = "SELECT DISTINCT MeasurementMetricId FROM Measurements WHERE " + ObjectName + "Id = " + ObjectId.ToString
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    ObjectMeasurementMetricIds.Add(rdr.GetInt32(0))
                End While
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return ObjectMeasurementMetricIds

    End Function

    Public Function GetCommentMeasurementIds(CommentIds As List(Of Integer)) As List(Of Integer) Implements IMeasurementsDAL.GetCommentMeasurementIds

        Dim MeasurementIds As New List(Of Integer)
        If CommentIds.Count = 0 Then Return MeasurementIds

        Dim sql As String = "SELECT Measurement_Id FROM MeasurementMeasurementComments WHERE MeasurementComment_Id IN (" + libSEC.Lists.ToCSVstring(CommentIds) + ")"
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    MeasurementIds.Add(rdr.GetInt32(0))
                End While
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return MeasurementIds

    End Function

    Public Function GetCommentTypeCommentIds(CommentTypeIds As List(Of Integer)) As List(Of Integer) Implements IMeasurementsDAL.GetCommentTypeCommentIds

        Dim MeasurementIds As New List(Of Integer)

        If CommentTypeIds.Count = 0 Then Return MeasurementIds

        Dim sql As String = "SELECT Id FROM MeasurementComments WHERE CommentTypeId IN (" + libSEC.Lists.ToCSVstring(CommentTypeIds) + ")"
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    MeasurementIds.Add(rdr.GetInt32(0))
                End While
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return MeasurementIds

    End Function

    Public Function ObjectHasMeasurements(ObjectName As String, ObjectId As Integer) As Boolean Implements IMeasurementsDAL.ObjectHasMeasurements

        Dim hasMeasurements As Int32 = 0
        Dim sql As String = "SELECT CASE WHEN EXISTS (SELECT * FROM [Measurements] WHERE [" + ObjectName + "Id] = " + ObjectId.ToString + ") THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END"
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                hasMeasurements = Convert.ToBoolean(cmd.ExecuteScalar())
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return hasMeasurements

    End Function

    Public Function GetObjectMeasurements(
        ObjectName As String, ObjectId As Integer, Optional OrderBy As String = ""
    ) As List(Of Measurement) Implements IMeasurementsDAL.GetObjectMeasurements

        Dim ObjectMeasurements As New List(Of Measurement)
        Dim sql As String = (
            "SELECT" +
            " Id, StartDateTime, Duration, Level, Overload, Underload," +
            " MeasurementMetricId, MonitorId, MonitorLocationId, ProjectId, DateTimeUploaded, UploadContactId, MeasurementFileId" + _
            " FROM Measurements " + _
            " WHERE " + ObjectName + "Id = " + ObjectId.ToString + _
            If(OrderBy <> "", " ORDER BY " + OrderBy, "")
        )
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    Dim measurement = New Measurement With {
                        .Id = rdr.GetInt32(0),
                        .StartDateTime = rdr.GetDateTime(1),
                        .Duration = rdr.GetDouble(2),
                        .Level = rdr.GetDouble(3),
                        .Overload = If(rdr.IsDBNull(4), Nothing, rdr.GetBoolean(4)),
                        .Underload = If(rdr.IsDBNull(5), Nothing, rdr.GetBoolean(5)),
                        .MeasurementMetricId = rdr.GetInt32(6),
                        .MonitorId = rdr.GetInt32(7),
                        .MonitorLocationId = rdr.GetInt32(8),
                        .ProjectId = rdr.GetInt32(9),
                        .DateTimeUploaded = rdr.GetDateTime(10),
                        .UploadContactId = rdr.GetInt32(11),
                        .MeasurementFileId = rdr.GetInt32(12)
                    }
                    ObjectMeasurements.Add(measurement)
                End While
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return ObjectMeasurements

    End Function

    Public Function GetMonitorLocationMeasurements(MonitorLocationId As Integer, StartDateTime As Date, EndDateTime As Date) As List(Of Measurement) Implements IMeasurementsDAL.GetMonitorLocationMeasurements

        Dim monitorLocationMeasurements As New List(Of Measurement)
        Dim sql As String = "SELECT Id, StartDateTime, Duration, Level, Overload, Underload, MeasurementMetricId, MonitorId, MonitorLocationId, ProjectId, DateTimeUploaded, UploadContactId, MeasurementFileId" + _
                            " FROM Measurements " + _
                            " WHERE MonitorLocationId = " + MonitorLocationId.ToString + _
                            " AND (StartDateTime BETWEEN '" + StartDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' AND '" + EndDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')" + _
                            " ORDER BY StartDateTime"

        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            ' Open the connection to the database
            Try
                conn.Open()
            Catch ex As Exception
                Console.WriteLine("Error opening connection to database")
                Console.WriteLine(ex.Message)
                Console.WriteLine(ex.Source)
                Return monitorLocationMeasurements
            End Try
            ' Read the measurements from the database
            Try
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    Dim measurement = New Measurement With {
                        .Id = rdr.GetInt32(0),
                        .StartDateTime = rdr.GetDateTime(1),
                        .Duration = rdr.GetDouble(2),
                        .Level = rdr.GetDouble(3),
                        .Overload = If(rdr.IsDBNull(4), Nothing, rdr.GetBoolean(4)),
                        .Underload = If(rdr.IsDBNull(5), Nothing, rdr.GetBoolean(5)),
                        .MeasurementMetricId = rdr.GetInt32(6),
                        .MonitorId = rdr.GetInt32(7),
                        .MonitorLocationId = rdr.GetInt32(8),
                        .ProjectId = rdr.GetInt32(9),
                        .DateTimeUploaded = rdr.GetDateTime(10),
                        .UploadContactId = rdr.GetInt32(11),
                        .MeasurementFileId = rdr.GetInt32(12)
                        }

                    monitorLocationMeasurements.Add(measurement)

                End While
            Catch ex As Exception
                Console.WriteLine("Error reading measurements from the database")
                Console.WriteLine(ex.Message)
                Console.WriteLine(ex.Source)
            End Try
        End Using

        Return monitorLocationMeasurements

    End Function

    Public Function GetObjectMeasurements(
        ObjectName As String, ObjectId As Integer, OrderByColumn As String, SortOrderList As List(Of Integer)
    ) As List(Of Measurement) Implements IMeasurementsDAL.GetObjectMeasurements

        Dim orderByString = "CASE " + OrderByColumn + " "
        For i = 0 To SortOrderList.Count - 1
            orderByString += "WHEN " + SortOrderList(i).ToString + " THEN " + (i + 1).ToString + " "
        Next
        orderByString += "END"

        Return GetObjectMeasurements(ObjectName, ObjectId, orderByString)

    End Function


#End Region

#End Region

#Region "Measurement Comments"

    Public Function GetMeasurementComment(MeasurementCommentId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementComment Implements IMeasurementsDAL.GetMeasurementComment

        If db Is Nothing Then db = Me.db

        Try

            Return GetMeasurementComments(db).Single(Function(mc) mc.Id = MeasurementCommentId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetMeasurementComments(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementComment) Implements IMeasurementsDAL.GetMeasurementComments

        If db Is Nothing Then db = Me.db

        Try

            Return db.MeasurementComments _
                .Include(Function(mc) mc.CommentType) _
                .Include(Function(mc) mc.MonitorLocation) _
                .Include(Function(mc) mc.Contact).OrderBy(Function(mc) mc.CommentDateTime)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function AddMeasurementComment(MeasurementComment As MeasurementComment) As MeasurementComment Implements IMeasurementsDAL.AddMeasurementComment

        Try

            Dim dbMeasurementComment = db.MeasurementComments.Add(MeasurementComment)
            SaveChanges(db)
            Return MeasurementComment

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function DeleteMeasurementComment(MeasurementCommentId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMeasurementComment

        Try

            Dim MeasurementCommentToDelete As MeasurementComment = db.MeasurementComments.Single(Function(mc) mc.Id = MeasurementCommentId)
            MeasurementCommentToDelete.CommentType = Nothing
            MeasurementCommentToDelete.Contact = Nothing
            MeasurementCommentToDelete.MonitorLocation = Nothing
            MeasurementCommentToDelete.Measurements.Clear()
            db.MeasurementComments.Remove(MeasurementCommentToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    Private Function GetMeasurementCommentMeasurementIds(MeasurementCommentId As Integer) As List(Of Integer)

        ' Get Measurement Ids for the Comment
        Dim measurementIds As New List(Of Integer)
        Dim sql1 As String = "SELECT Measurement_Id FROM MeasurementMeasurementComments WHERE MeasurementComment_Id = " + MeasurementCommentId.ToString
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql1, conn)
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    measurementIds.Add(rdr.GetInt32(0))
                End While
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return measurementIds

    End Function
    Private Function GetMeasurementIdRangeClause(ByRef MeasurementIds As List(Of Integer)) As String

        Dim ranges = GetSequentialRanges(MeasurementIds)
        Dim whereClauses = New List(Of String)
        For Each range In ranges
            whereClauses.Add(String.Format("(Id >= {0} AND Id <= {1})", range(0), range(1)))
        Next
        Dim whereClause As String = String.Join(" AND ", whereClauses)
        Return whereClause

    End Function

    Public Function GetMeasurementCommentStartDateTime(MeasurementCommentId As Integer) As Date Implements IMeasurementsDAL.GetMeasurementCommentStartDateTime

        Dim measurementIds = GetMeasurementCommentMeasurementIds(MeasurementCommentId)
        Dim whereClause = GetMeasurementIdRangeClause(measurementIds)

        Dim sql As String = "SELECT MIN(StartDateTime) as FirstStartDateTime FROM Measurements WHERE " + whereClause
        Dim firstMeasurementStartDateTime As Date
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                firstMeasurementStartDateTime = Convert.ToDateTime(cmd.ExecuteScalar())
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return firstMeasurementStartDateTime

    End Function

    Public Function GetMeasurementCommentEndDateTime(MeasurementCommentId As Integer) As Date Implements IMeasurementsDAL.GetMeasurementCommentEndDateTime

        Dim measurementIds = GetMeasurementCommentMeasurementIds(MeasurementCommentId)
        Dim whereClause = GetMeasurementIdRangeClause(measurementIds)

        Dim sql As String = "SELECT TOP(1) StartDateTime, Duration FROM Measurements WHERE " + whereClause + " ORDER BY StartDateTime DESC"
        Dim lastMeasurementStartDateTime As Date, duration As Double
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                Dim rdr As SqlDataReader = cmd.ExecuteReader()
                While rdr.Read()
                    lastMeasurementStartDateTime = rdr.GetDateTime(0)
                    duration = rdr.GetDouble(1)
                End While
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using
        Return lastMeasurementStartDateTime.AddDays(duration)

    End Function


#End Region

#Region "Measurement Comment Types"

    Public Function GetMeasurementCommentTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementCommentType) Implements IMeasurementsDAL.GetMeasurementCommentTypes

        If db Is Nothing Then db = Me.db

        Try

            Return db.MeasurementCommentTypes _
           .Include(Function(mct) mct.Comments).OrderBy(Function(mct) mct.CommentTypeName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementCommentType(MeasurementCommentTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementCommentType Implements IMeasurementsDAL.GetMeasurementCommentType

        If db Is Nothing Then db = Me.db

        Try

            Return GetMeasurementCommentTypes(db).Single(Function(mct) mct.Id = MeasurementCommentTypeId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementCommentType(CommentTypeName As String) As MeasurementCommentType Implements IMeasurementsDAL.GetMeasurementCommentType

        Try

            Return GetMeasurementCommentTypes().Single(Function(mct) LCase(mct.CommentTypeName) = LCase(CommentTypeName))

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function AddMeasurementCommentType(MeasurementCommentType As MeasurementCommentType) As MeasurementCommentType Implements IMeasurementsDAL.AddMeasurementCommentType

        Try

            db.MeasurementCommentTypes.Add(MeasurementCommentType)
            SaveChanges(db)
            Return MeasurementCommentType

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function UpdateMeasurementCommentType(MeasurementCommentType As MeasurementCommentType) As MeasurementCommentType Implements IMeasurementsDAL.UpdateMeasurementCommentType

        Using db = New SECMonitoringDbContext

            Dim dbMeasurementCommentType = GetMeasurementCommentTypes(db).Single(Function(mct) mct.Id = MeasurementCommentType.Id)

            dbMeasurementCommentType.CommentTypeName = MeasurementCommentType.CommentTypeName

            db.Entry(dbMeasurementCommentType).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbMeasurementCommentType

        End Using

    End Function
    Public Function AddMeasurementCommentTypeExcludedMeasurementView(MeasurementCommentTypeId As Integer, ExcludedMeasurementViewId As Integer) As Boolean Implements IMeasurementsDAL.AddMeasurementCommentTypeExcludedMeasurementView

        Try
            Dim obj = GetMeasurementCommentType(MeasurementCommentTypeId)
            obj.ExcludedMeasurementViews.Add(GetMeasurementView(ExcludedMeasurementViewId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveMeasurementCommentTypeExcludedMeasurementView(MeasurementCommentTypeId As Integer, ExcludedMeasurementViewId As Integer) As Boolean Implements IMeasurementsDAL.RemoveMeasurementCommentTypeExcludedMeasurementView

        Try
            Dim obj = GetMeasurementCommentType(MeasurementCommentTypeId)
            obj.ExcludedMeasurementViews.Remove(GetMeasurementView(ExcludedMeasurementViewId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function AddMeasurementCommentTypeExcludedAssessmentCriterionGroup(MeasurementCommentTypeId As Integer, ExcludedAssessmentCriterionGroupId As Integer) As Boolean Implements IMeasurementsDAL.AddMeasurementCommentTypeExcludedAssessmentCriterionGroup

        Try
            Dim obj = GetMeasurementCommentType(MeasurementCommentTypeId)
            obj.ExcludedAssessmentCriterionGroups.Add(GetAssessmentCriterionGroup(ExcludedAssessmentCriterionGroupId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveMeasurementCommentTypeExcludedAssessmentCriterionGroup(MeasurementCommentTypeId As Integer, ExcludedAssessmentCriterionGroupId As Integer) As Boolean Implements IMeasurementsDAL.RemoveMeasurementCommentTypeExcludedAssessmentCriterionGroup

        Try
            Dim obj = GetMeasurementCommentType(MeasurementCommentTypeId)
            obj.ExcludedAssessmentCriterionGroups.Remove(GetAssessmentCriterionGroup(ExcludedAssessmentCriterionGroupId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function DeleteMeasurementCommentType(MeasurementCommentTypeId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMeasurementCommentType

        Try

            Dim MeasurementCommentTypeToDelete As MeasurementCommentType = db.MeasurementCommentTypes.Single(Function(cf) cf.Id = MeasurementCommentTypeId)
            db.MeasurementCommentTypes.Remove(MeasurementCommentTypeToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "Measurement Files"

    Public Function GetMeasurementFiles(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementFile) Implements IMeasurementsDAL.GetMeasurementFiles

        If db Is Nothing Then db = Me.db

        Try

            Return db.MeasurementFiles _
               .Include(Function(mf) mf.Contact) _
               .Include(Function(mf) mf.Monitor) _
               .Include(Function(mf) mf.MonitorLocation)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMonitorLocationMeasurementFiles(MeasurementFileIds As List(Of Integer), Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementFile) Implements IMeasurementsDAL.GetMonitorLocationMeasurementFiles

        Return GetMeasurementFiles(db).Where(Function(mf) MeasurementFileIds.Contains(mf.MonitorLocationId))

    End Function
    Public Function GetMeasurementFile(MeasurementFileId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementFile Implements IMeasurementsDAL.GetMeasurementFile

        If db Is Nothing Then db = Me.db

        Try

            Return GetMeasurementFiles(db).Single(Function(mf) mf.Id = MeasurementFileId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementFile(MeasurementFileRouteName As String) As MeasurementFile Implements IMeasurementsDAL.GetMeasurementFile

        Try

            Dim fName = Left(MeasurementFileRouteName, Len(MeasurementFileRouteName) - 15).Replace("-", " ")
            Dim uploadDateTimeString = Right(MeasurementFileRouteName, 14)
            Return GetMeasurementFiles().Single(Function(mf) LCase(mf.MeasurementFileName) = LCase(fName) And mf.ServerFileName.Contains(uploadDateTimeString))

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function AddMeasurementFile(MeasurementFile As MeasurementFile) As MeasurementFile Implements IMeasurementsDAL.AddMeasurementFile

        Try

            Dim dbMeasurementFile = db.MeasurementFiles.Add(MeasurementFile)
            SaveChanges(db)
            Return dbMeasurementFile

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function UpdateMeasurementFileMeasurementCount(measurementFile As MeasurementFile) As MeasurementFile Implements IMeasurementsDAL.UpdateMeasurementFileMeasurementCount

        Using db = New SECMonitoringDbContext

            Dim dbMeasurementFile = db.MeasurementFiles.Single(Function(mf) mf.Id = measurementFile.Id)

            dbMeasurementFile.NumberOfMeasurements = measurementFile.NumberOfMeasurements
            db.Entry(dbMeasurementFile).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbMeasurementFile

        End Using

    End Function

    Public Function MeasurementFileCanBeDeleted(MeasurementFileId As Integer) As Boolean Implements IMeasurementsDAL.MeasurementFileCanBeDeleted

        ' See if the Measurement File has any Measurement Comments
        Dim format = (
            "SELECT COUNT(DISTINCT MeasurementFileId) from Measurements WHERE Id IN (" +
                "SELECT Measurement_Id FROM MeasurementMeasurementComments" +
            ") AND MeasurementFileId={0}"
        )
        Dim query = String.Format(format, MeasurementFileId)
        Dim hasComments As Boolean
        Using connection As New SqlConnection(MeasurementsConnectionString)
            connection.Open()
            Dim cmd As New SqlCommand(query, connection)
            hasComments = Convert.ToBoolean(cmd.ExecuteScalar())
        End Using

        Return Not (hasComments)

    End Function
    Public Function DeleteMeasurementFile(MeasurementFileId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMeasurementFile

        Try

            Dim MeasurementFileToDelete As MeasurementFile = db.MeasurementFiles.Single(Function(mf) mf.Id = MeasurementFileId)

            ' Delete Measurements
            Dim deleteMeasurementsSuccess = DeleteMeasurementsForFile(MeasurementFileId:=MeasurementFileId)
            If Not deleteMeasurementsSuccess Then Return False
            ' Delete File
            db.MeasurementFiles.Remove(MeasurementFileToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    Public Function GetMeasurementFileNumMeasurements(MeasurementFileId As Integer) As Integer Implements IMeasurementsDAL.GetMeasurementFileNumMeasurements

        Dim numMeasurements As Int32 = 0
        Dim sql As String = "SELECT COUNT (*) FROM Measurements WHERE MeasurementFileId = " + MeasurementFileId.ToString
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                numMeasurements = Convert.ToInt32(cmd.ExecuteScalar())
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return numMeasurements

    End Function


#End Region

#Region "Measurement File Types"

    Public Function GetMeasurementFileTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementFileType) Implements IMeasurementsDAL.GetMeasurementFileTypes

        If db Is Nothing Then db = Me.db

        Try

            Return db.MeasurementFileTypes _
           .Include(Function(mft) mft.MeasurementFiles) _
           .Include(Function(mft) mft.MeasurementType).OrderBy(Function(mft) mft.FileTypeName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementFileTypes(MeasurementTypeId As Integer) As IEnumerable(Of MeasurementFileType) Implements IMeasurementsDAL.GetMeasurementFileTypes

        Try
            Return GetMeasurementFileTypes().Where(Function(mft) mft.MeasurementTypeId = MeasurementTypeId)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function GetMeasurementFileType(MeasurementFileTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementFileType Implements IMeasurementsDAL.GetMeasurementFileType

        If db Is Nothing Then db = Me.db

        Try

            Return GetMeasurementFileTypes(db).Single(Function(mft) mft.Id = MeasurementFileTypeId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

#End Region

#Region "Measurement Metrics"

    Public Function GetMeasurementMetrics(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementMetric) Implements IMeasurementsDAL.GetMeasurementMetrics

        If db Is Nothing Then db = Me.db

        Try

            Return db.MeasurementMetrics _
               .Include(Function(mm) mm.MeasurementType) _
               .Include(Function(mm) mm.CalculationFilters) _
               .OrderBy(Function(mm) mm.DisplayName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementMetrics(MeasurementTypeId As Integer) As IEnumerable(Of MeasurementMetric) Implements IMeasurementsDAL.GetMeasurementMetrics

        Try
            Return GetMeasurementMetrics() _
                .Where(Function(mm) mm.MeasurementTypeId = MeasurementTypeId) _
                .OrderBy(Function(mm) mm.DisplayName).ToList()
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function GetMeasurementMetrics(RMP As IReadMeasurementParameters) As IEnumerable(Of MeasurementMetric) Implements IMeasurementsDAL.GetMeasurementMetrics

        Try
            Return ReadMeasurements(RMP).Select(Function(m) m.MeasurementMetric).Distinct.ToList
        Catch ex As Exception
            Return Nothing
        End Try


    End Function
    Public Function GetMeasurementMetric(MetricName As String) As MeasurementMetric Implements IMeasurementsDAL.GetMeasurementMetric

        Try
            Return db.MeasurementMetrics.Single(Function(m) LCase(m.MetricName) = LCase(MetricName))
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GetMeasurementMetric(MetricId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementMetric Implements IMeasurementsDAL.GetMeasurementMetric

        If db Is Nothing Then db = Me.db

        Try
            Return GetMeasurementMetrics(db).Single(Function(m) m.Id = MetricId)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GetMeasurementMetrics(MeasurementType As MeasurementType) As IEnumerable(Of MeasurementMetric) Implements IMeasurementsDAL.GetMeasurementMetrics

        Return db.MeasurementTypes.Single(
            Function(m) m.MeasurementTypeName = MeasurementType.MeasurementTypeName
        ).MeasurementMetrics.OrderBy(Function(mm) mm.DisplayName)

    End Function
    Public Function AddMeasurementMetric(MeasurementMetric As MeasurementMetric) As MeasurementMetric Implements IMeasurementsDAL.AddMeasurementMetric

        Try

            db.MeasurementMetrics.Add(MeasurementMetric)
            SaveChanges(db)
            Return MeasurementMetric

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function UpdateMeasurementMetric(MeasurementMetric As MeasurementMetric) As MeasurementMetric Implements IMeasurementsDAL.UpdateMeasurementMetric

        Using db = New SECMonitoringDbContext

            Dim dbMeasurementMetric = GetMeasurementMetrics(db).Single(Function(mm) mm.Id = MeasurementMetric.Id)

            dbMeasurementMetric.MetricName = MeasurementMetric.MetricName
            dbMeasurementMetric.MeasurementType = GetMeasurementType(MeasurementMetric.MeasurementType.Id, db)
            dbMeasurementMetric.RoundingDecimalPlaces = MeasurementMetric.RoundingDecimalPlaces
            dbMeasurementMetric.FundamentalUnit = MeasurementMetric.FundamentalUnit
            dbMeasurementMetric.DisplayName = MeasurementMetric.DisplayName

            db.Entry(dbMeasurementMetric).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbMeasurementMetric

        End Using

    End Function

    Public Function DeleteMeasurementMetric(MeasurementMetricId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMeasurementMetric

        Try

            Dim MeasurementMetricToDelete As MeasurementMetric = db.MeasurementMetrics.Single(Function(cf) cf.Id = MeasurementMetricId)
            db.MeasurementMetrics.Remove(MeasurementMetricToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    Public Function MeasurementMetricHasMeasurements(MeasurementMetricId As Integer) As Boolean Implements IMeasurementsDAL.MeasurementMetricHasMeasurements

        Dim RecordCount As Int32 = 0
        Dim sql As String = "SELECT CASE WHEN EXISTS (SELECT * FROM [Measurements] WHERE [MeasurementMetricId] = " + MeasurementMetricId.ToString + ") THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END"
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                RecordCount = Convert.ToBoolean(cmd.ExecuteScalar())
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return RecordCount

    End Function


#End Region

#Region "Measurement Types"

    Public Function GetMeasurementTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementType) Implements IMeasurementsDAL.GetMeasurementTypes

        If db Is Nothing Then db = Me.db

        Try

            Return db.MeasurementTypes _
           .Include(Function(mt) mt.MeasurementMetrics) _
           .Include(Function(mt) mt.Monitors) _
           .Include(Function(mt) mt.MeasurementViews).OrderBy(Function(mt) mt.MeasurementTypeName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementType(MeasurementTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementType Implements IMeasurementsDAL.GetMeasurementType

        If db Is Nothing Then db = Me.db

        Try

            Return GetMeasurementTypes(db).Single(Function(mt) mt.Id = MeasurementTypeId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementType(MeasurementTypeName As String) As MeasurementType Implements IMeasurementsDAL.GetMeasurementType

        Return db.MeasurementTypes.Single(Function(m) LCase(m.MeasurementTypeName) = LCase(MeasurementTypeName))

    End Function
    Public Function GetMeasurementTypesSelectList(Include0AsAll As Boolean, Optional db As SECMonitoringDbContext = Nothing) As SelectList Implements IMeasurementsDAL.GetMeasurementTypesSelectList

        If db Is Nothing Then db = Me.db

        Dim measurementTypes = Me.GetMeasurementTypes

        Dim mtList As New List(Of SelectListItem)
        If Include0AsAll Then
            mtList.Add(New SelectListItem With {.Value = 0, .Text = "All"})
        End If
        For Each mt In measurementTypes
            mtList.Add(New SelectListItem With {.Value = mt.Id, .Text = mt.MeasurementTypeName})
        Next

        Dim selItem As String = ""
        If Include0AsAll Then
            selItem = "All"
        End If
        Return New SelectList(mtList, "Value", "Text", selItem)

    End Function

#End Region

#Region "Measurement Views"

    Public Function GetMeasurementViews(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementView) Implements IMeasurementsDAL.GetMeasurementViews

        If db Is Nothing Then db = Me.db

        Try

            Return db.MeasurementViews _
           .Include(Function(mv) mv.MeasurementViewGroups) _
           .Include(Function(mv) mv.MeasurementType) _
           .Include(Function(mv) mv.MeasurementViewTableType) _
           .Include(Function(mv) mv.ExcludingMeasurementCommentTypes).OrderBy(Function(mv) mv.ViewName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementView(MeasurementViewId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementView Implements IMeasurementsDAL.GetMeasurementView

        If db Is Nothing Then db = Me.db

        Try

            Return GetMeasurementViews(db).Single(Function(mv) mv.Id = MeasurementViewId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementView(ViewName As String) As MeasurementView Implements IMeasurementsDAL.GetMeasurementView
        Try
            Return GetMeasurementViews().Single(Function(v) LCase(v.ViewName) = LCase(ViewName))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function AddMeasurementView(MeasurementView As MeasurementView) As MeasurementView Implements IMeasurementsDAL.AddMeasurementView

        Try

            db.MeasurementViews.Add(MeasurementView)
            SaveChanges(db)
            Return MeasurementView

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function UpdateMeasurementView(MeasurementView As MeasurementView) As MeasurementView Implements IMeasurementsDAL.UpdateMeasurementView

        Using db = New SECMonitoringDbContext

            Dim dbMeasurementView = GetMeasurementViews(db).Single(Function(mv) mv.Id = MeasurementView.Id)

            dbMeasurementView.ViewName = MeasurementView.ViewName
            dbMeasurementView.MeasurementType = GetMeasurementType(MeasurementView.MeasurementType.Id, db)
            dbMeasurementView.DisplayName = MeasurementView.DisplayName
            dbMeasurementView.MeasurementViewTableType = GetMeasurementViewTableType(MeasurementView.MeasurementViewTableType.Id, db)
            dbMeasurementView.TableResultsHeader = MeasurementView.TableResultsHeader

            db.Entry(dbMeasurementView).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbMeasurementView

        End Using

    End Function
    Public Function AddMeasurementViewUserAccessLevel(MeasurementViewId As Integer, UserAccessLevelId As Integer) As Boolean Implements IMeasurementsDAL.AddMeasurementViewUserAccessLevel

        Try
            Dim obj = GetMeasurementView(MeasurementViewId)
            'obj.AllowedUserAccessLevels.Add(GetUserAccessLevel(UserAccessLevelId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function AddMeasurementViewCommentType(MeasurementViewId As Integer, CommentTypeId As Integer) As Boolean Implements IMeasurementsDAL.AddMeasurementViewCommentType

        Try
            Dim obj = GetMeasurementView(MeasurementViewId)
            obj.ExcludingMeasurementCommentTypes.Add(GetMeasurementCommentType(CommentTypeId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveMeasurementViewUserAccessLevel(MeasurementViewId As Integer, UserAccessLevelId As Integer) As Boolean Implements IMeasurementsDAL.RemoveMeasurementViewUserAccessLevel

        Try
            Dim obj = GetMeasurementView(MeasurementViewId)
            'obj.AllowedUserAccessLevels.Remove(GetUserAccessLevel(UserAccessLevelId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveMeasurementViewCommentType(MeasurementViewId As Integer, CommentTypeId As Integer) As Boolean Implements IMeasurementsDAL.RemoveMeasurementViewCommentType

        Try
            Dim obj = GetMeasurementView(MeasurementViewId)
            obj.ExcludingMeasurementCommentTypes.Remove(GetMeasurementCommentType(CommentTypeId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function DeleteMeasurementView(MeasurementViewId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMeasurementView

        Try

            Dim MeasurementViewToDelete As MeasurementView = db.MeasurementViews.Single(Function(cf) cf.Id = MeasurementViewId)
            'MeasurementViewToDelete.AllowedUserAccessLevels = Nothing
            MeasurementViewToDelete.ExcludingMeasurementCommentTypes = Nothing
            MeasurementViewToDelete.MeasurementType = Nothing
            MeasurementViewToDelete.MeasurementViewGroups = Nothing
            MeasurementViewToDelete.MeasurementViewTableType = Nothing
            MeasurementViewToDelete.Projects = Nothing

            db.MeasurementViews.Remove(MeasurementViewToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "Measurement View Groups"

    Public Function AddMeasurementViewGroup(MeasurementViewGroup As MeasurementViewGroup) As MeasurementViewGroup Implements IMeasurementsDAL.AddMeasurementViewGroup

        Try

            db.MeasurementViewGroups.Add(MeasurementViewGroup)
            SaveChanges(db)
            Return MeasurementViewGroup

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementViewGroups(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementViewGroup) Implements IMeasurementsDAL.GetMeasurementViewGroups

        If db Is Nothing Then db = Me.db

        Try

            Return db.MeasurementViewGroups.OrderBy(Function(mvg) mvg.GroupIndex)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementViewGroup(MeasurementViewGroupId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementViewGroup Implements IMeasurementsDAL.GetMeasurementViewGroup

        If db Is Nothing Then db = Me.db

        Try

            Return GetMeasurementViewGroups(db).Single(Function(mvg) mvg.Id = MeasurementViewGroupId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementViewGroup(MeasurementViewName As String, GroupIndex As Integer) As MeasurementViewGroup Implements IMeasurementsDAL.GetMeasurementViewGroup

        Try
            Return GetMeasurementViewGroups().Single(Function(mvg) LCase(mvg.MeasurementView.ViewName) = LCase(MeasurementViewName) AndAlso
                                                       mvg.GroupIndex = GroupIndex)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function UpdateMeasurementViewGroup(MeasurementViewGroup As MeasurementViewGroup) As MeasurementViewGroup Implements IMeasurementsDAL.UpdateMeasurementViewGroup

        Using db = New SECMonitoringDbContext

            Dim dbMeasurementViewGroup = GetMeasurementViewGroups(db).Single(Function(mvg) mvg.Id = MeasurementViewGroup.Id)

            dbMeasurementViewGroup.GroupIndex = MeasurementViewGroup.GroupIndex
            dbMeasurementViewGroup.MainHeader = MeasurementViewGroup.MainHeader
            dbMeasurementViewGroup.SubHeader = MeasurementViewGroup.SubHeader

            db.Entry(dbMeasurementViewGroup).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbMeasurementViewGroup

        End Using

    End Function
    Public Function DeleteMeasurementViewGroup(MeasurementViewGroupId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMeasurementViewGroup

        Try

            Dim MeasurementViewGroupToDelete As MeasurementViewGroup = db.MeasurementViewGroups.Single(Function(mvg) mvg.Id = MeasurementViewGroupId)
            MeasurementViewGroupToDelete.MeasurementView = Nothing
            MeasurementViewGroupToDelete.MeasurementViewSequenceSettings = Nothing
            db.MeasurementViewGroups.Remove(MeasurementViewGroupToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "Measurement View Sequence Settings"

    Public Function AddMeasurementViewSequenceSetting(MeasurementViewSequenceSetting As MeasurementViewSequenceSetting) As MeasurementViewSequenceSetting Implements IMeasurementsDAL.AddMeasurementViewSequenceSetting

        Try

            db.MeasurementViewSequenceSettings.Add(MeasurementViewSequenceSetting)
            SaveChanges(db)
            Return MeasurementViewSequenceSetting

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMeasurementViewSequenceSettings(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementViewSequenceSetting) Implements IMeasurementsDAL.GetMeasurementViewSequenceSettings

        If db Is Nothing Then db = Me.db

        Try

            Return db.MeasurementViewSequenceSettings _
           .Include(Function(mvss) mvss.CalculationFilter) _
           .Include(Function(mvss) mvss.DayViewSeriesType) _
           .Include(Function(mvss) mvss.WeekViewSeriesType) _
           .Include(Function(mvss) mvss.MonthViewSeriesType) _
           .Include(Function(mvss) mvss.MeasurementViewGroup).OrderBy(Function(mvss) mvss.SequenceIndex)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetMeasurementViewSequenceSetting(MeasurementViewName As String, GroupIndex As Integer, SequenceIndex As Integer) As MeasurementViewSequenceSetting Implements IMeasurementsDAL.GetMeasurementViewSequenceSetting

        Try
            Return GetMeasurementViewSequenceSettings.Single(Function(mvss) LCase(mvss.MeasurementViewGroup.MeasurementView.ViewName) = LCase(MeasurementViewName) AndAlso
                                                                            mvss.MeasurementViewGroup.GroupIndex = GroupIndex AndAlso
                                                                            mvss.SequenceIndex = SequenceIndex)
        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function UpdateMeasurementViewSequenceSetting(MeasurementViewSequenceSetting As MeasurementViewSequenceSetting) As MeasurementViewSequenceSetting Implements IMeasurementsDAL.UpdateMeasurementViewSequenceSetting

        Using db = New SECMonitoringDbContext

            Dim dbMeasurementViewSequenceSetting = GetMeasurementViewSequenceSettings(db).Single(Function(mvss) mvss.Id = MeasurementViewSequenceSetting.Id)

            dbMeasurementViewSequenceSetting.SequenceIndex = MeasurementViewSequenceSetting.SequenceIndex
            dbMeasurementViewSequenceSetting.TableHeader = MeasurementViewSequenceSetting.TableHeader
            dbMeasurementViewSequenceSetting.SeriesName = MeasurementViewSequenceSetting.SeriesName
            dbMeasurementViewSequenceSetting.DayViewSeriesType = GetMeasurementViewSeriesType(MeasurementViewSequenceSetting.DayViewSeriesType.Id, db)
            dbMeasurementViewSequenceSetting.WeekViewSeriesType = GetMeasurementViewSeriesType(MeasurementViewSequenceSetting.WeekViewSeriesType.Id, db)
            dbMeasurementViewSequenceSetting.MonthViewSeriesType = GetMeasurementViewSeriesType(MeasurementViewSequenceSetting.MonthViewSeriesType.Id, db)
            dbMeasurementViewSequenceSetting.SeriesColour = MeasurementViewSequenceSetting.SeriesColour
            dbMeasurementViewSequenceSetting.CalculationFilter = GetCalculationFilter(MeasurementViewSequenceSetting.CalculationFilter.Id, db)

            db.Entry(dbMeasurementViewSequenceSetting).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbMeasurementViewSequenceSetting

        End Using

    End Function

    Public Function DeleteMeasurementViewSequenceSetting(MeasurementViewSequenceSettingId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMeasurementViewSequenceSetting

        Try

            Dim MeasurementViewSequenceSettingToDelete As MeasurementViewSequenceSetting = db.MeasurementViewSequenceSettings.Single(Function(cf) cf.Id = MeasurementViewSequenceSettingId)
            MeasurementViewSequenceSettingToDelete.MeasurementViewGroup = Nothing
            db.MeasurementViewSequenceSettings.Remove(MeasurementViewSequenceSettingToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "Measurement View Series Types"

    Public Function GetMeasurementViewSeriesTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementViewSeriesType) Implements IMeasurementsDAL.GetMeasurementViewSeriesTypes

        If db Is Nothing Then db = Me.db

        Try

            Return db.MeasurementViewSeriesTypes.OrderBy(Function(mvst) mvst.SeriesTypeName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetMeasurementViewSeriesType(MeasurementViewSeriesTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementViewSeriesType Implements IMeasurementsDAL.GetMeasurementViewSeriesType

        If db Is Nothing Then db = Me.db

        Try

            Return GetMeasurementViewSeriesTypes(db).Single(Function(mvst) mvst.Id = MeasurementViewSeriesTypeId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

#End Region

#Region "Series Dash Styles"

    Public Function GetSeriesDashStyle(SeriesDashStyleId As Integer, Optional db As SECMonitoringDbContext = Nothing) As SeriesDashStyle Implements IMeasurementsDAL.GetSeriesDashStyle

        If db Is Nothing Then db = Me.db

        Try

            Return GetSeriesDashStyles(db).Single(Function(sds) sds.Id = SeriesDashStyleId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetSeriesDashStyles(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of SeriesDashStyle) Implements IMeasurementsDAL.GetSeriesDashStyles

        If db Is Nothing Then db = Me.db

        Try

            Return db.SeriesDashStyles.OrderBy(Function(sds) sds.DashStyleName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

#End Region

#Region "Measurement View Table Types"

    Public Function GetMeasurementViewTableTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementViewTableType) Implements IMeasurementsDAL.GetMeasurementViewTableTypes

        If db Is Nothing Then db = Me.db

        Try

            Return db.MeasurementViewTableTypes _
           .Include(Function(mvtt) mvtt.MeasurementViews).OrderBy(Function(mvtt) mvtt.TableTypeName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetMeasurementViewTableType(MeasurementViewTableTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementViewTableType Implements IMeasurementsDAL.GetMeasurementViewTableType

        If db Is Nothing Then db = Me.db

        Return GetMeasurementViewTableTypes(db).Single(Function(mvtt) mvtt.Id = MeasurementViewTableTypeId)

    End Function

#End Region

#Region "Monitors"

    Public Function GetMonitors(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Monitor) Implements IMeasurementsDAL.GetMonitors

        If db Is Nothing Then db = Me.db

        Return db.Monitors.Include(Function(m) m.CurrentLocation) _
                          .Include(Function(m) m.CurrentStatus) _
                          .Include(Function(m) m.DeploymentRecords) _
                          .Include(Function(m) m.Documents) _
                          .Include(Function(m) m.MeasurementFiles) _
                          .Include(Function(m) m.MeasurementType) _
                          .Include(Function(m) m.OwnerOrganisation).OrderBy(Function(m) m.MonitorName)

    End Function
    Public Function GetMonitor(MonitorName As String) As Monitor Implements IMeasurementsDAL.GetMonitor

        Try
            Return GetMonitors().Single(Function(m) LCase(m.MonitorName) = LCase(MonitorName))
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function GetMonitor(MonitorId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Monitor Implements IMeasurementsDAL.GetMonitor

        If db Is Nothing Then db = Me.db

        Return GetMonitors(db).Single(Function(m) m.Id = MonitorId)

    End Function
    Public Function AddMonitor(Monitor As Monitor) As Monitor Implements IMeasurementsDAL.AddMonitor

        Try

            db.Monitors.Add(Monitor)
            SaveChanges(db)
            Return Monitor

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function UpdateMonitor(Monitor As Monitor) As Monitor Implements IMeasurementsDAL.UpdateMonitor

        Using db = New SECMonitoringDbContext

            Dim dbMonitor = GetMonitors(db).Single(Function(m) m.Id = Monitor.Id)

            dbMonitor.MonitorName = Monitor.MonitorName
            dbMonitor.MeasurementType = GetMeasurementType(Monitor.MeasurementType.Id, db)
            dbMonitor.SerialNumber = Monitor.SerialNumber
            dbMonitor.Manufacturer = Monitor.Manufacturer
            dbMonitor.Model = Monitor.Model
            dbMonitor.Category = Monitor.Category
            dbMonitor.OwnerOrganisation = GetOrganisation(Monitor.OwnerOrganisation.Id, db)
            dbMonitor.RequiresCalibration = Monitor.RequiresCalibration
            dbMonitor.LastFieldCalibration = Monitor.LastFieldCalibration
            dbMonitor.LastFullCalibration = Monitor.LastFullCalibration
            dbMonitor.NextFullCalibration = Monitor.NextFullCalibration

            db.Entry(dbMonitor).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbMonitor

        End Using

    End Function
    Public Function UpdateMonitorCurrentLocation(Monitor As Monitor) As Monitor Implements IMeasurementsDAL.UpdateMonitorCurrentLocation

        Using db = New SECMonitoringDbContext

            Dim dbMonitor = GetMonitors(db).Single(Function(m) m.Id = Monitor.Id)

            If Monitor.CurrentLocation Is Nothing Then
                dbMonitor.CurrentLocation = Nothing
            Else
                dbMonitor.CurrentLocation = GetMonitorLocation(Monitor.CurrentLocation.Id, db)
            End If

            db.Entry(dbMonitor).State = Entity.EntityState.Modified

            Try
                SaveChanges(db)
                Return dbMonitor
            Catch ex As Exception
                Return Nothing
            End Try

        End Using

    End Function
    Public Function DeleteMonitor(MonitorId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMonitor

        Try

            Dim MonitorToDelete As Monitor = db.Monitors.Single(Function(d) d.Id = MonitorId)
            MonitorToDelete.CurrentStatus = Nothing
            db.Monitors.Remove(MonitorToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "Monitor Deployment Records"

    Public Function GetMonitorDeploymentRecords(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MonitorDeploymentRecord) Implements IMeasurementsDAL.GetMonitorDeploymentRecords

        If db Is Nothing Then db = Me.db

        Try

            Return db.MonitorDeploymentRecords _
           .Include(Function(mdr) mdr.Monitor) _
           .Include(Function(mdr) mdr.MonitorLocation) _
           .Include(Function(mdr) mdr.MonitorSettings).OrderBy(Function(mdr) mdr.DeploymentStartDate)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMonitorDeploymentRecords(Monitor As Monitor) As IEnumerable(Of MonitorDeploymentRecord) Implements IMeasurementsDAL.GetMonitorDeploymentRecords

        Return GetMonitorDeploymentRecords().Where(Function(mdr) mdr.MonitorId = Monitor.Id)

    End Function
    Public Function GetMonitorDeploymentRecords(MonitorLocation As MonitorLocation) As IEnumerable(Of MonitorDeploymentRecord) Implements IMeasurementsDAL.GetMonitorDeploymentRecords

        Return GetMonitorDeploymentRecords().Where(Function(mdr) mdr.MonitorLocationId = MonitorLocation.Id)

    End Function
    Public Function AddMonitorDeploymentRecord(MonitorDeploymentRecord As MonitorDeploymentRecord) As MonitorDeploymentRecord Implements IMeasurementsDAL.AddMonitorDeploymentRecord

        Dim newRecord As MonitorDeploymentRecord
        Dim monitor = db.Monitors.Include(Function(m) m.MeasurementType).Single(Function(m) m.Id = MonitorDeploymentRecord.Monitor.Id)
        Dim monitorLocation = db.MonitorLocations.Single(Function(ml) ml.Id = MonitorDeploymentRecord.MonitorLocation.Id)
        Dim newMonitorSettings As MonitorSettings
        Dim newNoiseSetting As NoiseSetting = Nothing
        Dim newVibrationSetting As VibrationSetting = Nothing
        Dim newAirQualitySetting As AirQualitySetting = Nothing

        ' Create new deployment record
        Dim monitorDeploymentRecordId = db.MonitorDeploymentRecords.Select(Function(mmddrr) mmddrr.Id).Max() + 1
        newRecord = New MonitorDeploymentRecord With {
            .DeploymentEndDate = MonitorDeploymentRecord.DeploymentEndDate,
            .DeploymentStartDate = MonitorDeploymentRecord.DeploymentStartDate,
            .MonitorId = monitor.Id,
            .MonitorLocationId = monitorLocation.Id,
            .Id = monitorDeploymentRecordId
        }
        ' Create new monitor settings
        Dim ms = MonitorDeploymentRecord.MonitorSettings
        Dim monitorSettingsId = db.MonitorSettings.Select(Function(mmss) mmss.Id).Max() + 1
        newMonitorSettings = New MonitorSettings With {
            .AdditionalInfo1 = ms.AdditionalInfo1,
            .AdditionalInfo2 = ms.AdditionalInfo2,
            .MeasurementPeriod = ms.MeasurementPeriod,
            .Id = monitorSettingsId
        }
        ' Create new noise setting, if required
        If ms.NoiseSetting IsNot Nothing Then
            Dim ns = ms.NoiseSetting
            Dim noiseSettingId = db.NoiseSettings.Select(Function(nnss) nnss.Id).Max() + 1
            newNoiseSetting = New NoiseSetting With {
                .AlarmTriggerLevel = ns.AlarmTriggerLevel,
                .DynamicRangeLowerLevel = ns.DynamicRangeLowerLevel,
                .DynamicRangeUpperLevel = ns.DynamicRangeUpperLevel,
                .FrequencyWeighting = ns.FrequencyWeighting,
                .MicrophoneSerialNumber = ns.MicrophoneSerialNumber,
                .SoundRecording = ns.SoundRecording,
                .TimeWeighting = ns.TimeWeighting,
                .WindScreenCorrection = ns.WindScreenCorrection,
                .Id = noiseSettingId
            }
        End If
        ' Create new vibration setting, if required
        If ms.VibrationSetting IsNot Nothing Then
            Dim vs = ms.VibrationSetting
            Dim vibrationSettingId = db.VibrationSettings.Select(Function(vvss) vvss.Id).Max() + 1
            newVibrationSetting = New VibrationSetting With {
                .AlarmTriggerLevel = vs.AlarmTriggerLevel,
                .XChannelWeighting = vs.XChannelWeighting,
                .YChannelWeighting = vs.YChannelWeighting,
                .ZChannelWeighting = vs.ZChannelWeighting,
                .Id = vibrationSettingId
            }
        End If
        ' Create new air quality setting, if required
        If ms.AirQualitySetting IsNot Nothing Then
            Dim aqs = ms.AirQualitySetting
            Dim airQualitySettingId = db.AirQualitySettings.Select(Function(aass) aass.Id).Max() + 1
            newAirQualitySetting = New AirQualitySetting With {
                .AlarmTriggerLevel = aqs.AlarmTriggerLevel,
                .InletHeatingOn = aqs.InletHeatingOn,
                .NewDailySample = aqs.NewDailySample,
                .Id = airQualitySettingId
            }
        End If
        ' Attach settings to new record
        If newNoiseSetting IsNot Nothing Then newMonitorSettings.NoiseSetting = newNoiseSetting
        If newVibrationSetting IsNot Nothing Then newMonitorSettings.VibrationSetting = newVibrationSetting
        If newAirQualitySetting IsNot Nothing Then newMonitorSettings.AirQualitySetting = newAirQualitySetting
        newRecord.MonitorSettings = newMonitorSettings
        db.MonitorDeploymentRecords.Add(newRecord)
        SaveChanges(db)

        Return newRecord

    End Function
    Public Function UpdateMonitorDeploymentRecord(MonitorDeploymentRecord As MonitorDeploymentRecord) As MonitorDeploymentRecord Implements IMeasurementsDAL.UpdateMonitorDeploymentRecord

        Using db = New SECMonitoringDbContext

            Dim dbMonitorDeploymentRecord = GetMonitorDeploymentRecords(db).Single(Function(mdr) mdr.Id = MonitorDeploymentRecord.Id)

            dbMonitorDeploymentRecord.Monitor = GetMonitor(MonitorDeploymentRecord.MonitorId, db)
            dbMonitorDeploymentRecord.MonitorLocation = GetMonitorLocation(MonitorDeploymentRecord.MonitorLocationId, db)
            dbMonitorDeploymentRecord.DeploymentStartDate = MonitorDeploymentRecord.DeploymentStartDate
            dbMonitorDeploymentRecord.DeploymentEndDate = MonitorDeploymentRecord.DeploymentEndDate

            db.Entry(dbMonitorDeploymentRecord).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbMonitorDeploymentRecord

        End Using

    End Function
    Public Function DeleteMonitorDeploymentRecord(MonitorDeploymentRecordId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMonitorDeploymentRecord

        Try

            Dim MonitorDeploymentRecordToDelete As MonitorDeploymentRecord = db.MonitorDeploymentRecords.Single(Function(mdr) mdr.Id = MonitorDeploymentRecordId)
            MonitorDeploymentRecordToDelete.Monitor = Nothing
            MonitorDeploymentRecordToDelete.MonitorLocation = Nothing
            Dim MonitorSettingsToDelete As MonitorSettings = MonitorDeploymentRecordToDelete.MonitorSettings
            Dim NoiseSettingToDelete = MonitorSettingsToDelete.NoiseSetting
            Dim VibrationSettingToDelete = MonitorSettingsToDelete.VibrationSetting
            Dim AirQualitySettingToDelete = MonitorSettingsToDelete.AirQualitySetting
            db.MonitorDeploymentRecords.Remove(MonitorDeploymentRecordToDelete)
            db.MonitorSettings.Remove(MonitorSettingsToDelete)
            If NoiseSettingToDelete IsNot Nothing Then db.NoiseSettings.Remove(NoiseSettingToDelete)
            If VibrationSettingToDelete IsNot Nothing Then db.VibrationSettings.Remove(VibrationSettingToDelete)
            If AirQualitySettingToDelete IsNot Nothing Then db.AirQualitySettings.Remove(AirQualitySettingToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function
    Public Function GetMonitorDeploymentRecord(MonitorDeploymentRecordId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MonitorDeploymentRecord Implements IMeasurementsDAL.GetMonitorDeploymentRecord

        If db Is Nothing Then db = Me.db

        Return GetMonitorDeploymentRecords(db).Single(Function(mdr) mdr.Id = MonitorDeploymentRecordId)

    End Function


#End Region

#Region "Monitor Locations"

    Public Function GetMonitorLocations(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MonitorLocation) Implements IMeasurementsDAL.GetMonitorLocations

        If db Is Nothing Then db = Me.db

        Try
            Return db.MonitorLocations.Include(Function(ml) ml.AssessmentCriteria) _
                                      .Include(Function(ml) ml.CurrentMonitor) _
                                      .Include(Function(ml) ml.DeploymentRecords) _
                                      .Include(Function(ml) ml.Documents) _
                                      .Include(Function(ml) ml.DeploymentRecords) _
                                      .Include(Function(ml) ml.MonitorLocationGeoCoords).OrderBy(Function(ml) ml.MonitorLocationName)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function GetCurrentMonitor(ByVal ProjectShortName As String, ByVal MonitorLocationName As String) As Monitor Implements IMeasurementsDAL.GetCurrentMonitor

        Try

            Return GetMonitorLocation(ProjectShortName, MonitorLocationName).CurrentMonitor

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMonitorLocation(MonitorLocationId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MonitorLocation Implements IMeasurementsDAL.GetMonitorLocation

        If db Is Nothing Then db = Me.db

        Try

            Return GetMonitorLocations(db).Single(Function(m) m.Id = MonitorLocationId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetMonitorLocations(ProjectRouteName As String) As IEnumerable(Of MonitorLocation) Implements IMeasurementsDAL.GetMonitorLocations

        Try
            Return GetMonitorLocations().Where(Function(ml) ml.Project.ShortName = ProjectRouteName)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function GetMonitorLocationsForProject(ProjectId As Integer) As IEnumerable(Of MonitorLocation) Implements IMeasurementsDAL.GetMonitorLocationsForProject

        Try
            Return GetMonitorLocations().Where(Function(ml) ml.Project.Id = ProjectId)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function GetMonitorLocationsSelectList(Include0AsAll As Boolean,
                                                  Optional MeasurementTypeId As Integer = 0,
                                                  Optional ProjectId As Integer = 0,
                                                  Optional db As SECMonitoringDbContext = Nothing) As SelectList Implements IMeasurementsDAL.GetMonitorLocationsSelectList

        If db Is Nothing Then db = Me.db

        Dim monitorLocations = Me.GetMonitorLocations(db)

        ' Filter by measurement type
        If MeasurementTypeId > 0 Then
            monitorLocations = monitorLocations.Where(
                Function(ml) ml.MeasurementTypeId = MeasurementTypeId
                )
        End If

        ' Filter by project
        If ProjectId > 0 Then
            monitorLocations = monitorLocations.Where(
                Function(ml) ml.ProjectId = ProjectId
                )
        End If


        Dim mlList As New List(Of SelectListItem)
        If Include0AsAll Then
            mlList.Add(New SelectListItem With {.Value = 0,
                                                .Text = "All"})
        End If
        For Each ml In monitorLocations
            mlList.Add(New SelectListItem With {.Value = ml.Id,
                                                .Text = ml.MonitorLocationName})
        Next

        Dim selItem As String = ""
        If Include0AsAll Then
            selItem = "All"
        End If

        Return New SelectList(mlList, "Value", "Text", selItem)

    End Function
    Public Function AddMonitorLocation(MonitorLocation As MonitorLocation) As MonitorLocation Implements IMeasurementsDAL.AddMonitorLocation

        Try

            db.MonitorLocations.Add(MonitorLocation)
            SaveChanges(db)
            Return MonitorLocation

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function UpdateMonitorLocation(MonitorLocation As MonitorLocation) As MonitorLocation Implements IMeasurementsDAL.UpdateMonitorLocation

        Using db = New SECMonitoringDbContext

            Dim dbMonitorLocation = GetMonitorLocations(db).Single(Function(ml) ml.Id = MonitorLocation.Id)

            dbMonitorLocation.MonitorLocationName = MonitorLocation.MonitorLocationName
            dbMonitorLocation.Project = GetProject(MonitorLocation.Project.Id, db)
            dbMonitorLocation.HeightAboveGround = MonitorLocation.HeightAboveGround
            dbMonitorLocation.IsAFacadeLocation = MonitorLocation.IsAFacadeLocation
            dbMonitorLocation.MeasurementType = GetMeasurementType(MonitorLocation.MeasurementTypeId, db)
            dbMonitorLocation.MonitorLocationGeoCoords.Latitude = MonitorLocation.MonitorLocationGeoCoords.Latitude
            dbMonitorLocation.MonitorLocationGeoCoords.Longitude = MonitorLocation.MonitorLocationGeoCoords.Longitude

            db.Entry(dbMonitorLocation).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbMonitorLocation

        End Using

    End Function
    Public Function UpdateMonitorLocationCurrentMonitor(MonitorLocation As MonitorLocation) As MonitorLocation Implements IMeasurementsDAL.UpdateMonitorLocationCurrentMonitor

        Using db = New SECMonitoringDbContext

            Dim dbMonitorLocation = GetMonitorLocation(MonitorLocation.Id, db)

            If MonitorLocation.CurrentMonitor Is Nothing Then
                dbMonitorLocation.CurrentMonitor = Nothing
            Else
                dbMonitorLocation.CurrentMonitor = GetMonitor(MonitorLocation.CurrentMonitor.Id, db)
            End If

            db.Entry(dbMonitorLocation).State = Entity.EntityState.Modified

            Try
                SaveChanges(db)
            Catch ex As Exception
                Return Nothing
            End Try


            Return dbMonitorLocation

        End Using

    End Function
    Public Function GetMonitorLocation(ProjectShortName As String, MonitorLocationName As String) As MonitorLocation Implements IMeasurementsDAL.GetMonitorLocation

        Try
            Return db.MonitorLocations.Single(
                Function(m) LCase(m.Project.ShortName) = LCase(ProjectShortName) AndAlso
                            LCase(m.MonitorLocationName) = LCase(MonitorLocationName)
            )
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function GetMonitorLocationsForAssessmentCriterionGroup(AssessmentCriterionGroupId As Integer) As IEnumerable(Of MonitorLocation) Implements IMeasurementsDAL.GetMonitorLocationsForAssessmentCriterionGroup

        Try
            Return GetMonitorLocations().Where(Function(ml) ml.AssessmentCriteria.Select(Function(l) l.AssessmentCriterionGroup.Id).Distinct.ToList.Contains(AssessmentCriterionGroupId))
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function DeleteMonitorLocation(MonitorLocationId As Integer) As Boolean Implements IMeasurementsDAL.DeleteMonitorLocation

        Try

            Dim MonitorLocationToDelete As MonitorLocation = db.MonitorLocations.Single(Function(d) d.Id = MonitorLocationId)
            db.MonitorLocations.Remove(MonitorLocationToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function


#End Region

#Region "Monitor Location GeoCoords"


    Public Function GetMonitorLocationGeoCoords(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MonitorLocationGeoCoords) Implements IMeasurementsDAL.GetMonitorLocationGeoCoords

        If db Is Nothing Then db = Me.db

        Try

            Return db.MonitorLocationGeoCoords _
           .Include(Function(mlgc) mlgc.MonitorLocations).OrderBy(Function(mlgc) mlgc.Id)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetMonitorLocationGeoCoords(MonitorLocationGeoCoordsId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MonitorLocationGeoCoords Implements IMeasurementsDAL.GetMonitorLocationGeoCoords

        If db Is Nothing Then db = Me.db

        Try

            Return GetMonitorLocationGeoCoords(db).Single(Function(mlgc) mlgc.Id = MonitorLocationGeoCoordsId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function



#End Region

#Region "Monitor Status"

    Public Function UpdateMonitorStatus(MonitorStatus As MonitorStatus) As MonitorStatus Implements IMeasurementsDAL.UpdateMonitorStatus

        Using db = New SECMonitoringDbContext

            Dim dbMonitorStatus = db.MonitorStatuses.Single(Function(ms) ms.Id = MonitorStatus.Id)

            dbMonitorStatus.IsOnline = MonitorStatus.IsOnline
            dbMonitorStatus.PowerStatusOk = MonitorStatus.PowerStatusOk
            dbMonitorStatus.StatusComment = If(MonitorStatus.StatusComment Is Nothing, "", MonitorStatus.StatusComment)
            dbMonitorStatus.Monitor = db.Monitors.Single(Function(m) m.Id = MonitorStatus.Monitor.Id)

            db.Entry(dbMonitorStatus).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbMonitorStatus

        End Using

    End Function

#End Region

#Region "Organisations"

    Public Function GetOrganisations(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Organisation) Implements IMeasurementsDAL.GetOrganisations

        If db Is Nothing Then db = Me.db

        Try

            Return db.Organisations _
           .Include(Function(o) o.OrganisationType) _
           .Include(Function(o) o.Contacts) _
           .Include(Function(o) o.OwnedMonitors) _
           .Include(Function(o) o.Projects) _
           .Include(Function(o) o.ProjectsAsClient).OrderBy(Function(o) o.FullName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetOrganisation(OrganisationShortName As String) As Organisation Implements IMeasurementsDAL.GetOrganisation

        Return GetOrganisations.Single(Function(o) LCase(o.ShortName) = LCase(OrganisationShortName))

    End Function
    Public Function GetOrganisation(OrganisationId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Organisation Implements IMeasurementsDAL.GetOrganisation

        If db Is Nothing Then db = Me.db

        Try
            Return GetOrganisations(db).Single(Function(o) o.Id = OrganisationId)
        Catch ex As Exception
            Return Nothing
        End Try



    End Function

    Public Function AddOrganisation(Organisation As Organisation) As Organisation Implements IMeasurementsDAL.AddOrganisation

        Try
            db.Organisations.Add(Organisation)
            SaveChanges(db)
            Return Organisation

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function UpdateOrganisation(Organisation As Organisation) As Organisation Implements IMeasurementsDAL.UpdateOrganisation

        Using db = New SECMonitoringDbContext

            Dim dbOrganisation = GetOrganisations(db).Single(Function(o) o.Id = Organisation.Id)

            dbOrganisation.Address = Organisation.Address
            dbOrganisation.FullName = Organisation.FullName
            dbOrganisation.OrganisationType = GetOrganisationType(Organisation.OrganisationType.Id, db)
            dbOrganisation.ShortName = Organisation.ShortName

            db.Entry(dbOrganisation).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbOrganisation

        End Using

    End Function

    Public Function DeleteOrganisation(OrganisationId As Integer) As Boolean Implements IMeasurementsDAL.DeleteOrganisation

        Try

            Dim OrganisationToDelete As Organisation = db.Organisations.Single(Function(d) d.Id = OrganisationId)
            db.Organisations.Remove(OrganisationToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "Organisation Types"

    Public Function GetOrganisationTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of OrganisationType) Implements IMeasurementsDAL.GetOrganisationTypes

        If db Is Nothing Then db = Me.db

        Try

            Return db.OrganisationTypes.Include(Function(ot) ot.Organisations).OrderBy(Function(ot) ot.OrganisationTypeName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetOrganisationType(OrganisationTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As OrganisationType Implements IMeasurementsDAL.GetOrganisationType

        If db Is Nothing Then db = Me.db

        Try

            Return GetOrganisationTypes(db).Single(Function(ot) ot.Id = OrganisationTypeId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetOrganisationType(OrganisationTypeName As String) As OrganisationType Implements IMeasurementsDAL.GetOrganisationType

        Return GetOrganisationTypes().Single(Function(ot) LCase(ot.OrganisationTypeName) = LCase(OrganisationTypeName))

    End Function
    Public Function AddOrganisationType(OrganisationType As OrganisationType) As OrganisationType Implements IMeasurementsDAL.AddOrganisationType

        Try

            db.OrganisationTypes.Add(OrganisationType)
            SaveChanges(db)
            Return OrganisationType

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function UpdateOrganisationType(OrganisationType As OrganisationType) As OrganisationType Implements IMeasurementsDAL.UpdateOrganisationType

        Using db = New SECMonitoringDbContext

            Dim dbOrganisationType = GetOrganisationTypes(db).Single(Function(ot) ot.Id = OrganisationType.Id)

            dbOrganisationType.OrganisationTypeName = OrganisationType.OrganisationTypeName

            db.Entry(dbOrganisationType).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbOrganisationType

        End Using

    End Function
    Public Function DeleteOrganisationType(OrganisationTypeId As Integer) As Boolean Implements IMeasurementsDAL.DeleteOrganisationType

        Try

            Dim OrganisationTypeToDelete As OrganisationType = db.OrganisationTypes.Single(Function(cf) cf.Id = OrganisationTypeId)
            db.OrganisationTypes.Remove(OrganisationTypeToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "Projects"

    Public Function AddProject(Project As Project) As Project Implements IMeasurementsDAL.AddProject

        Try
            db.Projects.Add(Project)
            SaveChanges(db)
            Return Project
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function UpdateProject(Project As Project) As Project Implements IMeasurementsDAL.UpdateProject

        Using db = New SECMonitoringDbContext

            Dim dbProject = GetProject(Project.Id, db)

            dbProject.FullName = Project.FullName
            dbProject.ShortName = Project.ShortName
            dbProject.ProjectNumber = Project.ProjectNumber
            dbProject.ProjectGeoCoords.Latitude = Project.ProjectGeoCoords.Latitude
            dbProject.ProjectGeoCoords.Longitude = Project.ProjectGeoCoords.Longitude
            dbProject.MapLink = Project.MapLink
            dbProject.Country = GetCountry(Project.Country.Id, db)
            dbProject.ClientOrganisation = GetOrganisation(Project.ClientOrganisation.Id, db)

            Try
                db.Entry(dbProject).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return dbProject
            Catch ex As Exception
                Return Nothing
            End Try

        End Using

    End Function
    Public Function UpdateProjectWorkingHours(ProjectId As Integer, StandardWeeklyWorkingHours As StandardWeeklyWorkingHours) As Project Implements IMeasurementsDAL.UpdateProjectWorkingHours

        Using db = New SECMonitoringDbContext

            Dim dbProject = GetProjects(db).Single(Function(p) p.Id = ProjectId)
            dbProject.StandardWeeklyWorkingHours = StandardWeeklyWorkingHours
            db.Entry(dbProject).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbProject

        End Using

    End Function

    Public Function UpdateProjectWorkingHours(ProjectId As Integer, StandardWeeklyWorkingHours As StandardWeeklyWorkingHours, MeasurementViewIds As IEnumerable(Of Integer)) As Project Implements IMeasurementsDAL.UpdateProjectWorkingHours

        Using db = New SECMonitoringDbContext

            Dim dbProject = GetProjects(db).Single(Function(p) p.Id = ProjectId)
            ' remove existing standard daily working hours
            Dim swwh = dbProject.StandardWeeklyWorkingHours
            Dim sdwhExisting = swwh.StandardDailyWorkingHours
            db.StandardDailyWorkingHours.RemoveRange(sdwhExisting)
            ' add new standard daily working hours
            For Each sdwh In StandardWeeklyWorkingHours.StandardDailyWorkingHours
                dbProject.StandardWeeklyWorkingHours.StandardDailyWorkingHours.Add(sdwh)
            Next
            ' remove existing available measurement views
            dbProject.StandardWeeklyWorkingHours.AvailableMeasurementViews = Nothing
            ' add new available measurement views
            For Each Id In MeasurementViewIds
                dbProject.StandardWeeklyWorkingHours.AvailableMeasurementViews.Add(db.MeasurementViews.Single(Function(mv) mv.Id = Id))
            Next
            db.Entry(dbProject).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbProject

        End Using

    End Function

    Public Function GetProjects(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Project) Implements IMeasurementsDAL.GetProjects

        If db Is Nothing Then db = Me.db

        Try

            Return db.Projects _
           .Include(Function(p) p.AssessmentCriteria) _
           .Include(Function(p) p.ClientOrganisation) _
           .Include(Function(p) p.Contacts) _
           .Include(Function(p) p.Country) _
           .Include(Function(p) p.StandardWeeklyWorkingHours) _
           .Include(Function(p) p.MeasurementViews) _
           .Include(Function(p) p.MonitorLocations) _
           .Include(Function(p) p.Organisations) _
           .Include(Function(p) p.ProjectGeoCoords) _
           .Include(Function(p) p.VariedWeeklyWorkingHours).OrderBy(Function(p) p.FullName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetProject(ProjectId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Project Implements IMeasurementsDAL.GetProject

        If db Is Nothing Then db = Me.db

        Try
            Return GetProjects(db).Single(Function(p) p.Id = ProjectId)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function LoadProject(ProjectId As Integer) As Project Implements IMeasurementsDAL.LoadProject

        Return db.Projects.Single(Function(p) p.Id = ProjectId)

    End Function
    Public Function GetProject(ProjectShortName As String) As Project Implements IMeasurementsDAL.GetProject

        Try
            Return GetProjects.Single(Function(p) LCase(p.ShortName) = LCase(ProjectShortName))
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function UpdateProjectGeoCoords(ProjectGeoCoords As ProjectGeoCoords) As ProjectGeoCoords Implements IMeasurementsDAL.UpdateProjectGeoCoords
        Try
            db.ProjectGeoCoords.Attach(ProjectGeoCoords)
            db.Entry(ProjectGeoCoords).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return ProjectGeoCoords
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function GetProjects(Contact As Contact) As IEnumerable(Of Project) Implements IMeasurementsDAL.GetProjects

        Return GetProjects().Where(Function(p) p.Contacts.Select(Function(c) c.Id).Contains(Contact.Id))

    End Function

    Public Function GetProjectsSelectList(Include0AsAll As Boolean,
                                          Optional MeasurementTypeId As Integer = 0,
                                          Optional projectIds As List(Of Integer) = Nothing,
                                          Optional db As SECMonitoringDbContext = Nothing) As SelectList Implements IMeasurementsDAL.GetProjectsSelectList

        If db Is Nothing Then db = Me.db

        Dim projects = Me.GetProjects(db)

        If projectIds IsNot Nothing Then projects = projects.Where(Function(p) projectIds.Contains(p.Id))

        ' Filter by measurement type
        If MeasurementTypeId > 0 Then
            projects = projects.SelectMany(Function(p) p.MonitorLocations) _
                   .Where(Function(ml) ml.MeasurementTypeId = MeasurementTypeId) _
                   .Select(Function(ml) ml.Project).Distinct
        End If


        Dim pList As New List(Of SelectListItem)
        If Include0AsAll Then
            pList.Add(New SelectListItem With {.Value = 0,
                                               .Text = "All"})
        End If
        For Each p In projects
            pList.Add(New SelectListItem With {.Value = p.Id,
                                               .Text = p.FullName})
        Next

        Dim selItem As String = ""
        If Include0AsAll Then
            selItem = "All"
        End If

        Return New SelectList(pList, "Value", "Text", selItem)

    End Function

    Public Function AddProjectContact(ProjectId As Integer, ContactId As Integer) As Boolean Implements IMeasurementsDAL.AddProjectContact

        Try
            Dim proj = GetProject(ProjectId)
            Dim con = GetContact(ContactId)
            proj.Contacts.Add(con)
            If proj.Organisations.Select(Function(o) o.Id).ToList.Contains(con.OrganisationId) = False Then
                proj.Organisations.Add(con.Organisation)
            End If
            db.Entry(proj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveProjectContact(ProjectId As Integer, ContactId As Integer) As Boolean Implements IMeasurementsDAL.RemoveProjectContact

        Try
            Dim proj = GetProject(ProjectId)
            proj.Contacts.Remove(GetContact(ContactId))
            db.Entry(proj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function GetProjectContacts(ProjectId As Integer) As IEnumerable(Of Contact) Implements IMeasurementsDAL.GetProjectContacts

        Try
            Return GetProject(ProjectId).Contacts
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function AddProjectOrganisation(ProjectId As Integer, OrganisationId As Integer) As Boolean Implements IMeasurementsDAL.AddProjectOrganisation

        Try
            Dim obj = GetProject(ProjectId)
            obj.Organisations.Add(GetOrganisation(OrganisationId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveProjectOrganisation(ProjectId As Integer, OrganisationId As Integer) As Boolean Implements IMeasurementsDAL.RemoveProjectOrganisation

        Try
            Dim obj = GetProject(ProjectId)
            obj.Organisations.Remove(GetOrganisation(OrganisationId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function AddProjectMonitorLocation(ProjectId As Integer, MonitorLocationId As Integer) As Boolean Implements IMeasurementsDAL.AddProjectMonitorLocation

        Try
            Dim obj = GetProject(ProjectId)
            obj.MonitorLocations.Add(GetMonitorLocation(MonitorLocationId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveProjectMonitorLocation(ProjectId As Integer, MonitorLocationId As Integer) As Boolean Implements IMeasurementsDAL.RemoveProjectMonitorLocation

        Try
            Dim obj = GetProject(ProjectId)
            obj.MonitorLocations.Remove(GetMonitorLocation(MonitorLocationId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function AddProjectMeasurementView(ProjectId As Integer, MeasurementViewId As Integer) As Boolean Implements IMeasurementsDAL.AddProjectMeasurementView

        Try
            Dim obj = GetProject(ProjectId)
            obj.MeasurementViews.Add(GetMeasurementView(MeasurementViewId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveProjectMeasurementView(ProjectId As Integer, MeasurementViewId As Integer) As Boolean Implements IMeasurementsDAL.RemoveProjectMeasurementView

        Try
            Dim obj = GetProject(ProjectId)
            obj.MeasurementViews.Remove(GetMeasurementView(MeasurementViewId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function AddProjectAssessmentCriterionGroup(ProjectId As Integer, AssessmentCriterionGroupId As Integer) As Boolean Implements IMeasurementsDAL.AddProjectAssessmentCriterionGroup

        Try
            Dim obj = GetProject(ProjectId)
            obj.AssessmentCriteria.Add(GetAssessmentCriterionGroup(AssessmentCriterionGroupId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveProjectAssessmentCriterionGroup(ProjectId As Integer, AssessmentCriterionGroupId As Integer) As Boolean Implements IMeasurementsDAL.RemoveProjectAssessmentCriterionGroup

        Try
            Dim obj = GetProject(ProjectId)
            obj.AssessmentCriteria.Remove(GetAssessmentCriterionGroup(AssessmentCriterionGroupId))
            db.Entry(obj).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function DeleteProject(ProjectId As Integer) As Boolean Implements IMeasurementsDAL.DeleteProject

        Try

            Dim ProjectToDelete As Project = db.Projects.Single(Function(p) p.Id = ProjectId)
            db.Projects.Remove(ProjectToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    Public Function ProjectHasMeasurements(ProjectId As Integer) As Boolean Implements IMeasurementsDAL.ProjectHasMeasurements

        Dim RecordCount As Int32 = 0
        Dim sql As String = "SELECT CASE WHEN EXISTS (SELECT * FROM [Measurements] WHERE [ProjectId] = " + ProjectId.ToString + ") THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END"
        Using conn As New SqlConnection(MeasurementsConnectionString)
            Dim cmd As New SqlCommand(sql, conn)
            Try
                conn.Open()
                RecordCount = Convert.ToBoolean(cmd.ExecuteScalar())
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            End Try
        End Using

        Return RecordCount

    End Function

#End Region

#Region "Project Permissions"

    Public Function GetProjectPermissions(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of ProjectPermission) Implements IMeasurementsDAL.GetProjectPermissions

        If db Is Nothing Then db = Me.db

        Return db.ProjectPermissions.OrderBy(Function(pp) pp.PermissionName)

    End Function
    Public Function GetProjectPermission(UserAccessLevel As UserAccessLevel) As ProjectPermission Implements IMeasurementsDAL.GetProjectPermission

        Return GetProjectPermissions.Single(Function(pp) pp.Id = UserAccessLevel.ProjectPermissionId)

    End Function

    Public Function GetProjectPermission(ProjectPermissionId As Integer, Optional db As SECMonitoringDbContext = Nothing) As ProjectPermission Implements IMeasurementsDAL.GetProjectPermission

        If db Is Nothing Then db = Me.db

        Try
            Return GetProjectPermissions(db).Single(Function(pp) pp.Id = ProjectPermissionId)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Public Holidays"

    Public Function GetPublicHolidays(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of PublicHoliday) Implements IMeasurementsDAL.GetPublicHolidays

        If db Is Nothing Then db = Me.db

        Try

            Return db.PublicHolidays _
           .Include(Function(ph) ph.Country).OrderBy(Function(ph) ph.HolidayDate)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function AddPublicHoliday(PublicHoliday As PublicHoliday) As PublicHoliday Implements IMeasurementsDAL.AddPublicHoliday

        Try
            db.PublicHolidays.Add(PublicHoliday)
            SaveChanges(db)
            Return PublicHoliday

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function DeletePublicHoliday(PublicHolidayId As Integer) As Boolean Implements IMeasurementsDAL.DeletePublicHoliday

        Try

            Dim PublicHolidayToDelete As PublicHoliday = db.PublicHolidays.Single(Function(ph) ph.Id = PublicHolidayId)
            db.PublicHolidays.Remove(PublicHolidayToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "Standard Weekly Working Hours"

    Public Function GetStandardWeeklyWorkingHours(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of StandardWeeklyWorkingHours) Implements IMeasurementsDAL.GetStandardWeeklyWorkingHours

        If db Is Nothing Then db = Me.db

        Try

            Return db.StandardWeeklyWorkingHours _
           .Include(Function(swwh) swwh.Project) _
           .Include(Function(swwh) swwh.AvailableMeasurementViews)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetStandardWeeklyWorkingHours(StandardWeeklyWorkingHoursId As Integer, Optional db As SECMonitoringDbContext = Nothing) As StandardWeeklyWorkingHours Implements IMeasurementsDAL.GetStandardWeeklyWorkingHours

        If db Is Nothing Then db = Me.db

        Try

            Return GetStandardWeeklyWorkingHours(db).Single(Function(swwh) swwh.Id = StandardWeeklyWorkingHoursId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function AddStandardWeeklyWorkingHoursMeasurementView(StandardWeeklyWorkingHoursId As Integer, MeasurementViewId As Integer) As Boolean Implements IMeasurementsDAL.AddStandardWeeklyWorkingHoursMeasurementView

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = db.StandardWeeklyWorkingHours.Single(Function(vwwh) vwwh.Id = StandardWeeklyWorkingHoursId)
                obj.AvailableMeasurementViews.Add(db.MeasurementViews.Single(Function(mv) mv.Id = MeasurementViewId))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function RemoveStandardWeeklyWorkingHoursMeasurementView(StandardWeeklyWorkingHoursId As Integer, MeasurementViewId As Integer) As Boolean Implements IMeasurementsDAL.RemoveStandardWeeklyWorkingHoursMeasurementView

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = db.StandardWeeklyWorkingHours.Single(Function(vwwh) vwwh.Id = StandardWeeklyWorkingHoursId)
                obj.AvailableMeasurementViews.Remove(db.MeasurementViews.Single(Function(mv) mv.Id = MeasurementViewId))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

#End Region

#Region "System Messages"

    Public Function AddSystemMessage(SystemMessage As SystemMessage) As SystemMessage Implements IMeasurementsDAL.AddSystemMessage

        Try

            db.SystemMessages.Add(SystemMessage)
            SaveChanges(db)
            Return SystemMessage

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function DeleteSystemMessage(SystemMessageId As Integer) As Boolean Implements IMeasurementsDAL.DeleteSystemMessage

        Try

            Dim SystemMessageToDelete As SystemMessage = db.SystemMessages.Single(Function(sm) sm.Id = SystemMessageId)
            db.SystemMessages.Remove(SystemMessageToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    Public Function GetSystemMessages(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of SystemMessage) Implements IMeasurementsDAL.GetSystemMessages

        If db Is Nothing Then db = Me.db

        Try
            Return db.SystemMessages
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

    Public Function GetSystemMessage(systemMessageId As Integer, Optional db As SECMonitoringDbContext = Nothing) As SystemMessage Implements IMeasurementsDAL.GetSystemMessage

        If db Is Nothing Then db = Me.db

        Try
            Return db.SystemMessages.Single(Function(sm) sm.Id = systemMessageId)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function


    Public Function UpdateSystemMessage(SystemMessage As SystemMessage) As SystemMessage Implements IMeasurementsDAL.UpdateSystemMessage

        Using db = New SECMonitoringDbContext

            Dim dbSystemMessage = GetSystemMessages(db).Single(Function(sm) sm.Id = SystemMessage.Id)

            dbSystemMessage.MessageText = SystemMessage.MessageText
            dbSystemMessage.DateTimeCreated = SystemMessage.DateTimeCreated

            db.Entry(dbSystemMessage).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbSystemMessage

        End Using

    End Function

#End Region

#Region "Threshold Types"

    Public Function GetThresholdType(ThresholdTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As ThresholdType Implements IMeasurementsDAL.GetThresholdType

        If db Is Nothing Then db = Me.db

        Try

            Return GetThresholdTypes(db).Single(Function(tt) tt.Id = ThresholdTypeId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function GetThresholdTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of ThresholdType) Implements IMeasurementsDAL.GetThresholdTypes

        If db Is Nothing Then db = Me.db

        Try

            Return db.ThresholdTypes.OrderBy(Function(tt) tt.ThresholdTypeName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

#End Region

#Region "Users"

    Public Function GetUsers(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of User) Implements IMeasurementsDAL.GetUsers

        If db Is Nothing Then db = Me.db

        Try
            Return db.Users _
            .Include(Function(u) u.Contact) _
            .Include(Function(u) u.UserAccessLevel) _
            .OrderBy(Function(u) u.UserName)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function GetUser(UserName As String) As User Implements IMeasurementsDAL.GetUser

        Try

            Return GetUsers().Single(Function(u) u.UserName.ToLower() = UserName.ToLower())

        Catch ex As Exception
            Trace.WriteLine(ex.Message)
            Return Nothing

        End Try

    End Function
    Public Function GetUser(UserId As Integer, Optional db As SECMonitoringDbContext = Nothing) As User Implements IMeasurementsDAL.GetUser

        If db Is Nothing Then db = Me.db

        Try
            Return GetUsers(db).Single(Function(u) u.Id = UserId)
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function UpdateUser(User As User) As User Implements IMeasurementsDAL.UpdateUser

        Using db = New SECMonitoringDbContext

            Dim dbUser = GetUsers(db).Single(Function(u) u.Id = User.Id)

            dbUser.UserName = User.UserName
            dbUser.Contact = GetContact(User.Contact.Id, db)
            dbUser.UserAccessLevel = GetUserAccessLevel(User.UserAccessLevel.Id, db)
            dbUser.ConsecutiveUnsuccessfulLogins = User.ConsecutiveUnsuccessfulLogins
            dbUser.IsLocked = User.IsLocked
            dbUser.ReceivesLockNotifications = User.ReceivesLockNotifications

            db.Entry(dbUser).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbUser

        End Using

    End Function
    Public Function ChangeUserPassword(User As User, Password As String) As User Implements IMeasurementsDAL.ChangeUserPassword

        Using db = New SECMonitoringDbContext

            Dim dbUser = GetUsers(db).Single(Function(u) u.Id = User.Id)

            dbUser.Password = Password

            db.Entry(dbUser).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbUser

        End Using

    End Function
    Public Function AddUser(User As User) As User Implements IMeasurementsDAL.AddUser

        Try

            db.Users.Add(User)
            SaveChanges(db)
            Return User

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function DeleteUser(UserId As Integer) As Boolean Implements IMeasurementsDAL.DeleteUser

        Try

            Dim UserToDelete As User = db.Users.Single(Function(u) u.Id = UserId)
            db.Users.Remove(UserToDelete)
            SaveChanges(db)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region

#Region "User Access Levels"

    Public Function GetUserAccessLevels(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of UserAccessLevel) Implements IMeasurementsDAL.GetUserAccessLevels

        If db Is Nothing Then db = Me.db

        Try

            Return db.UserAccessLevels _
           .Include(Function(ual) ual.ProjectPermission) _
           .Include(Function(ual) ual.Users) _
           .OrderBy(Function(ual) ual.AccessLevelName)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetUserAccessLevel(UserAccessLevelId As Integer, Optional db As SECMonitoringDbContext = Nothing) As UserAccessLevel Implements IMeasurementsDAL.GetUserAccessLevel

        If db Is Nothing Then db = Me.db

        Try

            Return GetUserAccessLevels(db).Single(Function(ual) ual.Id = UserAccessLevelId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetUserAccessLevel(AccessLevelName As String) As UserAccessLevel Implements IMeasurementsDAL.GetUserAccessLevel

        Try
            Return GetUserAccessLevels().Single(Function(ual) LCase(ual.AccessLevelName) = LCase(AccessLevelName))
        Catch ex As Exception
            Return Nothing
        End Try

    End Function
    Public Function AddUserAccessLevel(UserAccessLevel As UserAccessLevel) As UserAccessLevel Implements IMeasurementsDAL.AddUserAccessLevel

        Try

            db.UserAccessLevels.Add(UserAccessLevel)
            SaveChanges(db)
            Return UserAccessLevel

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function DeleteUserAccessLevel(UserAccessLevelId As Integer) As Boolean Implements IMeasurementsDAL.DeleteUserAccessLevel

        Try

            Dim UserAccessLevelToDelete As UserAccessLevel = db.UserAccessLevels.Single(Function(u) u.Id = UserAccessLevelId)
            db.UserAccessLevels.Remove(UserAccessLevelToDelete)
            SaveChanges(db)
            Return True
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function UpdateUserAccessLevel(UserAccessLevel As UserAccessLevel) As UserAccessLevel Implements IMeasurementsDAL.UpdateUserAccessLevel

        Using db = New SECMonitoringDbContext

            Dim dbUserAccessLevel = GetUserAccessLevels(db).Single(Function(u) u.Id = UserAccessLevel.Id)

            ' Basic Properties
            dbUserAccessLevel.AccessLevelName = UserAccessLevel.AccessLevelName
            dbUserAccessLevel.ProjectPermission = db.ProjectPermissions.Single(Function(pp) pp.Id = UserAccessLevel.ProjectPermissionId)

            dbUserAccessLevel.CanCreateAssessmentCriteria = UserAccessLevel.CanCreateAssessmentCriteria
            dbUserAccessLevel.CanEditAssessmentCriteria = UserAccessLevel.CanEditAssessmentCriteria
            dbUserAccessLevel.CanViewAssessmentCriteria = UserAccessLevel.CanViewAssessmentCriteria
            dbUserAccessLevel.CanDeleteAssessmentCriteria = UserAccessLevel.CanDeleteAssessmentCriteria
            dbUserAccessLevel.CanViewAssessments = UserAccessLevel.CanViewAssessments
            dbUserAccessLevel.CanCreateCalculationFilters = UserAccessLevel.CanCreateCalculationFilters
            dbUserAccessLevel.CanViewCalculationFilterList = UserAccessLevel.CanViewCalculationFilterList
            dbUserAccessLevel.CanViewCalculationFilterDetails = UserAccessLevel.CanViewCalculationFilterDetails
            dbUserAccessLevel.CanEditCalculationFilters = UserAccessLevel.CanEditCalculationFilters
            dbUserAccessLevel.CanDeleteCalculationFilters = UserAccessLevel.CanDeleteCalculationFilters
            dbUserAccessLevel.CanCreateContacts = UserAccessLevel.CanCreateContacts
            dbUserAccessLevel.CanViewContactList = UserAccessLevel.CanViewContactList
            dbUserAccessLevel.CanViewContactDetails = UserAccessLevel.CanViewContactDetails
            dbUserAccessLevel.CanEditContacts = UserAccessLevel.CanEditContacts
            dbUserAccessLevel.CanDeleteContacts = UserAccessLevel.CanDeleteContacts
            dbUserAccessLevel.CanCreateCountries = UserAccessLevel.CanCreateCountries
            dbUserAccessLevel.CanViewCountryList = UserAccessLevel.CanViewCountryList
            dbUserAccessLevel.CanViewCountryDetails = UserAccessLevel.CanViewCountryDetails
            dbUserAccessLevel.CanEditCountries = UserAccessLevel.CanEditCountries
            dbUserAccessLevel.CanDeleteCountries = UserAccessLevel.CanDeleteCountries
            dbUserAccessLevel.CanViewDocumentDetails = UserAccessLevel.CanViewDocumentDetails
            dbUserAccessLevel.CanViewDocumentList = UserAccessLevel.CanViewDocumentList
            dbUserAccessLevel.CanCreateDocuments = UserAccessLevel.CanCreateDocuments
            dbUserAccessLevel.CanEditDocuments = UserAccessLevel.CanEditDocuments
            dbUserAccessLevel.CanDeleteDocuments = UserAccessLevel.CanDeleteDocuments
            dbUserAccessLevel.CanViewDocumentTypeList = UserAccessLevel.CanViewDocumentTypeList
            dbUserAccessLevel.CanViewDocumentTypeDetails = UserAccessLevel.CanViewDocumentTypeDetails
            dbUserAccessLevel.CanEditDocumentTypes = UserAccessLevel.CanEditDocumentTypes
            dbUserAccessLevel.CanDeleteDocumentTypes = UserAccessLevel.CanDeleteDocumentTypes
            dbUserAccessLevel.CanCreateDocumentTypes = UserAccessLevel.CanCreateDocumentTypes
            dbUserAccessLevel.CanViewMeasurements = UserAccessLevel.CanViewMeasurements
            dbUserAccessLevel.CanUploadMeasurements = UserAccessLevel.CanUploadMeasurements
            dbUserAccessLevel.CanDeleteMeasurements = UserAccessLevel.CanDeleteMeasurements
            dbUserAccessLevel.CanViewMeasurementCommentList = UserAccessLevel.CanViewMeasurementCommentList
            dbUserAccessLevel.CanDeleteMeasurementComments = UserAccessLevel.CanDeleteMeasurementComments
            dbUserAccessLevel.CanCreateMeasurementComments = UserAccessLevel.CanCreateMeasurementComments
            dbUserAccessLevel.CanViewMeasurementCommentTypeList = UserAccessLevel.CanViewMeasurementCommentTypeList
            dbUserAccessLevel.CanViewMeasurementCommentTypeDetails = UserAccessLevel.CanViewMeasurementCommentTypeDetails
            dbUserAccessLevel.CanCreateMeasurementCommentTypes = UserAccessLevel.CanCreateMeasurementCommentTypes
            dbUserAccessLevel.CanEditMeasurementCommentTypes = UserAccessLevel.CanEditMeasurementCommentTypes
            dbUserAccessLevel.CanDeleteMeasurementCommentTypes = UserAccessLevel.CanDeleteMeasurementCommentTypes
            dbUserAccessLevel.CanViewMeasurementFileDetails = UserAccessLevel.CanViewMeasurementFileDetails
            dbUserAccessLevel.CanViewMeasurementFileList = UserAccessLevel.CanViewMeasurementFileList
            dbUserAccessLevel.CanDeleteMeasurementFiles = UserAccessLevel.CanDeleteMeasurementFiles
            dbUserAccessLevel.CanCreateMeasurementMetrics = UserAccessLevel.CanCreateMeasurementMetrics
            dbUserAccessLevel.CanViewMeasurementMetricList = UserAccessLevel.CanViewMeasurementMetricList
            dbUserAccessLevel.CanViewMeasurementMetricDetails = UserAccessLevel.CanViewMeasurementMetricDetails
            dbUserAccessLevel.CanEditMeasurementMetrics = UserAccessLevel.CanEditMeasurementMetrics
            dbUserAccessLevel.CanDeleteMeasurementMetrics = UserAccessLevel.CanDeleteMeasurementMetrics
            dbUserAccessLevel.CanViewMeasurementViewList = UserAccessLevel.CanViewMeasurementViewList
            dbUserAccessLevel.CanViewMeasurementViewDetails = UserAccessLevel.CanViewMeasurementViewDetails
            dbUserAccessLevel.CanCreateMeasurementViews = UserAccessLevel.CanCreateMeasurementViews
            dbUserAccessLevel.CanEditMeasurementViews = UserAccessLevel.CanEditMeasurementViews
            dbUserAccessLevel.CanDeleteMeasurementViews = UserAccessLevel.CanDeleteMeasurementViews
            dbUserAccessLevel.CanViewMonitorDetails = UserAccessLevel.CanViewMonitorDetails
            dbUserAccessLevel.CanViewMonitorList = UserAccessLevel.CanViewMonitorList
            dbUserAccessLevel.CanCreateMonitors = UserAccessLevel.CanCreateMonitors
            dbUserAccessLevel.CanDeleteMonitors = UserAccessLevel.CanDeleteMonitors
            dbUserAccessLevel.CanEditMonitors = UserAccessLevel.CanEditMonitors
            dbUserAccessLevel.CanViewMonitorDeploymentRecordList = UserAccessLevel.CanViewMonitorDeploymentRecordList
            dbUserAccessLevel.CanDeleteMonitorDeploymentRecords = UserAccessLevel.CanDeleteMonitorDeploymentRecords
            dbUserAccessLevel.CanViewMonitorDeploymentRecordDetails = UserAccessLevel.CanViewMonitorDeploymentRecordDetails
            dbUserAccessLevel.CanCreateDeploymentRecords = UserAccessLevel.CanCreateDeploymentRecords
            dbUserAccessLevel.CanEndMonitorDeployments = UserAccessLevel.CanEndMonitorDeployments
            dbUserAccessLevel.CanViewMonitorLocationDetails = UserAccessLevel.CanViewMonitorLocationDetails
            dbUserAccessLevel.CanViewMonitorLocationList = UserAccessLevel.CanViewMonitorLocationList
            dbUserAccessLevel.CanDeleteMonitorLocations = UserAccessLevel.CanDeleteMonitorLocations
            dbUserAccessLevel.CanViewSelectMonitorLocations = UserAccessLevel.CanViewSelectMonitorLocations
            dbUserAccessLevel.CanEditMonitorLocations = UserAccessLevel.CanEditMonitorLocations
            dbUserAccessLevel.CanCreateMonitorLocations = UserAccessLevel.CanCreateMonitorLocations
            dbUserAccessLevel.CanEditOrganisations = UserAccessLevel.CanEditOrganisations
            dbUserAccessLevel.CanViewOrganisationDetails = UserAccessLevel.CanViewOrganisationDetails
            dbUserAccessLevel.CanViewOrganisationList = UserAccessLevel.CanViewOrganisationList
            dbUserAccessLevel.CanCreateOrganisations = UserAccessLevel.CanCreateOrganisations
            dbUserAccessLevel.CanDeleteOrganisations = UserAccessLevel.CanDeleteOrganisations
            dbUserAccessLevel.CanCreateOrganisationTypes = UserAccessLevel.CanCreateOrganisationTypes
            dbUserAccessLevel.CanDeleteOrganisationTypes = UserAccessLevel.CanDeleteOrganisationTypes
            dbUserAccessLevel.CanEditOrganisationTypes = UserAccessLevel.CanEditOrganisationTypes
            dbUserAccessLevel.CanViewOrganisationTypeList = UserAccessLevel.CanViewOrganisationTypeList
            dbUserAccessLevel.CanViewOrganisationTypeDetails = UserAccessLevel.CanViewOrganisationTypeDetails
            dbUserAccessLevel.CanCreateProjects = UserAccessLevel.CanCreateProjects
            dbUserAccessLevel.CanDeleteProjects = UserAccessLevel.CanDeleteProjects
            dbUserAccessLevel.CanEditProjects = UserAccessLevel.CanEditProjects
            dbUserAccessLevel.CanViewProjectDetails = UserAccessLevel.CanViewProjectDetails
            dbUserAccessLevel.CanViewProjectList = UserAccessLevel.CanViewProjectList
            dbUserAccessLevel.CanCreatePublicHolidays = UserAccessLevel.CanCreatePublicHolidays
            dbUserAccessLevel.CanDeletePublicHolidays = UserAccessLevel.CanDeletePublicHolidays
            dbUserAccessLevel.CanCreateSystemMessages = UserAccessLevel.CanCreateSystemMessages
            dbUserAccessLevel.CanEditSystemMessages = UserAccessLevel.CanEditSystemMessages
            dbUserAccessLevel.CanDeleteSystemMessages = UserAccessLevel.CanDeleteSystemMessages
            dbUserAccessLevel.CanViewSystemMessages = UserAccessLevel.CanViewSystemMessages
            dbUserAccessLevel.CanCreateUsers = UserAccessLevel.CanCreateUsers
            dbUserAccessLevel.CanDeleteUsers = UserAccessLevel.CanDeleteUsers
            dbUserAccessLevel.CanEditUsers = UserAccessLevel.CanEditUsers
            dbUserAccessLevel.CanInitiatePasswordResets = UserAccessLevel.CanInitiatePasswordResets
            dbUserAccessLevel.CanViewUserDetails = UserAccessLevel.CanViewUserDetails
            dbUserAccessLevel.CanViewUserList = UserAccessLevel.CanViewUserList
            dbUserAccessLevel.CanCreateUserAccessLevels = UserAccessLevel.CanCreateUserAccessLevels
            dbUserAccessLevel.CanDeleteUserAccessLevels = UserAccessLevel.CanDeleteUserAccessLevels
            dbUserAccessLevel.CanEditUserAccessLevels = UserAccessLevel.CanEditUserAccessLevels
            dbUserAccessLevel.CanViewUserAccessLevelDetails = UserAccessLevel.CanViewUserAccessLevelDetails
            dbUserAccessLevel.CanViewUserAccessLevelList = UserAccessLevel.CanViewUserAccessLevelList

            db.Entry(dbUserAccessLevel).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbUserAccessLevel

        End Using

    End Function

#End Region

#Region "Varied Weekly Working Hours"

    Public Function GetVariedWeeklyWorkingHours(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of VariedWeeklyWorkingHours) Implements IMeasurementsDAL.GetVariedWeeklyWorkingHours

        If db Is Nothing Then db = Me.db

        Try

            Return db.VariedWeeklyWorkingHours _
           .Include(Function(vwwh) vwwh.AvailableMeasurementViews).OrderBy(Function(vwwh) vwwh.StartDate)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function
    Public Function GetVariedWeeklyWorkingHours(VariedWeeklyWorkingHoursId As Integer, Optional db As SECMonitoringDbContext = Nothing) As VariedWeeklyWorkingHours Implements IMeasurementsDAL.GetVariedWeeklyWorkingHours

        If db Is Nothing Then db = Me.db

        Try

            Return GetVariedWeeklyWorkingHours(db).Single(Function(vwwh) vwwh.Id = VariedWeeklyWorkingHoursId)

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function AddProjectVariedWeeklyWorkingHours(ProjectId As Integer, VariedWeeklyWorkingHours As VariedWeeklyWorkingHours) As VariedWeeklyWorkingHours Implements IMeasurementsDAL.AddProjectVariedWeeklyWorkingHours

        Try

            Dim Project = GetProject(ProjectId)
            Project.VariedWeeklyWorkingHours.Add(VariedWeeklyWorkingHours)
            db.Entry(Project).State = Entity.EntityState.Modified
            SaveChanges(db)
            Return VariedWeeklyWorkingHours

        Catch ex As Exception

            Return Nothing

        End Try

    End Function

    Public Function UpdateVariedWeeklyWorkingHours(VariedWeeklyWorkingHours As VariedWeeklyWorkingHours) As VariedWeeklyWorkingHours Implements IMeasurementsDAL.UpdateVariedWeeklyWorkingHours

        Using db = New SECMonitoringDbContext

            Dim dbVariedWeeklyWorkingHours = GetVariedWeeklyWorkingHours(db).Single(Function(vwwh) vwwh.Id = VariedWeeklyWorkingHours.Id)

            dbVariedWeeklyWorkingHours.StartDate = VariedWeeklyWorkingHours.StartDate
            dbVariedWeeklyWorkingHours.EndDate = VariedWeeklyWorkingHours.EndDate

            db.Entry(dbVariedWeeklyWorkingHours).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbVariedWeeklyWorkingHours

        End Using

    End Function
    Public Function UpdateVariedWeeklyWorkingHoursVariedDailyWorkingHours(VariedWeeklyWorkingHoursId As Integer, VariedDailyWorkingHours As IEnumerable(Of VariedDailyWorkingHours)) As VariedWeeklyWorkingHours Implements IMeasurementsDAL.UpdateVariedWeeklyWorkingHoursVariedDailyWorkingHours

        Using db = New SECMonitoringDbContext

            Dim dbVariedWeeklyWorkingHours = GetVariedWeeklyWorkingHours(db).Single(Function(vwwh) vwwh.Id = VariedWeeklyWorkingHoursId)

            For Each vdwh In dbVariedWeeklyWorkingHours.VariedDailyWorkingHours.ToList
                dbVariedWeeklyWorkingHours.VariedDailyWorkingHours.Remove(vdwh)
                db.VariedDailyWorkingHours.Remove(vdwh)
            Next
            For Each vdwh In VariedDailyWorkingHours.ToList
                vdwh.VariedWeeklyWorkingHours = dbVariedWeeklyWorkingHours
                dbVariedWeeklyWorkingHours.VariedDailyWorkingHours.Add(vdwh)
            Next

            db.Entry(dbVariedWeeklyWorkingHours).State = Entity.EntityState.Modified
            SaveChanges(db)

            Return dbVariedWeeklyWorkingHours

        End Using

    End Function

    Public Function AddVariedWeeklyWorkingHoursMeasurementView(VariedWeeklyWorkingHoursId As Integer, MeasurementViewId As Integer) As Boolean Implements IMeasurementsDAL.AddVariedWeeklyWorkingHoursMeasurementView

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = db.VariedWeeklyWorkingHours.Single(Function(vwwh) vwwh.Id = VariedWeeklyWorkingHoursId)
                obj.AvailableMeasurementViews.Add(db.MeasurementViews.Single(Function(mv) mv.Id = MeasurementViewId))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function RemoveVariedWeeklyWorkingHoursMeasurementView(VariedWeeklyWorkingHoursId As Integer, MeasurementViewId As Integer) As Boolean Implements IMeasurementsDAL.RemoveVariedWeeklyWorkingHoursMeasurementView

        Try
            Using db = New SECMonitoringDbContext
                Dim obj = db.VariedWeeklyWorkingHours.Single(Function(vwwh) vwwh.Id = VariedWeeklyWorkingHoursId)
                obj.AvailableMeasurementViews.Remove(db.MeasurementViews.Single(Function(mv) mv.Id = MeasurementViewId))
                db.Entry(obj).State = Entity.EntityState.Modified
                SaveChanges(db)
                Return True
            End Using
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function DeleteVariedWeeklyWorkingHours(VariedWeeklyWorkingHoursId As Integer) As Boolean Implements IMeasurementsDAL.DeleteVariedWeeklyWorkingHours

        Try
            Dim VariedWeeklyWorkingHoursToDelete As VariedWeeklyWorkingHours = db.VariedWeeklyWorkingHours.Single(Function(vwwh) vwwh.Id = VariedWeeklyWorkingHoursId)
            VariedWeeklyWorkingHoursToDelete.AvailableMeasurementViews.Clear()
            Dim vdwhs = db.VariedDailyWorkingHours.Where(Function(dwh) dwh.VariedWeeklyWorkingHoursId = VariedWeeklyWorkingHoursToDelete.Id).ToList
            For Each vdwh In vdwhs
                db.VariedDailyWorkingHours.Remove(vdwh)
            Next
            db.VariedWeeklyWorkingHours.Remove(VariedWeeklyWorkingHoursToDelete)
            SaveChanges(db)
            Return True
        Catch ex As Exception

            Return False

        End Try

    End Function

#End Region


    Public Sub SaveChanges() Implements IMeasurementsDAL.SaveChanges

        SaveChanges(db)

    End Sub

    Public Sub Dispose() Implements IMeasurementsDAL.Dispose

        db.Dispose()

    End Sub

    Public Shared Function ConvertToDataTable(Of T)(list As IList(Of T), Optional OrderedColumnNames As String() = Nothing) As DataTable

        Dim table As New DataTable()
        Dim propertyDescriptorCollection As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim values As Object()

        If OrderedColumnNames Is Nothing Then

            For i As Integer = 0 To propertyDescriptorCollection.Count - 1
                Dim propertyDescriptor As PropertyDescriptor = propertyDescriptorCollection(i)
                Dim propType As Type = propertyDescriptor.PropertyType
                If propType.IsGenericType AndAlso propType.GetGenericTypeDefinition() = GetType(Nullable(Of )) Then
                    table.Columns.Add(propertyDescriptor.Name, Nullable.GetUnderlyingType(propType))
                Else
                    table.Columns.Add(propertyDescriptor.Name, propType)
                End If
            Next

            values = New Object(propertyDescriptorCollection.Count - 1) {}

        Else

            For Each colName In OrderedColumnNames

                Dim propertyDescriptor As PropertyDescriptor = Nothing
                For Each pd As PropertyDescriptor In propertyDescriptorCollection
                    If pd.Name = colName Then
                        propertyDescriptor = pd
                        Exit For
                    End If
                Next
                Dim propType As Type = propertyDescriptor.PropertyType
                If propType.IsGenericType AndAlso propType.GetGenericTypeDefinition() = GetType(Nullable(Of )) Then
                    table.Columns.Add(propertyDescriptor.Name, Nullable.GetUnderlyingType(propType))
                Else
                    table.Columns.Add(propertyDescriptor.Name, propType)
                End If
            Next

            values = New Object(OrderedColumnNames.Count - 1) {}

        End If

        For Each listItem As T In list
            For i As Integer = 0 To values.Length - 1
                If OrderedColumnNames Is Nothing Then
                    values(i) = propertyDescriptorCollection(i).GetValue(listItem)
                Else
                    For Each pd As PropertyDescriptor In propertyDescriptorCollection
                        If pd.Name = OrderedColumnNames(i) Then
                            values(i) = propertyDescriptorCollection(i).GetValue(listItem)
                            Exit For
                        End If
                    Next
                End If
            Next
            table.Rows.Add(values)
        Next

        Return table

    End Function


End Class