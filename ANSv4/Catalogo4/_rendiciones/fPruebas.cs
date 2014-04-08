using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo._pruebas
{
    public partial class fPruebas : Form
    {
      
        UserControl ucPruebas = new UserControl();

        public fPruebas()
        {
            InitializeComponent();
          }

        private void fPruebas_Load(object sender, EventArgs e)
        {
            ucPruebas = new Catalogo._rendiciones.ucRendiciones();
           
            ucPruebas.AutoScroll = true;
            ucPruebas.Dock = System.Windows.Forms.DockStyle.Fill;
            ucPruebas.Location = new System.Drawing.Point(0, 0);            
            //ucPruebas.Size = new System.Drawing.Size(681, 177);
            this.Controls.Add(ucPruebas);
        }

    }
}
