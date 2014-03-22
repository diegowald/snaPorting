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

    public partial class ucPedido : UserControl
    {

        public ucPedido()
        {
            InitializeComponent();

            Catalogo.Funciones.util.CargaCombo(ref Global01.Conexion , ref cboCliente, "tblClientes", "RazonSocial", "ID", "Activo<>1", "RazonSocial", true, true);

        }


        private void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {

            this.PedidoAnterioresDataGridView.AutoGenerateColumns = true;

            if (cboCliente.SelectedIndex > 0)
            {
                toolStripStatusLabel1.Text = "Nota de Venta para el cliente: " + this.cboCliente.Text.ToString() + " (" + this.cboCliente.ComboBox.SelectedValue + ")";
                this.PedidoAnterioresDataGridView.DataSource = Catalogo.Funciones.oleDbFunciones.xGetDt(ref Global01.Conexion, "v_CtaCte", "IDCliente=" + cboCliente.ComboBox.SelectedValue.ToString(), "Orden, Fecha");
            }
            else
            {

                if (!(this.Parent == null)) { toolStripStatusLabel1.Text = "Nota de Venta para el cliente ..."; }

                this.PedidoAnterioresDataGridView.DataSource = null;
            };

        }


    }

}