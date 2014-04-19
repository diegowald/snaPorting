using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo._application
{
    class UpdateAppConfig
    {
        //Public Event SincronizarAppProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)

        private AppConfigWS.appConfig cliente;
        private bool webServiceInicializado;
        private string _macAddress;
        private string _ipAddress;

        private System.Data.OleDb.OleDbConnection conexion;

        public UpdateAppConfig(string MacAddress, string ipAddress, string ipAddressIntranet)
        {
            inicializar(MacAddress, ipAddress, ipAddressIntranet);
            conexion = Global01.Conexion;
        }

        public bool Inicializado
        {
            get
            {
                return webServiceInicializado;
            }
        }

        protected void inicializar(string MacAddress, string ipAddress, string ipAddressIntranet)
        {
            bool conectado = util.SimplePing.ping(ipAddress, 5000, 0);

            if (!conectado)
            {
                conectado = util.SimplePing.ping(ipAddressIntranet, 5000, 0);
            }
            try
            {
                if (!webServiceInicializado)
                {
                    if (conectado)
                    {
                        cliente = new AppConfigWS.appConfig();
                        cliente.Url = "http://" + ipAddress + "/wsCatalogo4/appConfig.asmx?wsdl";
                        if (Global01.proxyServerAddress != "0.0.0.0")
                        {
                            cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                        }
                        _macAddress = MacAddress;
                        _ipAddress = ipAddress;
                        webServiceInicializado = true;
                    }
                    else
                    {
                        webServiceInicializado = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (System.Runtime.InteropServices.Marshal.GetExceptionCode() == -2147024809)
                {
                    cliente = new AppConfigWS.appConfig();
                    cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/appConfig.asmx?wsdl";
                    if (Global01.proxyServerAddress != "0.0.0.0")
                    {
                        cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                    }
                    _macAddress = MacAddress;
                    webServiceInicializado = true;
                    _ipAddress = ipAddressIntranet;
                }
                else
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);

                    throw ex;
                }
            }
        }


        private void sincroAppConfigCompletada(ref bool cancel)
        {
            if (!util.SimplePing.ping(_ipAddress, 5000, 0))
            {
                // Conexion no valida
                cancel = true;
                return;
            }

            cliente.SincronizacionAppConfigCompletada(_macAddress);
        }

        public void sincronizarApp()
        {
            bool cancel = false;

//RaiseEvent SincronizarAppProgress("Iniciando Sincronización App Config", 0, Cancel)
            if (!webServiceInicializado)
            {
                cancel = true;
            }

            if (!cancel)
            {
//RaiseEvent SincronizarAppProgress("Sincronizando App Config", 40, Cancel)
            }

            if (!cancel)
            {
                obtenerInfo(ref cancel);
            }

            if (!cancel)
            {
//RaiseEvent SincronizarAppProgress("Sincronizando comandos", 60, Cancel)
            }

            if (!cancel)
            {
                obtenerComandos(ref cancel);
            }

            if (!cancel)
            {
//RaiseEvent SincronizarAppProgress("Sincronizando App Config", 75, Cancel)
            }

            if (!cancel)
            {
                if (tenerQueEnviarInfo())
                {
                    System.Data.OleDb.OleDbDataReader reader = Funciones.oleDbFunciones.Comando(conexion, "SELECT TOP 1 * FROM v_appConfig2");

                    string auditarProceso;
                    string enviarAuditoria;

                    reader.Read();
                    enviarAuditoria = ((bool)reader["EnviarAuditoria"] ? "1" : "0");
                    auditarProceso = ((bool)reader["Auditor"] ? "1" : "0");

                    cancel = !enviarInfo(reader["Cuit"].ToString(),
                        reader["RazonSocial"].ToString(),
                        reader["ApellidoNombre"].ToString(),
                        reader["Domicilio"].ToString(),
                        reader["Telefono"].ToString(),
                        reader["Ciudad"].ToString(),
                        reader["Email"].ToString(),
                        reader["IDans"].ToString(),
                        reader["appCaduca"].ToString(),
                        reader["dbCaduca"].ToString(),
                        reader["PIN"].ToString(),
                        reader["FechaUltimoAcceso"].ToString(),
                        reader["Mensaje"].ToString(),
                        enviarAuditoria,
                        reader["Url"].ToString(),
                        reader["Modem"].ToString(),
                        reader["appCVersion"].ToString(),
                        reader["Build"].ToString(),
                        reader["appCListaPrecio"].ToString(),
                        auditarProceso);
                }
            }

            if (!cancel)
            {
//RaiseEvent SincronizarAppProgress("Fin de la sincronizacion de App Config", 100, Cancel)
            }

            if (!cancel)
            {
                sincroAppConfigCompletada(ref cancel);
            }
        }

        private bool tenerQueEnviarInfo()
        {
            if (!util.SimplePing.ping(_ipAddress, 5000, 0))
            {
                return false;
            }

            long resultado = cliente.TenerQueEnviarInfo(_macAddress);
            return resultado == 1;
        }

        protected bool enviarInfo(string Cuit, string RazonSocial,
            string ApellidoNombre, string Domicilio,
            string Telefono, string Ciudad,
            string Email, string IDAns,
            string appCaduca, string dbCaduca, string PIN,
            string FechaUltimoAcceso, string Mensaje,
            string EnviarAuditoria, string Url, string Modem,
            string Version, string build, string ListaPrecio,
            string auditor)
        {
            if (!util.SimplePing.ping(_ipAddress, 5000, 0))
            {
                return false;
            }

            long resultado = cliente.EnviarInfo(_macAddress,
                Cuit, RazonSocial, ApellidoNombre,
                Domicilio, Telefono, Ciudad,
                Email, IDAns, appCaduca,
                dbCaduca, PIN, FechaUltimoAcceso,
                Mensaje, EnviarAuditoria, Url, Modem,
                Version, build, ListaPrecio, auditor);

            return resultado == 0;
        }

        protected void obtenerInfo(ref bool cancel)
        {
            if (!util.SimplePing.ping(_ipAddress, 5000, 0))
            {
                // Conexion no valida
                cancel = true;
                return;
            }
            System.Data.DataSet ds = cliente.ObtenerInfoDS(_macAddress);

            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // Aca va la funcion de actualizacion
                        System.Data.DataRow row = ds.Tables[0].Rows[i];
                        if ((row["Campo"].ToString().Trim().Length > 0) && (row["Valor"].ToString().Trim().Length > 0))
                        {
                            string tipo = row["Tipo"].ToString().Trim().Substring(0,1);
                            if (tipo == "2") // Actualizo ansAppConfig
                            {
                                switch (tipo)
                                {
                                    case "N": // Numerico
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE ansConfig SET " + row["Campo"].ToString().Trim() + "=" + row["valor"].ToString().Trim());
                                        break;
                                    case "C": // Char
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE ansConfig SET " + row["Campo"].ToString().Trim() + "='" + row["valor"].ToString().Trim() + "'");
                                        break;
                                    case "R": // Real
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE ansConfig SET " + row["Campo"].ToString().Trim() + "='" + row["valor"].ToString().Trim() + "'");
                                        break;
                                    case "D": // Date
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE ansConfig SET " + row["Campo"].ToString().Trim() + "=#" + row["valor"].ToString().Substring(0, 10) + "#");
                                        break;
                                    case "X": // Nulo
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE ansConfig SET " + row["Campo"].ToString() + "=''");
                                        break;
                                    case "Y":
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE ansConfig SET ListaPrecio=" + row["valor"].ToString());
                                        if (row["valor"].ToString().Trim() == "2")
                                        {
                                            Funciones.oleDbFunciones.ComandoIU(conexion, "EXEC usp_Precio_upd");
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                switch (row["Tipo"].ToString().Trim())
                                {
                                    case "N": // Numerico
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE AppConfig SET " + row["Campo"].ToString().Trim() + "=" + row["valor"].ToString());
                                        break;
                                    case "C": // Char
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE AppConfig SET " + row["Campo"].ToString().Trim() + "='" + row["valor"].ToString() + "'");
                                        break;
                                    case "R": // Real
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE AppConfig SET " + row["Campo"].ToString().Trim() + "='" + row["valor"].ToString() + "'");
                                        break;
                                    case "D": // Date
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE AppConfig SET " + row["Campo"].ToString().Trim() + "=#" + row["valor"].ToString().Substring(0, 10) + "#");
                                        break;
                                    case "X": // Nulo
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE AppConfig SET " + row["Campo"].ToString() + "=''");
                                        break;
                                    case "Y":
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "UPDATE AppConfig SET ListaPrecio=" + row["valor"].ToString());
                                        if (row["valor"].ToString().Trim() == "2")
                                        {
                                            Funciones.oleDbFunciones.ComandoIU(conexion, "EXEC usp_Precio_upd");
                                        }
                                        break;
                                    case "W": // cambia el nro del ultimo Pedido,Recibo,Devolucion
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "EXECUTE " + row["Campo"].ToString().Trim() + " '" + row["valor"].ToString().Trim() + "'");
                                        break;
                                    case "V": // ejecuta cualquier consulta de la db SIN parametros
                                        Funciones.oleDbFunciones.ComandoIU(conexion, "EXECUTE " + row["campo"].ToString().Trim());
                                        break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);

                    //throw ex;  //util.errorHandling.ErrorForm.show();
                }
            }
        }

        public void obtenerComandos(ref bool cancel)
        {  
            if (!util.SimplePing.ping(_ipAddress, 5000, 0))
            {
                // Conexion no valida
                cancel = true;
                return;
            }

            bool wSalir = false;

            System.Data.DataSet ds = cliente.ObtenerComandosDS(_macAddress);

            if (ds.Tables[0].Rows.Count > 0)
            {
                try
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        // Aca la funcion de ejecucion de los comandos....
                        System.Data.DataRow row = ds.Tables[0].Rows[i];
                        string comando = row["Comando"].ToString();
                        if (comando.Trim().Length > 0)
                        {
                            switch (comando.Substring(0, 2))
                            {
                                case "sh":
                                    {
                                        System.Diagnostics.Process p = new System.Diagnostics.Process();
                                        p.StartInfo.FileName = row["Comando"].ToString().Substring(3);
                                        p.StartInfo.UseShellExecute = true;
                                        p.Start();
                                        p.WaitForExit();
                                    }
                                    break;
                                case "sw": // write key setting.ini
                                    {
                                        string[] sComando = row["Comando"].ToString().Substring(3).Split(new string[] {"//"}, StringSplitOptions.None);
                                        Funciones.modINIs.WriteINI(sComando[0], sComando[1], sComando[2]);
                                        if (sComando[1]=="mdb") wSalir=true;
                                    }
                                    break;
                                case "sd": // delete key setting.ini
                                    {
                                        string[] sComando = row["Comando"].ToString().Substring(3).Split(new string[] { "//" }, StringSplitOptions.None);
                                        Funciones.modINIs.DeleteKeyINI(sComando[0], sComando[1]);
                                }
                                    break;
                                case "db":
                                    Funciones.oleDbFunciones.ComandoIU(conexion, row["Comando"].ToString().Substring(3));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);

                    throw ex;  //util.errorHandling.ErrorForm.show();
                }
            }

            if (wSalir)
            {
                if (Global01.TranActiva != null)
                {
                    Global01.TranActiva.Commit();
                    Global01.TranActiva = null;
                }
                MessageBox.Show("Se han efectuado modificaciones en la aplicación, \n ésta de cerrará, luego re-ingrese nuevamente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Catalogo.MainMod.miEnd();
            }
        }

    }
}
