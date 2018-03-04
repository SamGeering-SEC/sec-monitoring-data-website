Imports System.ComponentModel.DataAnnotations
Imports libSEC.Strings

Module RegExConstants

    Public Const reLettersNumbersSpacesHyphens = "^(([a-zA-Z0-9çÇ]+\ )|(\-\ ))*([a-zA-Z0-9çÇ]+$)"
    Public Const reLettersNumbersCommasSpacesHyphens = "^(([a-zA-Z0-9çÇ\,]+\ )|(\-\ ))*([a-zA-Z0-9çÇ]+$)"
    Public Const msgLettersNumbersCommasSpacesHyphens = "Please use only letters, numbers, commas, spaces and hyphens. Any hyphens must be preceded and followed by a space (e.g. 1 - 14)."
    Public Const reLettersNumbersCommasSpacesParenthesesHyphens = "^(([a-zA-Z0-9çÇ\,\(\)]+\ )|(\-\ ))*([a-zA-Z0-9çÇ\,\(\)]+$)"
    Public Const reLettersNumbersCommasPeriodsSpacesParenthesesHyphens = "^(([a-zA-Z0-9çÇ\,\.\(\)]+\ )|(\-\ ))*([a-zA-Z0-9çÇ\.\,\(\)]+$)"

End Module

#Region "AirQualitySetting"

<MetadataType(GetType(AirQualitySettingMetadata))> _
Partial Public Class AirQualitySetting
End Class
Public Class AirQualitySettingMetadata

    Public Property Id As Int32

    <Display(Name:="Alarm Trigger Level")> _
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AlarmTriggerLevel As String = ""

    <Display(Name:="Inlet Heating On?")> _
    Public Property InletHeatingOn As Boolean

    <Display(Name:="New Daily Sample?")> _
    Public Property NewDailySample As Boolean

    Public Property MonitorSettings As MonitorSettings

End Class

#End Region

#Region "AssessmentCriterion"

<MetadataType(GetType(AssessmentCriterionMetadata))> _
Partial Public Class AssessmentCriterion
End Class
Public Class AssessmentCriterionMetadata

    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <Display(Name:="Assessed Level Series Name")> _
    Public Property AssessedLevelSeriesName As String

    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <Display(Name:="Criterion Level Series Name")> _
    Public Property CriterionLevelSeriesName As String

    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <Display(Name:="Assessed Level Header 1")> _
    Public Property AssessedLevelHeader1 As String

    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <Display(Name:="Assessed Level Header 2")> _
    Public Property AssessedLevelHeader2 As String

    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <Display(Name:="Assessed Level Header 3")> _
    Public Property AssessedLevelHeader3 As String

    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <Display(Name:="Criterion Trigger Header 1")> _
    Public Property CriterionTriggerHeader1 As String

    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <Display(Name:="Criterion Trigger Header 2")> _
    Public Property CriterionTriggerHeader2 As String

    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <Display(Name:="Criterion Trigger Header 3")> _
    Public Property CriterionTriggerHeader3 As String

End Class

#End Region

#Region "Assessment Criterion Group"

<MetadataType(GetType(AssessmentCriterionGroupMetadata))> _
Partial Public Class AssessmentCriterionGroup
End Class
Public Class AssessmentCriterionGroupMetadata


    <Required(ErrorMessage:="Please enter a Name for this Group.")> _
    <Display(Name:="Group Name")> _
    <RegularExpression(reLettersNumbersCommasSpacesHyphens,
        ErrorMessage:=msgLettersNumbersCommasSpacesHyphens)> _
    Public Property GroupName As String

    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <AllowHtml> _
    <Display(Name:="Graph Title")> _
    Public Property GraphTitle As String

    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property GraphXAxisLabel As String

    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property GraphYAxisLabel As String

    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property DateColumn1Header As String

    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property DateColumn1Format As String

    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property DateColumn2Header As String

    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property DateColumn2Format As String


End Class

#End Region

#Region "CalculationFilter"

<MetadataType(GetType(CalculationFilterMetadata))> _
Partial Public Class CalculationFilter
End Class
Public Class CalculationFilterMetadata

    <Required(ErrorMessage:="Please pick the Days of the Week on which this Filter will apply, including Bank Holidays.")> _
    <Display(Name:="Applicable Days Of Week")> _
    Public Property ApplicableDaysOfWeek As IEnumerable(Of DayOfWeek)

    <Required(ErrorMessage:="Please pick an Aggregate Function from the list.")> _
    <Display(Name:="Aggregate Function")> _
    Public Property CalculationAggregateFunction As CalculationAggregateFunction

    Public Property CalculationAggregateFunctionId As Int32

    <Required(ErrorMessage:="Please specify a descriptive name for the Filter so that others can tell what it does.")> _
    <Display(Name:="Filter Name")> _
    <DataType(DataType.Text)> _
    <RegularExpression(reLettersNumbersCommasSpacesParenthesesHyphens,
                       ErrorMessage:="Please enter a Filter Name containing only letters, numbers, commas, parentheses," +
                                     " spaces and hyphens. Any hyphens must be preceded and followed by a space " +
                                     "(e.g. 0700 - 1400).")> _
    Public Property FilterName As String

    Public Property Id As Int32

    <Required(ErrorMessage:="Please specify a scalar value to multiply each of the input measurements by. " +
                            "Use 1 if you do not want to adjust the input measurements.")> _
    <Display(Name:="Input Multiplier")> _
    Public Property InputMultiplier As Double

    <Required(ErrorMessage:="Please pick a Metric from the list.")> _
    <Display(Name:="Metric")> _
    Public Property MeasurementMetric As MeasurementMetric

    Public Property MeasurementMetricId As Int32

    <Required(ErrorMessage:="Please specify the Time Base of the Filter. " +
                            "This is the duration of each application of the Filter within the specified Time Window.")> _
    <Display(Name:="Time Base")> _
    <DataType(DataType.Time)> _
    Public Property TimeBase As Double

    <Required(ErrorMessage:="Please specify a value for the Time-Step of the Filter.")> _
    <Display(Name:="Time Step")> _
    <DataType(DataType.Time)> _
    Public Property TimeStep As String

    <Display(Name:="End Time")> _
    <DataType(DataType.Time)> _
    Public Property TimeWindowEndTime As IEnumerable(Of DateTime)

    <Display(Name:="Start Time")> _
    <DataType(DataType.Time)> _
    Public Property TimeWindowStartTime As IEnumerable(Of DateTime)

    <Required(ErrorMessage:="Please specify whether or not this Filter uses a Time Window.")> _
    <Display(Name:="Use Time Window?")> _
    Public Property UseTimeWindow As Boolean

End Class

#End Region

#Region "Contact"

<MetadataType(GetType(ContactMetadata))> _
Partial Public Class Contact
End Class
Public Class ContactMetadata

    <Required(ErrorMessage:="Please enter a Name for the Contact")> _
    <Display(Name:="Contact Name")> _
    <DataType(DataType.Text)> _
    <RegularExpression(reLettersNumbersSpacesHyphens, ErrorMessage:="Please enter a Contact Name containing only letters, spaces and hyphens. Any hyphens must be preceded and followed by a space (e.g. 1 - 14).")> _
    Public Property ContactName As String

    <Required(ErrorMessage:="Please enter an email address for the Contact.")> _
    <Display(Name:="Email Address")> _
    <DataType(DataType.EmailAddress)> _
    Public Property EmailAddress As String

    Public Property ExcludedDocuments As IEnumerable(Of Document)

    Public Property Id As Int32

    <Required(ErrorMessage:="Please select the Contact's Organisation.")> _
    Public Property Organisation As Organisation

    Public Property OrganisationId As Int32

    <Required(ErrorMessage:="Please enter the Contact's Telephone Number.")> _
    <Display(Name:="Primary Telephone Number")> _
    <DataType(DataType.PhoneNumber)> _
    Public Property PrimaryTelephoneNumber As String

    Public Property Projects As IEnumerable(Of Project)

    <Display(Name:="Secondary Telephone Number")> _
    <DataType(DataType.PhoneNumber)> _
    Public Property SecondaryTelephoneNumber As String

    Public Property SecureLoginId As IEnumerable(Of Int32)

    Public Property UploadedMeasurements As IEnumerable(Of Measurement)

End Class

#End Region

#Region "Country"

<MetadataType(GetType(CountryMetadata))> _
Partial Public Class Country
End Class
Public Class CountryMetadata

    <Required> _
    <Display(Name:="Country Name")> _
    <DataType(DataType.Text)> _
    Public Property CountryName As String

End Class

#End Region

#Region "Document"

<MetadataType(GetType(DocumentMetadata))> _
Partial Public Class Document
End Class
Public Class DocumentMetadata

    <Required(ErrorMessage:="Please select the Organisation that originated this Document.")> _
    <Display(Name:="Author Organisation")> _
    Public Property AuthorOrganisation As Organisation

    <Required> _
    Public Property AuthorOrganisationId As Int32

    '<Display(Name:="Child Documents")> _
    'Public Property ChildDocuments As IEnumerable(Of Document)

    <Required(ErrorMessage:="Please select the Type of Document.")> _
    <Display(Name:="Document Type")> _
    Public Property DocumentType As DocumentType

    <Required> _
    Public Property DocumentTypeId As Int32

    <Display(Name:="End Date Time")> _
    Public Property EndDateTime As DateTime

    <Display(Name:="Excluded Contacts")> _
    Public Property ExcludedContacts As IEnumerable(Of Contact)

    <Display(Name:="File Path")> _
    Public Property FilePath As String

    <Required> _
    Public Property Id As Int32

    <Display(Name:="Monitor Locations")> _
    Public Property MonitorLocations As IEnumerable(Of MonitorLocation)

    <Display(Name:="Monitors")> _
    Public Property Monitors As IEnumerable(Of Monitor)

    <Display(Name:="Start Date Time")> _
    Public Property StartDateTime As DateTime

    <Required(ErrorMessage:="Please enter the Title of the Document.")> _
    <Display(Name:="Title")> _
    Public Property Title As String

End Class

#End Region

#Region "DocumentType"

<MetadataType(GetType(DocumentTypeMetadata))> _
Partial Public Class DocumentType
End Class
Public Class DocumentTypeMetadata

    '<Display(Name:="Allowed User Access Levels")> _
    'Public Property AllowedUserAccessLevels As IEnumerable(Of UserAccessLevel)

    <Display(Name:="Documents")> _
    Public Property Documents As IEnumerable(Of Document)

    <Required(ErrorMessage:="Please enter a name for this Document Type")> _
    <Display(Name:="Document Type Name")> _
    Public Property DocumentTypeName As String

    Public Property Id As Int32

End Class

#End Region

#Region "MeasurementCommentType"

<MetadataType(GetType(MeasurementCommentTypeMetadata))> _
Partial Public Class MeasurementCommentType
End Class
Public Class MeasurementCommentTypeMetadata

    <Display(Name:="Comments")> _
    Public Property Comments As IEnumerable(Of MeasurementComment)

    <Required(ErrorMessage:="Please enter a Name for this Comment Type.")> _
    <Display(Name:="Comment Type Name")> _
    Public Property CommentTypeName As String

    <Display(Name:="Excluded Measurement Views")> _
    Public Property ExcludedMeasurementViews As IEnumerable(Of MeasurementView)

    Public Property Id As Int32

End Class

#End Region

#Region "MeasurementMetric"

<MetadataType(GetType(MeasurementMetricMetadata))> _
Partial Public Class MeasurementMetric
End Class
Public Class MeasurementMetricMetadata

    Public Property Id As Int32

    <Required(ErrorMessage:="Please enter the name of the Measurement Metric")> _
    <Display(Name:="Measurement Metric")> _
    Public Property MetricName As String

    <Required(ErrorMessage:="Please enter how the Measurement Metric should be displayed, using HTML formatting if required.")> _
    <AllowHtml> _
    <Display(Name:="Display Name")> _
    Public Property DisplayName As String

    Public Property MeasurementType As MeasurementType

    <Required(ErrorMessage:="Please enter the Fundamental Unit of Measurement for the Metric, using HTML formatting if required.")> _
    <AllowHtml> _
    <Display(Name:="Fundamental Unit")> _
    Public Property FundamentalUnit As String

    <Required(ErrorMessage:="Please enter the number of Decimal Places to round each Measurement to for display purposes.")>
    Public Property RoundingDecimalPlaces As Integer


End Class


#End Region

#Region "MeasurementView"

<MetadataType(GetType(MeasurementViewMetadata))> _
Partial Public Class MeasurementView
End Class
Public Class MeasurementViewMetadata

    Public Property Id As Int32

    <Required(ErrorMessage:="Please select the Type of Measurement this View relates to.")> _
    <Display(Name:="Measurement Type")> _
    Public Property MeasurementType As MeasurementType

    Public Property MeasurementTypeId As Int32

    <Required(ErrorMessage:="Please add at least one Measurement Group to this View.")> _
    <Display(Name:="View Groups")> _
    Public Property MeasurementViewGroups As IEnumerable(Of MeasurementViewGroup)

    <Required(ErrorMessage:="Please enter the Header Text for the Table which will be displayed in this View.")> _
    <Display(Name:="Table Results Header")> _
    Public Property TableResultsHeader As String

    <Required(ErrorMessage:="Please enter a Descriptive Name for this View.")> _
    <Display(Name:="View Name")> _
    <RegularExpression(reLettersNumbersCommasSpacesHyphens,
        ErrorMessage:=msgLettersNumbersCommasSpacesHyphens)> _
    Public Property ViewName As String

    <Required(ErrorMessage:="Please enter the name that should be used on the Measurements page to select this View")> _
    <Display(Name:="Display Name")> _
    Public Property DisplayName As String

    '<Display(Name:="Allowed Access Levels")> _
    'Public Property AllowedUserAccessLevels As IEnumerable(Of UserAccessLevel)

    Public Property MeasurementViewTableTypeId As Int32

    <Display(Name:="Table Type")> _
    Public Property MeasurementViewTableType As MeasurementViewTableType

    <Required(ErrorMessage:="Please enter the lowest value that should appear on the y-axis of the graph.")> _
    <Display(Name:="Y-axis Minimum Value")> _
    Public Property YAxisMinValue As Double

    <Required(ErrorMessage:="Please enter the highest value that should appear on the y-axis of the graph.")> _
    <Display(Name:="Y-axis Maximum Value")> _
    Public Property YAxisMaxValue As Double

    <Required(ErrorMessage:="Please enter the increments of the ticks on the Y-axis of the graph.")> _
    <Display(Name:="Y-axis Step Value")> _
    Public Property YAxisStepValue As Double

End Class

#End Region

#Region "MeasurementViewGroup"

<MetadataType(GetType(MeasurementViewGroupMetadata))> _
Partial Public Class MeasurementViewGroup
End Class
Public Class MeasurementViewGroupMetadata

    Public Property Id As Int32

    <Required(ErrorMessage:="Please enter the text that will go in the Main-Header of the Table Columns of this View Group.")> _
    <Display(Name:="Main Header")> _
    Public Property MainHeader As String

    <Display(Name:="Measurement View")> _
    Public Property MeasurementView As MeasurementView

    Public Property MeasurementViewId As Int32

    <Display(Name:="Sequence Settings")> _
    Public Property MeasurementViewSequenceSettings As IEnumerable(Of MeasurementViewSequenceSetting)

    <Required(ErrorMessage:="Please enter the text that will go in the Sub-Header of the Table Columns of this View Group.")> _
    <Display(Name:="Sub Header")> _
    Public Property SubHeader As String

End Class

#End Region

#Region "MeasurementViewSequenceSetting"

<MetadataType(GetType(MeasurementViewSequenceSettingMetadata))> _
Partial Public Class MeasurementViewSequenceSetting
End Class
Public Class MeasurementViewSequenceSettingMetadata

    <Required(ErrorMessage:="Please selecty the Calculation Filter which this Sequence will use.")> _
    <Display(Name:="Calculation Filter")> _
    Public Property CalculationFilter As CalculationFilter

    Public Property CalculationFilterId As Int32

    <Required(ErrorMessage:="Please select the Type of Series that will be displayed in the Graph which shows One Day of Measurements")> _
    <Display(Name:="Day Series")> _
    Public Property DayViewSeriesType As String

    Public Property Id As Int32

    <Display(Name:="Group")> _
    Public Property MeasurementViewGroup As MeasurementViewGroup

    Public Property MeasurementViewGroupId As Int32

    <Required(ErrorMessage:="Please select the Type of Series that will be displayed in the Graph which shows One Month of Measurements")> _
    <Display(Name:="Month Series")> _
    Public Property MonthViewSeriesType As String

    <Required(ErrorMessage:="Please specify the Colour of the Series that will be displayed in Graphs of this Sequence.")> _
    <Display(Name:="Series Colour")> _
    Public Property SeriesColour As Int32

    <Required(ErrorMessage:="Please enter the Name of the Series that will be displayed in the Legends of Graphs of this Sequence.")> _
    <Display(Name:="Series Name")> _
    Public Property SeriesName As String

    <Required(ErrorMessage:="Please enter the text that will go in the Header of the Table Column of this View Sequence.")> _
    <Display(Name:="Table Header")> _
    Public Property TableHeader As String

    <Required(ErrorMessage:="Please select the Type of Series that will be displayed in the Graph which shows One Week of Measurements")> _
    <Display(Name:="Week Series")> _
    Public Property WeekViewSeriesType As String

End Class

#End Region

#Region "Monitor"

<MetadataType(GetType(MonitorMetadata))> _
Partial Public Class Monitor
End Class
Public Class MonitorMetadata

    <Required(ErrorMessage:="Please specify the Category of this Monitor.")> _
    <Display(Name:="Category")> _
    <DataType(DataType.Text)> _
    Public Property Category As String

    <Display(Name:="Current Location")> _
    Public Property CurrentLocation As MonitorLocation

    '<Display(Name:="Current Settings")> _
    'Public Property CurrentSettings As MonitorSettings

    <Display(Name:="Documents")> _
    Public Property Documents As IEnumerable(Of Document)

    Public Property Id As Int32

    <Required> _
    Public Property RequiresCalibration As String

    <Display(Name:="Last Field Calibration")> _
    <DataType(DataType.Date)> _
    Public Property LastFieldCalibration As IEnumerable(Of DateTime)

    <Display(Name:="Last Full Calibration")> _
    <DataType(DataType.Date)> _
    Public Property LastFullCalibration As IEnumerable(Of DateTime)

    <Required(ErrorMessage:="Please specify the Manufacturer of this Monitor.")> _
    <Display(Name:="Manufacturer")> _
    <DataType(DataType.Text)> _
    Public Property Manufacturer As String

    <Display(Name:="Measurement Files")> _
    Public Property MeasurementFiles As IEnumerable(Of MeasurementFile)

    <Display(Name:="Measurements")> _
    Public Property Measurements As IEnumerable(Of Measurement)

    <Required(ErrorMessage:="Please select the Measurement Type of this Monitor.")> _
    <Display(Name:="Measurement Type")> _
    Public Property MeasurementType As MeasurementType

    Public Property MeasurementTypeId As Int32

    <Required(ErrorMessage:="Please enter the Model of this Monitor.")> _
    <Display(Name:="Model")> _
    <DataType(DataType.Text)> _
    Public Property Model As String

    <Required(ErrorMessage:="Please enter the Name of this Monitor. The name must be Unique.")> _
    <RegularExpression(reLettersNumbersSpacesHyphens,
        ErrorMessage:="Please enter a monitor name containing only alphanumeric characters, spaces, hyphens and commas." + vbCrLf +
                      "Any hypens must be preceded and followed by a space (e.g. 'SECN0001 - ABC').")> _
    <Display(Name:="Monitor Name")> _
    <DataType(DataType.Text)> _
    Public Property MonitorName As String

    <Display(Name:="Next Full Calibration")> _
    <DataType(DataType.Date)> _
    Public Property NextFullCalibration As IEnumerable(Of DateTime)

    <Display(Name:="Owner Organisation")> _
    Public Property OwnerOrganisation As Organisation

    Public Property OwnerOrganisationId As Int32

    <Required(ErrorMessage:="Please enter the Serial Number of this Monitor.")> _
    <Display(Name:="Serial Number")> _
    <DataType(DataType.Text)> _
    Public Property SerialNumber As String

    Public Property DeploymentRecords As IEnumerable(Of MonitorDeploymentRecord)

    Public Property CurrentStatus As MonitorStatus

End Class

#End Region

#Region "MonitorDeploymentRecord"

<MetadataType(GetType(MonitorDeploymentRecordMetadata))> _
Partial Public Class MonitorDeploymentRecord
End Class
Public Class MonitorDeploymentRecordMetadata

    <DataType(DataType.Date)> _
    <DisplayFormat(DataFormatString:="{0:yyyy-MM-dd}", ApplyFormatInEditMode:=True)> _
    Public Property DeploymentEndDate As Nullable(Of Date)

    <Required(ErrorMessage:="Please enter the Date when the Monitor was installed.")> _
    <DataType(DataType.Date)> _
    <DisplayFormat(DataFormatString:="{0:yyyy-MM-dd}", ApplyFormatInEditMode:=True)> _
    Public Property DeploymentStartDate As Nullable(Of Date)

    Public Property Id As Int32

    <Required> _
    Public Property Monitor As Monitor

    Public Property MonitorId As Int32

    <Required> _
    Public Property MonitorLocation As MonitorLocation

    Public Property MonitorLocationId As Int32

    <Required> _
    Public Property MonitorSettings As MonitorSettings

End Class

#End Region

#Region "MonitorLocation"

<MetadataType(GetType(MonitorLocationMetadata))> _
Partial Public Class MonitorLocation
End Class
Public Class MonitorLocationMetadata

    <Required> _
    <Display(Name:="Location Name")> _
    <RegularExpression(
        reLettersNumbersSpacesHyphens,
        ErrorMessage:="Please enter a location name containing only alphanumeric characters, spaces, hyphens and commas." + vbCrLf +
        "Any hypens must be preceded and followed by a space (e.g. '1 - 14')."
    )> _
    <DataType(DataType.Text)> _
    Public Property MonitorLocationName As String

    <Required> _
    <Display(Name:="Coordinates")> _
    Public Property MonitorLocationGeoCoords As MonitorLocationGeoCoords

    <Required> _
    <Display(Name:="Height Above Ground")> _
    Public Property HeightAboveGround As Double

    <Required> _
    <Display(Name:="Facade Location?")> _
    Public Property IsAFacadeLocation As Boolean

End Class

#End Region

#Region "MonitorSettings"

<MetadataType(GetType(MonitorSettingsMetadata))> _
Partial Public Class MonitorSettings
End Class
Public Class MonitorSettingsMetadata

    <Display(Name:="Additional Info 1")> _
    <DataType(DataType.MultilineText)> _
    Public Property AdditionalInfo1 As String

    <Display(Name:="Additional Info 2")> _
    <DataType(DataType.MultilineText)> _
    Public Property AdditionalInfo2 As String

    <Display(Name:="Air Quality Setting")> _
    Public Property AirQualitySetting As AirQualitySetting

    <Display(Name:="Noise Setting")> _
    Public Property NoiseSetting As NoiseSetting

    <Display(Name:="Vibration Setting")> _
    Public Property VibrationSetting As VibrationSetting

    Public Property Id As Int32

    '<Required> _
    '<Display(Name:="Monitor")> _
    'Public Property Monitor As Monitor

    Public Property DeploymentRecord As MonitorDeploymentRecord

End Class

#End Region

#Region "NoiseSetting"

<MetadataType(GetType(NoiseSettingMetadata))> _
Partial Public Class NoiseSetting
End Class
Public Class NoiseSettingMetadata

    Public Property Id As Int32

    <Display(Name:="Microphone Serial Number")> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property MicrophoneSerialNumber As String = ""

    <Display(Name:="Dynamic Range Lower Level")> _
    Public Property DynamicRangeLowerLevel As Double

    <Display(Name:="Dynamic Range Upper Level")> _
    Public Property DynamicRangeUpperLevel As Double

    <Display(Name:="Wind Screen Correction")> _
    Public Property WindScreenCorrection As Double

    <Display(Name:="Alarm Trigger Level")> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <AllowHtml> _
    Public Property AlarmTriggerLevel As String = ""

    <Display(Name:="Frequency Weighting")> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property FrequencyWeighting As String = ""

    <Display(Name:="Time Weighting")> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property TimeWeighting As String = ""

    <Display(Name:="Sound Recording")> _
    Public Property SoundRecording As Boolean

    Public Property MonitorSettings As MonitorSettings

End Class

#End Region

#Region "Organisation"

<MetadataType(GetType(OrganisationMetadata))> _
Partial Public Class Organisation
End Class
Public Class OrganisationMetadata

    <Required(ErrorMessage:="Please enter the business address of this Organisation.")> _
    <Display(Name:="Address")> _
    <DataType(DataType.MultilineText)> _
    Public Property Address As String

    <Display(Name:="Authored Documents")> _
    Public Property AuthoredDocuments As IEnumerable(Of Document)

    Public Property Contacts As IEnumerable(Of Contact)

    <Required(ErrorMessage:="Please enter the Full Name of this Organisation.")> _
    Public Property FullName As String

    Public Property Id As Int32

    <Required(ErrorMessage:="Please select the Type of Organisation from the list.")> _
    Public Property OrganisationType As OrganisationType

    Public Property OrganisationTypeId As Int32

    Public Property OwnedMonitors As IEnumerable(Of Monitor)

    Public Property Projects As IEnumerable(Of Project)

    Public Property ProjectsAsClient As IEnumerable(Of Project)

    <Required(ErrorMessage:="Please enter a Short Name for the Organisation. This Name will be used for navigation around the website.")> _
    <RegularExpression(reLettersNumbersSpacesHyphens, ErrorMessage:="Please enter a ShortName containing only alphanumeric characters, spaces, and hyphens. Any hyphens must be preceded and followed by a space (e.g. 1 - 14).")> _
    Public Property ShortName As String

End Class

#End Region

#Region "Project"

<MetadataType(GetType(ProjectMetadata))> _
Partial Public Class Project
End Class
Public Class ProjectMetadata

    <Display(Name:="Assessment Criteria")> _
    Public Property AssessmentCriteria As IEnumerable(Of AssessmentCriterionGroup)

    <Required(ErrorMessage:="Please select the Client Organisation from the list.")> _
    <Display(Name:="Client Organisation")> _
    Public Property ClientOrganisation As Organisation

    Public Property ClientOrganisationId As Int32

    <Display(Name:="Contacts")> _
    Public Property Contacts As IEnumerable(Of Contact)

    <Required(ErrorMessage:="Please select the Country the Project is in.")> _
    <Display(Name:="Country")> _
    Public Property Country As Country

    Public Property CountryId As Int32

    <Required> _
    <Display(Name:="Standard Weekly Working Hours")> _
    Public Property StandardWeeklyWorkingHours As StandardWeeklyWorkingHours

    <Required(ErrorMessage:="Please enter the Full Name of the Project.")> _
    <Display(Name:="Full Name")> _
    <DataType(DataType.Text)> _
    Public Property FullName As String

    Public Property Id As Int32

    <Display(Name:="Map Link")> _
    <DataType(DataType.Url)> _
    Public Property MapLink As String

    <Display(Name:="Measurements")> _
    Public Property Measurements As IEnumerable(Of Measurement)

    <Display(Name:="Measurement Views")> _
    Public Property MeasurementViews As IEnumerable(Of MeasurementView)

    <Display(Name:="Monitor Locations")> _
    Public Property MonitorLocations As IEnumerable(Of MonitorLocation)

    <Required(ErrorMessage:="Please select Organisations who will be involved with the Project.")> _
    <Display(Name:="Organisations (non-client)")> _
    Public Property Organisations As IEnumerable(Of Organisation)

    <Required> _
    <Display(Name:="Project Location")> _
    Public Property ProjectGeoCoords As ProjectGeoCoords

    Public Property ProjectGeoCoordsId As Int32

    <Required(ErrorMessage:="Please enter a Project Number.")> _
    <Display(Name:="Project Number")> _
    <DataType(DataType.Text)> _
    Public Property ProjectNumber As String

    <Required(ErrorMessage:="Please enter a Short Name for the Project. This Name will be used for navigation around the website.")> _
    <Display(Name:="Short Name")> _
    <RegularExpression(reLettersNumbersSpacesHyphens, ErrorMessage:="Short Name must contain only letters, spaces and hyphens. Any hyphens must be preceded and followed by a space (e.g. 1 - 14).")> _
    <DataType(DataType.Text)> _
    Public Property ShortName As String

    <Display(Name:="Varied Weekly Working Hours")> _
    Public Property VariedWeeklyWorkingHours As IEnumerable(Of VariedWeeklyWorkingHours)

End Class

#End Region

#Region "PublicHoliday"

<MetadataType(GetType(PublicHolidayMetadata))> _
Partial Public Class PublicHoliday
End Class
Public Class PublicHolidayMetadata

    <Required(ErrorMessage:="Please select the Country which this Public Holiday is for.")> _
    Public Property Country As Country

    Public Property CountryId As Int32

    <Required(ErrorMessage:="Please enter the Date of this Holiday.")> _
    <DataType(DataType.Date)> _
    Public Property HolidayDate As DateTime

    <Required(ErrorMessage:="Please enter the Name of this Holiday.")> _
    <DataType(DataType.Text)> _
    Public Property HolidayName As String

    Public Property Id As Int32

End Class

#End Region

#Region "UserAccessLevel"

<MetadataType(GetType(UserAccessLevelMetadata))> _
Partial Public Class UserAccessLevel
End Class
Public Class UserAccessLevelMetadata


    <Required(ErrorMessage:="Please enter the Name of this User Access Level. The name must be Unique.")> _
    <RegularExpression(reLettersNumbersSpacesHyphens,
    ErrorMessage:="Please enter an Access Level Name containing only alphanumeric characters, spaces, hyphens and commas." + vbCrLf +
                  "Any hypens must be preceded and followed by a space (e.g. 'SECN0001 - ABC').")> _
    <Display(Name:="Access Level Name")> _
    <DataType(DataType.Text)> _
    Public Property AccessLevelName As String

    <Required> _
    <Display(Name:="View")> _
    Public Property CanViewAssessments As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateAssessmentCriteria As Boolean

    <Required> _
    <Display(Name:="View")> _
    Public Property CanViewAssessmentCriteria As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditAssessmentCriteria As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteAssessmentCriteria As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateCalculationFilters As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewCalculationFilterDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewCalculationFilterList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditCalculationFilters As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteCalculationFilters As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateContacts As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewContactDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewContactList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditContacts As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteContacts As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateCountries As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewCountryDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewCountryList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditCountries As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteCountries As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateDocuments As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewDocumentDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewDocumentList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditDocuments As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteDocuments As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateDocumentTypes As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewDocumentTypeDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewDocumentTypeList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditDocumentTypes As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteDocumentTypes As Boolean

    <Required> _
    <Display(Name:="View")> _
    Public Property CanViewMeasurements As Boolean

    <Required> _
    <Display(Name:="Upload")> _
    Public Property CanUploadMeasurements As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteMeasurements As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewMeasurementCommentList As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateMeasurementComments As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteMeasurementComments As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateMeasurementCommentTypes As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewMeasurementCommentTypeDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewMeasurementCommentTypeList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditMeasurementCommentTypes As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteMeasurementCommentTypes As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewMeasurementFileDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewMeasurementFileList As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteMeasurementFiles As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateMeasurementMetrics As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewMeasurementMetricDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewMeasurementMetricList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditMeasurementMetrics As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteMeasurementMetrics As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateMeasurementViews As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewMeasurementViewDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewMeasurementViewList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditMeasurementViews As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteMeasurementViews As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateMonitors As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewMonitorDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewMonitorList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditMonitors As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteMonitors As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateDeploymentRecords As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewMonitorDeploymentRecordDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewMonitorDeploymentRecordList As Boolean

    <Required> _
    <Display(Name:="End")> _
    Public Property CanEndMonitorDeployments As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteMonitorDeploymentRecords As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateMonitorLocations As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewMonitorLocationDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewMonitorLocationList As Boolean

    <Required> _
    <Display(Name:="View Select")> _
    Public Property CanViewSelectMonitorLocations As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditMonitorLocations As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteMonitorLocations As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateOrganisations As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewOrganisationDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewOrganisationList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditOrganisations As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteOrganisations As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateOrganisationTypes As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewOrganisationTypeDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewOrganisationTypeList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditOrganisationTypes As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteOrganisationTypes As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateProjects As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewProjectDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewProjectList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditProjects As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteProjects As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreatePublicHolidays As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeletePublicHolidays As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateSystemMessages As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteSystemMessages As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditSystemMessages As Boolean

    <Required> _
    <Display(Name:="View")> _
    Public Property CanViewSystemMessages As Boolean


    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateUsers As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewUserDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewUserList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditUsers As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteUsers As Boolean

    <Required> _
    <Display(Name:="Create")> _
    Public Property CanCreateUserAccessLevels As Boolean

    <Required> _
    <Display(Name:="View Details")> _
    Public Property CanViewUserAccessLevelDetails As Boolean

    <Required> _
    <Display(Name:="View List")> _
    Public Property CanViewUserAccessLevelList As Boolean

    <Required> _
    <Display(Name:="Edit")> _
    Public Property CanEditUserAccessLevels As Boolean

    <Required> _
    <Display(Name:="Delete")> _
    Public Property CanDeleteUserAccessLevels As Boolean

End Class

#End Region

#Region "VariedWeeklyWorkingHours"

<MetadataType(GetType(VariedWeeklyWorkingHoursMetadata))> _
Partial Public Class VariedWeeklyWorkingHours
End Class
Public Class VariedWeeklyWorkingHoursMetadata

    <Display(Name:="Available Measurement Views")> _
    Public Property AvailableMeasurementViews As IEnumerable(Of MeasurementView)

    <DataType(DataType.Date)> _
    Public Property EndDate As DateTime

    Public Property Id As Int32

    Public Property Project As Project


    Public Property ProjectId As Int32

    <DataType(DataType.Date)> _
    Public Property StartDate As DateTime

    Public Property VariedDailyWorkingHours As IEnumerable(Of VariedDailyWorkingHours)

End Class

#End Region

#Region "VibrationSetting"

<MetadataType(GetType(VibrationSettingMetadata))> _
Partial Public Class VibrationSetting
End Class
Public Class VibrationSettingMetadata

    Public Property Id As Int32

    <Display(Name:="Alarm Trigger Level")> _
    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    Public Property AlarmTriggerLevel As String = ""

    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <Display(Name:="XChannel Weighting")> _
    Public Property XChannelWeighting As String

    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <Display(Name:="YChannel Weighting")> _
    Public Property YChannelWeighting As String

    <AllowHtml> _
    <DisplayFormat(ConvertEmptyStringToNull:=False)> _
    <Display(Name:="ZChannel Weighting")> _
    Public Property ZChannelWeighting As String

    Public Property MonitorSettings As MonitorSettings

End Class

#End Region
