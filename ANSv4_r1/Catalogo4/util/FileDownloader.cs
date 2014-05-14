using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util
{
    public class FileDownloader : Catalogo.util.BackgroundTasks.BackgroundTaskBase
    {
        string _URL;
        string _Destino;
        object _Tag;
        bool downloadOK;

        public FileDownloader(string URL, string Destino, object Tag, JOB_TYPE jobType)
            : base("FileDownloader", jobType)
        {
            _URL = URL;
            _Destino = Destino;
            _Tag = Tag;
        }

        private void startDownload()
        {
            downloadOK = false;
            try
            {
                //if (util.network.IPCache.instance.conectado) //DIEGO-PABLO
                if (util.SimplePing.ping(_URL, 2000, 3, Global01.TiposDePing.FILE))
                {
                    System.Net.WebClient downloader = new System.Net.WebClient();
                    _URL = ((_URL.ToLower().StartsWith("http://")) ? _URL : "http://" + _URL);
                    downloader.DownloadFile(new Uri(_URL), _Destino);
                    downloadOK = true;
                }
                else
                {
                    // El archivo no existe en el server
                    fireFileProblem(_URL + " no existe en el servidor");
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                fireFileProblem(ex.Message);
            }
        }

        public delegate void FileDownloadedDelegate(object Tag, string Destino);
        public delegate void FileProblemDelegate(object Tag, string Destino, string cause);
        public delegate void FileDownloadingDelegate(object Tag, string Destino, int progress);

        public FileDownloadedDelegate onFileDownloaded;
        public FileProblemDelegate onFileProblem;
        public FileDownloadingDelegate onFileDownloading;

        private void fireFileDownloaded()
        {
            if (onFileDownloaded != null)
            {
                onFileDownloaded(_Tag, _Destino);
            }
        }

        private void fireFileProblem(string cause)
        {
            if (onFileProblem != null)
            {
                onFileProblem(_Tag, _Destino, cause);
            }
        }

        private void fireFileDownloading(int progress)
        {
            if (onFileDownloading != null)
            {
                onFileDownloading(_Tag, _Destino, progress);
            }
        }

        public override void execute(ref bool cancel)
        {
            if ((worker != null) && (worker.CancellationPending))
            {
                cancel = true;
                worker.CancelAsync();
                return;
            }
            startDownload();
        }

        public override void cancelled()
        {
            fireFileProblem("Download cancelled");
        }

        public override void finished()
        {
            if (downloadOK)
            {
                fireFileDownloaded();
            }
        }
    }
}
