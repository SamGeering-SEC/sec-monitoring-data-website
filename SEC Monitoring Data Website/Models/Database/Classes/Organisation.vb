Imports System
Imports System.Collections.Generic

Partial Public Class Organisation
    Public Property Id As Integer
    Public Property FullName As String
    Public Property ShortName As String
    Public Property Address As String
    Public Property OrganisationTypeId As Integer

    Public Overridable Property OrganisationType As OrganisationType
    Public Overridable Property Contacts As ICollection(Of Contact) = New HashSet(Of Contact)
    Public Overridable Property ProjectsAsClient As ICollection(Of Project) = New HashSet(Of Project)
    Public Overridable Property Projects As ICollection(Of Project) = New HashSet(Of Project)
    Public Overridable Property AuthoredDocuments As ICollection(Of Document) = New HashSet(Of Document)
    Public Overridable Property OwnedMonitors As ICollection(Of Monitor) = New HashSet(Of Monitor)

End Class
