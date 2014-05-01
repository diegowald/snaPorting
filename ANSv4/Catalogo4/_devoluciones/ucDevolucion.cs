using System;
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

namespace Catalogo._devoluciones
{
    public partial class ucDevolucion : UserControl, 
        Funciones.emitter_receiver.IReceptor<System.Windows.Forms.DataGridViewRow>, // Para recibir el producto seleccionado
        Funciones.emitter_receiver.IReceptor<_pedidos.PedidosHelper.Acciones>,// Para recibir acciones a la Devolucion desde la grilla de productos.
        Funciones.emitter_receiver.IReceptor<short> // Para recibir una notificacion de cambio del cliente seleccionado

    {
        //private //const string m_sMODULENAME_ = "ucDevolucion";
        ToolTip _ToolTip = new System.Windows.Forms.ToolTip();
        DataGridViewRow ProductoSeleccionado = null;

        private System.Collections.Specialized.OrderedDictionary Filter_Marca = new System.Collections.Specialized.OrderedDictionary();
        private System.Collections.Specialized.OrderedDictionary Filter_Modelo = new System.Collections.Specialized.OrderedDictionary();

        public ucDevolucion()
        {
            InitializeComponent();
            _ToolTip.SetToolTip(btnIniciar, "INICIAR Devolución");
            _ToolTip.SetToolTip(btnImprimir, "Graba e Imprime Devolución ...");
            _ToolTip.SetToolTip(btnVer, "ver ...");

            if (!Global01.AppActiva)
            {
                this.Dispose();
            }

            cboCliente.SelectedIndexChanged -= cboCliente_SelectedIndexChanged;
            if (Funciones.modINIs.ReadINI("DATOS", "EsGerente", "0") == "1")
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1", "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Trim(cstr(ID)) & ')' as Cliente, ID");
            }
            else
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1 and (IdViajante=" + Global01.NroUsuario.ToString() + " or IdViajante=" + Global01.Zona.ToString() + ")", "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Format([ID],'00000') & ')' AS Cliente, ID");
                if (Global01.miSABOR == Global01.TiposDeCatalogo.Cliente) cboCliente.SelectedValue = Global01.NroUsuario;
            }
            cboCliente.SelectedIndexChanged += cboCliente_SelectedIndexChanged;

            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref devMfDepositoCbo, "v_Deposito", "D_Dep", "IdDep", "ALL", "D_Dep", true, false, "NONE");
            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref devMnDepositoCbo, "v_Deposito", "D_Dep", "IdDep", "ALL", "D_Dep", true, false, "NONE");

            _productos.FilterBuilder fb = new _productos.FilterBuilder();

            if (devMfVehiculoCbo.Items.Count < 1)
            {
                DataTable dtVehiculos = new DataTable();
                dtVehiculos = Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "tblMarcas");
                fb.PopulateFilter(ref Filter_Marca, dtVehiculos, "Marca");
                String[] filterQuantityArray = new String[Filter_Marca.Count];
                Filter_Marca.Keys.CopyTo(filterQuantityArray, 0);
                devMfVehiculoCbo.Items.Clear();
                devMfVehiculoCbo.Items.AddRange(filterQuantityArray);
                devMfVehiculoCbo.SelectedIndex = 0;
            }
            Catalogo.varios.NotificationCenter.instance.attachReceptor2(this);
            cboCliente.SelectedValue = Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado;
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {            
            paEnviosCbo.SelectedIndex = 2;

            if (cboCliente.SelectedIndex > 0)
            {
                toolStripStatusLabel1.Text = "Devolución para el cliente: " + this.cboCliente.Text.ToString();
                btnIniciar.Enabled = true;
            }
            else
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Devolución para el cliente ..."; }
                btnIniciar.Enabled = false;
            }
            Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado = (short) cboCliente.SelectedValue;
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
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.TODOS, "DEVOLUCION");
            }
            else if (paEnviosCbo.SelectedIndex == 1)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.ENVIADOS, "DEVOLUCION");
            }
            else if (paEnviosCbo.SelectedIndex == 2)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, "DEVOLUCION");
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

        private void btnIniciar_Click(object sender, EventArgs e)
        {

            if (Global01.OperacionActivada == "PEDIDO" )
            {
                MessageBox.Show("Debe cerrar el PEDIDO para comenzar la DEVOLUCION", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            else
            {
                if (cboCliente.SelectedIndex > 0)
                {
                    if (btnIniciar.Tag.ToString() == "INICIAR")
                    {
                        _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Devoluciones, _auditor.Auditor.AccionesAuditadas.INICIA, "");                            
                        
                        devMflistView.Items.Clear();
                        devMnlistView.Items.Clear();

                        OleDbDataReader dr = null;
     
                        if (Funciones.modINIs.ReadINI("DATOS", "DevolucionNE", "0") == "1")
                        {
                            _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, int.Parse(cboCliente.SelectedValue.ToString()));
                            dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS,"DEVOLUCION");
                            if (dr.HasRows)
                            {
                                MessageBox.Show("Hay una Devolución pendiente de envio, sugerimos: \n Ir a Devoluciones anteriores (no enviados) abrirla y continuar con la misma", "atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                paEnviosCbo.SelectedIndex = 2;
                                DevolucionTab.SelectedIndex = 1;                                
                                devTabAnt.Select();
                                DevolucionTab.Visible = true;
                                
                                return;
                            } ;                   
                        }
                        dr = null;
     
                        IniciarDevolucion();
                        HabilitarDevolucion();

                        devMfDepositoCbo.SelectedIndex = short.Parse(Funciones.modINIs.ReadINI("Preferencias", "Deposito", "0"));
                        devMnDepositoCbo.SelectedIndex = short.Parse(Funciones.modINIs.ReadINI("Preferencias", "Deposito", "0"));

                        DevolucionTab.SelectedIndex = 0;
                        DevolucionTab.Visible = true;
                    }
                    else
                    {
                        if (MessageBox.Show("¿Esta Seguro que quiere CANCELAR la Devolución?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Devoluciones, _auditor.Auditor.AccionesAuditadas.CANCELA, "");
                            DevolucionTab.Visible = false;
                            devMflistView.Items.Clear();

                            CerrarDevolucion();
                            InhabilitarDevolucion();

                            //cboCliente.SelectedIndex = 0;

                            devMfDepositoCbo.SelectedIndex = short.Parse(Funciones.modINIs.ReadINI("Preferencias", "Deposito", "0"));
                            devMnDepositoCbo.SelectedIndex = short.Parse(Funciones.modINIs.ReadINI("Preferencias", "Deposito", "0"));
                        }
                    }

                    devMflistView.Tag = "-1";
                    devMnlistView.Tag = "-1";
                }
                else
                {
                    MessageBox.Show("Seleccione un Cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }

        }

        private void CerrarDevolucion()
        {
            Global01.OperacionActivada = "nada";            
            btnIniciar.Text = "Iniciar";
            btnIniciar.Tag = "INICIAR";

            _ToolTip.SetToolTip(btnIniciar, "INICIAR Devolución");

            ObtenerMovimientos();
        }

        private void IniciarDevolucion()
        {
            Global01.OperacionActivada = "DEVOLUCION";
            btnIniciar.Text = "CANCELAR";
            btnIniciar.Tag = "CANCELAR";
            _ToolTip.SetToolTip(btnIniciar, "CANCELAR Devolucion");
        }

        private void HabilitarDevolucion()
        {
            devMfPnlTop.Enabled = true;
            devMfPnlMain.Enabled = true;
            devMnPnlTop.Enabled = true;
            devMnPnlMain.Enabled = true;
            btnImprimir.Enabled = true;
            btnVer.Enabled = true;
            devMflistView.Enabled = true;
            devMnlistView.Enabled = true;
            cboCliente.Enabled = false;
        }

        private void InhabilitarDevolucion()
        {
            devMfPnlTop.Enabled = false;
            devMfPnlMain.Enabled = false;
            devMnPnlTop.Enabled = false;
            devMnPnlMain.Enabled = false;
            btnImprimir.Enabled = false;
            btnVer.Enabled = false;
            devMflistView.Enabled = false;
            devMnlistView.Enabled = false;
            cboCliente.Enabled = true;
        }

        public void onRecibir(DataGridViewRow dato)
        {
            ProductoSeleccionado = dato;
        }

        public void onRecibir(_pedidos.PedidosHelper.Acciones dato)
        {
            switch (dato)
            {
                case _pedidos.PedidosHelper.Acciones.COMPRAR:
                    if (DevolucionTab.SelectedTab.Name.ToUpper()=="DEVTABMN")
                    {
                        DevMnAgregar();
                    }
                    else if (DevolucionTab.SelectedTab.Name.ToUpper()=="DEVTABMF")
                    {
                        DevMfAgregar();
                    }
                    break;
                case _pedidos.PedidosHelper.Acciones.INCREMENTAR:
                    if (DevolucionTab.SelectedTab.Name.ToUpper() == "DEVTABMN")
                    {
                        devMnCantidadTxt.Value++;
                    }
                    else if (DevolucionTab.SelectedTab.Name.ToUpper() == "DEVTABMF")
                    {
                        devMfCantidadTxt.Value++;
                    }
                    
                    break;
                case _pedidos.PedidosHelper.Acciones.DECREMENTAR:
                    if (DevolucionTab.SelectedTab.Name.ToUpper() == "DEVTABMN")
                    {
                        if (devMnCantidadTxt.Value > 1) {devMnCantidadTxt.Value--;}
                    }
                    else if (DevolucionTab.SelectedTab.Name.ToUpper() == "DEVTABMF")
                    {
                        if (devMfCantidadTxt.Value > 1) {devMfCantidadTxt.Value--;}
                    }                    
                    break;
                default:
                    break;
            }
        }

        private void devMflistView_KeyDown(object sender, KeyEventArgs e)
        {

            if (devMflistView.SelectedItems != null & devMflistView.SelectedItems.Count > 0)
            {              
                if (e.KeyCode==Keys.O && e.Modifiers==Keys.Control)
                {    //Observaciones del Item

                    string wItemObservaciones = devMflistView.SelectedItems[0].SubItems[9].Text;
                    if (Funciones.util.InputBox(" (Presione Cancelar para quitar la Observación)  ", "Observaciones para el Item", 80, ref wItemObservaciones) == DialogResult.OK)
                    {
                        devMflistView.SelectedItems[0].SubItems[9].Text = wItemObservaciones;
                    }
                    else
                    { // Apreto Cancelar
                        devMflistView.SelectedItems[0].SubItems[9].Text = "";
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {  //DEL
                    devMflistView.Items.Remove(devMflistView.SelectedItems[0]);
                    devMflistView.SelectedItems.Clear(); 
                }
                else if (e.KeyValue.ToString()  == "187")
                {
                    if (Decimal.Parse(devMflistView.SelectedItems[0].SubItems[2].Text.ToString()) < 999)
                    {
                        devMflistView.SelectedItems[0].SubItems[2].Text = (Decimal.Parse(devMflistView.SelectedItems[0].SubItems[2].Text.ToString()) + 1).ToString();
                    }
                }
                else if (e.KeyValue.ToString() == "189")
                {
                    if (Decimal.Parse(devMflistView.SelectedItems[0].SubItems[2].Text.ToString()) > 1)
                    {
                        devMflistView.SelectedItems[0].SubItems[2].Text = (Decimal.Parse(devMflistView.SelectedItems[0].SubItems[2].Text.ToString()) - 1).ToString();
                    }
                }
    
            }
        }

        private void devMnlistView_KeyDown(object sender, KeyEventArgs e)
        {

            if (devMnlistView.SelectedItems != null & devMnlistView.SelectedItems.Count > 0)
            {
                if (e.KeyCode == Keys.O && e.Modifiers == Keys.Control)
                {    //Observaciones del Item

                    string wItemObservaciones = devMnlistView.SelectedItems[0].SubItems[9].Text;
                    if (Funciones.util.InputBox(" (Presione Cancelar para quitar la Observación)  ", "Observaciones para el Item", 80, ref wItemObservaciones) == DialogResult.OK)
                    {
                        devMnlistView.SelectedItems[0].SubItems[9].Text = wItemObservaciones;
                    }
                    else
                    { // Apreto Cancelar
                        devMnlistView.SelectedItems[0].SubItems[9].Text = "";
                    }
                }
                else if (e.KeyCode == Keys.Delete)
                {  //DEL
                    devMnlistView.Items.Remove(devMnlistView.SelectedItems[0]);
                    devMnlistView.SelectedItems.Clear();
                }
                else if (e.KeyValue.ToString() == "187")
                {
                    if (Decimal.Parse(devMnlistView.SelectedItems[0].SubItems[2].Text.ToString()) < 999)
                    {
                        devMnlistView.SelectedItems[0].SubItems[2].Text = (Decimal.Parse(devMnlistView.SelectedItems[0].SubItems[2].Text.ToString()) + 1).ToString();
                    }
                }
                else if (e.KeyValue.ToString() == "189")
                {
                    if (Decimal.Parse(devMnlistView.SelectedItems[0].SubItems[2].Text.ToString()) > 1)
                    {
                        devMnlistView.SelectedItems[0].SubItems[2].Text = (Decimal.Parse(devMnlistView.SelectedItems[0].SubItems[2].Text.ToString()) - 1).ToString();
                    }
                }

            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            if (devMflistView.Items.Count > 0 | devMnlistView.Items.Count > 0)
            {
                InhabilitarDevolucion();

                Catalogo._devoluciones.Devolucion dev = new Catalogo._devoluciones.Devolucion(Global01.Conexion, Global01.NroUsuario.ToString(), Int16.Parse(cboCliente.SelectedValue.ToString()));
                dev.NroImpresion = 0;
                Cursor.Current = Cursors.Default; 
                string wItemObservaciones = "";
                if (Funciones.util.InputBox(" (Presione Cancelar para quitar la Observación)  ", "Observación para la devolución", 80, ref wItemObservaciones) == DialogResult.OK)
                {
                    dev.Observaciones = wItemObservaciones;
                }
                else
                { // Apreto Cancelar
                    dev.Observaciones = "";
                }
                Cursor.Current = Cursors.WaitCursor;
                //Mercaderia Nueva
                if (devMnlistView.Items.Count > 0)
                {
                    for (int i = 0; i < devMnlistView.Items.Count; i++)
                    {
                        dev.ADDItem(devMnlistView.Items[i].SubItems[11].Text.ToString(),
                                    Int16.Parse(devMnlistView.Items[i].SubItems[2].Text.ToString()),
                                    byte.Parse(devMnlistView.Items[i].SubItems[8].Text.ToString()),
                                    devMnlistView.Items[i].SubItems[3].Text.ToString(),
                                    byte.Parse(devMnlistView.Items[i].SubItems[12].Text.ToString()),
                                    "",
                                    "",
                                    "",
                                    "",
                                    devMnlistView.Items[i].SubItems[9].Text.ToString());
                    }
                }

                //Mercaderia Fallada
                if (devMflistView.Items.Count > 0)
                {
                    for (int i = 0; i < devMflistView.Items.Count; i++)
                    {
                        dev.ADDItem(devMflistView.Items[i].SubItems[11].Text.ToString(),
                                    Int16.Parse(devMflistView.Items[i].SubItems[2].Text.ToString()),
                                    byte.Parse(devMflistView.Items[i].SubItems[8].Text.ToString()),
                                    devMflistView.Items[i].SubItems[3].Text.ToString(),
                                    byte.Parse(devMflistView.Items[i].SubItems[12].Text.ToString()),
                                    devMflistView.Items[i].SubItems[4].Text.ToString(),
                                    devMflistView.Items[i].SubItems[5].Text.ToString(),
                                    devMflistView.Items[i].SubItems[6].Text.ToString(),
                                    devMflistView.Items[i].SubItems[7].Text.ToString(),
                                    devMflistView.Items[i].SubItems[9].Text.ToString());
                    }
                }

                dev.Guardar("grabar");

                Devolucion_Imprimir(Global01.NroImprimir);
                Global01.NroImprimir = "";
                
                CerrarDevolucion();
                devMflistView.Items.Clear();
                devMnlistView.Items.Clear();

                devMfDepositoCbo.SelectedIndex = short.Parse(Funciones.modINIs.ReadINI("Preferencias", "Deposito", "0"));
                devMnDepositoCbo.SelectedIndex = short.Parse(Funciones.modINIs.ReadINI("Preferencias", "Deposito", "0"));  
            }


        }

        private void btnVer_Click(object sender, EventArgs e)
        {

            if (devMflistView.Items.Count > 0 | devMnlistView.Items.Count > 0)
            {
                Cursor.Current = Cursors.WaitCursor;

                Catalogo._devoluciones.Devolucion dev = new Catalogo._devoluciones.Devolucion(Global01.Conexion, Global01.NroUsuario.ToString(), Int16.Parse(cboCliente.SelectedValue.ToString()));
                dev.NroImpresion = 0;
                dev.Observaciones = "";

                //Mercaderia Nueva
                if (devMnlistView.Items.Count > 0)
                {
                    for (int i = 0; i < devMnlistView.Items.Count; i++)
                    {
                        dev.ADDItem(devMnlistView.Items[i].SubItems[11].Text.ToString(),
                                    Int16.Parse(devMnlistView.Items[i].SubItems[2].Text.ToString()),
                                    byte.Parse(devMnlistView.Items[i].SubItems[8].Text.ToString()),
                                    devMnlistView.Items[i].SubItems[3].Text.ToString(),
                                    byte.Parse(devMnlistView.Items[i].SubItems[12].Text.ToString()),
                                    "",
                                    "",
                                    "",
                                    "",
                                    devMnlistView.Items[i].SubItems[9].Text.ToString());
                    }
                }

                //Mercaderia Fallada
                if (devMflistView.Items.Count > 0)
                {
                    for (int i = 0; i < devMflistView.Items.Count; i++)
                    {
                        dev.ADDItem(devMflistView.Items[i].SubItems[11].Text.ToString(),
                                    Int16.Parse(devMflistView.Items[i].SubItems[2].Text.ToString()),
                                    byte.Parse(devMflistView.Items[i].SubItems[8].Text.ToString()),
                                    devMflistView.Items[i].SubItems[3].Text.ToString(),
                                    byte.Parse(devMflistView.Items[i].SubItems[12].Text.ToString()),
                                    devMflistView.Items[i].SubItems[4].Text.ToString(),
                                    devMflistView.Items[i].SubItems[5].Text.ToString(),
                                    devMflistView.Items[i].SubItems[6].Text.ToString(),
                                    devMflistView.Items[i].SubItems[7].Text.ToString(),
                                    devMflistView.Items[i].SubItems[9].Text.ToString());
                    }
                }

                dev.Guardar("VER");

                Devolucion_Imprimir(Global01.NroImprimir);
                Global01.NroImprimir = "";
            };
        }

        public static void Devolucion_Imprimir(string NroDevolucion)
        {
            string sReporte = "";

            sReporte = Global01.AppPath + "\\Reportes\\Devolucion_Enc3.rpt";
       
            ReportDocument oReport = new ReportDocument();

            oReport.Load(sReporte);
            Funciones.util.ChangeReportConnectionInfo(ref oReport);

            oReport.SetParameterValue("pNroDevolucion", NroDevolucion);

            //oReport.TiTle = "P - " + NroPedido;

            varios.fReporte f = new varios.fReporte();
            f.Text = "Nota de Devolución n° " + NroDevolucion;
            f.DocumentoNro = "DE-" + NroDevolucion;
            f.oRpt = oReport;
            f.ShowDialog();
            f.Dispose();
            f = null;
            oReport.Dispose();
        }

        private void paDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (Global01.AppActiva)
            {               
               //[CTRL + M] ' Marcado de Devolucions, Recibos y Devoluciones como enviadas en forma manual
               if (e.KeyCode==Keys.M && e.Modifiers==Keys.Control)
               {
                    if (paEnviosCbo.Text.ToString().ToUpper()== "NO ENVIADOS")
                    {
                        if (paDataGridView.SelectedRows != null) 
                        {
                            if (MessageBox.Show("CUIDADO!! a los Items marcados NO podrá enviarlos electrónicamente, ¿Está Seguro?", "Marcando como: ENVIADO EN FORMA MANUAL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)                                                       
                            {
                                foreach (DataGridViewRow row in paDataGridView.SelectedRows)
                                {
                                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper()=="DEVO") 
                                    {
                                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_Devolucion_Transmicion_Upd '" + row.Cells["Nro"].Value.ToString() + "'");
                                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblDevolucion_Enc SET Observaciones='ENVIADO EN FORMA MANUAL' WHERE NroDevolucion='" + row.Cells["Nro"].Value.ToString() + "'");
                                    }
                                    paDataGridView.ClearSelection();
                                }
                                ObtenerMovimientos(); 
                            }
                        }
                    }
                }
            }
        }

        private void devMnAgregarBtn_Click(object sender, EventArgs e)
        {
            DevMnAgregar();
        }

        private void DevMnAgregar()
        {
            if (btnIniciar.Tag.ToString() == "CANCELAR")
            {
                bool existe = false;
                string tDevolucion = "0";

                if (ProductoSeleccionado != null)
                {
                    if (!devMnMalSolicitadoRb.Checked && !devMnMalEnviadoRb.Checked && !devMnErrorPedidoRb.Checked && !devMnErrorModeloRb.Checked)
                    {
                        MessageBox.Show("Debe completar datos para la DEVOLUCION!", "atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (devMnMalSolicitadoRb.Checked)
                    {
                        tDevolucion = "5";
                    }
                    else if (devMnMalEnviadoRb.Checked)
                    {
                        tDevolucion = "4";
                    }
                    else if (devMnErrorPedidoRb.Checked)
                    {
                        tDevolucion = "10";
                    }
                    else if (devMnErrorModeloRb.Checked)
                    {
                        tDevolucion = "8";
                    }

                    int ii = 0;
                    for (int i = 0; i < devMnlistView.Items.Count; i++)
                    {
                        // si los codigo de producto son iguales
                        if (devMnlistView.Items[i].SubItems[10].Text.ToString().Trim().ToUpper() == ProductoSeleccionado.Cells["CodigoAns"].Value.ToString().Trim().ToUpper())
                        {
                            existe = true;
                            devMnlistView.Items[i].Selected = true;
                            ii = i;
                            break;
                        }
                    }

                    if (existe)
                    {
                        if ((Decimal.Parse(devMnlistView.Items[ii].SubItems[2].Text.ToString()) + devMnCantidadTxt.Value) < 1000)
                        {
                            devMnlistView.Items[ii].SubItems[2].Text = (Decimal.Parse(devMnlistView.Items[ii].SubItems[2].Text.ToString()) + devMnCantidadTxt.Value).ToString();
                        }
                        devMnlistView.Items[ii].SubItems[8].Text = devMnDepositoCbo.SelectedValue.ToString();
                    }
                    else
                    {
                        ListViewItem ItemX = new ListViewItem(ProductoSeleccionado.Cells["C_Producto"].Value.ToString());
                        ////alternate row color
                        if (devMnlistView.Items.Count % 2 != 0)
                        {
                            ItemX.BackColor = System.Drawing.SystemColors.Control; //System.Drawing.Color.FromArgb(255, 255, 192);
                        }

                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["N_Producto"].Value.ToString());    //01
                        ItemX.SubItems.Add(devMnCantidadTxt.Value.ToString());                            //02
                        ItemX.SubItems.Add(devMnFacturaTxt.Text.ToString());                              //03

                        ItemX.SubItems.Add((tDevolucion == "5" ? "*" : " "));
                        ItemX.SubItems.Add((tDevolucion == "4" ? "*" : " "));
                        ItemX.SubItems.Add((tDevolucion == "10" ? "*" : " "));
                        ItemX.SubItems.Add((tDevolucion == "8" ? "*" : " "));

                        ItemX.SubItems.Add(devMnDepositoCbo.SelectedValue.ToString());                    //08
                        ItemX.SubItems.Add(devMnObservacionesTxt.Text);                                   //09
                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["CodigoAns"].Value.ToString());     //10
                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["ID"].Value.ToString());            //11
                        ItemX.SubItems.Add(tDevolucion);                                              // Mercaderia Fallada
                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["Linea"].Value.ToString());         //13                            

                        devMnlistView.Items.Add(ItemX);
                        devMnlistView.Items[ItemX.Index].Selected = true;
                        Funciones.util.AutoSizeLVColumnas(ref devMnlistView);

                    }

                    LimpiarIngresoDevolucion();
                }
            }
        }

        private void devMfAgregarBtn_Click(object sender, EventArgs e)
        {
            DevMfAgregar();
        }

        private void DevMfAgregar()
        {
            if (btnIniciar.Tag.ToString() == "CANCELAR")
            {
                bool existe = false;
                string tDevolucion = "1";                

                if (ProductoSeleccionado != null)
                {           
                    if (!(devMfVehiculoCbo.SelectedIndex > 0 && devMfModeloCbo.SelectedIndex > 0 && devMfMotorTxt.ToString().Trim().Length > 0 && devMfKmTxt.ToString().Trim().Length > 0))
                    {
                        MessageBox.Show("Debe completar datos para la DEVOLUCION!", "atención",MessageBoxButtons.OK,MessageBoxIcon.Exclamation); 
                        return;
                    }

                    int ii = 0;
                    for (int i = 0; i < devMflistView.Items.Count; i++)
                    {
                        // si los codigo de producto son iguales
                        if (devMflistView.Items[i].SubItems[10].Text.ToString().Trim().ToUpper() == ProductoSeleccionado.Cells["CodigoAns"].Value.ToString().Trim().ToUpper())
                        {
                            existe = true;
                            devMflistView.Items[i].Selected = true;
                            ii = i;
                            break;
                        }
                    }

                    if (existe)
                    {
                        if ((Decimal.Parse(devMflistView.Items[ii].SubItems[2].Text.ToString()) + devMfCantidadTxt.Value) < 1000)
                        {
                            devMflistView.Items[ii].SubItems[2].Text = (Decimal.Parse(devMflistView.Items[ii].SubItems[2].Text.ToString()) + devMfCantidadTxt.Value).ToString();
                        }
                        devMflistView.Items[ii].SubItems[8].Text = devMfDepositoCbo.SelectedValue.ToString();
                    }
                    else
                    {
                        ListViewItem ItemX = new ListViewItem(ProductoSeleccionado.Cells["C_Producto"].Value.ToString());
                        ////alternate row color
                        if (devMflistView.Items.Count % 2 != 0)
                        {
                            ItemX.BackColor = System.Drawing.SystemColors.Control; //System.Drawing.Color.FromArgb(255, 255, 192);
                        }

                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["N_Producto"].Value.ToString());    //01
                        ItemX.SubItems.Add(devMfCantidadTxt.Value.ToString());                            //02
                        ItemX.SubItems.Add(devMfFacturaTxt.Text.ToString());                              //03
                        ItemX.SubItems.Add(devMfVehiculoCbo.Text.ToString());                    //04
                        ItemX.SubItems.Add(devMfModeloCbo.Text.ToString());                      //05
                        ItemX.SubItems.Add(devMfMotorTxt.Text.ToString());                                //06
                        ItemX.SubItems.Add(devMfKmTxt.Text.ToString());                                   //07
                        ItemX.SubItems.Add(devMfDepositoCbo.SelectedValue.ToString());                    //08
                        ItemX.SubItems.Add(devMfObservacionesTxt.Text);                                   //09
                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["CodigoAns"].Value.ToString());     //10
                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["ID"].Value.ToString());            //11
                        ItemX.SubItems.Add(tDevolucion);                                              // Mercaderia Fallada
                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["Linea"].Value.ToString());         //13                            

                        devMflistView.Items.Add(ItemX);
                        devMflistView.Items[ItemX.Index].Selected = true;
                        Funciones.util.AutoSizeLVColumnas(ref devMflistView);
                    }

                    LimpiarIngresoDevolucion();
                }
            }
     
        }

        private void LimpiarIngresoDevolucion()
        {        
            //cboModeloDev.Clear();

            devMnlistView.SelectedItems.Clear();
            devMnCantidadTxt.Value = 1;
            devMnFacturaTxt.Text = "";
            //devMnDepositoCbo.SelectedIndex = 0;
            devMnObservacionesTxt.Text = "";

            devMnMalSolicitadoRb.Checked = false;
            devMnMalEnviadoRb.Checked = false;
            devMnErrorPedidoRb.Checked = false;
            devMnErrorModeloRb.Checked = false;

            devMflistView.SelectedItems.Clear();
            devMfCantidadTxt.Value = 1;
            devMfFacturaTxt.Text = "";
            //devMfVehiculoCbo.SelectedIndex = 0;
            //devMfModeloCbo.SelectedIndex = 0;
            devMfMotorTxt.Text = "";
            devMfKmTxt.Text = "";
            //devMfDepositoCbo.SelectedIndex = 0;
            devMfObservacionesTxt.Text = "";
        }

        private void devMfVehiculoCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (devMfVehiculoCbo.SelectedItem != null)
            {

                _productos.FilterBuilder fb = new _productos.FilterBuilder();

                if (devMfVehiculoCbo.SelectedItem.ToString() != "(todos)")
                {
                    DataTable dtVehiculos = new DataTable();
                    dtVehiculos = Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "tblMarcasModelos");

                    fb.PopulateFilter(ref Filter_Modelo, dtVehiculos, "_mm" + devMfVehiculoCbo.SelectedItem.ToString());
                    String[] filterPriceArray = new String[Filter_Modelo.Count];
                    Filter_Modelo.Keys.CopyTo(filterPriceArray, 0);
                    devMfModeloCbo.Items.Clear();
                    devMfModeloCbo.Items.AddRange(filterPriceArray);
                    devMfModeloCbo.SelectedIndex = 0;

                }
                else
                {
                    DataTable dtVehiculos = new DataTable();
                    dtVehiculos = Funciones.oleDbFunciones.xGetDt(Global01.Conexion, "tblModelos");

                    fb.PopulateFilter(ref Filter_Modelo, dtVehiculos, "Modelo");
                    String[] filterPriceArray = new String[Filter_Modelo.Count];
                    Filter_Modelo.Keys.CopyTo(filterPriceArray, 0);
                    devMfModeloCbo.Items.Clear();
                    devMfModeloCbo.Items.AddRange(filterPriceArray);
                    devMfModeloCbo.SelectedIndex = 0;

                }

                fb = null;
            }
        }

        private void devMnlistView_DoubleClick(object sender, EventArgs e)
        {
            devMnlistView.SelectedItems[0].SubItems[6].Text = devMnDepositoCbo.Text; 
        }

        private void devMflistView_DoubleClick(object sender, EventArgs e)
        {
            devMflistView.SelectedItems[0].SubItems[6].Text = devMfDepositoCbo.Text; 
        }

        private void paDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Global01.AppActiva)
            {
                DataGridViewCell cell = paDataGridView[e.ColumnIndex, e.RowIndex];
                if (cell != null)
                {
                    DataGridViewRow row = cell.OwningRow;
                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "DEVO")
                    {
                        Devolucion_Imprimir(row.Cells["Nro"].Value.ToString());
                    }
                }
            }
        }

        public Funciones.emitter_receiver.emisorHandler<int> emisor
        {
            get;
            set;
        }

        public void onRecibir(short dato)
        {
            if (btnIniciar.Tag.ToString() == "INICIAR")            
                cboCliente.SelectedValue = dato;
        }

        private void EnviarBtn_Click(object sender, EventArgs e)
        {
            System.Collections.Generic.List<Catalogo.util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO> filtro
               = new List<Catalogo.util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO>();

            _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, int.Parse(cboCliente.SelectedValue.ToString()));
            System.Data.OleDb.OleDbDataReader dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, "DEVOLUCION");

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO mov = new util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO();
                    mov.origen = "DEVOLUCION";
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