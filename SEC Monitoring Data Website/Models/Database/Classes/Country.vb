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

Partial Public Class Country
    Public Property Id As Integer
    Public Property CountryName As String

    Public Overridable Property PublicHolidays As ICollection(Of PublicHoliday) = New HashSet(Of PublicHoliday)
    Public Overridable Property Projects As ICollection(Of Project) = New HashSet(Of Project)

End Class
