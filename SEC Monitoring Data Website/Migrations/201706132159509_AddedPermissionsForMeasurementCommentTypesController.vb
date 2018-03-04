Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForMeasurementCommentTypesController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanCreateMeasurementCommentTypes", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditMeasurementCommentTypes", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteMeasurementCommentTypes", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMeasurementViewDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanViewMeasurementViewDetails")
            DropColumn("dbo.UserAccessLevels", "CanDeleteMeasurementCommentTypes")
            DropColumn("dbo.UserAccessLevels", "CanEditMeasurementCommentTypes")
            DropColumn("dbo.UserAccessLevels", "CanCreateMeasurementCommentTypes")
        End Sub
    End Class
End Namespace
