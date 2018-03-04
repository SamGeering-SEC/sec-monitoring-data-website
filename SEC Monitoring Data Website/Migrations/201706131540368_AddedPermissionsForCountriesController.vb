Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForCountriesController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanCreateCountries", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewCountryList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewCountryDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditCountries", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteCountries", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanCreatePublicHolidays", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeletePublicHolidays", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanDeletePublicHolidays")
            DropColumn("dbo.UserAccessLevels", "CanCreatePublicHolidays")
            DropColumn("dbo.UserAccessLevels", "CanDeleteCountries")
            DropColumn("dbo.UserAccessLevels", "CanEditCountries")
            DropColumn("dbo.UserAccessLevels", "CanViewCountryDetails")
            DropColumn("dbo.UserAccessLevels", "CanViewCountryList")
            DropColumn("dbo.UserAccessLevels", "CanCreateCountries")
        End Sub
    End Class
End Namespace
