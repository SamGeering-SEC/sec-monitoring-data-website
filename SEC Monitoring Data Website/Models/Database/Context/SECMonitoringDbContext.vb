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


    End Sub
End Class
