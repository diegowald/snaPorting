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
        public EnvioInterDeposito(System.Data.OleDb.OleDbConnection Conexion, string ipAddress, string MacAddress)
        {
            Inicializar(ipAddress, MacAddress);
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
            if (!I.HasRows)
            {
                return;
            }
            I.Read();
            
            m_CodCliente = I["IDCliente"].ToString().Trim().PadLeft(6,'0');
            m_Bco_Dep_Fecha = string.Format("{0:yyyyMMdd}",DateTime.Parse(I["Bco_Dep_Fecha"].ToString()));
            m_Bco_Dep_Tipo = I["Bco_Dep_Tipo"].ToString();
            m_Bco_Dep_Numero = I["Bco_Dep_Numero"].ToString().PadLeft(10, '0');
            m_Bco_Dep_Monto = (float.Parse(I["Bco_Dep_Monto"].ToString()) * 100).ToString().Trim().PadLeft(17, '0');
            m_Bco_Dep_Ch_Cantidad = I["Bco_Dep_Ch_Cantidad"].ToString().Trim().PadLeft(2, '0');
            m_Bco_Dep_IdCta = I["Bco_Dep_IdCta"].ToString().Trim().PadLeft(3, '0');

            m_Observaciones = "";

            m_Detalle = "";
            if (Ifacturas.HasRows)
            {
                while (Ifacturas.Read())
                {
                    m_Detalle += Ifacturas["T_Comprobante"].ToString() + "-" + Ifacturas["N_Comprobante"].ToString() + ",";
                    m_Detalle += (float.Parse(Ifacturas["Importe"].ToString()) * 100).ToString().Trim().PadLeft(17, '0') + ";";
                }
            }

            I = null;
            Ifacturas = null;

            DatosObtenidos = true;

        }

        public long EnviarInterDeposito()
        {
            bool Cancel = false;
            long resultado = 0;

            if (!WebServiceInicializado)
            {
                Cancel = true;
            }

            if (!DatosObtenidos)
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
                        util.errorHandling.ErrorLogger.LogMessage("9");
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
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                return -1;
            }
        }

        public void Inicializar(string ipAddress, string MacAddress)
        {
            bool Conectado = util.network.IPCache.instance.conectado;
   
            try
            {
                if (Conectado)
                {
                    if (!WebServiceInicializado)
                    {
                        Cliente = new InterDepositoWS.InterDeposito();
                        Cliente.Url = "http://" + ipAddress + "/wsCatalogo4/InterDeposito.asmx?wsdl";
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
                //    Cliente = new InterDepositoWS.InterDeposito();
                //    Cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/InterDeposito.asmx?wsdl";
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
