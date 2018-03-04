Imports System
Imports System.Data.Entity
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Linq
Imports System.Data.Entity.ModelConfiguration.Conventions

Partial Public Class SECMonitoringDbContext
    Inherits DbContext

    Public Sub New()
        MyBase.New("name=SECMonitoringDbContext")
    End Sub

    Public Overridable Property AirQualitySettings As DbSet(Of AirQualitySetting)
    Public Overridable Property AssessmentCriteria As DbSet(Of AssessmentCriterion)
    Public Overridable Property AssessmentCriterionGroups As DbSet(Of AssessmentCriterionGroup)
    Public Overridable Property AssessmentPeriodDurationTypes As DbSet(Of AssessmentPeriodDurationType)
    Public Overridable Property CalculationAggregateFunctions As DbSet(Of CalculationAggregateFunction)
    Public Overridable Property CalculationFilters As DbSet(Of CalculationFilter)
    Public Overridable Property Contacts As DbSet(Of Contact)
    Public Overridable Property Countries As DbSet(Of Country)
    Public Overridable Property DaysOfWeek As DbSet(Of DayOfWeek)
    Public Overridable Property Documents As DbSet(Of Document)
    Public Overridable Property DocumentTypes As DbSet(Of DocumentType)
    Public Overridable Property MeasurementComments As DbSet(Of MeasurementComment)
    Public Overridable Property MeasurementCommentTypes As DbSet(Of MeasurementCommentType)
    Public Overridable Property MeasurementFiles As DbSet(Of MeasurementFile)
    Public Overridable Property MeasurementFileTypes As DbSet(Of MeasurementFileType)
    Public Overridable Property MeasurementMetrics As DbSet(Of MeasurementMetric)
    Public Overridable Property Measurements As DbSet(Of Measurement)
    Public Overridable Property MeasurementTypes As DbSet(Of MeasurementType)
    Public Overridable Property MeasurementViewGroups As DbSet(Of MeasurementViewGroup)
    Public Overridable Property MeasurementViews As DbSet(Of MeasurementView)
    Public Overridable Property MeasurementViewSequenceSettings As DbSet(Of MeasurementViewSequenceSetting)
    Public Overridable Property MeasurementViewSeriesTypes As DbSet(Of MeasurementViewSeriesType)
    Public Overridable Property MeasurementViewTableTypes As DbSet(Of MeasurementViewTableType)
    Public Overridable Property MonitorDeploymentRecords As DbSet(Of MonitorDeploymentRecord)
    Public Overridable Property MonitorLocationGeoCoords As DbSet(Of MonitorLocationGeoCoords)
    Public Overridable Property MonitorLocations As DbSet(Of MonitorLocation)
    Public Overridable Property Monitors As DbSet(Of Monitor)
    Public Overridable Property MonitorSettings As DbSet(Of MonitorSettings)
    Public Overridable Property MonitorStatuses As DbSet(Of MonitorStatus)
    Public Overridable Property NoiseSettings As DbSet(Of NoiseSetting)
    Public Overridable Property Organisations As DbSet(Of Organisation)
    Public Overridable Property OrganisationTypes As DbSet(Of OrganisationType)
    Public Overridable Property ProjectGeoCoords As DbSet(Of ProjectGeoCoords)
    Public Overridable Property Projects As DbSet(Of Project)
    Public Overridable Property ProjectPermissions As DbSet(Of ProjectPermission)
    Public Overridable Property PublicHolidays As DbSet(Of PublicHoliday)
    Public Overridable Property SeriesDashStyles As DbSet(Of SeriesDashStyle)
    Public Overridable Property StandardDailyWorkingHours As DbSet(Of StandardDailyWorkingHours)
    Public Overridable Property StandardWeeklyWorkingHours As DbSet(Of StandardWeeklyWorkingHours)
    Public Overridable Property SystemMessages As DbSet(Of SystemMessage)
    Public Overridable Property ThresholdAggregateDurations As DbSet(Of ThresholdAggregateDuration)
    Public Overridable Property ThresholdTypes As DbSet(Of ThresholdType)
    Public Overridable Property UserAccessLevels As DbSet(Of UserAccessLevel)
    Public Overridable Property Users As DbSet(Of User)
    Public Overridable Property VariedDailyWorkingHours As DbSet(Of VariedDailyWorkingHours)
    Public Overridable Property VariedWeeklyWorkingHours As DbSet(Of VariedWeeklyWorkingHours)
    Public Overridable Property VibrationSettings As DbSet(Of VibrationSetting)

    Protected Overrides Sub OnModelCreating(ByVal modelBuilder As DbModelBuilder)

        modelBuilder.Conventions.Remove(Of OneToManyCascadeDeleteConvention)()

        modelBuilder.Entity(Of Monitor)() _
            .HasOptional(Function(e) e.CurrentLocation) _
            .WithOptionalPrincipal(Function(e) e.CurrentMonitor) _
            .WillCascadeOnDelete(False)

        modelBuilder.Entity(Of Organisation)() _
            .HasMany(Function(e) e.ProjectsAsClient) _
            .WithRequired(Function(e) e.ClientOrganisation) _
            .HasForeignKey(Function(e) e.ClientOrganisationId) _
            .WillCascadeOnDelete(False)

        modelBuilder.Entity(Of Organisation)() _
            .HasMany(Function(e) e.Projects) _
            .WithMany(Function(e) e.Organisations)


        'modelBuilder.Entity(Of MeasurementType)() _
        '    .HasMany(Function(e) e.MonitorLocations) _
        '    .WithRequired(Function(e) e.MeasurementType) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of SeriesDashStyle)() _
        '    .HasMany(Function(e) e.AssessmentCriteriaAsAssessedLevelStyle) _
        '    .WithRequired(Function(e) e.AssessedLevelDashStyle) _
        '    .HasForeignKey(Function(e) e.AssessedLevelDashStyleId) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of SeriesDashStyle)() _
        '    .HasMany(Function(e) e.AssessmentCriteriaAsCriterionLevelStyle) _
        '    .WithRequired(Function(e) e.CriterionLevelDashStyle) _
        '    .HasForeignKey(Function(e) e.CriterionLevelDashStyleId) _
        '    .WillCascadeOnDelete(False)


        'modelBuilder.Entity(Of AssessmentCriterionGroup)() _
        '    .HasMany(Function(e) e.AssessmentCriteria) _
        '    .WithRequired(Function(e) e.AssessmentCriterionGroup) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of AssessmentCriterionGroup)() _
        '    .HasMany(Function(e) e.ExcludingMeasurementCommentTypes) _
        '    .WithMany(Function(e) e.ExcludedAssessmentCriterionGroups) _
        '    .Map(Function(m) m.ToTable("MeasurementCommentTypeAssessmentCriterionGroup").MapLeftKey("ExcludedAssessmentCriterionGroups_Id").MapRightKey("ExcludingMeasurementCommentTypes_Id"))

        'modelBuilder.Entity(Of AssessmentPeriodDurationType)() _
        '    .HasMany(Function(e) e.AssessmentCriterionGroups) _
        '    .WithRequired(Function(e) e.AssessmentPeriodDurationType) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of CalculationAggregateFunction)() _
        '    .HasMany(Function(e) e.CalculationFilters) _
        '    .WithRequired(Function(e) e.CalculationAggregateFunction) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of CalculationFilter)() _
        '    .HasMany(Function(e) e.AssessmentCriteria) _
        '    .WithRequired(Function(e) e.CalculationFilter) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of CalculationFilter)() _
        '    .HasMany(Function(e) e.MeasurementViewSequenceSettings) _
        '    .WithRequired(Function(e) e.CalculationFilter) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of CalculationFilter)() _
        '    .HasMany(Function(e) e.ApplicableDaysOfWeek) _
        '    .WithMany(Function(e) e.CalculationFilters) _
        '    .Map(Function(m) m.ToTable("CalculationFilterDayOfWeek").MapLeftKey("CalculationFilters_Id").MapRightKey("ApplicableDaysOfWeek_Id"))

        'modelBuilder.Entity(Of Contact)() _
        '    .HasMany(Function(e) e.MeasurementComments) _
        '    .WithRequired(Function(e) e.Contact) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Contact)() _
        '    .HasMany(Function(e) e.UploadedMeasurements) _
        '    .WithRequired(Function(e) e.UploadedByContact) _
        '    .HasForeignKey(Function(e) e.UploadContactId) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Contact)() _
        '    .HasMany(Function(e) e.MeasurementFiles) _
        '    .WithRequired(Function(e) e.Contact) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Contact)() _
        '    .HasOptional(Function(e) e.User) _
        '    .WithRequired(Function(e) e.Contact) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Contact)() _
        '    .HasMany(Function(e) e.Projects) _
        '    .WithMany(Function(e) e.Contacts) _
        '    .Map(Function(m) m.ToTable("ContactProject").MapLeftKey("Contacts_Id").MapRightKey("Projects_Id"))

        'modelBuilder.Entity(Of Contact)() _
        '    .HasMany(Function(e) e.ExcludedDocuments) _
        '    .WithMany(Function(e) e.ExcludedContacts) _
        '    .Map(Function(m) m.ToTable("DocumentExcludedContact").MapLeftKey("ExcludedContacts_Id").MapRightKey("ExcludedDocuments_Id"))

        'modelBuilder.Entity(Of Country)() _
        '    .HasMany(Function(e) e.Projects) _
        '    .WithRequired(Function(e) e.Country) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Country)() _
        '    .HasMany(Function(e) e.PublicHolidays) _
        '    .WithRequired(Function(e) e.Country) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of DayOfWeek)() _
        '    .HasMany(Function(e) e.DailyWorkingHours) _
        '    .WithRequired(Function(e) e.DayOfWeek) _
        '    .HasForeignKey(Function(e) e.DayOfWeekId) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of DayOfWeek)() _
        '    .HasMany(Function(e) e.VariedDailyWorkingHours) _
        '    .WithRequired(Function(e) e.DayOfWeek) _
        '    .HasForeignKey(Function(e) e.DayOfWeekId) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Document)() _
        '    .HasMany(Function(e) e.ExcludedUserAccessLevels) _
        '    .WithMany(Function(e) e.ExcludedDocuments) _
        '    .Map(Function(m) m.ToTable("DocumentExcludedUserAccessLevel").MapLeftKey("ExcludedDocuments_Id").MapRightKey("DocumentExcludedUserAccessLevel_Document_Id"))

        'modelBuilder.Entity(Of Document)() _
        '    .HasMany(Function(e) e.Monitors) _
        '    .WithMany(Function(e) e.Documents) _
        '    .Map(Function(m) m.ToTable("DocumentMonitor").MapLeftKey("Documents_Id").MapRightKey("Monitors_Id"))

        'modelBuilder.Entity(Of Document)() _
        '    .HasMany(Function(e) e.Projects) _
        '    .WithMany(Function(e) e.Documents) _
        '    .Map(Function(m) m.ToTable("DocumentProject1").MapLeftKey("DocumentProject1_Project_Id").MapRightKey("DocumentProject1_Document_Id"))


        'modelBuilder.Entity(Of Document)() _
        '    .HasMany(Function(e) e.MonitorLocations) _
        '    .WithMany(Function(e) e.Documents) _
        '    .Map(Function(m) m.ToTable("MonitorLocationDocuments").MapLeftKey("Documents_Id").MapRightKey("MonitorLocations_Id"))

        ''modelBuilder.Entity(Of Document)() _
        ''    .HasMany(Function(e) e.VariedWeeklyWorkingHours) _
        ''    .WithMany(Function(e) e.Documents) _
        ''    .Map(Function(m) m.ToTable("VariedWeeklyWorkingHoursDocument").MapLeftKey("VariedWeeklyWorkingHoursDocument_VariedWeeklyWorkingHours_Id").MapRightKey("VariedWeeklyWorkingHoursDocument_Document_Id"))

        'modelBuilder.Entity(Of DocumentType)() _
        '    .HasMany(Function(e) e.Documents) _
        '    .WithRequired(Function(e) e.DocumentType) _
        '    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of DocumentType)() _
        ''    .HasMany(Function(e) e.DocumentTypes1) _
        ''    .WithMany(Function(e) e.DocumentTypes) _
        ''    .Map(Function(m) m.ToTable("DocumentTypeParentsChildlrenAssociation").MapLeftKey("DocumentTypeParentsChildlrenAssociation_DocumentType1_Id").MapRightKey("DocumentTypeParentsChildlrenAssociation_DocumentType_Id"))

        ''modelBuilder.Entity(Of DocumentType)() _
        ''    .HasMany(Function(e) e.UserAccessLevels) _
        ''    .WithMany(Function(e) e.DocumentTypes) _
        ''    .Map(Function(m) m.ToTable("UserAccessLevelDocumentType").MapLeftKey("AccessibleDocumentTypes_Id").MapRightKey("AllowedUserAccessLevels_Id"))

        ''modelBuilder.Entity(Of MeasurementCommentType)() _
        ''    .HasMany(Function(e) e.MeasurementComments) _
        ''    .WithRequired(Function(e) e.MeasurementCommentType) _
        ''    .HasForeignKey(Function(e) e.CommentTypeId) _
        ''    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of MeasurementCommentType)() _
        ''    .HasMany(Function(e) e.MeasurementViews) _
        ''    .WithMany(Function(e) e.MeasurementCommentTypes) _
        ''    .Map(Function(m) m.ToTable("MeasurementCommentTypeExcludedMeasurementViews").MapLeftKey("ExcludingMeasurementCommentTypes_Id").MapRightKey("ExcludedMeasurementViews_Id"))

        'modelBuilder.Entity(Of MeasurementFile)() _
        '    .HasMany(Function(e) e.Measurements) _
        '    .WithRequired(Function(e) e.MeasurementFile) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MeasurementFileType)() _
        '    .HasMany(Function(e) e.MeasurementFiles) _
        '    .WithRequired(Function(e) e.MeasurementFileType) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MeasurementMetric)() _
        '    .HasMany(Function(e) e.CalculationFilters) _
        '    .WithRequired(Function(e) e.MeasurementMetric) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MeasurementMetric)() _
        '    .HasMany(Function(e) e.Measurements) _
        '    .WithRequired(Function(e) e.MeasurementMetric) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Measurement)() _
        '    .HasMany(Function(e) e.MeasurementComments) _
        '    .WithMany(Function(e) e.Measurements) _
        '    .Map(Function(m) m.ToTable("MeasurementMeasurementComment").MapLeftKey("Measurements_Id").MapRightKey("MeasurementComments_Id"))

        'modelBuilder.Entity(Of MeasurementType)() _
        '    .HasMany(Function(e) e.AssessmentCriterionGroups) _
        '    .WithRequired(Function(e) e.MeasurementType) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MeasurementType)() _
        '    .HasMany(Function(e) e.MeasurementFileTypes) _
        '    .WithRequired(Function(e) e.MeasurementType) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MeasurementType)() _
        '    .HasMany(Function(e) e.MeasurementMetrics) _
        '    .WithRequired(Function(e) e.MeasurementType) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MeasurementType)() _
        '    .HasMany(Function(e) e.MeasurementViews) _
        '    .WithRequired(Function(e) e.MeasurementType) _
        '    .WillCascadeOnDelete(False)



        'modelBuilder.Entity(Of MeasurementType)() _
        '    .HasMany(Function(e) e.Monitors) _
        '    .WithRequired(Function(e) e.MeasurementType) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MeasurementViewGroup)() _
        '    .HasMany(Function(e) e.MeasurementViewSequenceSettings) _
        '    .WithRequired(Function(e) e.MeasurementViewGroup) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MeasurementView)() _
        '    .HasMany(Function(e) e.MeasurementViewGroups) _
        '    .WithRequired(Function(e) e.MeasurementView) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MeasurementView)() _
        '    .HasMany(Function(e) e.Projects) _
        '    .WithMany(Function(e) e.MeasurementViews) _
        '    .Map(Function(m) m.ToTable("ProjectMeasurementView").MapLeftKey("MeasurementViews_Id").MapRightKey("Projects_Id"))

        'modelBuilder.Entity(Of MeasurementView)() _
        '    .HasMany(Function(e) e.StandardWeeklyWorkingHours) _
        '    .WithMany(Function(e) e.MeasurementViews) _
        '    .Map(Function(m) m.ToTable("StandardWeeklyWorkingHoursAvailableMeasurementViews").MapLeftKey("AvailableMeasurementViews_Id").MapRightKey("StandardWeeklyWorkingHoursAvailableMeasurementViews_MeasurementView_Id"))

        ''modelBuilder.Entity(Of MeasurementView)() _
        ''    .HasMany(Function(e) e.StandardWeeklyWorkingHours1) _
        ''    .WithMany(Function(e) e.MeasurementViews1) _
        ''    .Map(Function(m) m.ToTable("StandardWeeklyWorkingHoursDefaultMeasurementViews").MapLeftKey("StandardWeeklyWorkingHoursDefaultMeasurementViews_StandardWeeklyWorkingHours_Id").MapRightKey("StandardWeeklyWorkingHoursDefaultMeasurementViews_MeasurementView_Id"))

        ''modelBuilder.Entity(Of MeasurementView)() _
        ''    .HasMany(Function(e) e.UserAccessLevels) _
        ''    .WithMany(Function(e) e.MeasurementViews) _
        ''    .Map(Function(m) m.ToTable("UserAccessLevelAccessibleMeasurementViews").MapLeftKey("AccessibleMeasurementViews_Id").MapRightKey("AllowedUserAccessLevels_Id"))

        ''modelBuilder.Entity(Of MeasurementView)() _
        ''    .HasMany(Function(e) e.VariedWeeklyWorkingHours) _
        ''    .WithMany(Function(e) e.MeasurementViews) _
        ''    .Map(Function(m) m.ToTable("VariedWeeklyWorkingHoursAvailableMeasurementViews").MapLeftKey("AvailableMeasurementViews_Id").MapRightKey("VariedWeeklyWorkingHoursAvailableMeasurementViews_MeasurementView_Id"))

        ''modelBuilder.Entity(Of MeasurementView)() _
        ''    .HasMany(Function(e) e.VariedWeeklyWorkingHours1) _
        ''    .WithMany(Function(e) e.MeasurementViews1) _
        ''    .Map(Function(m) m.ToTable("WorkingHoursVariationsDefaultMeasurementViews").MapLeftKey("VariedWeeklyWorkingHoursDefaultMeasurementViews_VariationOfWeeklyWorkingHours_Id").MapRightKey("VariedWeeklyWorkingHoursDefaultMeasurementViews_MeasurementView_Id"))

        ''modelBuilder.Entity(Of MeasurementViewSeriesType)() _
        ''    .HasMany(Function(e) e.AssessmentCriterias) _
        ''    .WithRequired(Function(e) e.MeasurementViewSeriesType) _
        ''    .HasForeignKey(Function(e) e.AssessedLevelSeriesTypeId) _
        ''    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of MeasurementViewSeriesType)() _
        ''    .HasMany(Function(e) e.MeasurementViewSequenceSettings) _
        ''    .WithRequired(Function(e) e.MeasurementViewSeriesType) _
        ''    .HasForeignKey(Function(e) e.DayViewSeriesTypeId) _
        ''    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of MeasurementViewSeriesType)() _
        ''    .HasMany(Function(e) e.MeasurementViewSequenceSettings1) _
        ''    .WithRequired(Function(e) e.MeasurementViewSeriesType1) _
        ''    .HasForeignKey(Function(e) e.MonthViewSeriesTypeId) _
        ''    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of MeasurementViewSeriesType)() _
        ''    .HasMany(Function(e) e.MeasurementViewSequenceSettings2) _
        ''    .WithRequired(Function(e) e.MeasurementViewSeriesType2) _
        ''    .HasForeignKey(Function(e) e.WeekViewSeriesTypeId) _
        ''    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MeasurementViewTableType)() _
        '    .HasMany(Function(e) e.MeasurementViews) _
        '    .WithRequired(Function(e) e.MeasurementViewTableType) _
        '    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of MonitorDeploymentRecord)() _
        ''    .HasMany(Function(e) e.MonitorSettings) _
        ''    .WithRequired(Function(e) e.MonitorDeploymentRecord) _
        ''    .HasForeignKey(Function(e) e.DeploymentRecord_Id) _
        ''    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MonitorLocationGeoCoords)() _
        '    .HasMany(Function(e) e.MonitorLocations) _
        '    .WithRequired(Function(e) e.MonitorLocationGeoCoord) _
        '    .HasForeignKey(Function(e) e.MonitorLocationGeoCoordsId) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MonitorLocation)() _
        '    .HasMany(Function(e) e.AssessmentCriteria) _
        '    .WithRequired(Function(e) e.MonitorLocation) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MonitorLocation)() _
        '    .HasMany(Function(e) e.MeasurementComments) _
        '    .WithRequired(Function(e) e.MonitorLocation) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MonitorLocation)() _
        '    .HasMany(Function(e) e.MeasurementFiles) _
        '    .WithRequired(Function(e) e.MonitorLocation) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MonitorLocation)() _
        '    .HasMany(Function(e) e.Measurements) _
        '    .WithRequired(Function(e) e.MonitorLocation) _
        '    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of MonitorLocation)() _
        ''    .HasMany(Function(e) e.MonitorDeploymentRecords) _
        ''    .WithRequired(Function(e) e.MonitorLocation) _
        ''    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Monitor)() _
        '    .HasMany(Function(e) e.MeasurementFiles) _
        '    .WithRequired(Function(e) e.Monitor) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Monitor)() _
        '    .HasMany(Function(e) e.Measurements) _
        '    .WithRequired(Function(e) e.Monitor) _
        '    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of Monitor)() _
        ''    .HasMany(Function(e) e.MonitorDeploymentRecords) _
        ''    .WithRequired(Function(e) e.Monitor) _
        ''    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of Monitor)() _
        ''    .HasMany(Function(e) e.MonitorSettings) _
        ''    .WithOptional(Function(e) e.Monitor) _
        ''    .HasForeignKey(Function(e) e.Monitor_Id)

        'modelBuilder.Entity(Of Monitor)() _
        '    .HasOptional(Function(e) e.CurrentStatus) _
        '    .WithOptionalDependent(Function(e) e.Monitor) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of MonitorSettings)() _
        '    .HasOptional(Function(e) e.AirQualitySetting) _
        '    .WithRequired(Function(e) e.MonitorSettings) _
        '    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of MonitorSettings)() _
        ''    .HasMany(Function(e) e.NoiseSettings) _
        ''    .WithRequired(Function(e) e.MonitorSetting) _
        ''    .HasForeignKey(Function(e) e.MonitorSettings_Id) _
        ''    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of MonitorSettings)() _
        ''    .HasMany(Function(e) e.VibrationSettings) _
        ''    .WithRequired(Function(e) e.MonitorSetting) _
        ''    .HasForeignKey(Function(e) e.MonitorSettings_Id) _
        ''    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Organisation)() _
        '    .HasMany(Function(e) e.Contacts) _
        '    .WithRequired(Function(e) e.Organisation) _
        '    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of Organisation)() _
        ''    .HasMany(Function(e) e.Documents) _
        ''    .WithRequired(Function(e) e.Organisation) _
        ''    .HasForeignKey(Function(e) e.AuthorOrganisationId) _
        ''    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of Organisation)() _
        ''    .HasMany(Function(e) e.Monitors) _
        ''    .WithRequired(Function(e) e.Organisation) _
        ''    .HasForeignKey(Function(e) e.OwnerOrganisationId) _
        ''    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of OrganisationType)() _
        '    .HasMany(Function(e) e.Organisations) _
        '    .WithRequired(Function(e) e.OrganisationType) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of ProjectGeoCoords)() _
        '    .HasMany(Function(e) e.Projects) _
        '    .WithRequired(Function(e) e.ProjectGeoCoord) _
        '    .HasForeignKey(Function(e) e.ProjectGeoCoordsId) _
        '    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of Project)() _
        ''    .HasMany(Function(e) e.AssessmentCriterionGroups) _
        ''    .WithRequired(Function(e) e.Project) _
        ''    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Project)() _
        '    .HasMany(Function(e) e.Measurements) _
        '    .WithRequired(Function(e) e.Project) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of Project)() _
        '    .HasMany(Function(e) e.MonitorLocations) _
        '    .WithRequired(Function(e) e.Project) _
        '    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of Project)() _
        ''    .HasMany(Function(e) e.StandardWeeklyWorkingHours) _
        ''    .WithOptional(Function(e) e.Project) _
        ''    .HasForeignKey(Function(e) e.Project_Id)

        'modelBuilder.Entity(Of Project)() _
        '    .HasMany(Function(e) e.VariedWeeklyWorkingHours) _
        '    .WithRequired(Function(e) e.Project) _
        '    .WillCascadeOnDelete(False)



        ''modelBuilder.Entity(Of SeriesDashStyle)() _
        ''    .HasMany(Function(e) e.AssessmentCriterias1) _
        ''    .WithRequired(Function(e) e.SeriesDashStyle1) _
        ''    .HasForeignKey(Function(e) e.CriterionLevelDashStyleId) _
        ''    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of SeriesDashStyle)() _
        ''    .HasMany(Function(e) e.MeasurementViewSequenceSettings) _
        ''    .WithRequired(Function(e) e.SeriesDashStyle) _
        ''    .HasForeignKey(Function(e) e.DayViewDashStyleId) _
        ''    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of SeriesDashStyle)() _
        ''    .HasMany(Function(e) e.MeasurementViewSequenceSettings1) _
        ''    .WithRequired(Function(e) e.SeriesDashStyle1) _
        ''    .HasForeignKey(Function(e) e.MonthViewDashStyleId) _
        ''    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of SeriesDashStyle)() _
        ''    .HasMany(Function(e) e.MeasurementViewSequenceSettings2) _
        ''    .WithRequired(Function(e) e.SeriesDashStyle2) _
        ''    .HasForeignKey(Function(e) e.WeekViewDashStyleId) _
        ''    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of StandardWeeklyWorkingHours)() _
        '    .HasMany(Function(e) e.StandardDailyWorkingHours) _
        '    .WithRequired(Function(e) e.StandardWeeklyWorkingHours) _
        '    .HasForeignKey(Function(e) e.StandardWeeklyWorkingHoursId) _
        '    .WillCascadeOnDelete(False)

        'modelBuilder.Entity(Of ThresholdAggregateDuration)() _
        '    .HasMany(Function(e) e.AssessmentCriterionGroups) _
        '    .WithRequired(Function(e) e.ThresholdAggregateDuration) _
        '    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of ThresholdType)() _
        ''    .HasMany(Function(e) e.AssessmentCriterias) _
        ''    .WithRequired(Function(e) e.ThresholdType) _
        ''    .WillCascadeOnDelete(False)

        ''modelBuilder.Entity(Of UserAccessLevel)() _
        ''    .HasMany(Function(e) e.WebsitePages) _
        ''    .WithMany(Function(e) e.UserAccessLevels) _
        ''    .Map(Function(m) m.ToTable("UserAccessLevelWebsitePage").MapLeftKey("AllowedUserAccessLevels_Id").MapRightKey("AccessibleWebsitePages_Id"))

        'modelBuilder.Entity(Of VariedWeeklyWorkingHours)() _
        '    .HasMany(Function(e) e.VariedDailyWorkingHours) _
        '    .WithRequired(Function(e) e.VariedWeeklyWorkingHours) _
        '    .HasForeignKey(Function(e) e.VariedWeeklyWorkingHoursId) _
        '    .WillCascadeOnDelete(False)
    End Sub
End Class
