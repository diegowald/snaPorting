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
        int _imgErrors = 0;
        int maxDownloadSimultaneo = 15; // Esto podria venir desde el .ini

        public DetalleProducto()
        {
            InitializeComponent();
            _imgEnDescarga = new List<string>();
        }

        public void onRecibir(System.Windows.Forms.DataGridViewRow dato)
        {
            txtDetalle.Inlines.Clear();
            txtDetalle.Visibility = System.Windows.Visibility.Hidden;
            imgIzquierda.Visibility = System.Windows.Visibility.Hidden;
            imgDerecha.Visibility = System.Windows.Visibility.Hidden;
            btnImagenAmpliada.Visibility = System.Windows.Visibility.Hidden;

            if (dato != null)
            {
                string xTipo = dato.Cells["Tipo"].Value.ToString();
                bool xOferta = (dato.Cells["Control"].Value.ToString() == "O");

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

                if (xOferta)
                {
                    txtDetalle.Inlines.Add(new Run("  Precio de Oferta: ") { FontWeight = FontWeights.Bold });
                    txtDetalle.Inlines.Add(new Run(dato.Cells["preciooferta"].Value.ToString()) { Foreground = Brushes.Red });
                    txtDetalle.Inlines.Add(new Run("  Mínimo de oferta: ") { FontWeight = FontWeights.Bold });
                    txtDetalle.Inlines.Add(new Run(dato.Cells["ofertacantidad"].Value.ToString() + " unidades") { Foreground = Brushes.Blue });
                    txtDetalle.Inlines.Add(new Run("  -EN OFERTA-") { FontWeight = FontWeights.Bold, FontStyle = FontStyles.Italic, Foreground = Brushes.Red });
                }

                if (xTipo == "prod_n")
                {
                    txtDetalle.Inlines.Add(new Run("  -NUEVO-") { FontWeight = FontWeights.Bold, FontStyle = FontStyles.Italic, Foreground = Brushes.Red });
                }
                else if (xTipo == "apli_n")
                {
                    txtDetalle.Inlines.Add(new Run("  -APLI. NUEVA-") { FontWeight = FontWeights.Bold, FontStyle = FontStyles.Italic, Foreground = Brushes.Red });
                }
                
                if (Global01.miSABOR > Global01.TiposDeCatalogo.Cliente)
                {
                    txtDetalle.Inlines.Add(new Run("  Rotación: ") { FontWeight = FontWeights.Bold });
                    txtDetalle.Inlines.Add(new Run(dato.Cells["Abc"].Value.ToString()) { Foreground = Brushes.Blue });
                }

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
                    }

                    //validateImagefile(ImgProducto);
                    if (isValidImage(ImgProducto) && !isImageDownloading(ImgProducto))
                    {
                        try
                        {
                            if (Funciones.modINIs.ReadINI("DATOS", "chkImagenUpdate", "1") == "1")
                            {
                                descargarImagenUpdate(ImgProductoWeb, ImgProducto); //, dato);
                            }
                            imgDerecha.Source = new BitmapImage(new Uri(ImgProducto, UriKind.Absolute));
                        }
                        catch
                        {
                            imgDerecha.Source = new BitmapImage(new Uri(ImgProductoDefault, UriKind.Absolute));
                        }
                    }
                    else
                    {
                        imgDerecha.Source = new BitmapImage(new Uri(ImgProductoDefault, UriKind.Absolute));

                        if (Funciones.modINIs.ReadINI("DATOS", "chkImagenNueva", "1") == "1")
                        {
                            descargarImagenNueva(ImgProductoWeb, ImgProducto); //, dato);
                        }
                    }
                }
                else
                {
                    imgIzquierda.Source = new BitmapImage(new Uri(ImgLineaDefault, UriKind.Absolute));
                }

                txtDetalle.Visibility = System.Windows.Visibility.Visible;
                imgIzquierda.Visibility = System.Windows.Visibility.Visible;
                imgDerecha.Visibility = System.Windows.Visibility.Visible;
                btnImagenAmpliada.Visibility = System.Windows.Visibility.Visible;
            }

        }

        private void descargarImagenNueva(string Origen, string Destino) //, System.Windows.Forms.DataGridViewRow dato)
        {
            if (!isImageDownloading(Destino) && canDownloadNewImage())
            {
                setImageDownloading(Destino);
                util.FileDownloader downloader = new util.FileDownloader(Origen, Destino, null, util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
                downloader.onFileDownloaded += onFileDownloaded;
                downloader.onFileDownloading += onFileDownloading;
                downloader.onFileProblem += onFileProblem;

                _Destino = Destino;
                try
                {
                    downloader.run();
                    //ImgWeb.DownloadFile(Origen, Destino);
                    //                        ImgWeb.DownloadFileAsync(new Uri(Origen), Destino);
                }
                catch (System.Web.HttpException ex)
                {
                    //throw ex;
                    util.errorHandling.ErrorLogger.LogMessage(ex);
                }
                catch (System.Net.WebException wex)
                {
                    //throw wex;
                    util.errorHandling.ErrorLogger.LogMessage(wex);
                }
            }
        }

        private void descargarImagenUpdate(string Origen, string Destino) //, System.Windows.Forms.DataGridViewRow dato)
        {
                try
                {
                    string xModified_Time_File = "2000-12-01 23:59";
            
                    xModified_Time_File = Funciones.oleDbFunciones.Comando(Global01.Conexion,"SELECT Modified_Time FROM ansImagenes WHERE Full_Path='" + Destino.Replace(Global01.AppPath,"") + "'","Modified_Time",false,null);
                    if (xModified_Time_File.Trim().Length > 0)
                    {
                        if (System.IO.File.Exists(Destino))
                        {
                            System.IO.FileInfo fi = new System.IO.FileInfo(Destino);
                            string fiModified_Time = fi.LastWriteTime.Date.ToString("yyyy-MM-dd HH:mm");
                            if (DateTime.Parse(xModified_Time_File) > DateTime.Parse(fiModified_Time))
                            {
                                descargarImagenNueva(Origen, Destino); //, dato);
                            }
                        }
                    }

                }
                catch (System.Web.HttpException ex)
                {
                    //throw ex;
                    util.errorHandling.ErrorLogger.LogMessage(ex);
                }
                catch (System.Net.WebException wex)
                {
                    //throw wex;
                    util.errorHandling.ErrorLogger.LogMessage(wex);
                }
        }

        private bool isValidImage(string fileName)
        {
            if (System.IO.File.Exists(fileName))
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
                return fi.Length > 0;
            }
            else
            {
                return false;
            }
        }


        //private void validateImagefile(string fileName)
        //{
        //    try
        //    {
        //        if (System.IO.File.Exists(fileName))
        //        {
        //            System.IO.FileInfo fi = new System.IO.FileInfo(fileName);
        //            if (fi.Length == 0)
        //            {
        //                //fi.Delete();
        //            }
        //        }
        //    }
        //    catch (System.IO.IOException ex)
        //    {
        //        //throw ex;
        //        util.errorHandling.ErrorLogger.LogMessage(ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        //throw ex;
        //        util.errorHandling.ErrorLogger.LogMessage(ex);
        //    }
        //}


        private System.Collections.Generic.List<string> _imgEnDescarga;

        private bool isImageDownloading(string file)
        {
            return _imgEnDescarga.Contains(file);
        }

        private bool canDownloadNewImage()
        {
            return _imgEnDescarga.Count - _imgErrors < maxDownloadSimultaneo;
        }

        private void setImageDownloading(string file)
        {
            if (!isImageDownloading(file))
            {
                _imgEnDescarga.Add(file);
            }
        }


        internal void onFileDownloaded(object Tag, string Destino)
        {
            
            //validateImagefile(_Destino);
            if (isValidImage(Destino))
            {
                try
                {
                    if (imgDerecha.Dispatcher.CheckAccess())
                    {
                        imgDerecha.Source = new BitmapImage(new Uri(_Destino, UriKind.Absolute));
                        _imgEnDescarga.Remove(_Destino);
                    }
                    else
                    {
                        imgDerecha.Dispatcher.Invoke(new Action(() =>
                            {
                                imgDerecha.Source = new BitmapImage(new Uri(_Destino, UriKind.Absolute));
                                _imgEnDescarga.Remove(_Destino);
                            }));
                    }
                }
                catch (Exception ex)
                {
                    string ImgProductoDefault = Global01.AppPath + "\\imagenes\\default.jpg";
                    imgDerecha.Source = new BitmapImage(new Uri(ImgProductoDefault, UriKind.Absolute));
                    //throw ex;
                    util.errorHandling.ErrorLogger.LogMessage(ex);
                }
            }
            else
            {
                util.errorHandling.ErrorLogger.LogMessage("Problema al descargar imagen " + Destino);
                string ImgProductoDefault = Global01.AppPath + "\\imagenes\\default.jpg";
                imgDerecha.Source = new BitmapImage(new Uri(ImgProductoDefault, UriKind.Absolute));
            }
        }

        internal void onFileProblem(object Tag, string Destino, string cause)
        {
            util.errorHandling.ErrorLogger.LogMessage("Problema al descargar imagen " + Destino + ". Causa; " + cause);
            _imgErrors++;
        }

        internal void onFileDownloading(object Tag, string Destino, int progress)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Catalogo._productos.DetalleImagen frm = new DetalleImagen(imgDerecha.Source);
            frm.ShowDialog();
        }
    }
}
