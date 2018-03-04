Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForMeasurementFilesController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanDeleteMeasurements", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteMeasurementFiles", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanDeleteMeasurementFiles")
            DropColumn("dbo.UserAccessLevels", "CanDeleteMeasurements")
        End Sub
    End Class
End Namespace
