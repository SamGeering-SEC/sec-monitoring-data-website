Imports System.IO

Namespace MeasurementFileSettings

    Public Class RedboxFileSettings

        Inherits MeasurementFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            Me.MeasurementsDAL = MeasurementsDAL

            FileFormat = FileFormats.Text
            ColumnDelimiter = " "

            DateTimeStampSetting = DateTimeStampSettings.InHeader
            DateTimeStampMarkerSetting = DateTimeStampMarkerSettings.MeasurementEnd

            DateTimeFieldSetting = DateTimeFieldSettings.TwoFields
            MeasurementDateFormat = "dd/MM/yyyy"
            MeasurementTimeFormat = "HH:mm:ss.fff"

            DurationSetting = DurationSettings.Header
            DurationEqualSetting = DurationEqualSettings.NotEqual
            RemoveRepeatsSetting = RemoveRepeatsSettings.DoNotRemove

            MetricMappings = New Dictionary(Of String, MeasurementMetric)

            MetricMappings.Add("X", Nothing)
            MetricMappings.Add("Y", Nothing)
            MetricMappings.Add("Z", Nothing)

            OverloadField = ""
            UnderloadField = ""

        End Sub

        Protected Overrides Sub readTextFileHeader(StreamReader As StreamReader)

            Dim numSamplesField, sampleRateField As String
            Dim dateString, timeString, numSamplesString, rateString As String
            Dim sequenceStart As DateTime, numSamples As Integer, sampleRate As Double, sampleDuration As Double

            ' Read header information
            MeasurementDateField = "# Start date: "
            MeasurementTimeField = "# Start time: "
            numSamplesField = "# Length (samples): "
            sampleRateField = "# Rate (Hertz): "
            Dim fileLine = ""
            While fileLine.Contains(MeasurementDateField) = False : fileLine = StreamReader.ReadLine : End While
            dateString = Mid(fileLine, Len(MeasurementDateField) + 1)
            While fileLine.Contains(MeasurementTimeField) = False : fileLine = StreamReader.ReadLine : End While
            timeString = Mid(fileLine, Len(MeasurementTimeField) + 1)
            While fileLine.Contains(numSamplesField) = False : fileLine = StreamReader.ReadLine : End While
            numSamplesString = Mid(fileLine, Len(numSamplesField) + 1)
            While fileLine.Contains(sampleRateField) = False : fileLine = StreamReader.ReadLine : End While
            rateString = Mid(fileLine, Len(sampleRateField) + 1)
            While fileLine <> "# Axis:" : fileLine = StreamReader.ReadLine : End While

            ' Create Start Times and Durations
            sequenceStart = getDateTime(dateString, timeString)
            numSamples = Val(numSamplesString)
            sampleRate = Val(rateString)
            sampleDuration = (1 / (24 * 60 * 60)) / sampleRate

            RowStartDateTimes = New List(Of DateTime)
            RowDurations = New List(Of Double)
            For s = 0 To numSamples - 1
                RowStartDateTimes.Add(sequenceStart.AddDays(s * sampleDuration))
                RowDurations.Add(sampleDuration)
            Next

            ColumnHeaders = StreamReader.ReadLine.Split(ColumnDelimiter).Select(Function(x) x.Trim(Chr(34))).Where(Function(x) x <> "" And x <> "#").ToArray

        End Sub

    End Class


End Namespace

