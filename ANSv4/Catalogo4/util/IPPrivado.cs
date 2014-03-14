﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util
{
    class IPPrivado
    {

	    // Esta clase realiza una consulta al servidor web y obtiene    
	    // el numero de ip del servidor privado.

	    // Define como se llama este modulo para el control de errores

	    private const string m_sMODULENAME_ = "clsIPPrivado";
	    private CatalogoWS.Info Cliente;

	    private bool WebServiceInicializado;

	    private string m_MacAddress;
	    public bool Inicializado {
		    get { return WebServiceInicializado; }
	    }

	    public string GetIP()
	    {
		    string functionReturnValue = null;

		     // ERROR: Not supported in C#: OnErrorStatement


		    string s = null;

		    if (!WebServiceInicializado) {
			    //Err.Raise 9999, , "Falta inicializar"
			    functionReturnValue = "";
			    return functionReturnValue;
		    }
		    s = Cliente.GetIp(m_MacAddress);
		    functionReturnValue = s.Trim();

		    return functionReturnValue;

            //AtajarError:


            //if (Err().Number == -2147467259) {
            //    Err().Clear();
            //    functionReturnValue = "";
            //    return functionReturnValue;
            //} else {
            //    Err().Raise(Err().Number, Err().Source, Err().Description);
            //}
            //return functionReturnValue;

	    }

	    public string GetIpIntranet()
	    {
		    string functionReturnValue = null;

		     // ERROR: Not supported in C#: OnErrorStatement


		    string s = null;

		    if (!WebServiceInicializado) {
			    //Err.Raise 9999, , "Falta inicializar"
			    functionReturnValue = "";
			    return functionReturnValue;
		    }

		    s = Cliente.GetIpIntranet(m_MacAddress);
            functionReturnValue = s.Trim();

		    return functionReturnValue;

            //AtajarError:


            //if (Err().Number == -2147467259) {
            //    Err().Clear();
            //    functionReturnValue = "";
            //    return functionReturnValue;
            //} else {
            //    Err().Raise(Err().Number, Err().Source, Err().Description);
            //}
            //return functionReturnValue;

	    }

	    public string GetIPCatalogo()
	    {
		    string functionReturnValue = null;

		     // ERROR: Not supported in C#: OnErrorStatement


		    string s = null;

		    if (!WebServiceInicializado) {
			    //Err.Raise 9999, , "Falta Inicializar"
			    functionReturnValue = "";
			    return functionReturnValue;
		    }

		    s = Cliente.GetIpCatalogo(m_MacAddress);
            functionReturnValue = s.Trim();

		    return functionReturnValue;

            //AtajarError:


            //if (Err().Number == -2147467259) {
            //    Err().Clear();
            //    functionReturnValue = "";
            //    return functionReturnValue;
            //} else {
            //    Err().Raise(Err().Number, Err().Source, Err().Description);
            //}
            //return functionReturnValue;

	    }

	    public IPPrivado(string ipAddress, string MacAddress, bool usaProxy, string URLProxyServer)
	    {
		     // ERROR: Not supported in C#: OnErrorStatement


		    bool Conectado = false;

            //Conectado = My.Computer.Network.Ping(ipAddress, 5000);

		    Conectado = true;
		    if (!WebServiceInicializado) {
			    if (Conectado) {
				    //Cliente = New WSCatalogo.InfoSoapClient("", "http://" & ipAddress & "/Catalogo/Info.asmx?wsdl")
				    Cliente = new CatalogoWS.Info();
				    // DIEGO -> Implementar proxy!
				    if (usaProxy) {
					    Cliente.Proxy = new System.Net.WebProxy(URLProxyServer);
				    }

				    WebServiceInicializado = true;
				    m_MacAddress = MacAddress;
			    } else {
				    WebServiceInicializado = false;
			    }
		    }

		    return;

            //ErrorHandler:


            //if (Err().Number == -2147024809) {
            //    // Este error se da debido
            //    WebServiceInicializado = false;
            //    Err().Clear();
            //} else {
            //    Err().Raise(Err().Number, Err().Source, Err().Description);
            //}

	    }

    }
}
