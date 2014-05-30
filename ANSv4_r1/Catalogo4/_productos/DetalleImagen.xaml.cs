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

            if (imageSource.Height < 150)
            {
                this.Width = imageSource.Width * 5.5;
                this.Height = imageSource.Height * 5.5;

            }
            else if (imageSource.Height > 400 & imageSource.Height < 600)
            {
                this.Width = imageSource.Width * 1.5;
                this.Height = imageSource.Height * 1.5;

            }
            else if (imageSource.Height > 600 )
            {
                this.Width = 640;
                this.Height = 480;
            }
            else
            {
            this.Width = imageSource.Width * 2.5;
            this.Height = imageSource.Height * 2.5;
            }

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
