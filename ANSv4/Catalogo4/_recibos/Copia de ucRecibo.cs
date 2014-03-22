using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo._recibos
{

    public partial class ucRecibo : UserControl
    {

        //public System.Data.OleDb.OleDbConnection Conexion
        //{
        //    set { oConexion = value; }
        //}

        //public System.Data.OleDb.OleDbConnection Conexion
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //        oConexion = value;
        //    }
        //}

        public ucRecibo()
        {
            InitializeComponent();

            Catalogo.Funciones.util.CargaCombo(ref Global01.Conexion , ref cboCliente, "tblClientes", "RazonSocial", "ID", "Activo<>1", "RazonSocial", true, true);

        }


        // diable Ordenar columna para todo el grid
        private void ccDataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

 
            if (cboCliente.SelectedIndex > 0)
            {
                toolStripStatusLabel1.Text = "Recibo para el cliente: " + this.cboCliente.Text.ToString() + " (" + this.cboCliente.ComboBox.SelectedValue + ")";
                CargarCtaCte();                
                CargarClienteNovedades();                
                CargarClienteDatos();
            }
            else 
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Recibo para el cliente ..."; }
                LimpiarClienteDatos();
            };
        }

        private void LimpiarClienteDatos()
        {
            cclistView.Items.Clear();
            this.CliNDataGridView.DataSource = null;

            CliDCuitTxt.Text = "";
            CliDDomicilioTxt.Text = "";
            CliDEmailTxt.Text = "";
            CliDLocalidadTxt.Text = "";
            CliDNroCuentaTxt.Text = "";
            CliDObservacionesTxt.Text = "";
            CliDRazonSocialTxt.Text = "";
            CliDTelefonoTxt.Text = "";
            CliDTitularTxt.Text = "";
        }

        private void CargarCtaCte()
        {
            DataTable dt =  Catalogo.Funciones.oleDbFunciones.xGetDt(ref Global01.Conexion, "v_CtaCte", "IDCliente=" + cboCliente.ComboBox.SelectedValue.ToString(), "Orden, Fecha");
            
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

                //ItemX.SubItems["1"].Text  = dr["Comprobante"].ToString();

                ItemX.SubItems.Add(dr["Comprobante"].ToString());
                ItemX.SubItems.Add(dr["Importe"].ToString());
                ItemX.SubItems.Add(dr["Saldo"].ToString());

                wAcumulado = wAcumulado + float.Parse(dr["SaldoS"].ToString());
                ItemX.SubItems.Add(wAcumulado.ToString()); 

                ItemX.SubItems.Add(dr["Det_Comprobante"].ToString()); 
                ItemX.SubItems.Add(dr["ImpOferta"].ToString()); 
                ItemX.SubItems.Add(dr["TextoOferta"].ToString()); 
                ItemX.SubItems.Add(dr["Vencida"].ToString()); 
                ItemX.SubItems.Add(dr["ImpPercep"].ToString()); 
                ItemX.SubItems.Add(dr["IdCliente"].ToString());
                ItemX.SubItems.Add((DBNull.Value.Equals(dr["EstaAplicada"]) ? "N" : "S"));
                ItemX.SubItems.Add((DBNull.Value.Equals(dr["EsContado"]) ? "0" : dr["EsContado"].ToString())); 
                
                if (ItemX.SubItems[1].ToString().Substring(1,3)=="DEB")
                {
                    ItemX.SubItems[1].ForeColor = Color.Red;
                    ItemX.SubItems[1].Font = new Font(cclistView.Font, FontStyle.Bold);
                };
                cclistView.Items.Add(ItemX);            
            }

            if (dt.Rows.Count>0) Funciones.util.AutoSizeLVColumnas(ref cclistView);

            cclistView.Visible = true;
            dt = null;

        }

        private void CargarClienteNovedades()
        {
            this.CliNDataGridView.DataSource = Catalogo.Funciones.oleDbFunciones.xGetDt(ref Global01.Conexion, "tblClientesNovedades", "IDCliente=" + cboCliente.ComboBox.SelectedValue.ToString(), "F_Carga");
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
            CliDTitularTxt.Text = "";

            OleDbDataReader dr = null;
            dr = Funciones.oleDbFunciones.Comando(ref Global01.Conexion, "SELECT * FROM tblClientes WHERE ID=" + cboCliente.ComboBox.SelectedValue.ToString());
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
                //CliDTitularTxt.Text = dr["titular"].ToString();
            };
  
        }

        private void CliDPnlMain_DoubleClick(object sender, EventArgs e)
        {
            //mandar mail
        }

        private void cvAceptarBtn_Click(object sender, EventArgs e)
        {
            if (datosvalidos("recibo"))
            {
                ListViewItem ItemX;
                
                if (ralistView.Tag.ToString() == "upd")
                {
                    if (ralistView.SelectedItems != null) 
                    {
                      ItemX = ralistView.SelectedItems[0];
                    }
                    else 
                    {
                      ItemX =  new ListViewItem(cvTipoValorCbo.SelectedItem.ToString());
                    };
                }
                else 
                {
                   ItemX =  new ListViewItem(cvTipoValorCbo.SelectedItem.ToString());
                };

                ItemX.Tag = "";
                ItemX.SubItems.Add(cvImporteTxt.Text); //1
                ItemX.SubItems.Add(cvFecEmiDt.Text); 
                ItemX.SubItems.Add(cvFecCobroDt.Text); 
                ItemX.SubItems.Add(cvNroChequeTxt.Text); 
                ItemX.SubItems.Add(cvNroCuentaTxt.Text);
                ItemX.SubItems.Add(((cvBancoCbo.SelectedIndex<=0) ? "" : cvBancoCbo.SelectedItem.ToString())); 
                ItemX.SubItems.Add(cvCpaTxt.Text);
                ItemX.SubItems.Add(cvDeTerceroCb.Text);
                ItemX.SubItems.Add(cvABahiaCb.Text);
                ItemX.SubItems.Add(cvTipoValorCbo.SelectedValue.ToString());
                ItemX.SubItems.Add(((cvBancoCbo.SelectedIndex <= 0) ? "0" : cvBancoCbo.SelectedValue.ToString())); 
                ItemX.SubItems.Add(cvTipoCambioTxt.Text); 

                ralistView.Items.Add(ItemX);           
        
                ralistView.Tag = "add";
        
                TotalRecibo();
                rTabsRecibo.TabIndex = 0;
            };
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
                };
            };   
              
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
                            cvDivisasLbl.Text = string.Format("{0}",float.Parse(cvImporteTxt.Text) * float.Parse(cvTipoCambioTxt.Text));
                        };
                    }
                    else 
                    {
                        cvDivisasLbl.Text = "";
                    };
                };
            };

          if (cvTipoValorCbo.SelectedIndex==1) 
          {  //Cheque
            if (cvTipoValorCbo.SelectedIndex<=0 | cvNroCuentaTxt.Text.Trim().Length==0 | cvNroChequeTxt.Text.Trim().Length==0)                      
            {
               MessageBox.Show("Ingrese Datos del Cheque", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               wDatosValidos = false;
               cvTipoValorCbo.Focus();
            };
          }
          else if (cvTipoValorCbo.SelectedIndex>=5) 
          { //retención               
            if (cvNroChequeTxt.Text.Trim().Length==0)                      
            {
                MessageBox.Show("Ingrese n° de Retención", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                wDatosValidos = false;
                cvNroCuentaTxt.Focus();
            };
          };

    
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

        private void cvTipoValorCbo_Click(object sender, EventArgs e)
        {            
            if (cvTipoValorCbo.SelectedIndex==1)
            {   //Cheque
                cvNroChequeLbl.Text = "n° Cheque";
                cvFecEmiLbl.Text = "Fecha Emisión";

                cvFecEmiDt.Enabled = true;
                cvFecCobroDt.Enabled = true;
                cvNroChequeTxt.Enabled = true;
                cvDeTerceroCb.Enabled = true;
                cvNroCuentaTxt.Enabled = true;
                cvABahiaCb.Enabled = true;
                cvBancoCbo.Enabled = true;
                cvCpaTxt.Enabled = true;
                cvTipoCambioTxt.Enabled = false;                
            }
            else if (cvTipoValorCbo.SelectedIndex==2)
            {   //Efectivo
                cvFecEmiDt.Enabled = false;
                cvFecCobroDt.Enabled = false;
                cvNroChequeTxt.Enabled = false;
                cvDeTerceroCb.Enabled = false;
                cvNroCuentaTxt.Enabled = false;
                cvABahiaCb.Enabled = false;
                cvBancoCbo.Enabled = false;
                cvCpaTxt.Enabled = false;
                cvTipoCambioTxt.Enabled = false;                
            }
            else if (cvTipoValorCbo.SelectedIndex==3 | cvTipoValorCbo.SelectedIndex==4)
            {   //Divisas
                cvFecEmiDt.Enabled = false;
                cvFecCobroDt.Enabled = false;
                cvNroChequeTxt.Enabled = false;
                cvDeTerceroCb.Enabled = false;
                cvNroCuentaTxt.Enabled = false;
                cvABahiaCb.Enabled = false;
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
                cvDeTerceroCb.Enabled = false;
                cvNroCuentaTxt.Enabled = false;
                cvABahiaCb.Enabled = false;
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
               LimpiarIngresosRecibo();
               rTabsRecibo.TabIndex = 0;
        }

        private void LimpiarIngresosRecibo()
        {
            cvTipoValorCbo.SelectedIndex  = 0;
            cvImporteTxt.Text = "0.00";
            cvTipoCambioTxt.Text = "";
            cvDivisasLbl.Text = "";
            cvFecEmiDt.Value = DateTime.Today.Date;
            cvFecCobroDt.Value = DateTime.Today.Date;
            cvNroChequeTxt.Text = "";
            cvNroCuentaTxt.Text = "";
            cvBancoCbo.SelectedIndex  = 0;
            cvCpaTxt.Text = "";
            cvDeTerceroCb.Checked  = false;
            cvABahiaCb.Checked = false;
        }

        private void tsBtnIniciar_Click(object sender, EventArgs e)
        {

            if (cboCliente.SelectedIndex > 0)
            {
                if (tsBtnIniciar.Tag.ToString() == "INICIAR")
                {
                    //vg.auditor.Guardar Recibo, INICIA

                    //Limpio Listados
                    TotalRecibo();
                    TotalADeducir();
                    TotalApli();

                    raObservacionesTxt.Text = "";
                    cclistView.Items.Clear();
                    aplistView.Items.Clear();
                    adlistView.Items.Clear();

                    AbrirRecibo();
                    HabilitarRecibo();
                }
                else
                {
                    if (MessageBox.Show("¿Esta Seguro que quiere CANCELAR el Recibo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // vg.auditor.Guardar Recibo, CANCELA
                        // Limpio Listados
                        raObservacionesTxt.Text = "";
                        cclistView.Items.Clear();
                        aplistView.Items.Clear();
                        adlistView.Items.Clear();

                        CerrarRecibo();
                        rTabsRecibo.SelectedIndex = 1;
                        InhabilitarRecibo();
                    };
                };

                cclistView.Tag = "-1";
            }
            else
            {
              MessageBox.Show("Seleccione un Cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            };

        }

        private void TotalRecibo()
        {
            throw new NotImplementedException();
        }

        private void InhabilitarRecibo()
        {
            throw new NotImplementedException();
        }

        private void CerrarRecibo()
        {
            throw new NotImplementedException();
        }

        private void HabilitarRecibo()
        {
            throw new NotImplementedException();
        }

        private void AbrirRecibo()
        {
            throw new NotImplementedException();
        }

        private void TotalApli()
        {
            throw new NotImplementedException();
        }

        private void TotalADeducir()
        {
            throw new NotImplementedException();
        }


    }

}