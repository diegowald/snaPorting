﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.BackgroundTasks
{
    public abstract class BackgroundTaskBase
    {
        public enum JOB_TYPE
        {
            Sincronico,
            Asincronico
        }

        protected System.ComponentModel.BackgroundWorker worker;
        protected JOB_TYPE job_type;

        private bool cancellationByNewExecution = false;
        private int _delayTime = -1;

        public BackgroundTaskBase(string taskName, JOB_TYPE jobType)
        {
            job_type = jobType;
            if (jobType == JOB_TYPE.Asincronico)
            {
                BackgroundTaskCoordinator.instance.addTask(taskName, this);
                worker = new System.ComponentModel.BackgroundWorker();
                worker.DoWork += worker_DoWork;
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.WorkerSupportsCancellation = true;
            }
            else
            {
                worker = null;
            }
        }

        internal void run()
        {
            if (job_type == JOB_TYPE.Asincronico)
            {

                if ((worker != null) && (worker.IsBusy))
                {
                    cancellationByNewExecution = true;
                    worker.CancelAsync();
                    return;
                }
                worker.RunWorkerAsync();
            }
            else
            {
                worker_DoWork(null, null);
                worker_RunWorkerCompleted(null, new System.ComponentModel.RunWorkerCompletedEventArgs(null, null, false));
            }
        }

        internal void delayedRun(int sleepTime)
        {
            _delayTime = sleepTime;
            run();
        }


        void worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            bool cancel = false;
            try
            {
                if ((job_type == JOB_TYPE.Asincronico) && (_delayTime != -1))
                {
                    System.Threading.Thread.Sleep(_delayTime);
                }
                execute(ref cancel);
                if (e != null)
                {
                    e.Cancel = cancel;
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
            }
        }

        void worker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                if (cancellationByNewExecution)
                {
                    cancellationByNewExecution = false;
                    worker.RunWorkerAsync();
                    return;
                }
                else
                {
                    cancelled();
                }
            }
            else
            {
                finished();
            }
        }

        internal void shutdown()
        {
            worker.CancelAsync();
        }

        public abstract void execute(ref bool cancel);
        public abstract void cancelled();
        public abstract void finished();


    }
}
