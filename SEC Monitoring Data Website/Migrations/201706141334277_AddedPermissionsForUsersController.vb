Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForUsersController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanCreateUsers", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteUsers", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditUsers", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewUserAccessLevelDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanViewUserAccessLevelDetails")
            DropColumn("dbo.UserAccessLevels", "CanEditUsers")
            DropColumn("dbo.UserAccessLevels", "CanDeleteUsers")
            DropColumn("dbo.UserAccessLevels", "CanCreateUsers")
        End Sub
    End Class
End Namespace
