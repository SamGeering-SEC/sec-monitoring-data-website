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


Partial Public Class User
    Public Property Id As Integer
    Public Property UserName As String
    Public Property Password As String
    Public Property UserAccessLevelId As Integer
    Public Property UserAccessLevel As UserAccessLevel
    Public Property Salt As String
    Public Property IsLocked As Boolean
    Public Property ConsecutiveUnsuccessfulLogins As Integer
    Public Property ReceivesLockNotifications As Boolean

    <Required()> _
    Public Overridable Property Contact As Contact

End Class