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
    /// Interaction logic for passwordFrm.xaml
    /// </summary>
    public partial class passwordFrm : Window
    {
        private string _password;
        public passwordFrm(string password)
        {
            InitializeComponent();
            _password = password;
        }

        private void OKButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (txtPwd.Password == _password)
            {
                DialogResult = true;
                Close();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Clave incorrecta", "Error", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
            }
        }

        private void CancelButton_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
