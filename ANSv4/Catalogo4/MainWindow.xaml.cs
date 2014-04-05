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
//using System.Threading;

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
            ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString("#CFD1D2"));
        }

        private Catalogo._productos.SearchFilter addSearchArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo._productos.SearchFilter filterControl = new _productos.SearchFilter();
            filterControl.AutoScroll = true;
            filterControl.Dock = System.Windows.Forms.DockStyle.Top;
            filterControl.Location = new System.Drawing.Point(0, 0);
            filterControl.Name = "searchFilter";

            //filterControl.Size = new System.Drawing.Size(640, 480);
            //filterControl.TabIndex = 0;
            //gridViewControl.Text = "Lista de Productos";

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
            //xNotaVenta.AutoScroll = true;
            //xPedido.Dock = System.Windows.Forms.DockStyle.Fill;
            //xPedido.Location = new System.Drawing.Point(0, 0);
            //xPedido.Name = "Notas de Venta";

            host.Child = xPedido;
            this.xNotaVentaArea.Children.Add(host);

            return xPedido;
        }

        //private Catalogo._devoluciones.ucDevolucion addDevolucionArea()
        //{
        //    // Create the interop host control.
        //    System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

        //    // Create the MaskedTextBox control.
        //    Catalogo._devoluciones.ucDevolucion xDevolucion;
        //    xDevolucion = new Catalogo._devoluciones.ucDevolucion();
        //    //xNotaVenta.AutoScroll = true;
        //    xDevolucion.Dock = System.Windows.Forms.DockStyle.Fill;
        //    xDevolucion.Location = new System.Drawing.Point(0, 0);
        //    xDevolucion.Name = "Devoluciones";

        //    host.Child = xDevolucion;
        //    this.xDevolucionesArea.Children.Add(host);

        //    return xDevolucion;
        //}

        private Catalogo._recibos.ucRecibo addReciboArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo._recibos.ucRecibo xRecibo;
            xRecibo = new Catalogo._recibos.ucRecibo() ;
            //xRecibo.AutoScroll = true;
            xRecibo.Dock = System.Windows.Forms.DockStyle.Fill;
            xRecibo.Location = new System.Drawing.Point(0, 0);
            xRecibo.Name = "Recibos";

            host.Child = xRecibo;
            this.xRecibosArea.Children.Add(host);

            return xRecibo;
        }

        private Catalogo._productos.GridViewFilter2  addProductsArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo._productos.GridViewFilter2 gridViewControl;
            gridViewControl = new Catalogo._productos.GridViewFilter2();
            //gridViewControl.AutoScroll = true;
            gridViewControl.Dock = System.Windows.Forms.DockStyle.Top;
            gridViewControl.Location = new System.Drawing.Point(0, 0);
            gridViewControl.Name = "GridViewProductos";
            //gridViewControl.Size = new System.Drawing.Size(640, 480);
            //gridViewControl.TabIndex = 0;
            //gridViewControl.Text = "Lista de Productos";

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
            //gridViewControl.Size = new System.Drawing.Size(640, 480);
            //gridViewControl.TabIndex = 0;
            //gridViewControl.Text = "Lista de Productos";

            host.Child = novedadesControl;
            this.grNovedades.Children.Add(host);

            return novedadesControl;
        }

        private void DocumentPane_Loaded_1(object sender, RoutedEventArgs e)
        {         
            Catalogo._productos.SearchFilter sf = addSearchArea();

            Catalogo._productos.GridViewFilter2 gv = addProductsArea();
            
            Catalogo._recibos.ucRecibo rec = addReciboArea();

            Catalogo._pedidos.ucPedido ped = addPedidoArea();

            //Catalogo._devoluciones.ucDevolucion dev = addDevolucionArea();

            //Catalogo._novedades.ucNovedades nov = addNovedadesArea();


            sf.attachReceptor(gv);
            sf.attachReceptor2(gv);

            gv.attachReceptor(productDetalle);
            gv.attachReceptor(ped);
            gv.attachReceptor2(sf);
            gv.attachReceptor3(ped);
            
            //this.sugerencias.Visibility = System.Windows.Visibility.Collapsed;
            
            this.Show();
            SplashScreen.CloseSplashScreen();
            //this.Activate(); 

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.ContentMenu.ToggleAutoHide();
            //this.ContentSugerencias.ToggleAutoHide();
        }

        private void DocumentPane_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine((e.AddedItems[0] as AvalonDock.DocumentContent).Title);
            bool mostrarParteInferior = (e.AddedItems[0] as AvalonDock.DocumentContent).Title == "Productos";
            //misTabsAcciones.Visible = PieVisible;
            if (mostrarParteInferior)
            {
                if (xDevolucionesAreaDockC != null)
                {
                    xDevolucionesAreaDockC.Show();
                }
                if (xNotaVentaAreaDockC != null)
                {
                    xNotaVentaAreaDockC.Show();
                }
            }
            else
            {
                if (xDevolucionesAreaDockC != null)
                {
                    xDevolucionesAreaDockC.Hide();
                }
                if (xNotaVentaAreaDockC != null)
                {
                    xNotaVentaAreaDockC.Hide();
                }
            }
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
            this.Close();
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
