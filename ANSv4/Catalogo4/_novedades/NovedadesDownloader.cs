using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._novedades
{
    public class NovedadesDownloader
    {
        string _URL;
        string _Destino;
        object _Tag;

        public NovedadesDownloader(string URL, string Destino, object Tag)
        {
            _URL = URL;
            _Destino = Destino;
            _Tag=Tag;
        }

        public void startDownload()
        {
            try
            {
                if (util.SimplePing.ping(_URL, 1000))
                {
                    System.Net.WebClient downloader = new System.Net.WebClient();
                    downloader.DownloadFileCompleted += downloader_DownloadFileCompleted;
                    downloader.DownloadProgressChanged += downloader_DownloadProgressChanged;
                    downloader.DownloadFileAsync(new Uri(_URL), _Destino);
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

        void downloader_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            fireFileDownloading(e.ProgressPercentage);
        }

        void downloader_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                fireFileProblem("Cancelled");
                return;
            }
            if (e.Error != null)
            {
                util.errorHandling.ErrorLogger.LogMessage(e.Error);
                fireFileProblem(e.Error.Message);
            }
            fireFileDownloaded();
        }
    }
}
