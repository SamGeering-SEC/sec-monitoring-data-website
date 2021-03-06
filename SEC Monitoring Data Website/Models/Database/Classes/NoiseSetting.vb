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
Imports System.ComponentModel.DataAnnotations.Schema

Partial Public Class NoiseSetting

    <DatabaseGenerated(DatabaseGeneratedOption.None)> _
    Public Property Id As Integer
    Public Property MicrophoneSerialNumber As String
    Public Property DynamicRangeLowerLevel As Double
    Public Property DynamicRangeUpperLevel As Double
    Public Property WindScreenCorrection As Double
    Public Property AlarmTriggerLevel As String
    Public Property FrequencyWeighting As String
    Public Property TimeWeighting As String
    Public Property SoundRecording As Boolean

    <Required()> _
    Public Overridable Property MonitorSettings As MonitorSettings

End Class
