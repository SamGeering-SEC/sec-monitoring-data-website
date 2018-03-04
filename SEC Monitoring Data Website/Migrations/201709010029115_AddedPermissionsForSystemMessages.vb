Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForSystemMessages
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanCreateSystemMessages", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteSystemMessages", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanEditSystemMessages", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanEditSystemMessages")
            DropColumn("dbo.UserAccessLevels", "CanDeleteSystemMessages")
            DropColumn("dbo.UserAccessLevels", "CanCreateSystemMessages")
        End Sub
    End Class
End Namespace
