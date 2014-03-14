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
        System.Windows.Forms.DataGridViewCell _cell;

        private string _semaforo;

        public void getExistencia(string idProducto, string idUsuario, System.Windows.Forms.DataGridViewCell cell)
        {
            _idProducto = idProducto;
            _idUsuario = idUsuario;
            _cell = cell;
            base.run();
        }

        public override void execute()
        {
            util.IPPrivado ipPriv = new util.IPPrivado(Global01.URL_ANS, Global01.IDMaquina, false, "");
            // TODO: agregar la configuracion del proxy
            string ipPrivado =  ipPriv.GetIP();
            string ipIntranet =  ipPriv.GetIpIntranet();
            string ipCatalogo =  ipPriv.GetIPCatalogo();

            Catalogo._existencia.VerExistencia existencia = new Catalogo._existencia.VerExistencia();
            existencia.Inicializar("3PRUEBA-CATALOGO-4", ipPrivado, ipIntranet, false, "");

            //existencia.Inicializar(Global01.IDMaquina, ipPrivado, ipIntranet, false, "");

            string pSemaforo = "";
            existencia.ExistenciaSemaforo(_idProducto, Global01.NroUsuario, ref pSemaforo);
            _semaforo = pSemaforo;
        }

        public delegate void CancelledHandler(System.Windows.Forms.DataGridViewCell cell);
        public delegate void FinishedHandler(string idProducto, string resultado, System.Windows.Forms.DataGridViewCell cell);

        public CancelledHandler onCancelled;
        public FinishedHandler onFinished;
        
        public override void cancelled()
        {
            if (onCancelled != null)
            {
                onCancelled(_cell);
            }
        }

        public override void finished()
        {
            if (onFinished != null)
            {
                onFinished(_idProducto, _semaforo, _cell);
            }
        }

    }
}
