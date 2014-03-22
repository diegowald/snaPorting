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
        private double m_Efectivo;
        private float m_Dolares;
        private float m_Euros;
        private byte m_ChequesCant;
        private double m_ChequesMonto;
        private byte m_CertificadosCant;

        private double m_CertificadosMonto;
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

            Ren = Funciones.oleDbFunciones.Comando(ref Conexion1, "SELECT * FROM v_Rendicion WHERE Nro='" + NroRendicion + "'");
            RenValores = Funciones.oleDbFunciones.Comando(ref Conexion1, "SELECT * FROM  v_RendicionValores1 WHERE Nro='" + NroRendicion + "'");
            RenRecibos = Funciones.oleDbFunciones.Comando(ref Conexion1, "EXECUTE v_Rendicion_Recibos_rpt '" + NroRendicion.Substring(NroRendicion.Length - 8) + "'");

            m_NroRendicion = NroRendicion;
            m_IdViajante = String.Format("000000", Ren["IDCliente"].ToString().Trim());
            m_F_Rendicion = Ren["F_Rendicion"].ToString().Substring(7, 4) +
                Ren["F_Rendicion"].ToString().Substring(4, 2) +
                Ren["F_Rendicion"].ToString().Substring(1, 2);
            m_Observaciones = Ren["Descripcion"].ToString().Trim();

            m_Efectivo = (double)Ren["Efectivo_Monto"] * 100;
            m_Dolares = (float)Ren["Dolar_Cantidad"] * 100;
            m_Euros = (float)Ren["Euros_Cantidad"] * 100;
            m_ChequesMonto = (float)Ren["Cheques_Monto"] * 100;
            m_CertificadosMonto = (float)Ren["Certificados_Monto"] * 100;
            m_ChequesCant = (byte)Ren["Cheques_Cantidad"];
            m_CertificadosCant = (byte)Ren["Certificados_Cantidad"];

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
                    m_DetalleValores += String.Format("00000000000000000", (float)RenValores["Bco_Dep_Monto"] * 100) + ",";
                    m_DetalleValores += String.Format("00", RenValores["Bco_Dep_Ch_Cantidad"]) + ",";
                    m_DetalleValores += RenValores["N_Cheque"].ToString().Trim().PadLeft(15, ' ') + ",";
                    m_DetalleValores += String.Format("000", RenValores["IdBanco"]) + ";";
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
                    m_DetalleRecibos += String.Format("000000", RenRecibos["Nro_Cuenta"].ToString().Trim()) + ",";
                    m_DetalleRecibos += String.Format("00000000000000000", (float)RenRecibos["Efectivo"] * 100) + ",";
                    m_DetalleRecibos += String.Format("00000000000000000", (float)RenRecibos["Divisas_Dolares"] * 100) + ",";
                    m_DetalleRecibos += String.Format("00000000000000000", (float)RenRecibos["Divisas_Euros"] * 100) + ",";
                    m_DetalleRecibos += String.Format("00000000000000000", (float)RenRecibos["Cheques_Total"] * 100) + ",";
                    m_DetalleRecibos += String.Format("00", RenRecibos["Cheques_Cantidad"]) + ",";
                    m_DetalleRecibos += String.Format("00000000000000000", (float)RenRecibos["Certificados_Total"] * 100) + ",";
                    m_DetalleRecibos += String.Format("00", RenRecibos["Certificados_Cantidad"]) + ",";
                    m_DetalleRecibos += String.Format("00000000000000000", (float)RenRecibos["TotalRecibo"] * 100) + ";";
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
                //if (vg.TranActiva == null) {
                //	vg.TranActiva = vg.Conexion.BeginTransaction;
                //}

                resultado = Cliente.EnviarRendicion(m_MacAddress, m_NroRendicion, m_IdViajante, m_F_Rendicion, m_Observaciones,
                    m_Efectivo.ToString(), m_Dolares.ToString(), m_Euros.ToString(),
                    m_ChequesCant.ToString(), m_ChequesMonto.ToString(),
                m_CertificadosCant.ToString(), m_CertificadosMonto.ToString(), m_DetalleValores, m_DetalleRecibos);

                if (resultado == 0)
                {
                    //                adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Rendicion_Transmicion_Upd '" & Right(m_NroRendicion, 8) & "'")
                    //				if ((vg.TranActiva != null)) {
                    //					vg.TranActiva.Commit();
                    //					vg.TranActiva = null;
                    //				}
                }
                else
                {
                    //				if ((vg.TranActiva != null)) {
                    //					vg.TranActiva.Rollback();
                    //					vg.TranActiva = null;
                    //				}
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
                        Cliente.Url = "http://" + ipAddress + "/wsCatalogo3/Rendicion.asmx?wsdl";
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

                //            if (Err().Number == -2147024809)
                {
                    // Intento con el ip interno
                    Cliente = new RendicionWS.Rendicion();
                    Cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo3/Rendicion.asmx?wsdl";
                    if (usaProxy)
                    {
                        Cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
                    }

                    m_MacAddress = MacAddress;
                    m_ip = ipAddressIntranet;
                    WebServiceInicializado = true;
                }
                //else
                //{
                //    Err().Raise(Err().Number, Err().Source, Err().Description);
                //}

            }
        }


    }
}