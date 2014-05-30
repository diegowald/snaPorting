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
        private ToolTip _ToolTip = new System.Windows.Forms.ToolTip();
        private System.Collections.Specialized.OrderedDictionary Filter_Marca = new System.Collections.Specialized.OrderedDictionary();

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

        private void ucVisitas_Load(object sender, EventArgs e)
        {
            _productos.FilterBuilder fb = new _productos.FilterBuilder();
            if (viMarcaCbo.Items.Count < 1)
            {
                DataTable dtVehiculos = new DataTable();
                dtVehiculos = Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "tblMarcas");
                fb.PopulateFilter(ref Filter_Marca, dtVehiculos, "Marca");
                String[] filterQuantityArray = new String[Filter_Marca.Count];
                Filter_Marca.Keys.CopyTo(filterQuantityArray, 0);
                viMarcaCbo.Items.Clear();
                viMarcaCbo.Items.AddRange(filterQuantityArray);
                viMarcaCbo.SelectedIndex = 0;
            }
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {                      
            if (cboCliente.SelectedIndex > 0)
            {                
                toolStripStatusLabel1.Text = "Visita para el cliente: " + this.cboCliente.Text.ToString();
                btnIniciar.Enabled = true;
                CargarClienteDatos();
            }
            else 
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Visita para el cliente ..."; }
                btnIniciar.Enabled = false;
                LimpiarPantalla("clientes");
            }
            ObtenerMovimientos();
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
  
        private void LimpiarPantalla(string queLimpio)
        {
            if (queLimpio.ToLower() == "clientes" | queLimpio.ToLower() == "#all")
            {
                viMarcaCbo.SelectedIndex = 0;
                viCuitTxt.Text = "";
                viDomicilioTxt.Text = "";
                viEmailTxt.Text = "";
                viCiudadTxt.Text = "";
                viRazonSocialTxt.Text = "";
                viTelefonoTxt.Text = "";
            }

            if (queLimpio.ToLower() == "datos" | queLimpio.ToLower() == "all")
            {
                viObservacionesTxt.Text =
                    "Temas tratados con el cliente: \r\n\r\n" +
                    "Ventas Logradas/Acciones de Marketing/Plazo/Cobranza: \r\n\r\n" +
                    "Comentarios y Observaciones (Líneas/Precios/Distribuidores/Competidores): \r\n";

                viQRecibeTxt.Text = "";
                viCComprasTxt.Text = "";
                viCPagosTxt.Text = "";

                viRamoLivCb.Checked = false;
                viRamoPesCb.Checked = false;
                viRamoAgrCb.Checked = false;

                viCatRepGralCb.Checked = false;
                viCatLubricentroCb.Checked = false;
                viCatEstServicioCb.Checked = false;
                viCatMotosCb.Checked = false;
                viCatEspecialistaCb.Checked = false;

                viEspMotorCb.Checked = false;
                viEspFrenosCb.Checked = false;
                viEspSuspCb.Checked = false;
                viEspElectricidadCb.Checked = false;
                viEspAccesoriosCb.Checked = false;
            }
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

            LimpiarPantalla("all");
            paEnviosCbo.SelectedIndex = 2;
            ObtenerMovimientos();
        }

        private void AbrirVisita()
        {
            btnIniciar.Text = "CANCELAR";
            btnIniciar.Tag = "CANCELAR";
            _ToolTip.SetToolTip(btnIniciar, "CANCELAR éste Visita");

            LimpiarPantalla("all");
            
        }

        private void HabilitarVisita()
        {
            btnImprimir.Enabled = true;
            btnVer.Enabled = true;
            cboCliente.Enabled = false;
        }

        private void InhabilitarVisita()
        {
            btnImprimir.Enabled = false;
            btnVer.Enabled = false;
            cboCliente.Enabled = true;
        }
        
        private void btnImprimir_Click(object sender, EventArgs e)
        {
            if (datosvalidos("grabar"))
            {
                using (new varios.WaitCursor())
                {

                    InhabilitarVisita();

                    try
                    {

                        int xID = 0;
                        ClientesVisitas_add(Int16.Parse(cboCliente.SelectedValue.ToString()), DateTime.Now.Date,
                                            viQRecibeTxt.Text, viCComprasTxt.Text, viCPagosTxt.Text,
                                            viRamoLivCb.Checked, viRamoPesCb.Checked, viRamoAgrCb.Checked,
                                            viMonoMarcaNO.Checked, viMarcaCbo.Text,
                                            viCatRepGralCb.Checked, viCatLubricentroCb.Checked, viCatEstServicioCb.Checked, viCatMotosCb.Checked, viCatEspecialistaCb.Checked,
                                            viEspMotorCb.Checked, viEspFrenosCb.Checked, viEspSuspCb.Checked, viEspElectricidadCb.Checked, viEspAccesoriosCb.Checked,
                                            viObservacionesTxt.Text, "", Global01.NroUsuario,
                                            viRazonSocialTxt.Text, viCuitTxt.Text, viEmailTxt.Text, viDomicilioTxt.Text, viCiudadTxt.Text, viTelefonoTxt.Text,
                                            ref xID);

                        Visita_Imprimir(xID.ToString());

                        Global01.NroImprimir = "";

                        CerrarVisita();
                    }
                    catch (util.errorHandling.RegistroDuplicadoException ex)
                    {
                        HabilitarVisita();
                        util.errorHandling.ErrorLogger.LogMessage(ex);
                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        return;
                    }
                    catch (System.Data.OleDb.OleDbException ex)
                    {
                        switch (ex.ErrorCode)
                        {
                            case -2147467259:
                                MessageBox.Show("Registro Duplicado", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            default:
                                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                util.errorHandling.ErrorLogger.LogMessage(ex);
                                util.errorHandling.ErrorForm.show();
                                return;
                        }
                    }
                    catch (Exception ex)
                    {
                        util.errorHandling.ErrorForm.show();
                        MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }

        private void ClientesVisitas_add(int pIdCliente, DateTime pFecha, 
                                        string pQRecibe, string pCCompras, string pCPagos,
                                        bool pRamoLiviano, 
                                        bool pRamoPesado, 
                                        bool pRamoAgricola, 
                                        bool pEsMononarca, 
                                        string pMarca, 
                                        bool pCatRepGral, 
                                        bool pCatLubricentro, 
                                        bool pCatEstServicio, 
                                        bool pCatMotos, 
                                        bool pCatEspecialista, 
                                        bool pEspMotor, 
                                        bool pEspFrenos, 
                                        bool pEspSuspension, 
                                        bool pEspElectricidad, 
                                        bool pEspAccesorios, 
                                        string pDetalle1, string pDetalle2, string pIdViajante, 
                                        string pRazonSocial, string pCuit, string pEmail, string pDomicilio, string pCiudad, string pTelefono,
                                        ref int pID)
        {
            try
            {
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

                cmd.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = pIdCliente;
                cmd.Parameters.Add("pF_Carga", System.Data.OleDb.OleDbType.Date).Value = pFecha;
                cmd.Parameters.Add("pQRecibe", System.Data.OleDb.OleDbType.VarChar, 50).Value = pQRecibe;
                cmd.Parameters.Add("pCCompras", System.Data.OleDb.OleDbType.VarChar, 50).Value = pCCompras;
                cmd.Parameters.Add("pCPagos", System.Data.OleDb.OleDbType.VarChar, 50).Value = pCPagos;
                cmd.Parameters.Add("pRamoLiviano", System.Data.OleDb.OleDbType.Boolean).Value = pRamoLiviano;
                cmd.Parameters.Add("pRamoPesado", System.Data.OleDb.OleDbType.Boolean).Value = pRamoPesado;
                cmd.Parameters.Add("pRamoAgricola", System.Data.OleDb.OleDbType.Boolean).Value = pRamoAgricola;
                cmd.Parameters.Add("pEsMonomarca", System.Data.OleDb.OleDbType.Boolean).Value = pEsMononarca;
                cmd.Parameters.Add("pMarca", System.Data.OleDb.OleDbType.VarChar, 25).Value = pMarca;
                cmd.Parameters.Add("pCatRepGral", System.Data.OleDb.OleDbType.Boolean).Value = pCatRepGral;
                cmd.Parameters.Add("pCatLubricentro", System.Data.OleDb.OleDbType.Boolean).Value = pCatLubricentro;
                cmd.Parameters.Add("pCatEstServicio", System.Data.OleDb.OleDbType.Boolean).Value = pCatEstServicio;
                cmd.Parameters.Add("pCatMotos", System.Data.OleDb.OleDbType.Boolean).Value = pCatMotos;
                cmd.Parameters.Add("pCatEspecialista", System.Data.OleDb.OleDbType.Boolean).Value = pCatEspecialista;
                cmd.Parameters.Add("pEspMotor", System.Data.OleDb.OleDbType.Boolean).Value = pEspMotor;
                cmd.Parameters.Add("pEspFrenos", System.Data.OleDb.OleDbType.Boolean).Value = pEspFrenos;
                cmd.Parameters.Add("pEspSuspension", System.Data.OleDb.OleDbType.Boolean).Value = pEspSuspension;
                cmd.Parameters.Add("pEspElectricidad", System.Data.OleDb.OleDbType.Boolean).Value = pEspElectricidad;
                cmd.Parameters.Add("pEspAccesorios", System.Data.OleDb.OleDbType.Boolean).Value = pEspAccesorios;

                cmd.Parameters.Add("pRazonSocial", System.Data.OleDb.OleDbType.VarChar, 255).Value = pDetalle1;
                cmd.Parameters.Add("pRazonSocial", System.Data.OleDb.OleDbType.VarChar, 255).Value = pDetalle2;
                cmd.Parameters.Add("pIdViajante", System.Data.OleDb.OleDbType.Integer).Value = pIdViajante;
                cmd.Parameters.Add("pRazonSocial", System.Data.OleDb.OleDbType.VarChar, 40).Value = pRazonSocial;
                cmd.Parameters.Add("pCuit", System.Data.OleDb.OleDbType.VarChar, 13).Value = pCuit;
                cmd.Parameters.Add("pEmail", System.Data.OleDb.OleDbType.VarChar, 64).Value = pEmail;
                cmd.Parameters.Add("pDomicilio", System.Data.OleDb.OleDbType.VarChar, 40).Value = pDomicilio;
                cmd.Parameters.Add("pCiudad", System.Data.OleDb.OleDbType.VarChar, 40).Value = pCiudad;
                cmd.Parameters.Add("pTelefono", System.Data.OleDb.OleDbType.VarChar, 40).Value = pTelefono;                

                cmd.Connection = Global01.Conexion;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_ClientesVisitas_add";

                cmd.ExecuteNonQuery();
                cmd = null;

                System.Data.OleDb.OleDbDataReader rec = null;
                rec = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "tblClientesVisitas", "@@identity");
                rec.Read();
                pID = Int16.Parse(rec["ID"].ToString());
                rec = null;
            }

            catch (System.Data.OleDb.OleDbException ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;
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
            using (new varios.WaitCursor())
            {
                NroVisita = NroVisita.PadLeft(10, '0');

                string sReporte = Global01.AppPath + "\\Reportes\\Visitas1.rpt";

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
                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "VISI")
                    {
                        Visita_Imprimir(row.Cells["Nro"].Value.ToString());
                    }
                }
            }
        }

        //private void ObtenerMovimientos()
        //{
        //    paDataGridView.Visible = false;

        //    string wOrden = "F_Carga DESC";
        //    string wCampos = "ID as Nro, F_Carga as Fecha, RazonSocial, F_Transmicion, 'Visita' as Origen, IdCliente, ' ' as Observaciones";
        //    string wCondicion = "IdViajante=" + Global01.NroUsuario ;

        //    wCondicion += " and IdCliente=" + cboCliente.SelectedValue.ToString();             

        //    System.Data.OleDb.OleDbDataReader dr = null;

        //    if (paEnviosCbo.SelectedIndex == 0)
        //    {
        //        //TODOS
        //    }
        //    else if (paEnviosCbo.SelectedIndex == 1)
        //    {
        //         wCondicion +=  " and not (F_Transmicion is null)";
        //    }
        //    else if (paEnviosCbo.SelectedIndex == 2)
        //    {
        //        wCondicion +=  " and (F_Transmicion is null)";
        //    }

        //    dr = Funciones.oleDbFunciones.xGetDr(Global01.Conexion, "v_ClientesVisitas", wCondicion, wOrden, wCampos);

        //    if (dr != null)
        //    {
        //        if (dr.HasRows)
        //        {
        //            DataTable dt = new DataTable();
        //            dt.Columns.Add("Selec", System.Type.GetType("System.Boolean"));
        //            dt.Load(dr);
        //            paDataGridView.AutoGenerateColumns = true;
        //            paDataGridView.DataSource = dt;
        //            paDataGridView.Refresh();
        //            paDataGridView.Visible = true;
        //            paDataGridView.ClearSelection();
        //            paDataGridView.Columns["Selec"].Visible = (paEnviosCbo.Text.ToUpper() == "NO ENVIADOS");
        //        }
        //    }
        //}

        //private void paDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        //{
        //    if (Global01.AppActiva)
        //    {
        //        DataGridViewCell cell = paDataGridView[e.ColumnIndex, e.RowIndex];
        //        if (cell != null)
        //        {
        //            DataGridViewRow row = cell.OwningRow;
        //            Visita_Imprimir(row.Cells["Nro"].Value.ToString());
        //        }
        //    }
        //}

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

        private void viMonoMarcaSI_CheckedChanged(object sender, EventArgs e)
        {
            if (viMonoMarcaSI.Checked)
            {
                viMarcaCbo.Enabled = true;
            }
            else
            {
                viMarcaCbo.Enabled = false;
                viMarcaCbo.SelectedIndex = 0;
            }
        }

        private void CargarClienteDatos()
        {

            LimpiarPantalla("clientes");

            OleDbDataReader dr = null;
            dr = Funciones.oleDbFunciones.Comando(Global01.Conexion, "SELECT * FROM tblClientes WHERE ID=" + cboCliente.SelectedValue.ToString());
            if (dr.HasRows)
            {
                dr.Read();

                viCuitTxt.Text = dr["cuit"].ToString();
                viDomicilioTxt.Text = dr["domicilio"].ToString();
                viEmailTxt.Text = dr["email"].ToString();
                viCiudadTxt.Text = dr["ciudad"].ToString();
                viRazonSocialTxt.Text = dr["razonsocial"].ToString();
                viTelefonoTxt.Text = dr["telefono"].ToString();
            }
        }

    } //fin clase
} //fin namespace