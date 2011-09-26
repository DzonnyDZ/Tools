Namespace ComponentModelT
    ''' <summary>Implements generic wrapper class</summary>
    ''' <typeparam name="T">Type of object to wrap</typeparam>
    ''' <version version="1.5.4">This clkass is new in version 1.5.4</version>
    Public Class Wrapper(Of T)
        Private ReadOnly _object As T
        ''' <summary>gets the object being wrapped</summary>
        Public ReadOnly Property [Object] As T
            Get
                Return _object
            End Get
        End Property
        ''' <summary>CTor - creates a new instance of the class <see cref="Wrapper(Of T)"/></summary>
        Public Sub New([object] As T)
            _object = [object]
        End Sub
    End Class
End Namespace