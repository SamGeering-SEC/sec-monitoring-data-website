Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedPermissionsForCalculationFiltersController
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.UserAccessLevels", "CanCreateCalculationFilters", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewCalculationFilterList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewCalculationFilterDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditCalculationFilters", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteCalculationFilters", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanCreateMeasurementMetrics", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMeasurementMetricList", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanViewMeasurementMetricDetails", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanEditMeasurementMetrics", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
            AddColumn("dbo.UserAccessLevels", "CanDeleteMeasurementMetrics", Function(c) c.Boolean(nullable:=False, defaultValue:=True))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.UserAccessLevels", "CanDeleteMeasurementMetrics")
            DropColumn("dbo.UserAccessLevels", "CanEditMeasurementMetrics")
            DropColumn("dbo.UserAccessLevels", "CanViewMeasurementMetricDetails")
            DropColumn("dbo.UserAccessLevels", "CanViewMeasurementMetricList")
            DropColumn("dbo.UserAccessLevels", "CanCreateMeasurementMetrics")
            DropColumn("dbo.UserAccessLevels", "CanDeleteCalculationFilters")
            DropColumn("dbo.UserAccessLevels", "CanEditCalculationFilters")
            DropColumn("dbo.UserAccessLevels", "CanViewCalculationFilterDetails")
            DropColumn("dbo.UserAccessLevels", "CanViewCalculationFilterList")
            DropColumn("dbo.UserAccessLevels", "CanCreateCalculationFilters")
        End Sub
    End Class
End Namespace
