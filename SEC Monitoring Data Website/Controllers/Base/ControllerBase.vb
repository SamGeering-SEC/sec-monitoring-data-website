Public Class ControllerBase

    Inherits System.Web.Mvc.Controller

    Protected MeasurementsDAL As IMeasurementsDAL

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        Me.MeasurementsDAL = MeasurementsDAL


    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)

        MeasurementsDAL.Dispose()
        MyBase.Dispose(disposing)

    End Sub

    Protected Function CurrentUser() As User

        Return MeasurementsDAL.GetUser(User.Identity.Name)

    End Function

    Protected Function CurrentContact() As Contact

        Return CurrentUser.Contact

    End Function

    Protected Function UAL() As UserAccessLevel

        Return CurrentUser.UserAccessLevel

    End Function

    Protected Function AllowedProjects() As List(Of Project)

        Dim contact = CurrentContact()
        Dim accessLevel = UAL()
        Dim projectPermission = MeasurementsDAL.GetProjectPermission(accessLevel)

        Select Case projectPermission.PermissionName
            Case "AllProjects"
                Return MeasurementsDAL.GetProjects.ToList()
            Case "MemberOrganisationProjects"
                Return contact.Organisation.Projects.ToList()
            Case "ClientOrganisationsProjects"
                Return contact.Organisation.ProjectsAsClient.ToList()
            Case "OwnProjects"
                Return contact.Projects.ToList()
        End Select

        Return New List(Of Project) ' Shouldn't happen

    End Function
    Protected Function AllowedProjectIds() As List(Of Integer)

        Return AllowedProjects.Select(Function(p) p.Id).ToList

    End Function
    Protected Function AllowedMonitorLocations() As List(Of MonitorLocation)

        Return AllowedProjects.SelectMany(Function(p) p.MonitorLocations).ToList

    End Function
    Protected Function AllowedMonitorLocationIds() As List(Of Integer)

        Return AllowedMonitorLocations.Select(Function(ml) ml.Id).ToList

    End Function

    Protected Function CanAccessProject(ProjectId As Integer) As Boolean

        Return AllowedProjects().Select(Function(p) p.Id).ToList.Contains(ProjectId)

    End Function
    Protected Function AllowedMonitors() As List(Of Monitor)

        Dim monitors As New List(Of Monitor)
        For Each MonitorLocation In AllowedMonitorLocations()
            If MonitorLocation.CurrentMonitor IsNot Nothing Then
                monitors.Add(MonitorLocation.CurrentMonitor)
            End If
        Next

        Return monitors

    End Function
    Protected Function CanAccessProject(ProjectRouteName As String) As Boolean

        Return AllowedProjects().Select(Function(p) p.getRouteName()).ToList.Contains(ProjectRouteName)

    End Function

End Class
