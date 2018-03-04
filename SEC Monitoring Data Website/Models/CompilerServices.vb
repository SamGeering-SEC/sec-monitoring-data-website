Imports System.Linq.Expressions
Imports System.Reflection

Module CompilerServicesExtensions

    ''' <summary>
    ''' Get the string equivalent of an instance property name using Reflection.
    ''' </summary>
    ''' <typeparam name="T">Any class type.</typeparam>
    ''' <typeparam name="TReturn">Any Property type for which a string translation is needed.</typeparam>
    ''' <param name="obj">A class instance.</param>
    ''' <param name="expression">Lambda expression describing the Property for which a string translation is needed.</param>
    ''' <returns>A string translation of a class Property member.</returns>
    ''' <remarks>http://handcraftsman.wordpress.com/2008/11/11/how-to-get-c-property-names-without-magic-strings/</remarks>
    <System.Runtime.CompilerServices.Extension> _
    Public Function GetPropertyName(Of T As Class, TReturn)(obj As T, expression As Expression(Of Func(Of T, TReturn))) As String
        Dim body As MemberExpression = DirectCast(expression.Body, MemberExpression)
        Return body.Member.Name
    End Function

    ''' <summary>
    ''' Set the value of a Public property specified by an input string.
    ''' </summary>
    ''' <param name="input">Any object instance.</param>
    ''' <param name="propertyName">Name of a Public property for which the value is set.</param>
    ''' <param name="value">Object value to set.</param>
    <System.Runtime.CompilerServices.Extension> _
    Public Sub SetValueByPropertyName(input As Object, propertyName As String, value As Object)
        Dim prop As PropertyInfo = input.[GetType]().GetProperty(propertyName, BindingFlags.Instance Or BindingFlags.[Public])
        prop.SetValue(input, value)
    End Sub

    ' ''' <summary>
    ' ''' Updates the value of an individual property and resets the ModelState.
    ' ''' </summary>
    ' ''' <typeparam name="T">Any class type.</typeparam>
    ' ''' <typeparam name="TReturn"></typeparam>
    ' ''' <typeparam name="TReturn">Any Property type for which a string translation is needed.</typeparam>
    ' ''' <param name="model">A model class instance.</param>
    ' ''' <param name="expression">Lambda expression describing the Property for which a string translation is needed.</param>
    ' ''' <param name="updateValue">The new value to set on the model.</param>
    '<System.Runtime.CompilerServices.Extension> _
    'Public Sub Update(Of T As Class, TReturn)(modelState As System.Web.Mvc.ModelStateDictionary, model As T, expression As Expression(Of Func(Of T, TReturn)), updateValue As Object)
    '    Dim propName As String = model.GetPropertyName(expression)
    '    modelState.Remove(propName)
    '    model.SetValueByPropertyName(propName, updateValue)
    'End Sub

End Module
