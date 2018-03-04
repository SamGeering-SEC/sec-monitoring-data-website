Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForContactsController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanCreateContacts", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewContactList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewContactDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditContacts", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteContacts", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewDocumentDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMeasurementFileDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMonitorDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMonitorLocationDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewOrganisationDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewUserDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanViewUserDetails")
            DropColumn("dbo.UserAccessLevels", "CanViewOrganisationDetails")
            DropColumn("dbo.UserAccessLevels", "CanViewMonitorLocationDetails")
            DropColumn("dbo.UserAccessLevels", "CanViewMonitorDetails")
            DropColumn("dbo.UserAccessLevels", "CanViewMeasurementFileDetails")
            DropColumn("dbo.UserAccessLevels", "CanViewDocumentDetails")
            DropColumn("dbo.UserAccessLevels", "CanDeleteContacts")
            DropColumn("dbo.UserAccessLevels", "CanEditContacts")
            DropColumn("dbo.UserAccessLevels", "CanViewContactDetails")
            DropColumn("dbo.UserAccessLevels", "CanViewContactList")
            DropColumn("dbo.UserAccessLevels", "CanCreateContacts")
        End Sub
    End Class
End Namespace
