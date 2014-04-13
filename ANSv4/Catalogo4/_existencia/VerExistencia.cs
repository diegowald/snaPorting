using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._existencia
{
    class VerExistencia
    {

	    // Define como se llama este modulo para el control de errores

	    private const string m_sMODULENAME_ = "clsVerExistencia";

	    private VerExistenciaWS.VerExistencia Cliente;

	    private bool WebServiceInicializado;
	    private string m_MacAddress;
	    private string m_ipAddress;
	    private bool m_UsaProxy;

	    private string m_IPProxyServer;
	    public bool Inicializado {
		    get { return WebServiceInicializado; }
	    }


	    public void Inicializar(string MacAddress, string ipAddress, string ipAddressIntranet, bool usaProxy, string ipProxyServer)
	    {
            m_UsaProxy = usaProxy;
            m_IPProxyServer = ipProxyServer;

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
                        Cliente = new VerExistenciaWS.VerExistencia();
                        Cliente.Url = "http://" + ipAddress + "/wsOracle/VerExistencia.asmx?wsdl";
                        if (m_UsaProxy)
                        {
                            Cliente.Proxy = new System.Net.WebProxy(m_IPProxyServer);
                        }

                        m_MacAddress = MacAddress;
                        m_ipAddress = ipAddress;
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
                    Cliente = new VerExistenciaWS.VerExistencia();
                    Cliente.Url = "http://" + ipAddressIntranet + "/wsOracle/VerExistencia.asmx?wsdl";
                    if (m_UsaProxy)
                    {
                        Cliente.Proxy = new System.Net.WebProxy(m_IPProxyServer);
                    }

                    m_ipAddress = ipAddressIntranet;
                    m_MacAddress = MacAddress;
                    WebServiceInicializado = true;                    
                }
                else
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);

                    throw ex;
                }
            }
	
		    return;

       }

	    public void ExistenciaSemaforo(string pIdProducto, string pNroUsuario, ref string pSemaforo)
	    {
		    bool Cancel = false;

		    if (!WebServiceInicializado) {
			    Cancel = true;
		    }

		    if (!Cancel) {
			    pSemaforo = ObtenerSemaforo(ref Cancel, pNroUsuario, pIdProducto);
		    }

	    }

	    private string ObtenerSemaforo(ref bool Cancel, string IDAns, string IdProducto)
	    {
            string sResultado = "x";
            sResultado = Cliente.ObtenerExistencia(m_MacAddress, IDAns, IdProducto);
            return sResultado;

            //if (!My.Computer.Network.Ping(m_ipAddress, 5000))
            //{
            //    // Conexion no valida
            //    Cancel = true;
            //}
            //else
            //{
            //    sResultado = Cliente.ObtenerExistencia(m_MacAddress, IDAns, IdProducto);
            //}
	    }

    }
}
