Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data

Namespace Funciones

    Friend Class adoModulo

        Public Shared Sub adoComandoIU(ByVal strConexion As String, ByVal pComando As String)

            Dim conexion As New OleDbConnection(strConexion)
            conexion.Open()

            Dim cmd As New OleDbCommand(pComando, conexion)
            cmd.CommandType = CommandType.Text

            cmd.ExecuteNonQuery()

        End Sub

        Public Shared Sub adoComandoIU(ByRef conexion As System.Data.OleDb.OleDbConnection, ByVal pComando As String)

            Dim cmd As New OleDbCommand

            conexion.Open()

            cmd.Connection = conexion
            cmd.CommandText = pComando
            cmd.CommandType = CommandType.Text

            cmd.ExecuteNonQuery()

        End Sub

        Public Shared Function adoComando(ByRef conexion As System.Data.OleDb.OleDbConnection, ByVal TextoComando As String) As OleDbDataReader

            Dim cmd As New OleDbCommand

            conexion.Open()

            cmd.Connection = conexion
            cmd.CommandText = TextoComando
            cmd.CommandType = CommandType.Text

            Return cmd.ExecuteReader()

        End Function

    End Class

End Namespace
