using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo._recibos
{
    public partial class fRecibo : Form
    {
      
        UserControl ucRecibo = new UserControl();

        public fRecibo()
        {
            InitializeComponent();
            this.Text = "Recibo para el cliente ...";
        }

        private void fRecibo_Load(object sender, EventArgs e)
        {
            ucRecibo = new ucRecibo();
           
            ucRecibo.AutoScroll = true;
            ucRecibo.Dock = System.Windows.Forms.DockStyle.Fill;
            ucRecibo.Location = new System.Drawing.Point(0, 0);            
            //ucRecibo.Size = new System.Drawing.Size(681, 177);
            this.Controls.Add(ucRecibo);
        }

    }
}
