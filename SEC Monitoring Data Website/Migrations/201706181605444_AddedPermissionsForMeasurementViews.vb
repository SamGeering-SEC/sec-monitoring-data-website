Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForMeasurementViews
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanCreateMeasurementViews", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditDeleteMeasurementViews", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteMeasurementViews", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanDeleteMeasurementViews")
            DropColumn("dbo.UserAccessLevels", "CanEditDeleteMeasurementViews")
            DropColumn("dbo.UserAccessLevels", "CanCreateMeasurementViews")
        End Sub
    End Class
End Namespace
