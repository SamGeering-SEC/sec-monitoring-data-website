Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedReceivesLockNotificationsToUserModel
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Users", "ReceivesLockNotifications", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Users", "ReceivesLockNotifications")
        End Sub
    End Class
End Namespace
