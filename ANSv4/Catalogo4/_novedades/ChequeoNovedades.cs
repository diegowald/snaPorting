using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.BackgroundTasks
{
    public class ChequeoNovedades : BackgroundTaskBase
    {

        private bool _running;

        public ChequeoNovedades(JOB_TYPE jobType)
            : base(jobType)
        {
            _running = true;
        }

        public override void execute()
        {
            while (_running)
            {
                System.Diagnostics.Debug.WriteLine("CHEQUEANDO NOVEDADES");
                util.IPPrivado ipPriv = new util.IPPrivado(Global01.URL_ANS, Global01.IDMaquina);
                // TODO: agregar la configuracion del proxy
                string ipPrivado = ipPriv.GetIP();
                string ipIntranet = ipPriv.GetIpIntranet();
                //string ipCatalogo = ipPriv.GetIPCatalogo();

                // Aca va el codigo para chequear si hay novedades en el server.

/*                Catalogo._existencia.VerExistencia existencia = new Catalogo._existencia.VerExistencia();
                existencia.Inicializar("3PRUEBA-CATALOGO-4", ipPrivado, ipIntranet);

                string pSemaforo = "";
                existencia.ExistenciaSemaforo(_idProducto, Global01.NroUsuario, ref pSemaforo);
                _semaforo = pSemaforo;
                */

                // min x seg X milisegundos 
                System.Threading.Thread.Sleep(10 * 60 * 1000); 
            }
        }

        public override void cancelled()
        {
            _running=false;
        }

        public override void finished()
        {
            throw new NotImplementedException();
        }

    }
}
