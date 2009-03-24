#include "Date.h"
#include "Exceptions.h"

using namespace ::System;
using namespace ::System::Text::RegularExpressions;
using namespace ::Tools::TotalCommanderT::ResourcesT;
using namespace Tools::ExtensionsT;

namespace Tools{

#pragma region "Date"
    inline Date::Date(DateTime value){this->date=value.Date;}
    inline Date::Date(int Year, int Month, int Day){this->date = DateTime(Year,Month,Day);}
    inline DateTime Date::Value::get(){return this->date;}
    inline int Date::Year::get(){return this->Value.Year;}
    inline int Date::Month::get(){return this->Value.Month;}
    inline int Date::Day::get(){return this->Value.Day;}
    inline System::DayOfWeek Date::DayOfWeek::get(){return this->Value.DayOfWeek;}
    inline int Date::DayOfYear::get(){return this->Value.DayOfYear;}
    inline Date Date::Today::get(){return Date(DateTime::Today);}
    inline DateTime Date::operator+(Date a, TimeSpan b){return a.Value + b;}
    inline DateTime Date::operator-(Date a, TimeSpan b){return a.Value - b;}
    inline TimeSpan Date::operator-(Date a, Date b){return a.Value - b.Value;}
    inline TimeSpan Date::operator-(Date a, DateTime b){return a.Value - b;}
    inline TimeSpan Date::operator-(DateTime a, Date b){return a - b.Value;}

    inline bool Date::operator>(Date a, Date b){return a.Value > b.Value;}
    inline bool Date::operator<(Date a, Date b){return a.Value < b.Value;}
    inline bool Date::operator<=(Date a, Date b){return a.Value <= b.Value;}
    inline bool Date::operator>=(Date a, Date b){return a.Value >= b.Value;}
    inline bool Date::operator==(Date a, Date b){return a.Value == b.Value;}
    inline bool Date::operator!=(Date a, Date b){return a.Value != b.Value;}

    inline bool Date::operator>(Date a, DateTime b){return a.Value > b;}
    inline bool Date::operator<(Date a, DateTime b){return a.Value < b;}
    inline bool Date::operator<=(Date a, DateTime b){return a.Value <= b;}
    inline bool Date::operator>=(Date a, DateTime b){return a.Value >= b;}
    inline bool Date::operator==(Date a, DateTime b){return a.Value == b;}
    inline bool Date::operator!=(Date a, DateTime b){return a.Value != b;}

    inline bool Date::operator>(DateTime a, Date b){return a > b.Value;}
    inline bool Date::operator<(DateTime a, Date b){return a < b.Value;}
    inline bool Date::operator<=(DateTime a, Date b){return a <= b.Value;}
    inline bool Date::operator>=(DateTime a, Date b){return a >= b.Value;}
    inline bool Date::operator==(DateTime a, Date b){return a == b.Value;}
    inline bool Date::operator!=(DateTime a, Date b){return a != b.Value;}

    inline String^ Date::ToString(){return this->Value.ToString();}
    inline String^ Date::ToString(IFormatProvider^ provider){return this->Value.ToString(provider);}
    inline String^ Date::ToString(String^ Format){return this->Value.ToString(Format);}
    inline String^ Date::ToString(String^ Format, IFormatProvider^ provider){return this->Value.ToString(Format,provider);}
    Int32 Date::CompareTo(Object^ obj){
        if(obj == nullptr) return 1;
        else if(Date::typeid->IsAssignableFrom(obj->GetType())) return this->CompareTo((Date)obj);
        else if(DateTime::typeid->IsAssignableFrom(obj->GetType())) return this->CompareTo((DateTime)obj);
        else throw gcnew ArgumentException(Tools::TotalCommanderT::ResourcesT::Exceptions::CannotCompareFormat(obj->GetType()->Name, Date::typeid->Name));
    }
    inline Int32 Date::CompareTo(Date obj){return this->Value.CompareTo(obj.Value);}
    inline Int32 Date::CompareTo(DateTime obj){return this->Value.CompareTo(obj);}
    inline Boolean Date::Equals(Date other){return *this == other;}
    inline Boolean Date::Equals(DateTime other){return *this == other;}
    bool Date::Equals(Object^ other){
        if(other == nullptr) return false;
        else if(Date::typeid->IsAssignableFrom(other->GetType())) return *this == (Date) other;
        else if(DateTime::typeid->IsAssignableFrom(other->GetType())) return *this == (DateTime) other;
        else return false;
    }
    inline int Date::GetHashCode(){return this->Value.GetHashCode();}

    inline Date::operator DateTime(Date a){return a.Value;}
    inline Date::operator Date(DateTime a){return Date(a);}

    void Date::Populate(pdateformat target){
        if(target==NULL) throw gcnew ArgumentNullException("target");
        target->wDay = this->Day;
        target->wMonth = this->Month;
        target->wYear = this->Year;
    }
    Date::Date(pdateformat a){
        if(a==NULL) throw gcnew ArgumentNullException("a");
        this->date = DateTime(a->wYear,a->wMonth,a->wDay);
    }
#pragma endregion
}