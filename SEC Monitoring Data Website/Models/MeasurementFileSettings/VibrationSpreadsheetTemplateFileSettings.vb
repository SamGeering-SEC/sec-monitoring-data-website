Namespace MeasurementFileSettings

    Public Class VibrationSpreadsheetTemplateFileSettings

        Inherits SpreadsheetTemplateFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)
            ExcelSheetName = "Vibration"

        End Sub

        Public Overrides Function GetAllMetrics() As List(Of MeasurementMetric)

            Dim measurementTypeId = MeasurementsDAL.GetMeasurementType("Vibration").Id
            Dim allMetrics = MeasurementsDAL.GetMeasurementMetrics(measurementTypeId).ToList
            Return allMetrics

        End Function

    End Class


End Namespace
