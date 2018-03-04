Namespace MeasurementFileSettings

    Public Class OsirisFileSettings

        Inherits MeasurementFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            Me.MeasurementsDAL = MeasurementsDAL

            FileFormat = FileFormats.TextOrExcel
            ColumnDelimiter = ","
            ExcelSheetName = "Report"

            DateTimeStampSetting = DateTimeStampSettings.InRows
            DateTimeStampMarkerSetting = DateTimeStampMarkerSettings.MeasurementEnd

            DateTimeFieldSetting = DateTimeFieldSettings.SingleField
            MeasurementDateTimeField = "TimeStamp"
            MeasurementDateTimeFormat = "dd/MM/yy HH:mm:ss"
            MeasurementDateTimeFormat2 = "dd/MM/yyyy HH:mm:ss"

            DurationSetting = DurationSettings.RowToRow
            DurationEqualSetting = DurationEqualSettings.Equal
            RemoveRepeatsSetting = RemoveRepeatsSettings.DoNotRemove

            MetricMappings = New Dictionary(Of String, MeasurementMetric)

            MetricMappings.Add("Total Particles (ug/m^3)", Nothing)
            MetricMappings.Add("PM10 particles (ug/m^3)", Nothing)
            MetricMappings.Add("PM2.5 particles (ug/m^3)", Nothing)
            MetricMappings.Add("PM1 particles (ug/m^3)", Nothing)
            MetricMappings.Add("Temperature (Celcius)", Nothing)
            MetricMappings.Add("Humidity (% RH)", Nothing)
            MetricMappings.Add("Wind Speed (mtr/sec)", Nothing)
            MetricMappings.Add("Wind Heading (degrees)", Nothing)

            OverloadField = ""
            UnderloadField = ""

        End Sub

    End Class


End Namespace

