using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util
{
    public static class SimplePing
    {
        public static bool ping(string ipAddress, int timeOut, byte reintentos, Global01.TiposDePing tipo /*= Global01.TiposDePing.ICMP*/)
        {
            //tipo<>0 para verificar la existencia de un archivo en la nube

            bool sResultado = false;

            switch (tipo)
            {
                case Global01.TiposDePing.HTTP:
                    {
                        ipAddress = "http://" + Global01.URL_ANS.ToString() + "/ping.txt";
                        sResultado = pingHTTP(ipAddress, timeOut, reintentos);
                    }
                    break;
                case Global01.TiposDePing.FILE:
                    sResultado = pingHTTP(ipAddress, timeOut, reintentos);
                    break;
                case Global01.TiposDePing.ICMP:
                    sResultado = pingICMP(ipAddress, timeOut);
                    break;
            }
            return sResultado;
        }

        public static bool pingICMP(string ipAddress, int timeOut)
        {
           // ipAddress = "8.8.8.8";

            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            
            System.Net.NetworkInformation.PingReply reply = p.Send(ipAddress, timeOut);

            return reply.Status == System.Net.NetworkInformation.IPStatus.Success;
        }

        public static bool pingHTTP(string ipAddress, int timeOut, int reintentos)
        {
            bool sResultado = false;
            
            if (util.network.IPCache.instance.conectado)
            {
                string url = ((ipAddress.ToLower().StartsWith("http://")) ? ipAddress : "http://" + ipAddress);
                try
                {
                    System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                    //request.AllowAutoRedirect = true; // find out if this site is up and don't follow a redirector
                    //request.Method = "HEAD";
                    //request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                    //request.Credentials = System.Net.CredentialCache.DefaultCredentials
                    //request.Timeout = timeOut;
                    using (System.Net.HttpWebResponse response = (System.Net.HttpWebResponse)request.GetResponse())
                    {
                        sResultado = response.StatusCode == System.Net.HttpStatusCode.OK;
                    }
                }
                catch (System.Net.WebException wex)
                {
                    util.errorHandling.ErrorLogger.LogMessage(wex);
                    return sResultado;
                }
            }
            return sResultado;
        }
    }
}
