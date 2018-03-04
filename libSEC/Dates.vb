Imports System.Runtime.CompilerServices
Imports System

Public Module Dates

#Region "Enums"

    Public Enum TimeResolutionType
        Millisecond
        Second
        Minute
        Hour
        Day
        Week
        FourWeeks
        Month
        Quarter
        SixMonths
        Year
    End Enum

#End Region

    Public Function MondayInTheWeekOf(ByVal ThisDate As Date) As Date

        Dim DoW

        DoW = ThisDate.DayOfWeek

        Select Case DoW
            Case 0 ' Sunday
                Return ThisDate.AddDays(-6)
            Case Else
                Return ThisDate.AddDays(1 - DoW)
        End Select

    End Function

    <Extension()> Public Function TimeOnly(DateTime As Date) As Date

        Return Date.FromOADate(DateTime.ToOADate - Int(DateTime.ToOADate))

    End Function
    <Extension()> Public Function DateOnly(DateTime As Date) As Date

        Return Date.FromOADate(Int(DateTime.ToOADate))

    End Function
    <Extension()> Public Function DatesOnly(ByRef DateTimeList As IEnumerable(Of Date)) As IEnumerable(Of Date)

        Dim dateList As New List(Of Date)
        For Each thisDateTime In DateTimeList
            Dim thisDate = thisDateTime.DateOnly
            If dateList.Count = 0 OrElse dateList.Contains(thisDate) = False Then dateList.Add(thisDate)
        Next

        Return dateList

    End Function
    <Extension()> Public Function ToTimeSpan(DateTime As Date) As TimeSpan

        Return TimeSpan.FromDays(TimeOnly(DateTime).ToOADate)

    End Function
    <Extension()> Public Function DayName(OfDateTime As Date) As String

        Dim thisCulture = Globalization.CultureInfo.CurrentCulture
        Dim dayOfWeek As DayOfWeek = thisCulture.Calendar.GetDayOfWeek(OfDateTime)
        Return thisCulture.DateTimeFormat.GetDayName(dayOfWeek)

    End Function
    <Extension()> Public Function DateIsInDateRangeInclusive(TestDate As Date, StartDate As Date, EndDate As Date) As Boolean

        Dim tDate = TestDate.DateOnly.ToOADate

        If tDate >= StartDate.DateOnly.ToOADate And tDate <= EndDate.DateOnly.ToOADate Then Return True
        Return False

    End Function

    ''' <summary>
    ''' Returns the earlier date of the two given.
    ''' </summary>
    ''' <param name="Date1"></param>
    ''' <param name="Date2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function EarlierDate(Date1 As Date, Date2 As Date) As Date

        If Date1.ToOADate < Date2.ToOADate Then Return Date1
        Return Date2

    End Function

    ''' <summary>
    ''' Returns the later date of the two given.
    ''' </summary>
    ''' <param name="Date1"></param>
    ''' <param name="Date2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LaterDate(Date1 As Date, Date2 As Date) As Date

        If Date1.ToOADate > Date2.ToOADate Then Return Date1
        Return Date2

    End Function

#Region "Lists"

    Public Function DateList(firstDate As Date, ByVal lastDate As Date, _
                                    Optional StepType As String = "Daily", _
                                    Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Select Case StepType
            Case "Second", "Seconds", "Secondly"
                Return SecondsList(firstDate, lastDate, AddOneAfterLastDate)
            Case "Minute", "Minutes", "Minutely"
                Return MinutesList(firstDate, lastDate, AddOneAfterLastDate)
            Case "Hour", "Hours", "Hourly"
                Return HourlyList(firstDate, lastDate, AddOneAfterLastDate)
            Case "Day", "Days", "Daily"
                Return DailyList(firstDate, lastDate, AddOneAfterLastDate)
            Case "Week", "Weeks", "Weekly"
                Return WeeklyList(firstDate, lastDate, AddOneAfterLastDate)
            Case "Four Weeks", "Four Weekly"
                Return FourWeeklyList(firstDate, lastDate, AddOneAfterLastDate)
            Case "Month", "Months", "Monthly"
                Return MonthlyList(firstDate, lastDate, AddOneAfterLastDate)
            Case "Quarter", "Quarters", "Quarterly"
                Return QuarterlyList(firstDate, lastDate, AddOneAfterLastDate)
            Case "Six Months", "Six Monthly"
                Return SixMonthlyList(firstDate, lastDate, AddOneAfterLastDate)
            Case "Year", "Years", "Annual", "Annually"
                Return AnnualList(firstDate, lastDate, AddOneAfterLastDate)
            Case Else
                Return Nothing
        End Select

    End Function
    Public Function DateList(firstDate As Date, ByVal lastDate As Date, _
                                 StepType As TimeResolutionType, _
                                Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Select Case StepType
            Case TimeResolutionType.Second
                Return SecondsList(firstDate, lastDate, AddOneAfterLastDate)
            Case TimeResolutionType.Minute
                Return MinutesList(firstDate, lastDate, AddOneAfterLastDate)
            Case TimeResolutionType.Hour
                Return HourlyList(firstDate, lastDate, AddOneAfterLastDate)
            Case TimeResolutionType.Day
                Return DailyList(firstDate, lastDate, AddOneAfterLastDate)
            Case TimeResolutionType.Week
                Return WeeklyList(firstDate, lastDate, AddOneAfterLastDate)
            Case TimeResolutionType.FourWeeks
                Return FourWeeklyList(firstDate, lastDate, AddOneAfterLastDate)
            Case TimeResolutionType.Month
                Return MonthlyList(firstDate, lastDate, AddOneAfterLastDate)
            Case TimeResolutionType.Quarter
                Return QuarterlyList(firstDate, lastDate, AddOneAfterLastDate)
            Case TimeResolutionType.SixMonths
                Return SixMonthlyList(firstDate, lastDate, AddOneAfterLastDate)
            Case TimeResolutionType.Year
                Return AnnualList(firstDate, lastDate, AddOneAfterLastDate)
            Case Else
                Return Nothing
        End Select

    End Function
    Public Function SecondsList(ByVal firstDate As Date, ByVal lastDate As Date, Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Dim dList As New List(Of Date)

        Dim d As Date = firstDate
        While Date.Compare(d, lastDate) <= 0
            dList.Add(d)
            d = d.AddSeconds(1)
        End While
        If AddOneAfterLastDate = True Then dList.Add(d)

        Return dList

    End Function
    Public Function MinutesList(ByVal firstDate As Date, ByVal lastDate As Date, Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Dim dList As New List(Of Date)

        Dim d As Date = firstDate
        While Date.Compare(d, lastDate) <= 0
            dList.Add(d)
            d = d.AddMinutes(1)
        End While
        If AddOneAfterLastDate = True Then dList.Add(d)

        Return dList

    End Function
    Public Function HourlyList(ByVal firstDate As Date, ByVal lastDate As Date, Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Dim dList As New List(Of Date)

        Dim d As Date = firstDate
        While Date.Compare(d, lastDate) <= 0
            dList.Add(d)
            d = d.AddHours(1)
        End While
        If AddOneAfterLastDate = True Then dList.Add(d)

        Return dList

    End Function
    Public Function DailyList(ByVal firstDate As Date, ByVal lastDate As Date, Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Dim dList As New List(Of Date)

        Dim d As Date = firstDate
        While Date.Compare(d, lastDate) <= 0
            dList.Add(d)
            d = d.AddDays(1)
        End While
        If AddOneAfterLastDate = True Then dList.Add(d)

        Return dList

    End Function
    Public Function WeeklyList(ByVal firstDate As Date, ByVal lastDate As Date, Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Dim dList As New List(Of Date)

        Dim d As Date = firstDate
        While Date.Compare(d, lastDate) <= 0
            dList.Add(d)
            d = d.AddDays(7)
        End While
        If AddOneAfterLastDate = True Then dList.Add(d)

        Return dList

    End Function
    Public Function FourWeeklyList(ByVal firstDate As Date, ByVal lastDate As Date, Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Dim dList As New List(Of Date)

        Dim d As Date = firstDate
        While Date.Compare(d, lastDate) <= 0
            dList.Add(d)
            d = d.AddDays(28)
        End While
        If AddOneAfterLastDate = True Then dList.Add(d)

        Return dList

    End Function
    Public Function MonthlyList(ByVal firstDate As Date, ByVal lastDate As Date, Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Dim dList As New List(Of Date)
        Dim dEnd As New Date(lastDate.Year, lastDate.Month, 1)

        Dim d As New Date(firstDate.Year, firstDate.Month, 1)
        While Date.Compare(d, dEnd) <= 0
            dList.Add(d)
            d = d.AddMonths(1)
        End While
        If AddOneAfterLastDate = True Then dList.Add(d)

        Return dList

    End Function
    Public Function QuarterlyList(ByVal firstDate As Date, ByVal lastDate As Date, Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Dim dList As New List(Of Date)
        Dim dEnd As New Date(lastDate.Year, lastDate.Month, 1)

        Dim d As New Date(firstDate.Year, firstDate.Month, 1)
        While Date.Compare(d, dEnd) <= 0
            dList.Add(d)
            d = d.AddMonths(3)
        End While
        If AddOneAfterLastDate = True Then dList.Add(d)

        Return dList

    End Function
    Public Function SixMonthlyList(ByVal firstDate As Date, ByVal lastDate As Date, Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Dim dList As New List(Of Date)
        Dim dEnd As New Date(lastDate.Year, lastDate.Month, 1)

        Dim d As New Date(firstDate.Year, firstDate.Month, 1)
        While Date.Compare(d, dEnd) <= 0
            dList.Add(d)
            d = d.AddMonths(6)
        End While
        If AddOneAfterLastDate = True Then dList.Add(d)

        Return dList

    End Function
    Public Function AnnualList(ByVal firstDate As Date, ByVal lastDate As Date, Optional AddOneAfterLastDate As Boolean = False) As List(Of Date)

        Dim dList As New List(Of Date)
        Dim dEnd As New Date(lastDate.Year, lastDate.Month, 1)

        Dim d As New Date(firstDate.Year, firstDate.Month, 1)
        While Date.Compare(d, dEnd) <= 0
            dList.Add(d)
            d = d.AddYears(1)
        End While
        If AddOneAfterLastDate = True Then dList.Add(d)

        Return dList

    End Function

    <Extension()> Public Function ToStringList(ByRef DateList As IEnumerable(Of Date)) As List(Of String)

        Return DateList.Select(Function(d) d.ToString).ToList

    End Function
    <Extension()> Public Function ToStringArray(ByRef DateList As IEnumerable(Of Date)) As String()

        Return DateList.ToStringList.ToArray

    End Function
    <Extension()> Public Function MergeDateTimeLists(DateLists As IEnumerable(Of IEnumerable(Of Date))) As IEnumerable(Of Date)

        Dim newList As New List(Of Date)

        For Each dList In DateLists
            newList.AddRange(dList)
        Next

        newList.Sort()
        newList = newList.Distinct.ToList

        Return newList

    End Function
    <Extension()> Public Function ToUTCTimestamp(ByRef FromDateTime As Date) As Integer

        Dim uTime As Integer
        uTime = (FromDateTime.Subtract(New DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds
        Return uTime

    End Function
    

#End Region

    ''' <summary>
    ''' A class to represent a range of DateTime, between StartDateTime and EndDateTime
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DateTimeRange

        Public Property StartDateTime As Date
        Public Property EndDateTime As Date

    End Class

End Module
