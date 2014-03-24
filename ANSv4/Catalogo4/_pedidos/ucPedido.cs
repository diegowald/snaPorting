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

    public partial class ucPedido : UserControl, Funciones.emitter_receiver.IReceptor<System.Windows.Forms.DataGridViewRow>
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
                Aux = Aux + float.Parse(nvlistView.Items[i].SubItems[1].Text);
            }

            nvImporteTotalLbl.Text = string.Format("{0:N2}", Aux);
        }

        System.Windows.Forms.DataGridViewRow infoSeleccionada;
        public void onRecibir(DataGridViewRow dato)
        {
            infoSeleccionada = dato;
        }

        private void nvComprarBtn_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(infoSeleccionada.Cells["linea"].Value.ToString() + " - " + infoSeleccionada.Cells["C_Producto"].Value.ToString());
        }
    } //fin clase
} //fin namespace