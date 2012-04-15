Imports System.Data.OleDb

Module Module1
    Public Cn As New OleDbConnection("provider=microsoft.jet.oledb.4.0;data source=db1.mdb")
    Public r As New Braa_Class.OleDbNewRs(Cn, "dd", "Code", Braa_Class.OleDbNewRs.NumberOrString.PK_Number)
    Dim tool As New Braa_Class.Bra_Tool
End Module
