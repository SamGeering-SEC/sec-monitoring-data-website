Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForMonitorLocationsController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanDeleteAssessmentCriteria", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteMonitorLocations", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewSelectMonitorLocations", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditMonitorLocations", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanCreateMonitorLocations", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanCreateMonitorLocations")
            DropColumn("dbo.UserAccessLevels", "CanEditMonitorLocations")
            DropColumn("dbo.UserAccessLevels", "CanViewSelectMonitorLocations")
            DropColumn("dbo.UserAccessLevels", "CanDeleteMonitorLocations")
            DropColumn("dbo.UserAccessLevels", "CanDeleteAssessmentCriteria")
        End Sub
    End Class
End Namespace
