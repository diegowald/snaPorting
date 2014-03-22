Module Global01


    '---------------------------------------------------------------
    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "MainMod"

    Public Const u2String As String = "?"
    Public Const c2String As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\Catalogo ANS\Datos\catalogo.mdb;Persist Security Info=True;Password=video80min;User ID=inVent;Jet OLEDB:System database=C:\Windows\Help\kbappcat.hlp"

    ' VARIABLES GLOBALES
    Public Structure vAPP


        Public IDMaquina As String 'vg.misabor & obtenerCRC(WMI)
        Public IDMaquinaCRC As String 'obtenerCRC(vg.IDMaquina)
        Public IDMaquinaREG As String 'obtenerCRC(vg.IDmaquina & vg.IDmaquinaCRC)
        Public LLaveViajante As String
        Public AppActiva As Boolean

        Public Conexion As System.Data.OleDb.OleDbConnection

        Public Cuit As String
        Public RazonSocial As String
        Public ApellidoNombre As String
        Public Domicilio As String
        Public Telefono As String
        Public Ciudad As String
        Public Email As String
        Public NroUsuario As String
        Public NroPDR As String
        Public NroImprimir As String
        Public dbCaduca As Date
        Public appCaduca As Date
        Public FechaUltimoAcceso As Date
        Public F_ActCatalogo As Date
        Public F_ActClientes As Date
        Public EnviarAuditoria As Boolean
        Public AuditarProceso As Boolean
        Public IpSettingIni As Boolean
        Public EsGerente As Boolean
        Public LoginSucceeded As Boolean
        Public ActualizarCatalogo As Boolean
        Public ActualizarCuentas As Boolean
        Public RecienRegistrado As Boolean
        Public xError As Boolean ' Se tuvo que cambiar el nombre de error a err        
        Public TranActiva As System.Data.OleDb.OleDbTransaction
        Public auditor As Auditor
        Public URL_ANS As String
        Public URL_ANS2 As String
        Public ExploradorCaption As String
        Public ValeCombo As Boolean
        Public NoConn As Boolean
        Public Path As String
        Public MiBuild As Integer
        Public ListaPrecio As Byte
        Public FileBak As String
        Public PathAcrobat As String
        Public Dolar As Single
        Public Euro As Single
    End Structure

    Public cstring As String = ""
    Public qstring As String = ""
    Public dstring As String = ""
    Public sstring As String = ""
    Public ArchCerradura As String = ""
    Public ArchLlave As String = ""

    Public vg As vAPP

End Module
