#pragma once

#include "..\Plugin\fsplugin.h"
#include "..\Common.h"
#include "..\ContentPluginBase.h"
#include "WFX Enums.h"

namespace Tools{namespace TotalCommanderT{
    using namespace System;
    using namespace System::ComponentModel;

    /// <summary>Defines source view column value</summary>
    /// <version version="1.5.3">This class in new in version 1.5.3</version>
    public ref class ColumnSource sealed{
    private:
        /// <summary>Contains value of the <see cref="Source"/> property</summary>
        String^ source;
        /// <summary>Contains value of the <see cref="Field"/> property</summary>
        String^ field;
        /// <summary>Contains value of the <see cref="Unit"/> property</summary>
        String^ unit;
    public:
        /// <summary>CTor</summary>
        /// <param name="source">Source of the value. This is plugin name or "<c>tc</c>" for Total Commander</param>
        /// <param name="field">Name of filed inside <paramref name="source"/> to get value of column form</param>
        /// <param name="unit">Name of value unit to use to display value of field <paramref name="field"/>. This can be null. Use null if <paramref name="field"/> does not support units.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> or <paramref name="field"/> is null or an empty string</exception>
        /// <exception cref="ArgumentException"><paramref name="source"/>, <paramref name="field"/> or <paramref name="unit"/> contains dot (.), pipe (|), colon (:), end brace (]) or null character</exception>
        ColumnSource(String^ source, String^ field, String^ unit);
        /// <summary>Gets name of source of column value</summary>
        /// <returns>Name of source of column value. This is name of plugin or "<c>tc</c>" for Total Commander itself.</returns>
        property String^ Source{String^ get();}
        /// <summary>Gets name of field to get value of column from</summary>
        /// <returns>Name of field to get value of column from. Possible names of fields depends on <see cref="Source"/>.</returns>
        property String^ Field{String ^get();}
        /// <summary>Gets name of unit to use to display value of <see cref="Field"/></summary>
        /// <returns>Name of unit to use to display value of <see cref="Field"/>. Null when no unit or default unit shall be used.</returns>
        property String^ Unit{String^ get();}
        /// <summary>Gets string representation of column source as used by Total Commander</summary>
        /// <returns>String in form "<c>[=<see cref="Source"/>.<see cref="Field"/>.<see cref="Unit"/>]</c>"</returns>
        virtual String^ ToString() sealed override;
    };
    
    /// <summary>Defines a column in Total Commander view</summary>
    /// <version version="1.5.3">This class in new in version 1.5.3</version>
    public ref class ColumnDefinition sealed{
    private:
        /// <summary>Contains value of the <see cref="Source"/> property</summary>
        initonly ColumnSource^ source;//[=<fs>.size.bkM2]\n[=fs.writetime]
        /// <summary>Contains value of the <see cref="Name"/> property</summary>
        initonly String^ name;//\n-separated
        /// <summary>Contains value of the <see cref="Width"/> property</summary>
        initonly int width;//,-separated
    public:
        /// <summary>CTor</summary>
        /// <param name="source">Source of column value</param>
        /// <param name="name">Name of column (as show to the user in header</param>
        /// <param name="width">Initial width of column in pixels. Negative value to right-align.</param>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is null -or- <paramref name="name"/> is null or an empty string</exception>
        /// <exception cref="ArgumentException"><paramref name="name"/> contains substring "<c>\n</c>" (not a new line but literally "<c>\n</c>").</exception>
        ColumnDefinition(ColumnSource^ source, String^ name, int width);
        /// <summary>Gets source of column value</summary>
        /// <returns>Source of column value</returns>
        property ColumnSource^ Source{ColumnSource^ get();}
        /// <summary>Gets name of column as show to user</summary>
        /// <returns>Header name of column</returns>
        /// <remarks>
        /// This name may be passed to Total Commander.
        /// Because of limitations of Total Commander interface plugin you should use only ASNI-defined (i.e. in <see cref="System::Text::Encoding::Default"/>) characters.
        /// E.g. in file system plugin, this value is returned from <see cref="FileSystemPlugin::FsGetDefRootName"/> (an ANSI-only function).
        /// </remarks>
        property String^ Name{String^ get();}
        /// <summary>Gets width of column</summary>
        /// <returns>Initial width of column. Negative for right align.</returns>
        property int Width{int get();}
    };
  
    /// <summary>Defines custom view in Total Commander</summary>
    /// <remarks>Custom view is a set of columns, their sources, names and widths as show in Total Commander pane to the user</remarks>
    /// <version version="1.5.3">This class in new in version 1.5.3</version>
    public ref class ViewDefinition sealed{
    private:
        /// <summary>Contains value of the <see cref="Columns"/> property</summary>
        initonly array<ColumnDefinition^>^ columns;
        /// <summary>Contains value of the <see cref="AutoAdjust"/> property</summary>
        initonly bool autoAdjust;
        /// <summary>Contains value of the <see cref="HorizontalScroll"/> property</summary>
        initonly bool horizontalScroll;
        /// <summary>Contains value of the <see cref="FileNameWidth"/> property</summary>
        int fileNameWidth;
        /// <summary>Contains value of the <see cref="ExtensionWidth"/> property</summary>
        int extensionWidth;
    public:
        /// <summary>CTor</summary>
        /// <param name="columns">View columns definition. Cannot be null, but can be empty. <note>File name and extension columns are <strong>not</strong> part of this definition.</note></param>
        /// <param name="FileNameWidth">Width of the File Name column. Negative for right align.</param>
        /// <param name="ExtensionWidth">Width of the Extension column. Negative for right align.</param>
        /// <param name="autoAdjust">True to automatically adjust columns width as Total Commander window resizes; false to keep widths</param>
        /// <param name="horizontalScroll">True to show horizontal scrollbar; false not to show horizontal scrollbar</param>
        /// <exception cref="ArgumentNullException"><paramref name="columns"/> is null</exception>
        ViewDefinition(array<ColumnDefinition^>^ columns, int FileNameWidth, int ExtensionWidth, bool autoAdjust, bool horizontalScroll);
        /// <summary>Gets columns definitions</summary>
        /// <returns>Columns definitions defining sources, names and widths of columns</returns>
        property array<ColumnDefinition^>^ Columns{array<ColumnDefinition^>^ get();}
        /// <summary>Gets value indicating if column widths are automatically adjusted as Total Commander window resize or not</summary>
        /// <returns>True when columns are automatically resized as Total Commander Window resizes; false when column widths are kept unchanged.</returns>
        property bool AutoAdjust{bool get();}
        /// <summary>Gets value indicating if horizontal scrollbar is shown</summary>
        /// <returns>True when horizontal scrollbar is shown; false if it is not shown</returns>
        property bool HorizontalScroll{bool get();}
        /// <summary>Gets width of the File Name column</summary>
        /// <returns>Width of the File Name column. Negative if the column is right-aligned.</returns>
        property int FileNameWidth{int get();}
        /// <summary>Gets width of the Extension column</summary>
        /// <returns>Width of the Extension column. Negative if the column is right-aligned.</returns>
        property int ExtensionWidth{int get();}
        /// <summary>Gets string in Total-Commander-used format that defines all the columns sources.</summary>
        /// <returns>String in format "<c>[=source.field.unit]\n[=source.filed.unit]</c>" Each column source is represented by <see cref="ColumnSource::ToString"/>, column sources are separated by \n (not a new line but "\n").</returns>
        String^ GetColumnSourcesString();
        /// <summary>Gets string in Total-Commander-used format that defines all the columns names.</summary>
        /// <returns>\n-separated column names (\n is "\n" not a new line)</returns>
        String^ GetNamesString();
        /// <summary>Gets string in Total-Commander.used format that defines all the columns widths</summary>
        /// <returns>Comma-(,)-separated column widths</returns>
        /// <remarks><note>Column widths always includes at least 2-items (1st and 2nd) for File Name and Extension. Those columns never have source and name configured.</note></remarks>
        String^ GetWidthsString();
        /// <summary>Gets string in Total-Commander-used format that defines view options</summary>
        /// <returns>Returns string consisting of two |-separated parts. Part 1 specifies <see cref="AutoAdjust"/> and it is either "<c>auto-adjust-width</c>" (for true) or "<c>-1</c>" (for false). Part 2 specifies <see cref="HorizontalScroll"/> and it is either 0 or 1 (for false or true respectively).</returns>
        String^ GetOptionsString();
    }; 
}}