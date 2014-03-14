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

		    bool Conectado = false;

            //Conectado = My.Computer.Network.Ping(ipAddress, 5000);
            Conectado = true;

            //if (!Conectado) {
            //    Conectado = My.Computer.Network.Ping(ipAddressIntranet, 5000);
            //}

		     // ERROR: Not supported in C#: OnErrorStatement

		    if (!WebServiceInicializado) {
			    if (Conectado) {
				    Cliente = new VerExistenciaWS.VerExistencia();
				    Cliente.Url = "http://" + ipAddress + "/wsOracle/VerExistencia.asmx?wsdl";
				    if (m_UsaProxy) {
					    Cliente.Proxy = new System.Net.WebProxy(m_IPProxyServer);
				    }

				    m_MacAddress = MacAddress;
				    m_ipAddress = ipAddress;
				    WebServiceInicializado = true;
			    } else {
				    WebServiceInicializado = false;
			    }
		    }

		    return;

            //errhandler:

            //if (Err().Number == -2147024809) {
            //    // Intento con el ip interno
            //    Cliente = new VerExistenciaWS.VerExistencia();
            //    Cliente.Url = "http://" + ipAddressIntranet + "/wsOracle/VerExistencia.asmx?wsdl";
            //    if (m_UsaProxy) {
            //        Cliente.Proxy = new System.Net.WebProxy(m_IPProxyServer);
            //    }

            //    m_MacAddress = MacAddress;
            //    WebServiceInicializado = true;
            //    m_ipAddress = ipAddressIntranet;
            //    Err().Clear();
            //} else {
            //    Err().Raise(Err().Number, Err().Source, Err().Description);
            //}

	    }


	    public void ExistenciaSemaforo(string pIdProducto, string pNroUsuario, ref string pSemaforo)
	    {
		    bool Cancel = false;

		    Cancel = false;

		    if (!WebServiceInicializado) {
			    Cancel = true;
		    }

		    if (!Cancel) {
			    pSemaforo = ObtenerSemaforo(ref Cancel, pNroUsuario, pIdProducto);
		    }

	    }

	    private string ObtenerSemaforo(ref bool Cancel, string IDAns, string IdProducto)
	    {
		    string functionReturnValue = null;

		    functionReturnValue = "x";

            //if (!My.Computer.Network.Ping(m_ipAddress, 5000)) {
            //    // Conexion no valida
            //    Cancel = true;
            //} else {
			    functionReturnValue = Cliente.ObtenerExistencia(m_MacAddress, IDAns, IdProducto);
            //}
		    return functionReturnValue;

	    }

    }
}
