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

Partial Public Class MeasurementFile
    Public Property Id As Integer
    Public Property MeasurementFileName As String
    Public Property UploadDateTime As Date
    Public Property UploadSuccess As Boolean
    Public Property MonitorId As Integer
    Public Property MonitorLocationId As Integer
    Public Property ContactId As Integer
    Public Property MeasurementFileTypeId As Integer
    Public Property ServerFileName As String
    Public Property FirstMeasurementStartDateTime As Date
    Public Property LastMeasurementStartDateTime As Date
    Public Property LastMeasurementDuration As Double
    Public Property NumberOfMeasurements As Integer


    Public Overridable Property Monitor As Monitor
    Public Overridable Property Contact As Contact
    Public Overridable Property MonitorLocation As MonitorLocation
    Public Overridable Property MeasurementFileType As MeasurementFileType
    Public Overridable Property Measurements As ICollection(Of Measurement) = New HashSet(Of Measurement)

End Class
