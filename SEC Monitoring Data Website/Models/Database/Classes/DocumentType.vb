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

Partial Public Class DocumentType
    Public Property Id As Integer
    Public Property DocumentTypeName As String

    Public Overridable Property Documents As ICollection(Of Document) = New HashSet(Of Document)
    'Public Overridable Property AllowedUserAccessLevels As ICollection(Of UserAccessLevel) = New HashSet(Of UserAccessLevel)

End Class
