using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.network
{
    internal class IPCache : Catalogo.util.BackgroundTasks.BackgroundTaskBase
    {
        private bool _conectado;
        private static IPCache _instance;

        public static IPCache instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new IPCache(JOB_TYPE.Asincronico);
                    _instance.run();
                }
                return _instance;
            }
        }
  
        private IPCache(JOB_TYPE jobType)
            : base(jobType)
        {
            lockPing = new object();
            _conectado = checkPing();
        }

        public override void execute()
        {
            while (true)
            {
                util.errorHandling.ErrorLogger.LogMessage("Chequeando conexion");
                setConectado(checkPing());
                System.Threading.Thread.Sleep(1000 * 60); // 1 minuto por defecto
            }
        }

        public override void cancelled()
        {
        }

        public override void finished()
        {
        }

        private object lockPing;
        public bool conectado
        {
            get
            {
                lock (lockPing)
                {
                    return _conectado;
                }
            }
        }

        private void setConectado(bool value)
        {
            util.errorHandling.ErrorLogger.LogMessage("Estado conexion: " + (value ? "conectado." : "desconectado"));
            if (value == _conectado)
            {
                // Es el mismo valor que antes, no hago nada
                return;
            }
            lock (lockPing)
            {
                _conectado = value;
            }
        }

        private bool checkPing()
        {
            return Catalogo.util.SimplePing.ping(Global01.IPPing, 2000, 0, Global01.TiposDePing.ICMP);
        }

      
    }
}
