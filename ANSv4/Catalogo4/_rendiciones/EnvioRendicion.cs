using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.VisualBasic;

namespace Catalogo._rendicion
{
    public class EnvioRendicion
    {
        // Esta clase realiza una consulta al servidor web y obtiene
        // el numero de ip del servidor privado.
        private RendicionWS.Rendicion Cliente;
        private bool WebServiceInicializado;
        private string m_MacAddress;

        private string m_ip;
        private string m_NroRendicion;
        private string m_IdViajante;
        private string m_F_Rendicion;
        private string m_Observaciones;
        private float m_Efectivo;
        private float m_Dolares;
        private float m_Euros;
        private byte m_ChequesCant;
        private float m_ChequesMonto;
        private byte m_CertificadosCant;

        private float m_CertificadosMonto;
        private string m_DetalleValores;

        private string m_DetalleRecibos;

        private bool DatosObtenidos;

        private System.Data.OleDb.OleDbConnection Conexion1;

        public EnvioRendicion(System.Data.OleDb.OleDbConnection conexcion, string ipAddress, string ipAddressIntranet, string MacAddress, bool usaProxy, string proxyServerAddress)
        {
            Inicializar(ipAddress, ipAddressIntranet, MacAddress, usaProxy, proxyServerAddress);
            Conexion1 = conexcion;
        }

        public bool Inicializado
        {
            get { return WebServiceInicializado; }
        }


        public void ObtenerDatos(string NroRendicion)
        {
            DatosObtenidos = false;

            System.Data.OleDb.OleDbDataReader Ren = null;
            System.Data.OleDb.OleDbDataReader RenValores = null;
            System.Data.OleDb.OleDbDataReader RenRecibos = null;

            Ren = Funciones.oleDbFunciones.Comando(Conexion1, "SELECT * FROM v_Rendicion WHERE Nro='" + NroRendicion + "'");
            RenValores = Funciones.oleDbFunciones.Comando(Conexion1, "SELECT * FROM  v_RendicionValores1 WHERE Nro='" + NroRendicion + "'");
            RenRecibos = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_Rendicion_Recibos_rpt '" + NroRendicion.Substring(NroRendicion.Length - 8) + "'");

            m_NroRendicion = NroRendicion;
            Ren.Read();
            m_IdViajante = Ren["IDCliente"].ToString().Trim().PadLeft(6, '0');
            m_F_Rendicion = Ren["F_Rendicion"].ToString().Substring(7, 4) +
                Ren["F_Rendicion"].ToString().Substring(4, 2) +
                Ren["F_Rendicion"].ToString().Substring(1, 2);
            m_Observaciones = Ren["Descripcion"].ToString().Trim();

            m_Efectivo = float.Parse(Ren["Efectivo_Monto"].ToString()) * 100;
            m_Dolares = float.Parse(Ren["Dolar_Cantidad"].ToString()) * 100;
            m_Euros = float.Parse(Ren["Euros_Cantidad"].ToString()) * 100;
            m_ChequesMonto = float.Parse(Ren["Cheques_Monto"].ToString()) * 100;
            m_CertificadosMonto = float.Parse(Ren["Certificados_Monto"].ToString()) * 100;
            m_ChequesCant = byte.Parse(Ren["Cheques_Cantidad"].ToString());
            m_CertificadosCant = byte.Parse(Ren["Certificados_Cantidad"].ToString());

            if (RenValores.HasRows)
            {
                m_DetalleValores = "";
                while (RenValores.Read())
                {
                    m_DetalleValores += RenValores["Bco_Dep_Tipo"].ToString() + ",";
                    m_DetalleValores += RenValores["Bco_Dep_Fecha"].ToString().Substring(7, 4)
                        + RenValores["Bco_Dep_Fecha"].ToString().Substring(4, 2)
                        + RenValores["Bco_Dep_Fecha"].ToString().Substring(1, 2) + ",";
                    m_DetalleValores += RenValores["Bco_Dep_Numero"].ToString().Substring(10) + ",";
                    m_DetalleValores += (float.Parse(RenValores["Bco_Dep_Monto"].ToString()) * 100).ToString().Trim().PadLeft(17, '0') + ",";
                    m_DetalleValores += RenValores["Bco_Dep_Ch_Cantidad"].ToString().Trim().PadLeft(2, '0') + ",";
                    m_DetalleValores += RenValores["N_Cheque"].ToString().Trim().PadLeft(15, ' ') + ",";
                    m_DetalleValores += RenValores["IdBanco"].ToString().Trim().PadLeft(3, '0') + ";";
                }
            }

            if (RenRecibos.HasRows)
            {
                m_DetalleRecibos = "";
                while (RenRecibos.Read())
                {
                    m_DetalleRecibos += RenRecibos["nroRecibo"].ToString() + ",";
                    m_DetalleRecibos += RenRecibos["F_Recibo"].ToString().Substring(7, 4)
                        + RenRecibos["F_Recibo"].ToString().Substring(4, 2)
                        + RenRecibos["F_Recibo"].ToString().Substring(1, 2) + ",";
                    m_DetalleRecibos += RenRecibos["Nro_Cuenta"].ToString().Trim().PadLeft(6, '0') + ",";
                    m_DetalleRecibos += (float.Parse(RenRecibos["Efectivo"].ToString()) * 100).ToString().Trim().PadLeft(17, '0') + ",";
                    m_DetalleRecibos += (float.Parse(RenRecibos["Divisas_Dolares"].ToString()) * 100).ToString().Trim().PadLeft(17, '0') + ",";
                    m_DetalleRecibos += (float.Parse(RenRecibos["Divisas_Euros"].ToString()) * 100).ToString().Trim().PadLeft(17, '0') + ",";
                    m_DetalleRecibos += (float.Parse(RenRecibos["Cheques_Total"].ToString()) * 100).ToString().Trim().PadLeft(17, '0') + ",";
                    m_DetalleRecibos += RenRecibos["Cheques_Cantidad"].ToString().Trim().PadLeft(2, '0') + ",";
                    m_DetalleRecibos += (float.Parse(RenRecibos["Certificados_Total"].ToString()) * 100).ToString().Trim().PadLeft(17, '0') + ",";
                    m_DetalleRecibos += RenRecibos["Certificados_Cantidad"].ToString().Trim().PadLeft(2, '0') + ",";
                    m_DetalleRecibos += (float.Parse(RenRecibos["TotalRecibo"].ToString()) * 100).ToString().Trim().PadLeft(17, '0') + ";";
                }
            }

            Ren = null;
            RenValores = null;
            RenRecibos = null;

            DatosObtenidos = true;

        }

        public long EnviarRendicion()
        {
            bool Cancel = false;
            long resultado = 0;

            if (!WebServiceInicializado)
            {
                Cancel = true;
            }


            if (!Cancel)
            {
                if (Global01.TranActiva == null)
                {
                    Global01.TranActiva = Conexion1.BeginTransaction();
                }

                resultado = Cliente.EnviarRendicion(m_MacAddress, m_NroRendicion, m_IdViajante, m_F_Rendicion, m_Observaciones,
                    m_Efectivo.ToString(), m_Dolares.ToString(), m_Euros.ToString(),
                    m_ChequesCant.ToString(), m_ChequesMonto.ToString(),
                m_CertificadosCant.ToString(), m_CertificadosMonto.ToString(), m_DetalleValores, m_DetalleRecibos);

                if (resultado == 0)
                {
                    Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_Rendicion_Transmicion_Upd '" + m_NroRendicion.Substring(m_NroRendicion.Length - 8) + "'");
                    if (Global01.TranActiva != null)
                    {
                        Global01.TranActiva.Commit();
                        Global01.TranActiva = null;
                    }
                }
                else
                {
                    if (Global01.TranActiva != null)
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


        public void Inicializar(string ipAddress, string ipAddressIntranet, string MacAddress, bool usaProxy, string proxyServerAddress)
        {
            bool Conectado = false;
            Conectado = util.SimplePing.ping(ipAddress, 5000);
            if (!Conectado)
            {
                Conectado = util.SimplePing.ping(ipAddressIntranet, 5000);
            }

            try
            {

                if (Conectado)
                {
                    if (!WebServiceInicializado)
                    {
                        Cliente = new RendicionWS.Rendicion();
                        Cliente.Url = "http://" + ipAddress + "/wsCatalogo4/Rendicion.asmx?wsdl";
                        if (usaProxy)
                        {
                            Cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
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
                if (System.Runtime.InteropServices.Marshal.GetExceptionCode() == -2147024809)
                {
                    //errhandler:
                    //        If Err.Number = -2147024809 Then
                    // Intento con el ip interno
                    Cliente = new RendicionWS.Rendicion();
                    Cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/Rendicion.asmx?wsdl";
                    if (usaProxy)
                    {
                        Cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
                    }

                    m_MacAddress = MacAddress;
                    m_ip = ipAddressIntranet;
                    WebServiceInicializado = true;
                }
                else
                {
                    throw ex;
                }
            }
        }
    }
}