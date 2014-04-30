using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Catalogo.varios
{
    public partial class fDataUpdate : Form
    {
        public string Url { get; set; }
        public bool SoloCatalogo { get; set; }

        public fDataUpdate()
        {
            InitializeComponent(); 

            vcUPDATECTL1.CloseRequest += vcUPDATECTL1_CloseRequest;
            vcUPDATECTL1.ConexionError += vcUPDATECTL1.ConexionError;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fDataUpdate_Load(object sender, EventArgs e)
        {
            string wArchivoTxt = "update3Z1";

            if (Global01.miSABOR == Global01.TiposDeCatalogo.Cliente)
            {
                wArchivoTxt = ((Global01.ListaPrecio==2) ? "update2Z2" : "update2Z1"); 
            }
            else if (Global01.miSABOR == Global01.TiposDeCatalogo.Viajante)
            {
                wArchivoTxt = ((Global01.ListaPrecio==2) ? "update3Z2" : "update3Z1"); 
            }
            else if (Global01.miSABOR == Global01.TiposDeCatalogo.Supervisor)
            {
                wArchivoTxt = ((Global01.ListaPrecio==2) ? "update4Z2" : "update4Z1"); 
            }

            vcUPDATECTL1.configFileURL = "http://" + Url + "/descargas400/" + wArchivoTxt + ".txt";

        }

        private void  vcUPDATECTL1_CloseRequest(string DownloadedFile)
        {
    
            if (DownloadedFile != "") //'they did download an update
            {
                Cursor.Current = Cursors.WaitCursor;
              
                ProcessStartInfo startInfo = new ProcessStartInfo();
                //startInfo.FileName = DownloadedFile; 
                startInfo.FileName = "\"" + DownloadedFile + "\" -d\"" + Global01.AppPath.ToString() + "\""; 

                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;

                try
                {
                    //Process.Start(startInfo);

                    // Start the process with the info we specified.
                    // Call WaitForExit and then the using statement will close.
                    using (Process exeProcess = Process.Start(startInfo))
                    {
                        exeProcess.WaitForExit();
                    }
                    
                    Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_Novedades_Anexar");

                }
                catch (Exception ex)
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);

                    throw ex;
                    // Log error.
                }

                System.IO.File.Delete(DownloadedFile);

                Cursor.Current = Cursors.Default;
            }

            this.Close();
    
        }

        private void vcUPDATECTL1_ConexionError(byte Estado)
        {
            Global01.xError = true;
    
            this.Close();
        }

    }
}

