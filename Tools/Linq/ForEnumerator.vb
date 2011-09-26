Imports System.Runtime.InteropServices
Imports Tools.ExtensionsT
#If Config <= Nightly Then
Namespace LinqT

    ''' <summary>Identifies states of for loop iteration</summary>
    ''' <version version="1.5.3">This enumeration is new in version 1.5.3</version>
    <Flags()>
    Public Enum LoopState
        ''' <summary>Process value returned from the loop and continue with next iteration</summary>
        ''' <remarks>Resembles VB <c>Next</c></remarks>
        [Next] = [Return] Or [Continue]
        ''' <summary>Ignore value returned from the loop and continue with next iteration</summary>
        ''' <remarks>Resembles VB <c>Continue</c> / C# <c>continue</c></remarks>
        [Continue] = 1
        ''' <summary>Ignore value returned from the loop and terminate loop execution</summary>
        ''' <remarks>Resembles VB <c>Exit For</c> / C# <c>break</c></remarks>
        [Exit] = 0
        ''' <summary>Process value returned from the loop and terminate loop execution</summary>
        [Return] = 2
    End Enum
#Region "Delegates"
    ''' <summary>Delegate of increment function for for loop</summary>
    ''' <typeparam name="TI">Type of for loop counter (<c>i</c>)</typeparam>
    ''' <param name="i">For loop variable to be incremented</param>
    ''' <version version="1.5.3">This delegate is new in version 1.5.3</version>
    Public Delegate Sub Increment(Of TI)(ByRef i As TI)
    ''' <summary>Delegate for body of for loop</summary>
    ''' <typeparam name="TReturn">Type of values returned from loop</typeparam>
    ''' <typeparam name="TI">Type of loop conter varieble (<c>i</c>)</typeparam>
    ''' <param name="i">Current value of loop counter. Note: Loop body can alter the value. This value is automatically incremented (if increment function was provided) after each loop iteration.</param>
    ''' <param name="yield">Value returned as result of this iteration</param>
    ''' <returns>State value indicating wheather to process <paramref name="yield"/> and how continue after current iteration</returns>
    ''' <remarks>If you don't plan to use continue or breal -like behavior in your loop, you can use <see cref="NonBreakingLoopBody(Of TReturn, TItem)"/> isntead</remarks>
    ''' <version version="1.5.3">This delegate is new in version 1.5.3</version>
    Public Delegate Function LoopBody(Of TReturn, TI)(ByRef i As TI, <out()> ByRef yield As TReturn) As LoopState
    ''' <summary>Delegate for body of for loop which does not use break and continue statements</summary>
    ''' <typeparam name="TReturn">Type of values returned from loop</typeparam>
    ''' <typeparam name="TI">Type of loop conter varieble (<c>i</c>)</typeparam>
    ''' <param name="i">Current value of loop counter. Note: Loop body can alter the value. This value is automatically incremented (if increment function was provided) after each loop iteration.</param>
    ''' <param name="yield">Value returned as result of this iteration</param>
    ''' <remarks>When this delegate is used <see cref="LoopState.[Next]"/> is assomed for all iterations</remarks>
    ''' <version version="1.5.3">This delegate is new in version 1.5.3</version>
    ''' <version version="1.5.4">Type parameter <c>ti</c> renamed to <c>TI</c></version>
    Public Delegate Sub NonBreakingLoopBody(Of TReturn, TI)(ByRef i As ti, <Out()> ByRef yield As TReturn)
    ''' <summary>Delegate for body of for-each loop</summary>
    ''' <typeparam name="TReturn">Type of value returned from loop</typeparam>
    ''' <typeparam name="TItem">Type of for-each item</typeparam>
    ''' <param name="item">Current item of for-each processing</param>
    ''' <param name="yield">Value returned as result of this iteration</param>
    ''' <returns>State value indicating wheather to process <paramref name="yield"/> and how continue after current iteration</returns>
    ''' <remarks>If you don't plan to use continue or breal -like behavior in your loop, you can use <see cref="NonBreakingForEachBody(Of TReturn, TItem)"/> isntead</remarks>
    ''' <version version="1.5.3">This delegate is new in version 1.5.3</version>
    Public Delegate Function ForEachBody(Of TReturn, TItem)(ByVal item As TItem, <Out()> ByRef yield As TReturn) As LoopState
    ''' <summary>Delegate for body of for-each loop which does not use break and continue statements</summary>
    ''' <typeparam name="TReturn">Type of value returned from loop</typeparam>
    ''' <typeparam name="TItem">Type of for-each item</typeparam>
    ''' <param name="item">Current item of for-each processing</param>
    ''' <param name="yield">Value returned as result of this iteration</param>
    ''' <remarks>If you don't plan to use continue or breal -like behavior in your loop, you can use <see cref="NonBreakingForEachBody(Of TReturn, TItem)"/> isntead</remarks>
    ''' <version version="1.5.3">This delegate is new in version 1.5.3</version>
    Public Delegate Sub NonBreakingForEachBody(Of TReturn, TItem)(ByVal item As titem, <out()> ByRef yield As treturn)
#End Region

    ''' <summary>This class allows to turn any C#/C++-style for loop to <see cref="IEnumerable(Of T)"/>/<see cref="IEnumerator(Of T)"/></summary>
    ''' <typeparam name="TReturn">Type of values being returned from the loop</typeparam>
    ''' <typeparam name="TI">Type of loop counter (<c>i</c>)</typeparam>
    ''' <remarks>Derived classes can work with VB-style for loops and for-each loops.</remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class ForLoopCollection(Of TReturn, TI)
        Implements IEnumerable(Of TReturn), IEnumerator(Of TReturn), ICloneable(Of ForLoopCollection(Of TReturn, TI))
        ''' <summary>Function used to initialize for loop</summary>
        Private initialize As Func(Of TI)
        ''' <summary>Function used as condition indicating wheather to continue with next iteration or not</summary>
        Private condition As Func(Of TI, Boolean)
        ''' <summary>Function used to increment current value, ignored if null</summary>
        Private increment As Increment(Of TI)
        ''' <summary>Function representing loop body</summary>
        Private [loop] As LoopBody(Of TReturn, TI)

        ''' <summary>True indicates that iteration has started (<see cref="initialize"/> was executed)</summary>
        Private started As Boolean
        ''' <summary>True indicatees that iteration has finished (<see cref="condition"/> returned false or <see cref="[loop]"/> returned <see cref="LoopState.[Exit]"/> or <see cref="LoopState.Exit"/>).</summary>
        Private finished As Boolean
        ''' <summary>Current value obtained from loop</summary>
        Private currVal As TReturn
        ''' <summary>Current value of iterator</summary>
        Private i As TI
#Region "CTors"
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of TReturn, TI)"/> class. Increment passed by reference, loop can use break and continue</summary>
        ''' <param name="initialize">Function to be used to initialized iteration. It returns initial value of iterator. Called once before the iteration starts (and again if <see cref="Reset"/> was called).</param>
        ''' <param name="condition">Function to test wheather to continue in iteration or not. Called before each iteration including the first one. If it returns true iteration is entered, if it returns false iteration ends and <see cref="MoveNext"/> returns false.</param>
        ''' <param name="increment">Function used to increment value. Called after each loop execution (unless termination was indicated). Value of iteration variable to this function is passed by reference and function should change it to a new value used in next loop iteration. This parameter is ignored if null. In such case incrementation logic should be in <see cref="condition"/> or <see cref="[loop]"/>.</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter. The function returns value indicating how to process value returned through the second parameter and wheather to continue the loop. When this function returns value that is not defined in the <see cref="LoopState"/> enumeration a <see cref="InvalidEnumArgumentException"/> is thrown by <see cref="MoveNext"/>.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="initialize"/>, <paramref name="condition"/> or <paramref name="loop"/> is null.</exception>
        Public Sub New(ByVal initialize As Func(Of TI), ByVal condition As Func(Of TI, Boolean), ByVal increment As Increment(Of TI), ByVal [loop] As LoopBody(Of TReturn, TI))
            If initialize Is Nothing Then Throw New ArgumentNullException("initialize")
            If condition Is Nothing Then Throw New ArgumentNullException("condition")
            If [loop] Is Nothing Then Throw New ArgumentNullException("loop")
            Me.initialize = initialize
            Me.condition = condition
            Me.increment = increment
            Me.loop = [loop]
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of TReturn, TI)"/> class. Increment passed by reference, loop cannot use break and continue</summary>
        ''' <param name="initialize">Function to be used to initialized iteration. It returns initial value of iterator. Called once before the iteration starts (and again if <see cref="Reset"/> was called).</param>
        ''' <param name="condition">Function to test wheather to continue in iteration or not. Called before each iteration including the first one. If it returns true iteration is entered, if it returns false iteration ends and <see cref="MoveNext"/> returns false.</param>
        ''' <param name="increment">Function used to increment value. Called after each loop execution (unless termination was indicated). Value of iteration variable to this function is passed by reference and function should change it to a new value used in next loop iteration. This parameter is ignored if null. In such case incrementation logic should be in <see cref="condition"/> or <see cref="[loop]"/>.</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="initialize"/>, <paramref name="condition"/> or <paramref name="loop"/> is null.</exception>
        Public Sub New(ByVal initialize As Func(Of TI), ByVal condition As Func(Of TI, Boolean), ByVal increment As Increment(Of TI), ByVal [loop] As NonBreakingLoopBody(Of TReturn, TI))
            Me.New(initialize, condition, increment,
                   [loop].ThrowIfNull(Of LoopBody(Of TReturn, TI))(
                       Function(ByRef i, ByRef yield)
                           [loop](i, yield)
                           Return LoopState.Next
                       End Function, "loop")
                  )
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of TReturn, TI)"/> class. Increment passed by value, loop can use break and continue</summary>
        ''' <param name="initialize">Function to be used to initialized iteration. It returns initial value of iterator. Called once before the iteration starts (and again if <see cref="Reset"/> was called).</param>
        ''' <param name="condition">Function to test wheather to continue in iteration or not. Called before each iteration including the first one. If it returns true iteration is entered, if it returns false iteration ends and <see cref="MoveNext"/> returns false.</param>
        ''' <param name="increment">Function used to increment value. Called after each loop execution (unless termination was indicated). Old iteration variable value is passed here and the function should return new value used in next loop iteration. This parameter is ignored if null. In such case incrementation logic should be in <see cref="condition"/> or <see cref="[loop]"/>.</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter. The function returns value indicating how to process value returned through the second parameter and wheather to continue the loop. When this function returns value that is not defined in the <see cref="LoopState"/> enumeration a <see cref="InvalidEnumArgumentException"/> is thrown by <see cref="MoveNext"/>.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="initialize"/>, <paramref name="condition"/> or <paramref name="loop"/> is null.</exception>
        Public Sub New(ByVal initialize As Func(Of TI), ByVal condition As Func(Of TI, Boolean), ByVal increment As Func(Of TI, TI), ByVal [loop] As LoopBody(Of TReturn, TI))
            Me.New(initialize, condition, increment.ThrowIfNull(Of Increment(Of TI))(Sub(ByRef i) i = increment(i), "increment"), [loop])
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of TReturn, TI)"/> class. Increment passed by value, loop cannot use break and continue</summary>
        ''' <param name="initialize">Function to be used to initialized iteration. It returns initial value of iterator. Called once before the iteration starts (and again if <see cref="Reset"/> was called).</param>
        ''' <param name="condition">Function to test wheather to continue in iteration or not. Called before each iteration including the first one. If it returns true iteration is entered, if it returns false iteration ends and <see cref="MoveNext"/> returns false.</param>
        ''' <param name="increment">Function used to increment value. Called after each loop execution (unless termination was indicated). Old iteration variable value is passed here and the function should return new value used in next loop iteration. This parameter is ignored if null. In such case incrementation logic should be in <see cref="condition"/> or <see cref="[loop]"/>.</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="initialize"/>, <paramref name="condition"/> or <paramref name="loop"/> is null.</exception>
        Public Sub New(ByVal initialize As Func(Of TI), ByVal condition As Func(Of TI, Boolean), ByVal increment As Func(Of TI, TI), ByVal [loop] As NonBreakingLoopBody(Of TReturn, TI))
            Me.New(initialize, condition, increment,
                   [loop].ThrowIfNull(Of LoopBody(Of TReturn, TI))(
                       Function(ByRef i, ByRef yield)
                           [loop](i, yield)
                           Return LoopState.Next
                       End Function, "loop")
                  )
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of TReturn, TI)"/> class. Increment passed by reference, loop can neitrher break nor affect loop variable</summary>
        ''' <param name="initialize">Function to be used to initialized iteration. It returns initial value of iterator. Called once before the iteration starts (and again if <see cref="Reset"/> was called).</param>
        ''' <param name="condition">Function to test wheather to continue in iteration or not. Called before each iteration including the first one. If it returns true iteration is entered, if it returns false iteration ends and <see cref="MoveNext"/> returns false.</param>
        ''' <param name="increment">Function used to increment value. Called after each loop execution (unless termination was indicated). Value of iteration variable to this function is passed by reference and function should change it to a new value used in next loop iteration. This parameter is ignored if null. In such case incrementation logic should be in <see cref="condition"/> or <see cref="[loop]"/>.</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="initialize"/>, <paramref name="condition"/> or <paramref name="loop"/> is null.</exception>
        Public Sub New(ByVal initialize As Func(Of TI), ByVal condition As Func(Of TI, Boolean), ByVal increment As Increment(Of TI), ByVal [loop] As Func(Of TI, TReturn))
            Me.New(initialize, condition, increment, [loop].ThrowIfNull(Of NonBreakingLoopBody(Of TReturn, TI))(Sub(ByRef i, ByRef value) value = [loop](i), "loop"))
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of TReturn, TI)"/> class. Increment passed by value, loop can neitrher break nor affect loop variable</summary>
        ''' <param name="initialize">Function to be used to initialized iteration. It returns initial value of iterator. Called once before the iteration starts (and again if <see cref="Reset"/> was called).</param>
        ''' <param name="condition">Function to test wheather to continue in iteration or not. Called before each iteration including the first one. If it returns true iteration is entered, if it returns false iteration ends and <see cref="MoveNext"/> returns false.</param>
        ''' <param name="increment">Function used to increment value. Called after each loop execution (unless termination was indicated). Old iteration variable value is passed here and the function should return new value used in next loop iteration. This parameter is ignored if null. In such case incrementation logic should be in <see cref="condition"/> or <see cref="[loop]"/>.</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="initialize"/>, <paramref name="condition"/> or <paramref name="loop"/> is null.</exception>
        Public Sub New(ByVal initialize As Func(Of TI), ByVal condition As Func(Of TI, Boolean), ByVal increment As Func(Of TI, TI), ByVal [loop] As Func(Of TI, TReturn))
            Me.New(initialize, condition, increment, [loop].ThrowIfNull(Of NonBreakingLoopBody(Of TReturn, TI))(Sub(ByRef i, ByRef value) value = [loop](i), "loop"))
        End Sub

        ''' <summary>Copy CTor - creates a new instance of the <see cref="ForLoopCollection(Of TReturn, TI)"/> which is clone of another instance.</summary>
        ''' <param name="other">Instance to clone</param>
        ''' <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        ''' <exception cref="ObjectDisposedException"><paramref name="other"/> is disposed</exception>
        ''' <remarks>Used by <see cref="Clone"/>.</remarks>
        Protected Sub New(ByVal other As ForLoopCollection(Of TReturn, TI))
            Me.New(other.ThrowIfNull("other").initialize, other.condition, other.increment, other.loop)
        End Sub
#End Region
#Region "IEnumerable"
        ''' <summary>Returns an enumerator that iterates through the collection.</summary>
        ''' <returns>A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <filterpriority>1</filterpriority>
        Public Function GetEnumerator() As System.Collections.Generic.IEnumerator(Of TReturn) Implements System.Collections.Generic.IEnumerable(Of TReturn).GetEnumerator
            If IsDisposed Then Throw New ObjectDisposedException(Me.GetType.Name)
            Return Me.Clone
        End Function

        ''' <summary>Returns an enumerator that iterates through a collection.</summary>
        ''' <returns>An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <filterpriority>2</filterpriority>
        Public Function IEnumerable_GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function
#End Region
#Region "IEnumerator"
        ''' <summary>Gets the element in the collection at the current position of the enumerator.</summary>
        ''' <returns>The element in the collection at the current position of the enumerator.</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        Public ReadOnly Property Current As TReturn Implements System.Collections.Generic.IEnumerator(Of TReturn).Current
            Get
                If IsDisposed Then Throw New ObjectDisposedException(Me.GetType.Name)
                Return currVal
            End Get
        End Property

        ''' <summary>Gets the current element in the collection.</summary>
        ''' <returns>The current element in the collection.</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <filterpriority>2</filterpriority>
        Public ReadOnly Property IEnumerator_Current As Object Implements System.Collections.IEnumerator.Current
            Get
                Return Current
            End Get
        End Property

        ''' <summary>Advances the enumerator to the next element of the collection.</summary>
        ''' <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <exception cref="InvalidEnumArgumentException">The loop function returned value that is not defined in <see cref="LoopState"/> enumeration.</exception>
        ''' <filterpriority>2</filterpriority>
        Public Function MoveNext() As Boolean Implements System.Collections.IEnumerator.MoveNext
            If IsDisposed Then Throw New ObjectDisposedException(Me.GetType.Name)
            If finished Then Return False
            If Not started Then
                i = initialize()
                started = True
            End If
condition:  If Not condition(i) Then
                finished = True
                Return False
            End If
            Dim retVal As TReturn
            Dim state As LoopState
            state = ([loop](i, retVal))
            Select Case state
                Case LoopState.Next
                    If increment IsNot Nothing Then increment(i)
                    currVal = retVal
                    Return True
                Case LoopState.Continue
                    If increment IsNot Nothing Then increment(i)
                    GoTo condition 'Continue next iteration
                Case LoopState.Exit
                    finished = True
                    Return False
                Case LoopState.Return
                    finished = True
                    currVal = retVal
                    Return True
                Case Else
                    Throw New InvalidEnumArgumentException(ResourcesT.Exceptions.InvalidValueReturnedByLoopFunction.f(state))
            End Select
        End Function

        ''' <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <filterpriority>2</filterpriority>
        Public Sub Reset() Implements System.Collections.IEnumerator.Reset
            If IsDisposed Then Throw New ObjectDisposedException(Me.GetType.Name)
            started = False
            finished = False
        End Sub

#Region "IDisposable Support"
        ''' <summary>Gets value indicating if this class was disposed</summary>
        Public ReadOnly Property IsDisposed As Boolean
            Get
                Return disposedValue
            End Get
        End Property
        ''' <summary> To detect redundant calls</summary>
        Private disposedValue As Boolean

        ''' <summary>Implements <see cref="IDisposable.Dispose"/></summary>
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If i IsNot Nothing AndAlso TypeOf i Is IDisposable Then DirectCast(i, IDisposable).Dispose()
                End If
                Me.disposedValue = True
            End If
        End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region
#End Region
#Region "ICloneable"
        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance.</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <filterpriority>2</filterpriority>
        Public Function ICloneable_Clone() As Object Implements System.ICloneable.Clone
            Return Clone()
        End Function

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <remarks><note type="inheritinfo">To override this function, override <see cref="CloneInternal"/> instead.</note></remarks>
        Public Function Clone() As ForLoopCollection(Of TReturn, TI) Implements ICloneable(Of ForLoopCollection(Of TReturn, TI)).Clone
            Return CloneInternal()
        End Function
        ''' <summary>Internally creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <remarks><note type="inheritinfo">Override this function if you want to override <see cref="ICloneable.Clone"/> functionality (the <see cref="Clone"/> function)</note></remarks>
        Protected Overridable Function CloneInternal() As ForLoopCollection(Of TReturn, TI)
            If IsDisposed Then Throw New ObjectDisposedException(Me.GetType.Name)
            Return New ForLoopCollection(Of TReturn, TI)(Me)
        End Function
#End Region
    End Class

    ''' <summary>This class allows to turn any <see cref="Integer"/>-based VB-style for loop to <see cref="IEnumerable(Of T)"/>/<see cref="IEnumerator(Of T)"/></summary>
    ''' <typeparam name="T">Type of values being returned from the loop</typeparam>
    ''' <remarks><example>Shows a C# iterator code and how to simulate it in VB using <see cref="ForLoopCollection(Of T)"/>
    ''' <code lang="C#"><![CDATA[
    ''' public static IEnumerable<int> SplitToInts(string commaSeparated){
    '''     if(commaSeparated == null) throw new ArgumentNullException("commaSeparated");
    '''     var parts = commaSeparated.Split(',');
    '''     for(var i = 0; i < parts.Length; i++){
    '''         if(string.IsNullOrEmpty(parts[i])) continue;
    '''         yield return int.Parse(parts[i]);
    '''     }
    ''' }
    ''' ]]></code><code><![CDATA[
    ''' Public Shared Function SplitToInts(ByVal commaSeparated As String) As IEnumerable(Of Integer)
    '''     If commaSeparated Is Nothing Then Throw New ArgumentNullException("commaSeparated")
    '''     Dim parts = commaSeparated.Split(","c)
    '''     Return New ForLoopCollection(Of Integer)(0, parts.Length - 1,
    '''                                              Function(ByRef i, ByRef yield)
    '''                                                  If parts(i) = "" Then Return LoopState.Continue
    '''                                                  yield = Integer.Parse(parts(i))
    '''                                                  Return LoopState.Next
    '''                                              End Function
    '''                                             )
    ''' End Function
    ''' ]]></code><note>The VB sample behavior is actually better than of C# one:
    ''' The <see cref="ArgumentNullException"/> is potentially thrown by C# at first call to <see cref="System.Collections.IEnumerator.MoveNext"/> while VB throws the error immediatelly on <c>SplitToInts</c> call.</note></example></remarks>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class ForLoopCollection(Of T)
        Inherits ForLoopCollection(Of T, Integer)
        Implements ICloneable(Of ForLoopCollection(Of T))
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of T)"/> class resembling loop with increment 1</summary>
        ''' <param name="from">Start position of the loop</param>
        ''' <param name="to">Maximum value of the loop</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter. The function returns value indicating how to process value returned through the second parameter and wheather to continue the loop. When this function returns value that is not defined in the <see cref="LoopState"/> enumeration a <see cref="InvalidEnumArgumentException"/> is thrown by <see cref="MoveNext"/>.</param>
        ''' <remarks>This constructor creates VB-style loop <c>For i = [from] To [to]</c> or C#-style loop <c>for(int i = from; i &lt;= to; i++)</c></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> is null</exception>
        Public Sub New(ByVal [from] As Integer, ByVal [to] As Integer, ByVal [loop] As LoopBody(Of T, Integer))
            Me.New([from], [to], 1, [loop])
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of T)"/> class resembling loop with custom increment</summary>
        ''' <param name="from">Start position of the loop</param>
        ''' <param name="to">Maximum value of the loop</param>
        ''' <param name="step">Increment size</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter. The function returns value indicating how to process value returned through the second parameter and wheather to continue the loop. When this function returns value that is not defined in the <see cref="LoopState"/> enumeration a <see cref="InvalidEnumArgumentException"/> is thrown by <see cref="MoveNext"/>.</param>
        ''' <remarks>This constructor creates VB-style loop <c>For i = [from] To [to] Step [step]</c> or C#-style loop <c>for(int i = from; i &lt;= to; i += step)</c></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> is null</exception>
        Public Sub New(ByVal [from] As Integer, ByVal [to] As Integer, ByVal [step] As Integer, ByVal [loop] As LoopBody(Of T, Integer))
            MyBase.New(Function() [from], Function(i) i <= [to], Sub(ByRef i) i += [step], [loop])
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of T)"/> class resembling loop with increment 1, loop cannot be prematurely terminated</summary>
        ''' <param name="from">Start position of the loop</param>
        ''' <param name="to">Maximum value of the loop</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter.</param>
        ''' <remarks>This constructor creates VB-style loop <c>For i = [from] To [to]</c> or C#-style loop <c>for(int i = from; i &lt;= to; i++)</c></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> is null</exception>
        Public Sub New(ByVal [from] As Integer, ByVal [to] As Integer, ByVal [loop] As NonBreakingLoopBody(Of T, Integer))
            Me.New([from], [to], 1, [loop])
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of T)"/> class resembling loop with custom increment, loop cannot be prematurely terminated</summary>
        ''' <param name="from">Start position of the loop</param>
        ''' <param name="to">Maximum value of the loop</param>
        ''' <param name="step">Increment size</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter.</param>
        ''' <remarks>This constructor creates VB-style loop <c>For i = [from] To [to] Step [step]</c> or C#-style loop <c>for(int i = from; i &lt;= to; i += step)</c></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> is null</exception>
        Public Sub New(ByVal [from] As Integer, ByVal [to] As Integer, ByVal [step] As Integer, ByVal [loop] As NonBreakingLoopBody(Of T, Integer))
            MyBase.New(Function() [from], Function(i) i <= [to], Sub(ByRef i) i += [step],
                            [loop].ThrowIfNull(Of LoopBody(Of T, Integer))(
                                Function(ByRef i, ByRef yield)
                                    [loop](i, yield)
                                    Return LoopState.Next
                                End Function, "loop")
                      )
        End Sub
        ''' <summary>Copy CTor - creates a new instance of the <see cref="ForLoopCollection(Of T)"/> class which is clone of another given instance</summary>
        ''' <param name="other">Instance to clone</param>
        ''' <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        ''' <exception cref="ObjectDisposedException"><paramref name="other"/> is disposed</exception>
        ''' <remarks>Used by <see cref="Clone"/></remarks>
        Protected Sub New(ByVal other As ForLoopCollection(Of T))
            MyBase.New(other)
        End Sub

        ''' <summary>Internally creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <remarks><note type="inheritinfo">Override this function if you want to override <see cref="ICloneable.Clone"/> functionality (the <see cref="Clone"/> function)</note></remarks>
        Protected Overrides Function CloneInternal() As ForLoopCollection(Of T, Integer)
            Return New ForLoopCollection(Of T)(Me)
        End Function

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <remarks><note type="inheritinfo">To override this function, override <see cref="CloneInternal"/> instead.</note></remarks>
        Public Shadows Function Clone() As ForLoopCollection(Of T) Implements ICloneable(Of ForLoopCollection(Of T)).Clone
            Return CloneInternal()
        End Function
    End Class

    ''' <summary>This class allows to turn any <see cref="Long"/>-based VB-style for loop to <see cref="IEnumerable(Of T)"/>/<see cref="IEnumerator(Of T)"/></summary>
    ''' <typeparam name="T">Type of values being returned from the loop</typeparam>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class ForLoopCollectionLong(Of T)
        Inherits ForLoopCollection(Of T, Long)
        Implements ICloneable(Of ForLoopCollectionLong(Of T))
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of T)"/> class resembling loop with increment 1</summary>
        ''' <param name="from">Start position of the loop</param>
        ''' <param name="to">Maximum value of the loop</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter. The function returns value indicating how to process value returned through the second parameter and wheather to continue the loop. When this function returns value that is not defined in the <see cref="LoopState"/> enumeration a <see cref="InvalidEnumArgumentException"/> is thrown by <see cref="MoveNext"/>.</param>
        ''' <remarks>This constructor creates VB-style loop <c>For i = [from] To [to]</c> or C#-style loop <c>for(int i = from; i &lt;= to; i++)</c></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> is null</exception>
        Public Sub New(ByVal [from] As Long, ByVal [to] As Long, ByVal [loop] As LoopBody(Of T, Long))
            Me.New([from], [to], 1, [loop])
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of T)"/> class resembling loop with custom increment</summary>
        ''' <param name="from">Start position of the loop</param>
        ''' <param name="to">Maximum value of the loop</param>
        ''' <param name="step">Increment size</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter. The function returns value indicating how to process value returned through the second parameter and wheather to continue the loop. When this function returns value that is not defined in the <see cref="LoopState"/> enumeration a <see cref="InvalidEnumArgumentException"/> is thrown by <see cref="MoveNext"/>.</param>
        ''' <remarks>This constructor creates VB-style loop <c>For i = [from] To [to] Step [step]</c> or C#-style loop <c>for(int i = from; i &lt;= to; i += step)</c></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> is null</exception>
        Public Sub New(ByVal [from] As Long, ByVal [to] As Long, ByVal [step] As Long, ByVal [loop] As LoopBody(Of T, Long))
            MyBase.New(Function() [from], Function(i) i <= [to], Sub(ByRef i) i += [step], [loop])
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of T)"/> class resembling loop with increment 1, loop cannot be prematurely terminated</summary>
        ''' <param name="from">Start position of the loop</param>
        ''' <param name="to">Maximum value of the loop</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter.</param>
        ''' <remarks>This constructor creates VB-style loop <c>For i = [from] To [to]</c> or C#-style loop <c>for(int i = from; i &lt;= to; i++)</c></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> is null</exception>
        Public Sub New(ByVal [from] As Integer, ByVal [to] As Integer, ByVal [loop] As NonBreakingLoopBody(Of T, Long))
            Me.New([from], [to], 1, [loop])
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ForLoopCollection(Of T)"/> class resembling loop with custom increment, loop cannot be prematurely terminated</summary>
        ''' <param name="from">Start position of the loop</param>
        ''' <param name="to">Maximum value of the loop</param>
        ''' <param name="step">Increment size</param>
        ''' <param name="loop">Function called as loop body. It receives current value of iterator and can change it. Loop return value should be returned through the second parameter.</param>
        ''' <remarks>This constructor creates VB-style loop <c>For i = [from] To [to] Step [step]</c> or C#-style loop <c>for(int i = from; i &lt;= to; i += step)</c></remarks>
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> is null</exception>
        Public Sub New(ByVal [from] As Integer, ByVal [to] As Integer, ByVal [step] As Integer, ByVal [loop] As NonBreakingLoopBody(Of T, Long))
            MyBase.New(Function() [from], Function(i) i <= [to], Sub(ByRef i) i += [step],
                       [loop].ThrowIfNull(Of LoopBody(Of T, Long))(
                           Function(ByRef i, ByRef yield)
                               [loop](i, yield)
                               Return LoopState.Next
                           End Function, "loop")
                      )
        End Sub
        ''' <summary>Copy CTor - creates a new instance of the <see cref="ForLoopCollection(Of T)"/> class which is clone of another given instance</summary>
        ''' <param name="other">Instance to clone</param>
        ''' <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        ''' <remarks>Used by <see cref="Clone"/></remarks>
        ''' <exception cref="ObjectDisposedException"><paramref name="other"/> is disposed</exception>
        Protected Sub New(ByVal other As ForLoopCollectionLong(Of T))
            MyBase.New(other)
        End Sub
        ''' <summary>Internally creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <remarks><note type="inheritinfo">Override this function if you want to override <see cref="ICloneable.Clone"/> functionality (the <see cref="Clone"/> function)</note></remarks>
        Protected Overrides Function CloneInternal() As ForLoopCollection(Of T, Long)
            Return New ForLoopCollectionLong(Of T)(Me)
        End Function

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <remarks><note type="inheritinfo">To override this function, override <see cref="CloneInternal"/> instead.</note></remarks>
        Public Shadows Function Clone() As ForLoopCollectionLong(Of T) Implements ICloneable(Of ForLoopCollectionLong(Of T)).Clone
            Return CloneInternal()
        End Function
    End Class

    ''' <summary>This class allows to turn any for-each loop to <see cref="IEnumerable(Of T)"/>/<see cref="IEnumerator(Of T)"/></summary>
    ''' <version version="1.5.3">This class is new in version 1.5.3</version>
    Public Class ForEachCollection(Of TReturn, TItem)
        Inherits ForLoopCollection(Of TReturn, IEnumerator(Of TItem))
        Implements ICloneable(Of ForEachCollection(Of TReturn, TItem))
        ''' <summary>CTor - creates a new instance of the <see cref="ForEachBody(Of TReturn, TItem)"/> class from <see cref="IEnumerator(Of TItem)"/></summary>
        ''' <param name="enumerator">Enumerator to be wrapped by this class</param>
        ''' <param name="loop">Function called as loop body. It receives current item form enumerator. Loop return value should be returned through the second parameter. The function returns value indicating how to process value returned through the second parameter and wheather to continue the loop. When this function returns value that is not defined in the <see cref="LoopState"/> enumeration a <see cref="InvalidEnumArgumentException"/> is thrown by <see cref="MoveNext"/>.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> or <paramref name="enumerator"/> is null</exception>
        ''' <remarks>Whenever possible pass <see cref="IEnumerable(Of T)"/> rather than <see cref="IEnumerator(Of T)"/> to <see cref="ForEachCollection(Of TReturn, TItem)"/> class constructor. Passing <see cref="IEnumerator(Of T)"/> here cripples <see cref="ICloneable(Of T)"/> and <see cref="IEnumerable(Of T)"/> functionality of the class. It means that all oljects obtained from <see cref="Clone"/> or <see cref="GetEnumerator"/> will always point to the same position (same <see cref="Current"/> value) inside the collection which may have unwanted side effects.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Sub New(ByVal enumerator As IEnumerator(Of TItem), ByVal [loop] As ForEachBody(Of TReturn, TItem))
            MyBase.New(enumerator.ThrowIfNull(Function() enumerator, "enumerator"),
                       Function(theE) theE.movenext,
                       DirectCast(Nothing, Increment(Of IEnumerator(Of TItem))),
                       [loop].ThrowIfNull(Of LoopBody(Of TReturn, IEnumerator(Of TItem)))(
                            Function(ByRef theE, ByRef yield)
                                Return [loop](theE.Current, yield)
                            End Function, "loop")
                      )
        End Sub

        ''' <summary>CTor - creates a new instance of the <see cref="ForEachBody(Of TReturn, TItem)"/> class from <see cref="IEnumerable(Of TItem)"/></summary>
        ''' <param name="collection">Colletion to be wrapped by this class</param>
        ''' <param name="loop">Function called as loop body. It receives current item form enumerator. Loop return value should be returned through the second parameter. The function returns value indicating how to process value returned through the second parameter and wheather to continue the loop. When this function returns value that is not defined in the <see cref="LoopState"/> enumeration a <see cref="InvalidEnumArgumentException"/> is thrown by <see cref="MoveNext"/>.</param>     
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> or <paramref name="collection"/> is null</exception>
        Public Sub New(ByVal collection As IEnumerable(Of TItem), ByVal [loop] As ForEachBody(Of TReturn, TItem))
            MyBase.New(collection.ThrowIfNull(Function() collection.GetEnumerator, "collection"),
                       Function(enumerator) enumerator.movenext,
                       DirectCast(Nothing, Increment(Of IEnumerator(Of TItem))),
                       [loop].ThrowIfNull(Of LoopBody(Of TReturn, IEnumerator(Of TItem)))(
                           Function(ByRef theE, ByRef yield)
                               Return [loop](theE.Current, yield)
                           End Function, "loop")
                      )
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ForEachBody(Of TReturn, TItem)"/> class from <see cref="IEnumerator(Of TItem)"/>, loop iteration cannot be prematurely interrupted</summary>
        ''' <param name="enumerator">Enumerator to be wrapped by this class</param>
        ''' <param name="loop">Function called as loop body. It receives current item form enumerator. Loop return value should be returned through the second parameter.</param>
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> or <paramref name="enumerator"/> is null</exception>
        ''' <remarks>Whenever possible pass <see cref="IEnumerable(Of T)"/> rather than <see cref="IEnumerator(Of T)"/> to <see cref="ForEachCollection(Of TReturn, TItem)"/> class constructor. Passing <see cref="IEnumerator(Of T)"/> here cripples <see cref="ICloneable(Of T)"/> and <see cref="IEnumerable(Of T)"/> functionality of the class. It means that all oljects obtained from <see cref="Clone"/> or <see cref="GetEnumerator"/> will always point to the same position (same <see cref="Current"/> value) inside the collection which may have unwanted side effects.</remarks>
        <EditorBrowsable(EditorBrowsableState.Advanced)>
        Public Sub New(ByVal enumerator As IEnumerator(Of TItem), ByVal [loop] As NonBreakingForEachBody(Of TReturn, TItem))
            Me.New(enumerator,
                   [loop].ThrowIfNull(Of ForEachBody(Of TReturn, TItem))(
                       Function(i, ByRef yield)
                           [loop](i, yield)
                           Return LoopState.Next
                       End Function, "loop")
                  )
        End Sub
        ''' <summary>CTor - creates a new instance of the <see cref="ForEachBody(Of TReturn, TItem)"/> class from <see cref="IEnumerable(Of TItem)"/>, loop iteration cannot be prematurely interrupted</summary>
        ''' <param name="collection">Colletion to be wrapped by this class</param>
        ''' <param name="loop">Function called as loop body. It receives current item form enumerator. Loop return value should be returned through the second parameter. The function returns value indicating how to process value returned through the second parameter and wheather to continue the loop. When this function returns value that is not defined in the <see cref="LoopState"/> enumeration a <see cref="InvalidEnumArgumentException"/> is thrown by <see cref="MoveNext"/>.</param>     
        ''' <exception cref="ArgumentNullException"><paramref name="loop"/> or <paramref name="collection"/> is null</exception>
        Public Sub New(ByVal collection As IEnumerable(Of TItem), ByVal [loop] As NonBreakingForEachBody(Of TReturn, TItem))
            Me.new(collection,
                   [loop].ThrowIfNull(Of ForEachBody(Of TReturn, TItem))(
                       Function(i, ByRef yield)
                           [loop](i, yield)
                           Return LoopState.Next
                       End Function, "loop")
                  )
        End Sub

        ''' <summary>Copy CTor - creates a new instance of the <see cref="ForEachCollection(Of TReturn, TItem)"/> class which is clone of another given instance</summary>
        ''' <param name="other">Instance to clone</param>
        ''' <exception cref="ArgumentNullException"><paramref name="other"/> is null</exception>
        ''' <remarks>Used by <see cref="Clone"/></remarks>
        ''' <exception cref="ObjectDisposedException"><paramref name="other"/> is disposed</exception>
        Protected Sub New(ByVal other As ForEachCollection(Of TReturn, TItem))
            MyBase.New(other)
        End Sub

        ''' <summary>Internally creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <remarks><note type="inheritinfo">Override this function if you want to override <see cref="ICloneable.Clone"/> functionality (the <see cref="Clone"/> function)</note></remarks>
        Protected Overrides Function CloneInternal() As ForLoopCollection(Of TReturn, IEnumerator(Of TItem))
            Return New ForEachCollection(Of TReturn, TItem)(Me)
        End Function

        ''' <summary>Creates a new object that is a copy of the current instance.</summary>
        ''' <returns>A new object that is a copy of this instance</returns>
        ''' <exception cref="ObjectDisposedException">This instance has already been disposed</exception>
        ''' <remarks><note type="inheritinfo">To override this function, override <see cref="CloneInternal"/> instead.</note></remarks>
        Public Shadows Function Clone() As ForEachCollection(Of TReturn, TItem) Implements ICloneable(Of ForEachCollection(Of TReturn, TItem)).Clone
            Return CloneInternal()
        End Function
    End Class
End Namespace
#End If