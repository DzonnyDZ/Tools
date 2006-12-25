Namespace Resources
    ''' <summary>Provides access to various resources that should be publicly available for users of ÐTools</summary>
    ''' <remarks>
    ''' Only calls to <see cref="My.Resources"/> are allowed here;
    ''' only properties are allowed here;
    ''' because properties shouldn't be decorated with AuthorAttribute and VersionAttribute, it is not necessary to use these attribute here
    ''' </remarks>
    <DoNotApplyAuthorAndVersionAttributes()> _
    Public Module Resources
#If Config <= Release Then
        ''' <summary>Icon reprecenting ÐTools project</summary>
        ''' <remarks>The 'Ð' letter</remarks>
        Public ReadOnly Property ToolsIcon() As System.Drawing.Icon
            Get
                Return My.Resources.ToolsIcon
            End Get
        End Property
#End If
    End Module
End Namespace