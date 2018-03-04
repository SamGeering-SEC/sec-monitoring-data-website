Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForMonitorDeploymentRecords
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanDeleteMonitorDeploymentRecords", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMonitorDeploymentRecordDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanCreateDeploymentRecords", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEndMonitorDeployments", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanEndMonitorDeployments")
            DropColumn("dbo.UserAccessLevels", "CanCreateDeploymentRecords")
            DropColumn("dbo.UserAccessLevels", "CanViewMonitorDeploymentRecordDetails")
            DropColumn("dbo.UserAccessLevels", "CanDeleteMonitorDeploymentRecords")
        End Sub
    End Class
End Namespace
