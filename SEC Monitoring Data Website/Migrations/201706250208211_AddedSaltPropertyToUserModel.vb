Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedSaltPropertyToUserModel
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Users", "Salt", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Users", "Salt")
        End Sub
    End Class
End Namespace
