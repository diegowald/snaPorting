using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Catalogo
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //Login login = new Login();
            //login.Show();
            //Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        public void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxRazonSocial.Text = "";
            textBoxCuit.Text = "";
            textBoxEmail.Text = "";
            textBoxNroCuenta.Text = "";
            textBoxNroZona.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {

            bool vfirstname = false;
            bool vlastname = false;
            bool vRazonSocial = false;
            bool vCuit = false;
            bool vemail = false;
            bool vNroCuenta = false;
            bool vNroZona = false;
            bool vpassword = false;

            if (textBoxFirstName.Text.Length == 0)
            {
                errormessage.Text = "ingresar un nombre";
                textBoxFirstName.Focus();
            }
            else vfirstname = true;

            if (textBoxLastName.Text.Length == 0)
            {
                errormessage.Text = "ingresar un apellido";
                textBoxLastName.Focus();
            }
            else vlastname = true;

            if (textBoxRazonSocial.Text.Length == 0)
            {
                errormessage.Text = "ingresar razón social";
                textBoxRazonSocial.Focus();
            }
            else vRazonSocial = true;

            if (textBoxCuit.Text.Length == 0)
            {
                errormessage.Text = "ingresar un cuit";
                textBoxCuit.Focus();
            }
            else vCuit = true;

            if (textBoxEmail.Text.Length == 0)
            {                
                errormessage.Text = "ingresar un email";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "ingresar un email válido.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else vemail = true;

            if (textBoxNroCuenta.Text.Length == 0)
            {
                errormessage.Text = "ingresar un n° de cuenta";
                textBoxNroCuenta.Focus();
            }
            else vNroCuenta = true;

            if (textBoxNroZona.Text.Length == 0)
            {
                errormessage.Text = "ingresar un n° de zona";
                textBoxNroZona.Focus();
            }
            else vNroZona = true;

            if (passwordBox1.Password.Length == 0)
            {
                errormessage.Text = "ingrese Llave.";
                passwordBox1.Focus();
            }
            else if (passwordBoxConfirm.Password.Length == 0)
            {
                errormessage.Text = "ingrese la llave nuevamente.";
                passwordBoxConfirm.Focus();
            }
            else if (passwordBox1.Password != passwordBoxConfirm.Password)
            {
                errormessage.Text = "las llaves ingresadas no coinciden.";
                passwordBoxConfirm.Focus();
            }
            else  vpassword = true;
      
            if ((vfirstname) && (vlastname) && (vRazonSocial) && (vCuit) && (vemail) && (vNroCuenta) && (vNroZona) && (vpassword))
            {
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string RazonSocial = textBoxRazonSocial.Text;
                string Cuit = textBoxCuit.Text;
                string email = textBoxEmail.Text;
                string NroCuenta = textBoxNroCuenta.Text;
                string NroZona = textBoxNroZona.Text;
                string password = passwordBox1.Password;

                errormessage.Text = "";
  
                //SqlConnection con = new SqlConnection("Data Source=.\\sqlexpress;Initial Catalog=TestDataBase;User ID=sa;Password=sqlserver223");
                //con.Open();
                //SqlCommand cmd = new SqlCommand("Insert into Registration (FirstName,LastName,Email,Password,Address) values('" + firstname + "','" + lastname + "','" + email + "','" + password + "','" + address + "')", con);
                //cmd.CommandType = CommandType.Text;
                //cmd.ExecuteNonQuery();
                //con.Close();
                //errormessage.Text = "You have Registered successfully.";
                    
                string xParam = passwordBox1.Password.ToString() + Global01.IDMaquinaCRC;
                Global01.NroUsuario = textBoxNroCuenta.Text.PadLeft(5,'0');
                Global01.LLaveViajante = textBoxNroZona.Text.PadLeft(5,'0') + textBoxNroCuenta.Text.PadLeft(5,'0') + textBoxCuit.Text.ToString().Replace("-", "") + Catalogo._registro.AppRegistro.ObtenerCRC(xParam);
                //zzzzziiiiiccccccccccc
                //123456789012345678901
                Funciones.modINIs.WriteINI("DATOS", "LLaveViajante", Global01.LLaveViajante);
                Global01.appCaduca = DateTime.Today.Date.AddDays(30);
                Global01.dbCaduca = DateTime.Today.Date.AddDays(21);
                Global01.RecienRegistrado = true;

                Reset();
                Close();
       
            }

            
            //adoModulo.adoConectar vg.Conexion, qstring
            
            //appConfig_Upd vg.Conexion, vg.Cuit, vg.RazonSocial, vg.ApellidoNombre, _
            //              vg.Domicilio, vg.Telefono, vg.Ciudad, vg.Email, CStr(vg.NroUsuario), CInt(vg.ListaPrecio)
            
            //If vg.miSABOR = A2_Clientes Then
            //    Cliente_Add vg.Conexion, vg.NroUsuario, vg.Cuit, vg.RazonSocial, vg.ApellidoNombre, _
            //             vg.Email, vg.Domicilio, vg.Telefono, vg.Ciudad
            //End If
            
            //adoModulo.adoDesconectar vg.Conexion
            
            //MsgBox ("¡BIENVENIDO A NUESTRO CATALOGO!."), vbInformation, "REGISTRADO"

        }


        private void textBoxCuit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            Regex regex2 = new Regex("-\\d{3}$");
            e.Handled = regex2.IsMatch(e.Text);
        }

        private void textBoxNroCuenta_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex2 = new Regex("[^0-9]+");
            e.Handled = regex2.IsMatch(e.Text);
        }

        private void textBoxNroZona_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex2 = new Regex("[^0-9]+");
            e.Handled = regex2.IsMatch(e.Text);
        }


        private bool validateCuit(string Cuit)
        {
            Regex rg = new Regex("[A-Z_a-z]");
            Cuit = Cuit.Replace("-", "");
            if (rg.IsMatch(Cuit))
                return false;
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


   }
}
