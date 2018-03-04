Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedNumberOfMeasurementsFieldToMeasurementFiles
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.MeasurementFiles", "NumberOfMeasurements", Function(c) c.Int(nullable:=False, defaultValue:=0))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.MeasurementFiles", "NumberOfMeasurements")
        End Sub

    End Class
End Namespace
