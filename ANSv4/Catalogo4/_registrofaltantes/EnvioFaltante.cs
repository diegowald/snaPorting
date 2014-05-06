using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace Catalogo._registrofaltantes
{
    public class EnvioFaltante
    {

        // Esta clase realiza una consulta al servidor web y obtiene
        // el numero de ip del servidor privado.

        // Define como se llama este modulo para el control de errores
        
        private RecibosWS.Recibos_v_303 cliente;
       // private FaltantesWS.Faltantes_v_101 cliente;
        private bool webServiceInicializado;
        private string _MacAddress;

        private string _ip;
        private string _NroFaltante;
        private string _CodCliente;
        private string _CodViajante;
        private string _Fecha;
        private string _Observaciones;

        private bool DatosObtenidos;
        private System.Data.OleDb.OleDbConnection Conexion1;

        public EnvioFaltante(System.Data.OleDb.OleDbConnection Conexion, string ipAddress, string MacAddress)
        {
            Inicializar(ipAddress, MacAddress);
            Conexion1 = Conexion;
        }

        public bool Inicializado
        {
            get
            {
                return webServiceInicializado;
            }
        }

        public void obtenerDatos(string nroFaltante)
        {
            DatosObtenidos = false;

            System.Data.OleDb.OleDbDataReader Enc = Funciones.oleDbFunciones.Comando(Conexion1, "EXEC v_Faltante1 '" + nroFaltante + "'");
     
            _NroFaltante = nroFaltante;
            Enc.Read();
            _CodCliente = Enc["IDCliente"].ToString().Trim().PadLeft(6, '0');
            _CodViajante = Enc["IDViajante"].ToString().Trim().PadLeft(6, '0');
            _Fecha = string.Format("{0:yyyyMMdd}", DateTime.Parse(Enc["F_Faltante"].ToString()));
            _Observaciones = Enc["Observaciones"].ToString();

            Enc = null;
            DatosObtenidos = true;

        }

        public long EnviarFaltante()
        {
            bool Cancel = false;
            long resultado = 0;

            if (!webServiceInicializado)
            {
                Cancel = true;
            }

            try
            {
                if (!Cancel)
                {
                    if (Global01.TranActiva == null)
                    {
                        Global01.TranActiva = Conexion1.BeginTransaction();
                        util.errorHandling.ErrorLogger.LogMessage("7");
                    }

                    //resultado = cliente.EnviarFaltante(_MacAddress, _NroFaltante, _CodViajante, _CodCliente, _Fecha, _Observaciones);

                    if (resultado == 0)
                    {
                        Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_Faltante_Transmicion_Upd '" + _NroFaltante + "'");
                        if (Global01.TranActiva != null)
                        {
                            Global01.TranActiva.Commit();
                            Global01.TranActiva = null;
                        }
                    }
                    else
                    {
                        if ((Global01.TranActiva != null))
                        {
                            Global01.TranActiva.Rollback();
                            Global01.TranActiva = null;
                        }
                    }

                    return resultado;
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                return -1;
            }
        }

        public void Inicializar(string ipAddress, string MacAddress)
        {
            try
            {
                bool Conectado = util.network.IPCache.instance.conectado;

                if (Conectado)
                {
                    if (!webServiceInicializado)
                    {
                        cliente = new RecibosWS.Recibos_v_303() ; //FaltantesWS.Faltantes_v_101();
                        
                        cliente.Url = "http://" + ipAddress + "/wsCatalogo4/Faltantes_v_101.asmx?wsdl";
                        if (Global01.proxyServerAddress != "0.0.0.0")
                        {
                            cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                        }

                        _MacAddress = MacAddress;
                        _ip = ipAddress;
                        webServiceInicializado = true;
                    }
                }
                else
                {
                    webServiceInicializado = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}