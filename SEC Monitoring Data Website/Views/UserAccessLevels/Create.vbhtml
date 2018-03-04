@ModelType SEC_Monitoring_Data_Website.CreateUserAccessLevelViewModel

@Code
    ViewData("Title") = "Create User Access Level"
End Code

<h2>Create User Access Level</h2>

@Using Html.BeginForm()
    @Html.AntiForgeryToken()
    @Html.ValidationSummary(True)

    @<fieldset>
        <legend>UserAccessLevel</legend>

        <table class="create-table">
            <tr>
                <th>
                    Access Level Name
                </th>
                <td>
                    @Html.EditorFor(Function(model) model.UserAccessLevel.AccessLevelName)
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.UserAccessLevel.AccessLevelName)
                </td>
            </tr>
        </table>

        <h3>Permissions</h3>
        <table class="create-table">
            <tr>
                <th>
                    Project Permission
                </th>
                <td>
                    @Html.DropDownListFor(Function(model) model.ProjectPermissionId, Model.ProjectPermissionList, "Please select a Project Permission...")
                </td>
                <td>
                    @Html.ValidationMessageFor(Function(model) model.ProjectPermissionId)
                </td>
            </tr>
        </table>

        <h3>Actions</h3>
        <table class="create-table">
            <tr>
                <th colspan="6">
                    Assessments
                </th>
            </tr>
            <tr>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewAssessments)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateAssessmentCriteria)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewAssessmentCriteria)
                    View
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditAssessmentCriteria)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteAssessmentCriteria)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateCalculationFilters)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewCalculationFilterDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewCalculationFilterList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditCalculationFilters)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteCalculationFilters)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateContacts)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewContactDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewContactList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditContacts)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteContacts)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateCountries)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewCountryDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewCountryList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditCountries)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteCountries)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateDocuments)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewDocumentDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewDocumentList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditDocuments)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteDocuments)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateDocumentTypes)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewDocumentTypeDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewDocumentTypeList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditDocumentTypes)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteDocumentTypes)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMeasurements)
                    View
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanUploadMeasurements)
                    Upload
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteMeasurements)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMeasurementCommentList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateMeasurementComments)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteMeasurementComments)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateMeasurementCommentTypes)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMeasurementCommentTypeDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMeasurementCommentTypeList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditMeasurementCommentTypes)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteMeasurementCommentTypes)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMeasurementFileDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMeasurementFileList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteMeasurementFiles)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateMeasurementMetrics)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMeasurementMetricDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMeasurementMetricList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditMeasurementMetrics)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteMeasurementMetrics)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateMeasurementViews)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMeasurementViewDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMeasurementViewList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditMeasurementViews)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteMeasurementViews)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateMonitors)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMonitorDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMonitorList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditMonitors)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteMonitors)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateDeploymentRecords)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMonitorDeploymentRecordDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMonitorDeploymentRecordList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEndMonitorDeployments)
                    End
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteMonitorDeploymentRecords)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateMonitorLocations)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMonitorLocationDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewMonitorLocationList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewSelectMonitorLocations)
                    View Select
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditMonitorLocations)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteMonitorLocations)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateOrganisations)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewOrganisationDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewOrganisationList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditOrganisations)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteOrganisations)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateOrganisationTypes)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewOrganisationTypeDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewOrganisationTypeList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditOrganisationTypes)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteOrganisationTypes)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateProjects)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewProjectDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewProjectList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditProjects)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteProjects)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreatePublicHolidays)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeletePublicHolidays)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateSystemMessages)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewSystemMessages)
                    View
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditSystemMessages)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteSystemMessages)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateUsers)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewUserDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewUserList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditUsers)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteUsers)
                    Delete
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanInitiatePasswordResets)
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
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanCreateUserAccessLevels)
                    Create
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewUserAccessLevelDetails)
                    View Details
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanViewUserAccessLevelList)
                    View List
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanEditUserAccessLevels)
                    Edit
                </td>
                <td width="16%">
                    @Html.EditorFor(Function(model) model.UserAccessLevel.CanDeleteUserAccessLevels)
                    Delete
                </td>
            </tr>
        </table>

        <p>
            @Html.JQueryUI.Button("Create")
        </p>
    </fieldset>
End Using


@Section Scripts
    @Scripts.Render("~/bundles/jqueryval")
End Section
