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
            System.Diagnostics.Debug.WriteLine(ipAddress);
            string url = ((ipAddress.ToLower().StartsWith("http://")) ? ipAddress : "http://" + ipAddress);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            request.AllowAutoRedirect = true; // find out if this site is up and don't follow a redirector
            request.Method = "HEAD";
            request.Timeout = timeOut;
            try
            {
                var response = request.GetResponse();
                // do something with response.Headers to find out information about the request
                return true;
            }
            catch (System.Net.WebException wex)
            {
                util.errorHandling.ErrorLogger.LogMessage(wex);
                //set flag if there was a timeout or some other issues
                if (reintentos > 0)
                {
                    util.errorHandling.ErrorLogger.LogMessage("Reintentando...");
                    return pingHTTP(ipAddress, timeOut * 2, reintentos - 1);
                }
                else
                {
                    util.errorHandling.ErrorLogger.LogMessage(wex);
                    return false;
                }
            }
        }
    }
}
