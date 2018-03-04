Public Module Lists

    Public Function ToCSVstring(ByVal thisList As List(Of Integer), Optional SpaceAfterComma As Boolean = True) As String

        Dim thisString As String = ""

        If thisList.Count > 0 Then

            For Each i As Integer In thisList
                thisString += i.ToString + "," + IIf(SpaceAfterComma = True, " ", "")
            Next

            thisString = Left(thisString, Len(thisString) - IIf(SpaceAfterComma = True, 2, 1))
        End If

        Return thisString

    End Function

    Function GetSequentialRanges(ByVal thisList As List(Of Integer)) As List(Of List(Of Integer))

        ' Calculate the continuous ranges in the list e.g. [1, 2, 3, 7, 8, 9] => [[1, 3], [7, 9]]

        Dim starts = New List(Of Integer)
        Dim ends = New List(Of Integer)
        starts.Add(thisList(0))
        For i = 1 To thisList.Count - 1
            If thisList(i) - thisList(i - 1) <> 1 Then
                ends.Add(thisList(i - 1))
                starts.Add(thisList(i))
            End If
        Next
        ends.Add(thisList(thisList.Count - 1))

        Dim sequentialRanges = New list(Of list(Of Integer))
        For i = 0 To starts.count - 1
            Dim sequentialRange = New list(Of Integer)
            sequentialRanges.add(New list(Of Integer) From {starts(i), ends(i)})
        Next

        Return sequentialRanges

    End Function


End Module
