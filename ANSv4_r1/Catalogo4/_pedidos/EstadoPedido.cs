using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.BackgroundTasks
{
    public sealed class EstadoPedido : BackgroundTaskBase
    {
        public EstadoPedido(JOB_TYPE jobType)
            : base("EstadoPedido", jobType)
        {
        }

        private string _pedidoNro;
        private string _idUsuario;
        System.Windows.Forms.DataGridViewCell _cell;

        private string _estado;

        internal void getEstado(string pedidoNro, string idUsuario, System.Windows.Forms.DataGridViewCell cell)
        {
            _pedidoNro = pedidoNro;
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
                Catalogo._pedidos.VerEstado estado = new Catalogo._pedidos.VerEstado();
                estado.Inicializar(Global01.IDMaquina, Global01.URL_ANS2);

                string pEstado = "";
                estado.EstadoPedido(_pedidoNro, Global01.NroUsuario, ref pEstado);
                _estado = pEstado;
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
            }
        }

        public delegate void CancelledHandler(System.Windows.Forms.DataGridViewCell cell);
        public delegate void FinishedHandler(string pedidoNro, string resultado, System.Windows.Forms.DataGridViewCell cell);

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
                onFinished(_pedidoNro, _estado, _cell);
            }
        }

    }
}
