Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class AddedCommentDateTimes
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.MeasurementComments", "FirstMeasurementStartDateTime", Function(c) c.DateTime(nullable := False))
            AddColumn("dbo.MeasurementComments", "LastMeasurementEndDateTime", Function(c) c.DateTime(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.MeasurementComments", "LastMeasurementEndDateTime")
            DropColumn("dbo.MeasurementComments", "FirstMeasurementStartDateTime")
        End Sub
    End Class
End Namespace
