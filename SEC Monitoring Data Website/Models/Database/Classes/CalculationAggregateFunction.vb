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

Partial Public Class CalculationAggregateFunction
    Public Property Id As Integer
    Public Property FunctionName As String

    Public Overridable Property CalculationFilters As ICollection(Of CalculationFilter) = New HashSet(Of CalculationFilter)

End Class
