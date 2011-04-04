#pragma once

#using <mscorlib.dll>
#using <System.dll>

using namespace System::Security::Permissions;
[assembly:SecurityPermissionAttribute(SecurityAction::RequestMinimum, SkipVerification=false)];
namespace Tools {
    namespace TotalCommanderT {
        namespace ResourcesT {
    using namespace System;
    using namespace System;
    ref class Exceptions;
    
    
    /// <summary>
    /// Třída silně typovaných zdrojů pro vyhledávání, formátování atd. lokalizačních řetězců.
    /// </summary>
    // Tato třída byla automaticky vygenerována pomocí třídy StronglyTypedResourceVuildeEx přes custom tool InternalResXFileCodeGeneratorEx.
    // Pro přidání nebo odebrání člena editujte váš .resx soubor a pak spusťte custom tool InternalResXFileCodeGeneratorEx nebo přebuildujte váš projek ve VS.NET.
    // Copyright © Dmytro Kryvko 2006-8 (http://dmytro.kryvko.googlepages.com/); Đonny 2008-2011 (http://tools.codeplex.com)
    [System::CodeDom::Compiler::GeneratedCodeAttribute(L"Tools.VisualStudioT.GeneratorsT.ResXFileGenerator.StronglyTypedResourceBuilderEx", 
    L"1.5.3.21235"), 
    System::Diagnostics::DebuggerNonUserCodeAttribute, 
    System::Diagnostics::CodeAnalysis::SuppressMessageAttribute(L"Microsoft.Naming", L"CA1724:TypeNamesShouldNotMatchNamespaces")]
    ref class Exceptions {
        
        private: static ::System::Resources::ResourceManager^  _resourceManager;
        
        private: static ::System::Object^  _internalSyncObject;
        
        private: static ::System::Globalization::CultureInfo^  _resourceCulture;
        
        private: [System::Diagnostics::CodeAnalysis::SuppressMessageAttribute(L"Microsoft.Performance", L"CA1811:AvoidUncalledPrivateCode")]
        Exceptions();
        /// <summary>
        /// Vláknově bezpešný zamykací objekt používaný touto třídou.
        /// </summary>
        public: static property ::System::Object^  InternalSyncObject {
            ::System::Object^  get();
        }
        
        /// <summary>
        /// Vrací nacacheovanou instanci ResourceManageru používanou touto třídou.
        /// </summary>
        public: [System::ComponentModel::EditorBrowsableAttribute(::System::ComponentModel::EditorBrowsableState::Advanced)]
        static property ::System::Resources::ResourceManager^  ResourceManager {
            ::System::Resources::ResourceManager^  get();
        }
        
        /// <summary>
        /// Přepisuje hodnoty vlastnosti CurrentUICulture aktuálního vlákna pro všechna
        /// vyhledávání resourců pomocí této silně typové třídy.
        /// </summary>
        public: [System::ComponentModel::EditorBrowsableAttribute(::System::ComponentModel::EditorBrowsableState::Advanced)]
        static property ::System::Globalization::CultureInfo^  Culture {
            ::System::Globalization::CultureInfo^  get();
            System::Void set(::System::Globalization::CultureInfo^  value);
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný '{0} cannot be represented as {1}, use {2} and {3} instead.'.
        /// </summary>
        public: static property System::String^  CannotBeRepresented {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'Value of type {0} cannot be compared to {1}.'.
        /// </summary>
        public: static property System::String^  CannotCompare {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'Column definition string returned by {0} is too long.'.
        /// </summary>
        public: static property System::String^  ColumnDefinitionStringTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'Default text is too long.'.
        /// </summary>
        public: static property System::String^  DefaultTextTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný '{0} returned fields which&apos;s name is longer than {1}'.
        /// </summary>
        public: static property System::String^  FieldNameTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'Value &quot;{0}&quot; contains invalid character.'.
        /// </summary>
        public: static property System::String^  InvalidCharacter {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný '{0} returned field at index {1} that has FieldIndex set to {2}'.
        /// </summary>
        public: static property System::String^  InvalidFieldIndex {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'Field name contain invalid character.'.
        /// </summary>
        public: static property System::String^  InvalidFieldNameCharacter {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný '{0:f} is not valid type of field.'.
        /// </summary>
        public: static property System::String^  InvalidFieldType {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'Macor name &quot;{0}&quot; is invalid.'.
        /// </summary>
        public: static property System::String^  InvalidMacroName {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'The path {0} has invalid format.'.
        /// </summary>
        public: static property System::String^  InvalidPathFormat {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'Unit name contains invalid character.'.
        /// </summary>
        public: static property System::String^  InvalidUnitNameCharacter {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'Name too long. Mamximum allowed length is {0}'.
        /// </summary>
        public: static property System::String^  NameTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'The {0} parameter of the {1} method was assigned to long string.'.
        /// </summary>
        public: static property System::String^  ParamAssignedTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'String returned by plugin is longer than maximal path length alowed.'.
        /// </summary>
        public: static property System::String^  PathTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'Plugin was not initialized.'.
        /// </summary>
        public: static property System::String^  PluginNotInitialized {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'The {0} property have already been initialized.'.
        /// </summary>
        public: static property System::String^  PropertyWasInitialized {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'The {0} property have not been initialized yet.'.
        /// </summary>
        public: static property System::String^  PropertyWasNotInitialized {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný '{0} was null.'.
        /// </summary>
        public: static property System::String^  PropertyWasNull {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'Array returned by {0} is too long.'.
        /// </summary>
        public: static property System::String^  ReturnedArrayToLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'The string returned by {0} for the {1} type is too long.'.
        /// </summary>
        public: static property System::String^  ReturnedStringTooLongForChoice {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný 'Unexteced type {0} retunrned by {1}'.
        /// </summary>
        public: static property System::String^  UnexpectedType {
            System::String^  get();
        }
        
        /// <summary>
        /// Najde lokalizivaný řetězec podobný '{0} returned unit names sum of which&apos;s length plus number of them minus 1 is more than {1}'.
        /// </summary>
        public: static property System::String^  UnitNamesTooLong {
            System::String^  get();
        }
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný '{0} cannot be represented as {1}, use {2} and {3} instead.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <param name="arg1">Objekt (1) pro formátování.</param>
        /// <param name="arg2">Objekt (2) pro formátování.</param>
        /// <param name="arg3">Objekt (3) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: [System::Diagnostics::CodeAnalysis::SuppressMessageAttribute(L"Microsoft.Design", L"CA1025:ReplaceRepetitiveArgumentsWithParamsArray")]
        static System::String^  CannotBeRepresentedFormat(System::Object^  arg0, System::Object^  arg1, System::Object^  arg2, 
                    System::Object^  arg3);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'Value of type {0} cannot be compared to {1}.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <param name="arg1">Objekt (1) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  CannotCompareFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'Column definition string returned by {0} is too long.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  ColumnDefinitionStringTooLongFormat(System::Object^  arg0);
        
        /// <summary>
        /// Pahýl formátovací metody vracející hodnotu vlastnosti DefaultTextTooLong.
        /// </summary>
        /// <returns>Hodnota vlastnosti DefaultTextTooLong.</returns>
        public: static System::String^  DefaultTextTooLongFormat();
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný '{0} returned fields which&apos;s name is longer than {1}'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <param name="arg1">Objekt (1) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  FieldNameTooLongFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'Value &quot;{0}&quot; contains invalid character.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  InvalidCharacterFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný '{0} returned field at index {1} that has FieldIndex set to {2}'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <param name="arg1">Objekt (1) pro formátování.</param>
        /// <param name="arg2">Objekt (2) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  InvalidFieldIndexFormat(System::Object^  arg0, System::Object^  arg1, System::Object^  arg2);
        
        /// <summary>
        /// Pahýl formátovací metody vracející hodnotu vlastnosti InvalidFieldNameCharacter.
        /// </summary>
        /// <returns>Hodnota vlastnosti InvalidFieldNameCharacter.</returns>
        public: static System::String^  InvalidFieldNameCharacterFormat();
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný '{0:f} is not valid type of field.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  InvalidFieldTypeFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'Macor name &quot;{0}&quot; is invalid.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  InvalidMacroNameFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'The path {0} has invalid format.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  InvalidPathFormatFormat(System::Object^  arg0);
        
        /// <summary>
        /// Pahýl formátovací metody vracející hodnotu vlastnosti InvalidUnitNameCharacter.
        /// </summary>
        /// <returns>Hodnota vlastnosti InvalidUnitNameCharacter.</returns>
        public: static System::String^  InvalidUnitNameCharacterFormat();
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'Name too long. Mamximum allowed length is {0}'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  NameTooLongFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'The {0} parameter of the {1} method was assigned to long string.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <param name="arg1">Objekt (1) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  ParamAssignedTooLongFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Pahýl formátovací metody vracející hodnotu vlastnosti PathTooLong.
        /// </summary>
        /// <returns>Hodnota vlastnosti PathTooLong.</returns>
        public: static System::String^  PathTooLongFormat();
        
        /// <summary>
        /// Pahýl formátovací metody vracející hodnotu vlastnosti PluginNotInitialized.
        /// </summary>
        /// <returns>Hodnota vlastnosti PluginNotInitialized.</returns>
        public: static System::String^  PluginNotInitializedFormat();
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'The {0} property have already been initialized.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  PropertyWasInitializedFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'The {0} property have not been initialized yet.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  PropertyWasNotInitializedFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný '{0} was null.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  PropertyWasNullFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'Array returned by {0} is too long.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  ReturnedArrayToLongFormat(System::Object^  arg0);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'The string returned by {0} for the {1} type is too long.'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <param name="arg1">Objekt (1) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  ReturnedStringTooLongForChoiceFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný 'Unexteced type {0} retunrned by {1}'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <param name="arg1">Objekt (1) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  UnexpectedTypeFormat(System::Object^  arg0, System::Object^  arg1);
        
        /// <summary>
        /// Formátuje lokalizovaný řetězec podobný '{0} returned unit names sum of which&apos;s length plus number of them minus 1 is more than {1}'.
        /// </summary>
        /// <param name="arg0">Objekt (0) pro formátování.</param>
        /// <param name="arg1">Objekt (1) pro formátování.</param>
        /// <returns>Kopie ofrmátovacího řetězce, kde formátovací položky byly nahrazeny řetězcovými ekvivalenty instancí objektů předaných do parametrů.</returns>
        public: static System::String^  UnitNamesTooLongFormat(System::Object^  arg0, System::Object^  arg1);
    };
        }
    }
}