Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class LinkedUserAccessLevelsToProjectPermissions
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "ProjectPermissionId", Function(c) c.Int(nullable:=False, defaultValue:=1))
            CreateIndex("dbo.UserAccessLevels", "ProjectPermissionId")
            AddForeignKey("dbo.UserAccessLevels", "ProjectPermissionId", "dbo.ProjectPermissions", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.UserAccessLevels", "ProjectPermissionId", "dbo.ProjectPermissions")
            DropIndex("dbo.UserAccessLevels", New String() { "ProjectPermissionId" })
            DropColumn("dbo.UserAccessLevels", "ProjectPermissionId")
        End Sub
    End Class
End Namespace
