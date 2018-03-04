Namespace MeasurementFileSettings

    Public Class NoiseSpreadsheetTemplateFileSettings

        Inherits SpreadsheetTemplateFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)
            ExcelSheetName = "Noise"

        End Sub

        Public Overrides Function GetAllMetrics() As List(Of MeasurementMetric)

            Dim measurementTypeId = MeasurementsDAL.GetMeasurementType("Noise").Id
            Dim allMetrics = MeasurementsDAL.GetMeasurementMetrics(measurementTypeId).ToList
            Return allMetrics

        End Function

    End Class


End Namespace

