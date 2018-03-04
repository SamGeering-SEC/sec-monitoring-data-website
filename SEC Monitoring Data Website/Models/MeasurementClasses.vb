Imports System.Runtime.CompilerServices
Imports libSEC.Dates

''' <summary>
''' A class to hold the scalar-data of a measurement
''' </summary>
''' <remarks></remarks>
Public Class MeasurementScalars

    Implements IMeasurementScalarData

    Private _startDateTime As Date
    Private _level As Double
    Private _duration As Double

    ''' <summary>
    ''' Create a new instance of a measurement with scalar-data only.
    ''' </summary>
    ''' <param name="StartDateTime">The DateTime on which the measurement started.</param>
    ''' <param name="Level">The measured level.</param>
    ''' <param name="Duration">The duration of the measurement.</param>
    ''' <remarks></remarks>
    Public Sub New(StartDateTime As Date, Level As Double, Duration As Double)

        _startDateTime = StartDateTime
        _level = Level
        _duration = Duration

    End Sub

    ''' <summary>
    ''' Getter for the measurement duration.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property getDuration As Double Implements IMeasurementScalarData.getDuration
        Get
            Return _duration
        End Get
    End Property

    ''' <summary>
    ''' Getter for the measurement level.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property getLevel As Double Implements IMeasurementScalarData.getLevel
        Get
            Return _level
        End Get
    End Property

    ''' <summary>
    ''' Getter for the DateTime at which the measurement started.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property getStartDateTime As Date Implements IMeasurementScalarData.getStartDateTime
        Get
            Return _startDateTime
        End Get
    End Property

    ''' <summary>
    ''' Getter for the DateTime at which the measurement ended.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>Calculated from the start DateTime and the duration of the measurement.</remarks>
    Public ReadOnly Property getEndDateTime As Date Implements IMeasurementScalarData.getEndDateTime
        Get
            Return _startDateTime.AddDays(_duration)
        End Get
    End Property

End Class

''' <summary>
''' A class to hold scalar-data of a list of measurements which have been passed through a CalculationFilter
''' </summary>
''' <remarks></remarks>
Public Class FilteredMeasurementScalars

    Implements IFilteredMeasurementScalarDatas

    Private _startDateTime As Date
    Private _filter_duration As Double
    Private _filtered_level As Double
    Private _numberOfInputMeasurements As Integer
    Private _totalInputMeasurementsDuration As Double
    Private _filter As CalculationFilter

    ''' <summary>
    ''' Create a new instance of FilteredMeasurementScalars.
    ''' </summary>
    ''' <param name="StartDateTime">The DateTime of the start of the first measurement.</param>
    ''' <param name="FilterDuration">The time period covered by the list of measurements, including missing data.</param>
    ''' <param name="FilteredLevel">The overall level of the measurements, after applying whatever CalculationFilter was used.</param>
    ''' <param name="NumberOfInputMeasurements">The number of measurements that were taken.</param>
    ''' <param name="TotalInputMeasurementsDuration">The sum of the durations of the individual measurements.</param>
    ''' <param name="Filter">The CalculationFilter used to filter the measurements.</param>
    ''' <remarks></remarks>
    Public Sub New(StartDateTime As Date, FilterDuration As Double, FilteredLevel As Double,
                   NumberOfInputMeasurements As Integer, TotalInputMeasurementsDuration As Double,
                   Filter As CalculationFilter)

        _startDateTime = StartDateTime
        _filter_duration = FilterDuration
        _filtered_level = FilteredLevel
        _numberOfInputMeasurements = NumberOfInputMeasurements
        _totalInputMeasurementsDuration = TotalInputMeasurementsDuration
        _filter = Filter

    End Sub

    Public ReadOnly Property getFilterDuration As Double Implements IFilteredMeasurementScalarDatas.getFilterDuration
        Get
            Return _filter_duration
        End Get
    End Property
    Public ReadOnly Property hasMissingInputMeasurements As Boolean Implements IFilteredMeasurementScalarDatas.hasMissingInputMeasurements
        Get
            Return _filter_duration > _totalInputMeasurementsDuration
        End Get
    End Property
    Public ReadOnly Property getFilteredLevel As Double Implements IFilteredMeasurementScalarDatas.getFilteredLevel
        Get
            Return _filtered_level
        End Get
    End Property
    Public ReadOnly Property getNumberOfInputMeasurements As Integer Implements IFilteredMeasurementScalarDatas.getNumberOfInputMeasurements
        Get
            Return _numberOfInputMeasurements
        End Get
    End Property
    Public ReadOnly Property getStartDateTime As Date Implements IFilteredMeasurementScalarDatas.getStartDateTime
        Get
            Return _startDateTime
        End Get
    End Property
    Public ReadOnly Property getTotalInputMeasurementsDuration As Double Implements IFilteredMeasurementScalarDatas.getTotalInputMeasurementsDuration
        Get
            Return _totalInputMeasurementsDuration
        End Get
    End Property
    Public ReadOnly Property getEndDateTime As Date Implements IFilteredMeasurementScalarDatas.getEndDateTime
        Get
            Return _startDateTime.AddDays(_filter_duration)
        End Get
    End Property
    Public ReadOnly Property getFilter As CalculationFilter Implements IFilteredMeasurementScalarDatas.getFilter
        Get
            Return _filter
        End Get
    End Property

End Class

''' <summary>
''' A class to hold scalar- and meta-data of a list of measurements which have been passed through a CalculationFilter.
''' </summary>
''' <remarks></remarks>
Public Class FilteredMeasurements

    Implements IFilteredMeasurements

    Private _filteredMeasurementScalarDatas As IFilteredMeasurementScalarDatas
    Private _metric As MeasurementMetric
    Private _monitor As Monitor
    Private _monitorLocation As MonitorLocation
    Private _filter As CalculationFilter
    Private _project As Project

    ''' <summary>
    ''' Create a new instance of `FilteredMeasurements`
    ''' </summary>
    ''' <param name="FilteredMeasurementScalarDatas">An instance of a class implementing IFilteredMeasurementScalarDatas</param>
    ''' <param name="Metric">The Measurement Metric of the data e.g. Noise, Vibration etc.</param>
    ''' <param name="Monitor">The monitor with which the original measurements were taken.</param>
    ''' <param name="MonitorLocation">The location where the original measurements where taken.</param>
    ''' <param name="CalculationFilter">The CalculationFilter used to filter the measurements.</param>
    ''' <param name="Project">The Project which the original measurements were taken for.</param>
    ''' <remarks></remarks>
    Public Sub New(FilteredMeasurementScalarDatas As IFilteredMeasurementScalarDatas,
                   Metric As MeasurementMetric,
                   Monitor As Monitor,
                   MonitorLocation As MonitorLocation,
                   CalculationFilter As CalculationFilter,
                   Project As Project)

        _filteredMeasurementScalarDatas = FilteredMeasurementScalarDatas
        _metric = Metric
        _monitor = Monitor
        _monitorLocation = MonitorLocation
        _filter = CalculationFilter
        _project = Project

    End Sub

#Region "IFilteredMeasurements Implementation"

    Public ReadOnly Property getMetric As MeasurementMetric Implements IFilteredMeasurements.getMetric
        Get
            Return _metric
        End Get
    End Property
    Public ReadOnly Property getMonitor As Monitor Implements IFilteredMeasurements.getMonitor
        Get
            Return _monitor
        End Get
    End Property
    Public ReadOnly Property getMonitorLocation As MonitorLocation Implements IFilteredMeasurements.getMonitorLocation
        Get
            Return _monitorLocation
        End Get
    End Property
    Public ReadOnly Property getProject As Project Implements IFilteredMeasurements.getProject
        Get
            Return _project
        End Get
    End Property

    Public ReadOnly Property getFilterDuration As Double Implements IFilteredMeasurementScalarDatas.getFilterDuration
        Get
            Return _filteredMeasurementScalarDatas.getFilterDuration
        End Get
    End Property
    Public ReadOnly Property hasMissingInputMeasurements As Boolean Implements IFilteredMeasurementScalarDatas.hasMissingInputMeasurements
        Get
            Return _filteredMeasurementScalarDatas.hasMissingInputMeasurements
        End Get
    End Property
    Public ReadOnly Property getFilteredLevel As Double Implements IFilteredMeasurementScalarDatas.getFilteredLevel
        Get
            Return _filteredMeasurementScalarDatas.getFilteredLevel
        End Get
    End Property
    Public ReadOnly Property getNumberOfInputMeasurements As Integer Implements IFilteredMeasurementScalarDatas.getNumberOfInputMeasurements
        Get
            Return _filteredMeasurementScalarDatas.getNumberOfInputMeasurements
        End Get
    End Property
    Public ReadOnly Property getStartDateTime As Date Implements IFilteredMeasurementScalarDatas.getStartDateTime
        Get
            Return _filteredMeasurementScalarDatas.getStartDateTime
        End Get
    End Property
    Public ReadOnly Property getTotalInputMeasurementsDuration As Double Implements IFilteredMeasurementScalarDatas.getTotalInputMeasurementsDuration
        Get
            Return _filteredMeasurementScalarDatas.getTotalInputMeasurementsDuration
        End Get
    End Property
    Public ReadOnly Property getEndDateTime As Date Implements IFilteredMeasurementScalarDatas.getEndDateTime
        Get
            Return getStartDateTime.AddDays(getFilterDuration)
        End Get
    End Property
    Public ReadOnly Property getFilter As CalculationFilter Implements IFilteredMeasurementScalarDatas.getFilter
        Get
            Return _filter
        End Get
    End Property

#End Region


End Class

''' <summary>
''' A class to hold a sequence of IFilteredMeasurements, where each IFilteredMeasurements uses the
''' same CalculationFilter.
''' </summary>
''' <remarks></remarks>
Public Class FilteredMeasurementsSequence

    Implements IFilteredMeasurementsSequence

    Private _filteredMeasurementsList As IEnumerable(Of IFilteredMeasurements)
    Private _calcFilter As CalculationFilter

    ''' <summary>
    ''' Create a new instance of a FilteredMeasurementsSequence.
    ''' </summary>
    ''' <param name="FilteredMeasurementsList">The list of IFilteredMeasurements to include in the sequence.</param>
    ''' <param name="CalculationFilter">The CalculationFilter that was used to create each IFilteredMeasurements instance in the FilteredMeasurementsList.</param>
    ''' <remarks></remarks>
    Public Sub New(FilteredMeasurementsList As IEnumerable(Of IFilteredMeasurements),
                   CalculationFilter As CalculationFilter)

        _filteredMeasurementsList = FilteredMeasurementsList
        _calcFilter = CalculationFilter

    End Sub

    Public ReadOnly Property getFilteredMeasurementsList As IEnumerable(Of IFilteredMeasurements) Implements IFilteredMeasurementsSequence.getFilteredMeasurementsList
        Get
            Return _filteredMeasurementsList
        End Get
    End Property

    ''' <summary>
    ''' Returns the number of IFilteredMeasurements used to create the FilteredMeasurementsSequence.
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Count() As Integer Implements IFilteredMeasurementsSequence.Count

        Return _filteredMeasurementsList.Count

    End Function

    Public ReadOnly Property getFilter As CalculationFilter Implements IFilteredMeasurementsSequence.getFilter
        Get
            Return _calcFilter
        End Get
    End Property

    Public Function hasMeasurementAtDateTime(StartDateTime As Date) As Boolean Implements IFilteredMeasurementsSequence.hasMeasurementAtDateTime

        For Each m In _filteredMeasurementsList
            If m.getStartDateTime.CompareTo(StartDateTime) = 0 Then Return True
        Next
        Return False

    End Function

    Public Function getMeasurementAtDateTime(StartDateTime As Date) As IFilteredMeasurements Implements IFilteredMeasurementsSequence.getMeasurementAtDateTime

        Return _filteredMeasurementsList.Single(Function(m) m.getStartDateTime = StartDateTime)

    End Function

    Public ReadOnly Property getMeasurementStartDateTimes As IEnumerable(Of Date) Implements IFilteredMeasurementsSequence.getMeasurementStartDateTimes
        Get
            Return _filteredMeasurementsList.Select(Function(m) m.getStartDateTime).ToList
        End Get
    End Property

    Public ReadOnly Property getMeasurementLevels As IEnumerable(Of Double) Implements IFilteredMeasurementsSequence.getMeasurementLevels
        Get
            Return _filteredMeasurementsList.Select(Function(m) m.getFilteredLevel).ToList
        End Get
    End Property

    Public ReadOnly Property getMetric As MeasurementMetric Implements IFilteredMeasurementsSequence.getMetric
        Get
            Return _filteredMeasurementsList.First.getMetric
        End Get
    End Property


    Public Function getMeasurementsOnDate(MeasurementDate As Date) As IEnumerable(Of IFilteredMeasurements) Implements IFilteredMeasurementsSequence.getMeasurementsOnDate

        Dim matchingMeasurements As New List(Of IFilteredMeasurements)

        For Each m In _filteredMeasurementsList
            If m.getStartDateTime.DateOnly.CompareTo(MeasurementDate) = 0 Then matchingMeasurements.Add(m)
        Next
        Return matchingMeasurements

    End Function

    Public Function getNumMeasurementsOnDate(MeasurementDate As Date) As Integer Implements IFilteredMeasurementsSequence.getNumMeasurementsOnDate

        Dim numMeasurementsOnDate As Integer = 0

        For Each m In _filteredMeasurementsList
            If m.getStartDateTime.DateOnly.CompareTo(MeasurementDate) = 0 Then numMeasurementsOnDate += 1
        Next
        Return numMeasurementsOnDate

    End Function

    Public Function hasMeasurementsOnDate(MeasurementDate As Date) As Boolean Implements IFilteredMeasurementsSequence.hasMeasurementsOnDate

        For Each m In _filteredMeasurementsList
            If m.getStartDateTime.DateOnly.CompareTo(MeasurementDate) = 0 Then Return True
        Next
        Return False

    End Function

    Public Function getDistinctStartDateTimes() As List(Of Date)

        Return _filteredMeasurementsList.Select(Function(m) m.getStartDateTime).Distinct.ToList

    End Function

End Class

Public Class ReadMeasurementParameters

    Implements IReadMeasurementParameters


    Public Property StartDate As Date = Date.MinValue Implements IReadMeasurementParameters.StartDate
    Public Property EndDate As Date = Date.MinValue Implements IReadMeasurementParameters.EndDate

    Public Property FilterText As String = "" Implements IReadMeasurementParameters.FilterText
    Public Property Metric As MeasurementMetric = Nothing Implements IReadMeasurementParameters.Metric
    Public Property Monitor As Monitor = Nothing Implements IReadMeasurementParameters.Monitor

    Public Property MeasurementType As MeasurementType = Nothing Implements IReadMeasurementParameters.MeasurementType

    Public Property MonitorLocation As MonitorLocation = Nothing Implements IReadMeasurementParameters.MonitorLocation
End Class

Public Class ReadFilterParameters

    Public MeasurementTypeID As Integer = 0

End Class

