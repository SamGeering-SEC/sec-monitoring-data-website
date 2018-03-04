Imports System.Runtime.CompilerServices
Imports System.Math
Imports libSEC.Dates
Imports libSEC.Maths

Public Module MeasurementCalculations

#Region "Analysis"

#Region "Aggregate Levels and Statistics"

    ''' <summary>
    ''' Calculate the minimum of the levels in an IEnumerable of IMeasurementScalarData.
    ''' </summary>
    ''' <param name="MeasurementList">An IEnumerable of IMeasurementScalarData</param>
    ''' <returns>The minimum level in the measurement data</returns>
    ''' <remarks></remarks>
    <Extension()> Public Function MinimumLevel(ByRef MeasurementList As IEnumerable(Of IMeasurementScalarData)) As Double

        Return (From md In MeasurementList Select md.getLevel).ToList.Min

    End Function

    ''' <summary>
    ''' Calculate the maximum of the levels in an IEnumerable of IMeasurementScalarData.
    ''' </summary>
    ''' <param name="MeasurementList">An IEnumerable of IMeasurementScalarData.</param>
    ''' <returns>The maximum level in the measurement data</returns>
    ''' <remarks></remarks>
    <Extension()> Public Function MaximumLevel(ByRef MeasurementList As IEnumerable(Of IMeasurementScalarData)) As Double

        Return (From md In MeasurementList Select md.getLevel).ToList.Max

    End Function

    ''' <summary>
    ''' Calculate the linear average of the levels in an IEnumerable of IMeasurementScalarData.
    ''' </summary>
    ''' <param name="MeasurementList">An IEnumerable of IMeasurementScalarData.</param>
    ''' <returns>The linear average of the levels in the measurement data.</returns>
    ''' <remarks></remarks>
    <Extension()> Public Function LinearAverageLevel(ByRef MeasurementList As IEnumerable(Of IMeasurementScalarData)) As Double

        Dim levelTimeTotal As Double = 0
        For Each measurement In MeasurementList
            levelTimeTotal += measurement.getLevel * measurement.getDuration
        Next
        Return levelTimeTotal / MeasurementList.TotalDuration

    End Function

    ''' <summary>
    ''' Calculate the logarithmic average of the levels in an IEnumerable of IMeasurementScalarData.
    ''' </summary>
    ''' <param name="MeasurementList">An IEnumerable of IMeasurementScalarData.</param>
    ''' <returns>The logarithmic average of the levels in the measurement data.</returns>
    ''' <remarks></remarks>
    <Extension()> Public Function LogarithmicAverageLevel(ByRef MeasurementList As IEnumerable(Of IMeasurementScalarData)) As Double

        Dim pressureTimeTotal As Double = 0
        For Each measurement In MeasurementList
            pressureTimeTotal += 10 ^ (measurement.getLevel / 10) * measurement.getDuration
        Next

        Return 10 * Log10(pressureTimeTotal / MeasurementList.TotalDuration)

    End Function

    ''' <summary>
    ''' Calculate the Root Mean Square of the levels in an IEnumerable of IMeasurementScalarData.
    ''' </summary>
    ''' <param name="MeasurementList">An IEnumerable of IMeasurementScalarData.</param>
    ''' <returns>The RMS of the levels in the measurement data.</returns>
    ''' <remarks></remarks>
    <Extension()> Public Function RootMeanSquareLevel(ByRef MeasurementList As IEnumerable(Of IMeasurementScalarData)) As Double

        Dim square = MeasurementList.Select(Function(m) m.getLevel ^ 2).Sum
        Dim meanSquare = square / MeasurementList.Count
        Dim rootMeanSquare = Sqrt(meanSquare)

        Return rootMeanSquare

    End Function

    ''' <summary>
    ''' Calculate the sum of the individual durations of each of a list of measurements.
    ''' </summary>
    ''' <param name="MeasurementList">The list of measurements</param>
    ''' <returns>The total duration of the measurements</returns>
    ''' <remarks>If the measurements are not contiguous i.e. there are gaps, then this will return the correct duration for averaging etc.</remarks>
    <Extension()> Public Function TotalDuration(ByRef MeasurementList As IEnumerable(Of IMeasurementScalarData)) As Double

        Return MeasurementList.Select(Function(m) m.getDuration).Sum

    End Function

    Private Delegate Function UpperComparator(a As Double, b As Double) As Boolean

    ''' <summary>
    ''' Count the number of measurements with a level in the given range.
    ''' </summary>
    ''' <param name="MeasurementList">The list of measurements to use.</param>
    ''' <param name="LowerBound">The lower bound of the range.</param>
    ''' <param name="UpperBound">The uppper bound of the range.</param>
    ''' <param name="IncludeLevelsAtUpperLimit">Whether use levels which are exactly at the upper limit.</param>
    ''' <returns>The number of measurements with a level in the given range.</returns>
    ''' <remarks></remarks>
    <Extension()> Public Function LevelRangeCount(ByRef MeasurementList As IEnumerable(Of IMeasurement),
                                                  LowerBound As Double, UpperBound As Double,
                                                  Optional IncludeLevelsAtUpperLimit As Boolean = False) As Integer

        ' Find the number of monitored data levels within the specified range
        Dim theCount As Integer = 0
        Dim upperComparator As UpperComparator = New UpperComparator(AddressOf LessThan)
        If IncludeLevelsAtUpperLimit = True Then upperComparator = New UpperComparator(AddressOf LessThanOrEqualTo)

        Return (From md In MeasurementList
                Where md.getLevel >= LowerBound AndAlso upperComparator(md.getLevel, UpperBound) = True
                Select md).Count

    End Function

    ''' <summary>
    ''' Calculate the distribution of levels in a list of measurements.
    ''' </summary>
    ''' <param name="MeasurementList">The list of measurements to calculate a histogram for.</param>
    ''' <param name="MinLevel">The minimum level to include in the histogram.</param>
    ''' <param name="MaxLevel">The maximum level to include in the histogram.</param>
    ''' <param name="Resolution">The width of each bucket in the histogram</param>
    ''' <returns>A list of integer counts of the number of levels in each bucket of the histogram.</returns>
    ''' <remarks></remarks>
    <Extension()> Public Function LevelHistogram(ByRef MeasurementList As IEnumerable(Of IMeasurement),
                                                 ByVal MinLevel As Double, ByVal MaxLevel As Double, ByVal Resolution As Double) As IEnumerable(Of Integer)

        Dim counts As New List(Of Integer)
        For l As Double = RoundToNearest(MinLevel, Resolution) To RoundToNearest(MaxLevel, Resolution) Step Resolution
            counts.Add(LevelRangeCount(MeasurementList, l - Resolution / 2, l + Resolution / 2))
        Next
        Return counts

    End Function

    Public Function ModalLevel(ByRef MeasurementList As List(Of IMeasurement), Resolution As Double) As Double

        Dim levels As New List(Of Double), counts As New List(Of Integer)

        For l = RoundToNearest(MeasurementList.MinimumLevel, Resolution) To _
                RoundToNearest(MeasurementList.MaximumLevel, Resolution) Step Resolution

            levels.Add(l)
            counts.Add(LevelRangeCount(MeasurementList, l - Resolution / 2, l + Resolution / 2))

        Next

        Dim maxCount As Integer = 0
        Dim typicalLevel As Double = 0

        For l = 0 To levels.Count - 1
            If counts(l) > maxCount Then
                maxCount = counts(l)
                typicalLevel = levels(l)
            End If

        Next

        Return typicalLevel

    End Function

#End Region

#Region "Calculation Filters"

    ''' <summary>
    ''' Calculate a list of DateTimes which are spanned by the MeasurementList, and which the 
    ''' CalculationFilter applies to. 
    ''' </summary>
    ''' <param name="MeasurementList">The list of measurements.</param>
    ''' <param name="CalculationFilter">The CalculationFilter to calculate start DateTimes for.</param>
    ''' <returns>The list of start DateTimes which apply to both the MeasurementList and the CalculationFilter.</returns>
    ''' <remarks>
    ''' Assumes that the measurement list is chronologically ordered.
    ''' If the TimeBase of the CalculationFilter is less than its total TimeWindow, 
    ''' then the output list of DateTimes will contain a start DateTime for each step of the TimeBase.
    ''' </remarks>
    <Extension()> Public Function GetStartDateTimesForCalculationFilter(ByRef MeasurementList As IEnumerable(Of IMeasurementScalarData),
                                                                        ByRef CalculationFilter As CalculationFilter) As IEnumerable(Of Date)

        Dim firstMeasurementStartDateTime = MeasurementList.First.getStartDateTime
        Dim lastMeasurementStartDateTime = MeasurementList.Last.getStartDateTime

        Dim firstPeriodStartOADate = firstMeasurementStartDateTime.DateOnly.AddDays(-1).ToOADate
        Dim lastPeriodStartOADate = lastMeasurementStartDateTime.ToOADate

        Dim dateTimeList As New List(Of Date)

        For d = firstPeriodStartOADate To lastPeriodStartOADate
            dateTimeList.AddRange(CalculationFilter.getStartDateTimes(Date.FromOADate(d)))
        Next

        Return dateTimeList

    End Function

    ''' <summary>
    ''' For a given CalculationFilter, split a list of measurements into sub-lists within the filter's time-spans on each day of the measurements.
    ''' </summary>
    ''' <param name="MeasurementList">The list of Measurements.</param>
    ''' <param name="CalculationFilter">The Calculation Filter</param>
    ''' <returns>A list of lists of measurements. Each sub-list is contains measurement data starting on a single day within the timespan of the filter.</returns>
    ''' <remarks>Assumes that the measurement list is chronologically ordered.</remarks>
    <Extension()> Public Function GetListsForCalculationFilter(
        ByRef MeasurementList As IEnumerable(Of IMeasurementScalarData),
        ByRef CalculationFilter As CalculationFilter) As IEnumerable(Of IEnumerable(Of IMeasurementScalarData)
    )

        ' Slice data into lists the duration of one measurement time-base

        Dim listsForCalculationFilter As New List(Of IEnumerable(Of IMeasurementScalarData))
        Dim cfTimeBase = CalculationFilter.TimeBase

        For Each d In MeasurementList.GetStartDateTimesForCalculationFilter(CalculationFilter)
            Try
                listsForCalculationFilter.Add(
                    MeasurementList.Where(
                        Function(m) m.getStartDateTime.CompareTo(d) >= 0 AndAlso
                                    m.getStartDateTime.CompareTo(d.AddDays(cfTimeBase)) < 0).ToList
                    )
            Catch ex As Exception
                ' List was empty i.e. there were measurements missing for the time span of the CalculationFilter starting on that day.
            End Try
        Next

        Return listsForCalculationFilter

    End Function

#End Region

#End Region

#Region "Filtering"

    ''' <summary>
    ''' Filter a list of raw Measurements with a CalculationFilter, and return a list of FilteredMeasurements instances,
    ''' one for each of the TimeWindows that are common to the Measurements and the CalculationFilter.
    ''' </summary>
    ''' <param name="MeasurementList"></param>
    ''' <param name="CalculationFilter"></param>
    ''' <returns>
    ''' A list of FilteredMeasurements instances, one for each TimeWindow of the CalculationFilter
    ''' that lies within the duration of the Measurements.
    ''' </returns>
    ''' <remarks>
    ''' Assumes that MeasurementList has been pre-filtered so it contains data from a single MeasurementMetric, 
    ''' Monitor and MonitorLocation.
    ''' </remarks>
    <Extension()> Public Function ApplyCalculationFilter(ByRef MeasurementList As IEnumerable(Of Measurement),
                                                         CalculationFilter As CalculationFilter) As List(Of FilteredMeasurements)

        Dim filteredMeasurementsList As New List(Of FilteredMeasurements)
        Dim filteredMeasurementScalarsList As New List(Of FilteredMeasurementScalars)

        If MeasurementList.Count = 0 Then Return filteredMeasurementsList

        Dim mFirst = MeasurementList.First

        If CalculationFilter.TimeBase = 0 Then
            ' No filtering required
            For Each m As IMeasurement In MeasurementList
                Dim multiplier = CalculationFilter.InputMultiplier
                Dim filteredMeasurementScalars = New FilteredMeasurementScalars(
                    StartDateTime:=m.getStartDateTime,
                    FilterDuration:=m.getDuration,
                    FilteredLevel:=m.getLevel * multiplier,
                    NumberOfInputMeasurements:=1,
                    TotalInputMeasurementsDuration:=m.getDuration,
                    Filter:=CalculationFilter
                )
                filteredMeasurementsList.Add(New FilteredMeasurements(
                    filteredMeasurementScalars,
                    mFirst.MeasurementMetric, mFirst.Monitor, mFirst.MonitorLocation,
                    CalculationFilter, mFirst.Project
                ))
            Next

        Else

            ' (1) Slice data into lists with a duration equal to one calculation filter time-base
            Dim listsForFilter = MeasurementList.GetListsForCalculationFilter(CalculationFilter)
            Dim filteredMeasurementStartDateTimes = _
                MeasurementList.GetStartDateTimesForCalculationFilter(CalculationFilter).ToList

            ' (2) Apply filter to each list and create a list of filtered measurement scalars
            For i = listsForFilter.Count - 1 To 0 Step -1
                ' calculate filtered level
                If listsForFilter(i).Count > 0 Then
                    ' the list contains measurements
                    Dim filteredLevel As Double
                    Select Case CalculationFilter.CalculationAggregateFunction.FunctionName
                        Case "Nothing" : filteredLevel = listsForFilter(i).MaximumLevel
                        Case "Minimum" : filteredLevel = listsForFilter(i).MinimumLevel
                        Case "Maximum" : filteredLevel = listsForFilter(i).MaximumLevel
                        Case "Linear Average" : filteredLevel = listsForFilter(i).LinearAverageLevel
                        Case "Log Average" : filteredLevel = listsForFilter(i).LogarithmicAverageLevel
                        Case "Root Mean Square" : filteredLevel = listsForFilter(i).RootMeanSquareLevel
                    End Select
                    ' scale result by input multiplier
                    filteredLevel *= CalculationFilter.InputMultiplier
                    ' add filteredMeasurementScalars object to list
                    filteredMeasurementScalarsList.Insert(
                        0,
                        New FilteredMeasurementScalars(
                            StartDateTime:=filteredMeasurementStartDateTimes(i),
                            FilterDuration:=CalculationFilter.TimeBase,
                            FilteredLevel:=filteredLevel,
                            NumberOfInputMeasurements:=listsForFilter(i).Count,
                            TotalInputMeasurementsDuration:=listsForFilter(i).TotalDuration,
                            Filter:=CalculationFilter
                        )
                    )
                Else
                    ' the list has no measurements so remove the associated start DateTime
                    filteredMeasurementStartDateTimes.RemoveAt(i)
                End If
            Next i

            ' (3) Create filtered measurements with selected properties of original measurement data
            For Each fms In filteredMeasurementScalarsList
                filteredMeasurementsList.Add(
                    New FilteredMeasurements(
                        fms, mFirst.MeasurementMetric, mFirst.Monitor, mFirst.MonitorLocation,
                        CalculationFilter, mFirst.Project
                        )
                    )
            Next

        End If

        Return filteredMeasurementsList

    End Function

    ''' <summary>
    ''' Read measurements from a database, filter them and return a FilteredMeasurementsSequence
    ''' </summary>
    ''' <param name="MeasurementsDAL"></param>
    ''' <param name="ReadMeasurementParameters"></param>
    ''' <param name="CalculationFilter"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ReadAndFilter(MeasurementsDAL As IMeasurementsDAL,
                                  ReadMeasurementParameters As ReadMeasurementParameters,
                                  CalculationFilter As CalculationFilter) As FilteredMeasurementsSequence

        ' Read measurements from database
        Dim measurements = MeasurementsDAL.ReadMeasurements(
            ReadMeasurementParameters
            )

        ' Round measurements to appropriate precision
        For Each m In measurements
            m.Level = Round(m.Level, m.MeasurementMetric.RoundingDecimalPlaces, MidpointRounding.AwayFromZero)
        Next

        ' Apply CalculationFilter to get a list of FilteredMeasurements
        Dim filteredMeasurementsList = measurements.ApplyCalculationFilter(CalculationFilter)

        ' Return a new FilteredMeasurementsSequence
        Return New FilteredMeasurementsSequence(filteredMeasurementsList,
                                                CalculationFilter)

    End Function

    <Extension()> Public Function DownSample(ByRef MeasurementList As IEnumerable(Of Measurement),
                                             ToNumberOfMeasurements As Integer) As List(Of Measurement)

        If MeasurementList.Count > ToNumberOfMeasurements Then
            Dim newMeasurementList As New List(Of Measurement)
            For m = 0 To MeasurementList.Count - 1 Step MeasurementList.Count / ToNumberOfMeasurements
                newMeasurementList.Add(MeasurementList(Math.Round(m)))
            Next
            MeasurementList = newMeasurementList
        End If

        Return MeasurementList

    End Function

#Region "Temporal"

    ''' <summary>
    ''' Filters a list of Measurements so that it only contains Measurements which begin between the specified DateTimeRanges.
    ''' </summary>
    ''' <param name="MeasurementList"></param>
    ''' <param name="DateTimeRangeList"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Assumes the DateRanges are non-overlapping. If they overlap, Measurements will be added to the new list twice.
    ''' </remarks>
    <Extension()> Public Function FilterDateTimeRanges(ByRef MeasurementList As IEnumerable(Of Measurement),
                                                       DateTimeRangeList As IEnumerable(Of DateTimeRange)) As List(Of Measurement)

        Dim newList As New List(Of Measurement)

        For Each dtRange In DateTimeRangeList
            If dtRange IsNot Nothing Then
                Dim dtMeasurements = MeasurementList.Where(
                    Function(m) m.StartDateTime.ToOADate >= dtRange.StartDateTime.ToOADate And
                                m.StartDateTime.ToOADate < dtRange.EndDateTime.ToOADate
                    )
                If dtMeasurements IsNot Nothing Then newList.AddRange(dtMeasurements.ToList)
            End If
        Next

        Return newList

    End Function

    'Public Function DailyTimeWindows(ByRef MeasurementList As IEnumerable(Of IMeasurement), StartTime As Date, EndTime As Date) As IEnumerable(Of IEnumerable(Of IMeasurement))

    '    ' Takes a list of measurements over several days and returns a list of lists of measurements on each day
    '    ' The lists of measurements returned are between the start and end time passed to the function
    '    ' Also handles scenarios where the time period between the start and end times spans 12am
    '    ' Assumes the measurements are ordered ascending by startdatetime

    '    Dim dataLists As New List(Of IEnumerable(Of IMeasurement))

    '    If MeasurementList.Count > 0 Then

    '        Dim tStart = StartTime.TimeOnly, tEnd = EndTime.TimeOnly
    '        Dim dstart = MeasurementList.First.getStartDateTime.DateOnly, dEnd = MeasurementList.Last.getStartDateTime.DateOnly

    '        For Each d As Date In DateList(dstart, dEnd)
    '            Dim dd = d
    '            If tStart.CompareTo(tEnd) < 0 Then ' same day
    '                dataLists.Add((From md In MeasurementList
    '                               Where md.getStartDateTime.CompareTo(dd.Add(tStart.ToTimeSpan)) >= 0 _
    '                               AndAlso md.getStartDateTime.CompareTo(dd.Add(tEnd.ToTimeSpan)) < 0
    '                               Select md).ToList)
    '            ElseIf tStart.CompareTo(tEnd) > 0 Then ' overnight
    '                dataLists.Add((From md In MeasurementList
    '                               Where md.getStartDateTime.CompareTo(dd.Add(tStart.ToTimeSpan)) >= 0 _
    '                               AndAlso md.getStartDateTime.CompareTo(dd.AddDays(1).Add(tEnd.ToTimeSpan)) < 0
    '                               Select md).ToList)
    '            Else ' 24 hr (same as overnight)
    '                dataLists.Add((From md In MeasurementList
    '                               Where md.getStartDateTime.CompareTo(dd.Add(tStart.ToTimeSpan)) >= 0 _
    '                               AndAlso md.getStartDateTime.CompareTo(dd.AddDays(1).Add(tEnd.ToTimeSpan)) < 0
    '                               Select md).ToList)
    '            End If

    '        Next d

    '    End If

    '    Return dataLists

    'End Function

    'Public Function FilterByStartHour(ByRef MeasurementList As IEnumerable(Of IMeasurement), StartHour As Integer) As IEnumerable(Of IMonitoringDataExtended)

    '    Try
    '        Return (From MLs In MeasurementList
    '                Where MLs.getStartDateTime.Hour = StartHour
    '                Select MLs).ToList
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try

    'End Function

    'Public Function FilterByDay(ByRef MeasurementList As IEnumerable(Of IMeasurement), ByVal DayNames As List(Of String)) As IEnumerable(Of IMonitoringDataExtended)

    '    Try
    '        Return (From MLs In MeasurementList
    '                Where DayNames.Contains(WeekdayName(Weekday(MLs.getStartDateTime, FirstDayOfWeek.Monday), False, FirstDayOfWeek.Monday))
    '                Select MLs).ToList
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try

    'End Function

    'Public Function FilterByDay(ByRef MeasurementList As IEnumerable(Of IMeasurement), ByVal DaysOfWeek As IEnumerable(Of Integer))

    '    ' e.g. if DayStartTime = 6am then Monday is defined as Mon@6am <= Monday < Tue@6am

    '    Return (From d In MeasurementList Where DaysOfWeek.Contains(Weekday(d.getStartDateTime, FirstDayOfWeek.Monday)) Select d).ToList

    'End Function
    'Public Shared Function FilterByDay(ByRef MeasurementList As List(Of MonitoringData), ByVal DaysOfWeek As List(Of Integer))

    '    ' e.g. if DayStartTime = 6am then Monday is defined as Mon@6am <= Monday < Tue@6am

    '    Return (From d In MeasurementList Where DaysOfWeek.Contains(Weekday(d.StartDateTime, FirstDayOfWeek.Monday)) Select d).ToList

    'End Function
    'Public Shared Function FilterByDay(ByRef MeasurementList As List(Of MonitoringData), ByVal DaysOfWeek As List(Of Integer),
    '                                   ByVal DayStartTime As TimeSpan) As List(Of MonitoringData)

    '    ' e.g. if DayStartTime = 6am then Monday is defined as Mon@6am <= Monday < Tue@6am

    '    Return (From d In MeasurementList Where DaysOfWeek.Contains(Weekday(d.StartDateTime.Subtract(DayStartTime), FirstDayOfWeek.Monday)) Select d).ToList

    'End Function

#End Region

#End Region

#Region "Assessment"

    ''' <summary>
    ''' A class to calculate the results of an assessment for a given list of FilteredMeasurements and a Threshold Level for a given date.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AssessmentResultSingleDay

        Private filteredMeasurements As IEnumerable(Of FilteredMeasurements)
        Private assessmentResultDate As Date
        Private calculationFilter As CalculationFilter
        Private thresholdRangeLower As Double
        Private thresholdRangeUpper As Double
        Private thresholdTypeName As String
        Private Delegate Function Comparator(a As Double, b As Double) As Boolean
        Private upperComparator As Comparator
        Private lowerComparator As Comparator
        Private roundingDecimalPlaces As Integer

        ''' <summary>
        ''' Create a new instance of AssessmentResultSingleDay
        ''' </summary>
        ''' <param name="FilteredMeasurements">A list of FilteredMeasurements instances.</param>
        ''' <param name="AssessmentDate">The Date which the assessment is for.</param>
        ''' <param name="CalculationFilter">The calculation filter that was used to filter the original Measurements.</param>
        ''' <param name="ThresholdRangeLower">The Lower Bound of the Threshold Range to compare the filtered level of each FilteredMeasurements instance to.</param>
        ''' <param name="ThresholdRangeUpper">The Upper Bound of the Threshold Range to compare the filtered level of each FilteredMeasurements instance to.</param>
        ''' <param name="ThresholdTypeName">The Method to use for Assessing whether a filtered level is in the range at the bottom i.e. "Greater Than" or "Greater Than or Equal To".</param>
        ''' <remarks></remarks>
        Public Sub New(FilteredMeasurements As IEnumerable(Of FilteredMeasurements),
                       AssessmentDate As Date,
                       CalculationFilter As CalculationFilter,
                       ThresholdRangeLower As Double,
                       ThresholdRangeUpper As Double,
                       RoundingDecimalPlaces As Integer,
                       ThresholdTypeName As String)

            Me.filteredMeasurements = FilteredMeasurements
            Me.assessmentResultDate = AssessmentDate
            Me.calculationFilter = CalculationFilter
            Me.thresholdRangeLower = ThresholdRangeLower
            Me.thresholdRangeUpper = ThresholdRangeUpper
            Me.thresholdTypeName = ThresholdTypeName
            Me.roundingDecimalPlaces = RoundingDecimalPlaces

            Me.lowerComparator = New Comparator(AddressOf GreaterThanOrEqualTo)
            Me.upperComparator = New Comparator(AddressOf LessThan)
            If ThresholdTypeName = "Greater Than" Then
                Me.lowerComparator = New Comparator(AddressOf GreaterThan)
                Me.upperComparator = New Comparator(AddressOf LessThanOrEqualTo)
            End If

        End Sub
        Public ReadOnly Property AssessmentDate As Date
            Get
                Return assessmentResultDate
            End Get
        End Property
        Public Function getFilteredMeasurements() As IEnumerable(Of FilteredMeasurements)

            Return filteredMeasurements

        End Function

        Public Function getStartDateTimes() As IEnumerable(Of DateTime)

            Return filteredMeasurements.Select(Function(fm) fm.getStartDateTime).ToList

        End Function

        Public Function getCalculationFilter() As CalculationFilter

            Return calculationFilter

        End Function

        Public Function getNumberOfExceedances() As Nullable(Of Integer)

            If filteredMeasurements.Count = 0 Then Return Nothing

            Dim numExceedances As Integer = 0
            For Each fm In filteredMeasurements
                Dim filteredLevel = Round(
                    fm.getFilteredLevel(),
                    Me.roundingDecimalPlaces,
                    MidpointRounding.AwayFromZero
                )
                If (Me.lowerComparator(filteredLevel, Me.thresholdRangeLower) And
                    Me.upperComparator(filteredLevel, Me.thresholdRangeUpper)) Then
                    numExceedances += 1
                End If
            Next

            Return numExceedances

        End Function
        ''' <summary>
        ''' Return the highest level from the list of FilteredMeasurements instances
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function getMaxLevel() As Nullable(Of Double)

            If filteredMeasurements.Count = 0 Then Return Nothing
            Return (From fms In filteredMeasurements Select fms.getFilteredLevel).Max()

        End Function

        Public Function getNumberOfMeasurements() As Integer

            Return filteredMeasurements.Count

        End Function

        Public Function isTriggeredAt(startDateTime As Date)

            If filteredMeasurements.Where(Function(fm) fm.getStartDateTime() = startDateTime).Count <> 1 Then Return False
            Dim filteredLevel = filteredMeasurements.Single(Function(fm) fm.getStartDateTime() = startDateTime).getFilteredLevel()
            If (Me.lowerComparator(filteredLevel, Me.thresholdRangeLower) And
                Me.upperComparator(filteredLevel, Me.thresholdRangeUpper)) Then
                Return True
            End If

            Return False

        End Function

        Public Function getThresholdRangeLower() As Double

            Return thresholdRangeLower

        End Function

    End Class

    ''' <summary>
    ''' Calculates a list of AssessmentResultSingleDay instances for the Measurements on the AssessmentDates.
    ''' </summary>
    ''' <param name="Measurements">The list of Measurements to apply the CalculationFilter to.</param>
    ''' <param name="CalculationFilter">The CalculationFilter to use to filter the Measurements</param>
    ''' <param name="ThresholdRangeLower">The Lower Bound of the Threshold Range to compare the filtered level of each FilteredMeasurements instance to.</param>
    ''' <param name="ThresholdRangeUpper">The Upper Bound of the Threshold Range to compare the filtered level of each FilteredMeasurements instance to.</param>
    ''' <param name="ThresholdTypeName">The Method to use for Assessing whether a filtered level is in the range at the bottom i.e. "Greater Than" or "Greater Than or Equal To".</param>
    ''' <param name="AssessmentDates">The Dates on which to carry out the Assessment.</param>
    ''' <returns></returns>
    ''' <remarks>Assumes that any Measurements taken outside of working hours or during extreme weather conditions have already been removed.</remarks>
    Public Function GetThresholdExceedances(Measurements As IEnumerable(Of Measurement),
                                            CalculationFilter As CalculationFilter,
                                            ThresholdRangeLower As Double,
                                            ThresholdRangeUpper As Double,
                                            ThresholdTypeName As String,
                                            RoundingDecimalPlaces As Integer,
                                            AssessmentDates As IEnumerable(Of Date)) As List(Of AssessmentResultSingleDay)

        Dim criterionExceedances As New List(Of AssessmentResultSingleDay)

        For Each assessmentDate In AssessmentDates

            Dim filteredMeasurementsList As New List(Of FilteredMeasurements)
            Dim timeWindow = CalculationFilter.getCalculationTimeWindow(assessmentDate)

            If timeWindow IsNot Nothing Then
                Dim dailyMeasurements = Measurements.Where(
                    Function(m) m.StartDateTime.ToOADate >= timeWindow.StartDateTime.ToOADate And
                                m.StartDateTime.ToOADate < timeWindow.EndDateTime.ToOADate
                ).ToList
                filteredMeasurementsList = dailyMeasurements.ApplyCalculationFilter(CalculationFilter)
            End If

            Dim result = New AssessmentResultSingleDay(
                FilteredMeasurements:=filteredMeasurementsList,
                AssessmentDate:=assessmentDate,
                CalculationFilter:=CalculationFilter,
                ThresholdRangeLower:=ThresholdRangeLower,
                ThresholdRangeUpper:=ThresholdRangeUpper,
                RoundingDecimalPlaces:=RoundingDecimalPlaces,
                ThresholdTypeName:=ThresholdTypeName
            )
            criterionExceedances.Add(result)

        Next

        Return criterionExceedances

    End Function

    Public Class TriggerEvent

        Public Property StartDateTime As DateTime
        Public Property TriggeredLevel As Double

    End Class

    ''' <summary>
    ''' Sum the Exceedances across a list of AssessmentResultSingleDay's
    ''' </summary>
    ''' <param name="AssessmentResults">The list of AssessmentResultSingleDay's</param>
    ''' <returns>The sum of Exceedances</returns>
    ''' <remarks></remarks>
    <Extension> Public Function SumOfExceedances(ByRef AssessmentResults As List(Of AssessmentResultSingleDay)) As Integer

        Dim numExceedances As Integer = 0

        For Each result In AssessmentResults
            Dim resultExceedances = result.getNumberOfExceedances
            If Not resultExceedances Is Nothing Then
                numExceedances += CInt(resultExceedances)
            End If
        Next

        Return numExceedances

    End Function
    <Extension> Public Function SumOfExceedances(ByRef AssessmentResults As List(Of List(Of AssessmentResultSingleDay))) As Integer

        Dim numExceedances As Integer = 0

        For Each results In AssessmentResults
            numExceedances += results.SumOfExceedances
        Next
        Return numExceedances

    End Function
    ''' <summary>
    ''' Count the number of AssessmentResultSingleDay's which have at least 1 Exceedance.
    ''' </summary>
    ''' <param name="AssessmentResults"></param>
    ''' <returns>The number of AssessmentResultSingleDay's in the list with at least 1 Exceedance.</returns>
    ''' <remarks></remarks>
    <Extension> Public Function CountResultsWithExceedances(ByRef AssessmentResults As List(Of AssessmentResultSingleDay)) As Integer

        Dim numResultsWithExceedances As Integer = 0

        For Each result In AssessmentResults
            Dim resultExceedances = result.getNumberOfExceedances
            If Not resultExceedances Is Nothing Then
                If CInt(resultExceedances) > 0 Then
                    numResultsWithExceedances += 1
                End If
            End If
        Next

        Return numResultsWithExceedances

    End Function
    ''' <summary>
    ''' Counts the number of Dates which have at least 1 Exeedance.
    ''' </summary>
    ''' <param name="AssessmentResults">A list of list of assessment results.</param>
    ''' <returns></returns>
    ''' <remarks>Typically, the sublists will each be for a single criterion over separate dates, but this is not required.</remarks>
    <Extension> Public Function CountDaysOfExceedances(ByRef AssessmentResults As List(Of List(Of AssessmentResultSingleDay)))

        Dim resultsDict = New Dictionary(Of Date, Integer)
        For Each subList In AssessmentResults
            For Each result In subList
                Dim resultDate = result.AssessmentDate
                If Not resultsDict.ContainsKey(resultDate) Then
                    resultsDict.Add(resultDate, 0)
                End If
                Dim resultExceedances = result.getNumberOfExceedances
                If Not IsNothing(resultExceedances) Then
                    resultsDict(resultDate) += CInt(resultExceedances)
                End If
            Next
        Next

        Dim numDaysWithExceedances = 0
        For Each kv In resultsDict
            If kv.Value > 0 Then
                numDaysWithExceedances += 1
            End If
        Next

        Return numDaysWithExceedances

    End Function
    <Extension> Public Function CountDaysOfExceedances(ByRef AssessmentResults As List(Of AssessmentResultSingleDay)) As Integer

        Dim resultsDict = New Dictionary(Of Date, Integer)
        For Each result In AssessmentResults
            Dim resultDate = result.AssessmentDate
            If Not resultsDict.ContainsKey(resultDate) Then
                resultsDict.Add(resultDate, 0)
            End If
            Dim resultExceedances = result.getNumberOfExceedances
            If Not IsNothing(resultExceedances) Then
                resultsDict(resultDate) += CInt(resultExceedances)
            End If
        Next

        Dim numDaysWithExceedances = 0
        For Each kv In resultsDict
            If kv.Value > 0 Then
                numDaysWithExceedances += 1
            End If
        Next

        Return numDaysWithExceedances

    End Function

    <Extension> Public Function GetDailyThresholdExceedanceEvents(ByRef AssessmentResults As List(Of AssessmentResultSingleDay)) As List(Of Integer)

        ' AssessmentResults should be ordered by trigger level e.g. 1, 1, 1, 5, 5, 5 for PPV and all be on the same day

        Dim triggerEvents As New List(Of TriggerEvent)

        ' Get start times for each list of filtered measurements in the assessment results
        ' Join them together, order ascending and find unique start times
        Dim startDateTimes = AssessmentResults.SelectMany(
            Function(ar) ar.getStartDateTimes
        ).OrderBy(Function(d) d).Distinct().ToList()

        ' For each unique startdatetime:
        For Each sdt In startDateTimes

            '   Find the highest triggered criterion and add a TriggerEvent with the startdatetime and range
            Dim triggeredResults = AssessmentResults.Where(Function(ar) ar.isTriggeredAt(sdt) = True)
            If triggeredResults.Count > 0 Then
                Dim maxTriggeredLevel = triggeredResults.Select(Function(tr) tr.getThresholdRangeLower).Max()
                triggerEvents.Add(
                    New TriggerEvent With {
                        .StartDateTime = sdt,
                        .TriggeredLevel = maxTriggeredLevel
                    }
                )
            End If

        Next

        Dim eventCounts As New List(Of Integer)
        ' For each criterion:
        For Each assessmentResult In AssessmentResults
            ' Find how many trigger events with that criterion's trigger range
            ' Add to the list of results
            eventCounts.Add(
                triggerEvents.Where(Function(te) te.TriggeredLevel = assessmentResult.getThresholdRangeLower).Count()
            )
        Next

        Return eventCounts

    End Function

#End Region

#Region "Summary"

    Public Class LevelsSummarySingleDay

        Private filteredMeasurements As IEnumerable(Of FilteredMeasurements)
        Private summaryDate As Date
        Private calculationFilter As CalculationFilter

        Public Sub New(FilteredMeasurements As IEnumerable(Of FilteredMeasurements),
                       SummaryDate As Date,
                       CalculationFilter As CalculationFilter)

            Me.filteredMeasurements = FilteredMeasurements
            Me.summaryDate = SummaryDate
            Me.calculationFilter = CalculationFilter

        End Sub

        Public Function getNumberOfMeasurements() As Integer

            Return filteredMeasurements.Count

        End Function

    End Class

    Public Function GetSummaryLevels(Measurements As IEnumerable(Of Measurement),
                                     CalculationFilter As CalculationFilter,
                                     SummaryDates As IEnumerable(Of Date)) As List(Of LevelsSummarySingleDay)

        Dim summaryLevels As New List(Of LevelsSummarySingleDay)

        For Each summaryDate In SummaryDates

            Dim filteredMeasurementsList As New List(Of FilteredMeasurements)
            Dim timeWindow = CalculationFilter.getCalculationTimeWindow(summaryDate)

            If timeWindow IsNot Nothing Then
                Dim dailyMeasurements = Measurements.Where(
                    Function(m) m.StartDateTime.ToOADate >= timeWindow.StartDateTime.ToOADate And
                                m.StartDateTime.ToOADate < timeWindow.EndDateTime.ToOADate
                ).ToList
                filteredMeasurementsList = dailyMeasurements.ApplyCalculationFilter(CalculationFilter)
            End If

            Dim result = New LevelsSummarySingleDay(filteredMeasurementsList, summaryDate, CalculationFilter)
            summaryLevels.Add(result)

        Next

        Return summaryLevels

    End Function


#End Region

End Module

