Namespace MeasurementFileSettings

    Public Class VibraFileSettings

        Inherits MeasurementFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            Me.MeasurementsDAL = MeasurementsDAL

            FileFormat = FileFormats.Text
            ColumnDelimiter = ","

            DateTimeStampSetting = DateTimeStampSettings.InRows
            DateTimeStampMarkerSetting = DateTimeStampMarkerSettings.MeasurementEnd

            DateTimeFieldSetting = DateTimeFieldSettings.SingleField
            MeasurementDateTimeField = "Date+Time"
            MeasurementDateTimeFormat = "yyyy-MM-dd HH:mm:ss"

            DurationSetting = DurationSettings.RowToRow
            DurationEqualSetting = DurationEqualSettings.NotEqual
            RemoveRepeatsSetting = RemoveRepeatsSettings.DoNotRemove

            MetricMappings = New Dictionary(Of String, MeasurementMetric)

            MetricMappings.Add("X [mm/s]", Nothing)
            MetricMappings.Add("Y [mm/s]", Nothing)
            MetricMappings.Add("Z [mm/s]", Nothing)
            MetricMappings.Add("X [Hz]", Nothing)
            MetricMappings.Add("Y [Hz]", Nothing)
            MetricMappings.Add("Z [Hz]", Nothing)

            OverloadField = ""
            UnderloadField = ""

        End Sub

    End Class

End Namespace


