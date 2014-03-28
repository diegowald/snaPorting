using System;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace Catalogo
{
    public partial class fReporte : Form
    {

        public ReportDocument oRpt=null;
 
        public fReporte()
        {
            InitializeComponent();

        }

        private void fReporte_Load(object sender, EventArgs e)
        {

            //if (Right(miReport.ReportTitle, 14) == "09999-99999999")
            crViewer1.ReportSource = oRpt;
            crViewer1.Refresh();

        }

        private void btnVerPDF_Click(object sender, EventArgs e)
        {

            //           if (Funciones.modINIs.ReadINI("DATOS", "RptPdf", "0") == "1")
            //{

            //    oReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Global01.AppPath + "\\pdf\\P" + NroPedido + ".pdf");
 
            //    System.Diagnostics.Process.Start(Global01.AppPath + "\\pdf\\P" + NroPedido + ".pdf");

            //    if (odsPedidos1.Tables[0].Rows[0]["EMAIL"].ToString().Trim().Length > 0)
            //    {
            //        CrystalDecisions.Shared.ExportOptions ExpOpts = new CrystalDecisions.Shared.ExportOptions();
            //        CrystalDecisions.Shared.HTMLFormatOptions htmlopts = new CrystalDecisions.Shared.HTMLFormatOptions();
            //        CrystalDecisions.Shared.MicrosoftMailDestinationOptions MailOpts = new CrystalDecisions.Shared.MicrosoftMailDestinationOptions();

            //        ExpOpts = oReport.ExportOptions;
            //        ExpOpts.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.MicrosoftMail;
            //        ExpOpts.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.RichText;

            //        MailOpts.MailToList = "pbrugnie@hotmail.com"; //odsPedidos1.Tables[0].Rows[0]["EMAIL"].ToString()
            //        MailOpts.MailSubject = "auto náutica sur - nota de venta n° " + NroPedido;
            //        MailOpts.MailCCList = odsPedidos1.Tables[0].Rows[0]["RAZONSONSOCIAL"].ToString();
            //        MailOpts.UserName = "intldwilliams";
            //        MailOpts.Password = "yourpassword";
            //        ExpOpts.DestinationOptions = MailOpts;
            //        oReport.Export(ExpOpts);
            //    }

            //}
            //else
            //{

        }

        private void btnEnviarMail_Click(object sender, EventArgs e)
        {

        }


   //     private void fReporte_Load(object sender, EventArgs e)
   //     {

        
   //Cursor.Current = Cursors.WaitCursor;
           
   // if (Right(miReport.ReportTitle, 14) == "09999-99999999")
   // if (true)
   // {
   //     crViewer1.EnableExportButton = false;
   //     crViewer1.EnableToolbar = true;
   //     crViewer1.EnablePopupMenu = false;
   //     crViewer1.EnableExportButton = false;
   //     crViewer1.EnableGroupTree = false;
   //     crViewer1.EnablePrintButton = false;
        
   //     crViewer1.DisplayGroupTree = false;
   //     crViewer1.DisplayTabs = false;
   //     crViewer1.DisplayBorder = true;
   //     crViewer1.DisplayBackgroundEdge = false;
   //     crViewer1.DisplayToolbar = true;
   // }
   // else
   // {
   //     crViewer1.EnableExportButton = false;
   //     crViewer1.EnableToolbar = true;
   //     crViewer1.EnablePopupMenu = false;
   //     crViewer1.EnableExportButton = true;
   //     crViewer1.DisplayGroupTree = false;
   //     crViewer1.DisplayTabs = false;
   //     crViewer1.DisplayBorder = true;
   //     crViewer1.DisplayBackgroundEdge = false;
   //     crViewer1.DisplayToolbar = true;
   // }
        
   // crViewer1.ViewReport
    
   // If miReport.PaperOrientation = crLandscape Then
   //     crViewer1.Zoom (75)
   // Else
   //     crViewer1.Zoom (100)
   // End If
    
   //   Cursor.Current = Cursors.Default;

   //     }

    }
}
