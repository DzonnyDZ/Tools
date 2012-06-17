Imports Tools.MetadataT

''' <summary>Common base class for user control used to configure export formats</summary>
''' <remarks><note type="inheritinfo">Classes implementing this interface must derive from <see cref="Control"/>.</note></remarks>
''' <version version="2.0.5">This interface is new in version 2.0.5</version>
Public Interface IExportFormatConfigurator
    'Inherits UserControl
    ''' <summary>Gets name of format to export data in</summary>
    ReadOnly Property FormatName$
    ''' <summary>Saves data in current format to given file</summary>
    ''' <param name="stream">Stream to export metadata to</param>
    ''' <param name="selectedFields">Denotes which fields are selected for metadata</param>
    ''' <param name="data">Metadata to be saved. One item per file.</param>
    ''' <param name="culture">Culture to export data in</param>
    ''' <version version="2.0.7">Added parameter <paramref name="culture"/>.</version>
    Sub Save(ByVal stream As IO.Stream, ByVal selectedFields As IDictionary(Of String, List(Of String)), ByVal data As IEnumerable(Of IMetadataProvider), culture As IFormatProvider)
    ''' <summary>Gets filter for <see cref="OpenFileDialog"/>.</summary>
    ''' <seelaso cref="OpenFileDialog.Filter"/>
    ReadOnly Property FileFilter$
End Interface
