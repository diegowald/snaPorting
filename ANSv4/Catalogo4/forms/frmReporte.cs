using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using CrystalDecisions.CrystalReports.Engine;

namespace Catalogo
{
    
    public partial class fReporte : Form
    {
        public ReportDocument oRpt = null;
        public string DocumentoNro = "";
        public string EmailTO = "";
        public string EmailAsunto = "";
        public string RazonSocial = "";

        public fReporte()
        {
            InitializeComponent();

        }

        private void fReporte_Load(object sender, EventArgs e)
        {
            if (DocumentoNro.Substring(DocumentoNro.ToString().Trim().Length - 8) == "99999999")
            {
                btnVerPDF.Enabled = true;
                btnEnviarMail.Enabled = true;
                crViewer1.ShowPrintButton = false;
            }
            else
            {
                btnVerPDF.Enabled = true;
                btnEnviarMail.Enabled = true;
                crViewer1.ShowPrintButton = true;
            }

            crViewer1.ReportSource = oRpt;
            crViewer1.Refresh();

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

                System.Diagnostics.Process.Start(Global01.AppPath + "\\pdf\\" + DocumentoNro + ".pdf");

                if (EmailTO.Trim().Length > 0)
                {
                    CrystalDecisions.Shared.ExportOptions ExpOpts = new CrystalDecisions.Shared.ExportOptions();
                    CrystalDecisions.Shared.MicrosoftMailDestinationOptions MailOpts = new CrystalDecisions.Shared.MicrosoftMailDestinationOptions();

                    ExpOpts = oRpt.ExportOptions;
                    ExpOpts.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.MicrosoftMail;
                    ExpOpts.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat;

                    MailOpts.MailMessage = EmailAsunto.ToString();
                    MailOpts.MailToList = EmailTO.ToString();
                    MailOpts.MailSubject = EmailAsunto.ToString();
                    MailOpts.MailCCList = RazonSocial.ToString();
                    MailOpts.UserName = "intldwilliams";
                    MailOpts.Password = "yourpassword";
                    ExpOpts.DestinationOptions = MailOpts;
                    oRpt.Export(ExpOpts);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                util.errorHandling.ErrorLogger.LogMessage(ex);

            }
        }


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
