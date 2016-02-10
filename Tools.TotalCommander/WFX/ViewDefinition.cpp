//#include "stdafx.h"
#include "..\Plugin\fsplugin.h"
#include "..\Exceptions.h"
#include <vcclr.h>
#include "..\Common.h"
#include "ViewDefinition.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;
using namespace Microsoft::VisualBasic;
using namespace System::ComponentModel;

namespace Tools {
    namespace TotalCommanderT {

#pragma region "ColumnSource"
        ColumnSource::ColumnSource(String^ source, String^ field, String^ unit) {
            if (source == nullptr || source == "") throw gcnew ArgumentNullException("source");
            if (field == nullptr || field == "") throw gcnew ArgumentNullException("field");
            if (source->Contains(".") || source->Contains("|") || source->Contains(":") || source->Contains("]") || source->Contains(gcnew String((char)0, 1)))
                throw gcnew ArgumentException(ResourcesT::Exceptions::InvalidCharacterFormat(source), "source");
            if (field->Contains(".") || field->Contains("|") || field->Contains(":") || field->Contains("]") || field->Contains(gcnew String((char)0, 1)))
                throw gcnew ArgumentException(ResourcesT::Exceptions::InvalidCharacterFormat(field), "field");
            if (unit != nullptr && unit != "")
                if (unit->Contains(".") || unit->Contains("|") || unit->Contains(":") || unit->Contains("]") || unit->Contains(gcnew String((char)0, 1)))
                    throw gcnew ArgumentException(ResourcesT::Exceptions::InvalidCharacterFormat(unit), "unit");
            this->source = source;
            this->field = field;
            if (unit == nullptr || unit == "") this->unit = nullptr; else this->unit = unit;
        }

        inline String^ ColumnSource::Source::get() { return this->source; }
        inline String^ ColumnSource::Field::get() { return this->field; }
        inline String^ ColumnSource::Unit::get() { return this->unit; }
        inline String^ ColumnSource::ToString() { return "[=" + Source + "." + Field + (Unit == nullptr ? "" : ("." + Unit)) + "]"; }
#pragma endregion

#pragma region ColumnDefinition
        ColumnDefinition::ColumnDefinition(ColumnSource^ source, String^ name, int width) {
            if (source == nullptr) throw gcnew ArgumentNullException("source");
            if (name == nullptr) throw gcnew ArgumentNullException("name");
            if (name->Contains("\\n")) throw gcnew ArgumentException(ResourcesT::Exceptions::InvalidCharacterFormat(name), "name");
            this->source = source;
            this->name = name;
            this->width = width;
        }

        inline String^ ColumnDefinition::Name::get() { return this->name; }
        inline ColumnSource^ ColumnDefinition::Source::get() { return this->source; }
        inline int ColumnDefinition::Width::get() { return this->width; }
#pragma endregion

#pragma region ViewDefinition
        ViewDefinition::ViewDefinition(array<ColumnDefinition^>^ columns, int FileNameWidth, int ExtensionWidth, bool autoAdjust, bool horizontalScroll) {
            if (columns == nullptr) throw gcnew ArgumentNullException("columns");
            this->columns = columns;
            this->fileNameWidth = FileNameWidth;
            this->extensionWidth = ExtensionWidth;
            this->autoAdjust = autoAdjust;
            this->horizontalScroll = horizontalScroll;
        }

        inline array<ColumnDefinition^>^ ViewDefinition::Columns::get() { return this->columns; }
        inline bool ViewDefinition::AutoAdjust::get() { return this->autoAdjust; }
        inline bool ViewDefinition::HorizontalScroll::get() { return this->horizontalScroll; }
        inline int ViewDefinition::FileNameWidth::get() { return this->fileNameWidth; }
        inline int ViewDefinition::ExtensionWidth::get() { return this->extensionWidth; }

        String^ ViewDefinition::GetColumnSourcesString() {
            System::Text::StringBuilder^ b = gcnew System::Text::StringBuilder();
            for each(ColumnDefinition^ cd in this->Columns) {
                if (b->Length > 0) b->Append("\\n");
                b->Append(cd->Source->ToString());
            }
            return b->ToString();
        }
        String^ ViewDefinition::GetNamesString() {
            System::Text::StringBuilder^ b = gcnew System::Text::StringBuilder();
            for each(ColumnDefinition^ cd in this->Columns) {
                if (b->Length > 0) b->Append("\\n");
                b->Append(cd->Name);
            }
            return b->ToString();
        }
        String^ ViewDefinition::GetWidthsString() {
            System::Text::StringBuilder^ b = gcnew System::Text::StringBuilder();
            b->Append(this->FileNameWidth.ToString(Globalization::CultureInfo::InvariantCulture) + "," + this->ExtensionWidth.ToString(Globalization::CultureInfo::InvariantCulture));
            for each(ColumnDefinition^ cd in this->Columns)
                b->Append("," + cd->Width.ToString(Globalization::CultureInfo::InvariantCulture));
            return b->ToString();
        }
        inline String^ ViewDefinition::GetOptionsString() {
            return (this->AutoAdjust ? "auto-adjust-width" : "-1") + "|" + (this->HorizontalScroll ? "1" : "0");
        }
#pragma endregion

    }
}