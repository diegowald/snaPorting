using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Catalogo._registro
{    
    public partial class fRegistro : Form
    {

        public fRegistro()
        {
            InitializeComponent();

           this.pictureBox1.BackColor = Color.Transparent;
           this.errormessage.BackColor = Color.Transparent;
           this.errormessage.ForeColor = Color.Red;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            if (rNombreTxt.Text.Trim().Length == 0)
            {
                errormessage.Text = "Ingrese Nombre";
                rNombreTxt.Focus();
                return;
            }

            if (rApellidoTxt.Text.Trim().Length == 0)
            {
                errormessage.Text = "Ingrese Apellido";
                rApellidoTxt.Focus();
                return;
            }

            if (rRazonSocialTxt.Text.Trim().Length == 0)
            {
                errormessage.Text = "Ingrese Razón Social";
                rRazonSocialTxt.Focus();
                return;
            }

            if (rCuitTxt.Text.Trim().Length == 0)
            {
                errormessage.Text = "Ingrese Cuit";
                rCuitTxt.Focus();
                return;
            }
            else if (!Regex.IsMatch(rCuitTxt.Text, @"^[0-9][\w\.-]*[0-9]\-[0-9]$")) 
            //else if (!validateCuit(rCuitTxt.Text))
            {
                errormessage.Text = "Ingresar un Cuit válido.";
                rCuitTxt.Select(0, rCuitTxt.Text.Length);
                rCuitTxt.Focus();
                return;
            }

            if (rEmailTxt.Text.Trim().Length == 0)
            {
                errormessage.Text = "Ingrese Email";
                rEmailTxt.Focus();
                return;
            }
            else if (!Regex.IsMatch(rEmailTxt.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Ingresar un email válido.";
                rEmailTxt.Select(0, rEmailTxt.Text.Length);
                rEmailTxt.Focus();
                return;
            }

            if (rNroCuentaTxt.Text.Trim().Length == 0)
            {
                errormessage.Text = "Ingrese n° Cuenta";
                rNroCuentaTxt.Focus();
                return;
            }

            if (rZonaTxt.Text.Trim().Length == 0)
            {
                errormessage.Text = "Ingrese Zona";
                rZonaTxt.Focus();
                return;
            }

            if (Global01.miSABOR >  Global01.TiposDeCatalogo.Cliente)
            {
                if (Int16.Parse(rZonaTxt.Text) != Int16.Parse(rNroCuentaTxt.Text))
                {
                errormessage.Text = "Catálogo de Viajante, debe coincidir nº cuenta cliente con zona";
                rNroCuentaTxt.Focus();
                return;
                }
            }

            if (!rBasRb.Checked & !rOtraRb.Checked)
            {
                errormessage.Text = "Seleccione Buenos Aires y/o Otra";
                return;
            }

            if (rLlave1Txt.Text.Trim().Length == 0)
            {
                errormessage.Text = "Ingrese Llave";
                rLlave1Txt.Focus();
                return;
            }

            if (rLlave2Txt.Text.Trim().Length == 0)
            {
                errormessage.Text = "Re-Ingrese Llave";
                rLlave2Txt.Focus();
                return;
            }

            if (rLlave1Txt.Text.Trim().ToUpper() != rLlave2Txt.Text.Trim().ToUpper())
            {
                errormessage.Text = "Error en Llave de Habilitación (no coinciden)";
                rLlave2Txt.Focus();
                return;
            }

            if (rBasRb.Checked)
            {
                Global01.ListaPrecio = 2;
            }
            else
            {
                Global01.ListaPrecio = 1;
            }
            
            Global01.Cuit = rCuitTxt.Text;
            Global01.RazonSocial = rRazonSocialTxt.Text;
            Global01.ApellidoNombre = rApellidoTxt.Text.Trim() + " " + rNombreTxt.Text.Trim();
            //Global01.Domicilio = "Chiclana 915";
            //Global01.Telefono = "(0291) 456-1111";
            //Global01.Ciudad = "Bahía Blanca";
            Global01.Zona = rZonaTxt.Text;
            Global01.EmailTO = rEmailTxt.Text;
            Global01.NroUsuario = rNroCuentaTxt.Text.PadLeft(5, '0');
            string xParam = rLlave1Txt.Text.ToString() + Global01.IDMaquinaCRC;
            Global01.LLaveViajante = rZonaTxt.Text.PadLeft(5, '0') + rNroCuentaTxt.Text.PadLeft(5, '0') + rCuitTxt.Text.ToString().Replace("-", "") + Catalogo._registro.AppRegistro.ObtenerCRC(xParam);          
            //zzzzziiiiiccccccccccc
            //123456789012345678901     

            Global01.Conexion.Open();

            appConfig_upd(Global01.Conexion, Global01.Cuit.Replace("-", ""), Global01.RazonSocial, Global01.ApellidoNombre, "Chiclana 915", "(0291) 456-1111", "Bahía Blanca", Global01.EmailTO, Global01.NroUsuario, Global01.ListaPrecio, Int16.Parse(Global01.Zona));

            if (Global01.miSABOR ==  Global01.TiposDeCatalogo.Cliente)
            {
                Cliente_add(Global01.Conexion, Int16.Parse(Global01.NroUsuario), Global01.ApellidoNombre, Global01.RazonSocial, 
                    Global01.Cuit.Replace("-",""), Global01.EmailTO, Int16.Parse(Global01.Zona),
                    "Chiclana 915", "Bahía Blanca", "(0291) 456-1111", 
                    "de alta en registro de catálogo", 0, DateTime.Now, "Cxx");
            }
            Global01.Conexion.Close();

            Funciones.modINIs.WriteINI("DATOS", "LLaveViajante", Global01.LLaveViajante);
            Global01.appCaduca = DateTime.Today.Date.AddDays(60);
            Global01.dbCaduca = DateTime.Today.Date.AddDays(21);
            Global01.RecienRegistrado = true;

            Close();

        }

        private void Cliente_add(System.Data.OleDb.OleDbConnection Conexion, int ID, string ApellidoNombre, string RazonSocial, 
                                string Cuit, string Email, int IDViajante,
                                string Domicilio, string Ciudad, string Telefono, 
                                string Observaciones, byte Activo, DateTime F_Actualizacion, string Cascara)
        {
            try
            {
               // Funciones.oleDbFunciones.ComandoIU(Conexion, "DELETE FROM tblClientes WHERE ID = " + ID.ToString());
                
                Funciones.oleDbFunciones.ComandoIU(Conexion, "DELETE FROM tblClientes");

                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

                cmd.Parameters.Add("pID", System.Data.OleDb.OleDbType.Integer).Value = ID;
                cmd.Parameters.Add("pRazonSocial", System.Data.OleDb.OleDbType.VarChar, 40).Value = RazonSocial;
                cmd.Parameters.Add("pCuit", System.Data.OleDb.OleDbType.VarChar, 13).Value = Cuit;
                cmd.Parameters.Add("pEmail", System.Data.OleDb.OleDbType.VarChar, 40).Value = Email;
                cmd.Parameters.Add("pIDViajante", System.Data.OleDb.OleDbType.Integer).Value = IDViajante;
                cmd.Parameters.Add("pDomicilio", System.Data.OleDb.OleDbType.VarChar, 40).Value = Domicilio;
                cmd.Parameters.Add("pCiudad", System.Data.OleDb.OleDbType.VarChar, 40).Value = Ciudad;
                cmd.Parameters.Add("pTelefono", System.Data.OleDb.OleDbType.VarChar, 40).Value = Telefono;
                cmd.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 200).Value = Observaciones;
                cmd.Parameters.Add("pActivo", System.Data.OleDb.OleDbType.TinyInt).Value = Activo;
                cmd.Parameters.Add("pCascara", System.Data.OleDb.OleDbType.VarChar, 3).Value = Cascara;
                cmd.Parameters.Add("pF_Actualizacion", System.Data.OleDb.OleDbType.Date).Value = F_Actualizacion;

                cmd.Connection = Conexion;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_Clientes_add";
                
                //if (Global01.TranActiva != null)
                //{
                //    cmd.Transaction = Global01.TranActiva;
                //}
                cmd.ExecuteNonQuery();

                cmd = null;
            }

            catch (System.Data.OleDb.OleDbException ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;              
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;
            }
        }
    
        private void appConfig_upd(System.Data.OleDb.OleDbConnection Conexion, string Cuit, string RazonSocial, string ApellidoNombre, string Domicilio, string Telefono, string Ciudad, string Email, string NroUsuario, byte ListaPrecio, int Zona)
        {
            try
            {
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

                cmd.Parameters.Add("pCuit", System.Data.OleDb.OleDbType.VarChar, 13).Value = Cuit;
                cmd.Parameters.Add("pRazonSocial", System.Data.OleDb.OleDbType.VarChar, 64).Value = RazonSocial;
                cmd.Parameters.Add("pApellidoNombre", System.Data.OleDb.OleDbType.VarChar, 64).Value = ApellidoNombre;
                cmd.Parameters.Add("pDomicilio", System.Data.OleDb.OleDbType.VarChar, 50).Value = Domicilio;
                cmd.Parameters.Add("pTelefono", System.Data.OleDb.OleDbType.VarChar, 50).Value = Telefono;
                cmd.Parameters.Add("pCiudad", System.Data.OleDb.OleDbType.VarChar, 50).Value = Ciudad;
                cmd.Parameters.Add("pEmail", System.Data.OleDb.OleDbType.VarChar, 64).Value = Email;
                cmd.Parameters.Add("pIDans", System.Data.OleDb.OleDbType.VarChar, 6).Value = NroUsuario;
                cmd.Parameters.Add("pListaPrecio", System.Data.OleDb.OleDbType.TinyInt).Value = ListaPrecio;
                cmd.Parameters.Add("pZona", System.Data.OleDb.OleDbType.VarChar, 3).Value = Zona.ToString().PadLeft(3,'0');
                
                cmd.Connection = Conexion;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_appConfig_Upd";
                //if (Global01.TranActiva != null)
                //{
                //    cmd.Transaction = Global01.TranActiva;
                //}
                cmd.ExecuteNonQuery();

                cmd = null;
            }

            catch (System.Data.OleDb.OleDbException ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }
      
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rCuitTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool bResultado = false;
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != '-' && e.KeyChar != '\b')
            {
                bResultado = true;
            }
            e.Handled =  bResultado;
        }

        private void rNroCuentaTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Funciones.util.SoloDigitos(e);
        }

        private void rZonaTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = Funciones.util.SoloDigitos(e);
        }

        private void rNombreTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void rApellidoTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void rRazonSocialTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void rEmailTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToLower(e.KeyChar);
        }

        private bool validateCuit(string Cuit)
        {                
            if (!Regex.IsMatch(Cuit, @"^[0-9][\w\.-]*[0-9]\-[0-9]$"))
                return false;

            Cuit = Cuit.Replace("-", "");
            if (Cuit.Length != 11)
                return false;

            char[] cuitArray = Cuit.ToCharArray();
            double sum = 0;
            int bint = 0;
            int j = 7;
            for (int i = 5, c = 0; c != 10; i--, c++)
            {
                if (i >= 2)
                    sum += (Char.GetNumericValue(cuitArray[c]) * i);
                else
                    bint = 1;
                if (bint == 1 && j >= 2)
                {
                    sum += (Char.GetNumericValue(cuitArray[c]) * j);
                    j--;
                }
            }
            if ((cuitArray.Length - (sum % 11)) == Char.GetNumericValue(cuitArray[cuitArray.Length - 1]))
                return true;
            return false;
        }

        private void fRegistro_Load(object sender, EventArgs e)
        {
            rNombreTxt.Focus();
        }

    }
}
