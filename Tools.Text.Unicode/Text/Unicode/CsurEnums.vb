Namespace TextT.UnicodeT
    ''' <summary>Status of character, group or block in ConScript Unicode Registry (CSUR)</summary>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum CsurStatus
        ''' <summary>The code point is not part of CSUR in any way (i.e. it's not private use character governed by CSUR)</summary>
        NotInCsur
        ''' <summary>Script registration was proposed but it's not yet registered.</summary>
        Proposal
        ''' <summary>Script was registered in ConScript Unicode Registry</summary>
        Registered
        ''' <summary>Used for characters that are reserved for special purposes. Not used for character marked as reserved within blocks of individual scipts (<see cref="Proposal"/> or <see cref="Registered"/> is used in this case)</summary>
        Reserved
        ''' <summary>The character falls under CSUR range but is currently neither reserved for special purposes nor it is part of registered or proposed character block.</summary>
        Unassigned
    End Enum

    ''' <summary>In case of <see cref="CsurStatus.Reserved"/> indicate purpose for which the character is reserved</summary>
    ''' <version version="1.5.4">This enumeration is new in version 1.5.4</version>
    Public Enum CsurReservedFor
        ''' <summary>The code point is either not reserved by CSUR or it is not part of CSUR at all</summary>
        notReserved
        ''' <summary>Reserved for encoding hacks</summary>
        EncodingHacks
        ''' <summary>Reserved for font hacks</summary>
        FontHacks
        ''' <summary>Reserved for Apple hacks</summary>
        AppleHacks
    End Enum
End Namespace