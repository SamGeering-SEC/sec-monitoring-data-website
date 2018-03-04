Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ExtendedPermissionsForSystemMessages
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanViewSystemMessages", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanViewSystemMessages")
        End Sub
    End Class
End Namespace
