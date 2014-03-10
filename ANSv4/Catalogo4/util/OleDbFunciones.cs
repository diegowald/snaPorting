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

        internal static void Conectar(ref System.Data.OleDb.OleDbConnection ObjetoDeConexion, string CadenaDeConexion)
        {
            ObjetoDeConexion.ConnectionString = CadenaDeConexion;
            ObjetoDeConexion.Open();
        }

        internal static void Desconectar(ref System.Data.OleDb.OleDbConnection ObjetoDeConexion)
        {
            ObjetoDeConexion.Close();
        }

        internal static System.Data.DataSet xGetDs(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Condicion = "ALL", string Orden = "NONE", string Campos = "*", string Alcance = "")
        {
            string sql = null;
            string sCondicion = "";
            string sOrden = "";
            string sAlcance = "";
            string sCampos = "*";

            if (Alcance.Trim().Length > 0)
                sAlcance = Alcance;
            if (Campos.Trim().Length > 0)
                sCampos = Campos;
            if (Condicion != "ALL")
                sCondicion = " WHERE " + Condicion;
            if (Orden != "NONE")
                sOrden = " ORDER BY " + Orden;

            sql = "SELECT " + sAlcance + sCampos + " FROM " + Tabla + sCondicion + sOrden;

            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter(sql, conexion);

            System.Data.DataSet ObjDS = new System.Data.DataSet();
            objAdapter.Fill(ObjDS, "miDataset");

            return ObjDS;
        }

        internal static System.Data.DataTable xGetDt(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Condicion = "ALL", string Orden = "NONE", string Campos = "*", string Alcance = "")
        {
            string sql = null;
            string sCondicion = "";
            string sOrden = "";
            string sAlcance = "";
            string sCampos = "*";

            if (Alcance.Trim().Length > 0)
                sAlcance = Alcance;
            if (Campos.Trim().Length > 0)
                sCampos = Campos;
            if (Condicion != "ALL")
                sCondicion = " WHERE " + Condicion;
            if (Orden != "NONE")
                sOrden = " ORDER BY " + Orden;

            sql = "SELECT " + sAlcance + sCampos + " FROM " + Tabla + sCondicion + sOrden;

            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter(sql, conexion);

            DataTable table = new DataTable("miDataTable");
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            objAdapter.Fill(table);

            return table;
        }

        internal static System.Data.OleDb.OleDbDataReader xGetDr(ref System.Data.OleDb.OleDbConnection conexion, string Tabla, string Condicion = "ALL", string Orden = "NONE", string Campos = "*", string Alcance = "")
        {

            string sql = null;
            string sCondicion = "";
            string sOrden = "";
            string sAlcance = "";
            string sCampos = "*";

            if (Alcance.Trim().Length > 0)
                sAlcance = Alcance;
            if (Campos.Trim().Length > 0)
                sCampos = Campos;
            if (Condicion != "ALL")
                sCondicion = " WHERE " + Condicion;
            if (Orden != "NONE")
                sOrden = " ORDER BY " + Orden;

            sql = "SELECT " + sAlcance + sCampos + " FROM " + Tabla + sCondicion + sOrden;

            conexion.Open();
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(sql, conexion);

            return cmd.ExecuteReader();

        }

        internal static System.Data.OleDb.OleDbDataReader Comando(ref System.Data.OleDb.OleDbConnection conexion, string TextoComando)
        {
            conexion.Open();
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(TextoComando,conexion);

            return cmd.ExecuteReader();
        }

        internal static void ComandoIU(ref System.Data.OleDb.OleDbConnection conexion, string TextoComando)
        {
            conexion.Open();            
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(TextoComando, conexion);

            cmd.ExecuteNonQuery();
        }

    }
}
