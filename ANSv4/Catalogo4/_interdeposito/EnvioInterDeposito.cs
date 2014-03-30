using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//        using Microsoft.VisualBasic;

namespace Catalogo._interdeposito
{
    public class EnvioInterDeposito
    {
        // Esta clase realiza una consulta al servidor web y obtiene
        // el numero de ip del servidor privado.
        private InterDepositoWS.InterDeposito Cliente;
        private bool WebServiceInicializado;
        private string m_MacAddress;

        private string m_ip;
        private string m_NroInterDeposito;
        private string m_CodCliente;
        private string m_Bco_Dep_Tipo;
        private string m_Bco_Dep_Fecha;
        private string m_Bco_Dep_Numero;
        private string m_Bco_Dep_Monto;
        private string m_Bco_Dep_Ch_Cantidad;
        private string m_Bco_Dep_IdCta;

        private string m_Observaciones;

        private string m_Detalle;

        private bool DatosObtenidos;

        private System.Data.OleDb.OleDbConnection Conexion1;
        public EnvioInterDeposito(System.Data.OleDb.OleDbConnection Conexion, string ipAddress, string ipAddressIntranet, string MacAddress, bool usaProxy, string proxyServerAddress)
        {
            Inicializar(ipAddress, ipAddressIntranet, MacAddress, usaProxy, proxyServerAddress);
            Conexion1 = Conexion;
        }

        public bool Inicializado
        {
            get { return WebServiceInicializado; }
        }


        public void ObtenerDatos(string NroInterDeposito)
        {
            DatosObtenidos = false;

            System.Data.OleDb.OleDbDataReader I = null;
            System.Data.OleDb.OleDbDataReader Ifacturas = null;

            I = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_InterDeposito_rpt '" + NroInterDeposito + "'");
            Ifacturas = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_InterDepositoFacturas '" + NroInterDeposito + "'");

            m_NroInterDeposito = NroInterDeposito;
            m_CodCliente = String.Format("000000", I["IDCliente"].ToString().Trim());
            m_Bco_Dep_Fecha = String.Format("ddMMyyyy", I["Bco_Dep_Fecha"]);
            m_Bco_Dep_Tipo = I["Bco_Dep_Tipo"].ToString();
            m_Bco_Dep_Numero = I["Bco_Dep_Numero"].ToString().PadLeft(10, '0');
            m_Bco_Dep_Monto = String.Format("00000000000000000", (float)I["Bco_Dep_Monto"] * 100);
            m_Bco_Dep_Ch_Cantidad = String.Format("00", I["Bco_Dep_Ch_Cantidad"]);
            m_Bco_Dep_IdCta = String.Format("000", I["Bco_Dep_IdCta"]);

            m_Observaciones = "";

            m_Detalle = "";
            if (Ifacturas.HasRows)
            {
                while (Ifacturas.Read())
                {
                    m_Detalle += Ifacturas["T_Comprobante"].ToString() + "-" + Ifacturas["N_Comprobante"].ToString() + ",";
                    m_Detalle += String.Format("00000000000000000", (float)Ifacturas["Importe"] * 100) + ";";
                }
            }

            I = null;
            Ifacturas = null;

            DatosObtenidos = true;

        }

        public long EnviarInterDeposito()
        {
            long functionReturnValue = 0;

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
                resultado = Cliente.EnviarInterDeposito(m_MacAddress, m_NroInterDeposito, m_CodCliente, m_Bco_Dep_Tipo, m_Bco_Dep_Fecha, m_Bco_Dep_Numero, m_Bco_Dep_Monto, m_Bco_Dep_Ch_Cantidad, m_Bco_Dep_IdCta, m_Observaciones,
                m_Detalle);

                if (resultado == 0)
                {
                    Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_InterDeposito_Transmicion_Upd '" + m_NroInterDeposito + "'");
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
            bool Conectado = util.SimplePing.ping(ipAddress, 5000);
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
                        Cliente = new InterDepositoWS.InterDeposito();
                        Cliente.Url = "http://" + ipAddress + "/wsCatalogo4/InterDeposito.asmx?wsdl";
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
            catch
            {
                //	if (Err().Number == -2147024809) {
                // Intento con el ip interno
                Cliente = new InterDepositoWS.InterDeposito();
                Cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/InterDeposito.asmx?wsdl";
                if (usaProxy)
                {
                    Cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
                }

                m_MacAddress = MacAddress;
                m_ip = ipAddressIntranet;
                WebServiceInicializado = true;
                //	} else {
                //		Err().Raise(Err().Number, Err().Source, Err().Description);
                //	}
            }
        }

    }
}
