Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForOrganisationsController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanEditOrganisations", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewOrganisationTypeList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewOrganisationTypeDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanCreateOrganisations", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteOrganisations", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanDeleteOrganisations")
            DropColumn("dbo.UserAccessLevels", "CanCreateOrganisations")
            DropColumn("dbo.UserAccessLevels", "CanViewOrganisationTypeDetails")
            DropColumn("dbo.UserAccessLevels", "CanViewOrganisationTypeList")
            DropColumn("dbo.UserAccessLevels", "CanEditOrganisations")
        End Sub
    End Class
End Namespace
