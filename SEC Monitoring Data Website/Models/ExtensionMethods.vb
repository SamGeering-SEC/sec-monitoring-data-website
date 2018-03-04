Imports System.Runtime.CompilerServices
Imports System.Linq.Expressions

Module ExtensionMethods


    <System.Runtime.CompilerServices.Extension> _
    Public Function ContentAbsUrl(url As UrlHelper, relativeContentPath As String) As String

        Dim contextUri As Uri = HttpContext.Current.Request.Url
        Dim baseUri = String.Format("{0}://{1}{2}", contextUri.Scheme, contextUri.Host, If(contextUri.Port = 80, String.Empty, ":" + contextUri.Port))
        Return String.Format("{0}{1}", baseUri, VirtualPathUtility.ToAbsolute(relativeContentPath))

    End Function

#Region "Calculation Filters"

    <Extension()>
    Public Function GetCalculationFilterByFilterName(ByRef SECMonitoringDbContext As SECMonitoringDbContext, ByVal FilterName As String) As CalculationFilter

        Try
            Return (From ents In SECMonitoringDbContext.CalculationFilters Where ents.FilterName = FilterName).First
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region

#Region "Days of Week"

    <Extension()>
    Public Function GetDayOfWeekByDayName(ByRef SECMonitoringDbContext As SECMonitoringDbContext, ByVal DayName As String) As DayOfWeek

        Try
            Return (From ents In SECMonitoringDbContext.DaysOfWeek Where ents.DayName = DayName).First
        Catch ex As Exception
            Return Nothing
        End Try

    End Function

#End Region


#Region "Select List Item Methods"

    <Extension()>
    Public Function ToSelectListItems(ByRef Monitors As IEnumerable(Of Monitor), ByVal selectedId As Integer) As IEnumerable(Of SelectListItem)

        Return Monitors.OrderBy(Function(monitor) monitor.MonitorName) _
                       .Select(Function(monitor) New SelectListItem With {.Selected = If(monitor.Id = selectedId, True, False),
                                                                          .Text = monitor.MonitorName,
                                                                          .Value = monitor.Id.ToString})


    End Function
    <Extension()>
    Public Function ToSelectListItems(ByRef MonitorLocations As IEnumerable(Of MonitorLocation), ByVal selectedId As Integer) As IEnumerable(Of SelectListItem)

        Return MonitorLocations.OrderBy(Function(MonitorLocation) MonitorLocation.MonitorLocationName) _
                       .Select(Function(MonitorLocation) New SelectListItem With {.Selected = If(MonitorLocation.Id = selectedId, True, False),
                                                                          .Text = MonitorLocation.MonitorLocationName,
                                                                          .Value = MonitorLocation.Id.ToString})


    End Function
    <Extension()>
    Public Function ToSelectListItems(ByRef CalculationFilters As IEnumerable(Of CalculationFilter), ByVal selectedId As Integer) As IEnumerable(Of SelectListItem)

        Return CalculationFilters.OrderBy(Function(CalculationFilter) CalculationFilter.FilterName) _
                       .Select(Function(CalculationFilter) New SelectListItem With {.Selected = If(CalculationFilter.Id = selectedId, True, False),
                                                                          .Text = CalculationFilter.FilterName,
                                                                          .Value = CalculationFilter.Id.ToString})


    End Function
    <Extension()>
    Public Function ToSelectListItems(ByRef MeasurementMetrics As IEnumerable(Of MeasurementMetric), ByVal selectedId As Integer) As IEnumerable(Of SelectListItem)

        Return MeasurementMetrics _
                .OrderBy(Function(MeasurementMetric) MeasurementMetric.MetricName) _
                .Select(Function(MeasurementMetric) New SelectListItem With {
                            .Selected = If(MeasurementMetric.Id = selectedId, True, False),
                            .Text = MeasurementMetric.MetricName,
                            .Value = MeasurementMetric.Id.ToString
                            })


    End Function
    <Extension()>
    Public Function ToSelectListItems(ByRef DaysOfWeek As IEnumerable(Of DayOfWeek), ByVal selectedId As Integer) As IEnumerable(Of SelectListItem)

        Return DaysOfWeek.OrderBy(Function(DayOfWeek) DayOfWeek.DayName) _
                         .Select(Function(DayOfWeek) New SelectListItem With {
                                     .Selected = If(DayOfWeek.Id = selectedId, True, False),
                                     .Text = DayOfWeek.DayName,
                                     .Value = DayOfWeek.Id.ToString})


    End Function


#End Region

End Module

