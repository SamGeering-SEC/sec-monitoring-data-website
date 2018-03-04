Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedFirstAndLastMeasurementFieldsToMeasurementFiles
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.MeasurementFiles", "FirstMeasurementStartDateTime", Function(c) c.DateTime(nullable:=False))
            AddColumn("dbo.MeasurementFiles", "LastMeasurementStartDateTime", Function(c) c.DateTime(nullable:=False))
            AddColumn("dbo.MeasurementFiles", "LastMeasurementDuration", Function(c) c.Double(nullable:=False, defaultValue:=0))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.MeasurementFiles", "LastMeasurementDuration")
            DropColumn("dbo.MeasurementFiles", "LastMeasurementStartDateTime")
            DropColumn("dbo.MeasurementFiles", "FirstMeasurementStartDateTime")
        End Sub
    End Class
End Namespace
