#include "Common.h"
#include "PluginBase.h"
#include "ContentFieldSpecification.h"
#include "Exceptions.h"

namespace Tools {
    namespace TotalCommanderT {
        using namespace System;
        using namespace System::ComponentModel;
#pragma region "ContentFieldSpecification"
        ContentFieldSpecification::ContentFieldSpecification(int FieldIndex, ContentFieldType FieldType, String^ FieldName, FieldFlags Flags, ... cli::array<String^>^ Units) {
            if (!Tools::TypeTools::IsDefined(FieldType)) throw gcnew InvalidEnumArgumentException("FieldType", (int)FieldType, FieldType.GetType());
            if (FieldName == nullptr) throw gcnew ArgumentNullException("FieldName");
            if (FieldIndex < 0) throw gcnew ArgumentOutOfRangeException("FieldIndex");
            if (FieldName->Contains(".") || FieldName->Contains("|") || FieldName->Contains(":") || FieldName->Contains(gcnew String((Char)0, 1))) throw gcnew ArgumentException(ResourcesT::Exceptions::InvalidFieldNameCharacter, "FieldName");
            if (Units != nullptr)
                for each(String^ Unit in Units)
                    if (Unit->Contains(".") || Unit->Contains("|") || Unit->Contains(":") || Unit->Contains(gcnew String((Char)0, 1))) throw gcnew ArgumentException(ResourcesT::Exceptions::InvalidUnitNameCharacter, "Units");
            if (FieldType == ContentFieldType::NoMoreFields) throw gcnew ArgumentException(ResourcesT::Exceptions::InvalidFieldTypeFormat(FieldType), "FieldType");
            this->fieldIndex = FieldIndex;
            this->fieldType = FieldType;
            this->fieldName = FieldName;
            this->units = Units;
            this->flags = Flags;
        }
        inline int ContentFieldSpecification::FieldIndex::get() { return this->fieldIndex; }
        inline ContentFieldType ContentFieldSpecification::FieldType::get() { return this->fieldType; }
        inline String^ ContentFieldSpecification::FieldName::get() { return this->fieldName; }
        inline cli::array<String^>^ ContentFieldSpecification::Units::get() { return this->units; }
        inline FieldFlags ContentFieldSpecification::Flags::get() { return this->flags; }
#pragma endregion
    }
}