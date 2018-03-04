Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForOrganisationTypesAndProjectsControllers
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanCreateProjects", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteProjects", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditProjects", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanCreateOrganisationTypes", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteOrganisationTypes", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditOrganisationTypes", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanEditProjects")
            DropColumn("dbo.UserAccessLevels", "CanDeleteProjects")
            DropColumn("dbo.UserAccessLevels", "CanCreateProjects")
            DropColumn("dbo.UserAccessLevels", "CanCreateOrganisationTypes")
            DropColumn("dbo.UserAccessLevels", "CanDeleteOrganisationTypes")
            DropColumn("dbo.UserAccessLevels", "CanEditOrganisationTypes")
        End Sub
    End Class
End Namespace
