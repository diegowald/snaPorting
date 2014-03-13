using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.BackgroundTasks
{
    public sealed class ExistenciaProducto : BackgroundTaskBase
    {
        public ExistenciaProducto(JOB_TYPE jobType)
            : base(jobType)
        {
        }

        private string _idProducto;
        private string _idUsuario;

        private string _semaforo;

        public void getExistencia(string idProducto, string idUsuario)
        {
            _idProducto = idProducto;
            _idUsuario = idUsuario;
            base.run();
        }

       /* public string getExistencia(string idProducto, string idUsuario)
        {
            // Toda esta clase hay que hacerla para que funcione como un background worker.
            CatalogoLibraryVB.IPPrivado ipPriv = new CatalogoLibraryVB.IPPrivado(Global01.URL_ANS, Global01.IDMaquina, false, "");
            // TODO: agregar la configuracion del proxy
            string ipPrivado = ipPriv.GetIP();
            string ipIntranet = ipPriv.GetIpIntranet();
            string ipCatalogo = ipPriv.GetIPCatalogo();

            CatalogoLibraryVB.VerExistencia existencia = new CatalogoLibraryVB.VerExistencia();
            existencia.Inicializar(Global01.IDMaquina,  ipPrivado,ipIntranet, false, "");
            string pSemaforo = "";
            existencia.ExistenciaSemaforo(idProducto, ref pSemaforo);
            return pSemaforo;
        }*/

        public override void execute()
        {
            CatalogoLibraryVB.IPPrivado ipPriv = new CatalogoLibraryVB.IPPrivado(Global01.URL_ANS, Global01.IDMaquina, false, "");
            // TODO: agregar la configuracion del proxy
            string ipPrivado = ipPriv.GetIP();
            string ipIntranet = ipPriv.GetIpIntranet();
            string ipCatalogo = ipPriv.GetIPCatalogo();

            CatalogoLibraryVB.VerExistencia existencia = new CatalogoLibraryVB.VerExistencia();
            existencia.Inicializar(Global01.IDMaquina, ipPrivado, ipIntranet, false, "");
            string pSemaforo = "";
            existencia.ExistenciaSemaforo(_idProducto, ref pSemaforo);
            _semaforo = pSemaforo;
        }

        public delegate void CancelledHandler();
        public delegate void FinishedHandler(string idProducto, string resultado);

        public CancelledHandler onCancelled;
        public FinishedHandler onFinished;
        
        public override void cancelled()
        {
            if (onCancelled != null)
            {
                onCancelled();
            }
        }

        public override void finished()
        {
            if (onFinished != null)
            {
                onFinished(_idProducto, _semaforo);
            }
        }

    }
}
