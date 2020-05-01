Imports System.Runtime.CompilerServices
Imports System.Math

Public Module Maths

    ''' <summary>
    ''' Round a floating point number to toNearest decimal places.
    ''' </summary>
    ''' <param name="theNumber"></param>
    ''' <param name="toNearest"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> Public Function RoundToNearest(theNumber As Double, toNearest As Double) As Double

        Dim d As Double = theNumber / toNearest
        Return Round(d, MidpointRounding.AwayFromZero) * toNearest

    End Function

#Region "Comparators"

    ''' <summary>
    ''' Return True if a is greater than b
    ''' </summary>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GreaterThan(a As Double, b As Double) As Boolean

        If a > b Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Return True if a is greater than or equal to b
    ''' </summary>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GreaterThanOrEqualTo(a As Double, b As Double) As Boolean

        If a >= b Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Return True if a is less than b
    ''' </summary>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LessThan(a As Double, b As Double) As Boolean

        If a < b Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' Return True if a is less than or equal to b
    ''' </summary>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LessThanOrEqualTo(a As Double, b As Double) As Boolean

        If a <= b Then
            Return True
        Else
            Return False
        End If

    End Function

#End Region

    ''' <summary>
    ''' Convert a List of Doubles to a List of Objects.
    ''' </summary>
    ''' <param name="DoubleList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> Public Function ToObjectList(ByRef DoubleList As IEnumerable(Of Double)) As List(Of Object)

        Return DoubleList.Select(Function(d) CType(d, Object)).ToList

    End Function

    ''' <summary>
    ''' Convert a List of Doubles to an Array of Objects.
    ''' </summary>
    ''' <param name="DoubleList"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> Public Function ToObjectArray(ByRef DoubleList As IEnumerable(Of Double)) As Object()

        Return DoubleList.ToObjectList.ToArray

    End Function

End Module

