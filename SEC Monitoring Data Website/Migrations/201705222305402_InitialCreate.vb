Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class InitialCreate
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.AirQualitySettings",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False),
                        .AlarmTriggerLevel = c.String(),
                        .InletHeatingOn = c.Boolean(nullable := False),
                        .NewDailySample = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.MonitorSettings", Function(t) t.Id) _
                .Index(Function(t) t.Id)
            
            CreateTable(
                "dbo.MonitorSettings",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False),
                        .MeasurementPeriod = c.Double(nullable := False),
                        .AdditionalInfo1 = c.String(),
                        .AdditionalInfo2 = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Monitors", Function(t) t.Id) _
                .Index(Function(t) t.Id)
            
            CreateTable(
                "dbo.MonitorDeploymentRecords",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False),
                        .MonitorId = c.Int(nullable := False),
                        .MonitorLocationId = c.Int(nullable := False),
                        .DeploymentStartDate = c.DateTime(nullable := False),
                        .DeploymentEndDate = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Monitors", Function(t) t.MonitorId) _
                .ForeignKey("dbo.MonitorLocations", Function(t) t.MonitorLocationId) _
                .ForeignKey("dbo.MonitorSettings", Function(t) t.Id) _
                .Index(Function(t) t.Id) _
                .Index(Function(t) t.MonitorId) _
                .Index(Function(t) t.MonitorLocationId)
            
            CreateTable(
                "dbo.Monitors",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .MonitorName = c.String(nullable := False),
                        .MeasurementTypeId = c.Int(nullable := False),
                        .SerialNumber = c.String(nullable := False),
                        .Manufacturer = c.String(nullable := False),
                        .Model = c.String(nullable := False),
                        .Category = c.String(nullable := False),
                        .OwnerOrganisationId = c.Int(nullable := False),
                        .RequiresCalibration = c.Boolean(nullable := False),
                        .LastFieldCalibration = c.DateTime(),
                        .LastFullCalibration = c.DateTime(),
                        .NextFullCalibration = c.DateTime()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Organisations", Function(t) t.OwnerOrganisationId) _
                .ForeignKey("dbo.MeasurementTypes", Function(t) t.MeasurementTypeId) _
                .Index(Function(t) t.MeasurementTypeId) _
                .Index(Function(t) t.OwnerOrganisationId)
            
            CreateTable(
                "dbo.MonitorLocations",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .MonitorLocationName = c.String(nullable := False),
                        .MonitorLocationGeoCoordsId = c.Int(nullable := False),
                        .HeightAboveGround = c.Double(nullable := False),
                        .IsAFacadeLocation = c.Boolean(nullable := False),
                        .ProjectId = c.Int(nullable := False),
                        .MeasurementTypeId = c.Int(nullable := False),
                        .CurrentMonitor_Id = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.MeasurementTypes", Function(t) t.MeasurementTypeId) _
                .ForeignKey("dbo.Projects", Function(t) t.ProjectId) _
                .ForeignKey("dbo.MonitorLocationGeoCoords", Function(t) t.MonitorLocationGeoCoordsId) _
                .ForeignKey("dbo.Monitors", Function(t) t.CurrentMonitor_Id) _
                .Index(Function(t) t.MonitorLocationGeoCoordsId) _
                .Index(Function(t) t.ProjectId) _
                .Index(Function(t) t.MeasurementTypeId) _
                .Index(Function(t) t.CurrentMonitor_Id)
            
            CreateTable(
                "dbo.AssessmentCriterions",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .CalculationFilterId = c.Int(nullable := False),
                        .AssessmentCriterionGroupId = c.Int(nullable := False),
                        .ThresholdRangeLower = c.Double(nullable := False),
                        .ThresholdRangeUpper = c.Double(nullable := False),
                        .ThresholdTypeId = c.Int(nullable := False),
                        .RoundingDecimalPlaces = c.Int(nullable := False),
                        .MonitorLocationId = c.Int(nullable := False),
                        .CriterionIndex = c.Int(nullable := False),
                        .PlotAssessedLevel = c.Boolean(nullable := False),
                        .AssessedLevelSeriesName = c.String(),
                        .AssessedLevelDashStyleId = c.Int(nullable := False),
                        .AssessedLevelLineWidth = c.Int(nullable := False),
                        .AssessedLevelLineColour = c.String(),
                        .AssessedLevelSeriesTypeId = c.Int(nullable := False),
                        .AssessedLevelMarkersOn = c.String(),
                        .PlotCriterionLevel = c.Boolean(nullable := False),
                        .CriterionLevelSeriesName = c.String(),
                        .CriterionLevelDashStyleId = c.Int(nullable := False),
                        .CriterionLevelLineWidth = c.Int(nullable := False),
                        .CriterionLevelLineColour = c.String(),
                        .TabulateAssessedLevels = c.Boolean(nullable := False),
                        .MergeAssessedLevels = c.String(),
                        .AssessedLevelHeader1 = c.String(),
                        .AssessedLevelHeader2 = c.String(),
                        .AssessedLevelHeader3 = c.String(),
                        .TabulateCriterionTriggers = c.Boolean(nullable := False),
                        .MergeCriterionTriggers = c.Boolean(nullable := False),
                        .CriterionTriggerHeader1 = c.String(),
                        .CriterionTriggerHeader2 = c.String(),
                        .CriterionTriggerHeader3 = c.String(),
                        .SeriesDashStyle_Id = c.Int(),
                        .SeriesDashStyle_Id1 = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.SeriesDashStyles", Function(t) t.SeriesDashStyle_Id) _
                .ForeignKey("dbo.SeriesDashStyles", Function(t) t.SeriesDashStyle_Id1) _
                .ForeignKey("dbo.SeriesDashStyles", Function(t) t.AssessedLevelDashStyleId) _
                .ForeignKey("dbo.MeasurementViewSeriesTypes", Function(t) t.AssessedLevelSeriesTypeId) _
                .ForeignKey("dbo.AssessmentCriterionGroups", Function(t) t.AssessmentCriterionGroupId) _
                .ForeignKey("dbo.CalculationFilters", Function(t) t.CalculationFilterId) _
                .ForeignKey("dbo.SeriesDashStyles", Function(t) t.CriterionLevelDashStyleId) _
                .ForeignKey("dbo.MonitorLocations", Function(t) t.MonitorLocationId) _
                .ForeignKey("dbo.ThresholdTypes", Function(t) t.ThresholdTypeId) _
                .Index(Function(t) t.CalculationFilterId) _
                .Index(Function(t) t.AssessmentCriterionGroupId) _
                .Index(Function(t) t.ThresholdTypeId) _
                .Index(Function(t) t.MonitorLocationId) _
                .Index(Function(t) t.AssessedLevelDashStyleId) _
                .Index(Function(t) t.AssessedLevelSeriesTypeId) _
                .Index(Function(t) t.CriterionLevelDashStyleId) _
                .Index(Function(t) t.SeriesDashStyle_Id) _
                .Index(Function(t) t.SeriesDashStyle_Id1)
            
            CreateTable(
                "dbo.SeriesDashStyles",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .DashStyleName = c.String(),
                        .DashStyleEnum = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.MeasurementViewSeriesTypes",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .SeriesTypeName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.AssessmentCriterionGroups",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .GroupName = c.String(nullable := False),
                        .ProjectId = c.Int(nullable := False),
                        .MeasurementTypeId = c.Int(nullable := False),
                        .ThresholdAggregateDurationId = c.Int(nullable := False),
                        .AssessmentPeriodDurationTypeId = c.Int(nullable := False),
                        .AssessmentPeriodDurationCount = c.Int(nullable := False),
                        .ShowGraph = c.Boolean(nullable := False),
                        .GraphTitle = c.String(),
                        .GraphYAxisMin = c.Double(nullable := False),
                        .GraphYAxisMax = c.Double(nullable := False),
                        .GraphXAxisLabel = c.String(),
                        .GraphYAxisLabel = c.String(),
                        .GraphYAxisTickInterval = c.Double(nullable := False),
                        .NumDateColumns = c.Int(nullable := False),
                        .DateColumn1Header = c.String(),
                        .DateColumn1Format = c.String(),
                        .DateColumn2Header = c.String(),
                        .DateColumn2Format = c.String(),
                        .MergeHeaderRow1 = c.Boolean(nullable := False),
                        .MergeHeaderRow2 = c.Boolean(nullable := False),
                        .MergeHeaderRow3 = c.Boolean(nullable := False),
                        .ShowIndividualResults = c.Boolean(nullable := False),
                        .SumExceedancesAcrossCriteria = c.Boolean(nullable := False),
                        .SumPeriodExceedances = c.Boolean(nullable := False),
                        .SumDaysWithExceedances = c.Boolean(nullable := False),
                        .SumDailyEvents = c.Boolean(nullable := False),
                        .ShowSumTitles = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.AssessmentPeriodDurationTypes", Function(t) t.AssessmentPeriodDurationTypeId) _
                .ForeignKey("dbo.Projects", Function(t) t.ProjectId) _
                .ForeignKey("dbo.MeasurementTypes", Function(t) t.MeasurementTypeId) _
                .ForeignKey("dbo.ThresholdAggregateDurations", Function(t) t.ThresholdAggregateDurationId) _
                .Index(Function(t) t.ProjectId) _
                .Index(Function(t) t.MeasurementTypeId) _
                .Index(Function(t) t.ThresholdAggregateDurationId) _
                .Index(Function(t) t.AssessmentPeriodDurationTypeId)
            
            CreateTable(
                "dbo.AssessmentPeriodDurationTypes",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .DurationTypeName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.MeasurementCommentTypes",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .CommentTypeName = c.String(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.MeasurementComments",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .CommentText = c.String(),
                        .CommentTypeId = c.Int(nullable := False),
                        .CommentDateTime = c.DateTime(nullable := False),
                        .ContactId = c.Int(nullable := False),
                        .MonitorLocationId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.MeasurementCommentTypes", Function(t) t.CommentTypeId) _
                .ForeignKey("dbo.Contacts", Function(t) t.ContactId) _
                .ForeignKey("dbo.MonitorLocations", Function(t) t.MonitorLocationId) _
                .Index(Function(t) t.CommentTypeId) _
                .Index(Function(t) t.ContactId) _
                .Index(Function(t) t.MonitorLocationId)
            
            CreateTable(
                "dbo.Contacts",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .ContactName = c.String(nullable := False),
                        .SecureLoginId = c.Int(),
                        .EmailAddress = c.String(nullable := False),
                        .PrimaryTelephoneNumber = c.String(nullable := False),
                        .SecondaryTelephoneNumber = c.String(),
                        .OrganisationId = c.Int(nullable := False),
                        .UserAccessLevelId = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Organisations", Function(t) t.OrganisationId) _
                .ForeignKey("dbo.UserAccessLevels", Function(t) t.UserAccessLevelId) _
                .Index(Function(t) t.OrganisationId) _
                .Index(Function(t) t.UserAccessLevelId)
            
            CreateTable(
                "dbo.Documents",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Title = c.String(nullable := False),
                        .HasDateRange = c.Boolean(nullable := False),
                        .StartDateTime = c.DateTime(nullable := False),
                        .EndDateTime = c.DateTime(nullable := False),
                        .FilePath = c.String(),
                        .DocumentTypeId = c.Int(nullable := False),
                        .AuthorOrganisationId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Organisations", Function(t) t.AuthorOrganisationId) _
                .ForeignKey("dbo.DocumentTypes", Function(t) t.DocumentTypeId) _
                .Index(Function(t) t.DocumentTypeId) _
                .Index(Function(t) t.AuthorOrganisationId)
            
            CreateTable(
                "dbo.Organisations",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .FullName = c.String(nullable := False),
                        .ShortName = c.String(nullable := False),
                        .Address = c.String(nullable := False),
                        .OrganisationTypeId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.OrganisationTypes", Function(t) t.OrganisationTypeId) _
                .Index(Function(t) t.OrganisationTypeId)
            
            CreateTable(
                "dbo.OrganisationTypes",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .OrganisationTypeName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Projects",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False),
                        .FullName = c.String(nullable := False),
                        .ShortName = c.String(nullable := False),
                        .ProjectNumber = c.String(nullable := False),
                        .ProjectGeoCoordsId = c.Int(nullable := False),
                        .MapLink = c.String(),
                        .ClientOrganisationId = c.Int(nullable := False),
                        .CountryId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Countries", Function(t) t.CountryId) _
                .ForeignKey("dbo.ProjectGeoCoords", Function(t) t.ProjectGeoCoordsId) _
                .ForeignKey("dbo.StandardWeeklyWorkingHours", Function(t) t.Id) _
                .ForeignKey("dbo.Organisations", Function(t) t.ClientOrganisationId) _
                .Index(Function(t) t.Id) _
                .Index(Function(t) t.ProjectGeoCoordsId) _
                .Index(Function(t) t.ClientOrganisationId) _
                .Index(Function(t) t.CountryId)
            
            CreateTable(
                "dbo.Countries",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .CountryName = c.String(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.PublicHolidays",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .HolidayDate = c.DateTime(nullable := False),
                        .HolidayName = c.String(nullable := False),
                        .CountryId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Countries", Function(t) t.CountryId) _
                .Index(Function(t) t.CountryId)
            
            CreateTable(
                "dbo.Measurements",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .StartDateTime = c.DateTime(nullable := False),
                        .Duration = c.Double(nullable := False),
                        .Level = c.Double(nullable := False),
                        .Overload = c.Boolean(),
                        .Underload = c.Boolean(),
                        .MeasurementMetricId = c.Int(nullable := False),
                        .MonitorId = c.Int(nullable := False),
                        .MonitorLocationId = c.Int(nullable := False),
                        .ProjectId = c.Int(nullable := False),
                        .DateTimeUploaded = c.DateTime(nullable := False),
                        .UploadContactId = c.Int(nullable := False),
                        .MeasurementFileId = c.Int(nullable := False),
                        .UploadedByContact_Id = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.MeasurementMetrics", Function(t) t.MeasurementMetricId) _
                .ForeignKey("dbo.MeasurementFiles", Function(t) t.MeasurementFileId) _
                .ForeignKey("dbo.Monitors", Function(t) t.MonitorId) _
                .ForeignKey("dbo.MonitorLocations", Function(t) t.MonitorLocationId) _
                .ForeignKey("dbo.Projects", Function(t) t.ProjectId) _
                .ForeignKey("dbo.Contacts", Function(t) t.UploadedByContact_Id) _
                .Index(Function(t) t.MeasurementMetricId) _
                .Index(Function(t) t.MonitorId) _
                .Index(Function(t) t.MonitorLocationId) _
                .Index(Function(t) t.ProjectId) _
                .Index(Function(t) t.MeasurementFileId) _
                .Index(Function(t) t.UploadedByContact_Id)
            
            CreateTable(
                "dbo.MeasurementFiles",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .MeasurementFileName = c.String(),
                        .UploadDateTime = c.DateTime(nullable := False),
                        .UploadSuccess = c.Boolean(nullable := False),
                        .MonitorId = c.Int(nullable := False),
                        .MonitorLocationId = c.Int(nullable := False),
                        .ContactId = c.Int(nullable := False),
                        .MeasurementFileTypeId = c.Int(nullable := False),
                        .ServerFileName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Contacts", Function(t) t.ContactId) _
                .ForeignKey("dbo.MeasurementFileTypes", Function(t) t.MeasurementFileTypeId) _
                .ForeignKey("dbo.Monitors", Function(t) t.MonitorId) _
                .ForeignKey("dbo.MonitorLocations", Function(t) t.MonitorLocationId) _
                .Index(Function(t) t.MonitorId) _
                .Index(Function(t) t.MonitorLocationId) _
                .Index(Function(t) t.ContactId) _
                .Index(Function(t) t.MeasurementFileTypeId)
            
            CreateTable(
                "dbo.MeasurementFileTypes",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .FileTypeName = c.String(),
                        .MeasurementTypeId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.MeasurementTypes", Function(t) t.MeasurementTypeId) _
                .Index(Function(t) t.MeasurementTypeId)
            
            CreateTable(
                "dbo.MeasurementTypes",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .MeasurementTypeName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.MeasurementMetrics",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .MetricName = c.String(nullable := False),
                        .DisplayName = c.String(nullable := False),
                        .MeasurementTypeId = c.Int(nullable := False),
                        .FundamentalUnit = c.String(nullable := False),
                        .RoundingDecimalPlaces = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.MeasurementTypes", Function(t) t.MeasurementTypeId) _
                .Index(Function(t) t.MeasurementTypeId)
            
            CreateTable(
                "dbo.CalculationFilters",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .FilterName = c.String(nullable := False),
                        .MeasurementMetricId = c.Int(nullable := False),
                        .InputMultiplier = c.String(nullable := False),
                        .CalculationAggregateFunctionId = c.Int(nullable := False),
                        .TimeBase = c.Double(nullable := False),
                        .UseTimeWindow = c.Boolean(nullable := False),
                        .TimeWindowStartTime = c.DateTime(nullable := False),
                        .TimeWindowEndTime = c.DateTime(nullable := False),
                        .TimeStep = c.Double(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.CalculationAggregateFunctions", Function(t) t.CalculationAggregateFunctionId) _
                .ForeignKey("dbo.MeasurementMetrics", Function(t) t.MeasurementMetricId) _
                .Index(Function(t) t.MeasurementMetricId) _
                .Index(Function(t) t.CalculationAggregateFunctionId)
            
            CreateTable(
                "dbo.DaysOfWeek",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .DayName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.StandardDailyWorkingHours",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .DayOfWeekId = c.Int(nullable := False),
                        .StartTime = c.DateTime(nullable := False),
                        .EndTime = c.DateTime(nullable := False),
                        .StandardWeeklyWorkingHoursId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.DaysOfWeek", Function(t) t.DayOfWeekId) _
                .ForeignKey("dbo.StandardWeeklyWorkingHours", Function(t) t.StandardWeeklyWorkingHoursId) _
                .Index(Function(t) t.DayOfWeekId) _
                .Index(Function(t) t.StandardWeeklyWorkingHoursId)
            
            CreateTable(
                "dbo.StandardWeeklyWorkingHours",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.MeasurementViews",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .ViewName = c.String(nullable := False),
                        .DisplayName = c.String(nullable := False),
                        .TableResultsHeader = c.String(nullable := False),
                        .MeasurementTypeId = c.Int(nullable := False),
                        .MeasurementViewTableTypeId = c.Int(nullable := False),
                        .YAxisMinValue = c.Double(nullable := False),
                        .YAxisMaxValue = c.Double(nullable := False),
                        .YAxisStepValue = c.Double(nullable := False),
                        .StandardWeeklyWorkingHours_Id = c.Int(),
                        .VariedWeeklyWorkingHours_Id = c.Int()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.MeasurementTypes", Function(t) t.MeasurementTypeId) _
                .ForeignKey("dbo.MeasurementViewTableTypes", Function(t) t.MeasurementViewTableTypeId) _
                .ForeignKey("dbo.StandardWeeklyWorkingHours", Function(t) t.StandardWeeklyWorkingHours_Id) _
                .ForeignKey("dbo.VariedWeeklyWorkingHours", Function(t) t.VariedWeeklyWorkingHours_Id) _
                .Index(Function(t) t.MeasurementTypeId) _
                .Index(Function(t) t.MeasurementViewTableTypeId) _
                .Index(Function(t) t.StandardWeeklyWorkingHours_Id) _
                .Index(Function(t) t.VariedWeeklyWorkingHours_Id)
            
            CreateTable(
                "dbo.MeasurementViewGroups",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .GroupIndex = c.Int(nullable := False),
                        .MainHeader = c.String(nullable := False),
                        .SubHeader = c.String(nullable := False),
                        .MeasurementViewId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.MeasurementViews", Function(t) t.MeasurementViewId) _
                .Index(Function(t) t.MeasurementViewId)
            
            CreateTable(
                "dbo.MeasurementViewSequenceSettings",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .SequenceIndex = c.Int(nullable := False),
                        .TableHeader = c.String(nullable := False),
                        .SeriesName = c.String(nullable := False),
                        .DayViewSeriesTypeId = c.Int(nullable := False),
                        .WeekViewSeriesTypeId = c.Int(nullable := False),
                        .MonthViewSeriesTypeId = c.Int(nullable := False),
                        .SeriesColour = c.String(nullable := False),
                        .MeasurementViewGroupId = c.Int(nullable := False),
                        .CalculationFilterId = c.Int(nullable := False),
                        .DayViewDashStyleId = c.Int(nullable := False),
                        .WeekViewDashStyleId = c.Int(nullable := False),
                        .MonthViewDashStyleId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.CalculationFilters", Function(t) t.CalculationFilterId) _
                .ForeignKey("dbo.SeriesDashStyles", Function(t) t.DayViewDashStyleId) _
                .ForeignKey("dbo.MeasurementViewSeriesTypes", Function(t) t.DayViewSeriesTypeId) _
                .ForeignKey("dbo.MeasurementViewGroups", Function(t) t.MeasurementViewGroupId) _
                .ForeignKey("dbo.SeriesDashStyles", Function(t) t.MonthViewDashStyleId) _
                .ForeignKey("dbo.MeasurementViewSeriesTypes", Function(t) t.MonthViewSeriesTypeId) _
                .ForeignKey("dbo.SeriesDashStyles", Function(t) t.WeekViewDashStyleId) _
                .ForeignKey("dbo.MeasurementViewSeriesTypes", Function(t) t.WeekViewSeriesTypeId) _
                .Index(Function(t) t.DayViewSeriesTypeId) _
                .Index(Function(t) t.WeekViewSeriesTypeId) _
                .Index(Function(t) t.MonthViewSeriesTypeId) _
                .Index(Function(t) t.MeasurementViewGroupId) _
                .Index(Function(t) t.CalculationFilterId) _
                .Index(Function(t) t.DayViewDashStyleId) _
                .Index(Function(t) t.WeekViewDashStyleId) _
                .Index(Function(t) t.MonthViewDashStyleId)
            
            CreateTable(
                "dbo.MeasurementViewTableTypes",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .TableTypeName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.VariedDailyWorkingHours",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .DayOfWeekId = c.Int(nullable := False),
                        .StartTime = c.DateTime(nullable := False),
                        .EndTime = c.DateTime(nullable := False),
                        .VariedWeeklyWorkingHoursId = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.DaysOfWeek", Function(t) t.DayOfWeekId) _
                .ForeignKey("dbo.VariedWeeklyWorkingHours", Function(t) t.VariedWeeklyWorkingHoursId) _
                .Index(Function(t) t.DayOfWeekId) _
                .Index(Function(t) t.VariedWeeklyWorkingHoursId)
            
            CreateTable(
                "dbo.VariedWeeklyWorkingHours",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .ProjectId = c.Int(nullable := False),
                        .StartDate = c.DateTime(nullable := False),
                        .EndDate = c.DateTime(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Projects", Function(t) t.ProjectId) _
                .Index(Function(t) t.ProjectId)
            
            CreateTable(
                "dbo.CalculationAggregateFunctions",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .FunctionName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.ProjectGeoCoords",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Latitude = c.Double(nullable := False),
                        .Longitude = c.Double(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.DocumentTypes",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .DocumentTypeName = c.String(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.Users",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False),
                        .UserName = c.String(),
                        .Password = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Contacts", Function(t) t.Id) _
                .Index(Function(t) t.Id)
            
            CreateTable(
                "dbo.UserAccessLevels",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .AccessLevelName = c.String(nullable := False),
                        .CanAddContacts = c.Boolean(nullable := False),
                        .CanEditContacts = c.Boolean(nullable := False),
                        .CanDeleteContacts = c.Boolean(nullable := False),
                        .CanAddOrganisations = c.Boolean(nullable := False),
                        .CanEditOrganisations = c.Boolean(nullable := False),
                        .CanDeleteOrganisations = c.Boolean(nullable := False),
                        .CanAddOrganisationTypes = c.Boolean(nullable := False),
                        .CanEditOrganisationTypes = c.Boolean(nullable := False),
                        .CanDeleteOrganisationTypes = c.Boolean(nullable := False),
                        .CanAddProjects = c.Boolean(nullable := False),
                        .CanEditProjects = c.Boolean(nullable := False),
                        .CanDeleteProjects = c.Boolean(nullable := False),
                        .CanAddDocuments = c.Boolean(nullable := False),
                        .CanEditDocuments = c.Boolean(nullable := False),
                        .CanDeleteDocuments = c.Boolean(nullable := False),
                        .CanAddDocumentTypes = c.Boolean(nullable := False),
                        .CanEditDocumentTypes = c.Boolean(nullable := False),
                        .CanDeleteDocumentTypes = c.Boolean(nullable := False),
                        .CanAddMonitors = c.Boolean(nullable := False),
                        .CanEditMonitors = c.Boolean(nullable := False),
                        .CanDeleteMonitors = c.Boolean(nullable := False),
                        .CanAddMonitorLocations = c.Boolean(nullable := False),
                        .CanEditMonitorLocations = c.Boolean(nullable := False),
                        .CanDeleteMonitorLocations = c.Boolean(nullable := False),
                        .CanAddMonitorLocationRecords = c.Boolean(nullable := False),
                        .CanEditMonitorLocationRecords = c.Boolean(nullable := False),
                        .CanDeleteMonitorLocationRecords = c.Boolean(nullable := False),
                        .CanAddMeasurements = c.Boolean(nullable := False),
                        .CanEditMeasurements = c.Boolean(nullable := False),
                        .CanDeleteMeasurements = c.Boolean(nullable := False),
                        .CanAddCalculationFilters = c.Boolean(nullable := False),
                        .CanEditCalculationFilters = c.Boolean(nullable := False),
                        .CanDeleteCalculationFilters = c.Boolean(nullable := False),
                        .CanAddUserAccessLevels = c.Boolean(nullable := False),
                        .CanEditUserAccessLevels = c.Boolean(nullable := False),
                        .CanDeleteUserAccessLevels = c.Boolean(nullable := False),
                        .CanAddPublicHolidays = c.Boolean(nullable := False),
                        .CanEditPublicHolidays = c.Boolean(nullable := False),
                        .CanDeletePublicHolidays = c.Boolean(nullable := False),
                        .CanAddCountries = c.Boolean(nullable := False),
                        .CanEditCountries = c.Boolean(nullable := False),
                        .CanDeleteCountries = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.ThresholdAggregateDurations",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .AggregateDurationName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.ThresholdTypes",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .ThresholdTypeName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.MonitorLocationGeoCoords",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .Latitude = c.Double(nullable := False),
                        .Longitude = c.Double(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            CreateTable(
                "dbo.MonitorStatus",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False),
                        .IsOnline = c.Boolean(nullable := False),
                        .PowerStatusOk = c.Boolean(nullable := False),
                        .StatusComment = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.Monitors", Function(t) t.Id) _
                .Index(Function(t) t.Id)
            
            CreateTable(
                "dbo.NoiseSettings",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False),
                        .MicrophoneSerialNumber = c.String(),
                        .DynamicRangeLowerLevel = c.Double(nullable := False),
                        .DynamicRangeUpperLevel = c.Double(nullable := False),
                        .WindScreenCorrection = c.Double(nullable := False),
                        .AlarmTriggerLevel = c.String(),
                        .FrequencyWeighting = c.String(),
                        .TimeWeighting = c.String(),
                        .SoundRecording = c.Boolean(nullable := False)
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.MonitorSettings", Function(t) t.Id) _
                .Index(Function(t) t.Id)
            
            CreateTable(
                "dbo.VibrationSettings",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False),
                        .AlarmTriggerLevel = c.String(),
                        .XChannelWeighting = c.String(),
                        .YChannelWeighting = c.String(),
                        .ZChannelWeighting = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id) _
                .ForeignKey("dbo.MonitorSettings", Function(t) t.Id) _
                .Index(Function(t) t.Id)
            
            CreateTable(
                "dbo.ProjectContacts",
                Function(c) New With
                    {
                        .Project_Id = c.Int(nullable := False),
                        .Contact_Id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.Project_Id, t.Contact_Id }) _
                .ForeignKey("dbo.Projects", Function(t) t.Project_Id, cascadeDelete := True) _
                .ForeignKey("dbo.Contacts", Function(t) t.Contact_Id, cascadeDelete := True) _
                .Index(Function(t) t.Project_Id) _
                .Index(Function(t) t.Contact_Id)
            
            CreateTable(
                "dbo.ProjectDocuments",
                Function(c) New With
                    {
                        .Project_Id = c.Int(nullable := False),
                        .Document_Id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.Project_Id, t.Document_Id }) _
                .ForeignKey("dbo.Projects", Function(t) t.Project_Id, cascadeDelete := True) _
                .ForeignKey("dbo.Documents", Function(t) t.Document_Id, cascadeDelete := True) _
                .Index(Function(t) t.Project_Id) _
                .Index(Function(t) t.Document_Id)
            
            CreateTable(
                "dbo.MeasurementMeasurementComments",
                Function(c) New With
                    {
                        .Measurement_Id = c.Int(nullable := False),
                        .MeasurementComment_Id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.Measurement_Id, t.MeasurementComment_Id }) _
                .ForeignKey("dbo.Measurements", Function(t) t.Measurement_Id, cascadeDelete := True) _
                .ForeignKey("dbo.MeasurementComments", Function(t) t.MeasurementComment_Id, cascadeDelete := True) _
                .Index(Function(t) t.Measurement_Id) _
                .Index(Function(t) t.MeasurementComment_Id)
            
            CreateTable(
                "dbo.DayOfWeekCalculationFilters",
                Function(c) New With
                    {
                        .DayOfWeek_Id = c.Int(nullable := False),
                        .CalculationFilter_Id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.DayOfWeek_Id, t.CalculationFilter_Id }) _
                .ForeignKey("dbo.DaysOfWeek", Function(t) t.DayOfWeek_Id, cascadeDelete := True) _
                .ForeignKey("dbo.CalculationFilters", Function(t) t.CalculationFilter_Id, cascadeDelete := True) _
                .Index(Function(t) t.DayOfWeek_Id) _
                .Index(Function(t) t.CalculationFilter_Id)
            
            CreateTable(
                "dbo.MeasurementViewMeasurementCommentTypes",
                Function(c) New With
                    {
                        .MeasurementView_Id = c.Int(nullable := False),
                        .MeasurementCommentType_Id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.MeasurementView_Id, t.MeasurementCommentType_Id }) _
                .ForeignKey("dbo.MeasurementViews", Function(t) t.MeasurementView_Id, cascadeDelete := True) _
                .ForeignKey("dbo.MeasurementCommentTypes", Function(t) t.MeasurementCommentType_Id, cascadeDelete := True) _
                .Index(Function(t) t.MeasurementView_Id) _
                .Index(Function(t) t.MeasurementCommentType_Id)
            
            CreateTable(
                "dbo.MeasurementViewProjects",
                Function(c) New With
                    {
                        .MeasurementView_Id = c.Int(nullable := False),
                        .Project_Id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.MeasurementView_Id, t.Project_Id }) _
                .ForeignKey("dbo.MeasurementViews", Function(t) t.MeasurementView_Id, cascadeDelete := True) _
                .ForeignKey("dbo.Projects", Function(t) t.Project_Id, cascadeDelete := True) _
                .Index(Function(t) t.MeasurementView_Id) _
                .Index(Function(t) t.Project_Id)
            
            CreateTable(
                "dbo.OrganisationProjects",
                Function(c) New With
                    {
                        .Organisation_Id = c.Int(nullable := False),
                        .Project_Id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.Organisation_Id, t.Project_Id }) _
                .ForeignKey("dbo.Organisations", Function(t) t.Organisation_Id, cascadeDelete := True) _
                .ForeignKey("dbo.Projects", Function(t) t.Project_Id, cascadeDelete := True) _
                .Index(Function(t) t.Organisation_Id) _
                .Index(Function(t) t.Project_Id)
            
            CreateTable(
                "dbo.DocumentContacts",
                Function(c) New With
                    {
                        .Document_Id = c.Int(nullable := False),
                        .Contact_Id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.Document_Id, t.Contact_Id }) _
                .ForeignKey("dbo.Documents", Function(t) t.Document_Id, cascadeDelete := True) _
                .ForeignKey("dbo.Contacts", Function(t) t.Contact_Id, cascadeDelete := True) _
                .Index(Function(t) t.Document_Id) _
                .Index(Function(t) t.Contact_Id)
            
            CreateTable(
                "dbo.DocumentMonitorLocations",
                Function(c) New With
                    {
                        .Document_Id = c.Int(nullable := False),
                        .MonitorLocation_Id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.Document_Id, t.MonitorLocation_Id }) _
                .ForeignKey("dbo.Documents", Function(t) t.Document_Id, cascadeDelete := True) _
                .ForeignKey("dbo.MonitorLocations", Function(t) t.MonitorLocation_Id, cascadeDelete := True) _
                .Index(Function(t) t.Document_Id) _
                .Index(Function(t) t.MonitorLocation_Id)
            
            CreateTable(
                "dbo.DocumentMonitors",
                Function(c) New With
                    {
                        .Document_Id = c.Int(nullable := False),
                        .Monitor_Id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.Document_Id, t.Monitor_Id }) _
                .ForeignKey("dbo.Documents", Function(t) t.Document_Id, cascadeDelete := True) _
                .ForeignKey("dbo.Monitors", Function(t) t.Monitor_Id, cascadeDelete := True) _
                .Index(Function(t) t.Document_Id) _
                .Index(Function(t) t.Monitor_Id)
            
            CreateTable(
                "dbo.MeasurementCommentTypeAssessmentCriterionGroups",
                Function(c) New With
                    {
                        .MeasurementCommentType_Id = c.Int(nullable := False),
                        .AssessmentCriterionGroup_Id = c.Int(nullable := False)
                    }) _
                .PrimaryKey(Function(t) New With { t.MeasurementCommentType_Id, t.AssessmentCriterionGroup_Id }) _
                .ForeignKey("dbo.MeasurementCommentTypes", Function(t) t.MeasurementCommentType_Id, cascadeDelete := True) _
                .ForeignKey("dbo.AssessmentCriterionGroups", Function(t) t.AssessmentCriterionGroup_Id, cascadeDelete := True) _
                .Index(Function(t) t.MeasurementCommentType_Id) _
                .Index(Function(t) t.AssessmentCriterionGroup_Id)
            
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.AirQualitySettings", "Id", "dbo.MonitorSettings")
            DropForeignKey("dbo.VibrationSettings", "Id", "dbo.MonitorSettings")
            DropForeignKey("dbo.NoiseSettings", "Id", "dbo.MonitorSettings")
            DropForeignKey("dbo.MonitorSettings", "Id", "dbo.Monitors")
            DropForeignKey("dbo.MonitorDeploymentRecords", "Id", "dbo.MonitorSettings")
            DropForeignKey("dbo.MonitorDeploymentRecords", "MonitorLocationId", "dbo.MonitorLocations")
            DropForeignKey("dbo.MonitorDeploymentRecords", "MonitorId", "dbo.Monitors")
            DropForeignKey("dbo.Monitors", "MeasurementTypeId", "dbo.MeasurementTypes")
            DropForeignKey("dbo.MonitorStatus", "Id", "dbo.Monitors")
            DropForeignKey("dbo.MonitorLocations", "CurrentMonitor_Id", "dbo.Monitors")
            DropForeignKey("dbo.MonitorLocations", "MonitorLocationGeoCoordsId", "dbo.MonitorLocationGeoCoords")
            DropForeignKey("dbo.AssessmentCriterions", "ThresholdTypeId", "dbo.ThresholdTypes")
            DropForeignKey("dbo.AssessmentCriterions", "MonitorLocationId", "dbo.MonitorLocations")
            DropForeignKey("dbo.AssessmentCriterions", "CriterionLevelDashStyleId", "dbo.SeriesDashStyles")
            DropForeignKey("dbo.AssessmentCriterionGroups", "ThresholdAggregateDurationId", "dbo.ThresholdAggregateDurations")
            DropForeignKey("dbo.MeasurementCommentTypeAssessmentCriterionGroups", "AssessmentCriterionGroup_Id", "dbo.AssessmentCriterionGroups")
            DropForeignKey("dbo.MeasurementCommentTypeAssessmentCriterionGroups", "MeasurementCommentType_Id", "dbo.MeasurementCommentTypes")
            DropForeignKey("dbo.MeasurementComments", "MonitorLocationId", "dbo.MonitorLocations")
            DropForeignKey("dbo.Contacts", "UserAccessLevelId", "dbo.UserAccessLevels")
            DropForeignKey("dbo.Users", "Id", "dbo.Contacts")
            DropForeignKey("dbo.Contacts", "OrganisationId", "dbo.Organisations")
            DropForeignKey("dbo.MeasurementComments", "ContactId", "dbo.Contacts")
            DropForeignKey("dbo.DocumentMonitors", "Monitor_Id", "dbo.Monitors")
            DropForeignKey("dbo.DocumentMonitors", "Document_Id", "dbo.Documents")
            DropForeignKey("dbo.DocumentMonitorLocations", "MonitorLocation_Id", "dbo.MonitorLocations")
            DropForeignKey("dbo.DocumentMonitorLocations", "Document_Id", "dbo.Documents")
            DropForeignKey("dbo.DocumentContacts", "Contact_Id", "dbo.Contacts")
            DropForeignKey("dbo.DocumentContacts", "Document_Id", "dbo.Documents")
            DropForeignKey("dbo.Documents", "DocumentTypeId", "dbo.DocumentTypes")
            DropForeignKey("dbo.Documents", "AuthorOrganisationId", "dbo.Organisations")
            DropForeignKey("dbo.Projects", "ClientOrganisationId", "dbo.Organisations")
            DropForeignKey("dbo.OrganisationProjects", "Project_Id", "dbo.Projects")
            DropForeignKey("dbo.OrganisationProjects", "Organisation_Id", "dbo.Organisations")
            DropForeignKey("dbo.Projects", "Id", "dbo.StandardWeeklyWorkingHours")
            DropForeignKey("dbo.Projects", "ProjectGeoCoordsId", "dbo.ProjectGeoCoords")
            DropForeignKey("dbo.MonitorLocations", "ProjectId", "dbo.Projects")
            DropForeignKey("dbo.Measurements", "UploadedByContact_Id", "dbo.Contacts")
            DropForeignKey("dbo.Measurements", "ProjectId", "dbo.Projects")
            DropForeignKey("dbo.Measurements", "MonitorLocationId", "dbo.MonitorLocations")
            DropForeignKey("dbo.Measurements", "MonitorId", "dbo.Monitors")
            DropForeignKey("dbo.MeasurementFiles", "MonitorLocationId", "dbo.MonitorLocations")
            DropForeignKey("dbo.MeasurementFiles", "MonitorId", "dbo.Monitors")
            DropForeignKey("dbo.Measurements", "MeasurementFileId", "dbo.MeasurementFiles")
            DropForeignKey("dbo.MonitorLocations", "MeasurementTypeId", "dbo.MeasurementTypes")
            DropForeignKey("dbo.MeasurementMetrics", "MeasurementTypeId", "dbo.MeasurementTypes")
            DropForeignKey("dbo.Measurements", "MeasurementMetricId", "dbo.MeasurementMetrics")
            DropForeignKey("dbo.CalculationFilters", "MeasurementMetricId", "dbo.MeasurementMetrics")
            DropForeignKey("dbo.CalculationFilters", "CalculationAggregateFunctionId", "dbo.CalculationAggregateFunctions")
            DropForeignKey("dbo.AssessmentCriterions", "CalculationFilterId", "dbo.CalculationFilters")
            DropForeignKey("dbo.VariedDailyWorkingHours", "VariedWeeklyWorkingHoursId", "dbo.VariedWeeklyWorkingHours")
            DropForeignKey("dbo.VariedWeeklyWorkingHours", "ProjectId", "dbo.Projects")
            DropForeignKey("dbo.MeasurementViews", "VariedWeeklyWorkingHours_Id", "dbo.VariedWeeklyWorkingHours")
            DropForeignKey("dbo.VariedDailyWorkingHours", "DayOfWeekId", "dbo.DaysOfWeek")
            DropForeignKey("dbo.StandardDailyWorkingHours", "StandardWeeklyWorkingHoursId", "dbo.StandardWeeklyWorkingHours")
            DropForeignKey("dbo.MeasurementViews", "StandardWeeklyWorkingHours_Id", "dbo.StandardWeeklyWorkingHours")
            DropForeignKey("dbo.MeasurementViewProjects", "Project_Id", "dbo.Projects")
            DropForeignKey("dbo.MeasurementViewProjects", "MeasurementView_Id", "dbo.MeasurementViews")
            DropForeignKey("dbo.MeasurementViews", "MeasurementViewTableTypeId", "dbo.MeasurementViewTableTypes")
            DropForeignKey("dbo.MeasurementViewSequenceSettings", "WeekViewSeriesTypeId", "dbo.MeasurementViewSeriesTypes")
            DropForeignKey("dbo.MeasurementViewSequenceSettings", "WeekViewDashStyleId", "dbo.SeriesDashStyles")
            DropForeignKey("dbo.MeasurementViewSequenceSettings", "MonthViewSeriesTypeId", "dbo.MeasurementViewSeriesTypes")
            DropForeignKey("dbo.MeasurementViewSequenceSettings", "MonthViewDashStyleId", "dbo.SeriesDashStyles")
            DropForeignKey("dbo.MeasurementViewSequenceSettings", "MeasurementViewGroupId", "dbo.MeasurementViewGroups")
            DropForeignKey("dbo.MeasurementViewSequenceSettings", "DayViewSeriesTypeId", "dbo.MeasurementViewSeriesTypes")
            DropForeignKey("dbo.MeasurementViewSequenceSettings", "DayViewDashStyleId", "dbo.SeriesDashStyles")
            DropForeignKey("dbo.MeasurementViewSequenceSettings", "CalculationFilterId", "dbo.CalculationFilters")
            DropForeignKey("dbo.MeasurementViewGroups", "MeasurementViewId", "dbo.MeasurementViews")
            DropForeignKey("dbo.MeasurementViews", "MeasurementTypeId", "dbo.MeasurementTypes")
            DropForeignKey("dbo.MeasurementViewMeasurementCommentTypes", "MeasurementCommentType_Id", "dbo.MeasurementCommentTypes")
            DropForeignKey("dbo.MeasurementViewMeasurementCommentTypes", "MeasurementView_Id", "dbo.MeasurementViews")
            DropForeignKey("dbo.StandardDailyWorkingHours", "DayOfWeekId", "dbo.DaysOfWeek")
            DropForeignKey("dbo.DayOfWeekCalculationFilters", "CalculationFilter_Id", "dbo.CalculationFilters")
            DropForeignKey("dbo.DayOfWeekCalculationFilters", "DayOfWeek_Id", "dbo.DaysOfWeek")
            DropForeignKey("dbo.MeasurementFileTypes", "MeasurementTypeId", "dbo.MeasurementTypes")
            DropForeignKey("dbo.AssessmentCriterionGroups", "MeasurementTypeId", "dbo.MeasurementTypes")
            DropForeignKey("dbo.MeasurementFiles", "MeasurementFileTypeId", "dbo.MeasurementFileTypes")
            DropForeignKey("dbo.MeasurementFiles", "ContactId", "dbo.Contacts")
            DropForeignKey("dbo.MeasurementMeasurementComments", "MeasurementComment_Id", "dbo.MeasurementComments")
            DropForeignKey("dbo.MeasurementMeasurementComments", "Measurement_Id", "dbo.Measurements")
            DropForeignKey("dbo.ProjectDocuments", "Document_Id", "dbo.Documents")
            DropForeignKey("dbo.ProjectDocuments", "Project_Id", "dbo.Projects")
            DropForeignKey("dbo.Projects", "CountryId", "dbo.Countries")
            DropForeignKey("dbo.PublicHolidays", "CountryId", "dbo.Countries")
            DropForeignKey("dbo.ProjectContacts", "Contact_Id", "dbo.Contacts")
            DropForeignKey("dbo.ProjectContacts", "Project_Id", "dbo.Projects")
            DropForeignKey("dbo.AssessmentCriterionGroups", "ProjectId", "dbo.Projects")
            DropForeignKey("dbo.Monitors", "OwnerOrganisationId", "dbo.Organisations")
            DropForeignKey("dbo.Organisations", "OrganisationTypeId", "dbo.OrganisationTypes")
            DropForeignKey("dbo.MeasurementComments", "CommentTypeId", "dbo.MeasurementCommentTypes")
            DropForeignKey("dbo.AssessmentCriterionGroups", "AssessmentPeriodDurationTypeId", "dbo.AssessmentPeriodDurationTypes")
            DropForeignKey("dbo.AssessmentCriterions", "AssessmentCriterionGroupId", "dbo.AssessmentCriterionGroups")
            DropForeignKey("dbo.AssessmentCriterions", "AssessedLevelSeriesTypeId", "dbo.MeasurementViewSeriesTypes")
            DropForeignKey("dbo.AssessmentCriterions", "AssessedLevelDashStyleId", "dbo.SeriesDashStyles")
            DropForeignKey("dbo.AssessmentCriterions", "SeriesDashStyle_Id1", "dbo.SeriesDashStyles")
            DropForeignKey("dbo.AssessmentCriterions", "SeriesDashStyle_Id", "dbo.SeriesDashStyles")
            DropIndex("dbo.MeasurementCommentTypeAssessmentCriterionGroups", New String() { "AssessmentCriterionGroup_Id" })
            DropIndex("dbo.MeasurementCommentTypeAssessmentCriterionGroups", New String() { "MeasurementCommentType_Id" })
            DropIndex("dbo.DocumentMonitors", New String() { "Monitor_Id" })
            DropIndex("dbo.DocumentMonitors", New String() { "Document_Id" })
            DropIndex("dbo.DocumentMonitorLocations", New String() { "MonitorLocation_Id" })
            DropIndex("dbo.DocumentMonitorLocations", New String() { "Document_Id" })
            DropIndex("dbo.DocumentContacts", New String() { "Contact_Id" })
            DropIndex("dbo.DocumentContacts", New String() { "Document_Id" })
            DropIndex("dbo.OrganisationProjects", New String() { "Project_Id" })
            DropIndex("dbo.OrganisationProjects", New String() { "Organisation_Id" })
            DropIndex("dbo.MeasurementViewProjects", New String() { "Project_Id" })
            DropIndex("dbo.MeasurementViewProjects", New String() { "MeasurementView_Id" })
            DropIndex("dbo.MeasurementViewMeasurementCommentTypes", New String() { "MeasurementCommentType_Id" })
            DropIndex("dbo.MeasurementViewMeasurementCommentTypes", New String() { "MeasurementView_Id" })
            DropIndex("dbo.DayOfWeekCalculationFilters", New String() { "CalculationFilter_Id" })
            DropIndex("dbo.DayOfWeekCalculationFilters", New String() { "DayOfWeek_Id" })
            DropIndex("dbo.MeasurementMeasurementComments", New String() { "MeasurementComment_Id" })
            DropIndex("dbo.MeasurementMeasurementComments", New String() { "Measurement_Id" })
            DropIndex("dbo.ProjectDocuments", New String() { "Document_Id" })
            DropIndex("dbo.ProjectDocuments", New String() { "Project_Id" })
            DropIndex("dbo.ProjectContacts", New String() { "Contact_Id" })
            DropIndex("dbo.ProjectContacts", New String() { "Project_Id" })
            DropIndex("dbo.VibrationSettings", New String() { "Id" })
            DropIndex("dbo.NoiseSettings", New String() { "Id" })
            DropIndex("dbo.MonitorStatus", New String() { "Id" })
            DropIndex("dbo.Users", New String() { "Id" })
            DropIndex("dbo.VariedWeeklyWorkingHours", New String() { "ProjectId" })
            DropIndex("dbo.VariedDailyWorkingHours", New String() { "VariedWeeklyWorkingHoursId" })
            DropIndex("dbo.VariedDailyWorkingHours", New String() { "DayOfWeekId" })
            DropIndex("dbo.MeasurementViewSequenceSettings", New String() { "MonthViewDashStyleId" })
            DropIndex("dbo.MeasurementViewSequenceSettings", New String() { "WeekViewDashStyleId" })
            DropIndex("dbo.MeasurementViewSequenceSettings", New String() { "DayViewDashStyleId" })
            DropIndex("dbo.MeasurementViewSequenceSettings", New String() { "CalculationFilterId" })
            DropIndex("dbo.MeasurementViewSequenceSettings", New String() { "MeasurementViewGroupId" })
            DropIndex("dbo.MeasurementViewSequenceSettings", New String() { "MonthViewSeriesTypeId" })
            DropIndex("dbo.MeasurementViewSequenceSettings", New String() { "WeekViewSeriesTypeId" })
            DropIndex("dbo.MeasurementViewSequenceSettings", New String() { "DayViewSeriesTypeId" })
            DropIndex("dbo.MeasurementViewGroups", New String() { "MeasurementViewId" })
            DropIndex("dbo.MeasurementViews", New String() { "VariedWeeklyWorkingHours_Id" })
            DropIndex("dbo.MeasurementViews", New String() { "StandardWeeklyWorkingHours_Id" })
            DropIndex("dbo.MeasurementViews", New String() { "MeasurementViewTableTypeId" })
            DropIndex("dbo.MeasurementViews", New String() { "MeasurementTypeId" })
            DropIndex("dbo.StandardDailyWorkingHours", New String() { "StandardWeeklyWorkingHoursId" })
            DropIndex("dbo.StandardDailyWorkingHours", New String() { "DayOfWeekId" })
            DropIndex("dbo.CalculationFilters", New String() { "CalculationAggregateFunctionId" })
            DropIndex("dbo.CalculationFilters", New String() { "MeasurementMetricId" })
            DropIndex("dbo.MeasurementMetrics", New String() { "MeasurementTypeId" })
            DropIndex("dbo.MeasurementFileTypes", New String() { "MeasurementTypeId" })
            DropIndex("dbo.MeasurementFiles", New String() { "MeasurementFileTypeId" })
            DropIndex("dbo.MeasurementFiles", New String() { "ContactId" })
            DropIndex("dbo.MeasurementFiles", New String() { "MonitorLocationId" })
            DropIndex("dbo.MeasurementFiles", New String() { "MonitorId" })
            DropIndex("dbo.Measurements", New String() { "UploadedByContact_Id" })
            DropIndex("dbo.Measurements", New String() { "MeasurementFileId" })
            DropIndex("dbo.Measurements", New String() { "ProjectId" })
            DropIndex("dbo.Measurements", New String() { "MonitorLocationId" })
            DropIndex("dbo.Measurements", New String() { "MonitorId" })
            DropIndex("dbo.Measurements", New String() { "MeasurementMetricId" })
            DropIndex("dbo.PublicHolidays", New String() { "CountryId" })
            DropIndex("dbo.Projects", New String() { "CountryId" })
            DropIndex("dbo.Projects", New String() { "ClientOrganisationId" })
            DropIndex("dbo.Projects", New String() { "ProjectGeoCoordsId" })
            DropIndex("dbo.Projects", New String() { "Id" })
            DropIndex("dbo.Organisations", New String() { "OrganisationTypeId" })
            DropIndex("dbo.Documents", New String() { "AuthorOrganisationId" })
            DropIndex("dbo.Documents", New String() { "DocumentTypeId" })
            DropIndex("dbo.Contacts", New String() { "UserAccessLevelId" })
            DropIndex("dbo.Contacts", New String() { "OrganisationId" })
            DropIndex("dbo.MeasurementComments", New String() { "MonitorLocationId" })
            DropIndex("dbo.MeasurementComments", New String() { "ContactId" })
            DropIndex("dbo.MeasurementComments", New String() { "CommentTypeId" })
            DropIndex("dbo.AssessmentCriterionGroups", New String() { "AssessmentPeriodDurationTypeId" })
            DropIndex("dbo.AssessmentCriterionGroups", New String() { "ThresholdAggregateDurationId" })
            DropIndex("dbo.AssessmentCriterionGroups", New String() { "MeasurementTypeId" })
            DropIndex("dbo.AssessmentCriterionGroups", New String() { "ProjectId" })
            DropIndex("dbo.AssessmentCriterions", New String() { "SeriesDashStyle_Id1" })
            DropIndex("dbo.AssessmentCriterions", New String() { "SeriesDashStyle_Id" })
            DropIndex("dbo.AssessmentCriterions", New String() { "CriterionLevelDashStyleId" })
            DropIndex("dbo.AssessmentCriterions", New String() { "AssessedLevelSeriesTypeId" })
            DropIndex("dbo.AssessmentCriterions", New String() { "AssessedLevelDashStyleId" })
            DropIndex("dbo.AssessmentCriterions", New String() { "MonitorLocationId" })
            DropIndex("dbo.AssessmentCriterions", New String() { "ThresholdTypeId" })
            DropIndex("dbo.AssessmentCriterions", New String() { "AssessmentCriterionGroupId" })
            DropIndex("dbo.AssessmentCriterions", New String() { "CalculationFilterId" })
            DropIndex("dbo.MonitorLocations", New String() { "CurrentMonitor_Id" })
            DropIndex("dbo.MonitorLocations", New String() { "MeasurementTypeId" })
            DropIndex("dbo.MonitorLocations", New String() { "ProjectId" })
            DropIndex("dbo.MonitorLocations", New String() { "MonitorLocationGeoCoordsId" })
            DropIndex("dbo.Monitors", New String() { "OwnerOrganisationId" })
            DropIndex("dbo.Monitors", New String() { "MeasurementTypeId" })
            DropIndex("dbo.MonitorDeploymentRecords", New String() { "MonitorLocationId" })
            DropIndex("dbo.MonitorDeploymentRecords", New String() { "MonitorId" })
            DropIndex("dbo.MonitorDeploymentRecords", New String() { "Id" })
            DropIndex("dbo.MonitorSettings", New String() { "Id" })
            DropIndex("dbo.AirQualitySettings", New String() { "Id" })
            DropTable("dbo.MeasurementCommentTypeAssessmentCriterionGroups")
            DropTable("dbo.DocumentMonitors")
            DropTable("dbo.DocumentMonitorLocations")
            DropTable("dbo.DocumentContacts")
            DropTable("dbo.OrganisationProjects")
            DropTable("dbo.MeasurementViewProjects")
            DropTable("dbo.MeasurementViewMeasurementCommentTypes")
            DropTable("dbo.DayOfWeekCalculationFilters")
            DropTable("dbo.MeasurementMeasurementComments")
            DropTable("dbo.ProjectDocuments")
            DropTable("dbo.ProjectContacts")
            DropTable("dbo.VibrationSettings")
            DropTable("dbo.NoiseSettings")
            DropTable("dbo.MonitorStatus")
            DropTable("dbo.MonitorLocationGeoCoords")
            DropTable("dbo.ThresholdTypes")
            DropTable("dbo.ThresholdAggregateDurations")
            DropTable("dbo.UserAccessLevels")
            DropTable("dbo.Users")
            DropTable("dbo.DocumentTypes")
            DropTable("dbo.ProjectGeoCoords")
            DropTable("dbo.CalculationAggregateFunctions")
            DropTable("dbo.VariedWeeklyWorkingHours")
            DropTable("dbo.VariedDailyWorkingHours")
            DropTable("dbo.MeasurementViewTableTypes")
            DropTable("dbo.MeasurementViewSequenceSettings")
            DropTable("dbo.MeasurementViewGroups")
            DropTable("dbo.MeasurementViews")
            DropTable("dbo.StandardWeeklyWorkingHours")
            DropTable("dbo.StandardDailyWorkingHours")
            DropTable("dbo.DaysOfWeek")
            DropTable("dbo.CalculationFilters")
            DropTable("dbo.MeasurementMetrics")
            DropTable("dbo.MeasurementTypes")
            DropTable("dbo.MeasurementFileTypes")
            DropTable("dbo.MeasurementFiles")
            DropTable("dbo.Measurements")
            DropTable("dbo.PublicHolidays")
            DropTable("dbo.Countries")
            DropTable("dbo.Projects")
            DropTable("dbo.OrganisationTypes")
            DropTable("dbo.Organisations")
            DropTable("dbo.Documents")
            DropTable("dbo.Contacts")
            DropTable("dbo.MeasurementComments")
            DropTable("dbo.MeasurementCommentTypes")
            DropTable("dbo.AssessmentPeriodDurationTypes")
            DropTable("dbo.AssessmentCriterionGroups")
            DropTable("dbo.MeasurementViewSeriesTypes")
            DropTable("dbo.SeriesDashStyles")
            DropTable("dbo.AssessmentCriterions")
            DropTable("dbo.MonitorLocations")
            DropTable("dbo.Monitors")
            DropTable("dbo.MonitorDeploymentRecords")
            DropTable("dbo.MonitorSettings")
            DropTable("dbo.AirQualitySettings")
        End Sub
    End Class
End Namespace
