Imports System.Runtime.CompilerServices

#Region "Measurements"

Public Interface IMeasurementType

    Enum MeasurementTypes

        Noise = 1
        Vibration = 2
        AirQuality = 3
        Dust = 4
        Wind = 5
        Climate = 6

    End Enum

    ReadOnly Property getMeasurementTypeName As String

End Interface

''' <summary>
''' Interface to the scalar-data of a measurement.
''' </summary>
''' <remarks></remarks>
Public Interface IMeasurementScalarData

    ReadOnly Property getStartDateTime As Date
    ReadOnly Property getDuration As Double
    ReadOnly Property getLevel As Double
    ReadOnly Property getEndDateTime As Date

End Interface

''' <summary>
''' Interface to the meta-data of a measurement.
''' </summary>
''' <remarks></remarks>
Public Interface IMeasurementMetaData

    ReadOnly Property getMetric As MeasurementMetric
    ReadOnly Property getMonitor As Monitor
    ReadOnly Property getMonitorLocation As MonitorLocation
    ReadOnly Property getProject As Project

End Interface

''' <summary>
''' Interface to a measurement with both its scalar- and meta-data.
''' </summary>
''' <remarks></remarks>
Public Interface IMeasurement

    Inherits IMeasurementScalarData, IMeasurementMetaData

End Interface

''' <summary>
''' Interface to the scalar data of a list of measurements which has been passed through a CalculationFilter.
''' </summary>
''' <remarks></remarks>
Public Interface IFilteredMeasurementScalarDatas

    ReadOnly Property getNumberOfInputMeasurements As Integer
    ReadOnly Property getTotalInputMeasurementsDuration As Double
    ReadOnly Property hasMissingInputMeasurements As Boolean
    ReadOnly Property getStartDateTime As Date
    ReadOnly Property getEndDateTime As Date
    ReadOnly Property getFilterDuration As Double
    ReadOnly Property getFilteredLevel As Double
    ReadOnly Property getFilter As CalculationFilter

End Interface

''' <summary>
''' Interface to the scalar- and meta-data of a list of measurements which has been passed through a CalculationFilter.
''' </summary>
''' <remarks></remarks>
Public Interface IFilteredMeasurements

    Inherits IFilteredMeasurementScalarDatas

    ReadOnly Property getMetric As MeasurementMetric
    ReadOnly Property getMonitor As Monitor
    ReadOnly Property getMonitorLocation As MonitorLocation
    ReadOnly Property getProject As Project


End Interface

''' <summary>
''' Interface to a sequence of lists of measurements which have been passed through a CalculationFilter
''' </summary>
''' <remarks></remarks>
Public Interface IFilteredMeasurementsSequence

    ReadOnly Property getMetric As MeasurementMetric
    ReadOnly Property getFilteredMeasurementsList As IEnumerable(Of IFilteredMeasurements)
    ReadOnly Property getFilter As CalculationFilter
    ReadOnly Property getMeasurementStartDateTimes As IEnumerable(Of Date)
    ReadOnly Property getMeasurementLevels As IEnumerable(Of Double)
    Function hasMeasurementAtDateTime(StartDateTime As Date) As Boolean
    Function getMeasurementAtDateTime(StartDateTime As Date) As IFilteredMeasurements
    Function hasMeasurementsOnDate(MeasurementDate As Date) As Boolean
    Function getNumMeasurementsOnDate(MeasurementDate As Date) As Integer
    Function getMeasurementsOnDate(MeasurementDate As Date) As IEnumerable(Of IFilteredMeasurements)
    Function Count() As Integer

End Interface

''' <summary>
''' Interface to parameters to use to read measurements from a database.
''' </summary>
''' <remarks></remarks>
Public Interface IReadMeasurementParameters

    Property StartDate As Date
    Property EndDate As Date
    Property FilterText As String
    Property Metric As MeasurementMetric
    Property Monitor As Monitor
    Property MonitorLocation As MonitorLocation
    Property MeasurementType As MeasurementType

End Interface

#End Region

Module MeasurementListExtensionMethods

    <Extension()> Public Function StartDateTimes(ByRef MonitoringDataList As IEnumerable(Of IMeasurement)) As IEnumerable(Of Date)

        Return (From d In MonitoringDataList Select d.getStartDateTime).ToList

    End Function
    <Extension()> Public Function DistinctStartDates(ByRef MonitoringDataList As IEnumerable(Of IMeasurement)) As IEnumerable(Of Date)

        Dim DSDs As List(Of Date) = (From mds In MonitoringDataList Select mds.getStartDateTime.Date).Distinct.ToList
        DSDs.Sort()
        Return DSDs

    End Function
    <Extension()> Public Function Levels(ByRef MonitoringDataList As IEnumerable(Of IMeasurement)) As IEnumerable(Of Double)

        Return (From d In MonitoringDataList Select d.getLevel).ToList

    End Function

    <Extension()> Public Function DistinctStartTimes(ByRef MonitoringDataList As IEnumerable(Of IMeasurement)) As List(Of TimeSpan)

        Dim DSTs = (From mds In MonitoringDataList Select mds.getStartDateTime.TimeOfDay).Distinct.ToList
        DSTs.Sort()
        Return DSTs

    End Function
    <Extension()> Public Function DistinctStartTimesOfHour(ByRef MonitoringDataList As IEnumerable(Of IMeasurement)) As List(Of TimeSpan)

        Dim DSTs = (From mds In MonitoringDataList Select mds.getStartDateTime.TimeOfDay.Add(New TimeSpan(0, -mds.getStartDateTime.Hour, 0, 0, 0))).Distinct.ToList
        DSTs.Sort()
        Return DSTs

    End Function
    <Extension()> Public Function DistinctMonitors(ByRef MonitoringDataList As IEnumerable(Of IMeasurement)) As List(Of Monitor)

        Return (From mds In MonitoringDataList Select mds.getMonitor).Distinct.ToList

    End Function
    <Extension()> Public Function DistinctMetrics(ByRef MonitoringDataList As IEnumerable(Of IMeasurement)) As List(Of MeasurementMetric)

        Return (From mds In MonitoringDataList Select mds.getMetric).Distinct.ToList

    End Function

End Module

Public Interface IMeasurementsDAL

#Region "Assessment Criteria"

#Region "Groups"

    Function GetAssessmentCriterionGroups(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of AssessmentCriterionGroup)
    Function GetAssessmentCriterionGroup(AssessmentCriterionGroupId As Integer, Optional db As SECMonitoringDbContext = Nothing) As AssessmentCriterionGroup

    Function GetAssessmentCriterionGroup(ProjectShortName As String, GroupName As String) As AssessmentCriterionGroup
    Function UpdateAssessmentCriterionGroup(AssessmentCriterionGroup As AssessmentCriterionGroup) As AssessmentCriterionGroup

    Function GetThresholdAggregateDurations(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of ThresholdAggregateDuration)
    Function GetThresholdAggregateDuration(ThresholdAggregateDurationId As Integer, Optional db As SECMonitoringDbContext = Nothing) As ThresholdAggregateDuration
    Function GetAssessmentPeriodDurationTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of AssessmentPeriodDurationType)
    Function GetAssessmentPeriodDurationType(AssessmentPeriodDurationTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As AssessmentPeriodDurationType
    Function AddAssessmentCriterionGroup(AssessmentCriterionGroup As AssessmentCriterionGroup) As AssessmentCriterionGroup
    Function GetAssessmentCriterionGroupsForProject(ProjectId As Integer) As IEnumerable(Of AssessmentCriterionGroup)

    Function DeleteAssessmentCriterionGroup(AssessmentCriterionGroupId As Integer) As Boolean

#End Region

#Region "Individual Criteria"

    Function AddAssessmentCriterion(AssessmentCriterion As AssessmentCriterion) As AssessmentCriterion
    Function GetAssessmentCriteria(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of AssessmentCriterion)
    Function GetAssessmentCriteria(CriteriaIds As IEnumerable(Of Integer),
                                   Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of AssessmentCriterion)
    Function GetAssessmentCriterion(AssessmentCriterionId As Integer, Optional db As SECMonitoringDbContext = Nothing) As AssessmentCriterion
    Function DeleteAssessmentCriterion(AssessmentCriterionId As Integer) As Boolean
    Function UpdateAssessmentCriterion(AssessmentCriterion As AssessmentCriterion) As AssessmentCriterion
    Function DecreaseAssessmentCriterionIndex(AssessmentCriterionId As Integer,
                                              Optional db As SECMonitoringDbContext = Nothing) As Boolean
    Function IncreaseAssessmentCriterionIndex(AssessmentCriterionId As Integer,
                                              Optional db As SECMonitoringDbContext = Nothing) As Boolean

#End Region

#End Region

#Region "Calculation Aggregate Functions"

    Function GetCalculationAggregateFunctions(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of CalculationAggregateFunction)
    Function GetCalculationAggregateFunction(CalculationAggregateFunctionId As Integer, Optional db As SECMonitoringDbContext = Nothing) As CalculationAggregateFunction

#End Region

#Region "Calculation Filters"

    Function GetCalculationFilters(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of CalculationFilter)
    Function GetCalculationFilter(FilterId As Integer, Optional db As SECMonitoringDbContext = Nothing) As CalculationFilter
    Function GetCalculationFilter(FilterName As String) As CalculationFilter
    Function UpdateCalculationFilter(CalculationFilter As CalculationFilter) As CalculationFilter
    Function AddCalculationFilterApplicableDayOfWeek(CalculationFilterId As Integer, ApplicableDayOfWeekId As Integer) As Boolean
    Function RemoveCalculationFilterApplicableDayOfWeek(CalculationFilterId As Integer, ApplicableDayOfWeekId As Integer) As Boolean
    Function AddCalculationFilter(CalculationFilter As CalculationFilter) As CalculationFilter
    Function GetCalculationFiltersForMeasurementType(MeasurementTypeId As Integer) As IEnumerable(Of CalculationFilter)
    Function DeleteCalculationFilter(CalculationFilterId As Integer) As Boolean

#End Region

#Region "Contacts"

    Function GetContacts(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Contact)
    Function GetContact(ContactId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Contact
    Function GetContact(ContactName As String) As Contact
    Function GetContacts(ForProject As Project) As IEnumerable(Of Contact)
    Function AddContact(Contact As Contact) As Contact
    Function UpdateContact(Contact As Contact) As Contact
    Function AddContactProject(ContactId As Integer, ProjectId As Integer) As Boolean
    Function AddContactExcludedDocument(ContactId As Integer, ExcludedDocumentId As Integer) As Boolean
    Function RemoveContactProject(ContactId As Integer, ProjectId As Integer) As Boolean
    Function RemoveContactExcludedDocument(ContactId As Integer, ExcludedDocumentId As Integer) As Boolean

    Function DeleteContact(ContactId As Integer) As Boolean

#End Region

#Region "Countries"

    Function GetCountries(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Country)
    Function GetCountry(CountryName As String) As Country
    Function GetCountry(CountryId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Country
    Function AddCountry(Country As Country) As Country
    Function UpdateCountry(Country As Country) As Country

    Function DeleteCountry(CountryId As Integer) As Boolean

#End Region

#Region "Days of Week"

    Function GetDaysOfWeek(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of DayOfWeek)
    Function GetDayOfWeek(DayOfWeekId As Integer, Optional db As SECMonitoringDbContext = Nothing) As DayOfWeek

#End Region

#Region "Documents"

    Function GetDocuments(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Document)
    Function GetDocuments(ProjectId As Integer, Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Document)
    Function GetDocument(DocumentId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Document
    Function GetDocument(Title As String) As Document
    Function AddDocument(Document As Document) As Document
    Function UpdateDocument(Document As Document) As Document
    Function DeleteDocument(DocumentId As Integer) As Boolean


    Function AddDocumentProject(DocumentId As Integer, ProjectId As Integer) As Boolean
    Function AddDocumentMonitor(DocumentId As Integer, MonitorId As Integer) As Boolean
    Function AddDocumentMonitorLocation(DocumentId As Integer, MonitorLocationId As Integer) As Boolean
    Function AddDocumentExcludedContact(DocumentId As Integer, ExcludedContactId As Integer) As Boolean

    Function RemoveDocumentProject(DocumentId As Integer, ProjectId As Integer) As Boolean
    Function RemoveDocumentMonitor(DocumentId As Integer, MonitorId As Integer) As Boolean
    Function RemoveDocumentMonitorLocation(DocumentId As Integer, MonitorLocationId As Integer) As Boolean
    Function RemoveDocumentExcludedContact(DocumentId As Integer, ExcludedContactId As Integer) As Boolean

#End Region

#Region "Document Types"

    Function GetDocumentTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of DocumentType)
    Function GetDocumentType(DocumentTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As DocumentType
    Function GetDocumentType(DocumentTypeName As String) As DocumentType
    Function GetDocumentTypesSelectList(Include0AsAll As Boolean, Optional db As SECMonitoringDbContext = Nothing) As SelectList
    Function UpdateDocumentType(DocumentType As DocumentType) As DocumentType
    Function AddDocumentType(DocumentType As DocumentType) As DocumentType

    Function DeleteDocumentType(DocumentTypeId As Integer) As Boolean

#End Region

#Region "Measurements"

    Function AddMeasurement(Measurement As Measurement) As Measurement
    Function TryAddMeasurements(Measurements As IEnumerable(Of Measurement)) As Boolean
    Function ReadMeasurements(RMP As IReadMeasurementParameters) As IEnumerable(Of Measurement)

    Function GetMeasurements(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Measurement)
    Function GetMeasurement(MeasurementId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Measurement
    Function DeleteMeasurement(MeasurementId As Integer) As Boolean

    Function GetObjectFirstMeasurementStartDateTime(ObjectName As String, ObjectId As Integer) As Date
    Function GetObjectLastMeasurementStartDateTime(ObjectName As String, ObjectId As Integer) As Date
    Function GetObjectLastMeasurementDuration(ObjectName As String, ObjectId As Integer) As Double
    Function GetObjectLastMeasurementEndDateTime(ObjectName As String, ObjectId As Integer) As Date
    Function GetObjectMeasurementMetricIds(ObjectName As String, ObjectId As Integer) As List(Of Integer)
    Function ObjectHasMeasurements(ObjectName As String, ObjectId As Integer) As Boolean
    Function GetObjectMeasurements(ObjectName As String, ObjectId As Integer, Optional OrderBy As String = "") As List(Of Measurement)
    Function GetObjectMeasurements(ObjectName As String, ObjectId As Integer, OrderBy As String, SortOrder As List(Of Integer)) As List(Of Measurement)
    Function GetMonitorLocationMeasurements(MonitorLocationId As Integer, StartDateTime As Date, EndDateTime As Date) As List(Of Measurement)
    Function GetCommentTypeCommentIds(CommentTypeIds As List(Of Integer)) As List(Of Integer)
    Function GetCommentMeasurementIds(CommentIds As List(Of Integer)) As List(Of Integer)

#End Region

#Region "Measurement Comments"

    Function GetMeasurementComments(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementComment)
    Function GetMeasurementComment(MeasurementCommentId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementComment
    Function AddMeasurementComment(MeasurementComment As MeasurementComment) As MeasurementComment
    Function DeleteMeasurementComment(MeasurementCommentId As Integer) As Boolean
    Function GetMeasurementCommentStartDateTime(MeasurementCommentId As Integer) As Date
    Function GetMeasurementCommentEndDateTime(MeasurementCommentId As Integer) As Date

#End Region

#Region "Measurement Comment Types"

    Function GetMeasurementCommentTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementCommentType)
    Function GetMeasurementCommentType(MeasurementCommentTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementCommentType
    Function GetMeasurementCommentType(CommentTypeName As String) As MeasurementCommentType
    Function AddMeasurementCommentType(MeasurementCommentType As MeasurementCommentType) As MeasurementCommentType
    Function UpdateMeasurementCommentType(MeasurementCommentType As MeasurementCommentType) As MeasurementCommentType
    Function AddMeasurementCommentTypeExcludedMeasurementView(MeasurementCommentTypeId As Integer, ExcludedMeasurementViewId As Integer) As Boolean
    Function RemoveMeasurementCommentTypeExcludedMeasurementView(MeasurementCommentTypeId As Integer, ExcludedMeasurementViewId As Integer) As Boolean
    Function AddMeasurementCommentTypeExcludedAssessmentCriterionGroup(MeasurementCommentTypeId As Integer, ExcludedAssessmentCriterionGroupId As Integer) As Boolean
    Function RemoveMeasurementCommentTypeExcludedAssessmentCriterionGroup(MeasurementCommentTypeId As Integer, ExcludedAssessmentCriterionGroupId As Integer) As Boolean

    Function DeleteMeasurementCommentType(MeasurementCommentTypeId As Integer) As Boolean

#End Region

#Region "Measurement Files"

    Function GetMeasurementFiles(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementFile)
    Function GetMeasurementFile(MeasurementFileId As Integer,
                                Optional db As SECMonitoringDbContext = Nothing) As MeasurementFile
    Function GetMonitorLocationMeasurementFiles(MeasurementFileIds As List(Of Integer),
                                                Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementFile)
    Function GetMeasurementFile(MeasurementFileRouteName As String) As MeasurementFile
    Function AddMeasurementFile(MeasurementFile As MeasurementFile) As MeasurementFile

    Function MeasurementFileCanBeDeleted(MeasurementFileId As Integer) As Boolean
    Function DeleteMeasurementFile(MeasurementFileId As Integer) As Boolean
    Function GetMeasurementFileNumMeasurements(MeasurementFileId As Integer) As Integer

#End Region

#Region "Measurement File Types"

    Function GetMeasurementFileTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementFileType)
    Function GetMeasurementFileTypes(MeasurementTypeId As Integer) As IEnumerable(Of MeasurementFileType)
    Function GetMeasurementFileType(MeasurementFileTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementFileType


#End Region

#Region "Measurement Metrics"

    Function GetMeasurementMetrics(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementMetric)
    Function GetMeasurementMetrics(MeasurementTypeId As Integer) As IEnumerable(Of MeasurementMetric)
    Function GetMeasurementMetrics(MeasurementType As MeasurementType) As IEnumerable(Of MeasurementMetric)
    Function GetMeasurementMetrics(RMP As IReadMeasurementParameters) As IEnumerable(Of MeasurementMetric)

    Function GetMeasurementMetric(MetricId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementMetric
    Function GetMeasurementMetric(MetricName As String) As MeasurementMetric
    Function AddMeasurementMetric(MeasurementMetric As MeasurementMetric) As MeasurementMetric
    Function UpdateMeasurementMetric(MeasurementMetric As MeasurementMetric) As MeasurementMetric

    Function DeleteMeasurementMetric(MeasurementMetricId As Integer) As Boolean
    Function MeasurementMetricHasMeasurements(MeasurementMetricId As Integer) As Boolean

#End Region

#Region "Measurement Types"

    Function GetMeasurementTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementType)
    Function GetMeasurementType(MeasurementTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementType
    Function GetMeasurementType(MeasurementTypeName As String) As MeasurementType
    Function GetMeasurementTypesSelectList(Include0AsAll As Boolean, Optional db As SECMonitoringDbContext = Nothing) As SelectList


#End Region

#Region "Measurement Views"

    Function GetMeasurementViews(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementView)
    Function GetMeasurementView(MeasurementViewId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementView
    Function GetMeasurementView(ViewName As String) As MeasurementView
    Function AddMeasurementView(MeasurementView As MeasurementView) As MeasurementView
    Function UpdateMeasurementView(MeasurementView As MeasurementView) As MeasurementView
    Function AddMeasurementViewUserAccessLevel(MeasurementViewId As Integer, UserAccessLevelId As Integer) As Boolean
    Function AddMeasurementViewCommentType(MeasurementViewId As Integer, CommentTypeId As Integer) As Boolean
    Function RemoveMeasurementViewUserAccessLevel(MeasurementViewId As Integer, UserAccessLevelId As Integer) As Boolean
    Function RemoveMeasurementViewCommentType(MeasurementViewId As Integer, CommentTypeId As Integer) As Boolean

    Function DeleteMeasurementView(MeasurementViewId As Integer) As Boolean

#End Region

#Region "Measurement View Groups"

    Function AddMeasurementViewGroup(MeasurementViewGroup As MeasurementViewGroup) As MeasurementViewGroup
    Function GetMeasurementViewGroups(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementViewGroup)
    Function GetMeasurementViewGroup(MeasurementViewGroupId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementViewGroup
    Function GetMeasurementViewGroup(MeasurementViewName As String, GroupIndex As Integer) As MeasurementViewGroup
    Function UpdateMeasurementViewGroup(MeasurementViewGroup As MeasurementViewGroup) As MeasurementViewGroup
    Function DeleteMeasurementViewGroup(MeasurementViewGroupId As Integer) As Boolean

#End Region

#Region "Measurement View Sequence Settings"

    Function AddMeasurementViewSequenceSetting(MeasurementViewSequenceSetting As MeasurementViewSequenceSetting) As MeasurementViewSequenceSetting
    Function GetMeasurementViewSequenceSettings(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementViewSequenceSetting)
    Function GetMeasurementViewSequenceSetting(MeasurementViewName As String, GroupIndex As Integer, SequenceIndex As Integer) As MeasurementViewSequenceSetting
    Function UpdateMeasurementViewSequenceSetting(MeasurementViewSequenceSetting As MeasurementViewSequenceSetting) As MeasurementViewSequenceSetting
    Function DeleteMeasurementViewSequenceSetting(MeasurementViewSequenceSettingId As Integer) As Boolean

#End Region

#Region "Measurement View Series Types"

    Function GetMeasurementViewSeriesTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementViewSeriesType)
    Function GetMeasurementViewSeriesType(MeasurementViewSeriesTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementViewSeriesType

#End Region

#Region "Series Dash Styles"

    Function GetSeriesDashStyles(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of SeriesDashStyle)
    Function GetSeriesDashStyle(SeriesDashStyleId As Integer, Optional db As SECMonitoringDbContext = Nothing) As SeriesDashStyle

#End Region

#Region "Measurement View Table Types"

    Function GetMeasurementViewTableTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MeasurementViewTableType)
    Function GetMeasurementViewTableType(MeasurementViewTableTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MeasurementViewTableType

#End Region

#Region "Monitors"


    Function GetMonitors(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Monitor)
    Function GetMonitor(MonitorName As String) As Monitor
    Function GetMonitor(MonitorId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Monitor
    Function GetCurrentMonitor(ByVal ProjectRouteName As String, ByVal MonitorLocationName As String) As Monitor
    Function AddMonitor(Monitor As Monitor) As Monitor
    Function UpdateMonitor(Monitor As Monitor) As Monitor
    Function UpdateMonitorCurrentLocation(Monitor As Monitor) As Monitor
    Function DeleteMonitor(MonitorId As Integer) As Boolean
    'Function AddMonitor(MonitorName As String, MonitorTypeID As Integer) As IMonitor
    'Function RenameMonitor(OldMonitorName As String, NewMonitorName As String) As Boolean
    'Function GetMonitors(MonitorTypeID As Integer) As IEnumerable(Of IMonitor)
    'Function GetMonitors(MonitorTypeIDs As IEnumerable(Of Integer)) As IEnumerable(Of IMonitor)
    'Function GetMonitorType(MonitorTypeName As String) As IMeasurementType

#End Region

#Region "Monitor Deployment Records"

    Function GetMonitorDeploymentRecords(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MonitorDeploymentRecord)
    Function GetMonitorDeploymentRecords(Monitor As Monitor) As IEnumerable(Of MonitorDeploymentRecord)
    Function GetMonitorDeploymentRecords(MonitorLocation As MonitorLocation) As IEnumerable(Of MonitorDeploymentRecord)
    Function AddMonitorDeploymentRecord(MonitorDeploymentRecord As MonitorDeploymentRecord) As MonitorDeploymentRecord
    Function UpdateMonitorDeploymentRecord(MonitorDeploymentRecord As MonitorDeploymentRecord) As MonitorDeploymentRecord
    Function GetMonitorDeploymentRecord(MonitorDeploymentRecordId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MonitorDeploymentRecord
    Function DeleteMonitorDeploymentRecord(MonitorDeploymentRecordId As Integer) As Boolean

#End Region

#Region "Monitor Locations"

    Function GetMonitorLocation(MonitorLocationID As Integer, Optional db As SECMonitoringDbContext = Nothing) As MonitorLocation
    Function GetMonitorLocation(ProjectShortName As String, MonitorLocationName As String) As MonitorLocation
    Function GetMonitorLocations(ByVal ProjectRouteName As String) As IEnumerable(Of MonitorLocation)
    Function GetMonitorLocationsForProject(ProjectId As Integer) As IEnumerable(Of MonitorLocation)
    Function GetMonitorLocations(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MonitorLocation)
    Function GetMonitorLocationsSelectList(Include0AsAll As Boolean,
                                           Optional MeasurementTypeId As Integer = 0,
                                           Optional ProjectId As Integer = 0,
                                           Optional db As SECMonitoringDbContext = Nothing) As SelectList
    Function AddMonitorLocation(MonitorLocation As MonitorLocation) As MonitorLocation
    Function UpdateMonitorLocation(MonitorLocation As MonitorLocation) As MonitorLocation
    Function UpdateMonitorLocationCurrentMonitor(MonitorLocation As MonitorLocation) As MonitorLocation
    Function GetMonitorLocationsForAssessmentCriterionGroup(AssessmentCriterionGroupId As Integer) As IEnumerable(Of MonitorLocation)
    Function DeleteMonitorLocation(MonitorLocationId As Integer) As Boolean

#End Region

#Region "Monitor Location GeoCoords"

    Function GetMonitorLocationGeoCoords(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of MonitorLocationGeoCoords)
    Function GetMonitorLocationGeoCoords(MonitorLocationGeoCoordsId As Integer, Optional db As SECMonitoringDbContext = Nothing) As MonitorLocationGeoCoords

#End Region

#Region "Monitor Status"

    Function UpdateMonitorStatus(MonitorStatus As MonitorStatus) As MonitorStatus

#End Region

#Region "Organisations"

    Function GetOrganisations(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Organisation)
    Function GetOrganisation(OrganisationName As String) As Organisation
    Function GetOrganisation(OrganisationId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Organisation
    Function AddOrganisation(Organisation As Organisation) As Organisation
    Function UpdateOrganisation(Organisation As Organisation) As Organisation
    Function DeleteOrganisation(OrganisationId As Integer) As Boolean

#End Region

#Region "Organisation Types"

    Function GetOrganisationTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of OrganisationType)
    Function GetOrganisationType(OrganisationTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As OrganisationType
    Function GetOrganisationType(OrganisationTypeName As String) As OrganisationType
    Function AddOrganisationType(OrganisationType As OrganisationType) As OrganisationType
    Function UpdateOrganisationType(OrganisationType As OrganisationType) As OrganisationType

    Function DeleteOrganisationType(OrganisationTypeId As Integer) As Boolean

#End Region

#Region "Projects"

    Function GetProject(ProjectId As Integer, Optional db As SECMonitoringDbContext = Nothing) As Project
    Function LoadProject(ProjectId As Integer) As Project
    Function GetProject(ProjectShortName As String) As Project
    Function GetProjects(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of Project)
    Function GetProjects(ForContact As Contact) As IEnumerable(Of Project)
    Function GetProjectsSelectList(Include0AsAll As Boolean,
                                   Optional MeasurementTypeId As Integer = 0,
                                   Optional projectIds As List(Of Integer) = Nothing,
                                   Optional db As SECMonitoringDbContext = Nothing) As SelectList
    Function AddProject(Project As Project) As Project
    Function UpdateProject(Project As Project) As Project
    Function UpdateProjectGeoCoords(ProjectGeoCoords As ProjectGeoCoords) As ProjectGeoCoords
    Function UpdateProjectWorkingHours(ProjectId As Integer,
                                       StandardWeeklyWorkingHours As StandardWeeklyWorkingHours) As Project
    Function UpdateProjectWorkingHours(ProjectId As Integer,
                                       StandardWeeklyWorkingHours As StandardWeeklyWorkingHours,
                                       MeasurementViewIds As IEnumerable(Of Integer)) As Project
    Function AddProjectContact(ProjectId As Integer, ContactId As Integer) As Boolean
    Function RemoveProjectContact(ProjectId As Integer, ContactId As Integer) As Boolean
    Function GetProjectContacts(ProjectId As Integer) As IEnumerable(Of Contact)
    Function AddProjectMeasurementView(ProjectId As Integer, MeasurementViewId As Integer) As Boolean
    Function RemoveProjectMeasurementView(ProjectId As Integer, MeasurementViewId As Integer) As Boolean
    Function AddProjectMonitorLocation(ProjectId As Integer, MonitorLocationId As Integer) As Boolean
    Function RemoveProjectMonitorLocation(ProjectId As Integer, MonitorLocationId As Integer) As Boolean
    Function AddProjectOrganisation(ProjectId As Integer, OrganisationId As Integer) As Boolean
    Function RemoveProjectOrganisation(ProjectId As Integer, OrganisationId As Integer) As Boolean
    Function AddProjectAssessmentCriterionGroup(ProjectId As Integer, AssessmentCriterionGroupId As Integer) As Boolean
    Function RemoveProjectAssessmentCriterionGroup(ProjectId As Integer, AssessmentCriterionGroupId As Integer) As Boolean

    Function AddProjectVariedWeeklyWorkingHours(ProjectId As Integer,
                                                VariedWeeklyWorkingHours As VariedWeeklyWorkingHours) As VariedWeeklyWorkingHours

    Function DeleteProject(ProjectId As Integer) As Boolean
    Function ProjectHasMeasurements(ProjectId As Integer) As Boolean


#End Region

#Region "ProjectPermissions"

    Function GetProjectPermissions(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of ProjectPermission)
    Function GetProjectPermission(ProjectPermissionId As Integer, Optional db As SECMonitoringDbContext = Nothing) As ProjectPermission
    Function GetProjectPermission(UserAccessLevel As UserAccessLevel) As ProjectPermission

#End Region

#Region "Public Holidays"

    Function GetPublicHolidays(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of PublicHoliday)
    Function AddPublicHoliday(PublicHoliday As PublicHoliday) As PublicHoliday
    Function DeletePublicHoliday(PublicHolidayId As Integer) As Boolean

#End Region

#Region "Standard Weekly Working Hours"

    Function GetStandardWeeklyWorkingHours(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of StandardWeeklyWorkingHours)
    Function GetStandardWeeklyWorkingHours(StandardWeeklyWorkingHoursId As Integer, Optional db As SECMonitoringDbContext = Nothing) As StandardWeeklyWorkingHours

    Function AddStandardWeeklyWorkingHoursMeasurementView(StandardWeeklyWorkingHoursId As Integer, MeasurementViewId As Integer) As Boolean
    Function RemoveStandardWeeklyWorkingHoursMeasurementView(StandardWeeklyWorkingHoursId As Integer, MeasurementViewId As Integer) As Boolean




#End Region

#Region "SystemMessages"

    Function GetSystemMessages(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of SystemMessage)
    Function GetSystemMessage(systemMessageId As Integer, Optional db As SECMonitoringDbContext = Nothing) As SystemMessage
    Function AddSystemMessage(SystemMessage As SystemMessage) As SystemMessage
    Function UpdateSystemMessage(SystemMessage As SystemMessage) As SystemMessage
    Function DeleteSystemMessage(SystemMessageId As Integer) As Boolean

#End Region

#Region "ThresholdTypes"

    Function GetThresholdTypes(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of ThresholdType)
    Function GetThresholdType(ThresholdTypeId As Integer, Optional db As SECMonitoringDbContext = Nothing) As ThresholdType

#End Region

#Region "Users"

    Function GetUsers(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of User)
    Function GetUser(UserName As String) As User
    Function GetUser(UserId As Integer, Optional db As SECMonitoringDbContext = Nothing) As User
    Function UpdateUser(User As User) As User
    Function AddUser(User As User) As User
    Function DeleteUser(UserId As Integer) As Boolean
    Function ChangeUserPassword(User As User, Password As String) As User

#End Region

#Region "User Access Levels"

    Function GetUserAccessLevels(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of UserAccessLevel)
    Function GetUserAccessLevel(UserAccessLevelId As Integer, Optional db As SECMonitoringDbContext = Nothing) As UserAccessLevel
    Function GetUserAccessLevel(AccessLevelName As String) As UserAccessLevel
    Function AddUserAccessLevel(UserAccessLevel As UserAccessLevel) As UserAccessLevel
    Function DeleteUserAccessLevel(UserAccessLevelId As Integer) As Boolean
    Function UpdateUserAccessLevel(UserAccessLevel As UserAccessLevel) As UserAccessLevel

#End Region

#Region "VariedWeeklyWorkingHours"

    Function AddVariedWeeklyWorkingHoursMeasurementView(VariedWeeklyWorkingHoursId As Integer, MeasurementViewId As Integer) As Boolean
    Function RemoveVariedWeeklyWorkingHoursMeasurementView(VariedWeeklyWorkingHoursId As Integer, MeasurementViewId As Integer) As Boolean

    Function GetVariedWeeklyWorkingHours(Optional db As SECMonitoringDbContext = Nothing) As IEnumerable(Of VariedWeeklyWorkingHours)
    Function GetVariedWeeklyWorkingHours(VariedWeeklyWorkingHoursId As Integer, Optional db As SECMonitoringDbContext = Nothing) As VariedWeeklyWorkingHours

    Function UpdateVariedWeeklyWorkingHours(VariedWeeklyWorkingHours As VariedWeeklyWorkingHours) As VariedWeeklyWorkingHours
    Function UpdateVariedWeeklyWorkingHoursVariedDailyWorkingHours(VariedWeeklyWorkingHoursId As Integer, VariedDailyWorkingHours As IEnumerable(Of VariedDailyWorkingHours)) As VariedWeeklyWorkingHours

    Function DeleteVariedWeeklyWorkingHours(VariedWeeklyWorkingHoursId As Integer) As Boolean

#End Region

    Sub SaveChanges()
    Sub Dispose()

End Interface





