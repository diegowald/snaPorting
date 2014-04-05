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
using AvalonDock;
using Catalogo.Funciones.emitter_receiver;
using System.Threading;


namespace Catalogo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            this.Hide();
            InitializeComponent();            
            //System.Windows.Application.Current.Resources["ThemeDictionary"] = new ResourceDictionary();
            //ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString("#CFD1D2"));
            ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString("#FFFFFF"));
        }

        private void addFlashPlayer()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo.util.FlashControl flash = new util.FlashControl();
            //ShockwaveFlashObjects.ShockwaveFlashClass flash = new ShockwaveFlashObjects.ShockwaveFlashClass();
            flash.AutoScroll = true;
            flash.Dock = System.Windows.Forms.DockStyle.Top;
            flash.Location = new System.Drawing.Point(0, 0);
            flash.Name = "flash";
            flash.file = "http://samples.mplayerhq.hu/SWF/962_fws.swf";
            //filterControl.Size = new System.Drawing.Size(640, 480);
            //filterControl.TabIndex = 0;
            //gridViewControl.Text = "Lista de Productos";
            flash.play();
            // Assign the MaskedTextBox control as the host control's child.
            host.Child = flash;

            //this.topBanner.Children.Add(host);
        }

        private Catalogo._productos.SearchFilter addSearchArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            Catalogo._productos.SearchFilter filterControl = new _productos.SearchFilter();
            filterControl.AutoScroll = false;
            filterControl.Location = new System.Drawing.Point(0, 0);
            filterControl.Dock = System.Windows.Forms.DockStyle.Top;       
            filterControl.Name = "searchFilter";
          //filterControl.Size = new System.Drawing.Size(640, 480);

            // Assign the MaskedTextBox control as the host control's child.
            host.Child = filterControl;

            this.searchArea.Children.Add(host);
            return filterControl;
        }

        private Catalogo._pedidos.ucPedido addPedidoArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo._pedidos.ucPedido xPedido;
            xPedido = new Catalogo._pedidos.ucPedido();
            //xPedido.AutoScroll = false;
            //xPedido.Location = new System.Drawing.Point(0, 0);
            //xPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            //xPedido.Name = "Notas de Venta";

            host.Child = xPedido;
            this.xNotaVentaArea.Children.Add(host);

            return xPedido;
        }

        private Catalogo._devoluciones.ucDevolucion addDevolucionArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo._devoluciones.ucDevolucion xDevolucion;
            xDevolucion = new Catalogo._devoluciones.ucDevolucion();
            //xDevolucion.AutoScroll = false;
            //xDevolucion.Location = new System.Drawing.Point(0, 0);
            //xDevolucion.Dock = System.Windows.Forms.DockStyle.Fill;
            //xDevolucion.Name = "Devoluciones";

            host.Child = xDevolucion;
            this.xDevolucionArea.Children.Add(host);

            return xDevolucion;
        }

        private Catalogo._movimientos.ucMovimientos addMovimientosArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            Catalogo._movimientos.ucMovimientos xMovimientos;
            xMovimientos = new Catalogo._movimientos.ucMovimientos();
            xMovimientos.AutoScroll = false;
            xMovimientos.Location = new System.Drawing.Point(0, 0);
            xMovimientos.Dock = System.Windows.Forms.DockStyle.Fill;
            xMovimientos.Name = "Movimientos";

            host.Child = xMovimientos;
            this.xMovimientosArea.Children.Add(host);

            return xMovimientos;
        }

        private Catalogo._recibos.ucRecibo addReciboArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            Catalogo._recibos.ucRecibo xRecibo;
            xRecibo = new Catalogo._recibos.ucRecibo() ;
            xRecibo.AutoScroll = false;
            xRecibo.Location = new System.Drawing.Point(0, 0);
            xRecibo.Dock = System.Windows.Forms.DockStyle.Fill;            
            xRecibo.Name = "Recibos";

            host.Child = xRecibo;
            this.xRecibosArea.Children.Add(host);

            return xRecibo;
        }

        private Catalogo._productos.GridViewFilter2  addProductsArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            Catalogo._productos.GridViewFilter2 gridViewControl;
            gridViewControl = new Catalogo._productos.GridViewFilter2();
            gridViewControl.AutoScroll = false;
            gridViewControl.Location = new System.Drawing.Point(0, 0);            
            gridViewControl.Dock = System.Windows.Forms.DockStyle.Fill;            
            gridViewControl.Name = "GridViewProductos";

            host.Child = gridViewControl;
            this.productsArea.Children.Add(host);

            return gridViewControl;
        }

        private Catalogo._novedades.ucNovedades addNovedadesArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo._novedades.ucNovedades novedadesControl = new _novedades.ucNovedades();

            //gridViewControl.AutoScroll = true;
            novedadesControl.Dock = System.Windows.Forms.DockStyle.Top;
            novedadesControl.Location = new System.Drawing.Point(0, 0);
            novedadesControl.Name = "Novedades";

            host.Child = novedadesControl;
            this.grNovedades.Children.Add(host);

            return novedadesControl;
        }

        private void DocumentPane_Loaded_1(object sender, RoutedEventArgs e)
        {

            Catalogo._recibos.ucRecibo rec = addReciboArea();
            Catalogo._movimientos.ucMovimientos mov = addMovimientosArea();
            ////Catalogo._novedades.ucNovedades nov = addNovedadesArea();

            Catalogo._productos.SearchFilter sf = addSearchArea();
            Catalogo._productos.GridViewFilter2 gv = addProductsArea();
            Catalogo._pedidos.ucPedido ped = addPedidoArea();
            Catalogo._devoluciones.ucDevolucion dev = addDevolucionArea();

            //addFlashPlayer();

            sf.attachReceptor(gv);
            sf.attachReceptor2(gv);

            gv.attachReceptor(productDetalle);
            gv.attachReceptor(ped);
            gv.attachReceptor(dev);
            gv.attachReceptor2(sf);
            gv.attachReceptor3(ped);
            gv.attachReceptor3(dev);

            ped.attachReceptor(rec);
            ped.attachReceptor(dev);
            ped.attachReceptor(mov); 

            rec.attachReceptor(ped);
            rec.attachReceptor(dev);
            rec.attachReceptor(mov); 

            dev.attachReceptor(ped);
            dev.attachReceptor(dev);
            dev.attachReceptor(mov); 

            this.Show();
            SplashScreen.CloseSplashScreen();

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.ContentMenu.ToggleAutoHide();
        }

        private void DocumentPane_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            ////System.Diagnostics.Debug.WriteLine((e.AddedItems[0] as AvalonDock.DocumentContent).Title);
            //bool mostrarParteInferior = (e.AddedItems[0] as AvalonDock.DocumentContent).Title == "Productos";

            //if (mostrarParteInferior)
            //{
            //    if (xVtaDevDockP != null)
            //    {
            //        //xVtaDevDockP.Visibility = System.Windows.Visibility.Visible;
            //    };

            //    if (xDevolucionesAreaDockC != null)
            //    {
            //        xDevolucionesAreaDockC.Show();
            //    }
            //    if (xNotaVentaAreaDockC != null)
            //    {
            //        xNotaVentaAreaDockC.Show();
            //    }
            //}
            //else
            //{
            //    if (xVtaDevDockP != null)
            //    {
            //        //xVtaDevDockP.Visibility = System.Windows.Visibility.Collapsed;
            //    };                
                

            //    if (xDevolucionesAreaDockC != null)
            //    {
            //        xDevolucionesAreaDockC.Hide();
            //    }
            //    if (xNotaVentaAreaDockC != null)
            //    {
            //        xNotaVentaAreaDockC.Hide();
            //    }
            //}
        }



        //const string LayoutFileName = "SampleLayout.xml";

        //private void SaveLayout(object sender, RoutedEventArgs e)
        //{
        //    dockManager.SaveLayout(LayoutFileName);
        //}

        //private void RestoreLayout(object sender, RoutedEventArgs e)
        //{
        //    if (System.IO.File.Exists(LayoutFileName))
        //        dockManager.RestoreLayout(LayoutFileName);
        //}

        //private void SetDefaultTheme(object sender, RoutedEventArgs e)
        //{
        //    ThemeFactory.ResetTheme();
        //}

        //private void ChangeCustomTheme(object sender, RoutedEventArgs e)
        //{
        //    string uri = (string)((System.Windows.Controls.MenuItem)sender).Tag;
        //    ThemeFactory.ChangeTheme(new Uri(uri, UriKind.RelativeOrAbsolute));
        //}

        //private void ChangeStandardTheme(object sender, RoutedEventArgs e)
        //{
        //    string name = (string)((System.Windows.Controls.MenuItem)sender).Tag;
        //    ThemeFactory.ChangeTheme(name);
        //}

        //private void ChangeColor(object sender, RoutedEventArgs e)
        //{
        //    ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString(((System.Windows.Controls.MenuItem)sender).Header.ToString()));
        //}


        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            if (eventArgs.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {

            if (Global01.Conexion != null)
            {
                Global01.Conexion.Close();
                Global01.Conexion = null;
            }

            MainMod.miEnd();
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
