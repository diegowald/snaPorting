using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util
{
    public static class SimplePing
    {
        /*public static bool ping(string ipAddress, int timeOut)
        {
            ipAddress = "8.8.8.8";

            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            
            System.Net.NetworkInformation.PingReply reply = p.Send(ipAddress, timeOut);

            return reply.Status == System.Net.NetworkInformation.IPStatus.Success;
        }*/

        public static bool ping(string ipAddress, int timeOut)
        {
            string url = ipAddress.StartsWith("http://") ? ipAddress : "http://" + ipAddress;
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
                util.errorHandling.ErrorLogger.LogMessage(wex);
                //set flag if there was a timeout or some other issues
                return false;
            }
        }
    }
}
