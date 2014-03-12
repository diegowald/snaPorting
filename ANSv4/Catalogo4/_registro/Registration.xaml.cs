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
            else
            {
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string RazonSocial = textBoxRazonSocial.Text;
                string Cuit = textBoxCuit.Text;
                string email = textBoxEmail.Text;
                string NroCuenta = textBoxNroCuenta.Text;
                string NroZona = textBoxNroZona.Text;
                string password = passwordBox1.Password;

                if (passwordBox1.Password.Length == 0)
                {
                    errormessage.Text = "Ingrese Llave.";
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
                else
                {
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
                    Global01.LLaveViajante = textBoxNroZona.Text.PadLeft(5,'0') + textBoxNroCuenta.Text.PadLeft(5,'0') + textBoxCuit.Text.ToString().Replace("-", "") + Catalogo._registro.AppRegistro.ObtenerCRC(ref xParam);
                    //zzzzziiiiiccccccccccc
                    //123456789012345678901
                    Funciones.modINIs.WriteINI("DATOS", "LLaveViajante", Global01.LLaveViajante);
                    Global01.appCaduca = DateTime.Today.Date.AddDays(30);
                    Global01.dbCaduca = DateTime.Today.Date.AddDays(21);
                    Global01.RecienRegistrado = true;

                    Reset();
                    Close();
                }
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
    }
}
