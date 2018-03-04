Module Debugging

    Public Sub LogModelStateErrors(ModelStateDictionary As ModelStateDictionary)

        Dim errors = From modelstate In ModelStateDictionary.AsQueryable().Where(
            Function(f) f.Value.Errors.Count > 0
        ) Select New With {.Title = modelstate.Key, .Value = modelstate.Value.ToString}
        Debug.WriteLine("Errors found in the following ModelState entries:")
        For Each e In errors
            Debug.WriteLine(e.ToString)
        Next

    End Sub

    Public Class DebugEvent

        Public Property dt As DateTime
        Public Property name As String


        Public Sub New(name As String)

            Me.name = name
            Me.dt = DateTime.Now()

        End Sub

    End Class

End Module
