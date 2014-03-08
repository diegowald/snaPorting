using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Catalogo._recibos
{
    /// <summary>
    /// Lógica de interacción para frmRecibo.xaml
    /// </summary>
    public partial class frmRecibo : Window
    {
        const string m_sMODULENAME_ = "frmRecibo";
       
        public frmRecibo()
        {
            InitializeComponent();
        }

        private void btnReciboIniciar_Click(object sender, RoutedEventArgs e)
        {
               
            if (this.btnReciboIniciar.Tag.ToString()=="INICIAR") 
            {
                //vg.auditor.Guardar Recibo, INICIA               
                //TotalRecibo
                //TotalADeducir
                //TotalApli
                //txtObservaciones.Text = ""
                //lvValores.ListItems.Clear
                //lvAplicacion.ListItems.Clear
                //lvADeducir.ListItems.Clear

                AbrirRecibo();
                HabilitarRecibo();               
            }
            else
            { 
                if (MessageBox.Show("¿Esta Seguro que quiere CANCELAR el Recibo?","ATENCIÓN",MessageBoxButton.YesNo,MessageBoxImage.Question,MessageBoxResult.No,MessageBoxOptions.DefaultDesktopOnly)==MessageBoxResult.Yes )
                {
                //vg.auditor.Guardar Recibo, CANCELA
                //txtObservaciones.Text = ""
                //lvValores.ListItems.Clear
                //lvAplicacion.ListItems.Clear
                //lvADeducir.ListItems.Clear
            
                CerrarRecibo();
                InhabilitarRecibo();
                };
            };

        }

        private void CerrarRecibo()
        {

            //-------- ErrorGuardian Begin --------
            const string PROCNAME_ = "CerrarRecibo";
            // ERROR: Not supported in C#: OnErrorStatement

            //-------- ErrorGuardian End ----------

            this.btnReciboIniciar.Content = "Iniciar";
            this.btnReciboIniciar.Tag = "INICIAR";
            this.btnReciboIniciar.ToolTip = "INICIAR Recibo Nuevo";
            //cboEnvios_Click();

            //-------- ErrorGuardian Begin --------
            return;
        //ErrorGuardianLocalHandler:

            //switch (Catalogo.Funciones.Util.ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_))
            //{
            //    case Microsoft.VisualBasic.Constants.vbRetry:
            //        // ERROR: Not supported in C#: ResumeStatement

            //        break;
            //    case Microsoft.VisualBasic.Constants.vbIgnore:
            //        // ERROR: Not supported in C#: ResumeStatement

            //        break;
            //}
            //-------- ErrorGuardian End ----------

        }

        private void AbrirRecibo()
        {
            // ERROR: Not supported in C#: OnErrorStatement

            this.btnReciboIniciar.Content = "CANCELAR";
            this.btnReciboIniciar.Tag = "CANCELAR";
            this.btnReciboIniciar.ToolTip = "Cancelar el Recibo";

        }

        private void HabilitarRecibo()
        {

            // ERROR: Not supported in C#: OnErrorStatement

            this.grCargaValores.IsEnabled = true;

            this.btnReciboImprimir.IsEnabled = true;
            this.btnReciboVer.IsEnabled = true;

            this.TabRecibos.SelectedIndex = 1;

            //lvValores.Enabled = true;
            //lvAplicacion.Enabled = true;
            //lvADeducir.Enabled = true;

            //EtiquetasRecibos.Enabled = true;
            //txtObservaciones.Enabled = true;

        }

        private void InhabilitarRecibo()
        {

            // ERROR: Not supported in C#: OnErrorStatement

             this.grCargaValores.IsEnabled = false;

            //Desactivar LISTVIEW o GRID
            //lvValores.Enabled = false;
            //lvAplicacion.Enabled = false;
            //lvADeducir.Enabled = false;

            this.btnReciboImprimir.IsEnabled = false;
            this.btnReciboVer.IsEnabled = false;

            this.TabRecibos.SelectedIndex = 0;

            // Desactivar TABs this.TabRecibos.

           //txtObservaciones.Enabled = false;

        }

        private void lstViewEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void pnlMainGrid_MouseUp(object sender, MouseButtonEventArgs e)
        //{
        //    MessageBox.Show("You clicked me at " + e.GetPosition(this).ToString());

        //    MessageBox.Show("You clicked me at " + e.GetPosition(this).ToString(), "Event Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
        //}



        //private void TotalRecibo()
        //{

        //    //-------- ErrorGuardian Begin --------
        //    const string PROCNAME_ = "TotalRecibo";
        //    // ERROR: Not supported in C#: OnErrorStatement

        //    //-------- ErrorGuardian End ----------

        //    float Aux = 0;

        //    if (lvValores.ListItems.Count < 1)
        //    {
        //        lblTotRecibo.Caption = Strings.Format(0, "fixed");
        //        return;
        //    }

        //    Aux = 0;
        //    for (m.I = 1; m.I <= lvValores.ListItems.Count; m.I++)
        //    {

        //        if (lvValores.ListItems(m.I).SubItems(10) == 3 | lvValores.ListItems(m.I).SubItems(10) == 4)
        //        {
        //            Aux = Aux + (Convert.ToSingle(lvValores.ListItems(m.I).SubItems(1)) * Convert.ToSingle(lvValores.ListItems(m.I).SubItems(12)));
        //        }
        //        else
        //        {
        //            Aux = Aux + Convert.ToSingle(lvValores.ListItems(m.I).SubItems(1));
        //            // simatoria de subtotal
        //        }

        //    }

        //    lblTotRecibo.Caption = Strings.Format(Aux, "fixed");
        //    //lblTotRecibo.Left = frmrecibolista.Width - lblTotRecibo.Width - lblTotRecibo.Width - 300
        //    //-------- ErrorGuardian Begin --------
        //    return;
        //ErrorGuardianLocalHandler:

        //    switch (ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_))
        //    {
        //        case Constants.vbRetry:
        //            // ERROR: Not supported in C#: ResumeStatement

        //            break;
        //        case Constants.vbIgnore:
        //            // ERROR: Not supported in C#: ResumeStatement

        //            break;
        //    }
        //    //-------- ErrorGuardian End ----------

        //}
        
        //private void TotalADeducir()
        //{

        //    //-------- ErrorGuardian Begin --------
        //    const string PROCNAME_ = "TotalADeducir";
        //    // ERROR: Not supported in C#: OnErrorStatement

        //    //-------- ErrorGuardian End ----------

        //    float AuxNotaCredito = 0;
        //    float Aux3 = 0;
        //    float Aux2 = 0;
        //    float Aux = 0;

        //    if (lvADeducir.ListItems.Count < 1)
        //    {
        //        lblTotaDedu.Caption = Strings.Format(0, "fixed");
        //        lblTotaDeduAlResto.Caption = Strings.Format(0, "fixed");
        //        return;
        //    }

        //    AuxNotaCredito = 0;
        //    Aux3 = 0;
        //    Aux2 = 0;
        //    Aux = 0;

        //    lvADeducir.Sorted = true;
        //    lvADeducir.SortKey = 3;
        //    lvADeducir.SortOrder = lvwAscending;

        //    for (m.I = 1; m.I <= lvADeducir.ListItems.Count; m.I++)
        //    {
        //        //es un porcentaje yNO al resto
        //        if (lvADeducir.ListItems(m.I).SubItems(2) == 1 & lvADeducir.ListItems(m.I).SubItems(3) == 0)
        //        {

        //            Aux = Aux + (Convert.ToSingle(lvADeducir.ListItems(m.I).SubItems(1)) * Convert.ToSingle(Strings.Replace(lblTotApli.Caption, "$", ""))) / 100;

        //            //es un importe yNO al resto
        //        }
        //        else if (lvADeducir.ListItems(m.I).SubItems(2) == 0 & lvADeducir.ListItems(m.I).SubItems(3) == 0)
        //        {

        //            Aux = Aux + Convert.ToSingle(lvADeducir.ListItems(m.I).SubItems(1));
        //            // sumatoria de subtotal

        //            if (Strings.Left(lvADeducir.ListItems(m.I).Text, 4) == "CRE-")
        //                AuxNotaCredito = AuxNotaCredito + Convert.ToSingle(lvADeducir.ListItems(m.I).SubItems(1));
        //            // sumatoria de subtotal

        //        }

        //        //es un porcentaje ySI al resto
        //        if (lvADeducir.ListItems(m.I).SubItems(2) == 1 & lvADeducir.ListItems(m.I).SubItems(3) == 1)
        //        {

        //            if (Convert.ToSingle(lvADeducir.ListItems(m.I).SubItems(1)) > 0)
        //            {
        //                Aux2 = ((Convert.ToSingle(Strings.Replace(lblTotApli.Caption, "$", "")) - Convert.ToSingle(Aux) - Convert.ToSingle(Aux2) + Convert.ToSingle(AuxNotaCredito)) * Convert.ToSingle(lvADeducir.ListItems(m.I).SubItems(1))) / 100;
        //                Aux3 = Aux3 + Aux2;
        //            }

        //        }

        //    }

        //    lblTotaDedu.Caption = Strings.Format(Aux, "fixed");
        //    lblTotaDeduAlResto.Caption = Strings.Format(Aux3, "fixed");

        //    //lblTotRecibo.Left = frmrecibolista.Width - lblTotRecibo.Width - lblTotRecibo.Width - 300
        //    //-------- ErrorGuardian Begin --------
        //    return;
        //ErrorGuardianLocalHandler:

        //    switch (ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_))
        //    {
        //        case Constants.vbRetry:
        //            // ERROR: Not supported in C#: ResumeStatement

        //            break;
        //        case Constants.vbIgnore:
        //            // ERROR: Not supported in C#: ResumeStatement

        //            break;
        //    }
        //    //-------- ErrorGuardian End ----------

        //}

        //private void TotalApli()
        //{

        //    //-------- ErrorGuardian Begin --------
        //    const string PROCNAME_ = "TotalApli";
        //    // ERROR: Not supported in C#: OnErrorStatement

        //    //-------- ErrorGuardian End ----------

        //    float Aux = 0;
        //    float AuxP = 0;

        //    if (lvAplicacion.ListItems.Count < 1)
        //    {
        //        lblTotApli.Caption = Strings.Format(0, "fixed");
        //        lblTotPercep.Caption = Strings.Format(0, "fixed");
        //        return;
        //    }

        //    Aux = 0;
        //    AuxP = 0;
        //    for (m.I = 1; m.I <= lvAplicacion.ListItems.Count; m.I++)
        //    {
        //        Aux = Aux + Convert.ToSingle(lvAplicacion.ListItems(m.I).SubItems(1));
        //        // sumatoria de subtotal
        //        AuxP = AuxP + Convert.ToSingle(lvAplicacion.ListItems(m.I).SubItems(2));
        //        // sumatoria de subtotal de Percepciones
        //    }

        //    lblTotApli.Caption = Strings.Format(Aux, "fixed");
        //    lblTotPercep.Caption = Strings.Format(AuxP, "fixed");

        //    //lblTotRecibo.Left = frmrecibolista.Width - lblTotRecibo.Width - lblTotRecibo.Width - 300
        //    //-------- ErrorGuardian Begin --------
        //    return;
        //ErrorGuardianLocalHandler:

        //    switch (ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_))
        //    {
        //        case Constants.vbRetry:
        //            // ERROR: Not supported in C#: ResumeStatement

        //            break;
        //        case Constants.vbIgnore:
        //            // ERROR: Not supported in C#: ResumeStatement

        //            break;
        //    }
        //    //-------- ErrorGuardian End ----------

        //}

        //private void VerDetalleRecibo()
        //{

        //    //-------- ErrorGuardian Begin --------
        //    const string PROCNAME_ = "VerDetalleRecibo";
        //    // ERROR: Not supported in C#: OnErrorStatement

        //    //-------- ErrorGuardian End ----------

        //    //-------- ErrorGuardian Begin --------
        //    return;
        //ErrorGuardianLocalHandler:

        //    switch (ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_))
        //    {
        //        case Constants.vbRetry:
        //            // ERROR: Not supported in C#: ResumeStatement

        //            break;
        //        case Constants.vbIgnore:
        //            // ERROR: Not supported in C#: ResumeStatement

        //            break;
        //    }
        //    //-------- ErrorGuardian End ----------

        //}

        //private int CalculaDiasCascara()
        //{
        //    int functionReturnValue = 0;

        //    //-------- ErrorGuardian Begin --------
        //    const string PROCNAME_ = "CalculaDiasCascara";
        //    // ERROR: Not supported in C#: OnErrorStatement

        //    //-------- ErrorGuardian End ----------

        //    functionReturnValue = 0;
        //    return functionReturnValue;
        //ErrorGuardianLocalHandler:

        //    //-------- ErrorGuardian Begin --------

        //    switch (ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_))
        //    {
        //        case Constants.vbRetry:
        //            // ERROR: Not supported in C#: ResumeStatement

        //            break;
        //        case Constants.vbIgnore:
        //            // ERROR: Not supported in C#: ResumeStatement

        //            break;
        //    }
        //    return functionReturnValue;
        //    //-------- ErrorGuardian End ----------

        //}

        //private void cmdCascara_Click()
        //{

        //    int wDias = 0;
        //    string wTipoCascara = null;
        //    byte xI = 0;

        //    txtDeduConcepto.Text = "";

        //    m.I = 0;

        //}
    }
}
