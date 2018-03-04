Public Class MetricMapping

    Public Property MappingName As String
    Public Property MetricId As Integer?
    Public Property MetricList As SelectList

    Public Sub New()

    End Sub

    Public Sub New(MappingName As String, MetricId As Integer?, MeasurementMetrics As List(Of MeasurementMetric))

        Me.MappingName = MappingName
        Me.MetricId = MetricId
        Me.MetricList = New SelectList(MeasurementMetrics, "Id", "MetricName")

    End Sub

End Class
