using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Diagnostics;
//using Microsoft.VisualBasic;

namespace Catalogo._devolucion
{
    public class EnvioDevolucion
    {

        // Esta clase realiza una consulta al servidor web y obtiene
        // el numero de ip del servidor privado.
        private DevolucionWS.Devolucion Cliente;
        private bool WebServiceInicializado;
        private string m_MacAddress;

        private string m_ip;
        private string m_NroDevolucion;
        private string m_CodCliente;
        private string m_Fecha;
        private string m_Observaciones;

        private string m_Detalle;

        private bool DatosObtenidos;
        public EnvioDevolucion(System.Data.OleDb.OleDbConnection conexion, string ipAddress, string ipAddressIntranet, string MacAddress, bool usaProxy, string proxyServerAddress)
        {
            Inicializar(ipAddress, ipAddressIntranet, MacAddress, usaProxy, proxyServerAddress);
            Conexion1 = conexion;
        }

        public bool Inicializado
        {
            get { return WebServiceInicializado; }
        }


        private System.Data.OleDb.OleDbConnection Conexion1;

        public void ObtenerDatos(string NroDevolucion)
	{
		DatosObtenidos = false;

		System.Data.OleDb.OleDbDataReader Enc = null;
		System.Data.OleDb.OleDbDataReader Det = null;

		Enc = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_Devolucion_Enc '" + NroDevolucion + "'");
		Det = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_Devolucion_Det '" + NroDevolucion + "'");

		m_NroDevolucion = NroDevolucion;
		m_CodCliente = String.Format("000000", Enc["IDCliente"].ToString().Trim());
		m_Fecha = Enc["F_Devolucion"].ToString();
		m_Observaciones = Enc["Observaciones"].ToString().Replace(",", " ");

		if (Det.HasRows) 
        {
			m_Detalle = "";
			while (Det.Read()) 
            {
				m_Detalle += Det["C_Producto"].ToString() + ",";
				m_Detalle += String.Format("00000000", Det["Cantidad"].ToString().Trim()) + "00,";
				m_Detalle += "NO" + ",";
				m_Detalle += "NO" + ",";
				m_Detalle += "NO" + ",";
				m_Detalle += Det["miDeposito"].ToString() + ",";
				m_Detalle += Det["Factura"].ToString() + ",";
				m_Detalle += String.Format("00", Det["TipoDev"]) + ",";
				m_Detalle += Det["Vehiculo"].ToString() + ",";
				m_Detalle += Det["Modelo"].ToString() + ",";
				m_Detalle += Det["Motor"].ToString() + ",";
				m_Detalle += Det["Km"].ToString() + ",";
				m_Detalle += Det["Observaciones"].ToString() + ";";
			}
		}

		DatosObtenidos = true;
	}

        public long EnviarDevolucion()
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
            //    if ((vg.TranActiva != null))
              //  {
                //    vg.TranActiva.Rollback();
              //      vg.TranActiva = null;
                //}

                resultado = Cliente.EnviarDevolucion(m_MacAddress, m_NroDevolucion, m_CodCliente, m_Fecha, m_Observaciones, m_Detalle);

                if (resultado == 0)
                {
                    //                adoModulo.adoComandoIU(vg.Conexion, "EXEC usp_Devolucion_Transmicion_Upd '" & m_NroDevolucion & "'")
//                    if ((vg.TranActiva != null))
//                    {
//                        vg.TranActiva.Commit();
//                        vg.TranActiva = null;
//                    }
                }
                else
                {
//                    if ((vg.TranActiva != null))
//                    {
//                        vg.TranActiva.Rollback();
//                        vg.TranActiva = null;
//                   }
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
                        Cliente = new DevolucionWS.Devolucion();
                        Cliente.Url = "http://" + ipAddress + "/wsCatalogo4/Devolucion.asmx?wsdl";
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
                    Cliente = new DevolucionWS.Devolucion();
                    Cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/Devolucion.asmx?wsdl";
                    if (usaProxy)
                    {
                        Cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
                    }

                    m_MacAddress = MacAddress;
                    m_ip = ipAddressIntranet;
                    WebServiceInicializado = true;
                }
                //          else
                //          {
                //              Err().Raise(Err().Number, Err().Source, Err().Description);
                //          }
            }
        }

    }
}