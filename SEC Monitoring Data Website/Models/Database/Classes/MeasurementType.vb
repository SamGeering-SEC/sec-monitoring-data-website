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

Partial Public Class MeasurementType
    Public Property Id As Integer
    Public Property MeasurementTypeName As String

    Public Overridable Property MeasurementMetrics As ICollection(Of MeasurementMetric) = New HashSet(Of MeasurementMetric)
    Public Overridable Property Monitors As ICollection(Of Monitor) = New HashSet(Of Monitor)
    Public Overridable Property MeasurementViews As ICollection(Of MeasurementView) = New HashSet(Of MeasurementView)
    Public Overridable Property MonitorLocations As ICollection(Of MonitorLocation) = New HashSet(Of MonitorLocation)
    Public Overridable Property AssessmentCriterionGroups As ICollection(Of AssessmentCriterionGroup) = New HashSet(Of AssessmentCriterionGroup)
    Public Overridable Property MeasurementFileTypes As ICollection(Of MeasurementFileType) = New HashSet(Of MeasurementFileType)

End Class
