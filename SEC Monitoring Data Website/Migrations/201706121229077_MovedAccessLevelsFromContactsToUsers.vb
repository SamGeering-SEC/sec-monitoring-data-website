Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class MovedAccessLevelsFromContactsToUsers
        Inherits DbMigration
    
        Public Overrides Sub Up()
            RenameColumn(table := "dbo.Contacts", name := "UserAccessLevelId", newName := "UserAccessLevel_Id")
            RenameIndex(table := "dbo.Contacts", name := "IX_UserAccessLevelId", newName := "IX_UserAccessLevel_Id")
            AddColumn("dbo.Users", "UserAccessLevelId", Function(c) c.Int(nullable:=False, defaultValue:=1))
            CreateIndex("dbo.Users", "UserAccessLevelId")
            AddForeignKey("dbo.Users", "UserAccessLevelId", "dbo.UserAccessLevels", "Id")
        End Sub
        
        Public Overrides Sub Down()
            DropForeignKey("dbo.Users", "UserAccessLevelId", "dbo.UserAccessLevels")
            DropIndex("dbo.Users", New String() { "UserAccessLevelId" })
            DropColumn("dbo.Users", "UserAccessLevelId")
            RenameIndex(table := "dbo.Contacts", name := "IX_UserAccessLevel_Id", newName := "IX_UserAccessLevelId")
            RenameColumn(table := "dbo.Contacts", name := "UserAccessLevel_Id", newName := "UserAccessLevelId")
        End Sub
    End Class
End Namespace
