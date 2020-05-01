Imports System.Runtime.CompilerServices
Imports System

Public Module Strings

    ''' <summary>
    ''' Convert a String into a String which can be used for a Route.
    ''' </summary>
    ''' <param name="FromString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> Public Function ToRouteName(ByVal FromString As String) As String

        Return FromString.Replace(" ", "-").Replace("%", "o|o")

    End Function

    ''' <summary>
    ''' Convert a Route string into its original representation.
    ''' </summary>
    ''' <param name="RouteName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> Public Function FromRouteName(ByVal RouteName As String) As String

        Return RouteName.Replace("-", " ").Replace("   ", " - ").Replace("o|o", "%")

    End Function

    ''' <summary>
    ''' Add spaces between the words of text in Pascal case.
    ''' </summary>
    ''' <param name="PascalCaseText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> Public Function SpaceOutPascalCaseText(PascalCaseText As String) As String

        Dim spacedText As String = ""

        Try
            For i = 0 To PascalCaseText.Count - 2
                spacedText += PascalCaseText(i)
                If Char.IsLower(PascalCaseText(i)) = True And Char.IsUpper(PascalCaseText(i + 1)) = True Then
                    spacedText += " "
                End If
            Next
            spacedText += PascalCaseText(PascalCaseText.Count - 1)

        Catch ex As Exception
            Return ""
        End Try

        Return spacedText

    End Function

    ''' <summary>
    ''' Return the pluralised version of a word.
    ''' </summary>
    ''' <param name="Word"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> Public Function Pluralise(Word As String) As String

        Dim newWord As String

        If Word.EndsWith("y") = True Then
            newWord = Left(Word, Len(Word) - 1) + "ies"
        Else
            newWord = Word + "s"
        End If

        Return newWord

    End Function

    ''' <summary>
    ''' Convert a string-encoded version of a Dictionary into a Dictionary object.
    ''' </summary>
    ''' <param name="RouteValuesString"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Extension()> Public Function ToDict(RouteValuesString As String) As Dictionary(Of String, Object)

        Dim rvDict As New Dictionary(Of String, Object)

        Dim rvs = RouteValuesString
        rvs = rvs.TrimStart("{")
        rvs = rvs.TrimEnd("}")
        rvs = rvs.Trim()
        Dim keyValuePairs = rvs.Split(", ")
        For Each keyValuePair In keyValuePairs
            Dim strKV = Trim(keyValuePair).Split("=")
            rvDict.Add(Trim(strKV(0)), Trim(strKV(1)))
        Next

        Return rvDict

    End Function

    ''' <summary>
    ''' Return the indefinite article for a given word.
    ''' </summary>
    ''' <param name="ForWord"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IndefiniteArticle(ForWord As String) As String

        Dim firstLetter = LCase(Left(ForWord, 1))
        If "aeiouh".Contains(firstLetter) Then Return "an"
        Return "a"

    End Function

End Module
