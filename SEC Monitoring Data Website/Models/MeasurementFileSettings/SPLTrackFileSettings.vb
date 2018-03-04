
Namespace MeasurementFileSettings

    Public Class SPLTrackFileSettings

        Inherits MeasurementFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            Me.MeasurementsDAL = MeasurementsDAL

            FileFormat = FileFormats.Excel
            ExcelSheetName = "Data"

            DateTimeStampSetting = DateTimeStampSettings.InRows
            DateTimeStampMarkerSetting = DateTimeStampMarkerSettings.MeasurementEnd

            DateTimeFieldSetting = DateTimeFieldSettings.TwoFields
            MeasurementDateField = "Date"
            MeasurementDateFormat = "dd-MM-yyyy"
            MeasurementTimeField = "Time"
            MeasurementTimeFormat = "HH:mm"

            DurationSetting = DurationSettings.RowToRow
            DurationEqualSetting = DurationEqualSettings.NotEqual
            RemoveRepeatsSetting = RemoveRepeatsSettings.DoNotRemove

            MetricMappings = New Dictionary(Of String, MeasurementMetric)

            MetricMappings.Add("Period LAeq", Nothing)
            MetricMappings.Add("LAmax", Nothing)
            MetricMappings.Add("L10", Nothing)
            MetricMappings.Add("L90", Nothing)

            OverloadField = "Overloads"
            UnderloadField = ""

        End Sub

    End Class


End Namespace


