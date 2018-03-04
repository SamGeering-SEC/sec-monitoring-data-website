Imports System.Data.Entity.Core

Public Class HomeController

    Inherits ControllerBase

    Public Sub New(MeasurementsDAL As IMeasurementsDAL)

        MyBase.New(MeasurementsDAL)

    End Sub


#Region "Index"

    Public Function Index() As ActionResult

        Dim vm As New HomePageViewModel

        vm.Message = "Welcome to the Southdowns Monitoring Website."

        Dim buttons As New List(Of HomePageButtonViewModel)

        If UAL.CanViewProjectList Then buttons.Add(
            New HomePageButtonViewModel("Projects", "ProjectIndexRoute",
                                        "home-projects-button", "View a list of your Projects")
        )
        If UAL.CanViewMonitorLocationlist Then buttons.Add(
            New HomePageButtonViewModel("Monitor Locations", "MonitorLocationIndexRoute",
                                        "home-monitorlocations-button", "View a list of your Monitor Locations")
        )
        If UAL.CanViewContactList Then buttons.Add(
            New HomePageButtonViewModel("Contacts", "ContactIndexRoute",
                                        "home-contacts-button", "View a list of your Contacts")
        )
        If UAL.CanViewDocumentList Then buttons.Add(
            New HomePageButtonViewModel("Documents", "DocumentIndexRoute",
                                        "home-documents-button", "View a list of Documents")
        )
        If UAL.CanViewCountryList Then buttons.Add(
            New HomePageButtonViewModel("Countries", "CountryIndexRoute",
                                        "home-countries-button", "View a list of Countries")
        )
        If UAL.CanViewMonitorList Then buttons.Add(
            New HomePageButtonViewModel("Monitors", "MonitorIndexRoute",
                                        "home-monitors-button", "View a list of Monitors")
        )
        If UAL.CanViewMonitorDeploymentRecordList Then buttons.Add(
            New HomePageButtonViewModel("Monitor Deployment Records", "MonitorDeploymentRecordIndexRoute",
                                        "home-monitordeploymentrecords-button", "View a list of Monitor Deployment Records")
        )
        If UAL.CanViewCalculationFilterList Then buttons.Add(
            New HomePageButtonViewModel("Calculation Filters", "CalculationFilterIndexRoute",
                                        "home-calculationfilters-button", "View a list of Calculation Filters")
        )
        If UAL.CanViewMeasurementMetricList Then buttons.Add(
            New HomePageButtonViewModel("Measurement Metrics", "MeasurementMetricIndexRoute",
                                        "home-measurementmetrics-button", "View a list of Measurement Metrics")
        )
        If UAL.CanViewOrganisationList Then buttons.Add(
            New HomePageButtonViewModel("Organisations", "OrganisationIndexRoute",
                                        "home-organisations-button", "View a list of Organisations on your Projects")
        )
        If UAL.CanViewMeasurementCommentTypeList Then buttons.Add(
            New HomePageButtonViewModel("Measurement Comment Types", "MeasurementCommentTypeIndexRoute",
                                        "home-measurementcommenttypes-button", "View a list of Measurement Comment Types")
        )
        If UAL.CanViewMeasurementFileList Then buttons.Add(
            New HomePageButtonViewModel("Measurement Files", "MeasurementFileIndexRoute",
                                        "home-measurementfiles-button", "View a list of Measurement Files")
        )
        If UAL.CanViewMeasurementViewList Then buttons.Add(
            New HomePageButtonViewModel("Measurement Views", "MeasurementViewIndexRoute",
                                        "home-measurementviews-button", "View a list of Measurement Views")
        )
        If UAL.CanViewUserList Then buttons.Add(
            New HomePageButtonViewModel("Users", "UserIndexRoute",
                                        "home-users-button", "View the list of Website Users")
        )
        If UAL.CanViewUserAccessLevelList Then buttons.Add(
            New HomePageButtonViewModel("User Access Levels", "UserAccessLevelIndexRoute",
                                        "home-useraccesslevels-button", "View the list of User Access Levels")
        )

        vm.Buttons = buttons
        vm.SystemMessages = getSystemMessages()

        setMessageSettings()

        Return View(vm)

    End Function

    Private Function getSystemMessages() As IEnumerable(Of SystemMessage)

        If UAL.CanViewSystemMessages Then
            Return MeasurementsDAL.GetSystemMessages.ToList()
        Else
            Return New List(Of SystemMessage)
        End If

    End Function

    Public Function ViewSystemMessages() As ActionResult

        setMessageSettings()
        Return PartialView("Index_SystemMessages", getSystemMessages())

    End Function

    Private Sub setMessageSettings()

        ViewData("ShowMessages") = UAL.CanViewSystemMessages
        ViewData("ShowCreateMessageButton") = UAL.CanCreateSystemMessages
        ViewData("ShowEditMessageButtons") = UAL.CanEditSystemMessages
        ViewData("ShowDeleteMessageButtons") = UAL.CanDeleteSystemMessages

    End Sub

    Public Function CreateSystemMessage(messageText As String) As ActionResult

        Dim message As New SystemMessage With {
            .DateTimeCreated = DateTime.Now(),
            .MessageText = messageText
        }
        Try
            MeasurementsDAL.AddSystemMessage(message)
            Return Json(True)
        Catch ex As Exception
            Return Json(False)
        End Try

    End Function

    <HttpPost()>
    Public Function UpdateSystemMessage(messageId As Integer, messageText As String) As ActionResult

        Dim message = MeasurementsDAL.GetSystemMessage(messageId)

        Try
            message.MessageText = messageText
            MeasurementsDAL.UpdateSystemMessage(message)
            Return Json(True)
        Catch ex As Exception
            Return Json(False)
        End Try

    End Function

    <HttpPost()>
    Public Function DeleteSystemMessage(SystemMessageId As Integer) As ActionResult

        Try
            MeasurementsDAL.DeleteSystemMessage(SystemMessageId)
            Return Json(True)
        Catch ex As Exception
            Return Json(False)
        End Try

    End Function

#End Region


End Class
