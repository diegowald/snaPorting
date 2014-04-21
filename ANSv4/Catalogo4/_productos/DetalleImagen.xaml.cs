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

    public partial class DetalleImagen : Window
    {

        public DetalleImagen(ImageSource imageSource)
        {
            InitializeComponent();
            this.ImagenDetalle.Source = imageSource;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
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

    }
}
