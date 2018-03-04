Imports System.IO
Imports System.Globalization
Imports System.Data.OleDb
Imports libSEC.Maths
Imports ExcelDataReader


Namespace MeasurementFileSettings

    Public Class MeasurementFileSettings

        Protected MeasurementsDAL As IMeasurementsDAL
        Protected Measurements As List(Of Measurement)
        Protected ColumnHeaders As String()
        Protected RowStartDateTimes As List(Of DateTime)
        Protected RowDurations As List(Of Double)

        Protected icolDate As Integer = -1, icolTime As Integer = -1, icolDateTime As Integer = -1
        Protected icolDuration As Integer = -1
        Protected icolOverload As Integer = -1, icolUnderload As Integer = -1

#Region "Enums"

        Public Enum FileFormats
            Text ' .csv or .txt files
            Excel ' .xls files
            TextOrExcel ' .csv, .txt or .xls files
        End Enum
        Public Enum DateTimeStampSettings
            InHeader
            InRows
        End Enum
        Public Enum DateTimeStampMarkerSettings
            MeasurementStart ' Time stamp field(s) mark starts of measurements
            MeasurementEnd ' Time stamp field(s) mark ends of measurements
        End Enum
        Public Enum DateTimeFieldSettings
            SingleField ' DateTime field
            TwoFields ' Date and Time fields
        End Enum
        Public Enum DurationSettings
            Field ' Measurements durations are marked by a field
            RowToRow ' Durations need to be calculated by comparing the DateTimes of consecutive measurements
            Header ' Measurements are of equal duration and in the header
        End Enum
        Public Enum DurationFieldSettings
            ' Iff the Duration Setting = Field then this indicates whether the duration of each measurement needs to be calculated from 
            ' EndDateTime - StartDateTime or whether there is a specific field for the duration
            EndDateTime
            Duration
        End Enum
        Public Enum DurationEqualSettings
            NotEqual ' The durations are allowed to be unequal
            Equal ' The durations are not allowed to be unequal
        End Enum
        Public Enum RemoveRepeatsSettings
            DoNotRemove ' Don't remove measurements which start at the same date time
            Remove ' Remove measurements which start at the same date time
        End Enum

#End Region

#Region "Properties"

        Public Property FileFormat As FileFormats
        Public Property ExcelSheetName As String
        Public Property ColumnDelimiter As String ' e.g.  ",", " ", vbTab

        Public Property DateTimeStampSetting As DateTimeStampSettings
        Public Property DateTimeStampMarkerSetting As DateTimeStampMarkerSettings

        Public Property DateTimeFieldSetting As DateTimeFieldSettings
        Public Property MeasurementDateTimeField As String
        Public Property MeasurementDateTimeFormat As String ' e.g. "yyyyMMdd HH:mm" - use "" if not specified
        Public Property MeasurementDateTimeFormat2 As String
        Public Property MeasurementDateField As String
        Public Property MeasurementDateFormat As String ' e.g. "yyyyMMdd" - use "" if not a string
        Public Property MeasurementDateFormat2 As String
        Public Property MeasurementTimeField As String
        Public Property MeasurementTimeFormat As String ' e.g. "HH:mm" - use "" if not a string

        Public Property DurationSetting As DurationSettings
        Public Property DurationField As String
        Public Property DurationFieldFormat As String

        Public Property DurationFieldSetting As DurationFieldSettings ' this is only currently used for the spreadsheet template

        Public Property DurationEqualSetting As DurationEqualSettings
        Public Property RemoveRepeatsSetting As RemoveRepeatsSettings

        Public Property MetricMappings As Dictionary(Of String, MeasurementMetric)

        Public Property OverloadField As String
        Public Property UnderloadField As String

#End Region

#Region "Methods"

#Region "Read File"

        Public Function ReadFile(pathToFile As String) As Boolean

            If File.Exists(pathToFile) = True Then

                Select Case FileFormat
                    Case FileFormats.Excel
                        Return readExcelFile(pathToFile)
                    Case FileFormats.Text
                        Return readTextFile(pathToFile)
                    Case FileFormats.TextOrExcel
                        If LCase(pathToFile).EndsWith(".xls") Or LCase(pathToFile).EndsWith(".xlsx") Then
                            Return readExcelFile(pathToFile)
                        Else
                            Return readTextFile(pathToFile)
                        End If
                End Select

            End If

            Return False

        End Function

#Region "Field Parsers"

        Public Function getDate(DateString As String) As Date

            Select Case MeasurementDateFormat
                Case ""
                    Return Date.Parse(DateString)
                Case Else
                    Try
                        Return Date.ParseExact(DateString, MeasurementDateFormat, CultureInfo.InvariantCulture)
                    Catch ex As Exception
                        Return Date.ParseExact(DateString, MeasurementDateFormat2, CultureInfo.InvariantCulture)
                    End Try
            End Select

        End Function
        Public Function getDateTime(DateTimeString As String) As Date

            Select Case MeasurementDateTimeFormat
                Case ""
                    Return Date.Parse(DateTimeString)
                Case Else
                    Try
                        Return Date.ParseExact(DateTimeString, MeasurementDateTimeFormat, CultureInfo.InvariantCulture)
                    Catch ex As Exception
                        Return Date.ParseExact(DateTimeString, MeasurementDateTimeFormat2, CultureInfo.InvariantCulture)
                    End Try
            End Select

        End Function
        Public Function getDateTime(DateString As String, TimeString As String) As Date

            Dim dateOA As Double, timeOA As Double

            DateString = DateString.Trim()
            TimeString = TimeString.Trim()

            Select Case MeasurementDateFormat
                Case ""
                    dateOA = Date.Parse(DateString).ToOADate
                Case Else
                    Try
                        dateOA = Date.ParseExact(
                            DateString, MeasurementDateFormat, CultureInfo.InvariantCulture
                        ).ToOADate
                    Catch ex As Exception
                        dateOA = Date.ParseExact(
                            DateString, MeasurementDateFormat2, CultureInfo.InvariantCulture
                        ).ToOADate
                    End Try
            End Select

            Select Case MeasurementTimeFormat
                Case ""
                    timeOA = Date.Parse(TimeString).ToOADate
                Case Else
                    timeOA = Date.ParseExact(
                        TimeString, MeasurementTimeFormat, CultureInfo.InvariantCulture
                    ).ToOADate
                    timeOA = timeOA - Int(timeOA)
            End Select

            Return Date.FromOADate(dateOA + timeOA)

        End Function
        Public Function getDuration(DurationString As String) As Double

            Dim durationOA = Date.ParseExact(DurationString, DurationFieldFormat, CultureInfo.InvariantCulture).ToOADate
            durationOA = durationOA - Int(durationOA)
            Return durationOA

        End Function
        Public Function getOverload(OverloadValue As Integer) As Boolean

            Return OverloadValue > 0

        End Function
        Public Function getOverload(OverloadValue As Double) As Boolean

            Return OverloadValue > 0

        End Function
        Public Function getOverload(OverloadString As String) As Boolean

            Return LCase(OverloadString) = "true" Or LCase(OverloadString) = "yes" Or OverloadString = "1"

        End Function
        Public Function getUnderload(UnderloadValue As Integer) As Boolean

            Return UnderloadValue > 0

        End Function
        Public Function getUnderload(UnderloadString As String) As Boolean

            Return LCase(UnderloadString) = "false" Or LCase(UnderloadString) = "no" Or UnderloadString = "0" Or UnderloadString = ""

        End Function

#End Region

        ' Excel file
        Protected Overridable Sub getExcelColumnHeaders(dt As DataTable)

            Dim colHeaders As New List(Of String)
            For col = 0 To dt.Columns.Count - 1
                Dim colName = dt.Columns(col).ColumnName
                If DateTimeFieldSetting = DateTimeFieldSettings.TwoFields Then
                    If colName = MeasurementDateField Then icolDate = col
                    If colName = MeasurementTimeField Then icolTime = col
                Else
                    If colName = MeasurementDateTimeField Then icolDateTime = col
                End If
                If colName = OverloadField Then icolOverload = col
                colHeaders.Add(colName.Replace("#", ".")) ' reading from excel converts . to # for some reason
            Next

            ColumnHeaders = colHeaders.ToArray()

        End Sub
        Protected Overridable Sub getExcelRowStartDateTimesAndDurations(dt As DataTable)

            ' Get StartDateTimes
            Debug.WriteLine("Number of rows in datatable: " + dt.Rows.Count.ToString())
            RowStartDateTimes = New List(Of Date)
            For Each row As DataRow In dt.Rows
                Try
                    If DateTimeFieldSetting = DateTimeFieldSettings.TwoFields Then
                        Dim strRowDate As String = row(icolDate)
                        Dim rowDate As Date = getDate(strRowDate)
                        Dim rowTime As String() = row(icolTime).split(":") ' need to do this because sometimes the time is given as 24:00 which causes an exception
                        RowStartDateTimes.Add(
                            rowDate.AddHours(Val(rowTime(0))) _
                                   .AddMinutes(Val(rowTime(1)))
                        )
                    Else
                        Dim rowStartDateTime = getDateTime(row(icolDateTime))
                        RowStartDateTimes.Add(rowStartDateTime)
                    End If
                Catch ex As Exception
                    Debug.WriteLine("Unable to get DateTime from Row")
                End Try
            Next
            ' Fill in durations
            RowDurations = New List(Of Double)
            For r = 0 To RowStartDateTimes.Count - 2
                RowDurations.Add(RowStartDateTimes(r + 1).ToOADate - RowStartDateTimes(r).ToOADate)
            Next
            RowDurations.Add(RowDurations.Last)
            ' Shift start date times back by durations, if required
            If DateTimeStampMarkerSetting = DateTimeStampMarkerSettings.MeasurementEnd Then
                If DurationEqualSetting = DurationEqualSettings.Equal Then
                    ' Calculate the median duration to use as the offset
                    Dim durations As New List(Of Double)
                    For i = 0 To RowStartDateTimes.Count - 2
                        durations.Add(
                            RowStartDateTimes(i + 1).ToOADate - RowStartDateTimes(i).ToOADate
                        )
                    Next
                    durations.Sort()
                    Dim medianDuration = durations(
                        Int(durations.Count / 2)
                    )
                    ' Offset by the median duration
                    For i = 0 To RowStartDateTimes.Count - 1
                        RowStartDateTimes(i) = RowStartDateTimes(i).AddDays(-medianDuration)
                    Next
                Else
                    For i = 0 To RowStartDateTimes.Count - 1
                        RowStartDateTimes(i) = RowStartDateTimes(i).AddDays(-RowDurations(i))
                    Next
                End If
            End If

        End Sub
        Public Function readExcelFile(pathToFile As String) As Boolean

            Dim conn As OleDbConnection = Nothing
            Dim cmd As OleDbCommand = Nothing
            Dim da As OleDbDataAdapter = Nothing
            Dim dt As New DataTable

            Try
                ' Open file and read to datatable
                Using stream = File.Open(pathToFile, FileMode.Open, FileAccess.Read)
                    Using reader = ExcelReaderFactory.CreateReader(stream)
                        Dim result = reader.AsDataSet(
                            configuration:=New ExcelDataSetConfiguration() With {
                                .UseColumnDataType = True,
                                .ConfigureDataTable = Function(tableReader) New ExcelDataTableConfiguration() With {
                                                          .UseHeaderRow = True
                                }
                            }
                        )
                        dt = result.Tables(ExcelSheetName)
                    End Using
                End Using

                ' Get Date and Time Column Indices and Column Headers
                getExcelColumnHeaders(dt)

                ' Get StartDateTimes and Durations
                getExcelRowStartDateTimesAndDurations(dt)

                ' Read Measurements
                Measurements = New List(Of Measurement)
                For row = 0 To dt.Rows.Count - 1

                    Dim duration = RowDurations(row)
                    Dim startDateTime = RowStartDateTimes(row)
                    Dim overload, underload As Boolean?
                    If OverloadField <> "" And icolOverload > -1 Then
                        Dim ol = dt.Rows(row)(icolOverload)
                        If Not IsDBNull(ol) Then
                            overload = getOverload(ol)
                        End If
                    End If
                    If UnderloadField <> "" And icolUnderload > -1 Then
                        Dim ul = dt.Rows(row)(icolUnderload)
                        If Not IsDBNull(ul) Then
                            underload = getUnderload(ul)
                        End If
                    End If

                    For icol = 0 To ColumnHeaders.Count - 1

                        Dim colHeader = ColumnHeaders(icol)
                        Dim measurementMetricId As Integer = -1
                        For Each mm In MetricMappings
                            If mm.Key.ToLower() = colHeader.ToLower Then measurementMetricId = mm.Value.Id
                        Next
                        If measurementMetricId <> -1 Then

                            Try
                                Dim level = Val(dt.Rows(row)(icol))

                                If level > 0 Then
                                    Dim measurement = New Measurement With {
                                        .StartDateTime = startDateTime,
                                        .Duration = duration,
                                        .MeasurementMetricId = measurementMetricId,
                                        .Level = level,
                                        .Overload = overload,
                                        .Underload = underload
                                    }
                                    Measurements.Add(measurement)
                                End If
                            Catch ex As Exception
                                ' The cell was empty
                            End Try

                        End If

                    Next

                Next

            Catch exc As Exception

                Return False

            End Try

            Return True

        End Function

        ' Text file
        Protected Overridable Sub readTextFileHeader(StreamReader As StreamReader)

            ColumnHeaders = StreamReader.ReadLine.Split(ColumnDelimiter).Select(Function(x) x.Trim(Chr(34))).Where(Function(x) x <> "").ToArray

            ' Assign Date and Time Fields
            Select Case DateTimeFieldSetting
                Case DateTimeFieldSettings.SingleField
                    icolDateTime = Array.IndexOf(ColumnHeaders, MeasurementDateTimeField)
                Case DateTimeFieldSettings.TwoFields
                    icolDate = Array.IndexOf(ColumnHeaders, MeasurementDateField)
                    icolTime = Array.IndexOf(ColumnHeaders, MeasurementTimeField)
            End Select

            ' Assign Duration Field (if applicable)
            If DurationSetting = DurationSettings.Field Then icolDuration = Array.IndexOf(ColumnHeaders, DurationField)

            ' Overload and Underload
            If OverloadField <> "" Then icolOverload = Array.IndexOf(ColumnHeaders, OverloadField)
            If UnderloadField <> "" Then icolUnderload = Array.IndexOf(ColumnHeaders, UnderloadField)

        End Sub
        Protected Overridable Sub getTextFileRowStartDateTimesAndDurations(pathToFile As String)

            Dim sr As New StreamReader(pathToFile)
            readTextFileHeader(sr)

            ' Read Date Times and Durations
            RowStartDateTimes = New List(Of DateTime)
            RowDurations = New List(Of Double)
            While Not sr.EndOfStream
                ' Read line from file
                Dim fileLine = sr.ReadLine.Split(ColumnDelimiter).Select(
                    Function(x) x.Trim(Chr(34))
                ).Where(Function(x) x <> "").ToArray
                ' Get DateTime
                Select Case DateTimeFieldSetting
                    Case DateTimeFieldSettings.SingleField
                        RowStartDateTimes.Add(getDateTime(fileLine(icolDateTime)))
                    Case DateTimeFieldSettings.TwoFields
                        RowStartDateTimes.Add(getDateTime(fileLine(icolDate), fileLine(icolTime)))
                End Select
                ' Get Duration (if there is a field)
                If DurationSetting = DurationSettings.Field Then RowDurations.Add(getDuration(fileLine(icolDuration)))
            End While

            ' Fill in durations (if there is no field)
            If DurationSetting = DurationSettings.RowToRow Then
                For r = 0 To RowStartDateTimes.Count - 2
                    RowDurations.Add(
                        RowStartDateTimes(r + 1).ToOADate - RowStartDateTimes(r).ToOADate
                    )
                Next
                RowDurations.Add(RowDurations.Last)
            End If

            ' Shift start date times back by durations, if required
            If DateTimeStampMarkerSetting = DateTimeStampMarkerSettings.MeasurementEnd Then
                If DurationEqualSetting = DurationEqualSettings.Equal Then
                    ' Calculate the median duration to use as the offset
                    Dim durations As New List(Of Double)
                    For i = 0 To RowStartDateTimes.Count - 2
                        durations.Add(
                            RowStartDateTimes(i + 1).ToOADate - RowStartDateTimes(i).ToOADate
                        )
                    Next
                    durations.Sort()
                    Dim medianDuration = durations(
                        Int(durations.Count / 2)
                    )
                    ' Offset by the median duration
                    For i = 0 To RowStartDateTimes.Count - 1
                        RowStartDateTimes(i) = RowStartDateTimes(i).AddDays(-medianDuration)
                    Next
                Else
                    ' Offset by the difference between this measurement and the next
                    For i = 0 To RowStartDateTimes.Count - 1
                        RowStartDateTimes(i) = RowStartDateTimes(i).AddDays(-RowDurations(i))
                    Next
                End If
            End If

        End Sub
        Protected Overridable Function readTextFile(pathToFile As String) As Boolean

            Try

                Measurements = New List(Of Measurement)

                If DateTimeStampSetting = DateTimeStampSettings.InRows Then getTextFileRowStartDateTimesAndDurations(pathToFile)

                Dim sr = New StreamReader(pathToFile)
                readTextFileHeader(sr)

                Dim iLine As Integer = 0

                While Not sr.EndOfStream

                    Dim fileLine = sr.ReadLine.Split(ColumnDelimiter).Select(
                        Function(x) x.Trim(Chr(34))
                    ).ToArray

                    Dim duration = RowDurations(iLine)
                    Dim startDateTime = RowStartDateTimes(iLine)
                    Dim overload, underload As Boolean?
                    If OverloadField <> "" Then overload = getOverload(fileLine(icolOverload))
                    If UnderloadField <> "" Then underload = getUnderload(fileLine(icolUnderload))

                    For icol = 0 To ColumnHeaders.Count - 1

                        Dim colHeader = ColumnHeaders(icol)
                        Dim measurementMetricId As Integer = -1
                        For Each mm In MetricMappings
                            If mm.Key.ToLower() = colHeader.ToLower Then measurementMetricId = mm.Value.Id
                        Next
                        If measurementMetricId <> -1 Then
                            Dim level = Val(fileLine(icol))
                            If level > 0 Then

                                Dim measurement = New Measurement With {
                                    .StartDateTime = startDateTime,
                                    .Duration = duration,
                                    .MeasurementMetricId = measurementMetricId,
                                    .Level = level,
                                    .Overload = overload,
                                    .Underload = underload
                                }
                                Measurements.Add(measurement)
                            End If
                        End If

                    Next

                    iLine += 1

                End While

                sr.Close()

                Return True

            Catch ex As Exception

                Return False

            End Try

        End Function



#End Region

        Public Function getMeasurements(
            Optional MeasurementFileId As Integer = -1,
            Optional MonitorId As Integer = -1, Optional MonitorLocationId As Integer = -1, Optional ProjectId As Integer = -1,
            Optional UploadContactId As Integer = -1,
            Optional DateTimeUploaded? As Date = Nothing, Optional RoundingTime As Double = 0, Optional OffsetTime As Double = 0,
            Optional DaylightSavingsOffsetStartDateTime As Date = Nothing, Optional DaylightSavingsOffsetDuration As Double = 0
        ) As IEnumerable(Of Measurement)

            If MeasurementFileId <> -1 Then
                For Each m In Measurements
                    m.MeasurementFileId = MeasurementFileId
                Next
            End If

            If MonitorId <> -1 Then
                For Each m In Measurements
                    m.MonitorId = MonitorId
                Next
            End If
            If MonitorLocationId <> -1 Then
                For Each m In Measurements
                    m.MonitorLocationId = MonitorLocationId
                Next
            End If
            If ProjectId <> -1 Then
                For Each m In Measurements
                    m.ProjectId = ProjectId
                Next
            End If
            If UploadContactId <> -1 Then
                For Each m In Measurements
                    m.UploadContactId = UploadContactId
                Next
            End If
            If DateTimeUploaded IsNot Nothing Then
                For Each m In Measurements
                    m.DateTimeUploaded = CDate(DateTimeUploaded)
                Next
            End If

            If RoundingTime > 0 Then
                For Each m In Measurements
                    m.StartDateTime = Date.FromOADate(
                        RoundToNearest(m.StartDateTime.ToOADate, RoundingTime)
                    )
                Next
            End If

            If OffsetTime <> 0 Then
                For Each m In Measurements
                    m.StartDateTime = m.StartDateTime.AddDays(OffsetTime)
                Next
            End If

            If DaylightSavingsOffsetDuration <> 0 Then
                For Each m In Measurements
                    If m.StartDateTime.ToOADate >= DaylightSavingsOffsetStartDateTime.ToOADate Then
                        m.StartDateTime = m.StartDateTime.AddDays(DaylightSavingsOffsetDuration)
                    End If
                Next
            End If

            ' Make all measurements the same duration
            Dim measurementDurations = Measurements.Select(Function(m) m.Duration).ToList
            measurementDurations.Sort()
            Dim medianDuration = measurementDurations(
                Int(measurementDurations.Count / 2)
            )
            For Each m In Measurements
                m.Duration = medianDuration
            Next

            ' Remove measurements which start at the same time
            If RemoveRepeatsSetting = RemoveRepeatsSettings.Remove Then
                Dim metricIds = Measurements.Select(Function(m) m.MeasurementMetricId).Distinct.ToList
                Dim newMeasurements As New List(Of Measurement)
                For Each metricId In metricIds
                    Dim metricMeasurements = Measurements.Where(Function(m) m.MeasurementMetricId = metricId).ToList
                    newMeasurements.Add(metricMeasurements(0))
                    For m = 1 To metricMeasurements.Count - 1
                        If metricMeasurements(m).StartDateTime <> metricMeasurements(m - 1).StartDateTime Then
                            newMeasurements.Add(metricMeasurements(m))
                        End If
                    Next
                Next
                Measurements = newMeasurements
            End If

            Return Measurements

        End Function

#End Region

    End Class


End Namespace