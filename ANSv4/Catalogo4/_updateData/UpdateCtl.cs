using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo.util
{
    public partial class UpdateCtl : UserControl
    {

        const string vclu = "_vclu.txt";
        private string tempFolder;
        private string _configFileURL;
        private string thisVer;
        private long step;
        private string name;
        private string webVer;
        private string updateURL;
        private string desktop;
        private string outFile;
        private string successFile;
        private long maxBytes;

        public delegate void CloseRequestHandler(string downloadedFile);
        public delegate void ConexionErrorHandler(byte estado);

        public CloseRequestHandler CloseRequest;
        public ConexionErrorHandler ConexionError;

        public string configFileURL
        {
            get
            {
                return _configFileURL;
            }
            set
            {
                _configFileURL = value;
            }
        }

        public UpdateCtl()
        {
            InitializeComponent();

            tempFolder = Global01.AppPath + "\\Reportes\\";            
            desktop = Global01.AppPath + "\\Reportes\\";
            thisVer = Global01.MiBuild.ToString();
            successFile = "";
            step = 0;
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            if (CloseRequest != null)
            {
                CloseRequest(successFile);
            }
        }

        private void cmdNext_Click(object sender, EventArgs e)
        {
            //long x;
            cmdCancel.Focus();
            cmdNext.Enabled = false;
            step++;
            fraFRAME1.Visible = step == 1;
            fraFRAME2.Visible = step != 1;
            if (step == 1)
            {
                if (configFileURL.Length > 0)
                {
                    Cursor.Current = Cursors.WaitCursor;

                    BeginDownload(configFileURL, tempFolder + vclu);

                    //System.Net.WebClient txtVersionFile = new System.Net.WebClient();
                    //txtVersionFile.DownloadFileCompleted += txtVersionFile_DownloadFileCompleted;
                    //txtVersionFile.DownloadFile(configFileURL, tempFolder + vclu);

                    Cursor.Current = Cursors.Default;

                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(
                    "A configuration download URL was not provided. Live update cannot continue.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    step--;
                }
            }
            else
            {
                lblUpdate.Text = lblUpdate.Text.Replace("$n", "\n" + name)
                    + "\n\n Esperar un momento, por favor...";
                BeginDownload(updateURL, desktop + outFile);
            }
        }

        //private void txtVersionFile_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        //{
        //    if (downloadingFile.EndsWith(vclu))
        //    {
        //        getConfigValues();
        //        lblVersion.Text = String.Format("Version actual: {0}. Version disponible: {1}.", thisVer, webVer);
        //        if (thisVer.CompareTo(webVer) > 0)
        //        {
        //            cmdCancel.Focus();
        //            cmdNext.Enabled = false;
        //        }
        //        else
        //        {
        //            lblSI.Visible = true;
        //            cmdNext.Enabled = true;
        //            cmdNext.Focus();
        //        }
        //    }
        //}

        private string downloadingFile;
        private void BeginDownload(string URL, string saveFile)
        {
            downloadingFile = saveFile;
            System.Net.WebClient client = new System.Net.WebClient();
            client.DownloadProgressChanged += client_DownloadProgressChanged;
            client.DownloadFileCompleted+=client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(URL), saveFile);
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (downloadingFile.EndsWith(vclu))
            {
                getConfigValues();
                lblVersion.Text = String.Format("Version actual: {0}. Version disponible: {1}.", thisVer, webVer);
                if (thisVer.CompareTo(webVer) >= 0)
                {
                   lblStep2.Visible = false;
                    lblNo.Visible = true;
                    cmdCancel.Left = cmdNext.Left + 10 ;

                    cmdCancel.Text = "Continuar";
                    cmdNext.Enabled = false;
                    cmdNext.Visible = false;
                    cmdCancel.Focus();
                }
                else
                {
                    lblSI.Visible = true;
                    cmdNext.Enabled = true;
                    cmdNext.Focus();
                }
            }
            else
            {
                cmdCancel.Text = "Finalizar";
                cmdCancel.Focus();
                if (maxBytes / 1000 < 1000)
                {
                    lblProgressUpdate.Text = String.Format("Se descargaron {0} KB, operacion completada.", maxBytes / 1000);
                }
                else
                {
                    lblProgressUpdate.Text = String.Format("Se descargaron {0} MB, operacion completada.", maxBytes / 1000000);
                }
                successFile = downloadingFile;
            }
        }

        private void getConfigValues()
        {
            try
            {
                System.IO.StreamReader file = new System.IO.StreamReader(tempFolder + vclu);
                name = "";
                webVer = "";
                updateURL = "";
                for (int lineNo = 1; lineNo <= 3; lineNo++)
                {
                    string line = file.ReadLine();
                    switch (lineNo)
                    {
                        case 1:
                            name = line;
                            break;
                        case 2:
                            webVer = line;
                            break;
                        case 3:
                        default:
                            updateURL = line;
                            int spos = updateURL.LastIndexOf('/');
                            outFile = updateURL.Substring(spos+1);
                            break;
                    }
                }
                file.Close();
                System.IO.File.Delete(tempFolder + vclu);
            }
            catch
            {
            }
        }

        void client_DownloadProgressChanged(object sender, System.Net.DownloadProgressChangedEventArgs e)
        {
            if (!downloadingFile.EndsWith(vclu))
            {
                if (e.TotalBytesToReceive / 1000 < 1000)
                {
                    lblProgressUpdate.Text = String.Format("{0} KB de {1} KB descargados.", e.BytesReceived / 1000, e.TotalBytesToReceive / 1000);
                }
                else
                {
                    lblProgressUpdate.Text = String.Format("{0} MB de {1} MB descargados.", e.BytesReceived / 1000000, e.TotalBytesToReceive / 1000000);
                }
                progressBar1.Minimum = 0;
                progressBar1.Maximum = (int)e.TotalBytesToReceive;
                progressBar1.Value = (int)e.BytesReceived;
                maxBytes = e.TotalBytesToReceive;
            }
        }

    }
}
