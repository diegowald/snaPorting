using System;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;

namespace Catalogo
{
    public partial class fReporte : Form
    {

        public ReportDocument oRpt = null;
 
        public fReporte()
        {
            InitializeComponent();

            crViewer1.ReportSource = oRpt;
            crViewer1.Refresh();
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
