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

Partial Public Class VariedWeeklyWorkingHours
    Public Property Id As Integer
    Public Property ProjectId As Integer
    Public Property StartDate As Date
    Public Property EndDate As Date

    Public Overridable Property Project As Project
    Public Overridable Property AvailableMeasurementViews As ICollection(Of MeasurementView) = New HashSet(Of MeasurementView)
    Public Overridable Property VariedDailyWorkingHours As ICollection(Of VariedDailyWorkingHours) = New HashSet(Of VariedDailyWorkingHours)

End Class
