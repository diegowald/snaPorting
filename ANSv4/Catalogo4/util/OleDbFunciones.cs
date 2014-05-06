using System;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Catalogo.Funciones
{
    class oleDbFunciones
    {

        //const string m_sMODULENAME_ = "oleDbFunciones";

        internal static System.Data.OleDb.OleDbConnection GetConn(string strConexion)
        {
            System.Data.OleDb.OleDbConnection conn = default(System.Data.OleDb.OleDbConnection);
            conn = new System.Data.OleDb.OleDbConnection(strConexion);
            return conn;
        }

        internal static void Conectar(System.Data.OleDb.OleDbConnection ObjetoDeConexion, string CadenaDeConexion)
        {
            ObjetoDeConexion.ConnectionString = CadenaDeConexion;
            ObjetoDeConexion.Open();
        }

        internal static void Desconectar(System.Data.OleDb.OleDbConnection ObjetoDeConexion)
        {
            ObjetoDeConexion.Close();
        }

        internal static System.Data.DataSet xGetDs(System.Data.OleDb.OleDbConnection conexion, string Tabla, string Condicion = "ALL", string Orden = "NONE", string Campos = "*", string Alcance = "")
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

        internal static System.Data.DataTable xGetDt(System.Data.OleDb.OleDbConnection conexion, string Tabla, string Condicion = "ALL", string Orden = "NONE", string Campos = "*", string Alcance = "")
        {
            string sql = null;
            string sCondicion = "";
            string sOrden = "";
            string sAlcance = "";
            string sCampos = "*";

            System.Data.OleDb.OleDbDataAdapter objAdapter = new System.Data.OleDb.OleDbDataAdapter();
            System.Data.DataTable table = new DataTable("miDataTable");
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;

            if (Alcance.Trim().Length > 0)
                sAlcance = Alcance;
            if (Campos.Trim().Length > 0)
                sCampos = Campos;
            if (Condicion != "ALL")
                sCondicion = " WHERE " + Condicion;
            if (Orden != "NONE")
                sOrden = " ORDER BY " + Orden;

            sql = "SELECT " + sAlcance + sCampos + " FROM " + Tabla + sCondicion + sOrden;

            if (!(conexion.State == ConnectionState.Open))
            {
                conexion.Open();
            }
            
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(sql, conexion);

            if (Global01.TranActiva != null)
            {
                cmd.Transaction = Global01.TranActiva;
            }
            try
            {
                objAdapter.SelectCommand = cmd;
                objAdapter.Fill(table);      
            }
            catch (Exception ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;  //throw new Exception(ex.Message.ToString());
            }

            return table;
        }

        internal static System.Data.OleDb.OleDbDataReader xGetDr(System.Data.OleDb.OleDbConnection conexion, string Tabla, string Condicion = "ALL", string Orden = "NONE", string Campos = "*", string Alcance = "")
        {

            System.Data.OleDb.OleDbDataReader dr = null;
 
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

            if (Condicion == "@@identity")
            {
                sql = "SELECT  @@identity as ID FROM " + Tabla;
            }
            else
            {
                sql = "SELECT " + sAlcance + sCampos + " FROM " + Tabla + sCondicion + sOrden;
            };            

            if (!(conexion.State == ConnectionState.Open)) 
            { 
                conexion.Open(); 
            }
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(sql, conexion);

            if (Global01.TranActiva != null)
            {
                cmd.Transaction = Global01.TranActiva;
            }
            try
            {
                dr =cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;  //throw new Exception(ex.Message.ToString());
            }

            return dr;
            
        }

        internal static System.Data.OleDb.OleDbDataReader Comando(System.Data.OleDb.OleDbConnection conexion, string TextoComando)
        {

            System.Data.OleDb.OleDbDataReader dr = null;

            if (!(conexion.State == ConnectionState.Open)) { conexion.Open(); }

            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(TextoComando,conexion);

            if (Global01.TranActiva != null)
            {
                cmd.Transaction = Global01.TranActiva;
            }
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);
                throw new Exception(ex.Message.ToString());
            }

            return dr;

        }

        internal static string Comando(System.Data.OleDb.OleDbConnection conexion, string TextoComando, string Campos, bool Concatena = false)
        {

            System.Data.OleDb.OleDbDataReader dr = null;

            string sResultado = "";

            if (!(conexion.State == ConnectionState.Open)) { conexion.Open(); }

            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(TextoComando, conexion);

            if (Global01.TranActiva != null)
            {
                cmd.Transaction = Global01.TranActiva;
            }
            try
            {
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    sResultado = (DBNull.Value.Equals(dr[Campos].ToString()) ? "" : dr[Campos].ToString().Trim());
                }
            }
            catch (Exception ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);
                throw new Exception(ex.Message.ToString());
            }

            return sResultado;
        }


        internal static void ComandoIU(System.Data.OleDb.OleDbConnection conexion, string TextoComando)
        {
            
            //const string PROCNAME_ = "ComandoIU";
         
            if (!(conexion.State == ConnectionState.Open)) 
            { 
                conexion.Open();
            }
                   
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(TextoComando, conexion);

            if (Global01.TranActiva != null)
            {
                cmd.Transaction = Global01.TranActiva;
            }
            try 
            {
                cmd.ExecuteNonQuery();

                //if (cmd.Transaction != null)
                //{
                //    cmd.Transaction.Commit();
                //}
            }
            catch (Exception ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex; //new Exception(ex.Message.ToString() + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
            }

        }

        internal static void CambiarLinks(string db)
        {
            //const string PROCNAME_ = "CambiarLinks";

            try
            {

                ADODB.Connection adoCN = new ADODB.Connection();

                adoDbConectar(adoCN, Global01.strConexionAd);

                ProcesarTablasLinks(adoCN, db);
                adoDbDesconectar(adoCN);
                adoCN = null;

            }
            catch (Exception ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex; // new Exception(ex.Message.ToString() + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
            }
        }

        private static void ProcesarTablasLinks(ADODB.Connection Conexion, string db)
        {
	     
	        //const string PROCNAME_ = "ProcesarTablasLinks";

            try
            {
	            ADOX.Catalog Adox_Cat = null;
     
	            Adox_Cat = new ADOX.Catalog();
	            Adox_Cat.ActiveConnection = Conexion;

	            foreach (  ADOX.Table Adox_Tbl in Adox_Cat.Tables) {

                    if (db.ToLower().StartsWith("copiacata"))
                    {
                        if (Adox_Tbl.Type == "LINK" & Adox_Tbl.Name.ToLower().StartsWith("bkp"))
                        {
				            Adox_Tbl.Properties["Jet OLEDB:Link Datasource"].Value = Global01.AppPath  + "\\datos\\" + db;
			            }
                    }
                    else if (db.ToLower() == "ans.mdb")
                    {
                        if (Adox_Tbl.Type == "LINK" & !Adox_Tbl.Name.ToLower().StartsWith("bkp"))
                        {
                            Adox_Tbl.Properties["Jet OLEDB:Link Datasource"].Value = Global01.AppPath + "\\datos\\ans.mdb";
			            }
		            }
	            }

	            Adox_Cat = null;
            }
            catch (System.IO.IOException ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);

                //throw new Exception(e.Message + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
                throw ex;
            }
            catch (Exception ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex;
            }
        }

        private static void adoDbConectar(ADODB.Connection ObjetoDeConexion, string CadenaDeConexion)
        {
            ObjetoDeConexion.ConnectionString = CadenaDeConexion;
            ObjetoDeConexion.ConnectionTimeout = 30;
            ObjetoDeConexion.Open();
        }


        private static void adoDbDesconectar(ADODB.Connection ObjetoDeConexion)
        {
            ObjetoDeConexion.Close();
        }


        public static void CompactDatabase(string dataBase)
        {

            //const string PROCNAME_ = "CompactDatabase";

            try
            {
                JRO.JetEngine JRO = new JRO.JetEngine();
                
                string dbNewBakup = Catalogo.Global01.dstring.ToLower().Replace(".mdb", "_") + DateTime.Now.ToString( "yyyyMMdd_HHmmss") + ".tmp";

                System.IO.File.Move(Catalogo.Global01.dstring, dbNewBakup);

                System.IO.File.Delete(Catalogo.Global01.dstring);

                string sDbFrom = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dbNewBakup +  Global01.up2014Ad + ";jet oledb:system database=" + Global01.sstring;

                string sDbTo = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Catalogo.Global01.dstring + Global01.up2014Ad + ";Jet oledb:engine type=5;jet oledb:system database=" + Global01.sstring;

                JRO.CompactDatabase(sDbFrom,sDbTo);

                System.IO.File.Delete(dbNewBakup);
            }
            catch (System.IO.IOException ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);
                //throw new Exception(e.Message + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
                //throw ex;               
            }
            catch (Exception ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);

                //throw ex;
            }
        }


        internal static void CambiarQuery(string pQueryNombre, string pQueryComando)
        {
            
            //const string PROCNAME_ = "CambiarQuery";

            try
            {
                ADODB.Connection adoCN = new ADODB.Connection();

                adoDbConectar(adoCN, Global01.strConexionAd);

                //-- acá va el codigo del cambio de la consulta -----
                ADODB.Command adoCMD = new ADODB.Command();
                ADOX.Catalog Adox_Cat = new ADOX.Catalog();

                adoCMD.CommandText = pQueryComando;

                Adox_Cat.ActiveConnection = adoCN;
                Adox_Cat.Views.Delete(pQueryNombre);
                Adox_Cat.Views.Append(pQueryNombre, adoCMD);

                Adox_Cat = null;
                adoCMD = null;
                //-- fin cambio consulta ----------------------------

                adoDbDesconectar(adoCN);
                adoCN = null;
            }
            catch (System.IO.IOException ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);

                //throw new Exception(e.Message + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
                throw ex;
            }
            catch (Exception ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex;
            }
        }


        internal static void Cambiar_usp(string pQueryNombre, string pQueryComando)
        {
            //const string PROCNAME_ = "Cambiar_usp";
            
            try
            {
                ADODB.Connection adoCN = new ADODB.Connection();

                adoDbConectar(adoCN, Global01.strConexionAd);

                //-- acá va el codigo del cambio de la consulta -----
                ADODB.Command adoCMD = new ADODB.Command();
                ADOX.Catalog Adox_Cat = new ADOX.Catalog();

                adoCMD.CommandText = pQueryComando;

                Adox_Cat.ActiveConnection = adoCN;
                Adox_Cat.Procedures.Delete(pQueryNombre);
                Adox_Cat.Procedures.Append(pQueryNombre, adoCMD);

                Adox_Cat = null;
                adoCMD = null;
                //-- fin cambio consulta ----------------------------

                adoDbDesconectar(adoCN);
                adoCN = null;
            }
            catch (System.IO.IOException ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);

                //throw new Exception(e.Message + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
                throw ex;
            }
            catch (Exception ex)
            {
                Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex;
            }
          }
      
    }
}
