using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Data;
using System.Data.OleDb;

namespace Catalogo.Funciones
{

    public class adoModulo
    {

        public static System.Data.OleDb.OleDbConnection GetConn(string Conexion)
        {

            System.Data.OleDb.OleDbConnection conn = null;

            conn = new System.Data.OleDb.OleDbConnection(Conexion);

            return conn;

        }

        public static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string NomPrm1)
        {

            OleDbDataAdapter objAdapter = new OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm1, OleDbType.VarChar, 400)).Value = Prm1;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        public static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string NomPrm1, string NomPrm2)
        {

            OleDbDataAdapter objAdapter = new OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm1, OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm2, OleDbType.TinyInt)).Value = Prm2;

            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        public static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string Prm3, string Prm4, string NomPrm1, string NomPrm2, string NomPrm3, string NomPrm4)
        {


            OleDbDataAdapter objAdapter = new OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm1, OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm2, OleDbType.VarChar, 100)).Value = Prm2;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm3, OleDbType.VarChar, 100)).Value = Prm3;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm4, OleDbType.VarChar, 100)).Value = Prm4;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        public static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string Prm3, string NomPrm1, string NomPrm2, string NomPrm3)
        {

            OleDbDataAdapter objAdapter = new OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm1, OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm2, OleDbType.VarChar, 100)).Value = Prm2;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm3, OleDbType.VarChar, 100)).Value = Prm3;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        public static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string Prm3, string Prm4, string Prm5, string NomPrm1, string NomPrm2, string NomPrm3,
        string NomPrm4, string NomPrm5)
        {

            OleDbDataAdapter objAdapter = new OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm1, OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm2, OleDbType.VarChar, 100)).Value = Prm2;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm3, OleDbType.VarChar, 100)).Value = Prm3;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm4, OleDbType.VarChar, 100)).Value = Prm4;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm5, OleDbType.VarChar, 100)).Value = Prm5;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        public static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string Prm3, string Prm4, string Prm5, string Prm6, string NomPrm1, string NomPrm2,
        string NomPrm3, string NomPrm4, string NomPrm5, string NomPrm6)
        {

            OleDbDataAdapter objAdapter = new OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm1, OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm2, OleDbType.VarChar, 100)).Value = Prm2;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm3, OleDbType.VarChar, 100)).Value = Prm3;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm4, OleDbType.VarChar, 100)).Value = Prm4;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm5, OleDbType.VarChar, 100)).Value = Prm5;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm6, OleDbType.VarChar, 100)).Value = Prm6;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        public static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string Prm3, string Prm4, string Prm5, string Prm6, string Prm7, string NomPrm1,
        string NomPrm2, string NomPrm3, string NomPrm4, string NomPrm5, string NomPrm6, string NomPrm7)
        {

            OleDbDataAdapter objAdapter = new OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm1, OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm2, OleDbType.VarChar, 100)).Value = Prm2;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm3, OleDbType.VarChar, 100)).Value = Prm3;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm4, OleDbType.VarChar, 100)).Value = Prm4;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm5, OleDbType.VarChar, 100)).Value = Prm5;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm6, OleDbType.VarChar, 100)).Value = Prm6;
            objAdapter.SelectCommand.Parameters.Add(new OleDbParameter(NomPrm7, OleDbType.VarChar, 100)).Value = Prm7;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        public static System.Data.DataSet xGetDS(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Condicion, string Orden)
        {

            OleDbDataAdapter objAdapter = new OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new OleDbDataAdapter("usp_getRS", conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand.Parameters.AddWithValue("@Tabla", Tabla);
            objAdapter.SelectCommand.Parameters.AddWithValue("@Condicion", Condicion);
            objAdapter.SelectCommand.Parameters.AddWithValue("@Orden", Orden);
            objAdapter.Fill(objDs, Tabla);

            return objDs;

        }

        public static OleDbDataReader xGetDR(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Condicion, string Orden)
        {

            OleDbCommand cmd = new OleDbCommand();

            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandText = "usp_getRS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Tabla", Tabla);
            cmd.Parameters.AddWithValue("@Condicion", Condicion);
            cmd.Parameters.AddWithValue("@Orden", Orden);



            return cmd.ExecuteReader();

        }


        public static void adoComandoIU(string strConexion, string pComando)
        {
            OleDbConnection conexion = new OleDbConnection(strConexion);
            conexion.Open();

            OleDbCommand cmd = new OleDbCommand(pComando, conexion);
            cmd.CommandType = CommandType.Text;

            cmd.ExecuteNonQuery();

        }


        public static void adoComandoIU(ref System.Data.OleDb.OleDbConnection conexion, string pComando)
        {
            OleDbCommand cmd = new OleDbCommand();

            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandText = pComando;
            cmd.CommandType = CommandType.Text;

            cmd.ExecuteNonQuery();

        }

        public static OleDbDataReader adoComando(ref System.Data.OleDb.OleDbConnection conexion, string TextoComando)
        {

            OleDbCommand cmd = new OleDbCommand();

            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandText = TextoComando;
            cmd.CommandType = CommandType.Text;

            return cmd.ExecuteReader();

        }

        public static void adoConectar(ref System.Data.OleDb.OleDbConnection ObjetoDeConexion, string CadenaDeConexion)
        {
            ObjetoDeConexion.ConnectionString = CadenaDeConexion;
            ObjetoDeConexion.Open();

            //.ConnectionTimeout = 30
            //.CursorLocation = adUseClient
            //.Properties("Prompt") = adPromptNever

        }


        public static void adoDesconectar(ref System.Data.OleDb.OleDbConnection ObjetoDeConexion)
        {
            ObjetoDeConexion.Close();
        }

    }

}