using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Catalogo.util
{
    public partial class fDataUpdate : Form
    {
        UserControl ucUpdateCtl = new UserControl();
        
        public string Url { get; set; }

        public bool SoloCatalogo { get; set; }

        public fDataUpdate()
        {
            InitializeComponent();
            
            ucUpdateCtl = new UpdateCtl();

            ucUpdateCtl.AutoScroll = true;
            //ucUpdateCtl.Dock = System.Windows.Forms.DockStyle.Fill;
            ucUpdateCtl.Location = new System.Drawing.Point(0, 0);
            ucUpdateCtl.Size = new System.Drawing.Size(681, 177);
            this.Controls.Add(ucUpdateCtl);
        }

        private void fDataUpdate_Load(object sender, EventArgs e)
        {
            ucUpdateCtl.confi
        }


    }
}

