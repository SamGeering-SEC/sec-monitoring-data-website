Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations


Partial Public Class Project
    Public Property Id As Integer
    Public Property FullName As String
    Public Property ShortName As String
    Public Property ProjectNumber As String
    Public Property ProjectGeoCoordsId As Integer
    Public Property MapLink As String
    Public Property ClientOrganisationId As Integer
    Public Property CountryId As Integer

    <Required()> _
    Public Overridable Property ClientOrganisation As Organisation
    Public Overridable Property Organisations As ICollection(Of Organisation) = New HashSet(Of Organisation)
    Public Overridable Property Contacts As ICollection(Of Contact) = New HashSet(Of Contact)
    Public Overridable Property MonitorLocations As ICollection(Of MonitorLocation) = New HashSet(Of MonitorLocation)
    Public Overridable Property Measurements As ICollection(Of Measurement) = New HashSet(Of Measurement)
    <Required()> _
    Public Overridable Property Country As Country
    <Required()> _
    Public Overridable Property ProjectGeoCoords As ProjectGeoCoords
    Public Overridable Property VariedWeeklyWorkingHours As ICollection(Of VariedWeeklyWorkingHours) = New HashSet(Of VariedWeeklyWorkingHours)
    Public Overridable Property AssessmentCriteria As ICollection(Of AssessmentCriterionGroup) = New HashSet(Of AssessmentCriterionGroup)
    Public Overridable Property StandardWeeklyWorkingHours As StandardWeeklyWorkingHours
    Public Overridable Property Documents As ICollection(Of Document) = New HashSet(Of Document)
    Public Overridable Property MeasurementViews As ICollection(Of MeasurementView) = New HashSet(Of MeasurementView)

End Class
