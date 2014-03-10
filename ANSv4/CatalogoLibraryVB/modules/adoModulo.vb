Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Data

Namespace Funciones

    Friend Class adoModulo

        Public Shared Function GetConn(ByVal Conexion As String) As System.Data.OleDb.OleDbConnection

            Dim conn As System.Data.OleDb.OleDbConnection

            conn = New System.Data.OleDb.OleDbConnection(Conexion)

            Return conn

        End Function

        Public Shared Function xGetDS_usp(ByRef conexion As System.Data.OleDb.OleDbConnection, ByVal Tabla As String, ByVal Prm1 As String, ByVal NomPrm1 As String) As System.Data.DataSet

            Dim objAdapter As New OleDbDataAdapter
            Dim objDs As New DataSet

            objAdapter = New OleDbDataAdapter(Tabla, conexion)
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm1, SqlDbType.NChar, 400)).Value = Prm1
            objAdapter.Fill(objDs, "dataset")

            Return objDs

        End Function

        Public Shared Function xGetDS_usp(ByRef conexion As System.Data.OleDb.OleDbConnection, ByVal Tabla As String, ByVal Prm1 As String, ByVal Prm2 As String, ByVal NomPrm1 As String, ByVal NomPrm2 As String) As System.Data.DataSet

            Dim objAdapter As New OleDbDataAdapter
            Dim objDs As New DataSet

            objAdapter = New OleDbDataAdapter(Tabla, conexion)
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm1, SqlDbType.NVarChar, 100)).Value = Prm1
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm2, SqlDbType.TinyInt)).Value = Prm2

            objAdapter.Fill(objDs, "dataset")

            Return objDs

        End Function

        Public Shared Function xGetDS_usp(ByRef conexion As System.Data.OleDb.OleDbConnection, ByVal Tabla As String, ByVal Prm1 As String, ByVal Prm2 As String, _
                   ByVal Prm3 As String, ByVal Prm4 As String, _
                   ByVal NomPrm1 As String, ByVal NomPrm2 As String, _
                   ByVal NomPrm3 As String, ByVal NomPrm4 As String) As System.Data.DataSet


            Dim objAdapter As New OleDbDataAdapter
            Dim objDs As New DataSet

            objAdapter = New OleDbDataAdapter(Tabla, conexion)
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm1, SqlDbType.NVarChar, 100)).Value = Prm1
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm2, SqlDbType.NVarChar, 100)).Value = Prm2
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm3, SqlDbType.NVarChar, 100)).Value = Prm3
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm4, SqlDbType.NVarChar, 100)).Value = Prm4
            objAdapter.Fill(objDs, "dataset")

            Return objDs

        End Function

        Public Shared Function xGetDS_usp(ByRef conexion As System.Data.OleDb.OleDbConnection, ByVal Tabla As String, ByVal Prm1 As String, ByVal Prm2 As String, _
                                           ByVal Prm3 As String, _
                                           ByVal NomPrm1 As String, ByVal NomPrm2 As String, _
                                           ByVal NomPrm3 As String) As System.Data.DataSet

            Dim objAdapter As New OleDbDataAdapter
            Dim objDs As New DataSet

            objAdapter = New OleDbDataAdapter(Tabla, conexion)
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm1, SqlDbType.NVarChar, 100)).Value = Prm1
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm2, SqlDbType.NVarChar, 100)).Value = Prm2
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm3, SqlDbType.NVarChar, 100)).Value = Prm3
            objAdapter.Fill(objDs, "dataset")

            Return objDs

        End Function

        Public Shared Function xGetDS_usp(ByRef conexion As System.Data.OleDb.OleDbConnection, ByVal Tabla As String, ByVal Prm1 As String, ByVal Prm2 As String, _
                                          ByVal Prm3 As String, ByVal Prm4 As String, _
                                          ByVal Prm5 As String, _
                                          ByVal NomPrm1 As String, ByVal NomPrm2 As String, _
                                          ByVal NomPrm3 As String, ByVal NomPrm4 As String, ByVal NomPrm5 As String) As System.Data.DataSet

            Dim objAdapter As New OleDbDataAdapter
            Dim objDs As New DataSet

            objAdapter = New OleDbDataAdapter(Tabla, conexion)
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm1, SqlDbType.NVarChar, 100)).Value = Prm1
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm2, SqlDbType.NVarChar, 100)).Value = Prm2
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm3, SqlDbType.NVarChar, 100)).Value = Prm3
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm4, SqlDbType.NVarChar, 100)).Value = Prm4
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm5, SqlDbType.NVarChar, 100)).Value = Prm5
            objAdapter.Fill(objDs, "dataset")

            Return objDs

        End Function

        Public Shared Function xGetDS_usp(ByRef conexion As System.Data.OleDb.OleDbConnection, ByVal Tabla As String, ByVal Prm1 As String, ByVal Prm2 As String, _
                                        ByVal Prm3 As String, ByVal Prm4 As String, _
                                        ByVal Prm5 As String, ByVal Prm6 As String, _
                                        ByVal NomPrm1 As String, ByVal NomPrm2 As String, _
                                        ByVal NomPrm3 As String, ByVal NomPrm4 As String, _
                                        ByVal NomPrm5 As String, ByVal NomPrm6 As String) As System.Data.DataSet

            Dim objAdapter As New OleDbDataAdapter
            Dim objDs As New DataSet

            objAdapter = New OleDbDataAdapter(Tabla, conexion)
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm1, SqlDbType.NVarChar, 100)).Value = Prm1
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm2, SqlDbType.NVarChar, 100)).Value = Prm2
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm3, SqlDbType.NVarChar, 100)).Value = Prm3
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm4, SqlDbType.NVarChar, 100)).Value = Prm4
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm5, SqlDbType.NVarChar, 100)).Value = Prm5
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm6, SqlDbType.NVarChar, 100)).Value = Prm6
            objAdapter.Fill(objDs, "dataset")

            Return objDs

        End Function

        Public Shared Function xGetDS_usp(ByRef conexion As System.Data.OleDb.OleDbConnection, ByVal Tabla As String, _
                                        ByVal Prm1 As String, ByVal Prm2 As String, _
                                        ByVal Prm3 As String, ByVal Prm4 As String, _
                                        ByVal Prm5 As String, ByVal Prm6 As String, ByVal Prm7 As String, _
                                        ByVal NomPrm1 As String, ByVal NomPrm2 As String, _
                                        ByVal NomPrm3 As String, ByVal NomPrm4 As String, _
                                        ByVal NomPrm5 As String, ByVal NomPrm6 As String, ByVal NomPrm7 As String) As System.Data.DataSet

            Dim objAdapter As New OleDbDataAdapter
            Dim objDs As New DataSet

            objAdapter = New OleDbDataAdapter(Tabla, conexion)
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure

            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm1, SqlDbType.NVarChar, 100)).Value = Prm1
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm2, SqlDbType.NVarChar, 100)).Value = Prm2
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm3, SqlDbType.NVarChar, 100)).Value = Prm3
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm4, SqlDbType.NVarChar, 100)).Value = Prm4
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm5, SqlDbType.NVarChar, 100)).Value = Prm5
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm6, SqlDbType.NVarChar, 100)).Value = Prm6
            objAdapter.SelectCommand.Parameters.Add(New OleDbParameter(NomPrm7, SqlDbType.NVarChar, 100)).Value = Prm7
            objAdapter.Fill(objDs, "dataset")

            Return objDs

        End Function

        Public Shared Function xGetDS(ByRef conexion As System.Data.OleDb.OleDbConnection, ByVal Tabla As String, ByVal Condicion As String, ByVal Orden As String) As System.Data.DataSet

            Dim objAdapter As New OleDbDataAdapter
            Dim objDs As New DataSet

            objAdapter = New OleDbDataAdapter("usp_getRS", conexion)
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure
            objAdapter.SelectCommand.Parameters.AddWithValue("@Tabla", Tabla)
            objAdapter.SelectCommand.Parameters.AddWithValue("@Condicion", Condicion)
            objAdapter.SelectCommand.Parameters.AddWithValue("@Orden", Orden)
            objAdapter.Fill(objDs, Tabla)

            Return objDs

        End Function

        Public Shared Function xGetDR(ByRef conexion As System.Data.OleDb.OleDbConnection, ByVal Tabla As String, ByVal Condicion As String, ByVal Orden As String) As OleDbDataReader

            Dim cmd As New OleDbCommand

            conexion.Open()

            cmd.Connection = conexion
            cmd.CommandText = "usp_getRS"
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@Tabla", Tabla)
            cmd.Parameters.AddWithValue("@Condicion", Condicion)
            cmd.Parameters.AddWithValue("@Orden", Orden)



            Return cmd.ExecuteReader()

        End Function

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

        Public Shared Sub adoDbConectar(ByRef ObjetoDeConexion As ADODB.Connection, ByVal CadenaDeConexion As String)

            ObjetoDeConexion.ConnectionString = CadenaDeConexion
            ObjetoDeConexion.ConnectionTimeout = 30
            'ObjetoDeConexion.CursorLocation = adUseClient
            'ObjetoDeConexion.Properties("Prompt") = adPromptNever
            ObjetoDeConexion.Open()

        End Sub

        Public Shared Sub adoConectar(ByRef ObjetoDeConexion As System.Data.OleDb.OleDbConnection, ByVal CadenaDeConexion As String)

            ObjetoDeConexion.ConnectionString = CadenaDeConexion
            ObjetoDeConexion.Open()

            '.ConnectionTimeout = 30
            '.CursorLocation = adUseClient
            '.Properties("Prompt") = adPromptNever

        End Sub

        Public Shared Sub adoDbDesconectar(ByRef ObjetoDeConexion As ADODB.Connection)
            ObjetoDeConexion.Close()
        End Sub

        Public Shared Sub adoDesconectar(ByRef ObjetoDeConexion As System.Data.OleDb.OleDbConnection)
            ObjetoDeConexion.Close()
        End Sub

        Public Shared Sub CompactDatabase()

            On Error GoTo CompactErr

            Dim JRO As New JRO.JetEngine
            Dim dbNewBakup As String = Replace(dstring, ".mdb", "_") & Format(Now(), "yyyyMMdd HHmmss") & ".mdb"

            'Application.DoEvents()

            Rename(dstring, dbNewBakup)

            Kill(dstring)

            JRO.CompactDatabase("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dbNewBakup, _
            "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & dstring & ";Jet OLEDB:Engine Type=5")

            'Application.DoEvents()

            Kill(dbNewBakup)

            Exit Sub

CompactErr:
            Err.Raise(Err.Number, Err.Source, Err.Description)

        End Sub


    End Class

End Namespace
