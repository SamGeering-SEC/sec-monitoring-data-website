Namespace MeasurementFileSettings

    Public Class RionRCDSFileSettings

        Inherits MeasurementFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            Me.MeasurementsDAL = MeasurementsDAL

            FileFormat = FileFormats.Text
            ColumnDelimiter = ","

            DateTimeStampSetting = DateTimeStampSettings.InRows
            DateTimeStampMarkerSetting = DateTimeStampMarkerSettings.MeasurementStart

            DateTimeFieldSetting = DateTimeFieldSettings.TwoFields
            MeasurementDateField = "Date"
            MeasurementDateFormat = "yyyy/MM/dd"
            MeasurementDateFormat2 = "dd/MM/yyyy"
            MeasurementTimeField = "Time"
            MeasurementTimeFormat = "HH:mm:ss"

            DurationSetting = DurationSettings.Field
            DurationEqualSetting = DurationEqualSettings.NotEqual
            RemoveRepeatsSetting = RemoveRepeatsSettings.DoNotRemove
            DurationField = "MeasurmentTime"
            DurationFieldFormat = "hh:mm:ss"

            MetricMappings = New Dictionary(Of String, MeasurementMetric)

            MetricMappings.Add("LAeq", Nothing)
            MetricMappings.Add("LAE", Nothing)
            MetricMappings.Add("LAmax", Nothing)
            MetricMappings.Add("LAmin", Nothing)
            MetricMappings.Add("LA05", Nothing)
            MetricMappings.Add("LA10", Nothing)
            MetricMappings.Add("LA50", Nothing)
            MetricMappings.Add("LA90", Nothing)
            MetricMappings.Add("LA95", Nothing)
            MetricMappings.Add("LCpk", Nothing)

            OverloadField = "Over"
            UnderloadField = "Under"

        End Sub

    End Class

End Namespace


