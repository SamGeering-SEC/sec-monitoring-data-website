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

Partial Public Class MeasurementViewGroup

    Public Property Id As Integer
    Public Property GroupIndex As Integer
    Public Property MainHeader As String
    Public Property SubHeader As String
    Public Property MeasurementViewId As Integer

    Public Overridable Property MeasurementViewSequenceSettings As ICollection(Of MeasurementViewSequenceSetting) = New HashSet(Of MeasurementViewSequenceSetting)
    Public Overridable Property MeasurementView As MeasurementView

End Class