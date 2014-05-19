using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo._movimientos
{
    public partial class ucMovimientos : UserControl,
        Funciones.emitter_receiver.IReceptor<short> // Para recibir una notificacion de cambio del cliente seleccionado
     {
       
        public ucMovimientos()
        {
            InitializeComponent();

            if (!Global01.AppActiva | Global01.Conexion == null)
            {
                this.Dispose();
            }

            //movDataGridView.RowPostPaint += OnRowPostPaint;

            movDataGridView.CellPainting += OnCellPainting;

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

            Catalogo.varios.NotificationCenter.instance.attachReceptor2(this);
            cboCliente.SelectedValue = Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado;
        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            //paEnviosCbo.SelectedIndex = 2;

            if (cboCliente.SelectedIndex > 0)
            {
                toolStripStatusLabel1.Text = "Movimientos para el cliente: " + this.cboCliente.Text.ToString();
            }
            else
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Movimientos para el cliente ..."; }
            }
            ObtenerMovimientos();
            Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado = (short)cboCliente.SelectedValue;
        }

         private void ucMovimientos_Load(object sender, EventArgs e)
        {         
            DocTipoCbo.SelectedIndex = 0;
            paEnviosCbo.SelectedIndex = 2;
            ObtenerMovimientos();
        }

        private void EnviosCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtenerMovimientos();
        }

        private void DocTipoCbo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtenerMovimientos();
        }

        private void ObtenerMovimientos()
        {
            movDataGridView.Visible = false;

            movDataGridView.Columns["Estado"].Visible = false;

            Int16 xClienteSelected = 0;
            if (cboCliente.SelectedValue != null) xClienteSelected = Int16.Parse(cboCliente.SelectedValue.ToString());

            _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, xClienteSelected);
            System.Data.OleDb.OleDbDataReader dr = null;

            if (paEnviosCbo.SelectedIndex == 0)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.TODOS, DocTipoCbo.Text.ToString());
            }
            else if (paEnviosCbo.SelectedIndex == 1)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.ENVIADOS, DocTipoCbo.Text.ToString());
            }
            else if (paEnviosCbo.SelectedIndex == 2)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, DocTipoCbo.Text.ToString());
            }

            if (dr != null)
            {
                if (dr.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Selec", System.Type.GetType("System.Boolean"));  
   
                    dt.Load(dr);

                    movDataGridView.AutoGenerateColumns = true;
                    movDataGridView.DataSource = dt;

                    movDataGridView.Columns["Selec"].Visible = (paEnviosCbo.Text.ToString().ToUpper() == "NO ENVIADOS");
                    movDataGridView.Columns["Estado"].Visible = (paEnviosCbo.Text.ToString().ToUpper() == "ENVIADOS");

                    movDataGridView.Columns["Selec"].Width = 30;

                    movDataGridView.Refresh();
                    movDataGridView.Visible = true;
                }
            }
        }

        private void movDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Global01.AppActiva)
                {
                    DataGridViewCell cell = movDataGridView[e.ColumnIndex, e.RowIndex];
                    if (cell != null)
                    {
                        DataGridViewRow row = cell.OwningRow;
                        if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "NOTA")
                        {
                            if ((paEnviosCbo.SelectedIndex == 1) && (e.ColumnIndex == 0))
                            {
                                Catalogo.util.BackgroundTasks.EstadoPedido Estado = new util.BackgroundTasks.EstadoPedido(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico);
                                Estado.onCancelled += EstadoPedidoCancelled;
                                Estado.onFinished += EstadoPedidoFinished;
                                Estado.getEstado(row.Cells["Nro"].Value.ToString(), Global01.NroUsuario, cell);
                            }
                            else
                            {
                                _pedidos.ucPedido.Pedido_Imprimir(row.Cells["Nro"].Value.ToString());
                            }        
                        }
                        else if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "RECI")
                        {
                            _recibos.ucRecibo.Recibo_Imprimir(row.Cells["Nro"].Value.ToString());
                        }
                        if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "DEVO")
                        {
                            _devoluciones.ucDevolucion.Devolucion_Imprimir(row.Cells["Nro"].Value.ToString());
                        }
                        if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "INTE")
                        {
                            _interdeposito.ucInterDeposito.InterDeposito_Imprimir(row.Cells["Nro"].Value.ToString());
                        }
                        if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "REND")
                        {
                            _rendiciones.ucRendiciones.Rendicion_Imprimir(row.Cells["Nro"].Value.ToString());
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

        private void movDataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Global01.AppActiva)
                {
                    //[CTRL + M] ' Marcado de Pedidos, Recibos y Devoluciones como enviadas en forma manual
                    if (e.KeyCode == Keys.M && e.Modifiers == Keys.Control)
                    {
                        if (paEnviosCbo.Text.ToString().ToUpper() == "NO ENVIADOS")
                        {
                            if (movDataGridView.SelectedRows != null)
                            {
                                if (MessageBox.Show("CUIDADO!! a los Items marcados NO podrá enviarlos electrónicamente, ¿Está Seguro?", "Marcando como: ENVIADO EN FORMA MANUAL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    foreach (DataGridViewRow row in movDataGridView.SelectedRows)
                                    {
                                        if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "NOTA")
                                        {
                                            Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_Pedido_Transmicion_Upd '" + row.Cells["Nro"].Value.ToString() + "'");
                                            Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblPedido_Enc SET Observaciones='ENVIADO EN FORMA MANUAL' WHERE NroPedido='" + row.Cells["Nro"].Value.ToString() + "'");
                                        }
                                        else if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "DEVO")
                                        {
                                            Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_Devolucion_Transmicion_Upd '" + row.Cells["Nro"].Value.ToString() + "'");
                                            Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblDevolucion_Enc SET Observaciones='ENVIADO EN FORMA MANUAL' WHERE NroDevolucion='" + row.Cells["Nro"].Value.ToString() + "'");
                                        }
                                        else if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "RECI")
                                        {
                                            MessageBox.Show("opción no disponible para recibos", "atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            //Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_Recibo_Transmicion_Upd '" + row.Cells["Nro"].Value.ToString() + "'");
                                            //Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblRecibo_Enc SET Observaciones='ENVIADO EN FORMA MANUAL' WHERE NroRecibo='" + row.Cells["Nro"].Value.ToString() + "'");
                                        }
                                        else if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "INTE")
                                        {
                                            Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_InterDeposito_Transmicion_Upd '" + row.Cells["Nro"].Value.ToString() + "'");
                                            //Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE tblInterDepotito SET Observaciones='ENVIADO EN FORMA MANUAL' WHERE NroInterDeposito='" & row.Cells["Nro"].Value.ToString() + "'");
                                        }
                                        //paDataGridView.Rows.Remove(row);
                                        movDataGridView.ClearSelection();
                                    }
                                    ObtenerMovimientos();
                                }
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

        private void EnviarBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global01.AppActiva)
                {
                    if (paEnviosCbo.Text.ToString().ToUpper() == "NO ENVIADOS")
                    {
                        if (movDataGridView.SelectedRows != null && movDataGridView.SelectedRows.Count > 0)
                        {
                            if (MessageBox.Show("Debe estar conectado a Internet. ¿QUIERE ENVIARLOS AHORA?", "Envio de Movimientos", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                System.Collections.Generic.List<Catalogo.util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO> filtro = new List<util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO>();

                                foreach (DataGridViewRow row in movDataGridView.Rows)
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
     
        public Funciones.emitter_receiver.emisorHandler<int> emisor
        {
            get;
            set;
        }

        public void onRecibir(short dato)
        {
           cboCliente.SelectedValue = dato;
        }
  
        void OnCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex > -1)
            {
                System.Windows.Forms.DataGridViewCell cell = movDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell != null)
                {
                    Brush brush;

                    if (cell.Tag != null)
                    {
                        string Estado = (string)cell.Tag;
                        switch (Estado)
                        {
                            case "1":
                                brush = Brushes.Red;
                                break;
                            case "2":
                                brush = Brushes.Yellow;
                                break;
                            case "3":
                                brush = Brushes.Green;
                                break;
                            case "4":
                                brush = Brushes.Green;
                                break;
                            case "5":
                                brush = Brushes.Green;
                                break;
                            default:
                                if (Estado.Trim().Length > 3)
                                {
                                    brush = Brushes.Cyan;
                                }
                                else
                                {
                                    brush = Brushes.DarkGray;
                                }
                                break;
                        }
                    }
                    else
                    {
                        // aca hay que dibujar sin informacion
                        brush = Brushes.DarkGray;
                    }

                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                    Rectangle rect = e.CellBounds;
                    rect.Inflate(-5, -4);
                    e.Graphics.FillEllipse(brush, rect);
                    e.Handled = true;
                }
            }
        }

        private void EstadoPedidoCancelled(System.Windows.Forms.DataGridViewCell cell)
        {
            throw new NotImplementedException();
        }

        private void EstadoPedidoFinished(string PedidoNro, string resultado, System.Windows.Forms.DataGridViewCell cell)
        {
            if (resultado.IndexOf(";") > 0)
            {
                bool xMostrar = false;

                string[] stringSeparators = new string[] { ";" };
                string[] aResultado = resultado.Split(stringSeparators, StringSplitOptions.None);
                cell.Tag = aResultado[0];

                if (aResultado[0].Trim().Length > 3)
                {
                    cell.ToolTipText = ((Global01.miSABOR > Global01.TiposDeCatalogo.Cliente) ? aResultado[0] : "");
                }
                else
                {
                    xMostrar = true;
                    switch (aResultado[0])
                    {
                        case "1":
                            cell.ToolTipText = "E1 -" + aResultado[1]; 
                            break;
                        case "2":
                            cell.ToolTipText = "E2 -" + aResultado[1]; 
                            break;
                        case "3":
                            cell.ToolTipText = "E3 -" + aResultado[1];
                            break;
                        case "4":
                            cell.ToolTipText = "E4 -" + aResultado[1];
                            break;
                        case "5":
                            cell.ToolTipText = "E5 -" + aResultado[1];
                            break;
                    }
                }

                if (xMostrar)
                {
                    _pedidos.EstadoPedidoMostrar fEstadoPedido = new _pedidos.EstadoPedidoMostrar();

                    fEstadoPedido.EstadoMsg = cell.ToolTipText.ToString();
                    fEstadoPedido.EstadoActivo = cell.Tag.ToString();

                    fEstadoPedido.ShowDialog();
                    fEstadoPedido.Dispose();
                    fEstadoPedido = null;
                };

            }
        }
        
        internal void actualizarMovimientos()
        {
            paEnviosCbo.SelectedIndex = 2;
            ObtenerMovimientos();
        }

     } //fin clase
} //fin namespace