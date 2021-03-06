﻿using System;
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
            : base("ChequeoNovedades", jobType)
        {
            _running = true;
        }

        public override void execute(ref bool cancel)
        {
            while (_running)
            {
                if ((worker != null) && (worker.CancellationPending))
                {
                    cancel = true;
                    worker.CancelAsync();
                    break;
                }
                try
                {
                    System.Diagnostics.Debug.WriteLine("CHEQUEANDO NOVEDADES");

                    util.BackgroundTasks.Updater updater = new util.BackgroundTasks.Updater(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico, util.BackgroundTasks.Updater.UpdateType.UpdateNovedadesCatalogo);
                    updater.run();

                    // min x seg X milisegundos 
                    int xMin = Int16.Parse("0" + Funciones.modINIs.ReadINI("DATOS", "checkNovedades", Global01.setDef_checkNovedades));
                    System.Threading.Thread.Sleep(xMin * 60 * 1000);
                }
                catch (Exception ex)
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);
                }
            }
        }
        

        public override void cancelled()
        {
            _running=false;
        }

        public override void finished()
        {
        }

    }
}
