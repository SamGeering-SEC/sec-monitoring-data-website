Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForMeasurementCommentsController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanViewMeasurementCommentList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteMeasurementComments", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanCreateMeasurementComments", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMeasurementCommentTypeDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanViewMeasurementCommentTypeDetails")
            DropColumn("dbo.UserAccessLevels", "CanCreateMeasurementComments")
            DropColumn("dbo.UserAccessLevels", "CanDeleteMeasurementComments")
            DropColumn("dbo.UserAccessLevels", "CanViewMeasurementCommentList")
        End Sub
    End Class
End Namespace
