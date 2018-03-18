Imports System.IO

Namespace MeasurementFileSettings

    Public Class SigicomVibrationFileSettings

        Inherits MeasurementFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            Me.MeasurementsDAL = MeasurementsDAL

            FileFormat = FileFormats.Text
            ColumnDelimiter = vbTab

            DateTimeStampSetting = DateTimeStampSettings.InRows
            DateTimeStampMarkerSetting = DateTimeStampMarkerSettings.MeasurementEnd

            DateTimeFieldSetting = DateTimeFieldSettings.SingleField
            MeasurementDateTimeField = "Date/Time"
            MeasurementDateTimeFormat = ""

            DurationSetting = DurationSettings.RowToRow
            DurationEqualSetting = DurationEqualSettings.NotEqual
            RemoveRepeatsSetting = RemoveRepeatsSettings.DoNotRemove

            MetricMappings = New Dictionary(Of String, MeasurementMetric)

            MetricMappings.Add("1", Nothing)
            MetricMappings.Add("2", Nothing)
            MetricMappings.Add("3", Nothing)

            OverloadField = ""
            UnderloadField = ""

        End Sub

        Protected Overrides Sub readTextFileHeader(StreamReader As StreamReader)

            Dim fileLine As String = ""
            While fileLine.Contains("Date/Time") = False
                fileLine = StreamReader.ReadLine()
            End While

            ColumnHeaders = fileLine.Split(ColumnDelimiter).Select(Function(x) x.Trim(Chr(34))).Where(Function(x) x <> "").ToArray

            icolDateTime = Array.IndexOf(ColumnHeaders, MeasurementDateTimeField)

            StreamReader.ReadLine()
            StreamReader.ReadLine()

        End Sub

        Public Overrides Sub getTextFileRowStartDateTimesAndDurations(pathToFile As String)

            Dim sr As New StreamReader(pathToFile)
            readTextFileHeader(sr)

            RowStartDateTimes = New List(Of DateTime)
            RowDurations = New List(Of Double)

            ' Read Date Times and Durations
            Dim fileLine = sr.ReadLine.Split(ColumnDelimiter).Select(Function(x) x.Trim(Chr(34))).Where(Function(x) x <> "").ToArray
            While Not fileLine(0).Contains("End of report")

                RowStartDateTimes.Add(getDateTime(fileLine(icolDateTime)))
                fileLine = sr.ReadLine.Split(ColumnDelimiter).Select(Function(x) x.Trim(Chr(34))).Where(Function(x) x <> "").ToArray

            End While

            ' Fill in durations
            For r = 0 To RowStartDateTimes.Count - 2
                RowDurations.Add(RowStartDateTimes(r + 1).ToOADate - RowStartDateTimes(r).ToOADate)
            Next
            RowDurations.Add(RowDurations.Last)

            ' Shift start date times back by durations
            For i = 0 To RowStartDateTimes.Count - 1
                RowStartDateTimes(i) = RowStartDateTimes(i).AddDays(-RowDurations(i))
            Next

        End Sub

        Public Overrides Function readTextFile(pathToFile As String) As Boolean

            Try

                Measurements = New List(Of Measurement)

                getTextFileRowStartDateTimesAndDurations(pathToFile)

                Dim sr = New StreamReader(pathToFile)
                readTextFileHeader(sr)

                Dim iLine As Integer = 0
                Dim fileLine = sr.ReadLine.Split(ColumnDelimiter).Select(Function(x) x.Trim(Chr(34))).Where(Function(x) x <> "").ToArray

                While Not fileLine(0).Contains("End of report")

                    Dim duration = RowDurations(iLine)
                    Dim startDateTime = RowStartDateTimes(iLine)
                    Dim overload, underload As Boolean?

                    For icol = 0 To ColumnHeaders.Count - 1

                        Dim colHeader = ColumnHeaders(icol)
                        If MetricMappings.ContainsKey(colHeader) AndAlso MetricMappings(colHeader) IsNot Nothing Then

                            Dim level = Val(fileLine(icol))

                            If level > 0 Then

                                Dim measurement = New Measurement With {.StartDateTime = startDateTime,
                                                                        .Duration = duration,
                                                                        .MeasurementMetric = MetricMappings(colHeader),
                                                                        .Level = level,
                                                                        .Overload = overload,
                                                                        .Underload = underload}
                                Measurements.Add(measurement)

                            End If

                        End If

                    Next

                    iLine += 1
                    fileLine = sr.ReadLine.Split(ColumnDelimiter).Select(Function(x) x.Trim(Chr(34))).Where(Function(x) x <> "").ToArray

                End While

                Return True

            Catch ex As Exception

                Return False

            End Try

        End Function

    End Class



End Namespace