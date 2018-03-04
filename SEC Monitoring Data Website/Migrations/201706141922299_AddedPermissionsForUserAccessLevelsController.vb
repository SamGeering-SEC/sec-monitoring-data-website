Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForUserAccessLevelsController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanCreateUserAccessLevels", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteUserAccessLevels", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditUserAccessLevels", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewUserAccessLevelList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanViewUserAccessLevelList")
            DropColumn("dbo.UserAccessLevels", "CanEditUserAccessLevels")
            DropColumn("dbo.UserAccessLevels", "CanDeleteUserAccessLevels")
            DropColumn("dbo.UserAccessLevels", "CanCreateUserAccessLevels")
        End Sub
    End Class
End Namespace
