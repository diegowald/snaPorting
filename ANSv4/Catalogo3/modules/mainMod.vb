Module mainMod

    ' VARIABLES GLOBALES
    Public Structure vAPP
        Public miSABOR As LosSabores     ' = SABOR

        Public IDMaquina As String 'vg.misabor & obtenerCRC(WMI)
        Public IDMaquinaCRC As String 'obtenerCRC(vg.IDMaquina)
        Public IDMaquinaREG As String 'obtenerCRC(vg.IDmaquina & vg.IDmaquinaCRC)
        Public LLaveViajante As String
        Public AppActiva As Boolean

        'Public Conexion As ADODB.Connection

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
        Public Modem As String  'Solomon USB Modem
        Public EsGerente As Boolean
        Public LoginSucceeded As Boolean
        Public ActualizarCatalogo As Boolean
        Public ActualizarCuentas As Boolean
        Public RecienRegistrado As Boolean
        Public ErrorD As Boolean
        Public TranActiva As Boolean
        'Public auditor As clsAuditor
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

    Public vg As vAPP

    Public Sub AbortarYA()

        On Error Resume Next


        For Each f As Windows.Forms.Form In My.Application.OpenForms
            f.Close()
        Next

        'If Not (vg.auditor Is Nothing) Then
        '    vg.auditor = Nothing
        'End If

        'If Not (vg.Conexion Is Nothing) Then
        '    vg.Conexion = Nothing
        'End If

        End

    End Sub


End Module
