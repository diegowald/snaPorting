Option Explicit On

Public Class Auditor

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "clsAuditor"

    Private Conexion1 As System.Data.OleDb.OleDbConnection

    Public Sub Guardar(ByVal Objeto As Enumerados.ObjetosAuditados, _
                       ByVal Accion As AccionesAuditadas, _
                       Optional ByVal Detalle As String = "")

        Dim adoCMD As New System.Data.OleDb.OleDbCommand

        If vg.AuditarProceso Then
            If Not (ValidarConexion()) Then Exit Sub

            On Error GoTo ErrorHandler
            adoCMD.Parameters.Add("pDetalle", OleDb.OleDbType.VarChar, 128).Value = CStr(Objeto) & "|" & CStr(Accion) & "|" & Detalle

            adoCMD.Connection = Conexion1
            adoCMD.CommandType = CommandType.StoredProcedure
            adoCMD.CommandText = "usp_Auditor_add"
            adoCMD.ExecuteNonQuery()

            adoCMD.Parameters.RemoveAt("pDetalle")

            adoCMD = Nothing
        End If
        Exit Sub

ErrorHandler:
        adoCMD = Nothing
        Err.Raise(Err.Number, Err.Source, Err.Description)
    End Sub

    Public WriteOnly Property Conexion() As System.Data.OleDb.OleDbConnection
        Set(ByVal value As System.Data.OleDb.OleDbConnection)
            Conexion1 = value
        End Set
    End Property

    Private Function ValidarConexion() As Boolean
        Return Not (Conexion1 Is Nothing)
    End Function


End Class
