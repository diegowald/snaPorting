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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Catalogo._productos
{
    /// <summary>
    /// Interaction logic for DetalleProducto.xaml
    /// </summary>
    public partial class DetalleProducto : UserControl, Funciones.emitter_receiver.IReceptor<System.Windows.Forms.DataGridViewRow>
    {
        public DetalleProducto()
        {
            InitializeComponent();
        }

        public void onRecibir(System.Windows.Forms.DataGridViewRow dato)
        {
            if (dato != null)
            {
                string s = dato.Cells["C_Producto"].Value.ToString() + " - " +
                    dato.Cells["N_Producto"].Value.ToString();
                txtDetalle.NavigateToString(s);
            }
        }

    }
}
