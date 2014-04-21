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

namespace Catalogo._productos
{
    /// <summary>
    /// Interaction logic for DeetalleImagen.xaml
    /// </summary>
    public partial class DeetalleImagen : Window
    {
        public DeetalleImagen(string file)
        {
            InitializeComponent();
            this.ImagenDetalle.Source = new BitmapImage(new Uri(file, UriKind.Absolute));
        }
    }
}
