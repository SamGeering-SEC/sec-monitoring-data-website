Namespace MeasurementFileSettings

    Public Class SpreadsheetTemplateFileSettings

        Inherits MeasurementFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            Me.MeasurementsDAL = MeasurementsDAL

            FileFormat = FileFormats.Excel

            DateTimeStampSetting = DateTimeStampSettings.InRows
            DateTimeStampMarkerSetting = DateTimeStampMarkerSettings.MeasurementStart

            DateTimeFieldSetting = DateTimeFieldSettings.SingleField
            MeasurementDateTimeField = "Start Date Time"
            MeasurementDateTimeFormat = ""

            DurationFieldSetting = DurationFieldSettings.Duration
            DurationField = "Duration"
            DurationFieldFormat = ""

            DurationSetting = DurationSettings.Field
            DurationEqualSetting = DurationEqualSettings.NotEqual
            RemoveRepeatsSetting = RemoveRepeatsSettings.DoNotRemove

            OverloadField = "Overload"
            UnderloadField = "Underload"

            MetricMappings = New Dictionary(Of String, MeasurementMetric)
            Dim allMetrics = MeasurementsDAL.GetMeasurementMetrics().ToList
            For Each metric In GetAllMetrics()
                MetricMappings.Add(metric.MetricName, metric)
            Next

        End Sub

        Public Overridable Function GetAllMetrics() As List(Of MeasurementMetric)

            Dim allMetrics = MeasurementsDAL.GetMeasurementMetrics().ToList
            Return allMetrics

        End Function

    End Class

End Namespace


