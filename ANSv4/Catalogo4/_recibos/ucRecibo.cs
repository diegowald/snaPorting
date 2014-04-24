﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo._recibos
{
    public partial class ucRecibo : UserControl,
        Funciones.emitter_receiver.IEmisor<int>, // Para enviar el indice del cliente seleccionado en el combo
        Funciones.emitter_receiver.IReceptor<int> // Para recibir una notificacion de cambio del cliente seleccionado
    {
        //private //const string m_sMODULENAME_ = "ucRecibo";
        ToolTip _ToolTip = new System.Windows.Forms.ToolTip();

        public ucRecibo()
        {
            InitializeComponent();
            _ToolTip.SetToolTip(btnIniciar, "INICIAR Recibo Nuevo");
            _ToolTip.SetToolTip(btnImprimir, "Graba e Imprime el Recibo ...");
            _ToolTip.SetToolTip(btnVer, "ver ...");
            _ToolTip.SetToolTip(btnResumen, "Detalle del Recibo ...");

            ccActualizadaFechaLbl.Text = "Cta. Cte. actualizada al " + Global01.F_ActClientes.ToString("dd/MM/yyyy HH:mm");

            if (!Global01.AppActiva)
            {
                this.Dispose();
            }

            if (Funciones.modINIs.ReadINI("DATOS", "EsGerente", "0") == "1")
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1", "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Trim(cstr(ID)) & ')' as Cliente, ID");
            }
            else
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1 and IdViajante=" + Global01.NroUsuario.ToString(), "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Format([ID],'00000') & ')' AS Cliente, ID");
            }

            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cvTipoValorCbo, "v_TipoValor", "D_valor", "IDvalor", "ALL", "IDvalor", true, false, "NONE");
            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cvBancoCbo, "tblBancos", "Banco", "ID", "Activo=0", "Format([ID],'000') & ' - ' & tblBancos.Nombre", true, false, "Format([ID],'000') & ' - ' & tblBancos.Nombre AS Banco, ID");            

        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            paEnviosCbo.SelectedIndex = -1;

            if (cboCliente.SelectedIndex > 0)
            {
                
                toolStripStatusLabel1.Text = "Recibo para el cliente: " + this.cboCliente.Text.ToString();
                CargarCtaCte();                
                CargarClienteNovedades();                
                CargarClienteDatos();
                btnIniciar.Enabled = true;
                paEnviosCbo.SelectedIndex = 2;

            }
            else 
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Recibo para el cliente ..."; }
                LimpiarClienteDatos();
                btnIniciar.Enabled = false;
            }
            this.emitir(cboCliente.SelectedIndex);
        }

        private void LimpiarClienteDatos()
        {
            cclistView.Items.Clear();
            //this.CliNDataGridView.DataSource = null;

            CliDCuitTxt.Text = "";
            CliDDomicilioTxt.Text = "";
            CliDEmailTxt.Text = "";
            CliDLocalidadTxt.Text = "";
            CliDNroCuentaTxt.Text = "";
            CliDObservacionesTxt.Text = "";
            CliDRazonSocialTxt.Text = "";
            CliDTelefonoTxt.Text = "";
        }

        private void CargarCtaCte()
        {
            DataTable dt =  Catalogo.Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "v_CtaCte", "IDCliente=" + cboCliente.SelectedValue.ToString(), "Orden, Fecha");
            
            cclistView.Visible = false;
            cclistView.Items.Clear();
            float wAcumulado = 0;
            for (int i = 0; i < dt.Rows.Count; i++)            {                
                DataRow dr = dt.Rows[i];

                ListViewItem ItemX =  new ListViewItem(string.Format("{0:dd/MM/yyyy}",DateTime.Parse(dr["Fecha"].ToString())));

                //alternate row color
                if (i % 2 == 0)
                {
                    ItemX.BackColor = Color.White;
                }
                else 
                {
                    ItemX.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
                }

                //ItemX.SubItems["1"].Text  = dr["Comprobante"].Text;
                ItemX.SubItems.Add(dr["Comprobante"].ToString());
                ItemX.SubItems.Add(string.Format("{0:N2}",float.Parse(dr["Importe"].ToString())));
                ItemX.SubItems.Add(string.Format("{0:N2}",float.Parse(dr["Saldo"].ToString())));

                wAcumulado = wAcumulado + float.Parse(dr["SaldoS"].ToString());
                ItemX.SubItems.Add(string.Format("{0:N2}", float.Parse(wAcumulado.ToString())));

                ItemX.SubItems.Add(dr["Det_Comprobante"].ToString());
                ItemX.SubItems.Add(string.Format("{0:N2}",float.Parse(dr["ImpOferta"].ToString())));
                ItemX.SubItems.Add(dr["TextoOferta"].ToString());
                ItemX.SubItems.Add(dr["Vencida"].ToString());
                ItemX.SubItems.Add(string.Format("{0:N2}",float.Parse(dr["ImpPercep"].ToString())));
                ItemX.SubItems.Add(dr["IdCliente"].ToString());
                ItemX.SubItems.Add((DBNull.Value.Equals(dr["EstaAplicada"]) ? "N" : "S"));
                ItemX.SubItems.Add((DBNull.Value.Equals(dr["EsContado"]) ? "0" : dr["EsContado"].ToString()));

                if (ItemX.SubItems[1].Text.Substring(0, 3).ToUpper()== "DEB")
                {
                    ItemX.SubItems[1].ForeColor = Color.Red;
                    ItemX.SubItems[1].Font = new Font(cclistView.Font, FontStyle.Bold);
                }
                cclistView.Items.Add(ItemX);            
            }

            if (dt.Rows.Count>0) Funciones.util.AutoSizeLVColumnas(ref cclistView);

            cclistView.Visible = true;
            dt = null;

        }

        private void CargarClienteNovedades()
        {
            //this.CliNDataGridView.DataSource = Catalogo.Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "tblClientesNovedades", "IDCliente=" + cboCliente.SelectedValue.ToString(), "F_Carga");
        }

        private void CargarClienteDatos()
        {
            CliDCuitTxt.Text = "";
            CliDDomicilioTxt.Text = "";
            CliDEmailTxt.Text = "";
            CliDLocalidadTxt.Text = "";
            CliDNroCuentaTxt.Text = "";
            CliDObservacionesTxt.Text = "";
            CliDRazonSocialTxt.Text = "";
            CliDTelefonoTxt.Text = "";

            OleDbDataReader dr = null;
            dr = Funciones.oleDbFunciones.Comando(Global01.Conexion, "SELECT * FROM tblClientes WHERE ID=" + cboCliente.SelectedValue.ToString());
            if (dr.HasRows)
            {
                dr.Read();

                CliDCuitTxt.Text = dr["cuit"].ToString();
                CliDDomicilioTxt.Text = dr["domicilio"].ToString();
                CliDEmailTxt.Text = dr["email"].ToString();
                CliDLocalidadTxt.Text = dr["ciudad"].ToString();
                CliDNroCuentaTxt.Text = dr["ID"].ToString();
                CliDObservacionesTxt.Text = dr["observaciones"].ToString();
                CliDRazonSocialTxt.Text = dr["razonsocial"].ToString();
                CliDTelefonoTxt.Text = dr["telefono"].ToString();
            }
  
        }

        private void CliDPnlMain_DoubleClick(object sender, EventArgs e)
        {
            if (CliDEmailTxt.Text.Trim().Length > 0)
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "mailto:" + CliDEmailTxt.Text.Trim() + "?subject=" + "auto náutica sur - OdN n° " + Global01.NroUsuario.ToString() + "&body=" + "mi estimado...";
                proc.Start();
            }
        }

        private void cvAgregarBtn_Click(object sender, EventArgs e)
        {
            if (datosvalidos("recibo"))
            {
                ListViewItem ItemX;
                
                if (ralistView.Tag.ToString() == "upd")
                {
                    if (ralistView.SelectedItems != null & ralistView.SelectedItems.Count > 0) 
                    {
                      ItemX = ralistView.SelectedItems[0];
                    }
                    else 
                    {
                      ItemX =  new ListViewItem(cvTipoValorCbo.Text);
                      ralistView.Tag = "add";
                    }
                }
                else 
                {
                   ItemX =  new ListViewItem(cvTipoValorCbo.Text);
                }

                //alternate row color
                if (ralistView.Items.Count % 2 == 0)
                {
                    ItemX.BackColor = Color.White;
                }
                else
                {
                    ItemX.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
                }

                ItemX.Tag = "";
                ItemX.SubItems.Add(string.Format("{0:N2}",float.Parse(cvImporteTxt.Text))); 
                ItemX.SubItems.Add(cvFecEmiDt.Text); 
                ItemX.SubItems.Add(cvFecCobroDt.Text); 
                ItemX.SubItems.Add(cvNroChequeTxt.Text); 
                ItemX.SubItems.Add(cvNroCuentaTxt.Text);
                ItemX.SubItems.Add(((cvBancoCbo.SelectedIndex <= 0) ? "" : cvBancoCbo.Text)); 
                ItemX.SubItems.Add(cvCpaTxt.Text);
                ItemX.SubItems.Add(cvDeTerceroChk.Text);
                ItemX.SubItems.Add(cvABahiaChk.Text);
                ItemX.SubItems.Add(cvTipoValorCbo.SelectedValue.ToString());
                ItemX.SubItems.Add(((cvBancoCbo.SelectedIndex <= 0) ? "0" : cvBancoCbo.SelectedValue.ToString())); 
                ItemX.SubItems.Add(cvTipoCambioTxt.Text); 

                ralistView.Items.Add(ItemX);

                Funciones.util.AutoSizeLVColumnas(ref ralistView);

                LimpiarIngresosValores();

                TotalRecibo();

            }
        }

        private bool datosvalidos(string pCampo)
        {

            bool wDatosValidos = true;
            string s = "";

            if (pCampo.ToLower()=="cvtipovalorcbo" | pCampo.ToLower()=="all" | pCampo.ToLower()=="recibo" )
            {
                if (cvTipoValorCbo.SelectedIndex  <= 0 )
                {
                    MessageBox.Show("Ingrese Tipo de Valor", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    cvTipoValorCbo.Focus();
                }
            }   
              
            if (pCampo.ToLower()=="cvimportetxt" | pCampo.ToLower()=="all" | pCampo.ToLower()=="recibo" )
            {
                if (float.Parse( "0" + cvImporteTxt.Text) <= 0 )
                {
                    MessageBox.Show("Ingrese Importe", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    cvImporteTxt.Focus();
                }
                else 
                {
                    s = cvTipoValorCbo.SelectedText;
                    if (s.IndexOf("dólar") != -1 | s.IndexOf("euro") != -1)
                    {
                        if (float.Parse( "0" + cvTipoCambioTxt.Text) <= 0 )
                        {
                            MessageBox.Show("Ingrese Tipo de Cambio", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            wDatosValidos = false;
                            cvImporteTxt.Focus();
                        }
                        else 
                        {
                            cvDivisasLbl.Text = string.Format("{0:N2}",float.Parse(cvImporteTxt.Text) * float.Parse(cvTipoCambioTxt.Text));
                        }
                    }
                    else 
                    {
                        cvDivisasLbl.Text = "";
                    }
                }
            }

          if (cvTipoValorCbo.SelectedIndex==1) 
          {  //Cheque
              if (cvBancoCbo.SelectedIndex <= 0 | cvNroCuentaTxt.Text.Trim().Length == 0 | cvNroChequeTxt.Text.Trim().Length == 0)                      
            {
               MessageBox.Show("Ingrese Datos del Cheque", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               wDatosValidos = false;
               cvBancoCbo.Focus();
            }
          }
          else if (cvTipoValorCbo.SelectedIndex>=5) 
          { //retención               
            if (cvNroChequeTxt.Text.Trim().Length==0)                      
            {
                MessageBox.Show("Ingrese n° de Retención", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                wDatosValidos = false;
                cvNroCuentaTxt.Focus();
            }
          }

    
    //if LCase(pCampo) = "clinovedad" Or LCase(pCampo) = "#all" Or LCase(pCampo) = "novedad" {
    //    if Len(Trim(CliNovedad.Text)) = 0 {
    //        MsgBox "Ingrese Novedad", vbExclamation, "Atención"
    //        DatosValidos = False
    //        CliNovedad.SetFocus
    //    }
    //}
        
    //if LCase(pCampo) = "all" Or LCase(pCampo) = "aplicacion" Or LCase(pCampo) = "adeducir" {
    //    if BuscarIndiceEnListView(lvADeducir, "cascara:", 0) = -1 Or Left(Me.txtDeduConcepto.Text, 8) = "cascara:" {
    //       DatosValidos = True
    //    else {
    //        DatosValidos = False
    //        MsgBox "NO puede agregar aplicación, ni descuentos," & vbCrLf & "para ello debe quitar la cascara", vbOKOnly + vbInformation, "Datos Válidos"
    //    }
    //}
    
    //if LCase(pCampo) = "txtapliimporte" Or LCase(pCampo) = "all" Or LCase(pCampo) = "aplicacion" {
    //    if Len(Trim(txtApliImporte.Text)) = 0 {
    //        MsgBox "Ingrese Importe", vbExclamation, "Atención"
    //        DatosValidos = False
    //        txtApliImporte.SelStart = 0
    //        txtApliImporte.SelLength = Len(txtApliImporte.Text)
    //        txtApliImporte.SetFocus
    //    }
    //}
    
    //if LCase(pCampo) = "txtdeduimporte" Or LCase(pCampo) = "all" Or LCase(pCampo) = "adeducir" {
    //    if Len(Trim(txtDeduImporte.Text)) = 0 {
    //        MsgBox "Ingrese Importe", vbExclamation, "Atención"
    //        DatosValidos = False
    //        txtDeduImporte.SelStart = 0
    //        txtDeduImporte.SelLength = Len(txtDeduImporte.Text)
    //        txtDeduImporte.SetFocus
    //    }
    //}
        
            return wDatosValidos;
        }

        private void cvTipoValorCbo_SelectedIndexChanged(object sender, EventArgs e)
        {            
            if (cvTipoValorCbo.SelectedIndex==1)
            {   //Cheque
                cvNroChequeLbl.Text = "n° Cheque";
                cvFecEmiLbl.Text = "Fecha Emisión";

                cvFecEmiDt.Enabled = true;
                cvFecCobroDt.Enabled = true;
                cvNroChequeTxt.Enabled = true;
                cvDeTerceroChk.Enabled = true;
                cvNroCuentaTxt.Enabled = true;
                cvABahiaChk.Enabled = true;
                cvBancoCbo.Enabled = true;
                cvCpaTxt.Enabled = true;
                cvTipoCambioTxt.Enabled = false;                
            }
            else if (cvTipoValorCbo.SelectedIndex==2)
            {   //Efectivo
                cvFecEmiDt.Enabled = false;
                cvFecCobroDt.Enabled = false;
                cvNroChequeTxt.Enabled = false;
                cvDeTerceroChk.Enabled = false;
                cvNroCuentaTxt.Enabled = false;
                cvABahiaChk.Enabled = false;
                cvBancoCbo.Enabled = false;
                cvCpaTxt.Enabled = false;
                cvTipoCambioTxt.Enabled = false;                
            }
            else if (cvTipoValorCbo.SelectedIndex==3 | cvTipoValorCbo.SelectedIndex==4)
            {   //Divisas
                cvFecEmiDt.Enabled = false;
                cvFecCobroDt.Enabled = false;
                cvNroChequeTxt.Enabled = false;
                cvDeTerceroChk.Enabled = false;
                cvNroCuentaTxt.Enabled = false;
                cvABahiaChk.Enabled = false;
                cvBancoCbo.Enabled = false;
                cvCpaTxt.Enabled = false;
                cvTipoCambioTxt.Enabled = true;                
            }
            else if (cvTipoValorCbo.SelectedIndex >= 5)
            {   //Retenciones     
                cvNroChequeLbl.Text = "n° Retención";
                cvFecEmiLbl.Text = "Fecha Retención";

                cvFecEmiDt.Enabled = true;
                cvFecCobroDt.Enabled = false;
                cvNroChequeTxt.Enabled = true;
                cvDeTerceroChk.Enabled = false;
                cvNroCuentaTxt.Enabled = false;
                cvABahiaChk.Enabled = false;
                cvBancoCbo.Enabled = false;
                cvCpaTxt.Enabled = false;
                cvTipoCambioTxt.Enabled = false;
            }

            cvTipoCambioTxt.Text = "";
            cvDivisasLbl.Text = "";

            string s = cvTipoValorCbo.SelectedText;
            if (s.IndexOf("dólar") > 0)
            {
                cvTipoCambioTxt.Text = Global01.Dolar.ToString(); 
            }
            else if (s.IndexOf("euro") > 0)
            {
                cvTipoCambioTxt.Text = Global01.Euro.ToString();
            }

        }

        private void cvCancelarBtn_Click(object sender, EventArgs e)
        {
               LimpiarIngresosValores();
        }

        private void LimpiarIngresosValores()
        {
            ralistView.Tag = "add";

            cvTipoValorCbo.SelectedIndex  = 0;
            cvImporteTxt.Text = "0,00";
            cvTipoCambioTxt.Text = "";
            cvDivisasLbl.Text = "";
            cvFecEmiDt.Value = DateTime.Today.Date;
            cvFecCobroDt.Value = DateTime.Today.Date;
            cvNroChequeTxt.Text = "";
            cvNroCuentaTxt.Text = "";
            cvBancoCbo.SelectedIndex  = 0;
            cvCpaTxt.Text = "";
            cvDeTerceroChk.Checked  = false;
            cvABahiaChk.Checked = false;
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

            if (cboCliente.SelectedIndex > 0)
            {
                if (btnIniciar.Tag.ToString() == "INICIAR")
                {
                    _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Recibo,
                         _auditor.Auditor.AccionesAuditadas.INICIA);
                    //Limpio Listados
                    AbrirRecibo();

                    TotalRecibo();
                    TotalADeducir();
                    TotalApli();
                    
                    HabilitarRecibo();
                    rTabsRecibo.SelectedIndex = 3;
                    rTabsRecibo.Visible = true;
                }
                else
                {
                    if (MessageBox.Show("¿Esta Seguro que quiere CANCELAR el Recibo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Recibo,
                             _auditor.Auditor.AccionesAuditadas.CANCELA);

                        InhabilitarRecibo();

                        rTabsRecibo.Visible = false;
                        cboCliente.SelectedIndex = 0;
                        CerrarRecibo();                                                
                        TotalRecibo();
                        TotalADeducir();
                        TotalApli();
                    }
                }

                cclistView.Tag = "-1";
            }
            else
            {
              MessageBox.Show("Seleccione un Cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void TotalApli()
        {
            //const string PROCNAME_ = "TotalApli";

            float Aux = 0;
            float AuxP = 0;

            if (aplistView.Items.Count < 1)
            {
                apTotalAplicacionLbl.Text = string.Format("{0:N2}", 0);
                apTotalPercepcionLbl.Text = string.Format("{0:N2}", 0);
                return;
            }

            for (int i = 0; i < aplistView.Items.Count; i++)
            {
                Aux = Aux + float.Parse(aplistView.Items[i].SubItems[1].Text.ToString());
                // sumatoria de subtotal
                AuxP = AuxP + float.Parse(aplistView.Items[i].SubItems[2].Text.ToString());
                // sumatoria de subtotal de Percepciones
            }

            apTotalAplicacionLbl.Text = string.Format("{0:N2}", Aux);
            apTotalPercepcionLbl.Text = string.Format("{0:N2}", AuxP);

        }

        private void TotalADeducir()
        {
            //const string PROCNAME_ = "TotalADeducir";

            float AuxNotaCredito = 0;
            float Aux3 = 0;
            float Aux2 = 0;
            float Aux = 0;

            if (adlistView.Items.Count < 1)
            {
                adTotalDeducirLbl.Text = string.Format("{0:N2}", 0);
                adTotalDeducirAlRestoLbl.Text = string.Format("{0:N2}", 0);
                return;
            }

            ColumnClickEventArgs eArgs = new ColumnClickEventArgs(3);
            adlistView_ColumnClick(this, eArgs);           

            for (int i = 0; i < adlistView.Items.Count; i++)
            {
                //es un porcentaje yNO al resto
                if (adlistView.Items[i].SubItems[2].Text.ToString()=="1" & adlistView.Items[i].SubItems[3].Text.ToString()=="0")
                {

                    Aux = Aux + (float.Parse(adlistView.Items[i].SubItems[1].Text.ToString()) * float.Parse(apTotalAplicacionLbl.Text)) / 100;

                    //es un importe yNO al resto
                }
                else if (adlistView.Items[i].SubItems[2].Text.ToString() == "0" & adlistView.Items[i].SubItems[3].Text.ToString() == "0")
                {
                    Aux = Aux + float.Parse(adlistView.Items[i].SubItems[1].Text.ToString());
                    // sumatoria de subtotal

                    if (adlistView.Items[i].Text.Trim().Length >= 4 &&  adlistView.Items[i].Text.Substring(0, 4) == "CRE-")
                        AuxNotaCredito = AuxNotaCredito + float.Parse(adlistView.Items[i].SubItems[1].Text.ToString());
                    // sumatoria de subtotal

                }

                //es un porcentaje ySI al resto
                if (adlistView.Items[i].SubItems[2].Text.ToString() == "1" & adlistView.Items[i].SubItems[3].Text.ToString() == "1")
                {

                    if (float.Parse(adlistView.Items[i].SubItems[1].Text.ToString()) > 0)
                    {
                        Aux2 = ((float.Parse(apTotalAplicacionLbl.Text) - Aux - Aux2 + AuxNotaCredito ) * float.Parse(adlistView.Items[i].SubItems[1].Text.ToString())) / 100;
                        Aux3 = Aux3 + Aux2;
                    }
                }
            }

            adTotalDeducirLbl.Text = string.Format("{0:N2}", Aux);
            adTotalDeducirAlRestoLbl.Text = string.Format("{0:N2}", Aux3);

        }

        private void VerDetalleRecibo()
        {
            //const string PROCNAME_ = "VerDetalleRecibo";

            string s = "";
            float Dif = 0;


            Dif = ((float.Parse("0" + apTotalAplicacionLbl.Text.ToString()) + float.Parse("0" + adTotalDeducirLbl.Text.ToString()) - float.Parse("0" + adTotalDeducirAlRestoLbl.Text.ToString())) + float.Parse("0" + apTotalPercepcionLbl.Text.ToString())) - float.Parse("0" + raImporteTotalLbl.Text.ToString());

            s += "$ " + string.Format("{0:N2}", float.Parse(apTotalAplicacionLbl.Text)) + "   Aplicación \n\n";
            s += "$ " + string.Format("{0:N2}", float.Parse(adTotalDeducirLbl.Text)) + "   A Deducir \n\n";
            s += "$ " + string.Format("{0:N2}", float.Parse(adTotalDeducirAlRestoLbl.Text)) + "   A Deducir al Resto \n\n";
            s += "$ " + string.Format("{0:N2}", float.Parse(apTotalPercepcionLbl.Text)) + "   Percepciones/Débitos \n\n";
            s += "$ " + string.Format("{0:N2}", float.Parse(raImporteTotalLbl.Text)) + "   Valores \n\n\n";
            s += "$ " + string.Format("{0:N2}",Dif) + "   Diferencia \n\n";

            MessageBox.Show(s, "Detalle del Recibo", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }


        private void cvFecEmiDt_Leave(object sender, EventArgs e)
        {
            if (cvTipoValorCbo.SelectedIndex  >= 5)
            {
                cvFecCobroDt.Value = cvFecEmiDt.Value;
            }
        }

        private void ralistView_KeyDown(object sender, KeyEventArgs e)
        {
            if (ralistView.SelectedItems != null & ralistView.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.Delete)
                {  //DEL
                    ralistView.Items.Remove(ralistView.SelectedItems[0]);
                    TotalRecibo();
                }
            }
        }

        private void CerrarRecibo()
        {    
            btnIniciar.Text = "Iniciar";
            btnIniciar.Tag = "INICIAR";
            
            _ToolTip.SetToolTip(btnIniciar, "INICIAR Recibo Nuevo");
            raObservacionesTxt.Text = "";
            aplistView.Items.Clear();
            adlistView.Items.Clear();
            ralistView.Items.Clear();

            apTotalAplicacionLbl.Text = string.Format("{0:N2}", 0);
            adTotalDeducirLbl.Text = string.Format("{0:N2}", 0);
            adTotalDeducirAlRestoLbl.Text = string.Format("{0:N2}", 0);
            apTotalPercepcionLbl.Text = string.Format("{0:N2}", 0);
            raImporteTotalLbl.Text = string.Format("{0:N2}", 0);
            
            ObtenerMovimientos();

            //cboEnvios_Click();
        }

        private void AbrirRecibo()
        {
            btnIniciar.Text = "CANCELAR";
            btnIniciar.Tag = "CANCELAR";
            _ToolTip.SetToolTip(btnIniciar, "CANCELAR éste Recibo");
            raObservacionesTxt.Text = "";
            aplistView.Items.Clear();
            adlistView.Items.Clear();
            ralistView.Items.Clear();

            apTotalAplicacionLbl.Text = string.Format("{0:N2}", 0);
            adTotalDeducirLbl.Text = string.Format("{0:N2}", 0);
            adTotalDeducirAlRestoLbl.Text = string.Format("{0:N2}", 0);
            apTotalPercepcionLbl.Text = string.Format("{0:N2}", 0);
            raImporteTotalLbl.Text = string.Format("{0:N2}", 0);

        }

        private void HabilitarRecibo()
        {
            adPnlTop.Enabled = true;
            adPnlMain.Enabled = true;
            apPnlTop.Enabled = true;
            apPnlMain.Enabled = true;
            btnImprimir.Enabled = true;
            btnVer.Enabled = true;
            btnResumen.Enabled = true;
            ralistView.Enabled = true;
            aplistView.Enabled = true;
            adlistView.Enabled = true;
            raPnlBotton.Enabled = true;
            cboCliente.Enabled = false;
        }

        private void InhabilitarRecibo()
        {
            adPnlTop.Enabled = false;
            adPnlMain.Enabled = false;
            apPnlTop.Enabled = false;
            apPnlMain.Enabled = false;
            btnImprimir.Enabled = false;
            btnVer.Enabled = false;
            btnResumen.Enabled = false;
            ralistView.Enabled = false;
            aplistView.Enabled = false;
            adlistView.Enabled = false;
            raPnlBotton.Enabled = false;
            cboCliente.Enabled = true;
        }

        private void TotalRecibo()
        {
            float Aux = 0;

            if (ralistView.Items.Count < 1)
            {
                raImporteTotalLbl.Text = string.Format("{0:N2}", 0);
                return;
            }

            for (int i=0; i < ralistView.Items.Count; i++)
            {

                if (ralistView.Items[i].SubItems[10].Text=="3" | ralistView.Items[i].SubItems[10].Text=="4")
                {
                    Aux = Aux + (float.Parse("0" + ralistView.Items[i].SubItems[1].Text) * float.Parse("0" + ralistView.Items[i].SubItems[12].Text));
                }
                else
                {
                    Aux = Aux + float.Parse(ralistView.Items[i].SubItems[1].Text);
                    // sumatoria de subtotal
                }
            }

            raImporteTotalLbl.Text = string.Format("{0:N2}", Aux);
        }

        private void cvImporteTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
             Funciones.util.EsImporte(sender, ref e);
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {

            float Dif = 0;
            float tImporte = 0;
            bool wDeTerceroChk = false;
            bool wABahiaChk = false;
            bool tOk = true;


            //' iNABILITO nUEVO iNGRESO

            if (ralistView.Items.Count == 0 | aplistView.Items.Count == 0) 
            {
                MessageBox.Show("Faltan Valores o Aplicación", "atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);   
                tOk = false;
            }
            else
            {
                Dif = ((float.Parse("0" + apTotalAplicacionLbl.Text.ToString()) + float.Parse("0" + adTotalDeducirLbl.Text.ToString()) - float.Parse("0" + adTotalDeducirAlRestoLbl.Text.ToString())) + float.Parse("0" + apTotalPercepcionLbl.Text.ToString())) - float.Parse("0" + raImporteTotalLbl.Text.ToString());

                if (Dif > 0 && Dif > float.Parse(aplistView.Items[aplistView.Items.Count-1].SubItems[1].Text))
                {
                    MessageBox.Show("Agregar valores ó quitar última Factura en Aplicación", "atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);   
                    tOk = false;
                }
            }

            if (tOk)
            {

                InhabilitarRecibo();

                Catalogo._recibos.Recibo rec = new Catalogo._recibos.Recibo(Global01.Conexion, Global01.NroUsuario.ToString(), Int16.Parse(cboCliente.SelectedValue.ToString()));                

                for (int i = 0; i < ralistView.Items.Count; i++)
                {
                    wDeTerceroChk = (ralistView.Items[i].SubItems[8].Text.ToString() == "0" ? (bool)(true) : (bool)(false));
                    wABahiaChk = (ralistView.Items[i].SubItems[9].Text.ToString() == "1" ? (bool)(true) : (bool)(false));

                    rec.ADDItem(byte.Parse(ralistView.Items[i].SubItems[10].Text.ToString()),
                                    float.Parse(ralistView.Items[i].SubItems[1].Text.ToString()),
                                    DateTime.Parse(ralistView.Items[i].SubItems[2].Text.ToString()),
                                    DateTime.Parse(ralistView.Items[i].SubItems[3].Text.ToString()),
                                    ralistView.Items[i].SubItems[4].Text.ToString(),
                                    ralistView.Items[i].SubItems[5].Text.ToString(),
                                    Int16.Parse(ralistView.Items[i].SubItems[11].Text.ToString()),
                                    ralistView.Items[i].SubItems[7].Text.ToString(),
                                    wDeTerceroChk,
                                    float.Parse("0" + ralistView.Items[i].SubItems[12].Text.ToString()));

                    if (ralistView.Items[i].SubItems[10].Text.ToString() == "3" | ralistView.Items[i].SubItems[10].Text.ToString() == "4")
                    {
                        tImporte = tImporte + float.Parse(ralistView.Items[i].SubItems[1].Text.ToString()) * float.Parse(ralistView.Items[i].SubItems[12].Text.ToString());
                    }
                    else
                    {
                        tImporte = tImporte + float.Parse(ralistView.Items[i].SubItems[1].Text.ToString());
                    }
                }

                rec.NroImpresion = 0;
                rec.Bahia = wABahiaChk;
                rec.Observaciones = raObservacionesTxt.Text;
                rec.Total = tImporte;
                rec.Percepciones = float.Parse(apTotalPercepcionLbl.Text);

                for (int i = 0; i < aplistView.Items.Count; i++)
                {
                    rec.ADDItemApli(aplistView.Items[i].Text.ToString(), 
                                    float.Parse(aplistView.Items[i].SubItems[1].Text.ToString()));
                }

                for (int i = 0; i < adlistView.Items.Count; i++)
                {
                    rec.ADDItemDedu(adlistView.Items[i].Text.ToString(),
                                         float.Parse(adlistView.Items[i].SubItems[1].Text.ToString()),
                                         ((adlistView.Items[i].SubItems[2].Text.ToString() == "1") ? (bool)(true) : (bool)(false)),
                                         ((adlistView.Items[i].SubItems[3].Text.ToString() == "1") ? (bool)(true) : (bool)(false)));             
                }

                rec.Guardar("GRABAR");   
                Recibo_Imprimir(Global01.NroImprimir);         
                Global01.NroImprimir = "";
                Cursor.Current = Cursors.Default;
                CerrarRecibo();
            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (ralistView.Items.Count > 0)
            {                
                Cursor.Current = Cursors.WaitCursor;

                float tImporte = 0;
                bool wDeTerceroChk = false;
                bool wABahiaChk = false;

                //' iNABILITO nUEVO iNGRESO
                //InhabilitarRecibo();

                Catalogo._recibos.Recibo rec = new Catalogo._recibos.Recibo(Global01.Conexion, Global01.NroUsuario.ToString(), Int16.Parse(cboCliente.SelectedValue.ToString()));                

                for (int i = 0; i < ralistView.Items.Count; i++)
                {
                    wDeTerceroChk = (ralistView.Items[i].SubItems[8].Text.ToString() == "0" ? (bool)(true) : (bool)(false));
                    wABahiaChk = (ralistView.Items[i].SubItems[9].Text.ToString() == "1" ? (bool)(true) : (bool)(false));

                    rec.ADDItem(byte.Parse(ralistView.Items[i].SubItems[10].Text.ToString()),
                                    float.Parse(ralistView.Items[i].SubItems[1].Text.ToString()),
                                    DateTime.Parse(ralistView.Items[i].SubItems[2].Text.ToString()),
                                    DateTime.Parse(ralistView.Items[i].SubItems[3].Text.ToString()),
                                    ralistView.Items[i].SubItems[4].Text.ToString(),
                                    ralistView.Items[i].SubItems[5].Text.ToString(),
                                    Int16.Parse(ralistView.Items[i].SubItems[11].Text.ToString()),
                                    ralistView.Items[i].SubItems[7].Text.ToString(),
                                    wDeTerceroChk,
                                    float.Parse("0" + ralistView.Items[i].SubItems[12].Text.ToString()));

                    if (ralistView.Items[i].SubItems[10].Text.ToString() == "3" | ralistView.Items[i].SubItems[10].Text.ToString() == "4")
                    {
                        tImporte = tImporte + float.Parse(ralistView.Items[i].SubItems[1].Text.ToString()) * float.Parse(ralistView.Items[i].SubItems[12].Text.ToString());
                    }
                    else
                    {
                        tImporte = tImporte + float.Parse(ralistView.Items[i].SubItems[1].Text.ToString());
                    }
                }

                rec.NroImpresion = 0;
                rec.Bahia = wABahiaChk;
                rec.Observaciones = raObservacionesTxt.Text;
                rec.Total = tImporte;
                rec.Percepciones = float.Parse(apTotalPercepcionLbl.Text);

                for (int i = 0; i < aplistView.Items.Count; i++)
                {
                    rec.ADDItemApli(aplistView.Items[i].Text.ToString(), 
                                    float.Parse(aplistView.Items[i].SubItems[1].Text.ToString()));
                }

                for (int i = 0; i < adlistView.Items.Count; i++)
                {
                    rec.ADDItemDedu(adlistView.Items[i].Text.ToString(),
                                    float.Parse(adlistView.Items[i].SubItems[1].Text.ToString()),
                                    ((adlistView.Items[i].SubItems[2].Text.ToString() == "1") ? (bool)(true) : (bool)(false)),
                                    ((adlistView.Items[i].SubItems[3].Text.ToString() == "1") ? (bool)(true) : (bool)(false)));             

                }

                rec.Guardar("VER");

                if (adCascaraBtn.Tag.ToString() != "Cascara") 
                {
                    Recibo_Imprimir(Global01.NroImprimir);
                }

                Global01.NroImprimir = "";

                Cursor.Current = Cursors.Default;
            }
            else
            {
                VerDetalleRecibo();
            }

        }

        public static void Recibo_Imprimir(string NroRecibo)
        {
            //string sReporte =  @"D:\Users\pablo\Documents\Visual Studio 2012\Projects\wf_35c_pruebas1\Recibo_Enc4.rpt";

            string sReporte = Global01.AppPath + "\\Reportes\\Recibo_Enc3.rpt";
            ReportDocument oReport = new ReportDocument();

            oReport.Load(sReporte);
            Funciones.util.ChangeReportConnectionInfo(ref oReport);

            oReport.SetParameterValue("pNroRecibo", NroRecibo);

            varios.fReporte f = new varios.fReporte();
            f.Text = "Recibo n° " + NroRecibo;
            f.DocumentoNro = "RE-" + NroRecibo;           
            f.oRpt = oReport;
            f.ShowDialog();
            f.Dispose();
            f = null;
            oReport.Dispose();
        }

        private void cclistView_Click(object sender, EventArgs e)
        {
          if (btnIniciar.Tag.ToString()=="CANCELAR") {  
              if (cclistView.SelectedItems[0].SubItems[1].Text.Substring(0,3) == "FAC") {         
                  apConceptoTxt.Text = cclistView.SelectedItems[0].SubItems[1].Text;
                  apImporteTxt.Text = string.Format("{0:N2}",float.Parse(cclistView.SelectedItems[0].SubItems[3].Text));
                  apPercepcionTxt.Text = string.Format("{0:N2}",float.Parse(cclistView.SelectedItems[0].SubItems[9].Text));
              }
              else if (cclistView.SelectedItems[0].SubItems[1].Text.Substring(0,3) == "DEB") {
                  apConceptoTxt.Text = cclistView.SelectedItems[0].SubItems[1].Text;
                  apImporteTxt.Text = string.Format("{0:N2}", 0);
                  apPercepcionTxt.Text = string.Format("{0:N2}",float.Parse(cclistView.SelectedItems[0].SubItems[3].Text)); 
              }
              else if (cclistView.SelectedItems[0].SubItems[1].Text.Substring(0,3) == "CRE" | cclistView.SelectedItems[0].SubItems[1].Text.Substring(0,3) == "REC") {
                  adConceptoTxt.Text = cclistView.SelectedItems[0].SubItems[1].Text;
                  adImporteTxt.Text = string.Format("{0:N2}", Math.Abs(float.Parse(cclistView.SelectedItems[0].SubItems[3].Text)));
                  apPercepcionTxt.Text = string.Format("{0:N2}", 0);
              }
          }
        }

        private void cclistView_DoubleClick(object sender, EventArgs e)
        { 
             if (btnIniciar.Tag.ToString() == "CANCELAR") 
             {  
                if (cclistView.SelectedItems[0].SubItems[11].Text == "N") 
                { //' no fue aplicada todavia
                //-------------------------------
                    if (cclistView.SelectedItems[0].SubItems[1].Text.Substring(0,3) == "FAC" |
                        cclistView.SelectedItems[0].SubItems[1].Text.Substring(0,3) == "DEB") 
                    {
                        if (cclistView.SelectedItems[0].ForeColor == Color.Red) 
                        { //'ya esta agregada
                            cclistView.SelectedItems[0].ForeColor = Color.Black; 
                            cclistView.SelectedItems[0].Font = new Font(cclistView.Font, FontStyle.Regular);

                            ListViewItem ItemX =  new ListViewItem();
                            ItemX = aplistView.FindItemWithText(cclistView.SelectedItems[0].SubItems[1].Text.ToUpper(), false, 0);

                            if (ItemX !=null) 
                            {
                                aplistView.Items[ItemX.Index].Selected = true;
                                aplistView.Items.Remove(aplistView.SelectedItems[0]);

                                if (aplistView.Items.Count < 1) 
                                {
                                    cclistView.Tag = "-1";
                                }
                            }
                            ItemX = null;

                            TotalApli();
                        }
                        else
                        {
                            if (cclistView.Tag.ToString()=="-1") 
                            {
                                cclistView.Tag = cclistView.SelectedItems[0].SubItems[12].Text;
                            }

                            if (cclistView.SelectedItems[0].SubItems[12].Text != cclistView.Tag.ToString()) 
                            {
                                MessageBox.Show("Debe elegir todas las facturas de " + ((cclistView.Tag.ToString() == "1") ? "contado" : "cta.cte"), "atención", MessageBoxButtons.OK, MessageBoxIcon.Information);                                                       
                                apConceptoTxt.Text = "";
                                apImporteTxt.Text = "";
                                apPercepcionTxt.Text = "";
                            }
                            else 
                            {
                                apAgregarBtn_Click(null,null);
                                cclistView.SelectedItems[0].ForeColor = Color.Red;
                                cclistView.SelectedItems[0].Font = new Font(cclistView.Font, FontStyle.Bold);
                            }
                        }
                    }
                    else if (cclistView.SelectedItems[0].SubItems[1].Text.Substring(0,3) == "CRE" |
                                cclistView.SelectedItems[0].SubItems[1].Text.Substring(0,3) == "REC") 
                    {
                        if (cclistView.SelectedItems[0].ForeColor == Color.Red) 
                        {//'ya esta agregada
                        
                            cclistView.SelectedItems[0].ForeColor = Color.Black;
                            cclistView.SelectedItems[0].Font = new Font(cclistView.Font, FontStyle.Regular);

                            ListViewItem ItemX =  new ListViewItem();
                            ItemX = adlistView.FindItemWithText(cclistView.SelectedItems[0].SubItems[1].Text.ToUpper(),false,0);

                            if (ItemX !=null) 
                            {
                                adlistView.SelectedItems[ItemX.Index].Selected = true;
                                adlistView.Items.Remove(adlistView.SelectedItems[0]);

                                if (adlistView.Items.Count < 1) 
                                {
                                    cclistView.Tag = "-1";
                                }
                            }
                            ItemX = null;

                            TotalADeducir();
                        }
                        else
                        {
                            adAgregarBtn_Click(null,null);
                            cclistView.SelectedItems[0].ForeColor = Color.Red;
                            cclistView.SelectedItems[0].Font = new Font(cclistView.Font, FontStyle.Bold);
                        }
                    }
                    else
                    {
                        MessageBox.Show("El comprobante ya fue aplicado", "atención", MessageBoxButtons.OK, MessageBoxIcon.Information);                                                       
                    }
                //-------------------------------
                }
             }
        }

        private void adCascaraBtn_Click(object sender, EventArgs e)
        {
            cmdCascara_Click();
        }

        private void cmdCascara_Click()
        {
            int wDias = 0;
            string wTipoCascara = null;
            byte xI = 0;

            adConceptoTxt.Text = "";

            OleDbDataReader dr = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "tblClientes", "ID=" + cboCliente.SelectedValue.ToString(), "NONE");
            if (dr.HasRows)
            {
                dr.Read();
                wTipoCascara = ((DBNull.Value.Equals(dr["Cascara"]) ? "XX" : dr["Cascara"].ToString())).ToUpper();
            }
            else
            {
                wTipoCascara = "XX";
            }

            adCascaraBtn.Tag = "Cascara";

            btnVer_Click(null, null);

            wDias = CalculaDiasCascara();
            dr = Funciones.oleDbFunciones.Comando(Global01.Conexion, "SELECT * FROM ansCascara WHERE Codigo='" + wTipoCascara + "' and Desde<=" + wDias + " and Hasta>=" + wDias);

            if (dr.HasRows)
            {
                dr.Read();
                // MsgBox m.adoREC!ID & " " & m.adoREC!Codigo & " " & m.adoREC!d1 & " " & m.adoREC!d2 & " " & m.adoREC!d3 & " " & m.adoREC!d4 & " " & m.adoREC!d5 & " "
                for (xI = 1; xI <= 5; xI++)
                {
                    if (float.Parse(dr["d" + xI].ToString()) > 0)
                    {
                        adConceptoTxt.Text = adConceptoTxt.Text + dr["d" + xI].ToString().Trim() + "+";
                        //adConceptoTxt.Text = "cascara: " & m.adoREC("codigo") & " " & m.adoREC("id")
                        //adImporteTxt.Text =  string.Format("{0:N2}", dr["d" + xI].Text);
                    }
                }
                adImporteTxt.Text = string.Format("{0:N2}", float.Parse(dr["dTotal"].ToString()));
                adConceptoTxt.Text = " cascara: " + adConceptoTxt.Text.ToString().Trim().Substring(0, adConceptoTxt.Text.ToString().Trim().Length - 1) + "%";
                adPorcentajeChk.Checked = true;
                adAplicarRestoChk.Checked = true;
                //if (xI > 1) {adAplicarRestoCb.Checked = true;} else {adAplicarRestoCb.Checked = false;}
                adAgregarBtn_Click(null, null);
            }
            else
            {
                MessageBox.Show("Los plazos NO son válidos para aplicar cascara", "atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            dr = null;
            adCascaraBtn.Tag = "nada";
        }

        private int CalculaDiasCascara()
        {
            //const string PROCNAME_ = "CalculaDiasCascara";

            byte X = 0;
            int wDias = 32000;
            float wSumaImp = 0;
            float wSumaImpDias = 0;
            bool wEntro1 = false;
            bool wEntro2 = false;

            DateTime wFecha1 = DateTime.Today.Date;
            DateTime wFechaFactPromedio = DateTime.Today.Date;
            DateTime wFechaValoresPromedio = DateTime.Today.Date;

            OleDbDataReader dr = Funciones.oleDbFunciones.Comando(Global01.Conexion, "SELECT * FROM v_Cascara_Facturas");
            if (dr.HasRows)
            {
                X = 1;
                while (dr.Read())
                {
                    if (X == 1)
                    {
                        wFecha1 = DateTime.Parse(dr["Fecha"].ToString());
                        wSumaImp = ((float.Parse(dr["Importe"].ToString()) != float.Parse(dr["Saldo"].ToString())) ? float.Parse(dr["Saldo"].ToString()) : float.Parse(dr["Importe"].ToString()) - float.Parse(dr["ImpOferta"].ToString()) - float.Parse(dr["ImpPercep"].ToString()));
                        wSumaImpDias = ((float.Parse(dr["Importe"].ToString()) != float.Parse(dr["Saldo"].ToString())) ? float.Parse(dr["Saldo"].ToString()) : float.Parse(dr["Importe"].ToString()) - float.Parse(dr["ImpOferta"].ToString()) - float.Parse(dr["ImpPercep"].ToString()));
                    }
                    else
                    {
                        wSumaImp += ((float.Parse(dr["Importe"].ToString()) != float.Parse(dr["Saldo"].ToString())) ? float.Parse(dr["Saldo"].ToString()) : float.Parse(dr["Importe"].ToString()) - float.Parse(dr["ImpOferta"].ToString()) - float.Parse(dr["ImpPercep"].ToString()));
                        wSumaImpDias += ((float.Parse(dr["Importe"].ToString()) != float.Parse(dr["Saldo"].ToString())) ? float.Parse(dr["Saldo"].ToString()) : (float.Parse(dr["Importe"].ToString()) - float.Parse(dr["ImpOferta"].ToString()) - float.Parse(dr["ImpPercep"].ToString())) * (wFecha1 - DateTime.Parse(dr["Fecha"].ToString())).Days);
                    }
                    wEntro1 = true;
                    X += 1;
                }
                wDias = (int)Math.Round(wSumaImpDias / wSumaImp) - 1;
                wFechaFactPromedio = wFecha1.AddDays(wDias);
            }

            dr = Funciones.oleDbFunciones.Comando(Global01.Conexion, "SELECT * FROM v_Cascara_Valores");
            if (dr.HasRows)
            {
                X = 1;
                while (dr.Read())
                {
                    if (X == 1)
                    {
                        wFecha1 = DateTime.Parse(dr["Fecha"].ToString());
                        wSumaImp = float.Parse(dr["ImpValor"].ToString());
                        wSumaImpDias = float.Parse(dr["ImpValor"].ToString());
                    }
                    else
                    {
                        wSumaImp += float.Parse(dr["ImpValor"].ToString());
                        wSumaImpDias += float.Parse(dr["ImpValor"].ToString()) * (wFecha1 - DateTime.Parse(dr["Fecha"].ToString())).Days;
                    }
                    wEntro2 = true;
                    X += 1;
                }
                wDias = (int)Math.Round(wSumaImpDias / wSumaImp) - 1;
                wFechaValoresPromedio = wFecha1.AddDays(wDias);
            }

            dr = null;

            if (wEntro1 & wEntro2)
            {
                wDias = (wFechaFactPromedio - wFechaValoresPromedio).Days;
            }

            return wDias;

        }

        private void apAgregarBtn_Click(object sender, EventArgs e)
        {
        	//const string PROCNAME_ = "cmdAgregarAplicacion_Click";

            if (datosvalidos("aplicacion"))
            {
                ListViewItem ItemX;
                
                if (aplistView.Tag.ToString() == "upd")
                {
                    if (aplistView.SelectedItems != null & aplistView.SelectedItems.Count > 0) 
                    {
                      ItemX = aplistView.SelectedItems[0];
                    }
                    else 
                    {
                      ItemX = new ListViewItem(apConceptoTxt.Text.ToUpper());
                      aplistView.Tag = "add";
                    }
                }
                else 
                {
                    ItemX = new ListViewItem(apConceptoTxt.Text.ToUpper());
                }

                //alternate row color
                if (aplistView.Items.Count % 2 == 0)
                {
                    ItemX.BackColor = Color.White;
                }
                else
                {
                    ItemX.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
                }

                ItemX.Tag = "add";
                ItemX.SubItems.Add(string.Format("{0:N2}",float.Parse("0" + apImporteTxt.Text))); 
                ItemX.SubItems.Add(string.Format("{0:N2}",float.Parse("0" + apPercepcionTxt.Text))); 
                
                aplistView.Items.Add(ItemX);

                Funciones.util.AutoSizeLVColumnas(ref aplistView);

                aplistView.Items[ItemX.Index].Selected = true;

                ItemX = null;
             
                ItemX = cclistView.FindItemWithText(aplistView.SelectedItems[0].Text.ToUpper(),true,0);
                if (ItemX != null)
                {
                    cclistView.Items[ItemX.Index].Selected = true;
                    cclistView.SelectedItems[0].ForeColor = Color.Red;
                    cclistView.SelectedItems[0].Font = new Font(cclistView.Font, FontStyle.Bold);                
                }
                ItemX = null;

		        TotalApli();

		        apConceptoTxt.Text = "";
		        apImporteTxt.Text = "";
		        apPercepcionTxt.Text = "";

            }
        
        }

        private void cclistView_KeyDown(object sender, KeyEventArgs e)
        {
            if (Global01.miSABOR > Global01.TiposDeCatalogo.Cliente) 
            {
                if (e.KeyCode==Keys.I && e.Modifiers==Keys.Control)
                {
                    if (Funciones.modINIs.ReadINI("DATOS", "ICC", "0") == "1")
                    {
                        //'Imprimir la cta. cte.
                        CtaCte_Imprimir(cboCliente.SelectedValue.ToString());
                    }
                }
            }
        }

        private void CtaCte_Imprimir(string pIdCliente)
        {
            string sReporte = "";

            sReporte = Global01.AppPath + "\\Reportes\\CtaCte3.rpt";

            ReportDocument oReport = new ReportDocument();

            oReport.Load(sReporte);

            Funciones.util.ChangeReportConnectionInfo(ref oReport);

            oReport.SetParameterValue("pIdCliente", pIdCliente);

            //oReport.TiTle = "P - " + NroPedido;

            varios.fReporte f = new varios.fReporte();
            f.Text = "Cta. Cte. del Cliente n° " + pIdCliente;
            f.DocumentoNro = "CC-" + pIdCliente;
            f.oRpt = oReport;
            f.ShowDialog();
            f.Dispose();
            f = null;
            oReport.Dispose();
        }

        private void adlistView_KeyDown(object sender, KeyEventArgs e)
        {
            if (adlistView.SelectedItems != null && adlistView.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.Delete)
                {  //DEL
                    ListViewItem ItemX =  new ListViewItem();

                    ItemX = cclistView.FindItemWithText(adlistView.SelectedItems[0].Text.ToUpper(), true, 0);
                    if (ItemX != null)
                    {
                        cclistView.Items[ItemX.Index].Selected = true;
                        cclistView.SelectedItems[0].ForeColor = Color.Black;
                        cclistView.SelectedItems[0].Font = new Font(cclistView.Font, FontStyle.Regular);
                    }
                    ItemX = null;

                    adlistView.Items.Remove(adlistView.SelectedItems[0]);
                    adlistView.SelectedItems.Clear(); 
                    
                    TotalADeducir();
                }
            }
        }

        private void aplistView_KeyDown(object sender, KeyEventArgs e)
        {
            if (aplistView.SelectedItems != null & aplistView.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.Delete)
                {  //DEL
                    ListViewItem ItemX = new ListViewItem();
                    
                    ItemX = cclistView.FindItemWithText(aplistView.SelectedItems[0].Text.ToUpper(), true, 0);
                    if (ItemX != null)
                    {
                        cclistView.Items[ItemX.Index].Selected = true;
                        cclistView.SelectedItems[0].ForeColor = Color.Black;
                        cclistView.SelectedItems[0].Font = new Font(cclistView.Font, FontStyle.Regular);
                    }
                    ItemX = null;

                    aplistView.Items.Remove(aplistView.SelectedItems[0]);
                    aplistView.SelectedItems.Clear();

                    TotalApli();
                }
            }
        }

        private void adAgregarBtn_Click(object sender, EventArgs e)
        {
            //const string PROCNAME_ = "adAgregarBtn_Click";

            if (datosvalidos("adeducir"))
            {
                ListViewItem ItemX;

                if (adlistView.Tag.ToString() == "upd")
                {
                    if (adlistView.SelectedItems != null & adlistView.SelectedItems.Count > 0)
                    {
                        ItemX = adlistView.SelectedItems[0];
                    }
                    else
                    {
                        ItemX = new ListViewItem(adConceptoTxt.Text.ToUpper());
                        adlistView.Tag = "add";
                    }
                }
                else
                {
                    ItemX = new ListViewItem(adConceptoTxt.Text.ToUpper());
                }

                //alternate row color
                if (adlistView.Items.Count % 2 == 0)
                {
                    ItemX.BackColor = Color.White;
                }
                else
                {
                    ItemX.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
                }

                ItemX.Tag = "add";
                ItemX.SubItems.Add(string.Format("{0:N2}", float.Parse("0" + adImporteTxt.Text)));
                ItemX.SubItems.Add((adPorcentajeChk.Checked ? "1" : "0"));
                ItemX.SubItems.Add((adAplicarRestoChk.Checked ? "1" : "0"));        

                adlistView.Items.Add(ItemX);

                Funciones.util.AutoSizeLVColumnas(ref adlistView);

                adlistView.Items[ItemX.Index].Selected = true;

                ItemX = null;

                ItemX = cclistView.FindItemWithText(adlistView.SelectedItems[0].Text.ToUpper(), true, 0);
                if (ItemX != null)
                {
                    cclistView.Items[ItemX.Index].Selected = true;
                    cclistView.SelectedItems[0].ForeColor = Color.Red;
                    cclistView.SelectedItems[0].Font = new Font(cclistView.Font, FontStyle.Bold);
                }
                ItemX = null;

                TotalADeducir();

                adConceptoTxt.Text = "";
                adImporteTxt.Text = "";
                adAplicarRestoChk.Checked = false;
                adPorcentajeChk.Checked = false;
            }
        }

        private void ralistView_DoubleClick(object sender, EventArgs e)
        {
            //const string PROCNAME_ = "lvValores_DblClick";

            if (ralistView.SelectedItems != null & ralistView.SelectedItems.Count > 0)
            {                
                Funciones.util.BuscarIndiceEnCombo(ref cvTipoValorCbo, ralistView.SelectedItems[0].SubItems[10].Text.ToString(), false);
                cvImporteTxt.Text = ralistView.SelectedItems[0].SubItems[1].Text;
                cvFecEmiDt.Value = DateTime.Parse(ralistView.SelectedItems[0].SubItems[2].Text);
                cvFecCobroDt.Value = DateTime.Parse(ralistView.SelectedItems[0].SubItems[3].Text);
                cvNroChequeTxt.Text = ralistView.SelectedItems[0].SubItems[4].Text;
                cvNroCuentaTxt.Text = ralistView.SelectedItems[0].SubItems[5].Text;                
                Funciones.util.BuscarIndiceEnCombo(ref cvBancoCbo, ralistView.SelectedItems[0].SubItems[11].Text.ToString(), false);
                cvCpaTxt.Text = ralistView.SelectedItems[0].SubItems[7].Text;
                cvDeTerceroChk.Checked = ((ralistView.SelectedItems[0].SubItems[8].Text=="1") ? true : false);
                cvABahiaChk.Checked = ((ralistView.SelectedItems[0].SubItems[9].Text=="1") ? true : false);

                ralistView.Tag = "upd";

                string s = cvTipoValorCbo.SelectedText;
                if (s.IndexOf("dólar") != -1 | s.IndexOf("euro") != -1)
                {
                    cvTipoCambioTxt.Text = ralistView.SelectedItems[0].SubItems[12].Text;
                    cvDivisasLbl.Text = string.Format("{0:N2}", float.Parse(cvImporteTxt.Text) * float.Parse(cvTipoCambioTxt.Text));
                }
                else
                {
                    cvDivisasLbl.Text = "";
                    cvTipoCambioTxt.Text = "";
                }
            }
        }

        private void adlistView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Set the ListViewItemSorter property to a new ListViewItemComparer object
            adlistView.ListViewItemSorter = new util.ListViewItemComparer(e.Column);
            // Call the sort method to manually sort.
            adlistView.Sort();
        }

        private void adImporteTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(sender, ref e);
        }

        private void apImporteTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(sender, ref e);
        }

        private void apPercepcionTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(sender, ref e);
        }

        private void cvTipoCambioTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(sender, ref e);
        }

        private void btnResumen_Click(object sender, EventArgs e)
        {
            if (btnIniciar.Tag.ToString() == "CANCELAR")
            {
                VerDetalleRecibo();
            }
        }

        private void paEnviosCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtenerMovimientos();
        }

        private void ObtenerMovimientos()
        {
            paDataGridView.Visible = false;

            _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, int.Parse(cboCliente.SelectedValue.ToString()));
            System.Data.OleDb.OleDbDataReader dr = null;

            if (paEnviosCbo.SelectedIndex == 0)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.TODOS, "RECIBO");
            }
            else if (paEnviosCbo.SelectedIndex == 1)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.ENVIADOS, "RECIBO");
            }
            else if (paEnviosCbo.SelectedIndex == 2)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, "RECIBO");
            }

            if (dr != null)
            {
                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();

                    dt.Load(dr);
                    paDataGridView.AutoGenerateColumns = true;
                    paDataGridView.DataSource = dt;
                    paDataGridView.Refresh();
                    paDataGridView.Visible = true;
                    paDataGridView.ClearSelection();
                }
            }
        }

        private void paDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Global01.AppActiva)
            {
                DataGridViewCell cell = paDataGridView[e.ColumnIndex, e.RowIndex];
                if (cell != null)
                {
                    DataGridViewRow row = cell.OwningRow;
                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "RECI")
                    {
                        Recibo_Imprimir(row.Cells["Nro"].Value.ToString());
                    }
                }
            }
        }

        public Funciones.emitter_receiver.emisorHandler<int> emisor
        {
            get;
            set;
        }

        public void onRecibir(int dato)
        {
            if (btnIniciar.Tag.ToString() == "INICIAR")
                cboCliente.SelectedIndex = dato;
        }

        private void CliNAgregarBtn_Click(object sender, EventArgs e)
        {

        }

        private void EnviarBtn_Click(object sender, EventArgs e)
        {
            System.Collections.Generic.List<Catalogo.util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO> filtro
                = new List<Catalogo.util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO>();

            _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, int.Parse(cboCliente.SelectedValue.ToString()));
            System.Data.OleDb.OleDbDataReader dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, "RECIBO");

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO mov = new util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO();
                    mov.origen = "RECIBO";
                    mov.nro = (string)dr["Nro"];
                    filtro.Add(mov);
                }
            }

            Catalogo.util.BackgroundTasks.EnvioMovimientos envio =
                new util.BackgroundTasks.EnvioMovimientos(
                    util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico,
                    int.Parse(this.cboCliente.SelectedValue.ToString()),
                    util.BackgroundTasks.EnvioMovimientos.MODOS_TRANSMISION.TRANSMITIR_LISTVIEW,
                    filtro);

            envio.run();
            ObtenerMovimientos();
        }

    } //fin clase
} //fin namespace