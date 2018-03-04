Imports System
Imports System.Collections.Generic

Partial Public Class SeriesDashStyle
    Public Property Id As Integer
    Public Property DashStyleName As String
    Public Property DashStyleEnum As Integer
    Public Property AssessmentCriteriaAsAssessedLevelStyle As ICollection(Of AssessmentCriterion)
    Public Property AssessmentCriteriaAsCriterionLevelStyle As ICollection(Of AssessmentCriterion)

End Class
