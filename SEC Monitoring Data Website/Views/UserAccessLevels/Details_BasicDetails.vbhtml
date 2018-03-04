@ModelType SEC_Monitoring_Data_Website.UserAccessLevel

@Code
    Dim showUserLinks = DirectCast(ViewData("ShowUserLinks"), Boolean)
End Code

<h3>Permissions</h3>
<table class="details-table">
    <tr>
        <th>
            Project Permissions
        </th>
        <td>
            @Html.DisplayFor(Function(model) model.ProjectPermission.PermissionName)
        </td>
    </tr>
</table>

<h3>Actions</h3>
<table class="details-table">
    <tr>
        <th colspan="6">
            Assessments
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewAssessments)
            View
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Assessment Criteria
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateAssessmentCriteria)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewAssessmentCriteria)
            View
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditAssessmentCriteria)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteAssessmentCriteria)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Calculation Filters
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateCalculationFilters)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewCalculationFilterDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewCalculationFilterList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditCalculationFilters)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteCalculationFilters)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Contacts
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateContacts)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewContactDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewContactList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditContacts)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteContacts)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Countries
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateCountries)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewCountryDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewCountryList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditCountries)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteCountries)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Documents
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateDocuments)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewDocumentDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewDocumentList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditDocuments)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteDocuments)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Document Types
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateDocumentTypes)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewDocumentTypeDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewDocumentTypeList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditDocumentTypes)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteDocumentTypes)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Measurements
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMeasurements)
            View
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanUploadMeasurements)
            Upload
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteMeasurements)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Measurement Comments
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMeasurementCommentList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateMeasurementComments)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteMeasurementComments)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Measurement Comment Types
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateMeasurementCommentTypes)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMeasurementCommentTypeDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMeasurementCommentTypeList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditMeasurementCommentTypes)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteMeasurementCommentTypes)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Measurement Files
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMeasurementFileDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMeasurementFileList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteMeasurementFiles)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Measurement Metrics
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateMeasurementMetrics)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMeasurementMetricDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMeasurementMetricList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditMeasurementMetrics)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteMeasurementMetrics)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Measurement Views
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateMeasurementViews)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMeasurementViewDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMeasurementViewList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditMeasurementViews)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteMeasurementViews)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Monitors
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateMonitors)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMonitorDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMonitorList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditMonitors)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteMonitors)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Monitor Deployment Records
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateDeploymentRecords)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMonitorDeploymentRecordDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMonitorDeploymentRecordList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEndMonitorDeployments)
            End
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteMonitorDeploymentRecords)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Monitor Locations
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateMonitorLocations)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMonitorLocationDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewMonitorLocationList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewSelectMonitorLocations)
            View Select
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditMonitorLocations)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteMonitorLocations)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Organisations
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateOrganisations)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewOrganisationDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewOrganisationList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditOrganisations)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteOrganisations)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Organisation Types
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateOrganisationTypes)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewOrganisationTypeDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewOrganisationTypeList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditOrganisationTypes)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteOrganisationTypes)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Projects
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateProjects)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewProjectDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewProjectList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditProjects)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteProjects)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Public Holidays
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreatePublicHolidays)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeletePublicHolidays)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            System Messages
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateSystemMessages)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewSystemMessages)
            View
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditSystemMessages)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteSystemMessages)
            Delete
        </td>
    </tr>
    <tr>
        <th colspan="6">
            Users
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateUsers)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewUserDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewUserList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditUsers)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteUsers)
            Delete
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanInitiatePasswordResets)
            Initiate Password Resets
        </td>
    </tr>
    <tr>
        <th colspan="6">
            User Access Levels
        </th>
    </tr>
    <tr>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanCreateUserAccessLevels)
            Create
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewUserAccessLevelDetails)
            View Details
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanViewUserAccessLevelList)
            View List
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanEditUserAccessLevels)
            Edit
        </td>
        <td width="16%">
            @Html.DisplayFor(Function(model) model.CanDeleteUserAccessLevels)
            Delete
        </td>
    </tr>
</table>