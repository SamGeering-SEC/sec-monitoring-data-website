Namespace MeasurementFileSettings

    Public Class AirQualitySpreadsheetTemplateFileSettings

        Inherits SpreadsheetTemplateFileSettings

        Public Sub New(MeasurementsDAL As IMeasurementsDAL)

            MyBase.New(MeasurementsDAL)
            ExcelSheetName = "Air Quality and Dust"

        End Sub

        Public Overrides Function GetAllMetrics() As List(Of MeasurementMetric)

            Dim measurementTypeId = MeasurementsDAL.GetMeasurementType("Air Quality, Dust and Meteorological").Id
            Dim allMetrics = MeasurementsDAL.GetMeasurementMetrics(measurementTypeId).ToList
            Return allMetrics

        End Function

    End Class


End Namespace
