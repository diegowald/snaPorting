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
        //UserControl ucUpdateCtl = new UserControl();

        public string Url { get; set; }

        public bool SoloCatalogo { get; set; }

        public fDataUpdate()
        {
            InitializeComponent();
            
            
            //ucUpdateCtl = new UpdateCtl();


            //ucUpdateCtl.AutoScroll = true;
            ////ucUpdateCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            //ucUpdateCtl.Location = new System.Drawing.Point(0, 0);
            //ucUpdateCtl.Size = new System.Drawing.Size(681, 177);
            //this.Controls.Add(ucUpdateCtl);
        }

        private void vcUPDATECTL1_Load(object sender, EventArgs e)
        {
            string wArchivoTxt = "update3Z1";
      
            vcUPDATECTL1.configFileURL = "http://" + Url + "/descargas320/" + wArchivoTxt + ".txt";
        }

private void  vcUPDATECTL1_CloseRequest(string DownloadedFile)
{
    //Dim lPid As Long
    //Dim lHnd As Long
    //Dim lRet As Long
            
    //'Dim fs As Scripting.FileSystemObject
    
    if (DownloadedFile != "") //'they did download an update
    {
        //Screen.MousePointer = vbHourglass
        
        //'- Paso 2 Descomprime el RAR autoejecutable -
        //lPid = Shell(DownloadedFile, vbNormalFocus)
        //If lPid <> 0 Then
        //    lHnd = OpenProcess(SYNCHRONIZE, 0, lPid)       'Get a handle to the shelled process.
        //    If lHnd <> 0 Then                              'If successful, wait for the
        //        lRet = WaitForSingleObject(lHnd, INFINITE) ' application to end.
        //        CloseHandle (lHnd)                         'Close the handle.
        //    End If
        //    'MsgBox "Just terminated.", vbInformation, "Shelled Application"
        //End If
                            
        //Screen.MousePointer = vbDefault
               
        //Kill DownloadedFile
        
    }
    
    //Unload Me
    

    
}

    }
}

