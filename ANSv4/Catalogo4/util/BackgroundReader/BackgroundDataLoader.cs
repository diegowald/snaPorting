using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.Funciones.BackgroundReader
{
    class BackgroundDataLoader
    {
        public enum JOB_TYPE
        {
            Sincronico,
            Asincronico
        }

        System.ComponentModel.BackgroundWorker worker;
        JOB_TYPE job_type;
        string sqlCommand;
        string strConnection;

        private bool cancellationByNewExecution = false;

        public BackgroundDataLoader(JOB_TYPE jobType, string strConnection)
        {
            sqlCommand = "";
            this.strConnection = strConnection;
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

        public delegate void WorkFinishedHandler(System.Data.DataTable dataTable);

        public WorkFinishedHandler onWorkFinishedHandler;

        public void executeQuery(string sqlCommand)
        {
            this.sqlCommand = sqlCommand;
            dataTable = null;
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
                worker_RunWorkerCompleted(null, null);
            }
        }


        private System.Data.DataTable dataTable;

        void worker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("INICIO CARGA PRODUCTOS");
            System.Data.OleDb.OleDbDataAdapter dataAdapter = new System.Data.OleDb.OleDbDataAdapter(sqlCommand, strConnection);

            System.Data.DataTable table = new System.Data.DataTable("dtProducts");
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);
            dataTable = table;
            System.Diagnostics.Debug.WriteLine("FIN CARGA PRODUCTOS");
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
            }
            if (onWorkFinishedHandler != null)
            {
                onWorkFinishedHandler(dataTable);
            }
        }

    }
}
