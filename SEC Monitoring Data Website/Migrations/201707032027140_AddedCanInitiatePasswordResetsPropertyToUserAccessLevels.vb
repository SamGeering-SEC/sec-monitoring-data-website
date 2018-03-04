Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedCanInitiatePasswordResetsPropertyToUserAccessLevels
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanInitiatePasswordResets", Function(c) c.Boolean(nullable:=False, defaultValue:=False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanInitiatePasswordResets")
        End Sub
    End Class
End Namespace
