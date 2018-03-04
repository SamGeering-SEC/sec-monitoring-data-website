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
Imports System.ComponentModel.DataAnnotations


Partial Public Class MonitorLocation
    Public Property Id As Integer
    Public Property MonitorLocationName As String
    Public Property MonitorLocationGeoCoordsId As Integer
    Public Property HeightAboveGround As Double
    Public Property IsAFacadeLocation As Boolean
    Public Property ProjectId As Integer
    Public Property MeasurementTypeId As Integer

    Public Overridable Property Project As Project
    Public Overridable Property CurrentMonitor As Monitor
    Public Overridable Property DeploymentRecords As ICollection(Of MonitorDeploymentRecord) = New HashSet(Of MonitorDeploymentRecord)
    Public Overridable Property Measurements As ICollection(Of Measurement) = New HashSet(Of Measurement)
    Public Overridable Property MonitorLocationGeoCoords As MonitorLocationGeoCoords
    Public Overridable Property AssessmentCriteria As ICollection(Of AssessmentCriterion) = New HashSet(Of AssessmentCriterion)
    Public Overridable Property Documents As ICollection(Of Document) = New HashSet(Of Document)
    Public Overridable Property MeasurementFiles As ICollection(Of MeasurementFile) = New HashSet(Of MeasurementFile)
    Public Overridable Property MeasurementType As MeasurementType
    Public Overridable Property MeasurementComments As ICollection(Of MeasurementComment) = New HashSet(Of MeasurementComment)

End Class