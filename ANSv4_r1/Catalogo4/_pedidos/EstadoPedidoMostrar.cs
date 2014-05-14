using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace Catalogo._pedidos
{
    public partial class EstadoPedidoMostrar : Form
    {
        public string EstadoMsg;

        public EstadoPedidoMostrar()
        {
            InitializeComponent();
            
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     

    }
}
