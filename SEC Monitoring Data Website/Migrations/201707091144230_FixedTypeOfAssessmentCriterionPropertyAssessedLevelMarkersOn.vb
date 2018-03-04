Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class FixedTypeOfAssessmentCriterionPropertyAssessedLevelMarkersOn
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.AssessmentCriterions", "AssessedLevelMarkersOn", Function(c) c.Boolean(nullable := False))
        End Sub
        
        Public Overrides Sub Down()
            AlterColumn("dbo.AssessmentCriterions", "AssessedLevelMarkersOn", Function(c) c.String())
        End Sub
    End Class
End Namespace
