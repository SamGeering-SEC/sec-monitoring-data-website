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

Partial Public Class MeasurementFileType
    Public Property Id As Integer
    Public Property FileTypeName As String
    Public Property MeasurementTypeId As Integer

    Public Overridable Property MeasurementType As MeasurementType
    Public Overridable Property MeasurementFiles As ICollection(Of MeasurementFile) = New HashSet(Of MeasurementFile)

End Class
