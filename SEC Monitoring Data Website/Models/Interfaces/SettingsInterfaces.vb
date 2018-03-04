'Public Interface IMeasurementView

'    ReadOnly Property getViewName As String
'    ReadOnly Property getTableType As String
'    ReadOnly Property getTableResultsHeader As String
'    ReadOnly Property getSequenceSettings As IEnumerable(Of IMeasurementViewSequenceSetting)
'    ReadOnly Property getMeasurementType As IMeasurementType
'    ReadOnly Property getGroups As IEnumerable(Of IMeasurementViewGroup)

'End Interface

'Public Interface IMeasurementViewSequenceSetting

'    ReadOnly Property getTableHeader As String
'    ReadOnly Property getSeriesName As String
'    ReadOnly Property getDayViewSeriesType As String
'    ReadOnly Property getWeekViewSeriesType As String
'    ReadOnly Property getMonthViewSeriesType As String
'    ReadOnly Property getSeriesColour As Integer
'    ReadOnly Property getCalculationFilter As ICalculationFilter
'    ReadOnly Property getView As IMeasurementView
'    ReadOnly Property getGroup As IMeasurementViewGroup

'End Interface

'Public Interface IMeasurementViewGroup

'    ReadOnly Property getMainHeader As String
'    ReadOnly Property getSubHeader As String
'    ReadOnly Property getView As IMeasurementView
'    ReadOnly Property getSequenceSettings As IEnumerable(Of IMeasurementViewSequenceSetting)

'End Interface

Public Interface ISettingsDAL




End Interface

Public Interface IRoutable
    Function getRouteName() As String

End Interface

Public Interface IDeletable

    Function canBeDeleted() As Boolean

End Interface