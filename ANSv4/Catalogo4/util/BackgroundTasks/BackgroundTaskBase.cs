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

        System.ComponentModel.BackgroundWorker worker;
        JOB_TYPE job_type;

        private bool cancellationByNewExecution = false;

        public BackgroundTaskBase(JOB_TYPE jobType)
        {
            job_type = jobType;
            if (jobType == JOB_TYPE.Asincronico)
            {
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

        public void run()
        {
            if (job_type == JOB_TYPE.Asincronico)
            {
                if (worker.IsBusy)
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


        void worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            execute();
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

        public abstract void execute();
        public abstract void cancelled();
        public abstract void finished();

    }
}