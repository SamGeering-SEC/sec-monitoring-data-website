Imports libSEC.Strings

#Region "Interfaces"

Public Interface IDetailsRouteButton64

    Function getDetailsRouteButton64(Optional ButtonText As String = "Details") As NavigationButtonViewModel

End Interface
Public Interface IEditRouteButton64

    Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel

End Interface
Public Interface IIndexRouteButton64

    Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel

End Interface
Public Interface IDeleteRouteButton64

    Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel

End Interface
Public Interface ICreateRouteButton64

    Function getCreateRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel

End Interface

#End Region

Module RouteButtonHelpers

    Public Function CreateButton64Class() As String

        Return "sitewide-button-64 create-button-64"

    End Function
    Public Function DeleteButton64Class(ObjectName) As String

        Return "sitewide-button-64 delete-button-64 Delete" + ObjectName + "Link"

    End Function
    Public Function EndButton64Class() As String

        Return "sitewide-button-64 end-button-64"

    End Function
    Public Function EditButton64Class() As String

        Return "sitewide-button-64 edit-button-64"

    End Function
    Public Function IndexButton64Class() As String

        Return "sitewide-button-64 index-button-64"

    End Function


    Public Function GetIndexButton64(ObjectName As String, Optional ButtonClass As String = "") As NavigationButtonViewModel

        Dim bClass As String

        If ButtonClass <> "" Then
            bClass = "sitewide-button-64 " + ButtonClass
        Else
            bClass = IndexButton64Class()
        End If

        Return New NavigationButtonViewModel("View all " + ObjectName.Pluralise.SpaceOutPascalCaseText,
                                             ObjectName + "IndexRoute",
                                             Nothing,
                                             bClass)

    End Function
    Public Function GetFilteredIndexButton64(ObjectName As String, FilterObjectName As String, Optional ButtonClass As String = "") As NavigationButtonViewModel

        Dim bClass As String

        If ButtonClass <> "" Then
            bClass = "sitewide-button-64 " + ButtonClass
        Else
            bClass = IndexButton64Class()
        End If

        Return New NavigationButtonViewModel("View " +
                                                FilterObjectName.SpaceOutPascalCaseText + " " +
                                                ObjectName.Pluralise.SpaceOutPascalCaseText,
                                             ObjectName + FilterObjectName + "IndexRoute",
                                             Nothing,
                                             bClass)


    End Function
    Public Function GetCreateButton64(ObjectName As String) As NavigationButtonViewModel

        Return New NavigationButtonViewModel(
            "Add a new " + ObjectName.SpaceOutPascalCaseText,
            ObjectName + "CreateRoute",
            Nothing,
            CreateButton64Class
        )

    End Function
    Public Function GetEditButton64(ObjectName As String, RouteValues As Object) As NavigationButtonViewModel

        Return New NavigationButtonViewModel("Edit " + ObjectName.SpaceOutPascalCaseText,
                                             ObjectName + "EditRoute",
                                             RouteValues,
                                             EditButton64Class)

    End Function

End Module




Partial Public Class CalculationFilter

    Implements IEditRouteButton64, IIndexRouteButton64, IDeleteRouteButton64

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        ButtonText = "Edit Calculation Filter"

        Return New NavigationButtonViewModel(ButtonText,
                     "CalculationFilterEditRoute",
                     New With {.controller = "CalculationFilters",
                               .CalculationFilterRouteName = Me.getRouteName},
                     EditButton64Class())

    End Function
    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("CalculationFilter")

    End Function
    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Calculation Filter",
                                             "CalculationFilterDeleteByIdRoute",
                                             New With {.CalculationFilterId = Me.Id},
                                             DeleteButton64Class("CalculationFilter"))

    End Function

End Class

Partial Public Class Contact

    Implements IIndexRouteButton64, IEditRouteButton64, IDeleteRouteButton64


    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Contact",
                                             "ContactDeleteByIdRoute",
                                             New With {.ContactId = Me.Id},
                                             DeleteButton64Class("Contact"))

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("Contact", New With {.controller = "Contacts",
                                                    .ContactRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("Contact")

    End Function

End Class

Partial Public Class Country


    Implements IIndexRouteButton64, IEditRouteButton64, IDeleteRouteButton64

    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Country",
                                             "CountryDeleteByIdRoute",
                                             New With {.CountryId = Me.Id},
                                             DeleteButton64Class("Country"))

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("Country", New With {.controller = "Countries",
                                                    .CountryRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("Country")

    End Function

End Class

Partial Public Class Document

    Implements IIndexRouteButton64, IDeleteRouteButton64, IEditRouteButton64

    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Document",
                                             "DocumentDeleteByIdRoute",
                                             New With {.DocumentId = Me.Id},
                                             DeleteButton64Class("Document"))

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("Document", New With {.controller = "Documents",
                                                    .DocumentFileName = Me.getFileName,
                                                    .DocumentUploadDate = Me.getUploadDate,
                                                    .DocumentUploadTime = Me.getUploadTime})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("Document")

    End Function

End Class

Partial Public Class DocumentType

    Implements IIndexRouteButton64, IDeleteRouteButton64, IEditRouteButton64

    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Document Type",
                                    "DocumentTypeDeleteByIdRoute",
                                    New With {.DocumentTypeId = Me.Id},
                                    DeleteButton64Class("DocumentType"))

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("DocumentType", New With {.controller = "DocumentTypes",
                                                         .DocumentTypeRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("DocumentType")

    End Function

End Class

Partial Public Class MeasurementCommentType

    Implements IDeleteRouteButton64, IEditRouteButton64, IIndexRouteButton64

    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Comment Type",
                                             "MeasurementCommentTypeDeleteByIdRoute",
                                             New With {.MeasurementCommentTypeId = Me.Id},
                                             DeleteButton64Class("MeasurementCommentType"))

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("MeasurementCommentType",
                               New With {.controller = "MeasurementCommentTypes",
                                         .MeasurementCommentTypeRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("MeasurementCommentType")

    End Function

End Class

Partial Public Class MeasurementFile

    Implements IIndexRouteButton64, IDeleteRouteButton64, IDetailsRouteButton64


    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Measurement File",
                                             "MeasurementFileDeleteByIdRoute",
                                             New With {.MeasurementFileId = Me.Id},
                                             DeleteButton64Class("MeasurementFile"))

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("MeasurementFile")

    End Function

    Public Function getDetailsRouteButton64(Optional ButtonText As String = "Details") As NavigationButtonViewModel Implements IDetailsRouteButton64.getDetailsRouteButton64

        Return New NavigationButtonViewModel(
            ButtonText,
            "MeasurementFileDetailsRoute",
            New With {.MeasurementFileId = Me.Id},
            "sitewide-button-64 measurements-button-64"
        )

    End Function

End Class

Partial Public Class MeasurementMetric


    Implements IDeleteRouteButton64, IEditRouteButton64, IIndexRouteButton64


    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Measurement Metric",
                                             "MeasurementMetricDeleteByIdRoute",
                                             New With {.MeasurementMetricId = Me.Id},
                                             DeleteButton64Class("MeasurementMetric"))

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("MeasurementMetric",
                       New With {.controller = "MeasurementMetrics",
                                 .MeasurementMetricRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("MeasurementMetric")

    End Function

End Class

Partial Public Class MeasurementView

    Implements IDeleteRouteButton64, IEditRouteButton64, IIndexRouteButton64


    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Measurement View",
                                     "MeasurementViewDeleteByIdRoute",
                                     New With {.MeasurementViewId = Me.Id},
                                     DeleteButton64Class("MeasurementView"))

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("MeasurementView",
               New With {.controller = "MeasurementViews",
                         .MeasurementViewRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("MeasurementView")

    End Function

End Class

Partial Public Class Monitor

    Implements IIndexRouteButton64, IDeleteRouteButton64, IEditRouteButton64


    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Monitor",
                                             "MonitorDeleteByIdRoute",
                                             New With {.MonitorId = Me.Id},
                                             DeleteButton64Class("Monitor"))

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("Monitor",
                               New With {.controller = "Monitors",
                                         .MonitorRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("Monitor")

    End Function

End Class

Partial Public Class MonitorDeploymentRecord

    Implements IIndexRouteButton64, IDeleteRouteButton64, IEditRouteButton64


    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel(
            "Delete Monitor Deployment Record",
            "MonitorDeploymentRecordDeleteByIdRoute",
            New With {.MonitorDeploymentRecordId = Me.Id},
            DeleteButton64Class("MonitorDeploymentRecord")
            )

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return New NavigationButtonViewModel(
            "End Deployment",
            "MonitorDeploymentRecordEditRoute",
            New With {.MonitorRouteName = Monitor.MonitorName.ToRouteName},
            EditButton64Class
            )

    End Function

    Public Function getEndRouteButton64(Optional ButtonText As String = "End") As NavigationButtonViewModel

        Return New NavigationButtonViewModel(
            "End Deployment",
            "MonitorDeploymentRecordEndRoute",
            New With {.MonitorRouteName = Monitor.MonitorName.ToRouteName},
            EndButton64Class
        )

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("MonitorDeploymentRecord")

    End Function

End Class

Partial Public Class MonitorLocation

    Implements IDetailsRouteButton64, IEditRouteButton64, IIndexRouteButton64, IDeleteRouteButton64

    Public Function getDetailsRouteButton(Optional ButtonText As String = "Details") As NavigationButtonViewModel Implements IDetailsRouteButton64.getDetailsRouteButton64

        Return New NavigationButtonViewModel(ButtonText,
                        "MonitorLocationDetailsRoute",
                        New With {.ProjectRouteName = Me.Project.getRouteName,
                                  .MonitorLocationRouteName = Me.getRouteName},
                        "sitewide-button-64 monitorlocation-button-64")

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("MonitorLocation",
                               New With {.ProjectRouteName = Me.Project.getRouteName,
                                         .MonitorLocationRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("MonitorLocation")

    End Function

    Public Function getMeasurementsViewRouteButton64(Optional assessmentDate As Nullable(Of Date) = Nothing) As NavigationButtonViewModel

        If assessmentDate Is Nothing Then assessmentDate = Date.Today

        Return New NavigationButtonViewModel("View Measurements",
                                             "MeasurementViewRoute",
                                             New With {.ProjectRouteName = Me.Project.getRouteName,
                                                       .MonitorLocationRouteName = Me.getRouteName,
                                                       .ViewRouteName = "Default",
                                                       .ViewDuration = "Week",
                                                       .strStartDate = Format(assessmentDate, "dd-MMM-yyyy")},
                                             "sitewide-button-64 measurements-button-64")

    End Function

    Public Function getMeasurementCommentIndexRouteButton64() As NavigationButtonViewModel

        Return New NavigationButtonViewModel("View Comments",
                                             "MeasurementCommentIndexRoute",
                                             New With {.ProjectRouteName = Me.Project.getRouteName,
                                                       .MonitorLocationRouteName = Me.getRouteName},
                                             "sitewide-button-64 comments-button-64")

    End Function

    Public Function getAssessmentViewRouteButton64(Optional assessmentDate As Nullable(Of Date) = Nothing) As NavigationButtonViewModel

        If assessmentDate Is Nothing Then
            Return New NavigationButtonViewModel(
                "View Assessments",
                "AssessmentViewRoute",
                New With {.ProjectRouteName = Me.Project.getRouteName,
                        .MonitorLocationRouteName = Me.getRouteName},
                "sitewide-button-64 assessments-button-64"
                )
        Else
            Return New NavigationButtonViewModel(
                "View Assessments",
                "AssessmentViewRoute",
                New With {.ProjectRouteName = Me.Project.getRouteName,
                        .MonitorLocationRouteName = Me.getRouteName,
                        .strAssessmentDate = Format(assessmentDate, "yyyy-MM-dd")},
                "sitewide-button-64 assessments-button-64"
                )
        End If

    End Function

    Public Function getCurrentMonitorRouteButton64() As NavigationButtonViewModel

        Return New NavigationButtonViewModel("View Assessments",
                                             "AssessmentViewRoute",
                                             New With {.ProjectRouteName = Me.Project.getRouteName,
                                                       .MonitorLocationRouteName = Me.getRouteName},
                                             "sitewide-button-64 assessments-button-64")

    End Function

    Public Function getMeasurementUploadRouteButton64() As NavigationButtonViewModel

        Return New NavigationButtonViewModel("Upload Measurements",
                                             "MeasurementUploadRoute",
                                             New With {.ProjectRouteName = Me.Project.getRouteName,
                                                       .MonitorLocationRouteName = Me.getRouteName},
                                             "sitewide-button-64 upload-button-64")

    End Function

    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Monitor Location",
                                             "MonitorLocationDeleteByIdRoute",
                                             New With {.MonitorLocationId = Me.Id},
                                             DeleteButton64Class("MonitorLocation"))

    End Function

End Class

Partial Public Class Organisation

    Implements IDeleteRouteButton64, IEditRouteButton64, IIndexRouteButton64

    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Organisation",
                                             "OrganisationDeleteByIdRoute",
                                             New With {.OrganisationId = Me.Id},
                                             DeleteButton64Class("Organisation"))

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("Organisation",
                               New With {.controller = "Organisations",
                                         .OrganisationRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("Organisation")

    End Function

End Class

Partial Public Class OrganisationType

    Implements IDeleteRouteButton64, IEditRouteButton64, IIndexRouteButton64


    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Organisation Type",
                                             "OrganisationTypeDeleteByIdRoute",
                                             New With {.OrganisationTypeId = Me.Id},
                                             DeleteButton64Class("OrganisationType"))

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("OrganisationType",
                       New With {.controller = "OrganisationTypes",
                                 .OrganisationTypeRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("OrganisationType")

    End Function

End Class

Partial Public Class Project


    Implements IDetailsRouteButton64, IDeleteRouteButton64, IEditRouteButton64, IIndexRouteButton64

    Public Function getDetailsRouteButton64(Optional ButtonText As String = "Details") As NavigationButtonViewModel Implements IDetailsRouteButton64.getDetailsRouteButton64

        Return New NavigationButtonViewModel(ButtonText,
                        "ProjectDetailsRoute",
                        New With {.ProjectRouteName = Me.getRouteName},
                        "sitewide-button-64 project-button-64")

    End Function

    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel("Delete Project",
                                             "ProjectDeleteByIdRoute",
                                             New With {.ProjectId = Me.Id},
                                             DeleteButton64Class("Project"))

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("Project",
               New With {.controller = "Projects",
                         .ProjectRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("Project")

    End Function

    
End Class

Partial Public Class User

    Implements IIndexRouteButton64, IEditRouteButton64, IDeleteRouteButton64

    Public Function getResetPasswordButton64() As NavigationButtonViewModel

        Return New NavigationButtonViewModel(
            "Initiate Password Reset",
            "UserResetPasswordByIdRoute",
            New With {.UserId = Me.Id},
            "sitewide-button-64 reset-button-64 ResetUserPasswordLink"
        )

    End Function

    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel(
            "Delete User",
            "UserDeleteByIdRoute",
            New With {.UserId = Me.Id},
            DeleteButton64Class("User")
        )

    End Function

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        Return GetEditButton64("User",
                               New With {.controller = "Users",
                                         .UserRouteName = Me.getRouteName})

    End Function

    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("User")

    End Function


End Class

Partial Public Class UserAccessLevel

    Implements IEditRouteButton64, IIndexRouteButton64, IDeleteRouteButton64

    Public Function getEditRouteButton64(Optional ButtonText As String = "Edit") As NavigationButtonViewModel Implements IEditRouteButton64.getEditRouteButton64

        ButtonText = "Edit User Access Level"

        Return New NavigationButtonViewModel(
            ButtonText,
            "UserAccessLevelEditRoute",
            New With {.controller = "UserAccessLevels",
                     .UserAccessLevelRouteName = Me.getRouteName},
            EditButton64Class()
        )

    End Function
    Public Function getIndexRouteButton64(Optional ButtonText As String = "Index") As NavigationButtonViewModel Implements IIndexRouteButton64.getIndexRouteButton64

        Return GetIndexButton64("UserAccessLevel")

    End Function
    Public Function getDeleteRouteButton64(Optional ButtonText As String = "Delete") As NavigationButtonViewModel Implements IDeleteRouteButton64.getDeleteRouteButton64

        Return New NavigationButtonViewModel(
            "Delete User Access Level",
            "UserAccessLevelDeleteByIdRoute",
            New With {.UserAccessLevelId = Me.Id},
            DeleteButton64Class("UserAccessLevel")
        )

    End Function


End Class