Namespace MetadataT
#If Config <= Nightly Then 'Stage:Nightly
    ''' <summary>Provides metadata information such as image or music metadata</summary>
    ''' <remarks>Purpose of this interface is read-only metadata retrieval</remarks>
    ''' <seelaso cref="IMetadata"/>
    ''' <version version="1.5.2" stage="Nightly">Interface introduced</version>
    Public Interface IMetadataProvider
        ''' <summary>Gets value indicating if metadata of particular type are provided by this provider</summary>
        ''' <param name="MetadataName">Name of metadata type</param>
        ''' <returns>True if this provider contains metadata with given name</returns>
        Function Contains(ByVal MetadataName$) As Boolean
        ''' <summary>Gets names of metadata actually contained in metadata source represented by this provider</summary>
        ''' <returns>Names of metadata usefull with the <see cref="Metadata"/> function. Never returns null; may return an empty enumeration.</returns>
        Function GetContainedMetadataNames() As IEnumerable(Of String)
        ''' <summary>Get all the names of metadata supported by this provider (even when some of the metadata cannot be provided by current instance)</summary>
        ''' <returns>Name sof metadata usefull with the <see cref="Metadata"/> function. Never returns null; should not return an empty enumeration.</returns>
        Function GetSupportedMetadataNames() As IEnumerable(Of String)
        ''' <summary>Gets metadata of particular type</summary>
        ''' <param name="MetadataName">Name of metadata to get (see <see cref="GetSupportedMetadataNames"/> for possible values)</param>
        ''' <returns>Metadata of requested type; or null if metadata of type <paramref name="MetadataName"/> are not contained in this instance or are not supported by this provider.</returns>
        Default ReadOnly Property Metadata(ByVal MetadataName$) As IMetadata
    End Interface
    ''' <summary>Represents kind of metadata provided by metadata provider (such as Exif or ID3)</summary>
    ''' <remarks>Purpose of this interface is read-only metadata retrieval</remarks>
    ''' <seelaso cref="IMetadataProvider"/>
    ''' <version version="1.5.2" stage="Nightly">Interface introduced</version>
    Public Interface IMetadata
        ''' <summary>Gets name of metadata format represented by implementation</summary>
        ''' <returns>Name to identify this metadata format by</returns>
        ''' <remarks>All <see cref="IMetadataProvider">IMetadataProviders</see> returning this format should identify the format by this name.</remarks>
        ReadOnly Property Name$()
        ''' <summary>Gets medata value with given key</summary>
        ''' <param name="Key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>Value of metadata item with given key; or null if given metadata value is not supported</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format and it is not one of predefined names</exception>
        ''' <remarks>The <paramref name="Key"/> peremeter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).</remarks>
        Default ReadOnly Property Value(ByVal Key$) As Object
        ''' <summary>Gets value indicating wheather metadata value with given key is present in current instance</summary>
        ''' <param name="Key">Key (or name) to check presence of (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>True when value for given key is present; false otherwise</returns>
        ''' <remarks>The <paramref name="Key"/> parameter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).</remarks>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format and it is not one of predefined names</exception>
        Function ContainsKey(ByVal Key$) As Boolean
        ''' <summary>Gets keys of all the metadata present in curent instance</summary>
        ''' <returns>Keys in metadata-specific format of all the metadata present in curent instance. Never returns null; may return anempt enumeration.</returns>
        Function GetContainedKeys() As IEnumerable(Of String)
        ''' <summary>Gets all keys predefined for curent metadata format</summary>
        ''' <returns>Eumeration containing all predefined (well-known) keys of metadata for this metadata format. Returns always the same enumeration even when values for some keys are not present. Never returns null; may return an empty enumeration.</returns>
        ''' <remarks>Not all predefined keys are required to have corresponding names obtainable via <see cref="GetNameOfKey"/>.</remarks>
        Function GetPredefinedKeys() As IEnumerable(Of String)
        ''' <summary>Gets all predefined names for metadata keys</summary>
        ''' <returns>Enumeration containing all predefined names of metadata items for this metadada format. Never returns null; may return an empty enumeration.</returns>
        ''' <remarks>Metadata format may support 2 formats of retrieving of metadata values: By key and by name. The by-name format is optional.
        ''' Keys are typically computer-friendly strings (like tag numbers or addresses) and metedata format may support values with non-predefined keys.
        ''' Names are typically human-friendly (not-localized) string (like names) and only predefined names are supported (if any). Each name must have its corresponding key. Names are only aliases to certain important keys.</remarks>
        Function GetPredefinedNames() As IEnumerable(Of String)
        ''' <summary>Gets name for key</summary>
        ''' <param name="Key">Key to get name for</param>
        ''' <returns>One of predefined names to use instead of <paramref name="Key"/>; null when given key has no corresponding name.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format</exception>
        Function GetNameOfKey$(ByVal Key$)
        ''' <summary>Gets key for predefined name</summary>
        ''' <param name="Name">Name to get key for</param>
        ''' <returns>Key in metadata-specific format for given predefined metadata item name</returns>
        ''' <exception cref="ArgumentException"><paramref name="Name"/> is not one of predefined names retuened by <see cref="GetPredefinedNames"/>.</exception>
        Function GetKeyOfName$(ByVal Name$)
        ''' <summary>Gets localized human-readable name for given key (or name)</summary>
        ''' <param name="Key">Key (or name) to get name for</param>
        ''' <returns>Human-readable descriptive name of metadata item identified by <paramref name="Key"/>; null when no such name is defined/known.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid formar or it is not one of predefined names.</exception>
        Function GetHumanName$(ByVal Key$)
        ''' <summary>Gets localized description for given key (or name)</summary>
        ''' <param name="Key">Key (or name) to get description of</param>
        ''' <returns>Localized description of purpose of metadata item identified by <paramref name="Key"/>; nul when description is not available.</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> is in invalid format or it is not one of predefined names.</exception>
        Function GetDescription$(ByVal Key$)
        ''' <summary>Gets metadata value with given key as string</summary>
        ''' <param name="Key">Key (or name) to get vaue for (see <see cref="GetPredefinedKeys"/> for possible values)</param>
        ''' <returns>Value of metadata item with given key as string; or null if given metadata value is not supported</returns>
        ''' <exception cref="ArgumentException"><paramref name="Key"/> has invalid format and it is not one of predefined names</exception>
        ''' <remarks>The <paramref name="Key"/> peremeter can be either key in metadata-specific format or predefined name of metadata item (if predefined names are supported).</remarks>
        Function GetStringValue$(ByVal Key$)
    End Interface
#End If
End Namespace