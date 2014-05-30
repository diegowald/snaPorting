using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CrystalDecisions.CrystalReports.Engine;
using System.Diagnostics;
//using CrystalDecisions.ReportAppServer.DataDefModel;
//using CrystalDecisions.Shared;
//using CrystalDecisions.ReportAppServer.ClientDoc; 

namespace Catalogo.varios
{
    
    public partial class fReporte : Form
    {
        public ReportDocument oRpt = null;
        public string DocumentoNro = "";


        public fReporte()
        {
            InitializeComponent();
        }

        private void fReporte_Load(object sender, EventArgs e)
        {          
            if (DocumentoNro.Substring(DocumentoNro.ToString().Trim().Length - 8) == "99999999")
            {
                btnVerPDF.Enabled = false;
                btnEnviarMail.Enabled = false;
                crViewer1.ShowPrintButton = false;
            }
            else
            {
                btnVerPDF.Enabled = true;
                btnEnviarMail.Enabled = true;
                crViewer1.ShowPrintButton = true;
            }            
            crViewer1.ReportSource = oRpt;
            //using (new varios.WaitCursor())
            //{
                crViewer1.Visible = true;
                Cursor.Current = Cursors.Default;
                crViewer1.Show(); //.Refresh();
            //}
        }

        private void btnVerPDF_Click(object sender, EventArgs e)
        {
            try
            {
                oRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Global01.AppPath + "\\pdf\\" + DocumentoNro + ".pdf");
                System.Diagnostics.Process.Start(Global01.AppPath + "\\pdf\\" + DocumentoNro + ".pdf");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                util.errorHandling.ErrorLogger.LogMessage(ex);
            }
        }

        private void btnEnviarMail_Click(object sender, EventArgs e)
        {
            try
            {
                oRpt.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Global01.AppPath + "\\pdf\\" + DocumentoNro + ".pdf");
                
                Global01.EmailAsunto = this.Text;
                //Global01.EmailTO = "sistemas@autonauticasur.com.ar";
                Global01.EmailBody = this.Text;

                if (Global01.EmailTO.Trim().Length==0) Global01.EmailTO = "@";

                if (Global01.EmailTO.Trim().Length > 0)
                {
                    Catalogo.util.SendFileTo.MAPI mapi = new Catalogo.util.SendFileTo.MAPI();
                    mapi.AddRecipientTo(Global01.EmailTO.ToString());

                    mapi.AddAttachment(Global01.AppPath + "\\pdf\\" + DocumentoNro + ".pdf");
                    mapi.SendMailPopup(Global01.EmailAsunto.ToString(), Global01.EmailBody.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                util.errorHandling.ErrorLogger.LogMessage(ex);
            }
        }
      
        //internal void SendSupportEmail(string emailAddress, string subject, string body)
        //{
        //    Process.Start("mailto:" + emailAddress + "?subject=" + subject + "&body="
        //       + body + "&Attach=" + Global01.AppPath + "\\pdf\\" + DocumentoNro + ".pdf");
        //}


        //private void fReporte_MouseDown(object sender, MouseEventArgs e)
        //{
        //    if (eventArgs.ChangedButton == MouseButton.Left)
        //    {
        //        this.DragMove();
        //    }
        //}

        //public FormulaField FindFormulaField (ReportClientDocument oReportClientDocument, string szFormula) 
        //{
        //    int iIndex; 

        //    FormulaField oFormulaField; 
        //    oFormulaField =   new FormulaFieldClass (); 

        //    Fields oFields; 
        //    oFields = oReportClientDocument.DataDefinition.FormulaFields; 

        //    iIndex = oFields.Find (szFormula, CrFieldDisplayNameTypeEnum.crFieldDisplayNameName, CeLocale.ceLocaleUserDefault); 
        //    oFormulaField = ((FormulaField) oFields [iIndex]); 

        //    return oFormulaField; 
        //}

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
