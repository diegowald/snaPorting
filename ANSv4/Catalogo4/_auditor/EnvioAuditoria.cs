using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._audit
{
    class EnvioAuditoria
    {

        private string _MacAddress;
        private string _ipAddress;
        private AuditWS.audit_v_304 cliente;
        private bool webServiceInicializado;

        public EnvioAuditoria(string MacAddress,
            string ipAddress,
            string ipAddressIntranet,
            bool usaProxy,
            string proxyServerAddress)
        {
            inicializar(MacAddress, ipAddress,
                ipAddressIntranet, usaProxy, proxyServerAddress);
        }

        public bool Inicializado
        {
            get
            {
                return webServiceInicializado;
            }
        }

        public void inicializar(string MacAddress,
            string ipAddress,
            string ipAddressIntranet,
            bool usaProxy,
            string proxyServerAddress)
        {
            bool conectado = util.SimplePing.ping(ipAddress, 5000);

            if (!conectado)
            {
                conectado = util.SimplePing.ping(ipAddressIntranet, 5000);
            }

            try
            {
                if (!webServiceInicializado)
                {
                    if (conectado)
                    {
                        cliente = new AuditWS.audit_v_304();
                        cliente.Url = "http://" + ipAddress + "/wsCatalogo4/audit_v_304.asmx?wsdl";
                        if (usaProxy)
                        {
                            cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
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
            catch
            {
                //If Err.Number = -2147024809 Then
                // Intento con el ip interno
                cliente = new AuditWS.audit_v_304();
                cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/audit_v_304.asmx?wsdl";
                if (usaProxy)
                {
                    cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
                }

                _MacAddress = MacAddress;
                webServiceInicializado = true;
                _ipAddress = ipAddressIntranet;
                //   Else
                //       Err.Raise(Err.Number, Err.Source, Err.Description)
                //   End If
            }
        }

        public bool enviarAuditoriaEnBloques(string[] Fecha, string[] Descripcion, long[] AuditIDs)
        {
            string Fechas = "";
            string Descripciones = "";
            string IDs = "";

            Fechas = String.Join(";", Fecha);
            Descripciones = String.Join(";", Descripcion);
            IDs = String.Join(";", Array.ConvertAll(AuditIDs, x => x.ToString()));
            if (Fechas.Length > 0)
            {
                long resultado = cliente.AuditInBlock304(_MacAddress, Fechas, Descripciones, IDs);
                return resultado == 0;
            }
            else
            {
                return true;
            }
        }

    }
}
