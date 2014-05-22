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

namespace Catalogo._clientesNovedades
{
    public partial class ucVisitas : UserControl
    
    {
        ToolTip _ToolTip = new System.Windows.Forms.ToolTip();

        public ucVisitas()
        {
            InitializeComponent();
            _ToolTip.SetToolTip(btnIniciar, "INICIAR Carga");
            _ToolTip.SetToolTip(btnImprimir, "Graba e Imprime el Formulario ...");
            _ToolTip.SetToolTip(btnVer, "ver ...");

            if (!Global01.AppActiva)
            {
                this.Dispose();
            }

            cboCliente.SelectedIndexChanged -= cboCliente_SelectedIndexChanged;
            Funciones.util.load_clientes(ref cboCliente);
            cboCliente.SelectedIndexChanged += cboCliente_SelectedIndexChanged;

            if (Global01.miSABOR == Global01.TiposDeCatalogo.Cliente)
            {
                cboCliente.SelectedValue = Global01.NroUsuario;
            }
            else
            {
                cboCliente.SelectedValue = Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado;
            }
      
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            paEnviosCbo.SelectedIndex = 2;

            if (cboCliente.SelectedIndex > 0)
            {
                
                toolStripStatusLabel1.Text = "Visita para el cliente: " + this.cboCliente.Text.ToString();
                btnIniciar.Enabled = true;
            }
            else 
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Visita para el cliente ..."; }
                btnIniciar.Enabled = false;
            }
        }

        private void cvAgregarBtn_Click(object sender, EventArgs e)
        {

            
        }

        private bool datosvalidos(string pCampo)
        {
            bool wDatosValidos = true;
            
            if (pCampo.ToLower() == "bdfacturastxt" | pCampo.ToLower() == "all" | pCampo.ToLower() == "Visitaf")
            {
                //if (bdFacturasTxt.Text.Trim().Length<=0 )
                //{
                //    MessageBox.Show("Ingrese n° Factura", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    wDatosValidos = false;
                //    bdFacturasTxt.Focus();
                //}
            }

            return wDatosValidos;
        }
  
        private void cvCancelarBtn_Click(object sender, EventArgs e)
        {
               LimpiarIngresosValores();
        }

        private void LimpiarIngresosValores()
        {
            viObservacionesTxt.Text =
                "Temas tratados con el cliente: \r\n\r\n" +
                "Ventas Logradas/Acciones de Marketing/Plazo/Cobranza: \r\n\r\n" +
                "Comentarios y Observaciones (Líneas/Precios/Distribuidores/Competidores): \r\n";

            //bdTipoEfectivoRb.Checked = false;
            //bdTipoChequesRb.Checked = false;
            //bdCaChequesTxt.Enabled = false;
            //bdBancoCbo.Enabled = false;
            //bdFechaDt.Value = DateTime.Today.Date;
            //bdImporteTxt.Text = "0,00";
            //bdNumeroTxt.Text = "";
            //bdCaChequesTxt.Text = "";
            //bdFacturasTxt.Text = "";
            //viQRecibeTxt.Text = "";
            //bdBancoCbo.SelectedIndex  = 0;
            //ralistView.Items.Clear();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex > 0)
            {
                if (btnIniciar.Tag.ToString() == "INICIAR")
                {
                    _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Visita,
                         _auditor.Auditor.AccionesAuditadas.INICIA);
                    //Limpio Listados
                    AbrirVisita();
                    
                    HabilitarVisita();
                    rTabsVisita.SelectedIndex = 0;
                    rTabsVisita.Visible = true;
                }
                else
                {
                    if (MessageBox.Show("¿Esta Seguro que quiere CANCELAR el Ingreso?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Visita,
                             _auditor.Auditor.AccionesAuditadas.CANCELA);

                        InhabilitarVisita();

                        rTabsVisita.Visible = false;
                        rTabsVisita.SelectedIndex = 1;
                        //if (Global01.miSABOR > Global01.TiposDeCatalogo.Cliente) cboCliente.SelectedIndex = 0;                                
                        CerrarVisita();                                                
                    }
                }
            }
            else
            {
              MessageBox.Show("Seleccione un Cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void CerrarVisita()
        {    
            btnIniciar.Text = "Iniciar";
            btnIniciar.Tag = "INICIAR";
            
            _ToolTip.SetToolTip(btnIniciar, "INICIAR Visita Nuevo");

            LimpiarIngresosValores();
            paEnviosCbo.SelectedIndex = 2;
            ObtenerMovimientos();

            //cboEnvios_Click();
        }

        private void AbrirVisita()
        {
            btnIniciar.Text = "CANCELAR";
            btnIniciar.Tag = "CANCELAR";
            _ToolTip.SetToolTip(btnIniciar, "CANCELAR éste Visita");
            
            LimpiarIngresosValores();
            
        }

        private void HabilitarVisita()
        {
            btnImprimir.Enabled = true;
            btnVer.Enabled = true;
            //ralistView.Enabled = true;
            //this.raPnlBotton.Enabled = true;
            cboCliente.Enabled = false;
        }

        private void InhabilitarVisita()
        {
            btnImprimir.Enabled = false;
            btnVer.Enabled = false;
            //ralistView.Enabled = false;
            //this.raPnlBotton.Enabled = false;
            cboCliente.Enabled = true;
        }

        
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (datosvalidos("Visita"))
            {                
                //Cursor.Current = Cursors.WaitCursor;

                //InhabilitarVisita();

                //try
                //{

                //    Visita_Imprimir(Global01.NroImprimir);

                //    Global01.NroImprimir = "";

                //    CerrarVisita();
                //}
                //catch (util.errorHandling.RegistroDuplicadoException ex)
                //{
                //    HabilitarVisita();
                //    util.errorHandling.ErrorLogger.LogMessage(ex);
                //    System.Windows.Forms.MessageBox.Show(ex.Message);
                //    return;
                //}
                //catch (System.Data.OleDb.OleDbException ex)
                //{
                //    Cursor.Current = Cursors.Default;
                //    switch (ex.ErrorCode)
                //    {
                //        case -2147467259:                            
                //            MessageBox.Show("Registro Duplicado", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //            return;
                //        default:
                //             MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //             util.errorHandling.ErrorLogger.LogMessage(ex);
                //             util.errorHandling.ErrorForm.show();
                //             return;
                //    }
                //}
                //catch (Exception ex)
                //{
                //    util.errorHandling.ErrorForm.show();
                //    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            //if (datosvalidos("Visita"))
            //{
            //    Cursor.Current = Cursors.WaitCursor;
                
            //    //intDep.Observaciones = viQRecibeTxt.Text;

            //    //intDep.Guardar("VER");
            //    Visita_Imprimir(Global01.NroImprimir);
            //    Global01.NroImprimir = "";
                
            //}
        }

        public static void Visita_Imprimir(string NroVisita)
        {
            Cursor.Current = Cursors.WaitCursor;
            
            string sReporte = Global01.AppPath + "\\Reportes\\Visita1.rpt";
      
            ReportDocument oReport = new ReportDocument();

            oReport.Load(sReporte);
            Funciones.util.ChangeReportConnectionInfo(ref oReport);
      
            oReport.SetParameterValue("pNroVisita", NroVisita);

            varios.fReporte f = new varios.fReporte();
            f.Text = "Visita n° " + NroVisita;
            f.DocumentoNro = "VI-" + NroVisita;
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
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.TODOS, "Visita");
            }
            else if (paEnviosCbo.SelectedIndex == 1)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.ENVIADOS, "Visita");
            }
            else if (paEnviosCbo.SelectedIndex == 2)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, "Visita");
            }

            if (dr != null)
            {
                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Selec", System.Type.GetType("System.Boolean"));
                    dt.Load(dr);
                    paDataGridView.AutoGenerateColumns = true;
                    paDataGridView.DataSource = dt;
                    paDataGridView.Refresh();
                    paDataGridView.Visible = true;
                    paDataGridView.ClearSelection();
                    paDataGridView.Columns["Selec"].Visible = (paEnviosCbo.Text.ToUpper() == "NO ENVIADOS");
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
                        Visita_Imprimir(row.Cells["Nro"].Value.ToString());
                    }
                }
            }
        }

        private void EnviarBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global01.AppActiva)
                {
                    if (paEnviosCbo.Text.ToString().ToUpper() == "NO ENVIADOS")
                    {
                        if (paDataGridView.SelectedRows != null && paDataGridView.SelectedRows.Count > 0)
                        {
                            if (MessageBox.Show("Debe estar conectado a Internet. ¿QUIERE ENVIARLOS AHORA?", "Envio de Movimientos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                System.Collections.Generic.List<Catalogo.util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO> filtro = new List<util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO>();

                                foreach (DataGridViewRow row in paDataGridView.Rows)
                                {
                                    if (row.Cells["Selec"].Value != null && row.Cells["Selec"].Value.ToString() != "" && (bool)row.Cells["Selec"].Value)
                                    {
                                        util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO item = new util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO();
                                        System.Diagnostics.Debug.WriteLine(row.Cells["Nro"].Value);
                                        item.nro = row.Cells["Nro"].Value.ToString();
                                        item.origen = row.Cells["Origen"].Value.ToString();
                                        filtro.Add(item);
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
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        private void viCatEspecialistaCb_CheckedChanged(object sender, EventArgs e)
        {
            viCategoriaPnl.Visible = (bool)viCatEspecialistaCb.Checked;
        }

    } //fin clase
} //fin namespace