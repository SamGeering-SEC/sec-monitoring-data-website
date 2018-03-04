Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForDocumentTypesController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanEditDocumentTypes", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanDeleteDocumentTypes", Function(c) c.Boolean(nullable := False))
            AddColumn("dbo.UserAccessLevels", "CanCreateDocumentTypes", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanCreateDocumentTypes")
            DropColumn("dbo.UserAccessLevels", "CanDeleteDocumentTypes")
            DropColumn("dbo.UserAccessLevels", "CanEditDocumentTypes")
        End Sub
    End Class
End Namespace
