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
        string _Destino = "";

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

                string ImgLineaDefault = Global01.AppPath + "\\imagenes\\default.jpg";
                string ImgProductoDefault = Global01.AppPath + "\\imagenes\\default.jpg";

                if (dato.Cells["linea"].Value.ToString().Length > 0)
                {
                    string ImgLinea = Global01.AppPath + "\\imagenes\\lineas\\" + dato.Cells["linea"].Value.ToString() + ".png";
                    string ImgProductoWeb = "http://" + Global01.URL_ANS + "/IMAGENES/" + dato.Cells["linea"].Value.ToString() + "/ARTICULOS/" + dato.Cells["c_producto"].Value.ToString().Replace("/", " ") + ".jpg";                    
                    string ImgProducto = Global01.AppPath + "\\imagenes\\" + dato.Cells["linea"].Value.ToString() + "\\Articulos\\" + dato.Cells["c_producto"].Value.ToString().Replace("/", " ") + ".jpg";

                    if (System.IO.File.Exists(ImgLinea))
                    {
                        imgIzquierda.Source = new BitmapImage(new Uri(ImgLinea, UriKind.Absolute));
                    }
                    else
                    {
                        imgIzquierda.Source = new BitmapImage(new Uri(ImgLineaDefault, UriKind.Absolute));
                    };

                    if (System.IO.File.Exists(ImgProducto))
                    {
                        imgDerecha.Source = new BitmapImage(new Uri(ImgProducto, UriKind.Absolute));
                    }
                    else 
                    {
                        imgDerecha.Source = new BitmapImage(new Uri(ImgProductoDefault, UriKind.Absolute));

                        if (Funciones.modINIs.ReadINI("DATOS","chkImagenUpdate", "0")=="1")
                        {                            
                            descargarImagen(ImgProductoWeb, ImgProducto);
                        }

                    };
                }
                else
                {
                    imgIzquierda.Source = new BitmapImage(new Uri(ImgLineaDefault, UriKind.Absolute));
                };

            };

        }

        private void descargarImagen(string Origen, string Destino)
        {
            System.Net.WebClient ImgWeb = new System.Net.WebClient();
            ImgWeb.DownloadFileCompleted += ImgWeb_DownloadFileCompleted;

           try 
            {
                _Destino = Destino;
                //ImgWeb.DownloadFile(Origen, Destino);
                ImgWeb.DownloadFileAsync(new Uri(Origen), Destino);

            }
            catch (Exception e)
            {               
                     throw new Exception(e.Message.ToString() );
            };
            
        }

        void ImgWeb_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            imgDerecha.Source = new BitmapImage(new Uri(_Destino, UriKind.Absolute));
        }

    }
}
