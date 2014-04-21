using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util
{
    public static class SimplePing
    {
        public static bool ping(string ipAddress, int timeOut, int reintentos)
        {
            bool sResultado = false;

            if (ipAddress.ToLower().IndexOf("www", 0) != -1 | ipAddress.ToLower().IndexOf("imagenes", 0) != -1 | ipAddress.ToLower().IndexOf("novedades", 0) != -1)
            {
                sResultado = ping3(ipAddress, timeOut, reintentos);
            }
            else
            {
                sResultado = ping2(ipAddress, timeOut);
            }
            return sResultado;
        }

        public static bool ping2(string ipAddress, int timeOut)
        {
           // ipAddress = "8.8.8.8";

            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            
            System.Net.NetworkInformation.PingReply reply = p.Send(ipAddress, timeOut);

            return reply.Status == System.Net.NetworkInformation.IPStatus.Success;
        }

        public static bool ping3(string ipAddress, int timeOut, int reintentos)
        {
            string url = ((ipAddress.ToLower().StartsWith("http://")) ? ipAddress : "http://" + ipAddress);
            System.Net.HttpWebRequest request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            request.AllowAutoRedirect = false; // find out if this site is up and don't follow a redirector
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
                //set flag if there was a timeout or some other issues
                if (reintentos > 0)
                {
                    return ping3(ipAddress, timeOut * 2, reintentos - 1);
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
