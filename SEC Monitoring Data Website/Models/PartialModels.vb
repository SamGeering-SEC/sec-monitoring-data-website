Imports libSEC
Imports System.Data.SqlClient
Imports System.ComponentModel

Partial Public Class AssessmentCriterionGroup

    Implements IRoutable, IDeletable


    Public ReadOnly Property getDescription As String

        Get
            Return "# " + ThresholdAggregateDuration.AggregateDurationName + "s of exceedances in " + AssessmentPeriodDurationCount.ToString + " " + AssessmentPeriodDurationType.DurationTypeName + "s"
        End Get

    End Property

    Public Function getRouteName() As String Implements IRoutable.getRouteName

        Return GroupName.ToRouteName

    End Function

    Public ReadOnly Property getHtmlName As String
        Get
            Return LCase(GroupName).Replace(" ", "_")
        End Get
    End Property

    ''' <summary>
    ''' Returns the Start Date of the Assessment based on the given End Date and the Duration Type and Count of the Assessment.
    ''' </summary>
    ''' <param name="AssessmentEndDate">A user-defined End Date for the Assessment</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getAssessmentStartDate(AssessmentEndDate As Date) As Date

        Select Case AssessmentPeriodDurationType.DurationTypeName
            Case "Day"
                Return AssessmentEndDate.AddDays(-AssessmentPeriodDurationCount + 1)
            Case "Working Day"
                Dim numWorkingDays = 0
                Dim currentDate = AssessmentEndDate
                While numWorkingDays < AssessmentPeriodDurationCount
                    If Project.isWorkingDay(currentDate) = True Then numWorkingDays += 1
                    currentDate = currentDate.AddDays(-1)
                End While
                currentDate = currentDate.AddDays(1)
                Return currentDate
            Case "Week"
                Return AssessmentEndDate.AddDays(-7 * AssessmentPeriodDurationCount).AddDays(1)
            Case "Month"
                Return AssessmentEndDate.AddMonths(-AssessmentPeriodDurationCount).AddDays(1)
            Case Else
                Throw New ArgumentException("Invalid AssessmentEndDate input to getAssessmentStartDate function.")
        End Select

    End Function

    Public Function getAssessmentEndDate(AssessmentStartDate As Date) As Date

        Select Case AssessmentPeriodDurationType.DurationTypeName
            Case "Day"
                Return AssessmentStartDate.AddDays(AssessmentPeriodDurationCount - 1)
            Case "Working Day"
                Dim numWorkingDays = 0
                Dim currentDate = AssessmentStartDate
                While numWorkingDays < AssessmentPeriodDurationCount
                    If Project.isWorkingDay(currentDate) = True Then numWorkingDays += 1
                    currentDate = currentDate.AddDays(1)
                End While
                currentDate = currentDate.AddDays(-1)
                Return currentDate
            Case "Week"
                Return AssessmentStartDate.AddDays(7 * AssessmentPeriodDurationCount).AddDays(-1)
            Case "Month"
                Return AssessmentStartDate.AddMonths(AssessmentPeriodDurationCount).AddDays(-1)
            Case Else
                Throw New ArgumentException("Invalid AssessmentStartDate input to getAssessmentStartDate function.")
        End Select

    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.AssessmentCriteria.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class AssessmentCriterion

    Public Function ThresholdTypeSymbol() As String

        If ThresholdType.ThresholdTypeName = "Greater Than" Then
            Return ">"
        End If
        Return ">="

    End Function

    Public Function getLowerBoundLTorLE() As String

        If ThresholdType.ThresholdTypeName = "Greater Than" Then
            Return "<"
        End If
        Return "<="

    End Function

    Public Function getUpperBoundLTorLE() As String

        If ThresholdType.ThresholdTypeName = "Greater Than" Then
            Return "<="
        End If
        Return "<"

    End Function

    Public Function GetApplicableDateTimeRanges(startDate As Date, endDate As Date) As IEnumerable(Of DateTimeRange)

        Dim dateTimeRanges As New List(Of DateTimeRange)
        For Each d In DateList(startDate, endDate)
            Dim timeWindow = CalculationFilter.getCalculationTimeWindow(d)
            If Not timeWindow Is Nothing Then
                dateTimeRanges.Add(New DateTimeRange With {.StartDateTime = timeWindow.StartDateTime, .EndDateTime = timeWindow.EndDateTime})
            End If
        Next

        Return dateTimeRanges

    End Function

End Class
Partial Public Class CalculationFilter

    Implements IRoutable, IDeletable


    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return FilterName.ToRouteName
    End Function

    Public ReadOnly Property getDuration As Double
        Get
            Dim twst = CDate(TimeWindowStartTime).TimeOnly.ToOADate
            Dim twet = CDate(TimeWindowEndTime).TimeOnly.ToOADate

            Return If(twst >= twet, twet + 1 - twst, twet - twst)

        End Get
    End Property
    Public Function getStartDateTimes(ForStartDate As Date) As IEnumerable(Of Date)

        Dim startTime = CDate(TimeWindowStartTime).TimeOnly.ToOADate
        Dim endTime = If(isOvernight, 1, 0) + CDate(TimeWindowEndTime).TimeOnly.ToOADate
        Dim sdts As New List(Of Date)
        Dim oneMilli = 1 / (24 * 60 * 60 * 1000)

        If ApplicableDaysOfWeek.Select(Function(d) d.DayName).ToList.Contains(ForStartDate.DayName) Then
            Dim tStep As Double = If(getDuration > TimeStep And TimeStep > 0, TimeStep, getDuration)
            For t = startTime To endTime - TimeBase + tStep / 100 Step tStep ' + tStep / 100 is to prevent rounding errors from missing one hour
                sdts.Add(ForStartDate.DateOnly.AddDays(t))
            Next
        End If

        Return sdts


    End Function
    Public Function isOvernight() As Boolean

        Return CDate(TimeWindowEndTime).TimeOnly <= CDate(TimeWindowStartTime).TimeOnly

    End Function
    Public Function is24hr() As Boolean

        Return getDuration = 1

    End Function

    ''' <summary>
    ''' Returns a new CalculationFilterTimeWindow for the CalculationFilter, 
    ''' using ForDate as the date on which the CalculationFilter's TimeWindowStartTime begins
    ''' </summary>
    ''' <param name="ForDate">The date on which the CalculationFilter is assumed to begin application.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getCalculationTimeWindow(ForDate As Date) As CalculationFilterTimeWindow

        If ApplicableDaysOfWeek.Select(
            Function(d) d.DayName
        ).ToList.Contains(ForDate.DayName) = False Then Return Nothing

        If UseTimeWindow = False Then Return New CalculationFilterTimeWindow With {
            .StartDateTime = ForDate,
            .EndDateTime = ForDate.AddDays(1)
        }

        Dim cftw As New CalculationFilterTimeWindow With {
            .StartDateTime = ForDate.Add(TimeWindowStartTime.ToTimeSpan),
            .EndDateTime = ForDate.Add(TimeWindowEndTime.ToTimeSpan)
        }

        If isOvernight() Then cftw.EndDateTime = cftw.EndDateTime.AddDays(1)

        Return cftw

    End Function

    Public Class CalculationFilterTimeWindow
        Public Property StartDateTime As Date
        Public Property EndDateTime As Date

    End Class


    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.MeasurementViewSequenceSettings.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class Contact

    Implements IRoutable, IDeletable


    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return ContactName.ToRouteName
    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.ExcludedDocuments.Count > 0 OrElse _
           Me.MeasurementComments.Count > 0 OrElse _
           Me.MeasurementFiles.Count > 0 OrElse _
           Me.Projects.Count > 0 OrElse _
           Me.User IsNot Nothing Then Return False

        Return True

    End Function

End Class
Partial Public Class Country

    Implements IRoutable, IDeletable

    Public Function getRouteName() As String Implements IRoutable.getRouteName

        Return CountryName.ToRouteName

    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.Projects.Count > 0 OrElse _
           Me.PublicHolidays.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class Document

    Implements IDeletable

    Public Function getTitle() As String
        Return Title.ToRouteName
    End Function
    Public Function getFileName() As String
        Return FilePath.Split("@")(0)
    End Function
    Public Function getUploadDate() As String
        Return FilePath.Split("@")(1)
    End Function
    Public Function getUploadTime() As String
        Return FilePath.Split("@")(2).Split(".")(0)
    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.ExcludedContacts.Count > 0 OrElse _
           Me.MonitorLocations.Count > 0 OrElse _
           Me.Monitors.Count > 0 OrElse _
           Me.Projects.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class DocumentType

    Implements IRoutable, IDeletable

    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return DocumentTypeName.ToRouteName
    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.Documents.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class Measurement

    Implements IMeasurement, IDeletable

#Region "IMeasurement Implementation"

    Public ReadOnly Property getDuration As Double Implements IMeasurement.getDuration
        Get
            Return _Duration
        End Get
    End Property
    Public ReadOnly Property getLevel As Double Implements IMeasurement.getLevel
        Get
            Return _Level
        End Get
    End Property
    Public ReadOnly Property getStartDateTime As Date Implements IMeasurement.getStartDateTime
        Get
            Return _StartDateTime
        End Get
    End Property
    Public ReadOnly Property getMetric As MeasurementMetric Implements IMeasurement.getMetric
        Get
            Return MeasurementMetric
        End Get
    End Property
    Public ReadOnly Property getMonitor As Monitor Implements IMeasurement.getMonitor
        Get
            Return Monitor
        End Get
    End Property
    Public ReadOnly Property getMonitorLocation As MonitorLocation Implements IMeasurement.getMonitorLocation
        Get
            Return MonitorLocation
        End Get
    End Property
    Public ReadOnly Property getProject As Project Implements IMeasurement.getProject
        Get
            Return Project
        End Get
    End Property

#End Region

    Public ReadOnly Property getEndDateTime As Date Implements IMeasurementScalarData.getEndDateTime
        Get
            Return _StartDateTime.AddDays(_Duration)
        End Get
    End Property

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.MeasurementComments.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class MeasurementComment

    Implements IDeletable

    Public ReadOnly Property getFirstMeasurementStartDateTime As Nullable(Of Date)
        Get
            Try
                Dim mDAL As New EFMeasurementsDAL
                Return mDAL.GetMeasurementCommentStartDateTime(Id)
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Public ReadOnly Property getLastMeasurementStartDateTime As Nullable(Of Date)
        Get
            Try
                Dim mDAL As New EFMeasurementsDAL
                Return mDAL.GetMeasurementCommentEndDateTime(Id)
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property
    Public ReadOnly Property getLastMeasurementEndDateTime As Nullable(Of Date)
        Get
            Try
                Dim mDAL As New EFMeasurementsDAL
                Return mDAL.GetMeasurementCommentEndDateTime(Id)
            Catch ex As Exception
                Return Nothing
            End Try
        End Get
    End Property

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.MonitorLocation IsNot Nothing OrElse _
           Me.Measurements.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class MeasurementCommentType

    Implements IRoutable, IDeletable

    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return CommentTypeName.ToRouteName
    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.Comments.Count > 0 OrElse _
           Me.ExcludedMeasurementViews.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class MeasurementFile

    Implements IRoutable, IDeletable

    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return (MeasurementFileName.Replace(" ", "-") + Format(UploadDateTime, "_yyyyMMddHHmmss"))
    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        Dim dal = New EFMeasurementsDAL
        Return dal.MeasurementFileCanBeDeleted(Id)

    End Function

End Class
Partial Public Class MeasurementFileType

    Public Function getHtmlName() As String

        Dim htmlName = LCase(FileTypeName).Replace(" ", "")
        ' Deal with Spreadsheet Template suffices
        If htmlName.Contains("(") = True Then
            Dim suffixPos = InStr(htmlName, "(")
            htmlName = Left(htmlName, suffixPos - 1)
        End If
        Return htmlName

    End Function
    Public Function getUploadViewName() As String

        Dim viewName = "Upload_" + FileTypeName.Replace(" ", "")
        ' Deal with Spreadsheet Template suffices
        If viewName.Contains("(") = True Then
            Dim suffixPos = InStr(viewName, "(")
            viewName = Left(viewName, suffixPos - 1)
        End If
        Return viewName

    End Function

End Class
Partial Public Class MeasurementMetric

    Implements IRoutable, IDeletable

    Public Function getRouteName() As String Implements IRoutable.getRouteName

        Return MetricName.ToRouteName

    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        Dim mdal = New EFMeasurementsDAL
        If Me.CalculationFilters.Count > 0 OrElse _
           mdal.MeasurementMetricHasMeasurements(Me.Id) Then Return False

        Return True

    End Function

End Class
Partial Public Class MeasurementType

    Implements IRoutable

    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return MeasurementTypeName.ToRouteName
    End Function

End Class
Partial Public Class MeasurementView

    Implements IRoutable, IDeletable

    Public ReadOnly Property getGroups As IEnumerable(Of MeasurementViewGroup)
        Get
            Return MeasurementViewGroups.OrderBy(Function(mvg) mvg.GroupIndex)
        End Get
    End Property
    Public ReadOnly Property getSequenceSettings As IEnumerable(Of MeasurementViewSequenceSetting)
        Get
            Dim mvss As New List(Of MeasurementViewSequenceSetting)
            For Each g In getGroups
                mvss.AddRange(g.getSequenceSettings)
            Next
            Return mvss
        End Get

    End Property
    Public ReadOnly Property getHtmlName As String
        Get
            Return LCase(ViewName).Replace(" ", "_")
        End Get
    End Property


    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return ViewName.ToRouteName
    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.ExcludingMeasurementCommentTypes.Count > 0 OrElse
           Me.Projects.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class MeasurementViewGroup

    Implements IDeletable

    Public ReadOnly Property getSequenceSettings As IEnumerable(Of MeasurementViewSequenceSetting)
        Get
            Return MeasurementViewSequenceSettings.OrderBy(Function(ss) ss.SequenceIndex)
        End Get
    End Property
    Public ReadOnly Property getTabName As String
        Get
            Return "Group " + GroupIndex.ToString
        End Get
    End Property
    Public ReadOnly Property getTabHtmlName As String
        Get
            Return LCase(getTabName).Replace(" ", "_")
        End Get
    End Property

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.MeasurementViewSequenceSettings.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class MeasurementViewSequenceSetting

    Implements IDeletable

    Public ReadOnly Property GetColour As System.Drawing.Color
        Get

            Return System.Drawing.ColorTranslator.FromHtml(SeriesColour)

        End Get
    End Property
    Public ReadOnly Property getTabName As String
        Get
            Return "Sequence " + SequenceIndex.ToString
        End Get
    End Property
    Public ReadOnly Property getTabHtmlName As String
        Get
            Return LCase(getTabName).Replace(" ", "_")
        End Get
    End Property

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        Return True

    End Function

End Class
Partial Public Class Monitor

    Implements IRoutable, IDeletable

    Public Function getRouteName() As String Implements IRoutable.getRouteName

        Return MonitorName.ToRouteName

    End Function

    Public Function isDeployed() As Boolean

        If DeploymentRecords.Where(Function(mdr) mdr.DeploymentEndDate Is Nothing).Count = 1 Then Return True
        Return False

    End Function
    Public Function getCurrentDeploymentRecord() As MonitorDeploymentRecord

        If isDeployed() = True Then
            Return DeploymentRecords.Single(Function(mdr) mdr.DeploymentEndDate Is Nothing)
        Else
            Return Nothing
        End If

    End Function
    Public Function getPreviousDeploymentRecords() As IEnumerable(Of MonitorDeploymentRecord)

        Return DeploymentRecords.Where(Function(mdr) mdr.DeploymentEndDate IsNot Nothing).OrderBy(Function(mdr) mdr.DeploymentStartDate)

    End Function
    Public Function getPreviousDeploymentRecords(AtMonitorLocation As MonitorLocation) As IEnumerable(Of MonitorDeploymentRecord)

        If isDeployed() = True Then
            Return DeploymentRecords.Where(Function(mdr) mdr.DeploymentEndDate IsNot Nothing AndAlso mdr.MonitorLocationId = AtMonitorLocation.Id).OrderBy(Function(mdr) mdr.DeploymentStartDate)
        Else
            Return Nothing
        End If

    End Function

    Public Function getFieldCalibrationRecords() As IEnumerable(Of Document)

        Return Documents.Where(Function(d) d.DocumentType.DocumentTypeName = "Field Calibration Record").OrderBy(Function(cr) cr.StartDateTime)

    End Function
    Public Function getCalibrationCertificates() As IEnumerable(Of Document)

        Return Documents.Where(Function(d) d.DocumentType.DocumentTypeName = "Calibration Certificate").OrderBy(Function(cr) cr.StartDateTime)

    End Function
    Public Function getPhotos() As IEnumerable(Of Document)

        Return Documents.Where(Function(d) d.DocumentType.DocumentTypeName = "Photo")

    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.CurrentLocation IsNot Nothing OrElse
            Me.DeploymentRecords.Count > 0 OrElse
            Me.Documents.Count > 0 OrElse
            Me.MeasurementFiles.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class MonitorDeploymentRecord

    Implements IDeletable


    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        Return DeploymentEndDate IsNot Nothing

    End Function

    Public Function getDeploymentIndex() As Integer

        Dim monitorDeploymentRecords = Monitor.DeploymentRecords.OrderBy(Function(mdr) mdr.Id).ToList
        Dim deploymentIndex = monitorDeploymentRecords.IndexOf(Me) + 1
        Return deploymentIndex

    End Function

End Class
Partial Public Class MonitorLocation

    Implements IRoutable, IDeletable

    Public Sub New()
    End Sub
    Public Sub New(Latitude As Double, Longitude As Double)

        MonitorLocationGeoCoords = New MonitorLocationGeoCoords With {.Latitude = Latitude, .Longitude = Longitude}

    End Sub

    Public Sub New(ForProject As Project)

        Me.ProjectId = ForProject.Id
        Me.Project = ForProject
        Me.MonitorLocationGeoCoords = New MonitorLocationGeoCoords With {.Latitude = ForProject.ProjectGeoCoords.Latitude,
                                                                        .Longitude = ForProject.ProjectGeoCoords.Longitude}

    End Sub

    Public ReadOnly Property IsActive As Boolean
        Get
            Return Me.CurrentMonitor Is Nothing
        End Get
    End Property

    Public Function hasDeployedMonitor() As Boolean

        If DeploymentRecords.Where(Function(mdr) mdr.DeploymentEndDate Is Nothing).Count = 1 Then Return True
        Return False

    End Function
    Public Function getCurrentDeploymentRecord() As MonitorDeploymentRecord

        If hasDeployedMonitor() = True Then
            Return DeploymentRecords.Single(Function(mdr) mdr.DeploymentEndDate Is Nothing)
        Else
            Return Nothing
        End If

    End Function
    Public Function getPreviousDeploymentRecords() As IEnumerable(Of MonitorDeploymentRecord)

        Return DeploymentRecords.Where(Function(mdr) mdr.DeploymentEndDate IsNot Nothing).OrderBy(Function(mdr) mdr.DeploymentStartDate)

    End Function
    Public Function getAssessmentCriterionGroups() As IEnumerable(Of AssessmentCriterionGroup)

        Return AssessmentCriteria.Select(Function(ac) ac.AssessmentCriterionGroup).Distinct.OrderBy(Function(g) g.GroupName).ToList

    End Function

    Public Function getFieldCalibrationRecords() As IEnumerable(Of Document)

        Return Documents.Where(Function(d) d.DocumentType.DocumentTypeName = "Field Calibration Record").OrderBy(Function(cr) cr.StartDateTime)

    End Function
    Public Function getCalibrationCertificates() As IEnumerable(Of Document)

        Return Documents.Where(Function(d) d.DocumentType.DocumentTypeName = "Calibration Certificate").OrderBy(Function(cr) cr.StartDateTime)

    End Function
    Public Function getPhotos() As IEnumerable(Of Document)

        Return Documents.Where(Function(d) d.DocumentType.DocumentTypeName = "Photo")

    End Function

    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return MonitorLocationName.ToRouteName
    End Function
    Public ReadOnly Property getHtmlName As String
        Get
            Return "html_" + LCase(MonitorLocationName).Replace(" ", "_").Replace("-", "_").Replace("ç", "c")
        End Get
    End Property

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.CurrentMonitor IsNot Nothing OrElse
            Me.DeploymentRecords.Count > 0 OrElse
            Me.Documents.Count > 0 OrElse
            Me.MeasurementComments.Count > 0 OrElse
            Me.AssessmentCriteria.Count > 0 OrElse
            Me.MeasurementFiles.Count > 0 Then Return False

        Return True

    End Function

    Public Function getFirstMeasurementStartDateTime() As Date

        Return MeasurementFiles.Select(Function(mf) mf.FirstMeasurementStartDateTime).Min()

    End Function
    Public Function getLastMeasurementStartDateTime() As Date

        Return MeasurementFiles.Select(Function(mf) mf.LastMeasurementStartDateTime).Max()

    End Function
    Public Function getLastMeasurementDuration() As Double

        Return MeasurementFiles.Where(
            Function(mf) mf.LastMeasurementStartDateTime = getLastMeasurementStartDateTime()
        ).First().LastMeasurementDuration

    End Function
    Public Function getLastMeasurementEndDateTime() As Date

        Return getLastMeasurementStartDateTime.AddDays(getLastMeasurementDuration)

    End Function

    Public Function hasMeasurements() As Boolean

        Dim mdal = New EFMeasurementsDAL

        Return MeasurementFiles.Where(Function(mf) mf.NumberOfMeasurements > 0).Count > 0

    End Function

End Class
Partial Public Class Organisation

    Implements IRoutable, IDeletable

    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return ShortName.ToRouteName
    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.AuthoredDocuments.Count > 0 OrElse
            Me.Contacts.Count > 0 OrElse
            Me.OwnedMonitors.Count > 0 OrElse
            Me.Projects.Count > 0 OrElse
            Me.ProjectsAsClient.Count > 0 Then Return False

        Return True

    End Function

End Class
Partial Public Class OrganisationType

    Implements IRoutable, IDeletable

    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return OrganisationTypeName.ToRouteName
    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        If Me.Organisations.Count = 0 Then Return True
        Return False

    End Function


End Class
Partial Public Class Project

    Implements IRoutable, IDeletable

    Public Sub New()
    End Sub
    Public Sub New(Latitude As Double, Longitude As Double)

        ProjectGeoCoords = New ProjectGeoCoords With {.Latitude = Latitude, .Longitude = Longitude}

    End Sub

    Public Function getVariations() As IEnumerable(Of VariedWeeklyWorkingHours)

        Return VariedWeeklyWorkingHours.OrderBy(Function(v) v.StartDate)

    End Function

    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return ShortName.ToRouteName
    End Function

    Public Function getWorkingDaysBetween(StartDate As Date, EndDate As Date) As List(Of Date)

        Dim workingDays As New List(Of Date)

        For Each d As Date In DateList(StartDate, EndDate, TimeResolutionType.Day)
            If isWorkingDay(d) Then workingDays.Add(d)
        Next

        Return workingDays

    End Function

    ''' <summary>
    ''' Returns the working hours on the given date. 
    ''' </summary>
    ''' <param name="ForDate">The date to return the working hours for.</param>
    ''' <returns>A DateTimeRange of the working hours if it was a working day, otherwise Nothing.</returns>
    ''' <remarks></remarks>
    Public Function getWorkingHours(ForDate As Date) As DateTimeRange

        Dim dateFound As Boolean = False
        Dim testDayName = ForDate.DayName
        Dim isPublicHoliday = Country.PublicHolidays.Select(Function(ph) ph.HolidayDate).ToList.Contains(ForDate)

        ' Test all Varied Working Hours periods
        For Each vwwh In VariedWeeklyWorkingHours
            ' If Date is in variation period
            If ForDate.DateIsInDateRangeInclusive(vwwh.StartDate, vwwh.EndDate) = True Then
                If isPublicHoliday Then
                    ' Date is a public holiday
                    For Each dwh In vwwh.VariedDailyWorkingHours
                        If dwh.DayOfWeek.DayName = "Public Holiday" Then
                            dateFound = True
                            Return New DateTimeRange With {
                                .StartDateTime = ForDate.Add(dwh.StartTime.TimeOnly.ToTimeSpan),
                                .EndDateTime = ForDate.Add(dwh.EndTime.TimeOnly.ToTimeSpan)
                            }
                        End If
                    Next
                    Return Nothing
                Else
                    ' Date is not a public holiday
                    For Each dwh In vwwh.VariedDailyWorkingHours
                        If dwh.DayOfWeek.DayName = testDayName Then
                            dateFound = True
                            Return New DateTimeRange With {
                                .StartDateTime = ForDate.Add(dwh.StartTime.TimeOnly.ToTimeSpan),
                                .EndDateTime = ForDate.Add(dwh.EndTime.TimeOnly.ToTimeSpan)
                            }
                        End If
                    Next
                    Return Nothing
                End If

            End If
        Next

        ' Use Standard Working Hours
        If isPublicHoliday Then
            ' Date is a public holiday
            For Each dwh In StandardWeeklyWorkingHours.StandardDailyWorkingHours
                If dwh.DayOfWeek.DayName = "Public Holiday" Then
                    Return New DateTimeRange With {
                        .StartDateTime = ForDate.Add(dwh.StartTime.TimeOnly.ToTimeSpan),
                        .EndDateTime = ForDate.Add(dwh.EndTime.TimeOnly.ToTimeSpan)
                        }
                End If
            Next
            Return Nothing
        End If
        ' Date is not a public holiday
        For Each dwh In StandardWeeklyWorkingHours.StandardDailyWorkingHours
            If dwh.DayOfWeek.DayName = testDayName Then
                Return New DateTimeRange With {
                    .StartDateTime = ForDate.Add(dwh.StartTime.TimeOnly.ToTimeSpan),
                    .EndDateTime = ForDate.Add(dwh.EndTime.TimeOnly.ToTimeSpan)
                }
            End If
        Next
        Return Nothing

    End Function
    Public Function getNonWorkingHours(ForDates As IEnumerable(Of Date)) As IEnumerable(Of DateTimeRange)

        Dim lstWorkingHours = getWorkingHours(ForDates)
        lstWorkingHours = lstWorkingHours.Where(Function(wh) wh IsNot Nothing).ToList
        Dim lstNonWorkingHours = New List(Of DateTimeRange)

        ' add gap from start of first date to start of first working period
        Dim firstWorkingPeriod = lstWorkingHours.First
        If firstWorkingPeriod.StartDateTime > ForDates.First Then
            lstNonWorkingHours.Add(
                New DateTimeRange With {
                    .StartDateTime = ForDates.First,
                    .EndDateTime = firstWorkingPeriod.StartDateTime
                }
            )
        End If

        ' add intermediate gaps between working periods
        For d = 0 To lstWorkingHours.Count - 2
            Dim firstPeriod = lstWorkingHours(d)
            Dim secondPeriod = lstWorkingHours(d + 1)
            lstNonWorkingHours.Add(
                New DateTimeRange With {
                    .StartDateTime = firstPeriod.EndDateTime,
                    .EndDateTime = secondPeriod.StartDateTime
                }
            )
        Next

        ' add gap from end of last working period to end of last date
        Dim lastWorkingPeriod = lstWorkingHours.Last
        If lastWorkingPeriod.EndDateTime < ForDates.Last.AddDays(1) Then
            lstNonWorkingHours.Add(
                New DateTimeRange With {
                    .StartDateTime = lastWorkingPeriod.EndDateTime,
                    .EndDateTime = ForDates.Last.AddDays(1)
                }
            )
        End If

        Return lstNonWorkingHours

    End Function



    ''' <summary>
    ''' Returns a list of DateTimeRanges representing the working hours for each day of the given list of Dates.
    ''' </summary>
    ''' <param name="ForDates">A list of dates to return working hours for.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function getWorkingHours(ForDates As IEnumerable(Of Date)) As IEnumerable(Of DateTimeRange)

        Dim lstWorkingHours As New List(Of DateTimeRange)

        For Each testDate In ForDates
            lstWorkingHours.Add(getWorkingHours(testDate))
        Next

        Return lstWorkingHours

    End Function

    Public Function isWorkingDay(TestDate As Date) As Boolean

        If getWorkingHours(TestDate) Is Nothing Then Return False
        Return True

    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        Dim mdal As New EFMeasurementsDAL

        If Me.AssessmentCriteria.Count > 0 OrElse _
            Me.Contacts.Count > 0 OrElse _
            Me.Documents.Count > 0 OrElse _
            Me.MeasurementViews.Count > 0 OrElse _
            Me.MonitorLocations.Count > 0 OrElse _
            Me.Organisations.Count > 0 OrElse _
            Me.VariedWeeklyWorkingHours.Count > 0 OrElse _
            mdal.ProjectHasMeasurements(Me.Id) Then Return False

        Return True

    End Function

    Public Function hasAssessmentGroupNamed(assessmentCriterionGroupName As String) As Boolean

        Return AssessmentCriteria.Select(Function(acg) acg.GroupName).ToList.Contains(assessmentCriterionGroupName)

    End Function

End Class
Partial Public Class User
    Implements IRoutable, IDeletable

    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return UserName.ToRouteName()
    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        Return True

    End Function

End Class
Partial Public Class UserAccessLevel
    Implements IRoutable, IDeletable


    Public Function getRouteName() As String Implements IRoutable.getRouteName
        Return AccessLevelName.ToRouteName
    End Function

    Public Function canBeDeleted() As Boolean Implements IDeletable.canBeDeleted

        Return Users.Count = 0

    End Function
End Class