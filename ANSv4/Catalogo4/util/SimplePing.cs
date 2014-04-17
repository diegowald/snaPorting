using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util
{
    public static class SimplePing
    {
        public static bool ping(string ipAddress, int timeOut)
        {
            ipAddress = "8.8.8.8";

            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            
            System.Net.NetworkInformation.PingReply reply = p.Send(ipAddress, timeOut);

            return reply.Status == System.Net.NetworkInformation.IPStatus.Success;
        }
    }
}
