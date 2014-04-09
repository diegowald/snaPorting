using System;
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

namespace Catalogo._interdeposito
{
    public partial class ucInterDeposito : UserControl
    
    {
        private const string m_sMODULENAME_ = "ucInterDeposito";
        ToolTip _ToolTip = new System.Windows.Forms.ToolTip();

        public ucInterDeposito()
        {
            InitializeComponent();
            _ToolTip.SetToolTip(btnIniciar, "INICIAR InterDeposito Nuevo");
            _ToolTip.SetToolTip(btnImprimir, "Graba e Imprime el InterDeposito ...");
            _ToolTip.SetToolTip(btnVer, "ver ...");

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
            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref bdBancoCbo, "tblBancosDepositosCtas", "BancoCta", "ID", "Activo=0", "Format([ID],'000') & ' - ' & tblBancosDepositosCtas.Nombre", true, false, "Format([ID],'000') & ' - ' & tblBancosDepositosCtas.Nombre AS BancoCta, ID");            
        
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            paEnviosCbo.SelectedIndex = -1;

            if (cboCliente.SelectedIndex > 0)
            {
                
                toolStripStatusLabel1.Text = "InterDeposito para el cliente: " + this.cboCliente.Text.ToString();
                btnIniciar.Enabled = true;
                paEnviosCbo.SelectedIndex = 2;

            }
            else 
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "InterDeposito para el cliente ..."; }
                btnIniciar.Enabled = false;
            }
//            this.emitir(cboCliente.SelectedIndex);
        }

        private void cvAgregarBtn_Click(object sender, EventArgs e)
        {
            if (datosvalidos("interdepositoF"))
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
                      ItemX =  new ListViewItem("FAC");
                      ralistView.Tag = "add";
                    }
                }
                else 
                {
                   ItemX =  new ListViewItem("FAC");
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
                ItemX.SubItems.Add(bdFacturasTxt.Text);
                ItemX.SubItems.Add(" ");
                ItemX.SubItems.Add(" "); 

                ralistView.Items.Add(ItemX);

                Funciones.util.AutoSizeLVColumnas(ref ralistView);

                bdFacturasTxt.Text = "";
                bdFacturasTxt.Focus();

            }
        }

        private bool datosvalidos(string pCampo)
        {
            bool wDatosValidos = true;

            if (pCampo.ToLower() == "all" | pCampo.ToLower() == "interdeposito")
            {
                if (ralistView.Items.Count <= 0)
                {
                    MessageBox.Show("Ingrese facturas que afecta", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                }
            }

            if  (pCampo.ToLower() == "all" | pCampo.ToLower() == "interdeposito")
            {
                if (!bdTipoEfectivoRb.Checked & !bdTipoChequesRb.Checked)
                {
                    MessageBox.Show("Ingrese Efectivo ó Cheque", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;                    
                }
            }   

            if (pCampo.ToLower() == "bdbancocbo" | pCampo.ToLower() == "all" | pCampo.ToLower()=="interdeposito")
            {
                if (bdBancoCbo.SelectedIndex <= 0 && bdTipoChequesRb.Checked)
                {
                    MessageBox.Show("Ingrese Cuenta de Banco", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    bdBancoCbo.Focus();
                }
            }
              
            if (pCampo.ToLower()=="bdimportetxt" | pCampo.ToLower()=="all" | pCampo.ToLower()=="interdeposito" )
            {
                if (float.Parse( "0" + bdImporteTxt.Text) <= 0 )
                {
                    MessageBox.Show("Ingrese Importe", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    bdImporteTxt.Focus();
                }
            }
            
            if (pCampo.ToLower() == "bdfacturastxt" | pCampo.ToLower() == "all" | pCampo.ToLower() == "interdepositof")
            {
                if (bdFacturasTxt.Text.Trim().Length<=0 )
                {
                    MessageBox.Show("Ingrese n° Factura", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    bdFacturasTxt.Focus();
                }
            }

            if (pCampo.ToLower() == "bdnumerotxt" | pCampo.ToLower() == "all" | pCampo.ToLower() == "interdeposito")
            {
                if (float.Parse("0" + bdNumeroTxt.Text) <= 0)
                {
                    MessageBox.Show("Ingrese n° de B.D.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    bdNumeroTxt.Focus();
                }
            }

            if (pCampo.ToLower() == "bdcachequestxt" | pCampo.ToLower() == "all" | pCampo.ToLower() == "interdeposito")
            {
                if (bdTipoChequesRb.Checked && float.Parse("0" + bdCaChequesTxt.Text) <= 0)
                {
                    MessageBox.Show("Ingrese Ca. de Cheques", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    wDatosValidos = false;
                    bdCaChequesTxt.Focus();
                }
            }

            return wDatosValidos;
        }
  
        private void cvCancelarBtn_Click(object sender, EventArgs e)
        {
               LimpiarIngresosValores();
        }

        private void LimpiarIngresosValores()
        {
            ralistView.Tag = "add";

            bdTipoEfectivoRb.Checked = false;
            bdTipoChequesRb.Checked = false;
            bdCaChequesTxt.Enabled = false;
            bdBancoCbo.Enabled = false;
            bdFechaDt.Value = DateTime.Today.Date;
            bdImporteTxt.Text = "0,00";
            bdNumeroTxt.Text = "";
            bdCaChequesTxt.Text = "";
            bdFacturasTxt.Text = "";
            bdObservacionesTxt.Text = "";
            bdBancoCbo.SelectedIndex  = 0;
            ralistView.Items.Clear();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex > 0)
            {
                if (btnIniciar.Tag.ToString() == "INICIAR")
                {
                    auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.InterDeposito,
                         auditoria.Auditor.AccionesAuditadas.INICIA);
                    //Limpio Listados
                    AbrirInterDeposito();
                    
                    HabilitarInterDeposito();
                    rTabsInterDeposito.SelectedIndex = 0;
                    rTabsInterDeposito.Visible = true;
                }
                else
                {
                    if (MessageBox.Show("¿Esta Seguro que quiere CANCELAR el Ingreso?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.InterDeposito,
                             auditoria.Auditor.AccionesAuditadas.CANCELA);

                        InhabilitarInterDeposito();

                        rTabsInterDeposito.Visible = false;
                        rTabsInterDeposito.SelectedIndex = 1;
                        cboCliente.SelectedIndex = 0;
                        CerrarInterDeposito();                                                
                    }
                }
            }
            else
            {
              MessageBox.Show("Seleccione un Cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ralistView_KeyDown(object sender, KeyEventArgs e)
        {
            if (ralistView.SelectedItems != null & ralistView.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.Delete)
                {  //DEL
                     ralistView.Items.Remove(ralistView.SelectedItems[0]);
                    ralistView.SelectedItems.Clear(); 
                }
            }
        }

        private void CerrarInterDeposito()
        {    
            btnIniciar.Text = "Iniciar";
            btnIniciar.Tag = "INICIAR";
            
            _ToolTip.SetToolTip(btnIniciar, "INICIAR InterDeposito Nuevo");

            LimpiarIngresosValores();

            ObtenerMovimientos();

            //cboEnvios_Click();
        }

        private void AbrirInterDeposito()
        {
            btnIniciar.Text = "CANCELAR";
            btnIniciar.Tag = "CANCELAR";
            _ToolTip.SetToolTip(btnIniciar, "CANCELAR éste InterDeposito");
            
            LimpiarIngresosValores();
            
        }

        private void HabilitarInterDeposito()
        {
            btnImprimir.Enabled = true;
            btnVer.Enabled = true;
            ralistView.Enabled = true;
            raPnlBotton.Enabled = true;
            cboCliente.Enabled = false;
        }

        private void InhabilitarInterDeposito()
        {
            btnImprimir.Enabled = false;
            btnVer.Enabled = false;
            ralistView.Enabled = false;
            raPnlBotton.Enabled = false;
            cboCliente.Enabled = true;
        }

        private void bdImporteTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
             Funciones.util.EsImporte(sender, ref e);
        }

        private void bdCaChequesTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(sender, ref e);
        }

        private void bdNumeroTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            Funciones.util.EsImporte(sender, ref e);
        }
        
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            //' iNABILITO nUEVO iNGRESO

            if (datosvalidos("interdeposito") & (ralistView.Items.Count >0))
            {
                InhabilitarInterDeposito();

                Catalogo._interdeposito.InterDeposito intDep = new Catalogo._interdeposito.InterDeposito(Global01.Conexion, Global01.NroUsuario.ToString(), Int16.Parse(cboCliente.SelectedValue.ToString()));                

                for (int i = 0; i < ralistView.Items.Count; i++)
                {
                    intDep.ADDFacturas(ralistView.Items[i].SubItems[1].Text.ToString(),
                                    ralistView.Items[i].SubItems[2].Text.ToString(),
                                    float.Parse("0" + ralistView.Items[i].SubItems[3].Text.ToString()));
                }
        
                intDep.Bco_Dep_Tipo = ((bdTipoEfectivoRb.Checked) ?  "E" : "C");
                intDep.Bco_Dep_Fecha = bdFechaDt.Value;
                intDep.Bco_Dep_Numero = Int16.Parse(bdNumeroTxt.Text);
                intDep.Bco_Dep_Monto = float.Parse(bdImporteTxt.Text);
                intDep.Bco_Dep_Ch_Cantidad = byte.Parse("0" + bdCaChequesTxt.Text);
                intDep.Bco_Dep_IdCta = byte.Parse(bdBancoCbo.SelectedValue.ToString());

                intDep.NroImpresion = 0;
                intDep.Observaciones = bdObservacionesTxt.Text;

                //try
                //{
                    intDep.Guardar("GRABAR");
                //}
                //catch
                //{ 
                //}

                InterDeposito_Imprimir(Global01.NroImprimir);         
                Global01.NroImprimir = "";
                Cursor.Current = Cursors.Default;
                CerrarInterDeposito();
   
            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            if (datosvalidos("interdeposito") & (ralistView.Items.Count > 0))
            {
                Catalogo._interdeposito.InterDeposito intDep = new Catalogo._interdeposito.InterDeposito(Global01.Conexion, Global01.NroUsuario.ToString(), Int16.Parse(cboCliente.SelectedValue.ToString()));

                for (int i = 0; i < ralistView.Items.Count; i++)
                {
                    intDep.ADDFacturas(ralistView.Items[i].Text,
                                    ralistView.Items[i].SubItems[1].Text.ToString(),
                                    float.Parse("0" + ralistView.Items[i].SubItems[2].Text.ToString()));
                }

                intDep.Bco_Dep_Tipo = ((bdTipoEfectivoRb.Checked) ? "E" : "C");
                intDep.Bco_Dep_Fecha = bdFechaDt.Value;
                intDep.Bco_Dep_Numero = Int16.Parse(bdNumeroTxt.Text);
                intDep.Bco_Dep_Monto = float.Parse(bdImporteTxt.Text);
                intDep.Bco_Dep_Ch_Cantidad = byte.Parse("0" + bdCaChequesTxt.Text);
                intDep.Bco_Dep_IdCta = byte.Parse(bdBancoCbo.SelectedValue.ToString());

                intDep.NroImpresion = 0;
                intDep.Observaciones = bdObservacionesTxt.Text;

                intDep.Guardar("VER");
                InterDeposito_Imprimir(Global01.NroImprimir);
                Global01.NroImprimir = "";
                
            }
        }

        public static void InterDeposito_Imprimir(string NroInterDeposito)
        {
            string sReporte = Global01.AppPath + "\\Reportes\\InterDeposito1.rpt";
            ReportDocument oReport = new ReportDocument();

            oReport.Load(sReporte);
            Funciones.util.ChangeReportConnectionInfo(ref oReport);

            oReport.SetParameterValue("pNroInterDeposito", NroInterDeposito);

            fReporte f = new fReporte();
            f.Text = "InterDeposito n° " + NroInterDeposito;
            f.DocumentoNro = "R" + NroInterDeposito;
            //f.EmailTO = odsInterDepositos1.Tables[0].Rows[0]["Email"].Text;
            f.EmailTO = "juanpablobrugniere@speedy.com.ar";
            //f.RazonSocial = odsInterDepositos1.Tables[0].Rows[0]["RazonSocial"].Text;
            f.EmailAsunto = "auto náutica sur - InterDeposito n° " + NroInterDeposito;
            f.oRpt = oReport;
            f.ShowDialog();
            f.Dispose();
            f = null;

            oReport.Dispose();

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
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.TODOS, "InterDeposito");
            }
            else if (paEnviosCbo.SelectedIndex == 1)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.ENVIADOS, "InterDeposito");
            }
            else if (paEnviosCbo.SelectedIndex == 2)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, "InterDeposito");
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
                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "INTE")
                    {
                        InterDeposito_Imprimir(row.Cells["Nro"].Value.ToString());
                    }
                }
            }
        }

        private void bdTipoChequesRb_CheckedChanged(object sender, EventArgs e)
        {
            if (bdTipoChequesRb.Checked)
            {
                bdCaChequesTxt.Enabled = true;
                bdBancoCbo.Enabled = true;
            }
        }

        private void bdTipoEfectivoRb_CheckedChanged(object sender, EventArgs e)
        {
            if (bdTipoEfectivoRb.Checked)
            {
                bdCaChequesTxt.Enabled = false;
                bdBancoCbo.Enabled = false;
            }
        }

    } //fin clase
} //fin namespace