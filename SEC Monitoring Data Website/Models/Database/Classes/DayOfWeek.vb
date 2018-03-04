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
Imports System.ComponentModel.DataAnnotations.Schema

<Table("DaysOfWeek")> _
Partial Public Class DayOfWeek
    Public Property Id As Integer
    Public Property DayName As String

    Public Overridable Property CalculationFilters As ICollection(Of CalculationFilter) = New HashSet(Of CalculationFilter)
    Public Overridable Property DailyWorkingHours As ICollection(Of StandardDailyWorkingHours) = New HashSet(Of StandardDailyWorkingHours)
    Public Overridable Property VariedDailyWorkingHours As ICollection(Of VariedDailyWorkingHours) = New HashSet(Of VariedDailyWorkingHours)

End Class