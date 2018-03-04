Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.Mvc


Public Class RouteConfig
    Public Shared Sub RegisterRoutes(ByVal routes As RouteCollection)

        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")

        ' Home
        routes.MapRoute(name:="HomeIndexRoute", url:="", defaults:=New With {.controller = "Home", .action = "Index"})
        routes.MapRoute(name:="SystemMessageEditRoute", url:="Home/SystemMessages/Edit", defaults:=New With {.controller = "Home", .action = "UpdateSystemMessage"})
        routes.MapRoute(name:="SystemMessageIndexRoute", url:="Home/SystemMessages/Index", defaults:=New With {.controller = "Home", .action = "ViewSystemMessages"})
        routes.MapRoute(name:="SystemMessageCreateRoute", url:="Home/SystemMessages/Create", defaults:=New With {.controller = "Home", .action = "CreateSystemMessage"})

        ' Log In
        routes.MapRoute(name:="AccountLoginRoute", url:="Login/Index", defaults:=New With {.controller = "LogIn", .action = "Index"})
        routes.MapRoute(name:="AccountLogOutRoute", url:="Account/LogOut", defaults:=New With {.controller = "LogIn", .action = "LogOut"})
        routes.MapRoute(name:="AccountChangePasswordRoute", url:="Account/Change-Password", defaults:=New With {.controller = "LogIn", .action = "ChangePassword"})

        ' Delete By Id
        routes.MapRoute(name:="CalculationFilterDeleteByIdRoute", url:="Calculation-Filter/Delete/{CalculationFilterId}", defaults:=New With {.controller = "CalculationFilters", .action = "DeleteCalculationFilter"})
        routes.MapRoute(name:="ContactDeleteByIdRoute", url:="Contact/Delete/{ContactId}", defaults:=New With {.controller = "Contacts", .action = "DeleteContact"})
        routes.MapRoute(name:="CountryDeleteByIdRoute", url:="Country/Delete/{CountryId}", defaults:=New With {.controller = "Countries", .action = "DeleteCountry"})
        routes.MapRoute(name:="DocumentDeleteByIdRoute", url:="Document/Delete/{DocumentId}", defaults:=New With {.controller = "Documents", .action = "DeleteDocument"})
        routes.MapRoute(name:="DocumentTypeDeleteByIdRoute", url:="Document-Type/Delete/{DocumentTypeId}", defaults:=New With {.controller = "DocumentTypes", .action = "DeleteDocumentType"})
        routes.MapRoute(name:="MeasurementDeleteByIdRoute", url:="Measurement/Delete/{MeasurementId}", defaults:=New With {.controller = "MeasurementFiles", .action = "DeleteMeasurement"})
        routes.MapRoute(name:="MeasurementCommentTypeDeleteByIdRoute", url:="Measurement-Comment-Type/Delete/{MeasurementCommentTypeId}", defaults:=New With {.controller = "MeasurementCommentTypes", .action = "DeleteMeasurementCommentType"})
        routes.MapRoute(name:="MeasurementFileDeleteByIdRoute", url:="Measurement-File/Delete/{MeasurementFileId}", defaults:=New With {.controller = "MeasurementFiles", .action = "DeleteMeasurementFile"})
        routes.MapRoute(name:="MeasurementMetricDeleteByIdRoute", url:="Measurement-Metric/Delete/{MeasurementMetricId}", defaults:=New With {.controller = "MeasurementMetrics", .action = "DeleteMeasurementMetric"})
        routes.MapRoute(name:="MeasurementViewDeleteByIdRoute", url:="Measurement-View/Delete/{MeasurementViewId}", defaults:=New With {.controller = "MeasurementViews", .action = "DeleteMeasurementView"})
        routes.MapRoute(name:="MonitorLocationDeleteByIdRoute", url:="Monitor-Location/Delete/{MonitorLocationId}", defaults:=New With {.controller = "MonitorLocations", .action = "DeleteMonitorLocation"})
        routes.MapRoute(name:="MonitorDeleteByIdRoute", url:="Monitor/Delete/{MonitorId}", defaults:=New With {.controller = "Monitors", .action = "DeleteMonitor"})
        routes.MapRoute(name:="MonitorDeploymentRecordDeleteByIdRoute", url:="MonitorDeploymentRecord/Delete/{MonitorDeploymentRecordId}", defaults:=New With {.controller = "MonitorDeploymentRecords", .action = "DeleteMonitorDeploymentRecord"})
        routes.MapRoute(name:="OrganisationDeleteByIdRoute", url:="Organisation/Delete/{OrganisationId}", defaults:=New With {.controller = "Organisations", .action = "DeleteOrganisation"})
        routes.MapRoute(name:="OrganisationTypeDeleteByIdRoute", url:="Organisation-Type/Delete/{OrganisationTypeId}", defaults:=New With {.controller = "OrganisationTypes", .action = "DeleteOrganisationType"})
        routes.MapRoute(name:="ProjectDeleteByIdRoute", url:="Project/Delete/{ProjectId}", defaults:=New With {.controller = "Projects", .action = "DeleteProject"})
        routes.MapRoute(name:="SystemMessageDeleteByIdRoute", url:="Home/SystemMessages/Delete/{SystemMessageId}", defaults:=New With {.controller = "Home", .action = "DeleteSystemMessage"})
        routes.MapRoute(name:="UserDeleteByIdRoute", url:="User/Delete/{UserId}", defaults:=New With {.controller = "Users", .action = "DeleteUser"})
        routes.MapRoute(name:="UserResetPasswordByIdRoute", url:="User/ResetPassword/{UserId}", defaults:=New With {.controller = "Users", .action = "ResetPassword"})
        routes.MapRoute(name:="UserAccessLevelDeleteByIdRoute", url:="User-Access-Level/Delete/{UserAccessLevelId}", defaults:=New With {.controller = "UserAccessLevels", .action = "DeleteUserAccessLevel"})

        ' Update Index Tables
        routes.MapRoute(name:="CalculationFilterUpdateIndexTableRoute", url:="Calculation-Filters/UpdateIndexTable", defaults:=New With {.controller = "CalculationFilters", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="ContactUpdateIndexTableRoute", url:="Contacts/UpdateIndexTable", defaults:=New With {.controller = "Contacts", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="CountryUpdateIndexTableRoute", url:="Countries/UpdateIndexTable", defaults:=New With {.controller = "Countries", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="DocumentUpdateIndexTableRoute", url:="Documents/UpdateIndexTable", defaults:=New With {.controller = "Documents", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="DocumentTypeUpdateIndexTableRoute", url:="Document-Types/UpdateIndexTable", defaults:=New With {.controller = "DocumentTypes", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="MeasurementCommentTypeUpdateIndexTableRoute", url:="Measurement-Comment-Types/UpdateIndexTable", defaults:=New With {.controller = "MeasurementCommentTypes", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="MeasurementFileUpdateIndexTableRoute", url:="Measurement-Files/UpdateIndexTable", defaults:=New With {.controller = "MeasurementFiles", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="MeasurementFileUpdateDetailsTableRoute", url:="Measurement-Files/UpdateDetailsTable", defaults:=New With {.controller = "MeasurementFiles", .action = "UpdateDetailsTable"})
        routes.MapRoute(name:="MeasurementMetricUpdateIndexTableRoute", url:="Measurement-Metrics/UpdateIndexTable", defaults:=New With {.controller = "MeasurementMetrics", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="MeasurementViewUpdateIndexTableRoute", url:="Measurement-Views/UpdateIndexTable", defaults:=New With {.controller = "MeasurementViews", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="MonitorDeploymentRecordUpdateIndexTableRoute", url:="MonitorDeploymentRecords/UpdateIndexTable", defaults:=New With {.controller = "MonitorDeploymentRecords", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="MonitorLocationEditUpdateAssessmentCriteriaRoute", url:="Monitor-Location/Edit/Assessment-Criteria/", defaults:=New With {.controller = "MonitorLocations", .action = "UpdateEditAssessmentCriteria"})
        routes.MapRoute(name:="MonitorLocationUpdateIndexTableRoute", url:="Monitor-Locations/UpdateIndexTable", defaults:=New With {.controller = "MonitorLocations", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="MonitorUpdateIndexTableRoute", url:="Monitors/UpdateIndexTable", defaults:=New With {.controller = "Monitors", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="OrganisationUpdateIndexTableRoute", url:="Organisations/UpdateIndexTable", defaults:=New With {.controller = "Organisations", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="OrganisationTypeUpdateIndexTableRoute", url:="Organisation-Types/UpdateIndexTable", defaults:=New With {.controller = "OrganisationTypes", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="ProjectUpdateIndexTableRoute", url:="Projects/UpdateIndexTable", defaults:=New With {.controller = "Projects", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="UserUpdateIndexTableRoute", url:="Users/UpdateIndexTable", defaults:=New With {.controller = "Users", .action = "UpdateIndexTable"})
        routes.MapRoute(name:="UserAccessLevelUpdateIndexTableRoute", url:="User-Access-Levels/UpdateIndexTable", defaults:=New With {.controller = "UserAccessLevels", .action = "UpdateIndexTable"})

        ' Add and Remove Associations
        routes.MapRoute(name:="OrganisationAddProjectRoute", url:="AddOrganisationProject/{OrganisationId}/{ProjectId}", defaults:=New With {.controller = "Organisations", .action = "AddProject"})
        routes.MapRoute(name:="OrganisationRemoveProjectRoute", url:="RemoveOrganisationProject/{OrganisationId}/{ProjectId}", defaults:=New With {.controller = "Organisations", .action = "RemoveProject"})


        ' Calculation Filters
        routes.MapRoute(name:="CalculationFilterIndexRoute", url:="Calculation-Filters", defaults:=New With {.controller = "CalculationFilters", .action = "Index"})
        routes.MapRoute(name:="CalculationFilterCreateRoute", url:="Calculation-Filters/Create", defaults:=New With {.controller = "CalculationFilters", .action = "Create"})
        routes.MapRoute(name:="CalculationFilterDetailsRoute", url:="Calculation-Filters/{CalculationFilterRouteName}", defaults:=New With {.controller = "CalculationFilters", .action = "Details"})
        routes.MapRoute(name:="CalculationFilterEditRoute", url:="Calculation-Filters/{CalculationFilterRouteName}/Edit", defaults:=New With {.controller = "CalculationFilters", .action = "Edit"})
        routes.MapRoute(name:="CalculationFilterDeleteRoute", url:="Calculation-Filters/{CalculationFilterRouteName}/Delete", defaults:=New With {.controller = "CalculationFilters", .action = "Delete"})
        routes.MapRoute(name:="CalculationFilterAddApplicableDayOfWeekRoute", url:="Calculation-Filters/{CalculationFilterRouteName}/AddApplicableDayOfWeek/ApplicableDayOfWeekId", defaults:=New With {.controller = "CalculationFilters", .action = "AddApplicableDayOfWeek"})
        routes.MapRoute(name:="CalculationFilterRemoveApplicableDayOfWeekRoute", url:="Calculation-Filters/{CalculationFilterRouteName}/RemoveApplicableDayOfWeek/ApplicableDayOfWeekId", defaults:=New With {.controller = "CalculationFilters", .action = "RemoveApplicableDayOfWeek"})

        ' Contacts
        routes.MapRoute(name:="ContactIndexRoute", url:="Contacts", defaults:=New With {.controller = "Contacts", .action = "Index"})
        routes.MapRoute(name:="ContactCreateRoute", url:="Contacts/Create", defaults:=New With {.controller = "Contacts", .action = "Create"})
        routes.MapRoute(name:="ContactDetailsRoute", url:="Contacts/{ContactRouteName}", defaults:=New With {.controller = "Contacts", .action = "Details"})
        routes.MapRoute(name:="ContactEditRoute", url:="Contacts/{ContactRouteName}/Edit", defaults:=New With {.controller = "Contacts", .action = "Edit"})
        routes.MapRoute(name:="ContactDeleteRoute", url:="Contacts/{ContactRouteName}/Delete", defaults:=New With {.controller = "Contacts", .action = "Delete"})
        routes.MapRoute(name:="ContactAddProjectRoute", url:="Contacts/{ContactRouteName}/AddProject/ProjectId", defaults:=New With {.controller = "Contacts", .action = "AddProject"})
        routes.MapRoute(name:="ContactRemoveProjectRoute", url:="Contacts/{ContactRouteName}/RemoveProject/ProjectId", defaults:=New With {.controller = "Contacts", .action = "RemoveProject"})
        routes.MapRoute(name:="ContactAddExcludedDocumentRoute", url:="Contacts/{ContactRouteName}/AddExcludedDocument/ExcludedDocumentId", defaults:=New With {.controller = "Contacts", .action = "AddExcludedDocument"})
        routes.MapRoute(name:="ContactRemoveExcludedDocumentRoute", url:="Contacts/{ContactRouteName}/RemoveExcludedDocument/ExcludedDocumentId", defaults:=New With {.controller = "Contacts", .action = "RemoveExcludedDocument"})

        ' Countries
        routes.MapRoute(name:="CountryIndexRoute", url:="Countries", defaults:=New With {.controller = "Countries", .action = "Index"})
        routes.MapRoute(name:="CountryCreateRoute", url:="Countries/Create", defaults:=New With {.controller = "Countries", .action = "Create"})
        routes.MapRoute(name:="CountryCreatePublicHolidayRoute", url:="Countries/{CountryRouteName}/Create-Public-Holiday", defaults:=New With {.controller = "PublicHolidays", .action = "Create"})
        routes.MapRoute(name:="CountryDetailsRoute", url:="Countries/{CountryRouteName}", defaults:=New With {.controller = "Countries", .action = "Details"})
        routes.MapRoute(name:="CountryEditRoute", url:="Countries/{CountryRouteName}/Edit", defaults:=New With {.controller = "Countries", .action = "Edit"})
        routes.MapRoute(name:="CountryDeleteRoute", url:="Countries/{CountryRouteName}/Delete", defaults:=New With {.controller = "Countries", .action = "Delete"})
        routes.MapRoute(name:="CountryDeletePublicHolidayRoute", url:="Countries/{CountryRouteName}/Delete-Public-Holiday/{PublicHolidayId}", defaults:=New With {.controller = "Countries", .action = "DeletePublicHoliday"})

        ' Documents
        routes.MapRoute(name:="DocumentIndexRoute", url:="Documents", defaults:=New With {.controller = "Documents", .action = "Index"})
        routes.MapRoute(name:="DocumentProjectIndexRoute", url:="Documents/Project/{ProjectShortName}", defaults:=New With {.controller = "Documents", .action = "ProjectIndex"})
        routes.MapRoute(name:="DocumentCreateRoute", url:="Documents/Create/", defaults:=New With {.controller = "Documents", .action = "Create", .MonitorLocationId = UrlParameter.Optional, .DocumentTypeId = UrlParameter.Optional})
        routes.MapRoute(name:="MonitorLocationDocumentCreateRoute", url:="Documents/CreateForMonitorLocation/{MonitorLocationId}/{DocumentTypeName}", defaults:=New With {.controller = "Documents", .action = "Create", .MonitorLocationId = UrlParameter.Optional, .DocumentTypeId = UrlParameter.Optional})
        routes.MapRoute(name:="DocumentDownloadRoute", url:="Documents/Download/{DocumentId}", defaults:=New With {.controller = "Documents", .action = "Download"})
        routes.MapRoute(name:="DocumentDetailsRoute", url:="Documents/Details/{DocumentFileName}/{DocumentUploadDate}/{DocumentUploadTime}", defaults:=New With {.controller = "Documents", .action = "Details"})
        routes.MapRoute(name:="DocumentEditRoute", url:="Documents/Edit/{DocumentFileName}/{DocumentUploadDate}/{DocumentUploadTime}", defaults:=New With {.controller = "Documents", .action = "Edit"})
        routes.MapRoute(name:="DocumentAddProjectRoute", url:="Documents/AddProject/{DocumentId}/{ProjectId}", defaults:=New With {.controller = "Documents", .action = "AddProject"})
        routes.MapRoute(name:="DocumentRemoveProjectRoute", url:="Documents/RemoveProject/{DocumentId}/{ProjectId}", defaults:=New With {.controller = "Documents", .action = "RemoveProject"})
        routes.MapRoute(name:="DocumentAddMonitorRoute", url:="Documents/AddMonitor/{DocumentId}/{MonitorId}", defaults:=New With {.controller = "Documents", .action = "AddMonitor"})
        routes.MapRoute(name:="DocumentRemoveMonitorRoute", url:="Documents/RemoveMonitor/{DocumentId}/{MonitorId}", defaults:=New With {.controller = "Documents", .action = "RemoveMonitor"})
        routes.MapRoute(name:="DocumentAddMonitorLocationRoute", url:="Documents/AddMonitorLocation/{DocumentId}/{MonitorLocationId}", defaults:=New With {.controller = "Documents", .action = "AddMonitorLocation"})
        routes.MapRoute(name:="DocumentRemoveMonitorLocationRoute", url:="Documents/RemoveMonitorLocation/{DocumentId}/{MonitorLocationId}", defaults:=New With {.controller = "Documents", .action = "RemoveMonitorLocation"})
        routes.MapRoute(name:="DocumentAddExcludedContactRoute", url:="Documents/AddExcludedContact/{DocumentId}/{ExcludedContactId}", defaults:=New With {.controller = "Documents", .action = "AddExcludedContact"})
        routes.MapRoute(name:="DocumentRemoveExcludedContactRoute", url:="Documents/RemoveExcludedContact/{DocumentId}/{ExcludedContactId}", defaults:=New With {.controller = "Documents", .action = "RemoveExcludedContact"})

        ' Document Types
        routes.MapRoute(name:="DocumentTypeIndexRoute", url:="Document-Types", defaults:=New With {.controller = "DocumentTypes", .action = "Index"})
        routes.MapRoute(name:="DocumentTypeCreateRoute", url:="Document-Types/Create", defaults:=New With {.controller = "DocumentTypes", .action = "Create"})
        routes.MapRoute(name:="DocumentTypeDetailsRoute", url:="Document-Types/{DocumentTypeRouteName}", defaults:=New With {.controller = "DocumentTypes", .action = "Details"})
        routes.MapRoute(name:="DocumentTypeEditRoute", url:="Document-Types/{DocumentTypeRouteName}/Edit", defaults:=New With {.controller = "DocumentTypes", .action = "Edit"})
        routes.MapRoute(name:="DocumentTypeDeleteRoute", url:="Document-Types/{DocumentTypeRouteName}/Delete", defaults:=New With {.controller = "DocumentTypes", .action = "Delete"})
        routes.MapRoute(name:="DocumentTypeAddChildDocumentTypeRoute", url:="Document-Types/{DocumentTypeRouteName}/AddChildDocumentType/{ChildDocumentTypeId}", defaults:=New With {.controller = "DocumentTypes", .action = "AddChildDocumentType"})
        routes.MapRoute(name:="DocumentTypeRemoveChildDocumentTypeRoute", url:="Document-Types/{DocumentTypeRouteName}/RemoveChildDocumentType/{ChildDocumentTypeId}", defaults:=New With {.controller = "DocumentTypes", .action = "RemoveChildDocumentType"})
        routes.MapRoute(name:="DocumentTypeAddParentDocumentTypeRoute", url:="Document-Types/{DocumentTypeRouteName}/AddParentDocumentType/{ParentDocumentTypeId}", defaults:=New With {.controller = "DocumentTypes", .action = "AddParentDocumentType"})
        routes.MapRoute(name:="DocumentTypeRemoveParentDocumentTypeRoute", url:="Document-Types/{DocumentTypeRouteName}/RemoveParentDocumentType/{ParentDocumentTypeId}", defaults:=New With {.controller = "DocumentTypes", .action = "RemoveParentDocumentType"})

        ' Measurement Comment Types
        routes.MapRoute(name:="MeasurementCommentTypeIndexRoute", url:="Measurement-Comment-Types", defaults:=New With {.controller = "MeasurementCommentTypes", .action = "Index"})
        routes.MapRoute(name:="MeasurementCommentTypeCreateRoute", url:="Measurement-Comment-Types/Create", defaults:=New With {.controller = "MeasurementCommentTypes", .action = "Create"})
        routes.MapRoute(name:="MeasurementCommentTypeDetailsRoute", url:="Measurement-Comment-Types/{MeasurementCommentTypeRouteName}", defaults:=New With {.controller = "MeasurementCommentTypes", .action = "Details"})
        routes.MapRoute(name:="MeasurementCommentTypeEditRoute", url:="Measurement-Comment-Types/{MeasurementCommentTypeRouteName}/Edit", defaults:=New With {.controller = "MeasurementCommentTypes", .action = "Edit"})
        routes.MapRoute(name:="MeasurementCommentTypeDeleteRoute", url:="Measurement-Comment-Types/{MeasurementCommentTypeRouteName}/Delete", defaults:=New With {.controller = "MeasurementCommentTypes", .action = "Delete"})
        routes.MapRoute(name:="MeasurementCommentTypeAddExcludedMeasurementViewRoute", url:="Measurement-Comment-Types/{MeasurementCommentTypeRouteName}/AddExcludedMeasurementView/ExcludedMeasurementViewId", defaults:=New With {.controller = "MeasurementCommentTypes", .action = "AddExcludedMeasurementView"})
        routes.MapRoute(name:="MeasurementCommentTypeRemoveExcludedMeasurementViewRoute", url:="Measurement-Comment-Types/{MeasurementCommentTypeRouteName}/RemoveExcludedMeasurementView/ExcludedMeasurementViewId", defaults:=New With {.controller = "MeasurementCommentTypes", .action = "RemoveExcludedMeasurementView"})
        routes.MapRoute(name:="MeasurementCommentTypeAddExcludedAssessmentCriterionGroupRoute", url:="Measurement-Comment-Types/{MeasurementCommentTypeRouteName}/AddExcludedAssessmentCriterionGroup/ExcludedAssessmentCriterionGroupId", defaults:=New With {.controller = "MeasurementCommentTypes", .action = "AddExcludedAssessmentCriterionGroup"})
        routes.MapRoute(name:="MeasurementCommentTypeRemoveExcludedAssessmentCriterionGroupRoute", url:="Measurement-Comment-Types/{MeasurementCommentTypeRouteName}/RemoveExcludedAssessmentCriterionGroup/ExcludedAssessmentCriterionGroupId", defaults:=New With {.controller = "MeasurementCommentTypes", .action = "RemoveExcludedAssessmentCriterionGroup"})

        ' Measurement Files
        routes.MapRoute(name:="MeasurementFileIndexRoute", url:="Measurement-Files", defaults:=New With {.controller = "MeasurementFiles", .action = "Index"})
        routes.MapRoute(name:="MeasurementFileIndexAfterDeleteSuccessRoute", url:="Measurement-Files/Delete-Success", defaults:=New With {.controller = "MeasurementFiles", .action = "IndexAfterDeleteSuccess"})
        routes.MapRoute(name:="MeasurementFileIndexAfterDeleteFailureRoute", url:="Measurement-Files/Delete-Failure", defaults:=New With {.controller = "MeasurementFiles", .action = "IndexAfterDeleteFailure"})
        routes.MapRoute(name:="MeasurementFileIndexUpdateProjectsRoute", url:="Measurement-Files/GetProjects/", defaults:=New With {.controller = "MeasurementFiles", .action = "GetProjectsSelectList"})
        routes.MapRoute(name:="MeasurementFileIndexUpdateMonitorLocationsRoute", url:="Measurement-Files/GetMonitorLocations/", defaults:=New With {.controller = "MeasurementFiles", .action = "GetMonitorLocationsSelectList"})
        routes.MapRoute(name:="MeasurementFileDetailsRoute", url:="Measurement-Files/{MeasurementFileId}", defaults:=New With {.controller = "MeasurementFiles", .action = "Details"})
        routes.MapRoute(name:="MeasurementFileDeleteRoute", url:="Measurement-Files/{MeasurementFileRouteName}/Delete", defaults:=New With {.controller = "MeasurementFiles", .action = "Delete"})

        ' Measurement Metrics
        routes.MapRoute(name:="MeasurementMetricIndexRoute", url:="Measurement-Metrics", defaults:=New With {.controller = "MeasurementMetrics", .action = "Index"})
        routes.MapRoute(name:="MeasurementMetricCreateRoute", url:="Measurement-Metrics/Create", defaults:=New With {.controller = "MeasurementMetrics", .action = "Create"})
        routes.MapRoute(name:="MeasurementMetricDetailsRoute", url:="Measurement-Metrics/{MeasurementMetricRouteName}", defaults:=New With {.controller = "MeasurementMetrics", .action = "Details"})
        routes.MapRoute(name:="MeasurementMetricEditRoute", url:="Measurement-Metrics/{MeasurementMetricRouteName}/Edit", defaults:=New With {.controller = "MeasurementMetrics", .action = "Edit"})
        routes.MapRoute(name:="MeasurementMetricDeleteRoute", url:="Measurement-Metrics/{MeasurementMetricRouteName}/Delete", defaults:=New With {.controller = "MeasurementMetrics", .action = "Delete"})

        ' Measurement Types
        routes.MapRoute(name:="MeasurementTypeIndexRoute", url:="Measurement-Types", defaults:=New With {.controller = "MeasurementTypes", .action = "Index"})
        routes.MapRoute(name:="MeasurementTypeCreateRoute", url:="Measurement-Types/Create", defaults:=New With {.controller = "MeasurementTypes", .action = "Create"})
        routes.MapRoute(name:="MeasurementTypeDetailsRoute", url:="Measurement-Types/{MeasurementTypeRouteName}", defaults:=New With {.controller = "MeasurementTypes", .action = "Details"})
        routes.MapRoute(name:="MeasurementTypeEditRoute", url:="Measurement-Types/{MeasurementTypeRouteName}/Edit", defaults:=New With {.controller = "MeasurementTypes", .action = "Edit"})
        routes.MapRoute(name:="MeasurementTypeDeleteRoute", url:="Measurement-Types/{MeasurementTypeRouteName}/Delete", defaults:=New With {.controller = "MeasurementTypes", .action = "Delete"})

        ' Measurement Views
        routes.MapRoute(name:="MeasurementViewIndexRoute", url:="Measurement-Views", defaults:=New With {.controller = "MeasurementViews", .action = "Index"})
        routes.MapRoute(name:="MeasurementViewCreateRoute", url:="Measurement-Views/Create", defaults:=New With {.controller = "MeasurementViews", .action = "Create"})
        routes.MapRoute(name:="MeasurementViewDetailsRoute", url:="Measurement-Views/{MeasurementViewRouteName}", defaults:=New With {.controller = "MeasurementViews", .action = "Details"})
        routes.MapRoute(name:="MeasurementViewEditRoute", url:="Measurement-Views/{MeasurementViewRouteName}/Edit", defaults:=New With {.controller = "MeasurementViews", .action = "Edit"})
        routes.MapRoute(name:="MeasurementViewDeleteRoute", url:="Measurement-Views/{MeasurementViewRouteName}/Delete", defaults:=New With {.controller = "MeasurementViews", .action = "Delete"})
        routes.MapRoute(name:="MeasurementViewAddUserAccessLevelRoute", url:="Measurement-Views/{MeasurementViewRouteName}/AddUserAccessLevel/UserAccessLevelId", defaults:=New With {.controller = "MeasurementViews", .action = "AddUserAccessLevel"})
        routes.MapRoute(name:="MeasurementViewRemoveUserAccessLevelRoute", url:="Measurement-Views/{MeasurementViewRouteName}/RemoveUserAccessLevel/UserAccessLevelId", defaults:=New With {.controller = "MeasurementViews", .action = "RemoveUserAccessLevel"})
        routes.MapRoute(name:="MeasurementViewAddCommentTypeRoute", url:="Measurement-Views/{MeasurementViewRouteName}/AddCommentType/CommentTypeId", defaults:=New With {.controller = "MeasurementViews", .action = "AddCommentType"})
        routes.MapRoute(name:="MeasurementViewRemoveCommentTypeRoute", url:="Measurement-Views/{MeasurementViewRouteName}/RemoveCommentType/CommentTypeId", defaults:=New With {.controller = "MeasurementViews", .action = "RemoveCommentType"})
        routes.MapRoute(name:="MeasurementViewAddProjectRoute", url:="MeasurementViews/AddProject/{MeasurementViewId}/{ProjectId}", defaults:=New With {.controller = "MeasurementViews", .action = "AddProject"})
        routes.MapRoute(name:="MeasurementViewRemoveProjectRoute", url:="MeasurementViews/RemoveProject/{MeasurementViewId}/{ProjectId}", defaults:=New With {.controller = "MeasurementViews", .action = "RemoveProject"})

        ' Measurement View Groups
        routes.MapRoute(name:="MeasurementViewGroupCreateRoute", url:="Measurement-View-Groups/Create/{MeasurementViewRouteName}", defaults:=New With {.controller = "MeasurementViewGroups", .action = "Create"})
        routes.MapRoute(name:="MeasurementViewGroupEditRoute", url:="Measurement-View-Groups/Edit/{MeasurementViewRouteName}/Group-{GroupIndex}", defaults:=New With {.controller = "MeasurementViewGroups", .action = "Edit"})
        routes.MapRoute(name:="MeasurementViewGroupDeleteRoute", url:="Measurement-View-Group/Delete/{MeasurementViewRouteName}/Group-{GroupIndex}", defaults:=New With {.controller = "MeasurementViews", .action = "DeleteGroup"})

        ' Measurement View Sequence Settings
        routes.MapRoute(name:="MeasurementViewSequenceSettingCreateRoute", url:="Measurement-View-Sequence/Create/{MeasurementViewRouteName}/Group-{GroupIndex}", defaults:=New With {.controller = "MeasurementViewSequenceSettings", .action = "Create"})
        routes.MapRoute(name:="MeasurementViewSequenceSettingEditRoute", url:="Measurement-View-Sequence/Edit/{MeasurementViewRouteName}/Group-{GroupIndex}/Sequence-{SequenceIndex}", defaults:=New With {.controller = "MeasurementViewSequenceSettings", .action = "Edit"})
        routes.MapRoute(name:="MeasurementViewSequenceSettingDeleteRoute", url:="Measurement-View-Sequence/Delete/{MeasurementViewRouteName}/Group-{GroupIndex}/Sequence-{SequenceIndex}", defaults:=New With {.controller = "MeasurementViews", .action = "DeleteSequence"})

        ' Measurements
        routes.MapRoute(name:="MeasurementPostUploadRoute", url:="Measurements/Upload", defaults:=New With {.controller = "Measurements", .action = "Upload"})

        ' Monitors
        routes.MapRoute(name:="MonitorIndexRoute", url:="Monitors", defaults:=New With {.controller = "Monitors", .action = "Index"})
        routes.MapRoute(name:="MonitorCreateRoute", url:="Monitors/Create", defaults:=New With {.controller = "Monitors", .action = "Create"})
        routes.MapRoute(name:="MonitorDetailsRoute", url:="Monitors/{MonitorRouteName}", defaults:=New With {.controller = "Monitors", .action = "Details"})
        routes.MapRoute(name:="MonitorEditRoute", url:="Monitors/{MonitorRouteName}/Edit", defaults:=New With {.controller = "Monitors", .action = "Edit"})
        routes.MapRoute(name:="MonitorDeleteRoute", url:="Monitors/{MonitorRouteName}/Delete", defaults:=New With {.controller = "Monitors", .action = "Delete"})

        ' Monitor Deployment Records
        routes.MapRoute(name:="MonitorDeploymentRecordIndexRoute", url:="Monitor-Deployment-Records", defaults:=New With {.controller = "MonitorDeploymentRecords", .action = "Index"})
        routes.MapRoute(name:="MonitorDeploymentRecordIndexUpdateProjectsRoute", url:="Monitor-Deployment-Records/UpdateProjects/", defaults:=New With {.controller = "MonitorDeploymentRecords", .action = "GetProjectsSelectList"})
        routes.MapRoute(name:="MonitorDeploymentRecordIndexUpdateMonitorLocationsRoute", url:="Monitor-Deployment-Records/UpdateMonitorLocations/", defaults:=New With {.controller = "MonitorDeploymentRecords", .action = "GetMonitorLocationsSelectList"})
        routes.MapRoute(name:="MonitorDeploymentRecordsGetProjectMonitorLocationsRoute", url:="Monitor-Deployment-Records/GetProjectMonitorLocations", defaults:=New With {.controller = "MonitorDeploymentRecords", .action = "GetProjectMonitorLocations"})
        routes.MapRoute(name:="MonitorDeploymentRecordCreateForMonitorRoute", url:="Monitor-Deployment-Records/Deploy-Monitor/{MonitorRouteName}", defaults:=New With {.controller = "MonitorDeploymentRecords", .action = "CreateForMonitor"})
        routes.MapRoute(name:="MonitorDeploymentRecordCreateForMonitorLocationRoute", url:="Monitor-Deployment-Records/Deploy-To-Location/{ProjectRouteName}/{MonitorLocationRouteName}", defaults:=New With {.controller = "MonitorDeploymentRecords", .action = "CreateForMonitorLocation"})
        routes.MapRoute(name:="MonitorDeploymentRecordEndRoute", url:="Monitor-Deployment-Records/{MonitorRouteName}/End-Deployment", defaults:=New With {.controller = "MonitorDeploymentRecords", .action = "EndDeployment"})
        routes.MapRoute(name:="MonitorDeploymentRecordDetailsRoute", url:="Monitor-Deployment-Records/{MonitorRouteName}/{DeploymentIndex}", defaults:=New With {.controller = "MonitorDeploymentRecords", .action = "Details"})

        ' Organisations
        routes.MapRoute(name:="OrganisationIndexRoute", url:="Organisations", defaults:=New With {.controller = "Organisations", .action = "Index"})
        routes.MapRoute(name:="OrganisationCreateRoute", url:="Organisations/Create", defaults:=New With {.controller = "Organisations", .action = "Create"})
        routes.MapRoute(name:="OrganisationDetailsRoute", url:="Organisations/{OrganisationRouteName}", defaults:=New With {.controller = "Organisations", .action = "Details"})
        routes.MapRoute(name:="OrganisationEditRoute", url:="Organisations/{OrganisationRouteName}/Edit", defaults:=New With {.controller = "Organisations", .action = "Edit"})
        routes.MapRoute(name:="OrganisationDeleteRoute", url:="Organisations/{OrganisationRouteName}/Delete", defaults:=New With {.controller = "Organisations", .action = "Delete"})

        ' Organisation Types
        routes.MapRoute(name:="OrganisationTypeIndexRoute", url:="Organisation-Types", defaults:=New With {.controller = "OrganisationTypes", .action = "Index"})
        routes.MapRoute(name:="OrganisationTypeCreateRoute", url:="Organisation-Types/Create", defaults:=New With {.controller = "OrganisationTypes", .action = "Create"})
        routes.MapRoute(name:="OrganisationTypeDetailsRoute", url:="Organisation-Types/{OrganisationTypeRouteName}", defaults:=New With {.controller = "OrganisationTypes", .action = "Details"})
        routes.MapRoute(name:="OrganisationTypeEditRoute", url:="Organisation-Types/{OrganisationTypeRouteName}/Edit", defaults:=New With {.controller = "OrganisationTypes", .action = "Edit"})
        routes.MapRoute(name:="OrganisationTypeDeleteRoute", url:="Organisation-Types/{OrganisationTypeRouteName}/Delete", defaults:=New With {.controller = "OrganisationTypes", .action = "Delete"})

        ' Public Holidays
        routes.MapRoute(name:="PublicHolidayIndexRoute", url:="Public-Holidays", defaults:=New With {.controller = "PublicHolidays", .action = "Index"})
        routes.MapRoute(name:="PublicHolidayDeleteRoute", url:="Public-Holidays/{PublicHolidayRouteName}/Delete", defaults:=New With {.controller = "PublicHolidays", .action = "Delete"})

        ' Users
        routes.MapRoute(name:="UserIndexRoute", url:="Users", defaults:=New With {.controller = "Users", .action = "Index"})
        routes.MapRoute(name:="UserCreateRoute", url:="Users/Create", defaults:=New With {.controller = "Users", .action = "Create"})
        routes.MapRoute(name:="UserDetailsRoute", url:="Users/{UserRouteName}", defaults:=New With {.controller = "Users", .action = "Details"})
        routes.MapRoute(name:="UserEditRoute", url:="Users/{UserRouteName}/Edit", defaults:=New With {.controller = "Users", .action = "Edit"})
        routes.MapRoute(name:="UserDeleteRoute", url:="Users/{UserRouteName}/Delete", defaults:=New With {.controller = "Users", .action = "Delete"})
        routes.MapRoute(name:="UserRequestPasswordResetRoute", url:="Users/{UserRouteName}/ResetPassword", defaults:=New With {.controller = "Users", .action = "RequestPasswordReset"})

        ' User Access Levels
        routes.MapRoute(name:="UserAccessLevelIndexRoute", url:="User-Access-Levels", defaults:=New With {.controller = "UserAccessLevels", .action = "Index"})
        routes.MapRoute(name:="UserAccessLevelCreateRoute", url:="User-Access-Levels/Create", defaults:=New With {.controller = "UserAccessLevels", .action = "Create"})
        routes.MapRoute(name:="UserAccessLevelDetailsRoute", url:="User-Access-Levels/{UserAccessLevelRouteName}", defaults:=New With {.controller = "UserAccessLevels", .action = "Details"})
        routes.MapRoute(name:="UserAccessLevelEditRoute", url:="User-Access-Levels/{UserAccessLevelRouteName}/Edit", defaults:=New With {.controller = "UserAccessLevels", .action = "Edit"})
        routes.MapRoute(name:="UserAccessLevelDeleteRoute", url:="User-Access-Levels/{UserAccessLevelRouteName}/Delete", defaults:=New With {.controller = "UserAccessLevels", .action = "Delete"})

        ' Assessment Criterion Groups
        routes.MapRoute(name:="AssessmentCriterionGroupIndexRoute",
                        url:="Assessment-Groups",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "Index"})
        routes.MapRoute(name:="AssessmentCriterionGroupGetMonitorLocationCriteriaForEditRoute",
                        url:="Assessment-Groups/Get-Criteria-For-Edit",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "GetMonitorLocationCriteriaForEdit"})
        routes.MapRoute(name:="AssessmentCriterionGroupGetMonitorLocationCriteriaForDetailsRoute",
                        url:="Assessment-Groups/Get-Criteria-For-Details",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "GetMonitorLocationCriteriaForDetails"})
        routes.MapRoute(name:="AssessmentCriterionGroupGetProjectAssessmentCriterionGroupsRoute",
                        url:="Assessment-Groups/GetProjectAssessmentCriterionGroups/",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "GetProjectAssessmentCriterionGroups"})
        routes.MapRoute(name:="AssessmentCriterionGroupGetAssessmentCriterionGroupMonitorLocationsRoute",
                        url:="Assessment-Groups/GetAssessmentCriterionGroupMonitorLocations/",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "GetAssessmentCriterionGroupMonitorLocations"})
        routes.MapRoute(name:="AssessmentCriterionGroupCreateNewRoute",
                        url:="Assessment-Criterion-Group/Create-New",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "CreateNew"})
        routes.MapRoute(name:="AssessmentCriterionGroupCreateFromExistingRoute",
                        url:="Assessment-Criterion-Group/Create-From-Existing",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "CreateFromExisting"})
        routes.MapRoute(name:="AssessmentCriterionGroupCreateRoute",
                        url:="Assessment-Groups/{ProjectRouteName}/Create",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "Create"})
        routes.MapRoute(name:="AssessmentCriterionGroupDetailsRoute",
                        url:="Assessment-Groups/{ProjectRouteName}/{AssessmentCriterionGroupRouteName}",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "Details"})
        routes.MapRoute(name:="AssessmentCriterionGroupEditRoute",
                        url:="Assessment-Groups/{ProjectRouteName}/{AssessmentCriterionGroupRouteName}/Edit",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "Edit"})
        routes.MapRoute(name:="AssessmentCriterionGroupDeleteRoute",
                        url:="Assessment-Groups/{ProjectRouteName}/{AssessmentCriterionGroupRouteName}/Delete",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "Delete"})
        routes.MapRoute(name:="AssessmentCriterionGroupDeleteFromProjectRoute",
                        url:="Assessment-Group/DeleteFromProject/{ProjectId}/{AssessmentCriterionGroupId}",
                        defaults:=New With {.controller = "Projects", .action = "DeleteAssessmentCriterionGroupFromProject"})

        ' Assessment Criteria
        '  old
        routes.MapRoute(name:="AssessmentCriteriaCopyRoute",
                        url:="Assessment-Criteria/Copy-Criteria",
                        defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "CopyCriteria"})
        '  new
        routes.MapRoute(name:="AssessmentCriterionIndexRoute",
                        url:="Assessment-Criteria/{ProjectRouteName}/{AssessmentCriterionGroupRouteName}/{MonitorLocationRouteName}",
                        defaults:=New With {.controller = "AssessmentCriteria", .action = "Index"})
        routes.MapRoute(name:="AssessmentCriterionCreateRoute",
                        url:="Assessment-Criteria/{ProjectRouteName}/{AssessmentCriterionGroupRouteName}/{MonitorLocationRouteName}/Create",
                        defaults:=New With {.controller = "AssessmentCriteria", .action = "Create"})
        routes.MapRoute(name:="AssessmentCriterionDetailsRoute",
                        url:="Assessment-Groups/{ProjectRouteName}/{AssessmentCriterionGroupRouteName}/{MonitorLocationRouteName}/{CriterionIndex}",
                        defaults:=New With {.controller = "AssessmentCriteria", .action = "Details"})
        routes.MapRoute(name:="AssessmentCriterionEditRoute",
                        url:="Assessment-Criteria/{ProjectRouteName}/{AssessmentCriterionGroupRouteName}/{MonitorLocationRouteName}/{CriterionIndex}/Edit",
                        defaults:=New With {.controller = "AssessmentCriteria", .action = "Edit"})

        '  old
        routes.MapRoute(name:="AssessmentCriterionDeleteRoute", url:="Assessment-Criterion/Delete/{DeleteAssessmentCriterionId}", defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "DeleteAssessmentCriterion"})
        routes.MapRoute(name:="AssessmentCriterionPopUpCreateRoute", url:="Assessment-Criterion/Create", defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "CreateAssessmentCriterion"})
        routes.MapRoute(name:="AssessmentCriterionPopUpEditRoute", url:="Assessment-Criterion/Edit", defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "EditAssessmentCriterion"})
        routes.MapRoute(name:="AssessmentCriterionMoveUpRoute", url:="Assessment-Criterion/MoveUp/{AssessmentCriterionId}", defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "MoveAssessmentCriterionUp"})
        routes.MapRoute(name:="AssessmentCriterionMoveDownRoute", url:="Assessment-Criterion/MoveDown/{AssessmentCriterionId}", defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "MoveAssessmentCriterionDown"})

        routes.MapRoute(name:="MonitorLocationAssessmentCriterionGroupDeleteRoute", url:="Monitor-Location/DeleteAssessmentCriterionGroup/{MonitorLocationId}/{AssessmentCriterionGroupId}", defaults:=New With {.controller = "MonitorLocations", .action = "DeleteAssessmentCriterionGroup"})

        ' Assessments
        routes.MapRoute(name:="ViewTableAndGraphAjaxRoute", url:="Assessment-Criteria/ViewTableAndGraph", defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "ViewTableAndGraphAjax"})
        routes.MapRoute(name:="AssessmentViewNavigationButtonsRoute", url:="Assessment-Criteria/View-Navigation-Buttons", defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "ViewNavigationButtons"})
        routes.MapRoute(name:="AssessmentTableDownloadRoute", url:="Assessment-Criteria/Download/{MonitorLocationId}/{AssessmentCriterionGroupId}/{strAssessmentDate}/{StartOrEnd}", defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "DownloadTable"})

        ' Standard Weekly Working Hours
        routes.MapRoute(name:="StandardWeeklyWorkingHoursEditRoute", url:="Standard-Weekly-Working-Hours/{ProjectRouteName}/Edit", defaults:=New With {.controller = "StandardWeeklyWorkingHours", .action = "Edit"})
        routes.MapRoute(name:="StandardWeeklyWorkingHoursAddMeasurementViewRoute", url:="Standard-Weekly-Working-Hours/{ProjectRouteName}/AddMeasurementView/MeasurementViewId", defaults:=New With {.controller = "StandardWeeklyWorkingHours", .action = "AddMeasurementView"})
        routes.MapRoute(name:="StandardWeeklyWorkingHoursRemoveMeasurementViewRoute", url:="Standard-Weekly-Working-Hours/{ProjectRouteName}/RemoveMeasurementView/MeasurementViewId", defaults:=New With {.controller = "StandardWeeklyWorkingHours", .action = "RemoveMeasurementView"})


        ' Variations
        routes.MapRoute(name:="VariedWeeklyWorkingHoursIndexRoute", url:="Variations", defaults:=New With {.controller = "VariedWeeklyWorkingHours", .action = "Index"})
        routes.MapRoute(name:="VariedWeeklyWorkingHoursCreateRoute", url:="{ProjectRouteName}/Variations/Create", defaults:=New With {.controller = "VariedWeeklyWorkingHours", .action = "Create"})
        routes.MapRoute(name:="VariedWeeklyWorkingHoursDetailsRoute", url:="Variations/Details/{VariedWeeklyWorkingHoursId}", defaults:=New With {.controller = "VariedWeeklyWorkingHours", .action = "Details"})
        routes.MapRoute(name:="VariedWeeklyWorkingHoursEditRoute", url:="Variations/Edit/{VariedWeeklyWorkingHoursId}", defaults:=New With {.controller = "VariedWeeklyWorkingHours", .action = "Edit"})
        routes.MapRoute(name:="VariedWeeklyWorkingHoursDeleteRoute", url:="Variations/Delete/{VariedWeeklyWorkingHoursId}", defaults:=New With {.controller = "Projects", .action = "DeleteVariation"})

        ' Projects
        routes.MapRoute(name:="ProjectEditAssessmentCriteriaRoute", url:="{ProjectRouteName}/Edit_AssessmentCriteria", defaults:=New With {.controller = "Projects", .action = "Edit_AssessmentCriteria"})
        routes.MapRoute(name:="ProjectEditContactsRoute", url:="{ProjectRouteName}/Edit_Contacts", defaults:=New With {.controller = "Projects", .action = "Edit_Contacts"})
        routes.MapRoute(name:="ProjectEditMeasurementViewsRoute", url:="{ProjectRouteName}/Edit_MeasurementViews", defaults:=New With {.controller = "Projects", .action = "Edit_MeasurementViews"})
        routes.MapRoute(name:="ProjectEditMonitorLocationsRoute", url:="{ProjectRouteName}/Edit_MonitorLocations", defaults:=New With {.controller = "Projects", .action = "Edit_MonitorLocations"})
        routes.MapRoute(name:="ProjectEditOrganisationsRoute", url:="{ProjectRouteName}/Edit_Organisations", defaults:=New With {.controller = "Projects", .action = "Edit_Organisations"})
        routes.MapRoute(name:="ProjectEditWorkingHoursRoute", url:="{ProjectRouteName}/Edit_WorkingHours", defaults:=New With {.controller = "Projects", .action = "Edit_WorkingHours"})

        routes.MapRoute(name:="ProjectIndexRoute", url:="Projects", defaults:=New With {.controller = "Projects", .action = "Index"})
        routes.MapRoute(name:="ProjectCreateRoute", url:="Projects/Create", defaults:=New With {.controller = "Projects", .action = "Create"})
        routes.MapRoute(name:="ProjectDetailsRoute", url:="{ProjectRouteName}/Details", defaults:=New With {.controller = "Projects", .action = "Details"})
        routes.MapRoute(name:="ProjectEditRoute", url:="{ProjectRouteName}/Edit", defaults:=New With {.controller = "Projects", .action = "Edit"})
        routes.MapRoute(name:="ProjectDeleteRoute", url:="{ProjectRouteName}/Delete", defaults:=New With {.controller = "Projects", .action = "Delete"})

        routes.MapRoute(name:="MonitorLocationSelectRoute", url:="{ProjectRouteName}/Select-Monitor", defaults:=New With {.controller = "MonitorLocations", .action = "Select"})

        routes.MapRoute(name:="ProjectAddAssessmentCriterionGroupRoute", url:="{ProjectRouteName}/AddAssessmentCriterionGroup/AssessmentCriterionGroupId", defaults:=New With {.controller = "Projects", .action = "AddAssessmentCriterionGroup"})
        routes.MapRoute(name:="ProjectRemoveAssessmentCriterionGroupRoute", url:="{ProjectRouteName}/RemoveAssessmentCriterionGroup/AssessmentCriterionGroupId", defaults:=New With {.controller = "Projects", .action = "RemoveAssessmentCriterionGroup"})

        routes.MapRoute(name:="ProjectAddMeasurementViewRoute", url:="{ProjectRouteName}/AddMeasurementView/MeasurementViewId", defaults:=New With {.controller = "Projects", .action = "AddMeasurementView"})
        routes.MapRoute(name:="ProjectRemoveMeasurementViewRoute", url:="{ProjectRouteName}/RemoveMeasurementView/MeasurementViewId", defaults:=New With {.controller = "Projects", .action = "RemoveMeasurementView"})

        routes.MapRoute(name:="ProjectAddMonitorLocationRoute", url:="{ProjectRouteName}/AddMonitorLocation/MonitorLocationId", defaults:=New With {.controller = "Projects", .action = "AddMonitorLocation"})
        routes.MapRoute(name:="ProjectRemoveMonitorLocationRoute", url:="{ProjectRouteName}/RemoveMonitorLocation/MonitorLocationId", defaults:=New With {.controller = "Projects", .action = "RemoveMonitorLocation"})

        routes.MapRoute(name:="ProjectAddOrganisationRoute", url:="{ProjectRouteName}/AddOrganisation/OrganisationId", defaults:=New With {.controller = "Projects", .action = "AddOrganisation"})
        routes.MapRoute(name:="ProjectRemoveOrganisationRoute", url:="{ProjectRouteName}/RemoveOrganisation/OrganisationId", defaults:=New With {.controller = "Projects", .action = "RemoveOrganisation"})

        routes.MapRoute(name:="ProjectAddContactRoute", url:="{ProjectRouteName}/AddContact/ContactId", defaults:=New With {.controller = "Projects", .action = "AddContact"})
        routes.MapRoute(name:="ProjectRemoveContactRoute", url:="{ProjectRouteName}/RemoveContact/ContactId", defaults:=New With {.controller = "Projects", .action = "RemoveContact"})


        ' Monitor Locations
        routes.MapRoute(name:="MonitorLocationIndexRoute", url:="Monitor-Locations", defaults:=New With {.controller = "MonitorLocations", .action = "Index"})
        routes.MapRoute(name:="MonitorLocationCreateRoute", url:="{ProjectRouteName}/Monitor-Locations/Create", defaults:=New With {.controller = "MonitorLocations", .action = "Create"})
        routes.MapRoute(name:="MonitorLocationDetailsRoute", url:="{ProjectRouteName}/{MonitorLocationRouteName}", defaults:=New With {.controller = "MonitorLocations", .action = "Details"})
        routes.MapRoute(name:="MonitorLocationEditRoute", url:="{ProjectRouteName}/{MonitorLocationRouteName}/Edit", defaults:=New With {.controller = "MonitorLocations", .action = "Edit"})

        ' Measurements
        routes.MapRoute(name:="MeasurementViewRoute", url:="{ProjectRouteName}/{MonitorLocationRouteName}/Measurements/View/{ViewRouteName}/{ViewDuration}/{strStartDate}", defaults:=New With {.controller = "Measurements", .action = "View"})
        routes.MapRoute(name:="MeasurementUploadResultRoute", url:="{ProjectRouteName}/{MonitorLocationRouteName}/Measurements/Upload-Result/{MeasurementFileId}", defaults:=New With {.controller = "Measurements", .action = "UploadResult"})
        routes.MapRoute(name:="MeasurementUploadRoute", url:="{ProjectRouteName}/{MonitorLocationRouteName}/Measurements/Upload", defaults:=New With {.controller = "Measurements", .action = "Upload"})
        routes.MapRoute(name:="MeasurementUpdateViewRoute", url:="{ProjectRouteName}/{MonitorLocationRouteName}/Measurements/Update-View", defaults:=New With {.controller = "Measurements", .action = "UpdateView"})
        routes.MapRoute(name:="MeasurementUpdateNavigationButtonsRoute", url:="{ProjectRouteName}/{MonitorLocationRouteName}/Measurements/Update-Navigation-Buttons", defaults:=New With {.controller = "Measurements", .action = "UpdateNavigationButtons"})

        ' Measurement Comments
        routes.MapRoute(name:="MeasurementCommentIndexRoute", url:="{ProjectRouteName}/{MonitorLocationRouteName}/Comments", defaults:=New With {.controller = "MeasurementComments", .action = "Index"})
        routes.MapRoute(name:="MeasurementCommentUpdateViewRoute", url:="{ProjectRouteName}/{MonitorLocationRouteName}/Comments/Update-View", defaults:=New With {.controller = "MeasurementComments", .action = "UpdateCommentIndexView"})
        routes.MapRoute(name:="MeasurementCommentCreateRoute", url:="{ProjectRouteName}/{MonitorLocationRouteName}/Comments/Create/{StartDateCode}/{EndDateCode}", defaults:=New With {.controller = "MeasurementComments", .action = "Create"})
        routes.MapRoute(name:="MeasurementCommentDeleteRoute", url:="Measurement-Comment/Delete/{MeasurementCommentId}/{ShowMeasurementCommentTypeLinks}/{ShowContactLinks}/{ShowDeleteCommentLinks}", defaults:=New With {.controller = "MeasurementComments", .action = "DeleteMeasurementComment"})

        ' Assessments
        routes.MapRoute(name:="AssessmentViewRoute", url:="{ProjectRouteName}/{MonitorLocationRouteName}/Assessments/{strAssessmentDate}", defaults:=New With {.controller = "AssessmentCriterionGroups", .action = "View", .strAssessmentDate = UrlParameter.Optional})

        ' Default
        routes.MapRoute(name:="Default", url:="{controller}/{action}/{id}", defaults:=New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional})

    End Sub

End Class