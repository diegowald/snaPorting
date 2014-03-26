using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo.util
{
    public partial class fDataUpdate : Form
    {
        public string Url { get; set; }
        public bool SoloCatalogo { get; set; }

        public fDataUpdate()
        {
            InitializeComponent(); 
            vcUPDATECTL1.CloseRequest += vcUPDATECTL1_CloseRequest;
        }

        private void vcUPDATECTL1_Load(object sender, EventArgs e)
        {
            string wArchivoTxt = "update3Z1";
      
            vcUPDATECTL1.configFileURL = "http://" + Url + "/descargas320/" + wArchivoTxt + ".txt";
        }

        private void  vcUPDATECTL1_CloseRequest(string DownloadedFile)
        {
    
            if (DownloadedFile != "") //'they did download an update
            {
                Cursor.Current = Cursors.WaitCursor;

                ShellAndWait(DownloadedFile, "");

                //System.Diagnostics.Process.Start(DownloadedFile, "").WaitForExit();
               
                //System.IO.File.Delete(DownloadedFile);

                Cursor.Current = Cursors.Default;
            }

            this.Close();
    
        }

         private void vcUPDATECTL1_ConexionError(short Estado)
         {
            Global01.xError = true;
    
            this.Close();
         }


    //place this somewhere in your code:
    private void ShellAndWait(string myFileName, string myArguments)
    {
        //Create a new process info structure.
        System.Diagnostics.ProcessStartInfo myProcessInfo = new System.Diagnostics.ProcessStartInfo();
        //Set the file name member of the process info structure.
        myProcessInfo.FileName = myFileName;
        myProcessInfo.Arguments = myArguments;

        //Start the process but wait for the process to end
        System.Diagnostics.Process myProcess = System.Diagnostics.Process.Start(myProcessInfo);
        myProcess.WaitForExit();
    }


    }
}

