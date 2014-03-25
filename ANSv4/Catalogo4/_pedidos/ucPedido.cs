using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        public ucPedido()
        {
            InitializeComponent();
            _ToolTip.SetToolTip(btnIniciar, "INICIAR Nota de Venta");
            _ToolTip.SetToolTip(btnImprimir, "Graba e Imprime Nota de Venta ...");
            _ToolTip.SetToolTip(btnVer, "ver ...");
        }


        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCliente.SelectedIndex > 0)
            {
                toolStripStatusLabel1.Text = "Nota de Venta para el cliente: " + this.cboCliente.Text.ToString() + " (" + this.cboCliente.SelectedValue + ")";
                //CargarCtaCte();
                //CargarClienteNovedades();
                //CargarClienteDatos();
                btnIniciar.Enabled = true;
            }
            else
            {
                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Pedido para el cliente ..."; }
                //LimpiarClienteDatos();
                btnIniciar.Enabled = false;
            };
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {

            if (cboCliente.SelectedIndex > 0)
            {
                if (btnIniciar.Tag.ToString() == "INICIAR")
                {
                    //vg.auditor.Guardar Pedido, INICIA
                    //Limpio Listados
                    TotalPedido();
                    AbrirPedido();
                    HabilitarPedido();
                    PedidoTab.SelectedIndex = 0;
                    PedidoTab.Visible = true;
                }
                else
                {
                    if (MessageBox.Show("¿Esta Seguro que quiere CANCELAR el Pedido?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // vg.auditor.Guardar Pedido, CANCELA
                        PedidoTab.Visible = false;
                        cboCliente.SelectedIndex = 0;
                        CerrarPedido();
                        InhabilitarPedido();
                    };
                };

                nvlistView.Tag = "-1";
            }
            else
            {
                MessageBox.Show("Seleccione un Cliente", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            };

        }

        private void ucPedido_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Char.IsControl(e.KeyChar) && e.KeyChar != ((char)Keys.C | (char)Keys.ControlKey))

            if (Char.IsControl(e.KeyChar) && e.KeyChar == ((char)Keys.D))
            {   //CTRL + D
                if (btnIniciar.Tag.ToString() == "CANCELAR")
                {
                    //VerDetallePedido();
                    e.Handled = true;
                };
            };
        }

        private void ralistView_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ucPedido_Load(object sender, EventArgs e)
        {
            if (!Global01.AppActiva)
            {
                this.Dispose();
            };

            if (Funciones.modINIs.ReadINI("DATOS", "EsGerente", "0") == "1")
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1", "RazonSocial", true, true,"Trim(RazonSocial) & '  (' & Trim(cstr(ID)) & ')' as Cliente, ID");
            }
            else
            {
                Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref cboCliente, "tblClientes", "Cliente", "ID", "Activo<>1 and IdViajante=" + Global01.NroUsuario.ToString(), "RazonSocial", true, true, "Trim(RazonSocial) & '  (' & Format([ID],'00000') & ')' AS Cliente, ID");
            }

            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref nvTransporteCbo, "ansTransportes", "Nombre", "ID", "Activo=1", "Nombre", true, false, "NONE");
            Catalogo.Funciones.util.CargaCombo(Global01.Conexion, ref nvDepositoCbo, "v_Deposito", "D_Dep", "IdDep", "ALL", "D_Dep", true, false, "NONE");
        }

        private void CerrarPedido()
        {
            btnIniciar.Text = "Iniciar";
            btnIniciar.Tag = "INICIAR";

            _ToolTip.SetToolTip(btnIniciar, "INICIAR Pedido Nuevo");
            nvObservacionesTxt.Text = "";
            //cboEnvios_Click();
        }

        private void AbrirPedido()
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

        System.Windows.Forms.DataGridViewRow ProductoSeleccionado = null;

        public void onRecibir(DataGridViewRow dato)
        {
            ProductoSeleccionado = dato;
        }

        private void nvComprarBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(ProductoSeleccionado.Cells["linea"].Value.ToString() + " - " + ProductoSeleccionado.Cells["C_Producto"].Value.ToString());
            cmdProductoAgregar();
        }

        public void onRecibir(PedidosHelper.Acciones dato)
        {
            switch (dato)
            {
                case PedidosHelper.Acciones.COMPRAR:
                    System.Diagnostics.Debug.WriteLine("compra");
                    cmdProductoAgregar();
                    break;
                case PedidosHelper.Acciones.INCREMENTAR:
                    System.Diagnostics.Debug.WriteLine("+");
                    nvCantidadTxt.Value++;
                    break;
                case PedidosHelper.Acciones.DECREMENTAR:
                    System.Diagnostics.Debug.WriteLine("-");
                    nvCantidadTxt.Value--;
                    break;
                default:
                    break;
            }
        }

        private void cmdProductoAgregar()
        {
      
            bool existe = false;
    
            if (ProductoSeleccionado!=null) 
            {
    
                if (ProductoSeleccionado.Cells["suspendido"].Value.ToString() == "1") {
                    MessageBox.Show("El Artículo está suspendido momentáneamente", "atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
        
                //if (m.EsEnterCantidad < 1) {
                if (nvCantidadTxt.Value  < Decimal.Parse(ProductoSeleccionado.Cells["OfertaCantidad"].Value.ToString())) 
                {
                    MessageBox.Show("Mínimo de oferta: " + ProductoSeleccionado.Cells["OfertaCantidad"].Value.ToString() + " unidades", "atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //m.EsEnterCantidad = m.EsEnterCantidad + 1
                    //return;
                }
                //}
        
                int ii = 0;
                for (int i=0;i < nvlistView.Items.Count; i++)
                {
                    // si los codigo de producto son iguales
                    if (nvlistView.Items[i].SubItems[9].Text.ToString().Trim().ToUpper()==ProductoSeleccionado.Cells["CodigoAns"].Value.ToString().Trim().ToUpper())
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
                       nvlistView.Items[ii].SubItems[3].Text  = (Decimal.Parse(nvlistView.Items[ii].SubItems[3].Text.ToString()) + nvCantidadTxt.Value).ToString() ;
                       nvlistView.Items[ii].SubItems[4].Text = (float.Parse(nvlistView.Items[ii].SubItems[3].Text.ToString()) * float.Parse(ProductoSeleccionado.Cells["PrecioLista"].Value.ToString())).ToString();
                   }
                   nvlistView.Items[ii].SubItems[5].Text = nvSimilarChk.ToString();
                   nvlistView.Items[ii].SubItems[6].Text  = nvDepositoCbo.SelectedValue.ToString();
                }
                else
                {
                    ListViewItem ItemX =  new ListViewItem(ProductoSeleccionado.Cells["C_Producto"].Value.ToString());
                    ////alternate row color
                    if (nvlistView.Items.Count % 2 == 0)
                    {
                    ItemX.BackColor = Color.White;
                    }
                    else
                    {
                        ItemX.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
                    }

                    ItemX.SubItems.Add(ProductoSeleccionado.Cells["N_Producto"].Value.ToString());
                    ItemX.SubItems.Add(ProductoSeleccionado.Cells["PrecioLista"].Value.ToString());
                    ItemX.SubItems.Add(nvCantidadTxt.Value.ToString());
                    float pTotal = float.Parse(nvCantidadTxt.Value.ToString()) * float.Parse(ProductoSeleccionado.Cells["PrecioLista"].Value.ToString());
                    ItemX.SubItems.Add(pTotal.ToString());
                    ItemX.SubItems.Add(nvSimilarChk.ToString());
                    ItemX.SubItems.Add(nvDepositoCbo.SelectedValue.ToString());
                    ItemX.SubItems.Add((ProductoSeleccionado.Cells["Control"].Value.ToString()=="O" ? "1" : "0")); // ¿ es oferta ?
                    ItemX.SubItems.Add(ProductoSeleccionado.Cells["ID"].Value.ToString());
                    ItemX.SubItems.Add(ProductoSeleccionado.Cells["CodigoAns"].Value.ToString());                   
                    ItemX.SubItems.Add(nvObservacionesTxt.Text);
                    
                    nvlistView.Items.Add(ItemX);     
                    Funciones.util.AutoSizeLVColumnas(ref nvlistView);       
                };

                //miModulo.PedidoBkp CLng(m.IdCliente), _
                //                   CStr(m.ItemX.SubItems(9)), _
                //                   CStr(m.ItemX.SubItems(1)), _
                //                   CSng(m.ItemX.SubItems(2)), _
                //                   CInt(m.ItemX.SubItems(3)), _
                //                   CSng(m.ItemX.SubItems(4)), _
                //                   CByte(m.ItemX.SubItems(5)), _
                //                   CByte(m.ItemX.SubItems(6)), _
                //                   CByte(m.ItemX.SubItems(7)), _
                //                   CStr(m.ItemX.SubItems(8)), _
                //                   CStr(m.ItemX.Text), _
                //                   CStr(m.ItemX.SubItems(10)), _
                //                   Existe

                //nvlistView.SelectedItems[0].Selected = false;

                TotalPedido();
        
                nvCantidadTxt.Value = 1;
            }
        }

    } //fin clase
} //fin namespace