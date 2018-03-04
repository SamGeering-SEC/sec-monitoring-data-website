Namespace MeasurementFileSettings

    Public Class RionLeqLiveWebsystemFileSettings

        Inherits MeasurementFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            Me.MeasurementsDAL = MeasurementsDAL

            FileFormat = FileFormats.Text
            ColumnDelimiter = ","

            DateTimeStampSetting = DateTimeStampSettings.InRows
            DateTimeStampMarkerSetting = DateTimeStampMarkerSettings.MeasurementEnd

            DateTimeFieldSetting = DateTimeFieldSettings.SingleField
            MeasurementDateTimeField = "End Time"
            MeasurementDateTimeFormat = "yyyy/MM/dd HH:mm:ss"

            DurationSetting = DurationSettings.RowToRow
            DurationEqualSetting = DurationEqualSettings.Equal
            RemoveRepeatsSetting = RemoveRepeatsSettings.Remove

            MetricMappings = New Dictionary(Of String, MeasurementMetric)

            MetricMappings.Add("Lp", Nothing)
            MetricMappings.Add("Leq", Nothing)
            MetricMappings.Add("Lmax", Nothing)
            MetricMappings.Add("Lmin", Nothing)
            MetricMappings.Add("ln1", Nothing)
            MetricMappings.Add("ln2", Nothing)
            MetricMappings.Add("ln3", Nothing)
            MetricMappings.Add("ln4", Nothing)
            MetricMappings.Add("ln5", Nothing)

            OverloadField = "Overload"
            UnderloadField = "Underrange"

        End Sub

    End Class

End Namespace


