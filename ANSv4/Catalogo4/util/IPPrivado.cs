using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo.util
{
    class IPPrivado
    {

	    // Esta clase realiza una consulta al servidor web y obtiene    
	    // el numero de ip del servidor privado.

	    // Define como se llama este modulo para el control de errores

	    //private //const string m_sMODULENAME_ = "clsIPPrivado";
	    private CatalogoWS.Info Cliente;

	    private bool WebServiceInicializado;

	    private string m_MacAddress;
	    public bool Inicializado 
        {
		    get { return WebServiceInicializado; }
	    }

	    public string GetIP()
	    {
		    string functionReturnValue = null;

		     // ERROR: Not supported in C#: OnErrorStatement
            
		    string s = "";

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

		    string s = "";

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

        //public string GetIPCatalogo()
        //{
        //    string functionReturnValue = null;

        //     // ERROR: Not supported in C#: OnErrorStatement


        //    string s = null;

        //    if (!WebServiceInicializado) {
        //        //Err.Raise 9999, , "Falta Inicializar"
        //        functionReturnValue = "";
        //        return functionReturnValue;
        //    }

        //    s = Cliente.GetIpCatalogo(m_MacAddress);
        //    functionReturnValue = s.Trim();

        //    return functionReturnValue;

        //}

	    public IPPrivado(string ipAddress, string MacAddress)
	    {

            bool Conectado;

        VadeNuevo:
            Conectado = util.SimplePing.ping(ipAddress, 5000, 0);
            if (!Conectado)
            {
                if (MessageBox.Show("Error de Conexión al Servidor, ¿quiere intentar de nuevo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Global01.URL_ANS = Global01.URL_ANS2;
                    ipAddress = Global01.URL_ANS;
                    goto VadeNuevo;
                }
            }

		    if (!WebServiceInicializado) {       
                if (Conectado) {
				    Cliente = new CatalogoWS.Info();
                    //Cliente.Url = Catalogo.Properties.Settings.Default.Catalogo_CatalogoWS_Info;
                    Cliente.Url = "http://" + ipAddress + "/wsCatalogo4/Info.asmx?wsdl";
                    if (Global01.proxyServerAddress != "0.0.0.0")
                    {
					    Cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
				    }

				    WebServiceInicializado = true;
				    m_MacAddress = MacAddress;
			    } else {
				    WebServiceInicializado = false;
                    util.errorHandling.ErrorLogger.LogMessage(new Exception("IpPrivado - ws Info - No Inicializado"));
			    }
		    }

		    return;

            //.ErrorForm.show():


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
