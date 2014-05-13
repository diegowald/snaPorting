using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._pedidos
{
    class VerEstado
    {
	    private VerEstadoPedidoWS.VerEstadoPedido Cliente;

	    private bool WebServiceInicializado;
	    private string m_MacAddress;
	    private string m_ipAddress;

        public bool Inicializado 
        {
		    get { return WebServiceInicializado; }
	    }

	    internal void Inicializar(string MacAddress, string ipAddress)
	    {
            bool Conectado = util.network.IPCache.instance.conectado;

            try
            {
                if (Conectado)
                {
                    if (!WebServiceInicializado)
                    {
                        Cliente = new VerEstadoPedidoWS.VerEstadoPedido();
                        Cliente.Url = "http://" + ipAddress + "/wsOracle/VerEstadoPedido.asmx?wsdl";
                        if (Global01.proxyServerAddress != "0.0.0.0")
                        {
                            Cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
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
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;
            }
	
		    return;

       }

	    internal void EstadoPedido(string pPedidoNro, string pNroUsuario, ref string pEstado)
	    {
		    bool Cancel = false;

		    if (!WebServiceInicializado) {
			    Cancel = true;
		    }

		    if (!Cancel) {
                pEstado = ObtenerEstado(ref Cancel, pNroUsuario, pPedidoNro);
		    }
	    }

	    private string ObtenerEstado(ref bool Cancel, string IDAns, string PedidoNro)
	    {
            string sResultado = "x";
            try
            {

                sResultado = Cliente.ObtenerEstado(m_MacAddress, IDAns, PedidoNro);
                return sResultado;
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                return sResultado;
            }
	    }

    }
}
