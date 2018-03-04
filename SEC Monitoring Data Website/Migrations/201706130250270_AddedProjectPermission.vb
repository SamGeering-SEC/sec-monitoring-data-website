Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedProjectPermission
        Inherits DbMigration
    
        Public Overrides Sub Up()
            CreateTable(
                "dbo.ProjectPermissions",
                Function(c) New With
                    {
                        .Id = c.Int(nullable := False, identity := True),
                        .PermissionName = c.String()
                    }) _
                .PrimaryKey(Function(t) t.Id)
            
            AddColumn("dbo.UserAccessLevels", "CanCreateAssessmentCriteria", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditAssessmentCriteria", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewAssessmentCriteria", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            DropColumn("dbo.UserAccessLevels", "CanAddContacts")
            DropColumn("dbo.UserAccessLevels", "CanEditContacts")
            DropColumn("dbo.UserAccessLevels", "CanDeleteContacts")
            DropColumn("dbo.UserAccessLevels", "CanAddOrganisations")
            DropColumn("dbo.UserAccessLevels", "CanEditOrganisations")
            DropColumn("dbo.UserAccessLevels", "CanDeleteOrganisations")
            DropColumn("dbo.UserAccessLevels", "CanAddOrganisationTypes")
            DropColumn("dbo.UserAccessLevels", "CanEditOrganisationTypes")
            DropColumn("dbo.UserAccessLevels", "CanDeleteOrganisationTypes")
            DropColumn("dbo.UserAccessLevels", "CanAddProjects")
            DropColumn("dbo.UserAccessLevels", "CanEditProjects")
            DropColumn("dbo.UserAccessLevels", "CanDeleteProjects")
            DropColumn("dbo.UserAccessLevels", "CanAddDocuments")
            DropColumn("dbo.UserAccessLevels", "CanEditDocuments")
            DropColumn("dbo.UserAccessLevels", "CanDeleteDocuments")
            DropColumn("dbo.UserAccessLevels", "CanAddDocumentTypes")
            DropColumn("dbo.UserAccessLevels", "CanEditDocumentTypes")
            DropColumn("dbo.UserAccessLevels", "CanDeleteDocumentTypes")
            DropColumn("dbo.UserAccessLevels", "CanAddMonitors")
            DropColumn("dbo.UserAccessLevels", "CanEditMonitors")
            DropColumn("dbo.UserAccessLevels", "CanDeleteMonitors")
            DropColumn("dbo.UserAccessLevels", "CanAddMonitorLocations")
            DropColumn("dbo.UserAccessLevels", "CanEditMonitorLocations")
            DropColumn("dbo.UserAccessLevels", "CanDeleteMonitorLocations")
            DropColumn("dbo.UserAccessLevels", "CanAddMonitorLocationRecords")
            DropColumn("dbo.UserAccessLevels", "CanEditMonitorLocationRecords")
            DropColumn("dbo.UserAccessLevels", "CanDeleteMonitorLocationRecords")
            DropColumn("dbo.UserAccessLevels", "CanAddMeasurements")
            DropColumn("dbo.UserAccessLevels", "CanEditMeasurements")
            DropColumn("dbo.UserAccessLevels", "CanDeleteMeasurements")
            DropColumn("dbo.UserAccessLevels", "CanAddCalculationFilters")
            DropColumn("dbo.UserAccessLevels", "CanEditCalculationFilters")
            DropColumn("dbo.UserAccessLevels", "CanDeleteCalculationFilters")
            DropColumn("dbo.UserAccessLevels", "CanAddUserAccessLevels")
            DropColumn("dbo.UserAccessLevels", "CanEditUserAccessLevels")
            DropColumn("dbo.UserAccessLevels", "CanDeleteUserAccessLevels")
            DropColumn("dbo.UserAccessLevels", "CanAddPublicHolidays")
            DropColumn("dbo.UserAccessLevels", "CanEditPublicHolidays")
            DropColumn("dbo.UserAccessLevels", "CanDeletePublicHolidays")
            DropColumn("dbo.UserAccessLevels", "CanAddCountries")
            DropColumn("dbo.UserAccessLevels", "CanEditCountries")
            DropColumn("dbo.UserAccessLevels", "CanDeleteCountries")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.UserAccessLevels", "CanDeleteCountries", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditCountries", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddCountries", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeletePublicHolidays", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditPublicHolidays", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddPublicHolidays", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteUserAccessLevels", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditUserAccessLevels", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddUserAccessLevels", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteCalculationFilters", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditCalculationFilters", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddCalculationFilters", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteMeasurements", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditMeasurements", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddMeasurements", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteMonitorLocationRecords", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditMonitorLocationRecords", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddMonitorLocationRecords", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteMonitorLocations", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditMonitorLocations", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddMonitorLocations", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteMonitors", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditMonitors", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddMonitors", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteDocumentTypes", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditDocumentTypes", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddDocumentTypes", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteDocuments", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditDocuments", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddDocuments", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteProjects", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditProjects", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddProjects", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteOrganisationTypes", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditOrganisationTypes", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddOrganisationTypes", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteOrganisations", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditOrganisations", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddOrganisations", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteContacts", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditContacts", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanAddContacts", Function(c) c.Boolean(nullable := False))
            DropColumn("dbo.UserAccessLevels", "CanViewAssessmentCriteria")
            DropColumn("dbo.UserAccessLevels", "CanEditAssessmentCriteria")
            DropColumn("dbo.UserAccessLevels", "CanCreateAssessmentCriteria")
            DropTable("dbo.ProjectPermissions")
        End Sub
    End Class
End Namespace
