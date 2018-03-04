Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class CorrectedPermissionForMeasurementViews
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanEditMeasurementViews", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            DropColumn("dbo.UserAccessLevels", "CanEditDeleteMeasurementViews")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.UserAccessLevels", "CanEditDeleteMeasurementViews", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            DropColumn("dbo.UserAccessLevels", "CanEditMeasurementViews")
        End Sub
    End Class
End Namespace
