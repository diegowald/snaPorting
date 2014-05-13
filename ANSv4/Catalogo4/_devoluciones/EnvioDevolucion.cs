using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
//using Microsoft.VisualBasic;

namespace Catalogo._devoluciones
{
    public class EnvioDevolucion
    {
        // Esta clase realiza una consulta al servidor web y obtiene
        // el numero de ip del servidor privado.
        private System.Data.OleDb.OleDbTransaction _TranActiva = null;

        private DevolucionWS.Devolucion Cliente;
        private bool WebServiceInicializado;
        private string m_MacAddress;

        private string m_ip;
        private string m_NroDevolucion;
        private string m_CodCliente;
        private string m_Fecha;
        private string m_Observaciones;

        private string m_Detalle;


        public EnvioDevolucion(System.Data.OleDb.OleDbConnection conexion, string ipAddress, string MacAddress)
        {
            Inicializar(ipAddress, MacAddress);
            Conexion1 = conexion;
        }

        public bool Inicializado
        {
            get { return WebServiceInicializado; }
        }


        private System.Data.OleDb.OleDbConnection Conexion1;

        internal void ObtenerDatos(string NroDevolucion)
        {

            System.Data.OleDb.OleDbDataReader Enc = null;
            System.Data.OleDb.OleDbDataReader Det = null;

            Enc = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_Devolucion_Enc '" + NroDevolucion + "'");
            Det = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_Devolucion_Det '" + NroDevolucion + "'");

            m_NroDevolucion = NroDevolucion;
            Enc.Read();
            m_CodCliente = Enc["IDCliente"].ToString().Trim().PadLeft(6, '0');
            m_Fecha = string.Format("{0:yyyyMMdd}", DateTime.Parse(Enc["F_Devolucion"].ToString()));

            m_Observaciones = Enc["Observaciones"].ToString().Replace(",", " ");

            if (Det.HasRows)
            {
                m_Detalle = "";
                while (Det.Read())
                {
                    m_Detalle += Det["C_Producto"].ToString() + ",";
                    m_Detalle += Det["Cantidad"].ToString().Trim().PadLeft(8, '0') + "00,";
                    m_Detalle += "NO" + ",";
                    m_Detalle += "NO" + ",";
                    m_Detalle += "NO" + ",";
                    m_Detalle += Det["miDeposito"].ToString() + ",";
                    m_Detalle += Det["Factura"].ToString() + ",";
                    m_Detalle += Det["TipoDev"].ToString().Trim().PadLeft(2, '0') + ",";
                    m_Detalle += Det["Vehiculo"].ToString() + ",";
                    m_Detalle += Det["Modelo"].ToString() + ",";
                    m_Detalle += Det["Motor"].ToString() + ",";
                    m_Detalle += Det["Km"].ToString() + ",";
                    m_Detalle += Det["Observaciones"].ToString() + ";";
                }
            }

        }

        public long EnviarDevolucion()
        {
            bool Cancel = false;

            long resultado = 0;

            if (!WebServiceInicializado)
            {
                Cancel = true;
            }

            try
            {
                if (!Cancel)
                {
                    if (_TranActiva == null)
                    {
                        //@ _TranActiva =Conexion1.BeginTransaction();
                        util.errorHandling.ErrorLogger.LogMessage("2");
                    }

                    resultado = Cliente.EnviarDevolucion(m_MacAddress, m_NroDevolucion, m_CodCliente, m_Fecha, m_Observaciones, m_Detalle);

                    if (resultado == 0)
                    {
                        Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_Devolucion_Transmicion_Upd '" + m_NroDevolucion + "'");
                        if (_TranActiva != null)
                        {
                            _TranActiva.Commit();
                            _TranActiva = null;
                        }
                    }
                    else
                    {
                        if (_TranActiva != null)
                        {
                            _TranActiva.Rollback();
                            _TranActiva = null;
                            util.errorHandling.ErrorLogger.LogMessage("10 rollback");
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


        internal void Inicializar(string ipAddress, string MacAddress)
        {
            bool Conectado = util.network.IPCache.instance.conectado;

            try
            {
                if (Conectado)
                {
                    if (!WebServiceInicializado)
                    {
                        Cliente = new DevolucionWS.Devolucion();
                        Cliente.Url = "http://" + ipAddress + "/wsCatalogo4/Devolucion.asmx?wsdl";
                        if (Global01.proxyServerAddress != "0.0.0.0")
                        {
                            Cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                        }

                        m_MacAddress = MacAddress;
                        m_ip = ipAddress;
                        WebServiceInicializado = true;
                    }
                }
                else
                {
                    WebServiceInicializado = false;
                }
            }
            catch (Exception ex)
            {
                //if (System.Runtime.InteropServices.Marshal.GetExceptionCode() == -2147024809)
                //{
                //    //	if (Err().Number == -2147024809) {
                //    // Intento con el ip interno
                //    Cliente = new DevolucionWS.Devolucion();
                //    Cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/Devolucion.asmx?wsdl";
                //    if (Global01.proxyServerAddress != "0.0.0.0")
                //    {
                //        Cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                //    }

                //    m_MacAddress = MacAddress;
                //    m_ip = ipAddressIntranet;
                //    WebServiceInicializado = true;
                //}
                //else
                //{
                throw ex;
                //}
            }
        }

    }
}