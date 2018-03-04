Imports System.Runtime.CompilerServices
Imports System.Math

Public Module Maths

    <Extension()> Public Function RoundToNearest(theNumber As Double, toNearest As Double) As Double

        Dim d As Double = theNumber / toNearest
        Return Round(d, MidpointRounding.AwayFromZero) * toNearest

    End Function

#Region "Comparators"

    Public Function GreaterThan(a As Double, b As Double) As Boolean

        If a > b Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function GreaterThanOrEqualTo(a As Double, b As Double) As Boolean

        If a >= b Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function LessThan(a As Double, b As Double) As Boolean

        If a < b Then
            Return True
        Else
            Return False
        End If

    End Function
    Public Function LessThanOrEqualTo(a As Double, b As Double) As Boolean

        If a <= b Then
            Return True
        Else
            Return False
        End If

    End Function

#End Region

    <Extension()> Public Function ToObjectList(ByRef DoubleList As IEnumerable(Of Double)) As List(Of Object)

        Return DoubleList.Select(Function(d) CType(d, Object)).ToList

    End Function

    <Extension()> Public Function ToObjectArray(ByRef DoubleList As IEnumerable(Of Double)) As Object()

        Return DoubleList.ToObjectList.ToArray

    End Function

End Module

