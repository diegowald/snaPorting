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

namespace Catalogo._pedidos
{
    public partial class ucPedido : UserControl,
        Funciones.emitter_receiver.IReceptor<System.Windows.Forms.DataGridViewRow>, // Para recibir el producto seleccionado
        Funciones.emitter_receiver.IReceptor<_pedidos.PedidosHelper.Acciones>, // Para recibir acciones al pedido desde la grilla de productos.
        Funciones.emitter_receiver.IEmisor<int>, // Para enviar el indice del cliente seleccionado en el combo
        Funciones.emitter_receiver.IReceptor<int> // Para recibir una notificacion de cambio del cliente seleccionado
    {
        //private //const string m_sMODULENAME_ = "ucPedido";
        ToolTip _ToolTip = new System.Windows.Forms.ToolTip();
        DataGridViewRow ProductoSeleccionado = null;

        public ucPedido()
        {
            InitializeComponent();
            _ToolTip.SetToolTip(btnIniciar, "INICIAR Nota de Venta");
            _ToolTip.SetToolTip(btnImprimir, "Graba e Imprime Nota de Venta ...");
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
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1 and (IdViajante=" + Global01.NroUsuario.ToString() + " or IdViajante=" + Global01.Zona.ToString() + ")", "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Format([ID],'00000') & ')' AS Cliente, ID");
                if (Global01.miSABOR == Global01.TiposDeCatalogo.Cliente) cboCliente.SelectedValue = Global01.NroUsuario;
            }

            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref nvTransporteCbo, "ansTransportes", "Nombre", "ID", "Activo=1", "Nombre", true, false, "NONE");
            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref nvDepositoCbo, "v_Deposito", "D_Dep", "IdDep", "ALL", "D_Dep", true, false, "NONE");

        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                paEnviosCbo.SelectedIndex = 2;

                if (cboCliente.SelectedIndex > 0)
                {
                    toolStripStatusLabel1.Text = "Nota de Venta para el cliente: " + this.cboCliente.Text.ToString();
                    btnIniciar.Enabled = true;
                }
                else
                {
                    if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Pedido para el cliente ..."; }
                    btnIniciar.Enabled = false;
                }
                this.emitir(cboCliente.SelectedIndex);
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex;  //util.errorHandling.ErrorForm.show();
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
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.TODOS, "NOTA DE VENTA");
            }
            else if (paEnviosCbo.SelectedIndex == 1)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.ENVIADOS, "NOTA DE VENTA");
            }
            else if (paEnviosCbo.SelectedIndex == 2)
            {
                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, "NOTA DE VENTA");
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
            try
            {
                if (Global01.OperacionActivada == "DEVOLUCION")
                {
                    MessageBox.Show("Debe cerrar la DEVOLUCION para comenzar la VENTA", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    if (cboCliente.SelectedIndex > 0)
                    {
                        if (btnIniciar.Tag.ToString() == "INICIAR")
                        {
                            _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Pedido,
                                _auditor.Auditor.AccionesAuditadas.INICIA, "");
                            nvlistView.Items.Clear();

                            OleDbDataReader dr = null;

                            if (Funciones.modINIs.ReadINI("DATOS", "PedidoNE", "1") == "1")
                            {
                                _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, int.Parse(cboCliente.SelectedValue.ToString()));
                                dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, "NOTA DE VENTA");
                                if (dr.HasRows)
                                {
                                    MessageBox.Show("Hay un pedido pendiente de envio, sugerimos: \n Ir a pedidos anteriores (no enviados) abrirlo y continuar con el mismo", "atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    paEnviosCbo.SelectedIndex = 2;
                                    PedidoTab.SelectedIndex = 1;
                                    nvAntTab.Select();
                                    PedidoTab.Visible = true;

                                    return;
                                }
                            }

                            dr = Funciones.oleDbFunciones.Comando(Global01.Conexion, "SELECT * FROM tblPedido_Bkp WHERE IdCliente=" + cboCliente.SelectedValue.ToString());
                            if (dr.HasRows)
                            {
                                if (MessageBox.Show("¿Desea RECUPERAR la copia del pedido anterior?", "atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {

                                    while (dr.Read())
                                    {
                                        ListViewItem ItemX = new ListViewItem(dr["CodigoCorto"].ToString());
                                        ////alternate row color
                                        if (nvlistView.Items.Count % 2 != 0)
                                        {
                                            ItemX.BackColor = System.Drawing.SystemColors.Control; //System.Drawing.Color.FromArgb(255, 255, 192);
                                        }

                                        ItemX.SubItems.Add(dr["Descrip"].ToString());          //01
                                        ItemX.SubItems.Add(dr["Precio"].ToString());           //02
                                        ItemX.SubItems.Add(dr["Cantidad"].ToString());         //03
                                        ItemX.SubItems.Add(dr["SubTotal"].ToString());         //04
                                        ItemX.SubItems.Add(dr["Similar"].ToString());          //05
                                        ItemX.SubItems.Add(dr["Deposito"].ToString());         //06
                                        ItemX.SubItems.Add(dr["Oferta"].ToString());           //07
                                        ItemX.SubItems.Add(dr["IdCatalogo"].ToString());       //08
                                        ItemX.SubItems.Add(dr["Codigo"].ToString());           //09
                                        ItemX.SubItems.Add(dr["Observaciones"].ToString());    //10

                                        nvlistView.Items.Add(ItemX);
                                    }
                                    Funciones.util.AutoSizeLVColumnas(ref nvlistView);
                                }
                                else
                                {
                                    Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblPedido_Bkp");
                                }

                            }

                            dr = null;

                            // nvlistView.Items.Clear();
                            TotalPedido();
                            IniciarPedido();
                            HabilitarPedido();

                            nvSimilarChk.Checked = false;
                            nvEsOfertaChk.Checked = false;
                            nvDepositoCbo.SelectedIndex = short.Parse(Funciones.modINIs.ReadINI("Preferencias", "Deposito", "0"));
                            PedidoTab.SelectedIndex = 0;
                            PedidoTab.Visible = true;
                        }
                        else
                        {
                            if (MessageBox.Show("¿Esta Seguro que quiere CANCELAR el Pedido?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Pedido, _auditor.Auditor.AccionesAuditadas.CANCELA, "");
                                PedidoTab.Visible = false;
                                nvlistView.Items.Clear();
                                TotalPedido();
                                CerrarPedido();
                                InhabilitarPedido();

                                //if (Global01.miSABOR > Global01.TiposDeCatalogo.Cliente) cboCliente.SelectedIndex = 0;                                
                                nvSimilarChk.Checked = false;
                                nvEsOfertaChk.Checked = false;
                                nvDepositoCbo.SelectedIndex = short.Parse(Funciones.modINIs.ReadINI("Preferencias", "Deposito", "0"));
                            }
                        }

                        nvlistView.Tag = "-1";
                    }
                    else
                    {
                        MessageBox.Show("Seleccione un Cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        private void ucPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Char.IsControl(e.KeyChar) && e.KeyChar == ((char)Keys.D))
                {   //CTRL + D
                    if (btnIniciar.Tag.ToString() == "CANCELAR")
                    {
                        //VerDetallePedido();                  
                    }
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        private void CerrarPedido()
        {
            Global01.OperacionActivada = "nada";
            btnIniciar.Text = "Iniciar";
            btnIniciar.Tag = "INICIAR";

            _ToolTip.SetToolTip(btnIniciar, "INICIAR Pedido Nuevo");
            nvObservacionesTxt.Text = "";
            ObtenerMovimientos();
        }

        private void IniciarPedido()
        {
            Global01.OperacionActivada = "PEDIDO";
            btnIniciar.Text = "CANCELAR";
            btnIniciar.Tag = "CANCELAR";
            _ToolTip.SetToolTip(btnIniciar, "CANCELAR éste Pedido");
            nvObservacionesTxt.Text = "";
        }

        private void HabilitarPedido()
        {
            nvPnlTop.Enabled = true;
            nvPnlMain.Enabled = true;
            btnImprimir.Enabled = true;
            btnVer.Enabled = true;
            nvlistView.Enabled = true;
            cboCliente.Enabled = false;
        }

        private void InhabilitarPedido()
        {
            nvPnlTop.Enabled = false;
            nvPnlMain.Enabled = false;
            btnImprimir.Enabled = false;
            btnVer.Enabled = false;
            nvlistView.Enabled = false;
            cboCliente.Enabled = true;
        }

        private void TotalPedido()
        {
            float Aux = 0;

            if (nvlistView.Items.Count < 1)
            {
                nvImporteTotalLbl.Text = string.Format("{0:N2}", "0,00");
                return;
            }

            for (int i = 0; i < nvlistView.Items.Count; i++)
            {
                Aux = Aux + float.Parse(nvlistView.Items[i].SubItems[4].Text.ToString());
            }

            nvImporteTotalLbl.Text = string.Format("{0:N2}", Aux);
        }

        public void onRecibir(DataGridViewRow dato)
        {
            ProductoSeleccionado = dato;
        }

        private void nvComprarBtn_Click(object sender, EventArgs e)
        {
            cmdProductoAgregar();
        }

        public void onRecibir(_pedidos.PedidosHelper.Acciones dato)
        {
            switch (dato)
            {
                case _pedidos.PedidosHelper.Acciones.COMPRAR:
                    cmdProductoAgregar();
                    break;
                case _pedidos.PedidosHelper.Acciones.INCREMENTAR:
                    nvCantidadTxt.Value++;
                    break;
                case _pedidos.PedidosHelper.Acciones.DECREMENTAR:
                    if (nvCantidadTxt.Value > 1) {nvCantidadTxt.Value--;}
                    break;
                default:
                    break;
            }
        }

        private void cmdProductoAgregar()
        {
            if (btnIniciar.Tag.ToString() == "CANCELAR")
            {
                bool existe = false;

                if (ProductoSeleccionado != null)
                {

                    nvEsOfertaChk.Checked = (ProductoSeleccionado.Cells["Control"].Value.ToString() == "O" ? bool.Parse("true") : bool.Parse("false"));

                    if (ProductoSeleccionado.Cells["suspendido"].Value.ToString() == "1")
                    {
                        MessageBox.Show("El Artículo está suspendido momentáneamente", "atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    //if (m.EsEnterCantidad < 1) {
                    if (nvCantidadTxt.Value < Decimal.Parse(ProductoSeleccionado.Cells["OfertaCantidad"].Value.ToString()))
                    {
                        MessageBox.Show("Mínimo de oferta: " + ProductoSeleccionado.Cells["OfertaCantidad"].Value.ToString() + " unidades", "atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //m.EsEnterCantidad = m.EsEnterCantidad + 1
                        //return;
                    }
                    //}

                    int ii = 0;
                    for (int i = 0; i < nvlistView.Items.Count; i++)
                    {
                        // si los codigo de producto son iguales
                        if (nvlistView.Items[i].SubItems[9].Text.ToString().Trim().ToUpper() == ProductoSeleccionado.Cells["CodigoAns"].Value.ToString().Trim().ToUpper())
                        {
                            existe = true;
                            nvlistView.Items[i].Selected = true;
                            ii = i;
                            break;
                        }
                    }

                    if (existe)
                    {
                        if ((Decimal.Parse(nvlistView.Items[ii].SubItems[3].Text.ToString()) + nvCantidadTxt.Value) < 1000)
                        {
                            nvlistView.Items[ii].SubItems[3].Text = (Decimal.Parse(nvlistView.Items[ii].SubItems[3].Text.ToString()) + nvCantidadTxt.Value).ToString();
                            nvlistView.Items[ii].SubItems[4].Text = (float.Parse(nvlistView.Items[ii].SubItems[3].Text.ToString()) * float.Parse(ProductoSeleccionado.Cells["PrecioLista"].Value.ToString())).ToString();
                        }
                        nvlistView.Items[ii].SubItems[5].Text = (nvSimilarChk.Checked ? "1" : "0");
                        nvlistView.Items[ii].SubItems[6].Text = nvDepositoCbo.SelectedValue.ToString();
                    }
                    else
                    {
                        ListViewItem ItemX = new ListViewItem(ProductoSeleccionado.Cells["C_Producto"].Value.ToString());
                        ////alternate row color
                        if (nvlistView.Items.Count % 2 != 0)
                        {
                            ItemX.BackColor = System.Drawing.SystemColors.Control;  //System.Drawing.Color.FromArgb(255, 255, 192);
                        }

                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["N_Producto"].Value.ToString());
                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["PrecioLista"].Value.ToString());
                        ItemX.SubItems.Add(nvCantidadTxt.Value.ToString());
                        float pTotal = float.Parse(nvCantidadTxt.Value.ToString()) * float.Parse(ProductoSeleccionado.Cells["PrecioLista"].Value.ToString());
                        ItemX.SubItems.Add(pTotal.ToString());                                          //04
                        ItemX.SubItems.Add((nvSimilarChk.Checked ? "1" : "0"));                         //05
                        ItemX.SubItems.Add(nvDepositoCbo.SelectedValue.ToString());                     //06
                        ItemX.SubItems.Add((nvEsOfertaChk.Checked ? "1" : "0")); // ¿ es oferta ?       //07    
                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["ID"].Value.ToString());          //08
                        ItemX.SubItems.Add(ProductoSeleccionado.Cells["CodigoAns"].Value.ToString());   //09    
                        ItemX.SubItems.Add(nvObservacionesTxt.Text);                                    //10

                        nvlistView.Items.Add(ItemX);
                        nvlistView.Items[ItemX.Index].Selected = true;
                        Funciones.util.AutoSizeLVColumnas(ref nvlistView);

                    }

                    Pedido_bkp(Global01.Conexion,
                                Int32.Parse(cboCliente.SelectedValue.ToString()), 
                                nvlistView.SelectedItems[0].SubItems[9].Text,
                                nvlistView.SelectedItems[0].SubItems[1].Text,
                                float.Parse(nvlistView.SelectedItems[0].SubItems[2].Text.ToString()),
                                Int16.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()),
                                float.Parse(nvlistView.SelectedItems[0].SubItems[4].Text.ToString()),
                                short.Parse(nvlistView.SelectedItems[0].SubItems[5].Text.ToString()),
                                short.Parse(nvlistView.SelectedItems[0].SubItems[6].Text.ToString()),
                                short.Parse(nvlistView.SelectedItems[0].SubItems[7].Text.ToString()),
                                nvlistView.SelectedItems[0].SubItems[8].Text,                                
                                nvlistView.SelectedItems[0].Text,
                                nvlistView.SelectedItems[0].SubItems[10].Text,
                                existe);
                    
                    nvlistView.SelectedItems.Clear();

                    nvEsOfertaChk.Checked = false;
                    nvSimilarChk.Checked = false;
                    nvCantidadTxt.Value = 1;

                    TotalPedido();                    
                }
            }
        }
        
        private void nvlistView_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (nvlistView.SelectedItems != null & nvlistView.SelectedItems.Count > 0)
                {
                    bool wEntro = false;

                    if (e.KeyCode == Keys.O && e.Modifiers == Keys.Control)
                    {    //Observaciones del Item

                        string wItemObservaciones = nvlistView.SelectedItems[0].SubItems[10].Text;
                        if (Funciones.util.InputBox(" (Presione Cancelar para quitar la Observación)  ", "Observaciones para el Item", 80, ref wItemObservaciones) == DialogResult.OK)
                        {
                            nvlistView.SelectedItems[0].SubItems[10].Text = wItemObservaciones;
                        }
                        else
                        { // Apreto Cancelar
                            nvlistView.SelectedItems[0].SubItems[10].Text = "";
                        }
                        wEntro = true;
                    }
                    else if (e.KeyCode == Keys.Delete)
                    {  //DEL
                        Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblPedido_Bkp WHERE IdCatalogo='" + nvlistView.SelectedItems[0].SubItems[8].Text.ToString() + "'");
                        nvlistView.Items.Remove(nvlistView.SelectedItems[0]);

                        nvlistView.SelectedItems.Clear();
                        TotalPedido();
                    }
                    else if (e.KeyValue.ToString() == "187")
                    {
                        if (Decimal.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) < 999)
                        {
                            nvlistView.SelectedItems[0].SubItems[3].Text = (Decimal.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) + 1).ToString();
                            nvlistView.SelectedItems[0].SubItems[4].Text = (float.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) * float.Parse(nvlistView.SelectedItems[0].SubItems[2].Text.ToString())).ToString();
                            wEntro = true;
                        }
                    }
                    else if (e.KeyValue.ToString() == "189")
                    {
                        if (Decimal.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) > 1)
                        {
                            nvlistView.SelectedItems[0].SubItems[3].Text = (Decimal.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) - 1).ToString();
                            nvlistView.SelectedItems[0].SubItems[4].Text = (float.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) * float.Parse(nvlistView.SelectedItems[0].SubItems[2].Text.ToString())).ToString();
                            wEntro = true;
                        }
                    }

                    if (wEntro)
                    {
                        Pedido_bkp(Global01.Conexion,
                                    Int32.Parse(cboCliente.SelectedValue.ToString()),
                                    nvlistView.SelectedItems[0].SubItems[9].Text,
                                    nvlistView.SelectedItems[0].SubItems[1].Text,
                                    float.Parse(nvlistView.SelectedItems[0].SubItems[2].Text.ToString()),
                                    Int16.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()),
                                    float.Parse(nvlistView.SelectedItems[0].SubItems[4].Text.ToString()),
                                    short.Parse(nvlistView.SelectedItems[0].SubItems[5].Text.ToString()),
                                    short.Parse(nvlistView.SelectedItems[0].SubItems[6].Text.ToString()),
                                    short.Parse(nvlistView.SelectedItems[0].SubItems[7].Text.ToString()),
                                    nvlistView.SelectedItems[0].SubItems[8].Text,
                                    nvlistView.SelectedItems[0].Text,
                                    nvlistView.SelectedItems[0].SubItems[10].Text,
                                    true);
                        TotalPedido();
                    }
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        private void Pedido_bkp(System.Data.OleDb.OleDbConnection Conexion,
                                 int IdCliente,
                                 string Codigo,
                                 string Descrip,
                                 float Precio,
                                 int Cantidad,
                                 float SubTotal,
                                 short Similar,
                                 short Deposito,
                                 short Oferta,
                                 string IDCatalogo,
                                 string CodigoCorto,
                                 string Observaciones,
                                 bool Existe)            
        {
            try
            {

                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

                if (!(Conexion.State == ConnectionState.Open)) 
                { 
                    Conexion.Open(); 
                }

                if (!Existe)
                {
                    cmd.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = IdCliente;
                    cmd.Parameters.Add("pCodigo", System.Data.OleDb.OleDbType.VarChar, 30).Value = Codigo;
                    cmd.Parameters.Add("pDescrip", System.Data.OleDb.OleDbType.VarChar, 64).Value = Descrip;
                    cmd.Parameters.Add("pPrecio", System.Data.OleDb.OleDbType.Single).Value = Precio;
                    cmd.Parameters.Add("pCantidad", System.Data.OleDb.OleDbType.Integer).Value = Cantidad;
                    cmd.Parameters.Add("pSubTotal", System.Data.OleDb.OleDbType.Single).Value = SubTotal;
                    cmd.Parameters.Add("pSimilar", System.Data.OleDb.OleDbType.TinyInt).Value = Similar;
                    cmd.Parameters.Add("pDeposito", System.Data.OleDb.OleDbType.TinyInt).Value = Deposito;
                    cmd.Parameters.Add("pOferta", System.Data.OleDb.OleDbType.TinyInt).Value = Oferta;
                    cmd.Parameters.Add("pIdCatalogo", System.Data.OleDb.OleDbType.VarChar, 38).Value = IDCatalogo;
                    cmd.Parameters.Add("pCodigoCorto", System.Data.OleDb.OleDbType.VarChar, 30).Value = CodigoCorto;
                    cmd.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 80).Value = Observaciones;

                    cmd.Connection = Conexion;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Pedido_Bkp_add";
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    cmd.Parameters.Add("pCantidad", System.Data.OleDb.OleDbType.Integer).Value = Cantidad;
                    cmd.Parameters.Add("pSubTotal", System.Data.OleDb.OleDbType.Single).Value = SubTotal;
                    cmd.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 80).Value = Observaciones;
                    cmd.Parameters.Add("pIdCatalogo", System.Data.OleDb.OleDbType.VarChar, 38).Value = IDCatalogo;

                    cmd.Connection = Conexion;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Pedido_Bkp_upd";
                    cmd.ExecuteNonQuery();
                }

                cmd = null;

            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                switch (ex.ErrorCode)
                {
                    case -2147467259: // registro duplicado
                        throw new util.errorHandling.RegistroDuplicadoException(ex);
                }
            }
            catch (Exception ex)
            {
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        private void btnImprimir_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (nvlistView.Items.Count > 0)
                {
                    InhabilitarPedido();

                    bool wSimilar;
                    bool wOferta;

                    Catalogo._pedidos.Pedido ped = new Catalogo._pedidos.Pedido(Global01.Conexion, Global01.NroUsuario.ToString(), Int16.Parse(cboCliente.SelectedValue.ToString()));
                    ped.NroImpresion = 0;
                    for (int i = 0; i < nvlistView.Items.Count; i++)
                    {
                        wSimilar = (nvlistView.Items[i].SubItems[5].Text.ToString() == "1" ? (bool)(true) : (bool)(false));
                        wOferta = (nvlistView.Items[i].SubItems[7].Text.ToString() == "1" ? (bool)(true) : (bool)(false));

                        ped.ADDItem(nvlistView.Items[i].SubItems[8].Text.ToString(),
                                    float.Parse(nvlistView.Items[i].SubItems[2].Text.ToString()),
                                    Int16.Parse(nvlistView.Items[i].SubItems[3].Text.ToString()),
                                    wSimilar,
                                    byte.Parse(nvlistView.Items[i].SubItems[6].Text.ToString()),
                                    wOferta,
                                    nvlistView.Items[i].SubItems[10].Text.ToString());
                    }

                    ped.Guardar("grabar");

                    Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblPedido_Bkp");

                    Pedido_Imprimir(Global01.NroImprimir);
                    Global01.NroImprimir = "";

                    CerrarPedido();
                    nvlistView.Items.Clear();
                    TotalPedido();
                    nvSimilarChk.Checked = false;
                    nvEsOfertaChk.Checked = false;
                    nvDepositoCbo.SelectedIndex = short.Parse(Funciones.modINIs.ReadINI("Preferencias", "Deposito", "0"));

                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (nvlistView.Items.Count > 0)
                {

                    bool wSimilar;
                    bool wOferta;

                    Catalogo._pedidos.Pedido ped = new Catalogo._pedidos.Pedido(Global01.Conexion, Global01.NroUsuario.ToString(), Int16.Parse(cboCliente.SelectedValue.ToString()));
                    ped.NroImpresion = 0;
                    for (int i = 0; i < nvlistView.Items.Count; i++)
                    {
                        wSimilar = (nvlistView.Items[i].SubItems[5].Text.ToString() == "1" ? (bool)(true) : (bool)(false));
                        wOferta = (nvlistView.Items[i].SubItems[7].Text.ToString() == "1" ? (bool)(true) : (bool)(false));

                        ped.ADDItem(nvlistView.Items[i].SubItems[8].Text.ToString(),
                                    float.Parse(nvlistView.Items[i].SubItems[2].Text.ToString()),
                                    Int16.Parse(nvlistView.Items[i].SubItems[3].Text.ToString()),
                                    wSimilar,
                                    byte.Parse(nvlistView.Items[i].SubItems[6].Text.ToString()),
                                    wOferta,
                                    nvlistView.Items[i].SubItems[10].Text.ToString());
                    }

                    ped.Guardar("VER");

                    Pedido_Imprimir(Global01.NroImprimir);
                    Global01.NroImprimir = "";
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        public static void Pedido_Imprimir(string NroPedido)
        {
            string sReporte = "";

            if ((int)Global01.miSABOR >= 3)
            {
                //sReporte = @"D:\Desarrollos\GitHub\snaPorting\ANSv4\Catalogo4\reportes\Pedido_Enc4.rpt";
               sReporte = Global01.AppPath + "\\Reportes\\Pedido_Enc3.rpt";
            }
            else
            {
                sReporte = Global01.AppPath + "\\Reportes\\Pedido_Enc2.rpt";
            }
            
            ReportDocument oReport = new ReportDocument();

            oReport.Load(sReporte);
            Funciones.util.ChangeReportConnectionInfo(ref oReport);

            oReport.SetParameterValue("pNroPedido", NroPedido);
            
            //oReport.TiTle = "P - " + NroPedido;
 
            varios.fReporte f = new varios.fReporte();
            f.Text = "Nota de Venta n° " + NroPedido;
            f.DocumentoNro = "NV-" + NroPedido;
            f.oRpt = oReport;
            f.ShowDialog();
            f.Dispose();
            f = null;
            oReport.Dispose();
        }

        private void paDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (Global01.AppActiva)
                {
                    DataGridViewCell cell = paDataGridView[e.ColumnIndex, e.RowIndex];
                    if (cell != null)
                    {
                        DataGridViewRow row = cell.OwningRow;
                        if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "NOTA")
                        {
                            Pedido_Imprimir(row.Cells["Nro"].Value.ToString());
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

        private void paDataGridView_KeyDown(object sender, KeyEventArgs e)
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
                            if (paDataGridView.SelectedRows != null)
                            {
                                if (MessageBox.Show("CUIDADO!! a los Items marcados NO podrá enviarlos electrónicamente, ¿Está Seguro?", "Marcando como: ENVIADO EN FORMA MANUAL", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    foreach (DataGridViewRow row in paDataGridView.SelectedRows)
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
                                        paDataGridView.ClearSelection();
                                    }
                                    ObtenerMovimientos();
                                }
                            }
                        }
                    }
                    else if (e.KeyCode == Keys.A && e.Modifiers == Keys.Control)
                    {
                        if (paEnviosCbo.Text.ToString().ToUpper() == "NO ENVIADOS")
                        {
                            if (paDataGridView.SelectedRows != null)
                            {
                                foreach (DataGridViewRow row in paDataGridView.SelectedRows)
                                {
                                    if (row.Cells["Origen"].Value.ToString().Substring(0, 4).ToUpper() == "NOTA")
                                    {
                                        if (MessageBox.Show("¿ Desea abrir el pedido n° " + row.Cells["Nro"].Value.ToString() + " ?", "Abriendo pedido no enviado ...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                        {
                                            if (cboCliente.SelectedValue.ToString() == row.Cells["IDCliente"].Value.ToString())
                                            {
                                                if (btnIniciar.Tag.ToString() == "INICIAR")
                                                {
                                                    AbrirPedido("pedido", row.Cells["Nro"].Value.ToString(), Int16.Parse(row.Cells["IdCliente"].Value.ToString()));
                                                }
                                                else
                                                {
                                                    MessageBox.Show("CUIDADO!! Debe cerrar el pedido ACTUAL", "atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                }
                                            }
                                        }
                                    }
                                    paDataGridView.ClearSelection();
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


        private void AbrirPedido(string EstadoActual, string NroPedido, int IdCliente)
        {
            System.Data.OleDb.OleDbDataReader dr = Funciones.oleDbFunciones.Comando(Global01.Conexion , "EXECUTE v_Pedido_Det_Hist '" + NroPedido + "'");

            if (dr.HasRows )
            {
                //If (vg.miSABOR > 2) And (EstadoActual = "todo") Then
                //  cboCliente.ListIndex = BuscarIndiceEnCombo(cboCliente, CStr(IdCliente), False)
                //  If cboCliente.ListIndex <= 0 Then
                //      MsgBox "El Cliente YA no existe, seleccione primero en la lista desplegable", vbInformation, "Atención"
                //      Exit Sub
                //  Else
                //    cmdVISITA_Click
                //  End If
                //End If
                
                nvlistView.Items.Clear();
                string wC_Producto;
                int wDisCont = 0;

                while (dr.Read())
                {
                    if (DBNull.Value.Equals(dr["C_Producto"]))
                    {
                        wC_Producto = "?";
                    }
                    else
                    {
                        wC_Producto = dr["C_Producto"].ToString().Substring(5);
                    }

                    ListViewItem ItemX = new ListViewItem(wC_Producto); //dr["CodigoCorto"].ToString()
                    ////alternate row color
                    if (nvlistView.Items.Count % 2 != 0)
                    {
                        ItemX.BackColor = System.Drawing.SystemColors.Control; //System.Drawing.Color.FromArgb(255, 255, 192);
                    }

                    ItemX.SubItems.Add(dr["N_Producto"].ToString());          //01
                    ItemX.SubItems.Add(dr["PUnit"].ToString());           //02
                    ItemX.SubItems.Add(dr["Cantidad"].ToString());         //03
                    ItemX.SubItems.Add(dr["SubTotal"].ToString());         //04
                    ItemX.SubItems.Add(dr["miSimilar"].ToString());          //05
                    ItemX.SubItems.Add(dr["Deposito"].ToString());         //06
                    ItemX.SubItems.Add(dr["miOferta"].ToString());           //07
                    ItemX.SubItems.Add(dr["IdCatalogo"].ToString());       //08
                    ItemX.SubItems.Add(dr["C_Producto"].ToString());           //09
                    ItemX.SubItems.Add(dr["Observaciones"].ToString());    //10
       
                    nvDepositoCbo.SelectedValue = Int16.Parse(dr["Deposito"].ToString());

                    if (dr["DisCont"].ToString()=="1" | wC_Producto=="?")
                    {
                        ItemX.SubItems[1].ForeColor = Color.Red;
                        ItemX.SubItems[1].Font = new Font(nvlistView.Font, FontStyle.Bold);
                        wDisCont = wDisCont + 1;
                    }
                    nvlistView.Items.Add(ItemX);
                }
                Funciones.util.AutoSizeLVColumnas(ref nvlistView);
                
                Global01.NroDocumentoAbierto = NroPedido;

                TotalPedido();
                IniciarPedido();
                HabilitarPedido();

                nvSimilarChk.Checked = false;
                nvEsOfertaChk.Checked = false;
                nvDepositoCbo.SelectedIndex = short.Parse(Funciones.modINIs.ReadINI("Preferencias", "Deposito", "0"));
                PedidoTab.SelectedIndex = 0;
                PedidoTab.Visible = true;

                if (wDisCont > 0)
                {
                    MessageBox.Show("CUIDADO!!, Hay Códigos Discontinuados", "atención",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }

            }

            dr = null;
        }

        private void nvlistView_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                nvlistView.SelectedItems[0].SubItems[5].Text = (nvSimilarChk.Checked ? "1" : "0");
                nvlistView.SelectedItems[0].SubItems[6].Text = nvDepositoCbo.SelectedValue.ToString();
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

        public void onRecibir(int dato)
        {
            if (btnIniciar.Tag.ToString() == "INICIAR")            
                cboCliente.SelectedIndex = dato;
        }

        public int IDClienteSeleccionado
        {
            get
            {
                Int16 xClienteSelected = 0;
                if (cboCliente.SelectedValue != null) xClienteSelected = Int16.Parse(cboCliente.SelectedValue.ToString());
                return xClienteSelected;
            }
        }

        private void nvTransporteBuscarBtn_Click(object sender, EventArgs e)
        {

        }

        private void EnviarBtn_Click(object sender, EventArgs e)
        {
            System.Collections.Generic.List<Catalogo.util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO> filtro
            = new List<Catalogo.util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO>();

            _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, int.Parse(cboCliente.SelectedValue.ToString()));
            System.Data.OleDb.OleDbDataReader dr = dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS, "NOTA DE VENTA");

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO mov = new util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO();
                    mov.origen = "NOTA DE VENTA";
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