using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Catalogo.varios
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

           if (Global01.miSABOR >= Global01.TiposDeCatalogo.Viajante) this.chkActualizarClientes.Visible = true;

           TodoBien = false;
        }

        private void fLogin_Load(object sender, EventArgs e)
        {
            this.lblUsuario.Text = Global01.RazonSocial.ToString() + " - (" + Global01.NroUsuario.ToString() + ")";

            if (Global01.pin.Trim().Length == 0)
            {
                btnNuevo.Visible = true;
            }
            else
            {
                btnIngresar.Enabled = true;
                if (Global01.AppActiva) { chkActualizarClientes.Enabled = true; }
            }
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (txtPIN.Text.Trim().Length == 0)
            {
                errormessage.Text = "ingrese pin.";
                txtPIN.Focus();
            }
            else
            {
                if (Codificar(txtPIN.Text.Trim().ToUpper())==Global01.pin.ToString().ToUpper())
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            
            if (btnNuevo.Text=="Generar")
            {
                if (txtPIN.Text.Trim().Length == 0)
                {
                    errormessage.Text = "ingrese pin.";
                    txtPIN.Focus();
                    return;
                }

                txtPIN2.Text = "";
                txtPIN.Visible = false;
                txtPIN.Enabled = false;
                lblPIN.Text = "Reingresar PIN";
                btnNuevo.Text = "Verificar";
                txtPIN2.Visible = true;
                txtPIN2.Enabled = true;
                txtPIN2.Focus();                
            }
            else 
            {
                if (txtPIN2.Text.Trim().Length == 0)
                {
                    errormessage.Text = "re-ingrese pin.";
                    txtPIN2.Focus();
                    return;
                }

                if (txtPIN.Text.Trim().ToUpper() == txtPIN2.Text.Trim().ToUpper())
                {
                    btnIngresar.Enabled = true;
                    if (Global01.AppActiva) { chkActualizarClientes.Enabled = true; }
                    txtPIN2.Enabled = false;
                    btnNuevo.Enabled = false;
                    Global01.pin = Codificar(txtPIN.Text);
                    Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "UPDATE appConfig SET PIN='" + Codificar(txtPIN.Text.Trim().ToUpper()) + "'");
                    MessageBox.Show("PIN generado con éxito!", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //btnIngresar_Click(null,null); 
                }
                else 
                {
                    txtPIN.Text = "";
                    txtPIN2.Text = "";
                    txtPIN2.Visible = false;
                    txtPIN2.Enabled = false;
                    lblPIN.Text = "PIN";
                    btnNuevo.Text = "Generar";
                    txtPIN.Visible = true;
                    txtPIN.Enabled = true;
                    txtPIN.Focus();

                    MessageBox.Show("Ingrese el PIN nuevamente!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void chkActualizarClientes_CheckedChanged(object sender, EventArgs e)
        {
            Global01.ActualizarClientes = chkActualizarClientes.Checked;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string Codificar(string PIN)
        {
            string s = null;

            for (int i = PIN.Trim().Length-1 ; i >= 0; i += -1)
            {
                s = s + ((int)System.Convert.ToChar(PIN.Substring(i, 1))).ToString();
            }
            
            return s;
        }

    }
}
