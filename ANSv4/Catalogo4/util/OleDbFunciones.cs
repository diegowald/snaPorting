using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Catalogo.Funciones
{
    class oleDbFunciones
    {


        internal static System.Data.OleDb.OleDbConnection GetConn(string strConexion)
        {

            System.Data.OleDb.OleDbConnection conn = default(System.Data.OleDb.OleDbConnection);

            conn = new System.Data.OleDb.OleDbConnection(strConexion);

            return conn;

        }

        internal static System.Data.DataSet xGetDS(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Condicion, string Orden)
        {

            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
            DataSet objDs = new DataSet();

            string strComando = "SELECT * FROM " + Tabla;

            if (Condicion != "ALL")
            {
                strComando = strComando + " WHERE " + Condicion;
            }

            if (Orden != "NONE")
            {
                strComando = strComando + " ORDER BY  " + Orden;
            }

            objAdapter = new System.Data.OleDb.OleDbDataAdapter(strComando, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.Text;
            objAdapter.Fill(objDs, Tabla);

            return objDs;

        }


        internal static System.Data.OleDb.OleDbDataReader xGetDR(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Condicion, string Orden)
        {

            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandText = "usp_getRS";
            cmd.CommandType = System.Data.CommandType.StoredProcedure; 
            cmd.Parameters.AddWithValue("@Tabla", Tabla);
            cmd.Parameters.AddWithValue("@Condicion", Condicion);
            cmd.Parameters.AddWithValue("@Orden", Orden);

            return cmd.ExecuteReader();

        }

        internal static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string NomPrm1)
        {

            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new System.Data.OleDb.OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm1, System.Data.OleDb.OleDbType.VarChar, 400)).Value = Prm1;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        internal static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string NomPrm1, string NomPrm2)
        {

            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new System.Data.OleDb.OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm1, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm2, System.Data.OleDb.OleDbType.TinyInt)).Value = Prm2;

            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        internal static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string Prm3, string Prm4, string NomPrm1, string NomPrm2, string NomPrm3, string NomPrm4)
        {


            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new System.Data.OleDb.OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm1, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm2, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm2;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm3, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm3;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm4, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm4;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        internal static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string Prm3, string NomPrm1, string NomPrm2, string NomPrm3)
        {

            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new System.Data.OleDb.OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm1, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm2, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm2;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm3, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm3;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        internal static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string Prm3, string Prm4, string Prm5, string NomPrm1, string NomPrm2, string NomPrm3,
        string NomPrm4, string NomPrm5)
        {

            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new System.Data.OleDb.OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm1, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm2, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm2;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm3, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm3;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm4, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm4;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm5, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm5;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        internal static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string Prm3, string Prm4, string Prm5, string Prm6, string NomPrm1, string NomPrm2,
        string NomPrm3, string NomPrm4, string NomPrm5, string NomPrm6)
        {

            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new System.Data.OleDb.OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm1, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm2, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm2;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm3, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm3;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm4, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm4;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm5, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm5;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm6, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm6;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        internal static System.Data.DataSet xGetDS_usp(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Prm1, string Prm2, string Prm3, string Prm4, string Prm5, string Prm6, string Prm7, string NomPrm1,
        string NomPrm2, string NomPrm3, string NomPrm4, string NomPrm5, string NomPrm6, string NomPrm7)
        {

            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
            DataSet objDs = new DataSet();

            objAdapter = new System.Data.OleDb.OleDbDataAdapter(Tabla, conexion);
            objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;

            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm1, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm1;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm2, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm2;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm3, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm3;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm4, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm4;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm5, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm5;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm6, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm6;
            objAdapter.SelectCommand.Parameters.Add(new System.Data.OleDb.OleDbParameter(NomPrm7, System.Data.OleDb.OleDbType.VarChar, 100)).Value = Prm7;
            objAdapter.Fill(objDs, "dataset");

            return objDs;

        }

        //internal static System.Data.DataSet xGetDS(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Condicion, string Orden)
        //{

        //    System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
        //    DataSet objDs = new DataSet();

        //    objAdapter = new System.Data.OleDb.OleDbDataAdapter("usp_getRS", conexion);
        //    objAdapter.SelectCommand.CommandType = CommandType.StoredProcedure;
        //    objAdapter.SelectCommand.Parameters.AddWithValue("@Tabla", Tabla);
        //    objAdapter.SelectCommand.Parameters.AddWithValue("@Condicion", Condicion);
        //    objAdapter.SelectCommand.Parameters.AddWithValue("@Orden", Orden);
        //    objAdapter.Fill(objDs, Tabla);

        //    return objDs;

        //}

        internal static System.Data.OleDb.OleDbDataReader Comando(ref System.Data.OleDb.OleDbConnection conexion, string TextoComando)
        {

            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandText = TextoComando;
            cmd.CommandType = CommandType.Text;

            return cmd.ExecuteReader();

        }

        internal static void ComandoIU(ref System.Data.OleDb.OleDbConnection conexion, string pComando)
        {
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            conexion.Open();

            cmd.Connection = conexion;
            cmd.CommandText = pComando;
            cmd.CommandType = CommandType.Text;

            cmd.ExecuteNonQuery();

        }


        internal static void Conectar(ref System.Data.OleDb.OleDbConnection ObjetoDeConexion, string CadenaDeConexion)
        {
            ObjetoDeConexion.ConnectionString = CadenaDeConexion;
            ObjetoDeConexion.Open();
        }


        internal static void Desconectar(ref System.Data.OleDb.OleDbConnection ObjetoDeConexion)
        {
            ObjetoDeConexion.Close();
        }

    }
}
