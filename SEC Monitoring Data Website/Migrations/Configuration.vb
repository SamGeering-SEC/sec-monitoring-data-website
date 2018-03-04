Imports System
Imports System.Data.Entity
Imports System.Data.Entity.Migrations
Imports System.Linq

Namespace Migrations

    Friend NotInheritable Class Configuration 
        Inherits DbMigrationsConfiguration(Of SECMonitoringDbContext)

        Public Sub New()
            AutomaticMigrationsEnabled = False
            ContextKey = "SEC_Monitoring_Data_Website.SECMonitoringDbContext"
        End Sub

        Protected Overrides Sub Seed(context As SECMonitoringDbContext)

            If context.ProjectPermissions.Count = 0 Then
                context.ProjectPermissions.AddOrUpdate(New ProjectPermission With {.PermissionName = "AllProjects"})
                context.ProjectPermissions.AddOrUpdate(New ProjectPermission With {.PermissionName = "MemberOrganisationProjects"})
                context.ProjectPermissions.AddOrUpdate(New ProjectPermission With {.PermissionName = "ClientOrganisationsProjects"})
                context.ProjectPermissions.AddOrUpdate(New ProjectPermission With {.PermissionName = "OwnProjects"})
                context.SaveChanges()
            End If

        End Sub

    End Class

End Namespace
