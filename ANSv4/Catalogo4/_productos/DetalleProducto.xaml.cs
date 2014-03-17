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
            txtDetalle.Inlines.Clear();

            if (dato != null)
            {
                txtDetalle.TextWrapping = TextWrapping.Wrap;
                txtDetalle.Margin = new Thickness(5);
                txtDetalle.Inlines.Add(new Run(dato.Cells["linea"].Value.ToString() + " - " + dato.Cells["C_Producto"].Value.ToString()) { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run("    (" + dato.Cells["familia"].Value.ToString() + ")") { FontStyle = FontStyles.Italic });
                txtDetalle.Inlines.Add(new LineBreak());
                txtDetalle.Inlines.Add(new Run("Para: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["marca"].Value.ToString() + " " + dato.Cells["modelo"].Value.ToString()) { Foreground = Brushes.Blue });
                txtDetalle.Inlines.Add(new Run("  Año/Modelo: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["año"].Value.ToString()) { Foreground = Brushes.Blue });

                txtDetalle.Inlines.Add(new Run("  Motor: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["motor"].Value.ToString()) { Foreground = Brushes.Blue });

                txtDetalle.Inlines.Add(new Run("  Descripción: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["n_producto"].Value.ToString()) { Foreground = Brushes.Blue });
                txtDetalle.Inlines.Add(new LineBreak());

                txtDetalle.Inlines.Add(new Run("Medidas: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["o_producto"].Value.ToString()) { Foreground = Brushes.Blue });
                
                txtDetalle.Inlines.Add(new Run("  Reemplaza a: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["reemplazaa"].Value.ToString()) { Foreground = Brushes.Blue });
                txtDetalle.Inlines.Add(new Run("  Equivalencia: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["equivalencia"].Value.ToString()) { Foreground = Brushes.Blue });
                txtDetalle.Inlines.Add(new Run("  Original: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["original"].Value.ToString()) { Foreground = Brushes.Blue });
                txtDetalle.Inlines.Add(new LineBreak());

                txtDetalle.Inlines.Add(new Run("Precio: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["precio"].Value.ToString()) { Foreground = Brushes.Red });

                txtDetalle.Inlines.Add(new Run("  Precio de Oferta: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["preciooferta"].Value.ToString()) { Foreground = Brushes.Red });
                txtDetalle.Inlines.Add(new Run("  Mínimo de oferta: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["ofertacantidad"].Value.ToString() + " unidades") { Foreground = Brushes.Blue });
                txtDetalle.Inlines.Add(new Run("  -EN OFERTA-") { FontWeight = FontWeights.Bold, FontStyle = FontStyles.Italic, Foreground = Brushes.Red });
                txtDetalle.Inlines.Add(new Run("  -NUEVO-") { FontWeight = FontWeights.Bold, FontStyle = FontStyles.Italic, Foreground = Brushes.Red });
                txtDetalle.Inlines.Add(new Run("  Rotación: ") { FontWeight = FontWeights.Bold });
                txtDetalle.Inlines.Add(new Run(dato.Cells["Abc"].Value.ToString()) { Foreground = Brushes.Blue });

                imgIzquierda.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/imagenes/lineas/" + dato.Cells["linea"].Value.ToString() + ".png"));
                imgDerecha.Source = new BitmapImage(new Uri("pack://siteoforigin:,,,/imagenes/lineas/" + dato.Cells["linea"].Value.ToString() + ".png"));
       
            }
          

        }

    }
}
