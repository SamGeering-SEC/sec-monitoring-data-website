Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForDocumentsController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanViewDocumentList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanCreateDocuments", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditDocuments", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteDocuments", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewDocumentTypeList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewDocumentTypeDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanViewDocumentTypeDetails")
            DropColumn("dbo.UserAccessLevels", "CanViewDocumentTypeList")
            DropColumn("dbo.UserAccessLevels", "CanDeleteDocuments")
            DropColumn("dbo.UserAccessLevels", "CanEditDocuments")
            DropColumn("dbo.UserAccessLevels", "CanCreateDocuments")
            DropColumn("dbo.UserAccessLevels", "CanViewDocumentList")
        End Sub
    End Class
End Namespace
