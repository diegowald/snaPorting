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
    public partial class fPedido : Form
    {
      
        UserControl ucPedido = new UserControl();

        public fPedido()
        {
            InitializeComponent();
            this.Text = "Pedido para el cliente ...";
        }

        private void fPedido_Load(object sender, EventArgs e)
        {
            ucPedido = new ucPedido();
           
            ucPedido.AutoScroll = true;
            ucPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            ucPedido.Location = new System.Drawing.Point(0, 0);            
            //ucPedido.Size = new System.Drawing.Size(681, 177);
            this.Controls.Add(ucPedido);
        }

    }
}
