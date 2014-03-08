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
    public partial class DetalleProducto : UserControl, util.emitter_receiver.IReceptor<System.Data.DataRow>
    {
        public DetalleProducto()
        {
            InitializeComponent();
        }

        public void onRecibir(System.Data.DataRow dato)
        {
            txtDetalle.NavigateToString("hola");
        }

    }
}
