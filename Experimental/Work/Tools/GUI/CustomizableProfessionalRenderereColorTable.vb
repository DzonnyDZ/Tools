Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms

Namespace GUI
    Public Class CustomizableProfessionalRenderereColorTable
        Inherits ProfessionalColorTable



        ''' <summary>Contains value of the <see cref="ButtonCheckedGradientBegin"/> and <see cref="ButtonCheckedGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonCheckedGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used when the button is checked.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used when the button is checked.</returns>
        ''' <seealso cref="ButtonCheckedGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonCheckedGradientBegin() As Color
            Get
                If _ButtonCheckedGradientBegin.HasValue Then Return _ButtonCheckedGradientBegin Else Return MyBase.ButtonCheckedGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used when the button is checked.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used when the button is checked.</returns>
        ''' <value>If this has not value <see cref="ButtonCheckedGradientBegin"/> returns default value</value>
        ''' <seealso cref="ButtonCheckedGradientBegin"/>
        <Description("the starting color of the gradient used when the button is checked.")> _
        Public Property ButtonCheckedGradientBeginColor() As Nullable(Of Color)
            Get
                Return _ButtonCheckedGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonCheckedGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonCheckedGradientEnd"/> and <see cref="ButtonCheckedGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonCheckedGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used when the button is checked.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used when the button is checked.</returns>
        ''' <seealso cref="ButtonCheckedGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonCheckedGradientEnd() As Color
            Get
                If _ButtonCheckedGradientEnd.HasValue Then Return _ButtonCheckedGradientEnd Else Return MyBase.ButtonCheckedGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used when the button is checked.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used when the button is checked.</returns>
        ''' <value>If this has not value <see cref="ButtonCheckedGradientEnd"/> returns default value</value>
        ''' <seealso cref="ButtonCheckedGradientEnd"/>
        <Description("the end color of the gradient used when the button is checked.")> _
        Public Property ButtonCheckedGradientEndColor() As Nullable(Of Color)
            Get
                Return _ButtonCheckedGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonCheckedGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonCheckedGradientMiddle"/> and <see cref="ButtonCheckedGradientMiddleColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonCheckedGradientMiddle As Nullable(Of Color)
        ''' <summary>Gets the middle color of the gradient used when the button is checked.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used when the button is checked.</returns>
        ''' <seealso cref="ButtonCheckedGradientMiddleColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonCheckedGradientMiddle() As Color
            Get
                If _ButtonCheckedGradientMiddle.HasValue Then Return _ButtonCheckedGradientMiddle Else Return MyBase.ButtonCheckedGradientMiddle
            End Get
        End Property
        ''' <summary>Gets or sets the middle color of the gradient used when the button is checked.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used when the button is checked.</returns>
        ''' <value>If this has not value <see cref="ButtonCheckedGradientMiddle"/> returns default value</value>
        ''' <seealso cref="ButtonCheckedGradientMiddle"/>
        <Description("the middle color of the gradient used when the button is checked.")> _
        Public Property ButtonCheckedGradientMiddleColor() As Nullable(Of Color)
            Get
                Return _ButtonCheckedGradientMiddle
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonCheckedGradientMiddle = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonCheckedHighlight"/> and <see cref="ButtonCheckedHighlightColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonCheckedHighlight As Nullable(Of Color)
        ''' <summary>Gets the solid color used when the button is checked.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color used when the button is checked.</returns>
        ''' <seealso cref="ButtonCheckedHighlightColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonCheckedHighlight() As Color
            Get
                If _ButtonCheckedHighlight.HasValue Then Return _ButtonCheckedHighlight Else Return MyBase.ButtonCheckedHighlight
            End Get
        End Property
        ''' <summary>Gets or sets the solid color used when the button is checked.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color used when the button is checked.</returns>
        ''' <value>If this has not value <see cref="ButtonCheckedHighlight"/> returns default value</value>
        ''' <seealso cref="ButtonCheckedHighlight"/>
        <Description("the solid color used when the button is checked.")> _
        Public Property ButtonCheckedHighlightColor() As Nullable(Of Color)
            Get
                Return _ButtonCheckedHighlight
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonCheckedHighlight = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonCheckedHighlightBorder"/> and <see cref="ButtonCheckedHighlightBorderColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonCheckedHighlightBorder As Nullable(Of Color)
        ''' <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonCheckedHighlight"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonCheckedHighlight"></see>.</returns>
        ''' <seealso cref="ButtonCheckedHighlightBorderColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonCheckedHighlightBorder() As Color
            Get
                If _ButtonCheckedHighlightBorder.HasValue Then Return _ButtonCheckedHighlightBorder Else Return MyBase.ButtonCheckedHighlightBorder
            End Get
        End Property
        ''' <summary>Gets or sets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonCheckedHighlight"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonCheckedHighlight"></see>.</returns>
        ''' <value>If this has not value <see cref="ButtonCheckedHighlightBorder"/> returns default value</value>
        ''' <seealso cref="ButtonCheckedHighlightBorder"/>
        <Description("the border color to use with ButtonCheckedHighlight.")> _
        Public Property ButtonCheckedHighlightBorderColor() As Nullable(Of Color)
            Get
                Return _ButtonCheckedHighlightBorder
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonCheckedHighlightBorder = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonPressedBorder"/> and <see cref="ButtonPressedBorderColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonPressedBorder As Nullable(Of Color)
        ''' <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientBegin"></see>, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientMiddle"></see>, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientEnd"></see> colors.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientBegin"></see>, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientMiddle"></see>, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientEnd"></see> colors.</returns>
        ''' <seealso cref="ButtonPressedBorderColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonPressedBorder() As Color
            Get
                If _ButtonPressedBorder.HasValue Then Return _ButtonPressedBorder Else Return MyBase.ButtonPressedBorder
            End Get
        End Property
        ''' <summary>Gets or sets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientBegin"></see>, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientMiddle"></see>, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientEnd"></see> colors.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientBegin"></see>, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientMiddle"></see>, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedGradientEnd"></see> colors.</returns>
        ''' <value>If this has not value <see cref="ButtonPressedBorder"/> returns default value</value>
        ''' <seealso cref="ButtonPressedBorder"/>
        <Description("the border color to use with the ButtonPressedGradientEnd colors.")> _
        Public Property ButtonPressedBorderColor() As Nullable(Of Color)
            Get
                Return _ButtonPressedBorder
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonPressedBorder = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonPressedGradientBegin"/> and <see cref="ButtonPressedGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonPressedGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used when the button is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used when the button is pressed.</returns>
        ''' <seealso cref="ButtonPressedGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonPressedGradientBegin() As Color
            Get
                If _ButtonPressedGradientBegin.HasValue Then Return _ButtonPressedGradientBegin Else Return MyBase.ButtonPressedGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used when the button is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used when the button is pressed.</returns>
        ''' <value>If this has not value <see cref="ButtonPressedGradientBegin"/> returns default value</value>
        ''' <seealso cref="ButtonPressedGradientBegin"/>
        <Description("the starting color of the gradient used when the button is pressed.")> _
        Public Property ButtonPressedGradientBeginColor() As Nullable(Of Color)
            Get
                Return _ButtonPressedGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonPressedGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonPressedGradientEnd"/> and <see cref="ButtonPressedGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonPressedGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used when the button is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used when the button is pressed.</returns>
        ''' <seealso cref="ButtonPressedGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonPressedGradientEnd() As Color
            Get
                If _ButtonPressedGradientEnd.HasValue Then Return _ButtonPressedGradientEnd Else Return MyBase.ButtonPressedGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used when the button is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used when the button is pressed.</returns>
        ''' <value>If this has not value <see cref="ButtonPressedGradientEnd"/> returns default value</value>
        ''' <seealso cref="ButtonPressedGradientEnd"/>
        <Description("the end color of the gradient used when the button is pressed.")> _
        Public Property ButtonPressedGradientEndColor() As Nullable(Of Color)
            Get
                Return _ButtonPressedGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonPressedGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonPressedGradientMiddle"/> and <see cref="ButtonPressedGradientMiddleColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonPressedGradientMiddle As Nullable(Of Color)
        ''' <summary>Gets the middle color of the gradient used when the button is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used when the button is pressed.</returns>
        ''' <seealso cref="ButtonPressedGradientMiddleColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonPressedGradientMiddle() As Color
            Get
                If _ButtonPressedGradientMiddle.HasValue Then Return _ButtonPressedGradientMiddle Else Return MyBase.ButtonPressedGradientMiddle
            End Get
        End Property
        ''' <summary>Gets or sets the middle color of the gradient used when the button is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used when the button is pressed.</returns>
        ''' <value>If this has not value <see cref="ButtonPressedGradientMiddle"/> returns default value</value>
        ''' <seealso cref="ButtonPressedGradientMiddle"/>
        <Description("the middle color of the gradient used when the button is pressed.")> _
        Public Property ButtonPressedGradientMiddleColor() As Nullable(Of Color)
            Get
                Return _ButtonPressedGradientMiddle
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonPressedGradientMiddle = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonPressedHighlight"/> and <see cref="ButtonPressedHighlightColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonPressedHighlight As Nullable(Of Color)
        ''' <summary>Gets the solid color used when the button is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color used when the button is pressed.</returns>
        ''' <seealso cref="ButtonPressedHighlightColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonPressedHighlight() As Color
            Get
                If _ButtonPressedHighlight.HasValue Then Return _ButtonPressedHighlight Else Return MyBase.ButtonPressedHighlight
            End Get
        End Property
        ''' <summary>Gets or sets the solid color used when the button is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color used when the button is pressed.</returns>
        ''' <value>If this has not value <see cref="ButtonPressedHighlight"/> returns default value</value>
        ''' <seealso cref="ButtonPressedHighlight"/>
        <Description("the solid color used when the button is pressed.")> _
        Public Property ButtonPressedHighlightColor() As Nullable(Of Color)
            Get
                Return _ButtonPressedHighlight
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonPressedHighlight = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonPressedHighlightBorder"/> and <see cref="ButtonPressedHighlightBorderColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonPressedHighlightBorder As Nullable(Of Color)
        ''' <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedHighlight"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedHighlight"></see>.</returns>
        ''' <seealso cref="ButtonPressedHighlightBorderColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonPressedHighlightBorder() As Color
            Get
                If _ButtonPressedHighlightBorder.HasValue Then Return _ButtonPressedHighlightBorder Else Return MyBase.ButtonPressedHighlightBorder
            End Get
        End Property
        ''' <summary>Gets or sets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedHighlight"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonPressedHighlight"></see>.</returns>
        ''' <value>If this has not value <see cref="ButtonPressedHighlightBorder"/> returns default value</value>
        ''' <seealso cref="ButtonPressedHighlightBorder"/>
        <Description("the border color to use with ButtonPressedHighlight.")> _
        Public Property ButtonPressedHighlightBorderColor() As Nullable(Of Color)
            Get
                Return _ButtonPressedHighlightBorder
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonPressedHighlightBorder = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonSelectedBorder"/> and <see cref="ButtonSelectedBorderColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonSelectedBorder As Nullable(Of Color)
        ''' <summary>Gets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientBegin"></see>, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientMiddle"></see>, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientEnd"></see> colors.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientBegin"></see>, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientMiddle"></see>, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientEnd"></see> colors.</returns>
        ''' <seealso cref="ButtonSelectedBorderColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonSelectedBorder() As Color
            Get
                If _ButtonSelectedBorder.HasValue Then Return _ButtonSelectedBorder Else Return MyBase.ButtonSelectedBorder
            End Get
        End Property
        ''' <summary>Gets or sets the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientBegin"></see>, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientMiddle"></see>, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientEnd"></see> colors.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with the <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientBegin"></see>, <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientMiddle"></see>, and <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedGradientEnd"></see> colors.</returns>
        ''' <value>If this has not value <see cref="ButtonSelectedBorder"/> returns default value</value>
        ''' <seealso cref="ButtonSelectedBorder"/>
        <Description("the border color to use with the ButtonSelectedGradientEnd colors.")> _
        Public Property ButtonSelectedBorderColor() As Nullable(Of Color)
            Get
                Return _ButtonSelectedBorder
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonSelectedBorder = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonSelectedGradientBegin"/> and <see cref="ButtonSelectedGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonSelectedGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used when the button is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used when the button is selected.</returns>
        ''' <seealso cref="ButtonSelectedGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonSelectedGradientBegin() As Color
            Get
                If _ButtonSelectedGradientBegin.HasValue Then Return _ButtonSelectedGradientBegin Else Return MyBase.ButtonSelectedGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used when the button is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used when the button is selected.</returns>
        ''' <value>If this has not value <see cref="ButtonSelectedGradientBegin"/> returns default value</value>
        ''' <seealso cref="ButtonSelectedGradientBegin"/>
        <Description("the starting color of the gradient used when the button is selected.")> _
        Public Property ButtonSelectedGradientBeginColor() As Nullable(Of Color)
            Get
                Return _ButtonSelectedGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonSelectedGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonSelectedGradientEnd"/> and <see cref="ButtonSelectedGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonSelectedGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used when the button is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used when the button is selected.</returns>
        ''' <seealso cref="ButtonSelectedGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonSelectedGradientEnd() As Color
            Get
                If _ButtonSelectedGradientEnd.HasValue Then Return _ButtonSelectedGradientEnd Else Return MyBase.ButtonSelectedGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used when the button is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used when the button is selected.</returns>
        ''' <value>If this has not value <see cref="ButtonSelectedGradientEnd"/> returns default value</value>
        ''' <seealso cref="ButtonSelectedGradientEnd"/>
        <Description("the end color of the gradient used when the button is selected.")> _
        Public Property ButtonSelectedGradientEndColor() As Nullable(Of Color)
            Get
                Return _ButtonSelectedGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonSelectedGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonSelectedGradientMiddle"/> and <see cref="ButtonSelectedGradientMiddleColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonSelectedGradientMiddle As Nullable(Of Color)
        ''' <summary>Gets the middle color of the gradient used when the button is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used when the button is selected.</returns>
        ''' <seealso cref="ButtonSelectedGradientMiddleColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonSelectedGradientMiddle() As Color
            Get
                If _ButtonSelectedGradientMiddle.HasValue Then Return _ButtonSelectedGradientMiddle Else Return MyBase.ButtonSelectedGradientMiddle
            End Get
        End Property
        ''' <summary>Gets or sets the middle color of the gradient used when the button is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used when the button is selected.</returns>
        ''' <value>If this has not value <see cref="ButtonSelectedGradientMiddle"/> returns default value</value>
        ''' <seealso cref="ButtonSelectedGradientMiddle"/>
        <Description("the middle color of the gradient used when the button is selected.")> _
        Public Property ButtonSelectedGradientMiddleColor() As Nullable(Of Color)
            Get
                Return _ButtonSelectedGradientMiddle
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonSelectedGradientMiddle = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonSelectedHighlight"/> and <see cref="ButtonSelectedHighlightColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonSelectedHighlight As Nullable(Of Color)
        ''' <summary>Gets the solid color used when the button is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color used when the button is selected.</returns>
        ''' <seealso cref="ButtonSelectedHighlightColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonSelectedHighlight() As Color
            Get
                If _ButtonSelectedHighlight.HasValue Then Return _ButtonSelectedHighlight Else Return MyBase.ButtonSelectedHighlight
            End Get
        End Property
        ''' <summary>Gets or sets the solid color used when the button is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color used when the button is selected.</returns>
        ''' <value>If this has not value <see cref="ButtonSelectedHighlight"/> returns default value</value>
        ''' <seealso cref="ButtonSelectedHighlight"/>
        <Description("the solid color used when the button is selected.")> _
        Public Property ButtonSelectedHighlightColor() As Nullable(Of Color)
            Get
                Return _ButtonSelectedHighlight
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonSelectedHighlight = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ButtonSelectedHighlightBorder"/> and <see cref="ButtonSelectedHighlightBorderColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ButtonSelectedHighlightBorder As Nullable(Of Color)
        ''' <summary>Gets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedHighlight"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedHighlight"></see>.</returns>
        ''' <seealso cref="ButtonSelectedHighlightBorderColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ButtonSelectedHighlightBorder() As Color
            Get
                If _ButtonSelectedHighlightBorder.HasValue Then Return _ButtonSelectedHighlightBorder Else Return MyBase.ButtonSelectedHighlightBorder
            End Get
        End Property
        ''' <summary>Gets or sets the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedHighlight"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with <see cref="P:System.Windows.Forms.ProfessionalColorTable.ButtonSelectedHighlight"></see>.</returns>
        ''' <value>If this has not value <see cref="ButtonSelectedHighlightBorder"/> returns default value</value>
        ''' <seealso cref="ButtonSelectedHighlightBorder"/>
        <Description("the border color to use with ButtonSelectedHighlight.")> _
        Public Property ButtonSelectedHighlightBorderColor() As Nullable(Of Color)
            Get
                Return _ButtonSelectedHighlightBorder
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ButtonSelectedHighlightBorder = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="GripDark"/> and <see cref="GripDarkColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _GripDark As Nullable(Of Color)
        ''' <summary>Gets the color to use for shadow effects on the grip (move handle).</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the color to use for shadow effects on the grip (move handle).</returns>
        ''' <seealso cref="GripDarkColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property GripDark() As Color
            Get
                If _GripDark.HasValue Then Return _GripDark Else Return MyBase.GripDark
            End Get
        End Property
        ''' <summary>Gets or sets the color to use for shadow effects on the grip (move handle).</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the color to use for shadow effects on the grip (move handle).</returns>
        ''' <value>If this has not value <see cref="GripDark"/> returns default value</value>
        ''' <seealso cref="GripDark"/>
        <Description("the color to use for shadow effects on the grip (move handle).")> _
        Public Property GripDarkColor() As Nullable(Of Color)
            Get
                Return _GripDark
            End Get
            Set(ByVal value As Nullable(Of Color))
                _GripDark = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="GripLight"/> and <see cref="GripLightColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _GripLight As Nullable(Of Color)
        ''' <summary>Gets the color to use for highlight effects on the grip (move handle).</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the color to use for highlight effects on the grip (move handle).</returns>
        ''' <seealso cref="GripLightColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property GripLight() As Color
            Get
                If _GripLight.HasValue Then Return _GripLight Else Return MyBase.GripLight
            End Get
        End Property
        ''' <summary>Gets or sets the color to use for highlight effects on the grip (move handle).</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the color to use for highlight effects on the grip (move handle).</returns>
        ''' <value>If this has not value <see cref="GripLight"/> returns default value</value>
        ''' <seealso cref="GripLight"/>
        <Description("the color to use for highlight effects on the grip (move handle).")> _
        Public Property GripLightColor() As Nullable(Of Color)
            Get
                Return _GripLight
            End Get
            Set(ByVal value As Nullable(Of Color))
                _GripLight = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="CheckBackground"/> and <see cref="CheckBackgroundColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _CheckBackground As Nullable(Of Color)
        ''' <summary>Gets the solid color to use when the button is checked and gradients are being used.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color to use when the button is checked and gradients are being used.</returns>
        ''' <seealso cref="CheckBackgroundColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property CheckBackground() As Color
            Get
                If _CheckBackground.HasValue Then Return _CheckBackground Else Return MyBase.CheckBackground
            End Get
        End Property
        ''' <summary>Gets or sets the solid color to use when the button is checked and gradients are being used.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color to use when the button is checked and gradients are being used.</returns>
        ''' <value>If this has not value <see cref="CheckBackground"/> returns default value</value>
        ''' <seealso cref="CheckBackground"/>
        <Description("the solid color to use when the button is checked and gradients are being used.")> _
        Public Property CheckBackgroundColor() As Nullable(Of Color)
            Get
                Return _CheckBackground
            End Get
            Set(ByVal value As Nullable(Of Color))
                _CheckBackground = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="CheckPressedBackground"/> and <see cref="CheckPressedBackgroundColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _CheckPressedBackground As Nullable(Of Color)
        ''' <summary>Gets the solid color to use when the button is checked and selected and gradients are being used.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color to use when the button is checked and selected and gradients are being used.</returns>
        ''' <seealso cref="CheckPressedBackgroundColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property CheckPressedBackground() As Color
            Get
                If _CheckPressedBackground.HasValue Then Return _CheckPressedBackground Else Return MyBase.CheckPressedBackground
            End Get
        End Property
        ''' <summary>Gets or sets the solid color to use when the button is checked and selected and gradients are being used.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color to use when the button is checked and selected and gradients are being used.</returns>
        ''' <value>If this has not value <see cref="CheckPressedBackground"/> returns default value</value>
        ''' <seealso cref="CheckPressedBackground"/>
        <Description("the solid color to use when the button is checked and selected and gradients are being used.")> _
        Public Property CheckPressedBackgroundColor() As Nullable(Of Color)
            Get
                Return _CheckPressedBackground
            End Get
            Set(ByVal value As Nullable(Of Color))
                _CheckPressedBackground = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="CheckSelectedBackground"/> and <see cref="CheckSelectedBackgroundColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _CheckSelectedBackground As Nullable(Of Color)
        ''' <summary>Gets the solid color to use when the button is checked and selected and gradients are being used.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color to use when the button is checked and selected and gradients are being used.</returns>
        ''' <seealso cref="CheckSelectedBackgroundColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property CheckSelectedBackground() As Color
            Get
                If _CheckSelectedBackground.HasValue Then Return _CheckSelectedBackground Else Return MyBase.CheckSelectedBackground
            End Get
        End Property
        ''' <summary>Gets or sets the solid color to use when the button is checked and selected and gradients are being used.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color to use when the button is checked and selected and gradients are being used.</returns>
        ''' <value>If this has not value <see cref="CheckSelectedBackground"/> returns default value</value>
        ''' <seealso cref="CheckSelectedBackground"/>
        <Description("the solid color to use when the button is checked and selected and gradients are being used.")> _
        Public Property CheckSelectedBackgroundColor() As Nullable(Of Color)
            Get
                Return _CheckSelectedBackground
            End Get
            Set(ByVal value As Nullable(Of Color))
                _CheckSelectedBackground = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ImageMarginGradientBegin"/> and <see cref="ImageMarginGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ImageMarginGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</returns>
        ''' <seealso cref="ImageMarginGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ImageMarginGradientBegin() As Color
            Get
                If _ImageMarginGradientBegin.HasValue Then Return _ImageMarginGradientBegin Else Return MyBase.ImageMarginGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</returns>
        ''' <value>If this has not value <see cref="ImageMarginGradientBegin"/> returns default value</value>
        ''' <seealso cref="ImageMarginGradientBegin"/>
        <Description("the starting color of the gradient used in the image margin of a ToolStripDropDownMenu.")> _
        Public Property ImageMarginGradientBeginColor() As Nullable(Of Color)
            Get
                Return _ImageMarginGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ImageMarginGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ImageMarginGradientEnd"/> and <see cref="ImageMarginGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ImageMarginGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</returns>
        ''' <seealso cref="ImageMarginGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ImageMarginGradientEnd() As Color
            Get
                If _ImageMarginGradientEnd.HasValue Then Return _ImageMarginGradientEnd Else Return MyBase.ImageMarginGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</returns>
        ''' <value>If this has not value <see cref="ImageMarginGradientEnd"/> returns default value</value>
        ''' <seealso cref="ImageMarginGradientEnd"/>
        <Description("the end color of the gradient used in the image margin of a ToolStripDropDownMenu.")> _
        Public Property ImageMarginGradientEndColor() As Nullable(Of Color)
            Get
                Return _ImageMarginGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ImageMarginGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ImageMarginGradientMiddle"/> and <see cref="ImageMarginGradientMiddleColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ImageMarginGradientMiddle As Nullable(Of Color)
        ''' <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</returns>
        ''' <seealso cref="ImageMarginGradientMiddleColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ImageMarginGradientMiddle() As Color
            Get
                If _ImageMarginGradientMiddle.HasValue Then Return _ImageMarginGradientMiddle Else Return MyBase.ImageMarginGradientMiddle
            End Get
        End Property
        ''' <summary>Gets or sets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see>.</returns>
        ''' <value>If this has not value <see cref="ImageMarginGradientMiddle"/> returns default value</value>
        ''' <seealso cref="ImageMarginGradientMiddle"/>
        <Description("the middle color of the gradient used in the image margin of a ToolStripDropDownMenu.")> _
        Public Property ImageMarginGradientMiddleColor() As Nullable(Of Color)
            Get
                Return _ImageMarginGradientMiddle
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ImageMarginGradientMiddle = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ImageMarginRevealedGradientBegin"/> and <see cref="ImageMarginRevealedGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ImageMarginRevealedGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</returns>
        ''' <seealso cref="ImageMarginRevealedGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ImageMarginRevealedGradientBegin() As Color
            Get
                If _ImageMarginRevealedGradientBegin.HasValue Then Return _ImageMarginRevealedGradientBegin Else Return MyBase.ImageMarginRevealedGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</returns>
        ''' <value>If this has not value <see cref="ImageMarginRevealedGradientBegin"/> returns default value</value>
        ''' <seealso cref="ImageMarginRevealedGradientBegin"/>
        <Description("the starting color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.")> _
        Public Property ImageMarginRevealedGradientBeginColor() As Nullable(Of Color)
            Get
                Return _ImageMarginRevealedGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ImageMarginRevealedGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ImageMarginRevealedGradientEnd"/> and <see cref="ImageMarginRevealedGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ImageMarginRevealedGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</returns>
        ''' <seealso cref="ImageMarginRevealedGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ImageMarginRevealedGradientEnd() As Color
            Get
                If _ImageMarginRevealedGradientEnd.HasValue Then Return _ImageMarginRevealedGradientEnd Else Return MyBase.ImageMarginRevealedGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</returns>
        ''' <value>If this has not value <see cref="ImageMarginRevealedGradientEnd"/> returns default value</value>
        ''' <seealso cref="ImageMarginRevealedGradientEnd"/>
        <Description("the end color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.")> _
        Public Property ImageMarginRevealedGradientEndColor() As Nullable(Of Color)
            Get
                Return _ImageMarginRevealedGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ImageMarginRevealedGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ImageMarginRevealedGradientMiddle"/> and <see cref="ImageMarginRevealedGradientMiddleColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ImageMarginRevealedGradientMiddle As Nullable(Of Color)
        ''' <summary>Gets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</returns>
        ''' <seealso cref="ImageMarginRevealedGradientMiddleColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ImageMarginRevealedGradientMiddle() As Color
            Get
                If _ImageMarginRevealedGradientMiddle.HasValue Then Return _ImageMarginRevealedGradientMiddle Else Return MyBase.ImageMarginRevealedGradientMiddle
            End Get
        End Property
        ''' <summary>Gets or sets the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used in the image margin of a <see cref="T:System.Windows.Forms.ToolStripDropDownMenu"></see> when an item is revealed.</returns>
        ''' <value>If this has not value <see cref="ImageMarginRevealedGradientMiddle"/> returns default value</value>
        ''' <seealso cref="ImageMarginRevealedGradientMiddle"/>
        <Description("the middle color of the gradient used in the image margin of a ToolStripDropDownMenu when an item is revealed.")> _
        Public Property ImageMarginRevealedGradientMiddleColor() As Nullable(Of Color)
            Get
                Return _ImageMarginRevealedGradientMiddle
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ImageMarginRevealedGradientMiddle = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="MenuBorder"/> and <see cref="MenuBorderColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _MenuBorder As Nullable(Of Color)
        ''' <summary>Gets the color that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip"></see>.</returns>
        ''' <seealso cref="MenuBorderColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property MenuBorder() As Color
            Get
                If _MenuBorder.HasValue Then Return _MenuBorder Else Return MyBase.MenuBorder
            End Get
        End Property
        ''' <summary>Gets or sets the color that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use on a <see cref="T:System.Windows.Forms.MenuStrip"></see>.</returns>
        ''' <value>If this has not value <see cref="MenuBorder"/> returns default value</value>
        ''' <seealso cref="MenuBorder"/>
        <Description("the color that is the border color to use on a MenuStrip.")> _
        Public Property MenuBorderColor() As Nullable(Of Color)
            Get
                Return _MenuBorder
            End Get
            Set(ByVal value As Nullable(Of Color))
                _MenuBorder = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="MenuItemBorder"/> and <see cref="MenuItemBorderColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _MenuItemBorder As Nullable(Of Color)
        ''' <summary>Gets the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see>.</returns>
        ''' <seealso cref="MenuItemBorderColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property MenuItemBorder() As Color
            Get
                If _MenuItemBorder.HasValue Then Return _MenuItemBorder Else Return MyBase.MenuItemBorder
            End Get
        End Property
        ''' <summary>Gets or sets the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use with a <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see>.</returns>
        ''' <value>If this has not value <see cref="MenuItemBorder"/> returns default value</value>
        ''' <seealso cref="MenuItemBorder"/>
        <Description("the border color to use with a ToolStripMenuItem.")> _
        Public Property MenuItemBorderColor() As Nullable(Of Color)
            Get
                Return _MenuItemBorder
            End Get
            Set(ByVal value As Nullable(Of Color))
                _MenuItemBorder = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="MenuItemPressedGradientBegin"/> and <see cref="MenuItemPressedGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _MenuItemPressedGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</returns>
        ''' <seealso cref="MenuItemPressedGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property MenuItemPressedGradientBegin() As Color
            Get
                If _MenuItemPressedGradientBegin.HasValue Then Return _MenuItemPressedGradientBegin Else Return MyBase.MenuItemPressedGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</returns>
        ''' <value>If this has not value <see cref="MenuItemPressedGradientBegin"/> returns default value</value>
        ''' <seealso cref="MenuItemPressedGradientBegin"/>
        <Description("the starting color of the gradient used when a top-level ToolStripMenuItem is pressed.")> _
        Public Property MenuItemPressedGradientBeginColor() As Nullable(Of Color)
            Get
                Return _MenuItemPressedGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _MenuItemPressedGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="MenuItemPressedGradientEnd"/> and <see cref="MenuItemPressedGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _MenuItemPressedGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</returns>
        ''' <seealso cref="MenuItemPressedGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property MenuItemPressedGradientEnd() As Color
            Get
                If _MenuItemPressedGradientEnd.HasValue Then Return _MenuItemPressedGradientEnd Else Return MyBase.MenuItemPressedGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</returns>
        ''' <value>If this has not value <see cref="MenuItemPressedGradientEnd"/> returns default value</value>
        ''' <seealso cref="MenuItemPressedGradientEnd"/>
        <Description("the end color of the gradient used when a top-level ToolStripMenuItem is pressed.")> _
        Public Property MenuItemPressedGradientEndColor() As Nullable(Of Color)
            Get
                Return _MenuItemPressedGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _MenuItemPressedGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="MenuItemPressedGradientMiddle"/> and <see cref="MenuItemPressedGradientMiddleColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _MenuItemPressedGradientMiddle As Nullable(Of Color)
        ''' <summary>Gets the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</returns>
        ''' <seealso cref="MenuItemPressedGradientMiddleColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property MenuItemPressedGradientMiddle() As Color
            Get
                If _MenuItemPressedGradientMiddle.HasValue Then Return _MenuItemPressedGradientMiddle Else Return MyBase.MenuItemPressedGradientMiddle
            End Get
        End Property
        ''' <summary>Gets or sets the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used when a top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is pressed.</returns>
        ''' <value>If this has not value <see cref="MenuItemPressedGradientMiddle"/> returns default value</value>
        ''' <seealso cref="MenuItemPressedGradientMiddle"/>
        <Description("the middle color of the gradient used when a top-level ToolStripMenuItem is pressed.")> _
        Public Property MenuItemPressedGradientMiddleColor() As Nullable(Of Color)
            Get
                Return _MenuItemPressedGradientMiddle
            End Get
            Set(ByVal value As Nullable(Of Color))
                _MenuItemPressedGradientMiddle = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="MenuItemSelected"/> and <see cref="MenuItemSelectedColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _MenuItemSelected As Nullable(Of Color)
        ''' <summary>Gets the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</returns>
        ''' <seealso cref="MenuItemSelectedColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property MenuItemSelected() As Color
            Get
                If _MenuItemSelected.HasValue Then Return _MenuItemSelected Else Return MyBase.MenuItemSelected
            End Get
        End Property
        ''' <summary>Gets or sets the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid color to use when a <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> other than the top-level <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</returns>
        ''' <value>If this has not value <see cref="MenuItemSelected"/> returns default value</value>
        ''' <seealso cref="MenuItemSelected"/>
        <Description("the solid color to use when a ToolStripMenuItem is selected.")> _
        Public Property MenuItemSelectedColor() As Nullable(Of Color)
            Get
                Return _MenuItemSelected
            End Get
            Set(ByVal value As Nullable(Of Color))
                _MenuItemSelected = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="MenuItemSelectedGradientBegin"/> and <see cref="MenuItemSelectedGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _MenuItemSelectedGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</returns>
        ''' <seealso cref="MenuItemSelectedGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property MenuItemSelectedGradientBegin() As Color
            Get
                If _MenuItemSelectedGradientBegin.HasValue Then Return _MenuItemSelectedGradientBegin Else Return MyBase.MenuItemSelectedGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</returns>
        ''' <value>If this has not value <see cref="MenuItemSelectedGradientBegin"/> returns default value</value>
        ''' <seealso cref="MenuItemSelectedGradientBegin"/>
        <Description("the starting color of the gradient used when the ToolStripMenuItem is selected.")> _
        Public Property MenuItemSelectedGradientBeginColor() As Nullable(Of Color)
            Get
                Return _MenuItemSelectedGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _MenuItemSelectedGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="MenuItemSelectedGradientEnd"/> and <see cref="MenuItemSelectedGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _MenuItemSelectedGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</returns>
        ''' <seealso cref="MenuItemSelectedGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property MenuItemSelectedGradientEnd() As Color
            Get
                If _MenuItemSelectedGradientEnd.HasValue Then Return _MenuItemSelectedGradientEnd Else Return MyBase.MenuItemSelectedGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used when the <see cref="T:System.Windows.Forms.ToolStripMenuItem"></see> is selected.</returns>
        ''' <value>If this has not value <see cref="MenuItemSelectedGradientEnd"/> returns default value</value>
        ''' <seealso cref="MenuItemSelectedGradientEnd"/>
        <Description("the end color of the gradient used when the ToolStripMenuItem is selected.")> _
        Public Property MenuItemSelectedGradientEndColor() As Nullable(Of Color)
            Get
                Return _MenuItemSelectedGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _MenuItemSelectedGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="MenuStripGradientBegin"/> and <see cref="MenuStripGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _MenuStripGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip"></see>.</returns>
        ''' <seealso cref="MenuStripGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property MenuStripGradientBegin() As Color
            Get
                If _MenuStripGradientBegin.HasValue Then Return _MenuStripGradientBegin Else Return MyBase.MenuStripGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip"></see>.</returns>
        ''' <value>If this has not value <see cref="MenuStripGradientBegin"/> returns default value</value>
        ''' <seealso cref="MenuStripGradientBegin"/>
        <Description("the starting color of the gradient used in the MenuStrip.")> _
        Public Property MenuStripGradientBeginColor() As Nullable(Of Color)
            Get
                Return _MenuStripGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _MenuStripGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="MenuStripGradientEnd"/> and <see cref="MenuStripGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _MenuStripGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip"></see>.</returns>
        ''' <seealso cref="MenuStripGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property MenuStripGradientEnd() As Color
            Get
                If _MenuStripGradientEnd.HasValue Then Return _MenuStripGradientEnd Else Return MyBase.MenuStripGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.MenuStrip"></see>.</returns>
        ''' <value>If this has not value <see cref="MenuStripGradientEnd"/> returns default value</value>
        ''' <seealso cref="MenuStripGradientEnd"/>
        <Description("the end color of the gradient used in the MenuStrip.")> _
        Public Property MenuStripGradientEndColor() As Nullable(Of Color)
            Get
                Return _MenuStripGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _MenuStripGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="OverflowButtonGradientBegin"/> and <see cref="OverflowButtonGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _OverflowButtonGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</returns>
        ''' <seealso cref="OverflowButtonGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property OverflowButtonGradientBegin() As Color
            Get
                If _OverflowButtonGradientBegin.HasValue Then Return _OverflowButtonGradientBegin Else Return MyBase.OverflowButtonGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</returns>
        ''' <value>If this has not value <see cref="OverflowButtonGradientBegin"/> returns default value</value>
        ''' <seealso cref="OverflowButtonGradientBegin"/>
        <Description("the starting color of the gradient used in the ToolStripOverflowButton.")> _
        Public Property OverflowButtonGradientBeginColor() As Nullable(Of Color)
            Get
                Return _OverflowButtonGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _OverflowButtonGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="OverflowButtonGradientEnd"/> and <see cref="OverflowButtonGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _OverflowButtonGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</returns>
        ''' <seealso cref="OverflowButtonGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property OverflowButtonGradientEnd() As Color
            Get
                If _OverflowButtonGradientEnd.HasValue Then Return _OverflowButtonGradientEnd Else Return MyBase.OverflowButtonGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</returns>
        ''' <value>If this has not value <see cref="OverflowButtonGradientEnd"/> returns default value</value>
        ''' <seealso cref="OverflowButtonGradientEnd"/>
        <Description("the end color of the gradient used in the ToolStripOverflowButton.")> _
        Public Property OverflowButtonGradientEndColor() As Nullable(Of Color)
            Get
                Return _OverflowButtonGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _OverflowButtonGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="OverflowButtonGradientMiddle"/> and <see cref="OverflowButtonGradientMiddleColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _OverflowButtonGradientMiddle As Nullable(Of Color)
        ''' <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</returns>
        ''' <seealso cref="OverflowButtonGradientMiddleColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property OverflowButtonGradientMiddle() As Color
            Get
                If _OverflowButtonGradientMiddle.HasValue Then Return _OverflowButtonGradientMiddle Else Return MyBase.OverflowButtonGradientMiddle
            End Get
        End Property
        ''' <summary>Gets or sets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripOverflowButton"></see>.</returns>
        ''' <value>If this has not value <see cref="OverflowButtonGradientMiddle"/> returns default value</value>
        ''' <seealso cref="OverflowButtonGradientMiddle"/>
        <Description("the middle color of the gradient used in the ToolStripOverflowButton.")> _
        Public Property OverflowButtonGradientMiddleColor() As Nullable(Of Color)
            Get
                Return _OverflowButtonGradientMiddle
            End Get
            Set(ByVal value As Nullable(Of Color))
                _OverflowButtonGradientMiddle = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="RaftingContainerGradientBegin"/> and <see cref="RaftingContainerGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _RaftingContainerGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer"></see>.</returns>
        ''' <seealso cref="RaftingContainerGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property RaftingContainerGradientBegin() As Color
            Get
                If _RaftingContainerGradientBegin.HasValue Then Return _RaftingContainerGradientBegin Else Return MyBase.RaftingContainerGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer"></see>.</returns>
        ''' <value>If this has not value <see cref="RaftingContainerGradientBegin"/> returns default value</value>
        ''' <seealso cref="RaftingContainerGradientBegin"/>
        <Description("the starting color of the gradient used in the ToolStripContainer.")> _
        Public Property RaftingContainerGradientBeginColor() As Nullable(Of Color)
            Get
                Return _RaftingContainerGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _RaftingContainerGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="RaftingContainerGradientEnd"/> and <see cref="RaftingContainerGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _RaftingContainerGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer"></see>.</returns>
        ''' <seealso cref="RaftingContainerGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property RaftingContainerGradientEnd() As Color
            Get
                If _RaftingContainerGradientEnd.HasValue Then Return _RaftingContainerGradientEnd Else Return MyBase.RaftingContainerGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContainer"></see>.</returns>
        ''' <value>If this has not value <see cref="RaftingContainerGradientEnd"/> returns default value</value>
        ''' <seealso cref="RaftingContainerGradientEnd"/>
        <Description("the end color of the gradient used in the ToolStripContainer.")> _
        Public Property RaftingContainerGradientEndColor() As Nullable(Of Color)
            Get
                Return _RaftingContainerGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _RaftingContainerGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="SeparatorDark"/> and <see cref="SeparatorDarkColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _SeparatorDark As Nullable(Of Color)
        ''' <summary>Gets the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator"></see>.</returns>
        ''' <seealso cref="SeparatorDarkColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property SeparatorDark() As Color
            Get
                If _SeparatorDark.HasValue Then Return _SeparatorDark Else Return MyBase.SeparatorDark
            End Get
        End Property
        ''' <summary>Gets or sets the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the color to use to for shadow effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator"></see>.</returns>
        ''' <value>If this has not value <see cref="SeparatorDark"/> returns default value</value>
        ''' <seealso cref="SeparatorDark"/>
        <Description("the color to use to for shadow effects on the ToolStripSeparator.")> _
        Public Property SeparatorDarkColor() As Nullable(Of Color)
            Get
                Return _SeparatorDark
            End Get
            Set(ByVal value As Nullable(Of Color))
                _SeparatorDark = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="SeparatorLight"/> and <see cref="SeparatorLightColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _SeparatorLight As Nullable(Of Color)
        ''' <summary>Gets the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator"></see>.</returns>
        ''' <seealso cref="SeparatorLightColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property SeparatorLight() As Color
            Get
                If _SeparatorLight.HasValue Then Return _SeparatorLight Else Return MyBase.SeparatorLight
            End Get
        End Property
        ''' <summary>Gets or sets the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the color to use to for highlight effects on the <see cref="T:System.Windows.Forms.ToolStripSeparator"></see>.</returns>
        ''' <value>If this has not value <see cref="SeparatorLight"/> returns default value</value>
        ''' <seealso cref="SeparatorLight"/>
        <Description("the color to use to for highlight effects on the ToolStripSeparator.")> _
        Public Property SeparatorLightColor() As Nullable(Of Color)
            Get
                Return _SeparatorLight
            End Get
            Set(ByVal value As Nullable(Of Color))
                _SeparatorLight = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="StatusStripGradientBegin"/> and <see cref="StatusStripGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _StatusStripGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip"></see>.</returns>
        ''' <seealso cref="StatusStripGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property StatusStripGradientBegin() As Color
            Get
                If _StatusStripGradientBegin.HasValue Then Return _StatusStripGradientBegin Else Return MyBase.StatusStripGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip"></see>.</returns>
        ''' <value>If this has not value <see cref="StatusStripGradientBegin"/> returns default value</value>
        ''' <seealso cref="StatusStripGradientBegin"/>
        <Description("the starting color of the gradient used on the StatusStrip.")> _
        Public Property StatusStripGradientBeginColor() As Nullable(Of Color)
            Get
                Return _StatusStripGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _StatusStripGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="StatusStripGradientEnd"/> and <see cref="StatusStripGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _StatusStripGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip"></see>.</returns>
        ''' <seealso cref="StatusStripGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property StatusStripGradientEnd() As Color
            Get
                If _StatusStripGradientEnd.HasValue Then Return _StatusStripGradientEnd Else Return MyBase.StatusStripGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used on the <see cref="T:System.Windows.Forms.StatusStrip"></see>.</returns>
        ''' <value>If this has not value <see cref="StatusStripGradientEnd"/> returns default value</value>
        ''' <seealso cref="StatusStripGradientEnd"/>
        <Description("the end color of the gradient used on the StatusStrip.")> _
        Public Property StatusStripGradientEndColor() As Nullable(Of Color)
            Get
                Return _StatusStripGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _StatusStripGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ToolStripBorder"/> and <see cref="ToolStripBorderColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ToolStripBorder As Nullable(Of Color)
        ''' <summary>Gets the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip"></see>.</returns>
        ''' <seealso cref="ToolStripBorderColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ToolStripBorder() As Color
            Get
                If _ToolStripBorder.HasValue Then Return _ToolStripBorder Else Return MyBase.ToolStripBorder
            End Get
        End Property
        ''' <summary>Gets or sets the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the border color to use on the bottom edge of the <see cref="T:System.Windows.Forms.ToolStrip"></see>.</returns>
        ''' <value>If this has not value <see cref="ToolStripBorder"/> returns default value</value>
        ''' <seealso cref="ToolStripBorder"/>
        <Description("the border color to use on the bottom edge of the ToolStrip.")> _
        Public Property ToolStripBorderColor() As Nullable(Of Color)
            Get
                Return _ToolStripBorder
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ToolStripBorder = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ToolStripContentPanelGradientBegin"/> and <see cref="ToolStripContentPanelGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ToolStripContentPanelGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel"></see>.</returns>
        ''' <seealso cref="ToolStripContentPanelGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ToolStripContentPanelGradientBegin() As Color
            Get
                If _ToolStripContentPanelGradientBegin.HasValue Then Return _ToolStripContentPanelGradientBegin Else Return MyBase.ToolStripContentPanelGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel"></see>.</returns>
        ''' <value>If this has not value <see cref="ToolStripContentPanelGradientBegin"/> returns default value</value>
        ''' <seealso cref="ToolStripContentPanelGradientBegin"/>
        <Description("the starting color of the gradient used in the ToolStripContentPanel.")> _
        Public Property ToolStripContentPanelGradientBeginColor() As Nullable(Of Color)
            Get
                Return _ToolStripContentPanelGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ToolStripContentPanelGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ToolStripContentPanelGradientEnd"/> and <see cref="ToolStripContentPanelGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ToolStripContentPanelGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel"></see>.</returns>
        ''' <seealso cref="ToolStripContentPanelGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ToolStripContentPanelGradientEnd() As Color
            Get
                If _ToolStripContentPanelGradientEnd.HasValue Then Return _ToolStripContentPanelGradientEnd Else Return MyBase.ToolStripContentPanelGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripContentPanel"></see>.</returns>
        ''' <value>If this has not value <see cref="ToolStripContentPanelGradientEnd"/> returns default value</value>
        ''' <seealso cref="ToolStripContentPanelGradientEnd"/>
        <Description("the end color of the gradient used in the ToolStripContentPanel.")> _
        Public Property ToolStripContentPanelGradientEndColor() As Nullable(Of Color)
            Get
                Return _ToolStripContentPanelGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ToolStripContentPanelGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ToolStripDropDownBackground"/> and <see cref="ToolStripDropDownBackgroundColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ToolStripDropDownBackground As Nullable(Of Color)
        ''' <summary>Gets the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown"></see>.</returns>
        ''' <seealso cref="ToolStripDropDownBackgroundColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ToolStripDropDownBackground() As Color
            Get
                If _ToolStripDropDownBackground.HasValue Then Return _ToolStripDropDownBackground Else Return MyBase.ToolStripDropDownBackground
            End Get
        End Property
        ''' <summary>Gets or sets the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the solid background color of the <see cref="T:System.Windows.Forms.ToolStripDropDown"></see>.</returns>
        ''' <value>If this has not value <see cref="ToolStripDropDownBackground"/> returns default value</value>
        ''' <seealso cref="ToolStripDropDownBackground"/>
        <Description("the solid background color of the ToolStripDropDown.")> _
        Public Property ToolStripDropDownBackgroundColor() As Nullable(Of Color)
            Get
                Return _ToolStripDropDownBackground
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ToolStripDropDownBackground = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ToolStripGradientBegin"/> and <see cref="ToolStripGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ToolStripGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</returns>
        ''' <seealso cref="ToolStripGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ToolStripGradientBegin() As Color
            Get
                If _ToolStripGradientBegin.HasValue Then Return _ToolStripGradientBegin Else Return MyBase.ToolStripGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</returns>
        ''' <value>If this has not value <see cref="ToolStripGradientBegin"/> returns default value</value>
        ''' <seealso cref="ToolStripGradientBegin"/>
        <Description("the starting color of the gradient used in the ToolStrip background.")> _
        Public Property ToolStripGradientBeginColor() As Nullable(Of Color)
            Get
                Return _ToolStripGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ToolStripGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ToolStripGradientEnd"/> and <see cref="ToolStripGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ToolStripGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</returns>
        ''' <seealso cref="ToolStripGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ToolStripGradientEnd() As Color
            Get
                If _ToolStripGradientEnd.HasValue Then Return _ToolStripGradientEnd Else Return MyBase.ToolStripGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</returns>
        ''' <value>If this has not value <see cref="ToolStripGradientEnd"/> returns default value</value>
        ''' <seealso cref="ToolStripGradientEnd"/>
        <Description("the end color of the gradient used in the ToolStrip background.")> _
        Public Property ToolStripGradientEndColor() As Nullable(Of Color)
            Get
                Return _ToolStripGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ToolStripGradientEnd = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ToolStripGradientMiddle"/> and <see cref="ToolStripGradientMiddleColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ToolStripGradientMiddle As Nullable(Of Color)
        ''' <summary>Gets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</returns>
        ''' <seealso cref="ToolStripGradientMiddleColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ToolStripGradientMiddle() As Color
            Get
                If _ToolStripGradientMiddle.HasValue Then Return _ToolStripGradientMiddle Else Return MyBase.ToolStripGradientMiddle
            End Get
        End Property
        ''' <summary>Gets or sets the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the middle color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStrip"></see> background.</returns>
        ''' <value>If this has not value <see cref="ToolStripGradientMiddle"/> returns default value</value>
        ''' <seealso cref="ToolStripGradientMiddle"/>
        <Description("the middle color of the gradient used in the ToolStrip background.")> _
        Public Property ToolStripGradientMiddleColor() As Nullable(Of Color)
            Get
                Return _ToolStripGradientMiddle
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ToolStripGradientMiddle = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ToolStripPanelGradientBegin"/> and <see cref="ToolStripPanelGradientBeginColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ToolStripPanelGradientBegin As Nullable(Of Color)
        ''' <summary>Gets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel"></see>.</returns>
        ''' <seealso cref="ToolStripPanelGradientBeginColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ToolStripPanelGradientBegin() As Color
            Get
                If _ToolStripPanelGradientBegin.HasValue Then Return _ToolStripPanelGradientBegin Else Return MyBase.ToolStripPanelGradientBegin
            End Get
        End Property
        ''' <summary>Gets or sets the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the starting color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel"></see>.</returns>
        ''' <value>If this has not value <see cref="ToolStripPanelGradientBegin"/> returns default value</value>
        ''' <seealso cref="ToolStripPanelGradientBegin"/>
        <Description("the starting color of the gradient used in the ToolStripPanel.")> _
        Public Property ToolStripPanelGradientBeginColor() As Nullable(Of Color)
            Get
                Return _ToolStripPanelGradientBegin
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ToolStripPanelGradientBegin = value
            End Set
        End Property

        ''' <summary>Contains value of the <see cref="ToolStripPanelGradientEnd"/> and <see cref="ToolStripPanelGradientEndColor"/> properties</summary>
        <EditorBrowsable(EditorBrowsableState.Never)> _
        Private _ToolStripPanelGradientEnd As Nullable(Of Color)
        ''' <summary>Gets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel"></see>.</returns>
        ''' <seealso cref="ToolStripPanelGradientEndColor"/>
        <EditorBrowsable(EditorBrowsableState.Advanced)> _
        <Browsable(False)> _
        Public NotOverridable Overrides ReadOnly Property ToolStripPanelGradientEnd() As Color
            Get
                If _ToolStripPanelGradientEnd.HasValue Then Return _ToolStripPanelGradientEnd Else Return MyBase.ToolStripPanelGradientEnd
            End Get
        End Property
        ''' <summary>Gets or sets the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel"></see>.</summary>
        ''' <returns>A <see cref="T:System.Drawing.Color"></see> that is the end color of the gradient used in the <see cref="T:System.Windows.Forms.ToolStripPanel"></see>.</returns>
        ''' <value>If this has not value <see cref="ToolStripPanelGradientEnd"/> returns default value</value>
        ''' <seealso cref="ToolStripPanelGradientEnd"/>
        <Description("the end color of the gradient used in the ToolStripPanel.")> _
        Public Property ToolStripPanelGradientEndColor() As Nullable(Of Color)
            Get
                Return _ToolStripPanelGradientEnd
            End Get
            Set(ByVal value As Nullable(Of Color))
                _ToolStripPanelGradientEnd = value
            End Set
        End Property

    End Class
End Namespace