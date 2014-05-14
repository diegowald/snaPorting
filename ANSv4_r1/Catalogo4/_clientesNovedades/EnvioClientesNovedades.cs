using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._clientesNovedades
{
    public class EnvioClientesNovedades
    {
        private string _MacAddress;
        private string _ipAddress;
        private ClientesNovedadesWS.ClientesNovedades cliente;
        private bool webServiceInicializado;

        public bool inicializado
        {
            get
            {
                return webServiceInicializado;
            }
        }

        public EnvioClientesNovedades(string MacAddress, string ipAddress)
        {
            inicializar(MacAddress, ipAddress);
        }

        private void inicializar(string MacAddress, string ipAddress)
        {
            bool conectado = util.network.IPCache.instance.conectado;

            if (!webServiceInicializado)
            {
                if (conectado)
                {
                    cliente = new ClientesNovedadesWS.ClientesNovedades();
                    cliente.Url = "http://" + ipAddress + "/wsCatalogo4/ClientesNovedades.asmx?wsdl";
                    if (Global01.proxyServerAddress != "0.0.0.0")
                    {
                        cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                    }
                    _MacAddress = MacAddress;
                    _ipAddress = ipAddress;
                    webServiceInicializado = true;
                }
                else
                {
                    webServiceInicializado = false;
                }
            }
        }

        public bool enviarNovedadesEnBloques(System.Collections.Generic.List<string> fechas, System.Collections.Generic.List<string> novedades, System.Collections.Generic.List<string> clientes, System.Collections.Generic.List<string> viajantes, System.Collections.Generic.List<string> tipos)
        {
            string sFechas;
            string sNovedades;
            string sClientes;
            string sViajantes;
            string sTipos;

            sFechas = "";
            sNovedades = "";
            sClientes = "";

            sFechas = string.Join(";", fechas.ToArray());
            sNovedades = string.Join(";", novedades.ToArray());
            sClientes = string.Join(";", clientes.ToArray());
            sViajantes = string.Join(";", viajantes.ToArray());
            sTipos = string.Join(";", tipos.ToArray());

            if (sFechas.Length > 0)
            {
                long resultado = cliente.CallClientesNovedades(_MacAddress, sFechas, sNovedades, sClientes, sViajantes, sTipos);
                return resultado == 0;
            }
            else
            {
                return true;
            }
        }

    }
}