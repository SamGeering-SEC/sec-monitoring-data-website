Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForHomeController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanViewMeasurementCommentTypeList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMeasurementFileList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMeasurementViewList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMonitorList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMonitorDeploymentRecordList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMonitorLocationList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewOrganisationList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewProjectList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewUserList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanViewUserList")
            DropColumn("dbo.UserAccessLevels", "CanViewProjectList")
            DropColumn("dbo.UserAccessLevels", "CanViewOrganisationList")
            DropColumn("dbo.UserAccessLevels", "CanViewMonitorLocationList")
            DropColumn("dbo.UserAccessLevels", "CanViewMonitorDeploymentRecordList")
            DropColumn("dbo.UserAccessLevels", "CanViewMonitorList")
            DropColumn("dbo.UserAccessLevels", "CanViewMeasurementViewList")
            DropColumn("dbo.UserAccessLevels", "CanViewMeasurementFileList")
            DropColumn("dbo.UserAccessLevels", "CanViewMeasurementCommentTypeList")
        End Sub
    End Class
End Namespace
