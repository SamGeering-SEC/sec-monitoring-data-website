Namespace MeasurementFileSettings

    Public Class PPVLiveFileSettings

        Inherits MeasurementFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            Me.MeasurementsDAL = MeasurementsDAL

            FileFormat = FileFormats.Text
            ColumnDelimiter = ","

            DateTimeStampSetting = DateTimeStampSettings.InRows
            DateTimeStampMarkerSetting = DateTimeStampMarkerSettings.MeasurementStart

            DateTimeFieldSetting = DateTimeFieldSettings.TwoFields
            MeasurementDateField = "Date"
            MeasurementTimeField = "Time"
            MeasurementDateFormat = "yyyy/MM/dd"
            MeasurementDateFormat2 = "dd/MM/yyyy"
            MeasurementTimeFormat = "HH:mm:ss"

            DurationSetting = DurationSettings.RowToRow
            DurationEqualSetting = DurationEqualSettings.Equal
            RemoveRepeatsSetting = RemoveRepeatsSettings.Remove

            MetricMappings = New Dictionary(Of String, MeasurementMetric)

            MetricMappings.Add("X_cv", Nothing)
            MetricMappings.Add("Y_cv", Nothing)
            MetricMappings.Add("Z_cv", Nothing)
            MetricMappings.Add("X_cf", Nothing)
            MetricMappings.Add("Y_cf", Nothing)
            MetricMappings.Add("Z_cf", Nothing)
            MetricMappings.Add("X_u", Nothing)
            MetricMappings.Add("Y_u", Nothing)
            MetricMappings.Add("Z_u", Nothing)

            OverloadField = ""
            UnderloadField = ""

        End Sub

    End Class

End Namespace


