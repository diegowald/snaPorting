using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._auditor
{
    class EnvioAuditoria
    {

        private string _MacAddress;
        private string _ipAddress;
        private AuditWS.audit_v_304 cliente;
        private bool webServiceInicializado;

        public EnvioAuditoria(string MacAddress, string ipAddress)
        {
            inicializar(MacAddress, ipAddress);
        }

        public bool Inicializado
        {
            get
            {
                return webServiceInicializado;
            }
        }

        public void inicializar(string MacAddress, string ipAddress)
        {
            bool conectado = util.network.IPCache.instance.conectado;

            try
            {
                if (!webServiceInicializado)
                {
                    if (conectado)
                    {
                        cliente = new AuditWS.audit_v_304();
                        cliente.Url = "http://" + ipAddress + "/wsCatalogo4/audit_v_304.asmx?wsdl";
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
            catch (Exception ex)
            {
                //if (System.Runtime.InteropServices.Marshal.GetExceptionCode() == -2147024809)
                //{
                    //If Err.Number = -2147024809 Then
                    // Intento con el ip interno
                //    cliente = new AuditWS.audit_v_304();
                //    cliente.Url = "http://" + ipAddressIntranet + "/wsCatalogo4/audit_v_304.asmx?wsdl";
                //    if (Global01.proxyServerAddress != "0.0.0.0")
                //    {
                //        cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                //    }

                //    _MacAddress = MacAddress;
                //    webServiceInicializado = true;
                //    _ipAddress = ipAddressIntranet;
                //}
                //else
                //{
                    throw ex;
                //}
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
