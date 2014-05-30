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

namespace Catalogo._preferencias
{
    /// <summary>
    /// Interaction logic for PreferenciasFrm.xaml
    /// </summary>
    public partial class PreferenciasFrm : Window
    {
        public PreferenciasFrm()
        {
            InitializeComponent();
            createAndLoadCheckboxes();
        }

        public delegate void SaveDelegate();
        public delegate void LoadDelegate();
        public delegate void ResetDelegate();
        public delegate void PasswordValidDelegate();

        private SaveDelegate doSave;
        private LoadDelegate doLoad;
        private ResetDelegate doReset;
        private PasswordValidDelegate doEnablePasswordProtectedControls;

        private string password
        {
            get
            {
                int MinutoDecena = (int)(System.DateTime.Now.Minute / 10);
                string xPassword = "chiclana917";

                switch (MinutoDecena)
                {
                    case 0:
                        xPassword = "Chiclana917";
                        break;
                    case 1:
                        xPassword = "cHiclana917";
                        break;
                    case 2:
                        xPassword = "chIclana917";
                        break;
                    case 3:
                        xPassword = "chiClana917";
                        break;
                    case 4:
                        xPassword = "chicLana917";
                        break;
                    case 5:
                        xPassword = "chiclAna917";
                        break;
                };

                return xPassword + MinutoDecena.ToString();

            }
        }
        bool passwordOK = false;

        private void createAndLoadCheckboxes()
        {
            // -- DEFAULT de Setting ------
            //setDef_INFO = "0";                  //control catalogo online
            //setDef_DEP  = "0";                  //deposito
            //setDef_DEV_abierta_ne = "1";        //Devolucion Abierta no enviada
            //setDef_PED_abierto_ne = "1";        //Pedido Abierto no enviado
            //setDef_checkNovedades = "10";       //tiempo de chequeo de novedades
            //setDef_checkConectadoMinutos = "1"; //tiempo de chequeo de conectado
            //setDef_checkImagenUpdate = "0";     //Imagen Existe PERO busca nueva
            //setDef_checkImagenNueva = "1";      //Imagen NO Existe
            //setDef_ICC = "0";                   //Imprime Cta. Cte.
            //setDef_CCC = "0";                   //Combo Cliente x Nro. de Cuenta
            //setDef_DelayedEnviar = "2";         //tiempo de demora para envios
            //setDef_SiempreEnviar = "0";         //enviar siempre
            //setDef_IP =  "0.0.0.0";             //
            //setDef_IP2 = "0.0.0.0";             //
            //setDef_ProxyServer = "0.0.0.0";     //
            //setDef_IPPing = "8.8.8.8";          //
            //setDef_ConfirmaSalida = "1";
            //setDef_EsGerente = "0";

            addCheckBox("Confirma Salida", "DATOS", "ConfirmaSalida", "1", "0", Global01.setDef_ConfirmaSalida, false, password);
            addCheckBox("Usar Pedido NO Enviado", "DATOS", "PED_abierto_ne", "1", "0", Global01.setDef_PED_abierto_ne, false, password);
            addCheckBox("Actualizar imágenes que tengo con Nuevas", "DATOS", "chkImagenUpdate", "1", "0", Global01.setDef_checkImagenUpdate, false, password);
            addCheckBox("Descargar imágenes que NO tengo", "DATOS", "chkImagenNueva", "1", "0", Global01.setDef_checkImagenNueva, false, password);
            addCheckBox("Lista de Clientes x Nro. Cuenta", "DATOS", "CCC", "1", "0", Global01.setDef_CCC, false, password);
            addEditBox("Depósito", "DATOS", "Deposito", Global01.setDef_DEP, false, password);
            addEditBox("Transporte", "DATOS", "Transporte", Global01.setDef_Transporte, false, password);
            addEditBox("IP Ping", "DATOS", "IPPing", Global01.setDef_IPPing, false, password);
            addEditBox("Proxy", "DATOS", "ProxyServer", Global01.setDef_ProxyServer, false, password);

            addEditBox("IP 1", "DATOS", "IP", Global01.setDef_IP, true, password);
            addEditBox("IP 2", "DATOS", "IP2", Global01.setDef_IP2, true, password);
            addCheckBox("Envio Electrónico Automático", "DATOS", "SiempreEnviar", "1", "0", Global01.setDef_SiempreEnviar, true, password);
            addCheckBox("Es Gerente", "DATOS", "EsGerente", "1", "0", Global01.setDef_EsGerente, true, password);


            //addCheckBox("Devolucion NE", "DATOS", "DevolucionNE", "1", "0", "0");
            //addCheckBox("ICC", "DATOS", "ICC", "1", "0", "0");

            if (doLoad != null)
            {
                doLoad();
            }
        }

        private void addEditBox(string DisplayName, string SectionName, string KeyName, string DefaultValue = null, bool RequiresPasswordToEdit = false, string Password = "")
        {
            IINIProperty ctrl = new TextEditINI(DisplayName, SectionName, KeyName, DefaultValue, RequiresPasswordToEdit);
            addControl(ctrl);
        }

        private void addCheckBox(string DisplayName, string SectionName, string KeyName, string TrueValue, string FalseValue, string DefaultValue = null, bool RequiresPasswordToEdit = false, string Password = "")
        {
            IINIProperty chk = new CheckBoxINI(DisplayName, SectionName, KeyName, TrueValue, FalseValue, DefaultValue, RequiresPasswordToEdit);
            addControl(chk);
        }

        private void addControl(IINIProperty ctrl)
        {
            doSave += ctrl.onSave;
            doLoad += ctrl.onLoad;
            doReset += ctrl.onReset;

            doEnablePasswordProtectedControls += ctrl.onEnablePasswrodProtectedControl;
            stack.Children.Add(ctrl as UserControl);
        }
        private void OKButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (doSave != null)
            {
                doSave();
            }
            Close();
        }

        private void ResetButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (doReset != null)
            {
                doReset();
            }
        }

        private void CancelButton_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            if (eventArgs.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChangeViewButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Normal;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void PasswordButton_Click_1(object sender, RoutedEventArgs e)
        {
           string wPassword = "";
           if (Funciones.util.InputBox(" Contraseña ", "Ingrese Clave", 15, ref wPassword) == System.Windows.Forms.DialogResult.OK)
           {
               passwordOK = (wPassword == password);
               if (passwordOK && doEnablePasswordProtectedControls != null)
               {
                   doEnablePasswordProtectedControls();
               }
               PasswordButton.Visibility = passwordOK ? System.Windows.Visibility.Hidden : System.Windows.Visibility.Visible;
           }

        }

    }
}
