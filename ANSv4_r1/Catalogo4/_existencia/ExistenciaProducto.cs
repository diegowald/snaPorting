using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.BackgroundTasks
{
    public sealed class ExistenciaProducto : BackgroundTaskBase
    {
        public ExistenciaProducto(JOB_TYPE jobType)
            : base("ExistenciaProducto", jobType)
        {
        }

        private string _idProducto;
        private string _idUsuario;
        System.Windows.Forms.DataGridViewCell _cell;

        private string _semaforo;

        internal void getExistencia(string idProducto, string idUsuario, System.Windows.Forms.DataGridViewCell cell)
        {
            _idProducto = idProducto;
            _idUsuario = idUsuario;
            _cell = cell;
            base.run();
        }

        public override void execute(ref bool cancel)
        {
            if ((worker != null) && (worker.CancellationPending))
            {
                cancel = true;
                worker.CancelAsync();
                return;
            }
            try
            {
                Catalogo._existencia.VerExistencia existencia = new Catalogo._existencia.VerExistencia();
                existencia.Inicializar(Global01.IDMaquina, Global01.URL_ANS2);

                string pSemaforo = "";
                existencia.ExistenciaSemaforo(_idProducto, Global01.NroUsuario, ref pSemaforo);
                _semaforo = pSemaforo;
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
            }
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
