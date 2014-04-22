using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catalogo.Funciones.emitter_receiver;
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
                
                util.BackgroundTasks.Updater updater = new util.BackgroundTasks.Updater(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico, util.BackgroundTasks.Updater.UpdateType.UpdateNovedadesCatalogo);
                updater.run();

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
