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

        private SaveDelegate doSave;
        private LoadDelegate doLoad;

        private string password
        {
            get
            {
                return "chiclana917" + ((int)(System.DateTime.Now.Minute / 10)).ToString();
            }
        }


        private void createAndLoadCheckboxes()
        {
            addCheckBox("Confirma Salida", "DATOS", "ConfirmaSalida", "1", "0", "1", true, password);
            addCheckBox("EEA", "DATOS", "EEA", "1", "0", "1");
            addCheckBox("Es gerente", "DATOS", "EsGerente", "1", "0", "0");
            addCheckBox("Pedido NE", "DATOS", "PedidoNE", "1", "0", "1");
            addCheckBox("Devolucion NE", "DATOS", "DevolucionNE", "1", "0", "0");
            addCheckBox("Deposito", "PREFERENCIAS", "Deposito", "1", "0", "1");
            addCheckBox("Solo Catalogo", "DATOS", "SoloCatalogo", "true", "false", "false");
            addCheckBox("Usar Proxy", "DATOS", "proxy", "1", "0", "0");
            addEditBox("IP", "DATOS", "IP");
            addCheckBox("Descargar imagenes actualizadas", "DATOS", "chkImagenUpdate", "1", "0", "1");
            addCheckBox("ICC", "DATOS", "ICC", "1", "0", "0");
            if (doLoad != null)
            {
                doLoad();
            }
        }

        private void addEditBox(string DisplayName, string SectionName, string KeyName, string DefaultValue = null, bool RequiresPasswordToEdit = false, string Password = "")
        {
            IINIProperty ctrl = new TextEditINI(DisplayName, SectionName, KeyName, DefaultValue, RequiresPasswordToEdit, Password);
            addControl(ctrl);
        }

        private void addCheckBox(string DisplayName, string SectionName, string KeyName, string TrueValue, string FalseValue, string DefaultValue = null, bool RequiresPasswordToEdit = false, string Password = "")
        {
            IINIProperty chk = new CheckBoxINI(DisplayName, SectionName, KeyName, TrueValue, FalseValue, DefaultValue, RequiresPasswordToEdit, Password);
            addControl(chk);
        }

        private void addControl(IINIProperty ctrl)
        {
            doSave += ctrl.onSave;
            doLoad += ctrl.onLoad;
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
            if (doLoad != null)
            {
                doLoad();
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

    }
}
