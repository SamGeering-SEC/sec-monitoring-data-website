Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedLockingForUserAccounts
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Users", "IsLocked", Function(c) c.Boolean(nullable:=False, defaultValue:=False))
            AddColumn("dbo.Users", "ConsecutiveUnsuccessfulLogins", Function(c) c.Int(nullable:=False, defaultValue:=0))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Users", "ConsecutiveUnsuccessfulLogins")
            DropColumn("dbo.Users", "IsLocked")
        End Sub
    End Class
End Namespace
