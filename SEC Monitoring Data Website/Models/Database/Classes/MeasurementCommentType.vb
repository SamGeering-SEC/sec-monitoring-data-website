'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class MeasurementCommentType
    Public Property Id As Integer
    Public Property CommentTypeName As String

    Public Overridable Property Comments As ICollection(Of MeasurementComment) = New HashSet(Of MeasurementComment)
    Public Overridable Property ExcludedMeasurementViews As ICollection(Of MeasurementView) = New HashSet(Of MeasurementView)
    Public Overridable Property ExcludedAssessmentCriterionGroups As ICollection(Of AssessmentCriterionGroup) = New HashSet(Of AssessmentCriterionGroup)

End Class
