using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb; 
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo._pedidos
{
    public partial class ucPedido : UserControl, 
        Funciones.emitter_receiver.IReceptor<System.Windows.Forms.DataGridViewRow>, // Para recibir el producto seleccionado
        Funciones.emitter_receiver.IReceptor<_pedidos.PedidosHelper.Acciones> // Para recibir acciones al pedido desde la grilla de productos.

    {
        private const string m_sMODULENAME_ = "ucPedido";
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
            };

            if (Funciones.modINIs.ReadINI("DATOS", "EsGerente", "0") == "1")
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1", "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Trim(cstr(ID)) & ')' as Cliente, ID");
            }
            else
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1 and IdViajante=" + Global01.NroUsuario.ToString(), "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Format([ID],'00000') & ')' AS Cliente, ID");
            }

            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref nvTransporteCbo, "ansTransportes", "Nombre", "ID", "Activo=1", "Nombre", true, false, "NONE");
            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref nvDepositoCbo, "v_Deposito", "D_Dep", "IdDep", "ALL", "D_Dep", true, false, "NONE");

        }

        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex > 0)
            {
                toolStripStatusLabel1.Text = "Nota de Venta para el cliente: " + this.cboCliente.Text.ToString();
                btnIniciar.Enabled = true;
            }
            else
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Pedido para el cliente ..."; }
                btnIniciar.Enabled = false;
            };
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

            if ("HayDevolucionActiva" == "CANCELAR" )
            {
                MessageBox.Show("Debe cerrar la DEVOLUCION para comenzar la VENTA", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (cboCliente.SelectedIndex > 0)
                {
                    if (btnIniciar.Tag.ToString() == "INICIAR")
                    {
                        //vg.auditor.Guardar Pedido, INICIA
                        nvlistView.Items.Clear();

                        OleDbDataReader dr = null;
     
                        if (Funciones.modINIs.ReadINI("DATOS", "PedidoNE", "1") == "1")
                        {
                            _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, int.Parse(cboCliente.SelectedValue.ToString()));
                            dr = movimientos.Leer(_movimientos.Movimientos.DATOS_MOSTRAR.NO_ENVIADOS);
                            if (dr.HasRows)
                            {
                                MessageBox.Show("Hay un pedido pendiente de envio, sugerimos: \n Ir a pedidos no enviados abrirlo y continuar con el mismo", "atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            } ;                   
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
                                        ItemX.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
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
                                };
                                Funciones.util.AutoSizeLVColumnas(ref nvlistView);
                            }
                            else
                            {
                                Funciones.oleDbFunciones.ComandoIU(Global01.Conexion,"DELETE FROM tblPedido_Bkp");
                            };

                        };

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
                            // vg.auditor.Guardar Pedido, CANCELA
                            PedidoTab.Visible = false;
                            nvlistView.Items.Clear();
                            TotalPedido();                            
                            CerrarPedido();
                            InhabilitarPedido();

                            cboCliente.SelectedIndex = 0;
                            nvSimilarChk.Checked = false;
                            nvEsOfertaChk.Checked = false;
                            nvDepositoCbo.SelectedIndex = short.Parse(Funciones.modINIs.ReadINI("Preferencias", "Deposito", "0"));                            
                        };
                    };

                    nvlistView.Tag = "-1";
                }
                else
                {
                    MessageBox.Show("Seleccione un Cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                };

            };

        }

        private void ucPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsControl(e.KeyChar) && e.KeyChar == ((char)Keys.D))
            {   //CTRL + D
                if (btnIniciar.Tag.ToString() == "CANCELAR")
                {
                    //VerDetallePedido();                  
                };
            };
        }

        private void CerrarPedido()
        {
            btnIniciar.Text = "Iniciar";
            btnIniciar.Tag = "INICIAR";

            _ToolTip.SetToolTip(btnIniciar, "INICIAR Pedido Nuevo");
            nvObservacionesTxt.Text = "";
            //cboEnvios_Click();
        }

        private void IniciarPedido()
        {
            btnIniciar.Text = "CANCELAR";
            btnIniciar.Tag = "CANCELAR";
            _ToolTip.SetToolTip(btnIniciar, "CANCELAR éste Pedido");
            nvObservacionesTxt.Text = "";
        }

        private void HabilitarPedido()
        {
            PnlTop.Enabled = true;
            PnlMain.Enabled = true;
            btnImprimir.Enabled = true;
            btnVer.Enabled = true;
            nvlistView.Enabled = true;
            cboCliente.Enabled = false;
        }

        private void InhabilitarPedido()
        {
            PnlTop.Enabled = false;
            PnlMain.Enabled = false;
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

        public void onRecibir(PedidosHelper.Acciones dato)
        {
            switch (dato)
            {
                case PedidosHelper.Acciones.COMPRAR:
                    cmdProductoAgregar();
                    break;
                case PedidosHelper.Acciones.INCREMENTAR:
                    nvCantidadTxt.Value++;
                    break;
                case PedidosHelper.Acciones.DECREMENTAR:
                    nvCantidadTxt.Value--;
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
                        };
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
                            ItemX.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
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

                    };

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

                    nvlistView.SelectedItems[0].Selected = false;

                    nvEsOfertaChk.Checked = false;
                    nvSimilarChk.Checked = false;
                    nvCantidadTxt.Value = 1;

                    TotalPedido();                    
                }
            }
        }
        
        private void nvlistView_KeyDown(object sender, KeyEventArgs e)
        {
    
            if (nvlistView.SelectedItems != null)
            {
                bool wEntro = false;
               
                if (e.KeyCode==Keys.O && e.Modifiers==Keys.Control)
                {    //Observaciones del Item

                    string wItemObservaciones = nvlistView.SelectedItems[0].SubItems[10].Text;
                    if (Funciones.util.InputBox(" (Presione Cancelar para quitar la Observación)  ", "Observaciones para el Item", 80, ref wItemObservaciones) == DialogResult.OK)
                    {
                        nvlistView.SelectedItems[0].SubItems[10].Text = wItemObservaciones;
                    }
                    else
                    { // Apreto Cancelar
                        nvlistView.SelectedItems[0].SubItems[10].Text = "";
                    };
                    wEntro = true;
                }
                else if (e.KeyCode == Keys.Delete)
                {  //DEL
                    Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblPedido_Bkp WHERE IdCatalogo='" + nvlistView.SelectedItems[0].SubItems[8].Text.ToString() + "'");
                    nvlistView.Items.Remove(nvlistView.SelectedItems[0]);
                    TotalPedido();
                }
                else if (e.KeyValue.ToString()  == "187")
                {
                    if (Decimal.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) < 999)
                    {
                        nvlistView.SelectedItems[0].SubItems[3].Text = (Decimal.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) + 1).ToString();
                        nvlistView.SelectedItems[0].SubItems[4].Text = (float.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) * float.Parse(nvlistView.SelectedItems[0].SubItems[2].Text.ToString())).ToString();
                        wEntro = true;
                    };
                }
                else if (e.KeyValue.ToString() == "189")
                {
                    if (Decimal.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) > 1)
                    {
                        nvlistView.SelectedItems[0].SubItems[3].Text = (Decimal.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) - 1).ToString();
                        nvlistView.SelectedItems[0].SubItems[4].Text = (float.Parse(nvlistView.SelectedItems[0].SubItems[3].Text.ToString()) * float.Parse(nvlistView.SelectedItems[0].SubItems[2].Text.ToString())).ToString();
                        wEntro = true;
                    }
                };

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
    
            };
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
                
                if (!(Conexion.State == ConnectionState.Open)) { Conexion.Open(); };

                if (!Existe)
                {
                    cmd.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = IdCliente;
                    cmd.Parameters.Add("pCodigo", System.Data.OleDb.OleDbType.VarChar, 30).Value = Codigo;
                    cmd.Parameters.Add("pDescrip", System.Data.OleDb.OleDbType.VarChar, 64).Value = Descrip;
                    cmd.Parameters.Add("pPrecio", System.Data.OleDb.OleDbType.Single).Value = Precio;
                    cmd.Parameters.Add("pCantidad", System.Data.OleDb.OleDbType.Integer).Value = Cantidad;
                    cmd.Parameters.Add("pSubTotal", System.Data.OleDb.OleDbType.Single).Value =SubTotal;
                    cmd.Parameters.Add("pSimilar", System.Data.OleDb.OleDbType.TinyInt).Value = Similar;
                    cmd.Parameters.Add("pDeposito", System.Data.OleDb.OleDbType.TinyInt).Value = Deposito;
                    cmd.Parameters.Add("pOferta", System.Data.OleDb.OleDbType.TinyInt).Value =  Oferta;
                    cmd.Parameters.Add("pIdCatalogo", System.Data.OleDb.OleDbType.VarChar, 38).Value =  IDCatalogo;
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
                    cmd.Parameters.Add("pIdCatalogo", System.Data.OleDb.OleDbType.VarChar, 38).Value =  IDCatalogo;

                    cmd.Connection = Conexion;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "usp_Pedido_Bkp_upd";
                    cmd.ExecuteNonQuery();
                };
                
                cmd = null;

            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                switch (ex.ErrorCode)
                { 
                    case -2147467259:
                        break;
                }
                //ErrorHandler:

                //        If Err.Number = -2147467259 Then
                //            ' El registro está duplicado... debo borrar el registro e intentar nuevamente
                //            ' El error dice así:
                //            ' Los cambios solicitados en la tabla no se realizaron correctamente
                //            '  porque crearían valores duplicados en el índice, clave principal o relación.
                //            ' Cambie los datos en el campo o los campos que contienen datos duplicados,
                //            ' quite el índice o vuelva a definir el índice para permitir entradas duplicadas e inténtelo de nuevo.

            }
        }

        private void btnVer_Click(object sender, EventArgs e)
        {

            const string PROCNAME_ = "btnVer_Click";

            Cursor.Current = Cursors.WaitCursor;

            if (nvlistView.Items.Count > 0)
            {

                //              if ((int)Global01.miSABOR==2) 
                //              {// Catalogo para cliente
                //                _IdCliente = Global01.NroUsuario;
                //              }

                //              _pedidos.
                //  elpedido.Nuevo m.IdCliente
                //  elpedido.NroImpresion = 0

                //  For m.I = 1 To lvPedido.ListItems.Count
                //    elpedido.ADDItem CStr(lvPedido.ListItems(m.I).ListSubItems(8)), _
                //                     CInt(lvPedido.ListItems(m.I).ListSubItems(3)), _
                //                     CBool(lvPedido.ListItems(m.I).ListSubItems(5)), _
                //                     CBool(chkEsOfertaBahia.value), _
                //                     CBool(lvPedido.ListItems(m.I).ListSubItems(7)), _
                //                     CByte(lvPedido.ListItems(m.I).ListSubItems(6)), _
                //                     CSng(lvPedido.ListItems(m.I).ListSubItems(2)), _
                //                     CStr(lvPedido.ListItems(m.I).ListSubItems(10))

                //  Next m.I

                //  elpedido.Guardar "VER"
                //  Pedido_Imprimir vg.NroImprimir
                //  vg.NroImprimir = ""

                //End If

                Cursor.Current = Cursors.Default;

            }
        }

    } //fin clase
} //fin namespace