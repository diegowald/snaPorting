using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Catalogo
{

    public partial class fLogin : Form
    {
        public bool TodoBien { get; set; }

        public fLogin()
        {
            InitializeComponent();

           this.pictureBox1.BackColor = Color.Transparent;
           this.errormessage.BackColor = Color.Transparent;
           this.errormessage.ForeColor = Color.Red;

            TodoBien = false;
        }
  

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtPIN.Text.Trim().Length == 0)
            {
                errormessage.Text = "ingrese el pin.";
                txtPIN.Focus();
            }
            else
            {
                if (txtPIN.Text.Trim().ToUpper()==Global01.pin.ToString().ToUpper())
                {
                    TodoBien = true;
                    Close();
                }
                else
                {
                    errormessage.Text = "El PIN de ingreso no es válido.";
                }
            }
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            this.lblUsuario.Text = Global01.RazonSocial.ToString() + " - (" + Global01.NroUsuario.ToString() + ")";
        }

    }

}
