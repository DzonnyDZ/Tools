'This script generates once-in-any-order XML-schema choice for intellisense.xsd
Option Explicit
Const rep = "<xs:group ref='repeateble-items' minOccurs='0' maxOccurs='unbounded'/>" 
Main
Sub Main()
	Dim x(5)
	x(0) = "<xs:element name='summary' minOccurs='1' maxOccurs='1' type='textblock'/>"
	x(1) = "<xs:element name='value' minOccurs='1' maxOccurs='1' type='textblock'/>"
	x(2) = "<xs:element name='returns' minOccurs='1' maxOccurs='1' type='textblock'/>"
	x(3) = "<xs:element name='remarks' minOccurs='1' maxOccurs='1' type='textblock'/>"
	x(4) = "<xs:element name='filterpriority' minOccurs='1' maxOccurs='1' type='xs:nonNegativeInteger'/>"
	x(5) = "<xs:element name='completionlist' type='emptycref' minOccurs='1' maxOccurs='1'/>"
	out x,0 
End Sub

Sub out(items,l)
	Dim i,j,cj
	Dim inner()
	If l = 0 Then prn "<xs:choice>" _
	Else prn "<xs:choice minOccurs='0' maxOccurs='1'>" 
	For i = LBound(items) To UBound(items)
	        prn "<xs:sequence minOccurs='0' maxOccurs='1'>" 
		prn items(i) 
		prn rep 
		If LBound(items) <> UBound(items) Then
			ReDim inner(UBound(items)-1)
			For j = LBound(items) To UBound(items)
				If i <> j Then
					If j < i Then cj = j Else cj = j - 1
					inner(cj) = items(j)
				End If		
			Next
			out inner, l + 1 
		End If
		prn "</xs:sequence>" 
	Next
	prn "</xs:choice>"
End Sub

Sub prn(str)
	WScript.Echo str 
End Sub