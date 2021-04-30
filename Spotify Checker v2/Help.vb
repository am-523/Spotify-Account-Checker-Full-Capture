Imports System
Imports System.Runtime.CompilerServices
Public Class Help
	Shared Sub New()
		Help.time = DateTime.Now.ToString("MM-dd-HH_mm")
		Help.object_0 = RuntimeHelpers.GetObjectValue(New Object())
		Help.Rand = New Random()
	End Sub

	Public Shared Function getProxy() As String
		Return Convert.ToString(Class1.list_1(Help.Rand.[Next](0, Class1.list_1.Count)))
	End Function

	' Token: 0x0400007F RID: 127
	Public Shared time As String

	' Token: 0x04000080 RID: 128
	Public Shared object_0 As Object

	' Token: 0x04000081 RID: 129
	Public Shared Rand As Random = New Random()

End Class
