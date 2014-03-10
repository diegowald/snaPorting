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
using Catalogo.util.emitter_receiver;

namespace Catalogo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            System.Windows.Application.Current.Resources["ThemeDictionary"] = new ResourceDictionary();
        }

        private Catalogo._productos.SearchFilter addSearchArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo._productos.SearchFilter filterControl = new _productos.SearchFilter();
            filterControl.AutoScroll = true;
            filterControl.Dock = System.Windows.Forms.DockStyle.Fill;
            filterControl.Location = new System.Drawing.Point(0, 0);
            filterControl.Name = "searchFilter";
            //filterControl.Size = new System.Drawing.Size(640, 480);
            filterControl.TabIndex = 0;
            //gridViewControl.Text = "Lista de Productos";

            // Assign the MaskedTextBox control as the host control's child.
            host.Child = filterControl;

            this.searchArea.Children.Add(host);
            return filterControl;
        }

        private Catalogo._productos.GridViewFilter2  addProductsArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo._productos.GridViewFilter2 gridViewControl;
            gridViewControl = new Catalogo._productos.GridViewFilter2();
            gridViewControl.AutoScroll = true;
            gridViewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            gridViewControl.Location = new System.Drawing.Point(0, 0);
            gridViewControl.Name = "GridViewProductos";
            gridViewControl.Size = new System.Drawing.Size(640, 480);
            gridViewControl.TabIndex = 0;
            gridViewControl.Text = "Lista de Productos";

            // Assign the MaskedTextBox control as the host control's child.
            host.Child = gridViewControl;

            // Add the interop host control to the Grid 
            // control's collection of child controltts. 
            //this.grid1.Children.Add(host);
            //this.dockManager.Documents[0]. .DefaultElement.set  .Children.Add(host);

            //this.grid2.Children.Add(host);
            this.productsArea.Children.Add(host);

            return gridViewControl;
        }

        private void DocumentPane_Loaded_1(object sender, RoutedEventArgs e)
        {
            Catalogo._productos.SearchFilter sf = addSearchArea();
            Catalogo._productos.GridViewFilter2 gv = addProductsArea();
            
            sf.attachReceptor(gv);
            sf.attachReceptor2(gv);
            gv.attachReceptor(productDetalle);
            gv.attachReceptor2(sf);
            
            this.sugerencias.Visibility = System.Windows.Visibility.Collapsed;
        }

        const string LayoutFileName = "SampleLayout.xml";

        private void SaveLayout(object sender, RoutedEventArgs e)
        {
            dockManager.SaveLayout(LayoutFileName);
        }

        private void RestoreLayout(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists(LayoutFileName))
                dockManager.RestoreLayout(LayoutFileName);
        }

        private void SetDefaultTheme(object sender, RoutedEventArgs e)
        {
            ThemeFactory.ResetTheme();
        }

        private void ChangeCustomTheme(object sender, RoutedEventArgs e)
        {
            string uri = (string)((System.Windows.Controls.MenuItem)sender).Tag;
            ThemeFactory.ChangeTheme(new Uri(uri, UriKind.RelativeOrAbsolute));
        }

        private void ChangeStandardTheme(object sender, RoutedEventArgs e)
        {
            string name = (string)((System.Windows.Controls.MenuItem)sender).Tag;
            ThemeFactory.ChangeTheme(name);
        }

        private void ChangeColor(object sender, RoutedEventArgs e)
        {

            ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString(((System.Windows.Controls.MenuItem)sender).Header.ToString()));
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.toolsContent.ToggleAutoHide();
            this.detailsContent.ToggleAutoHide();
//            if (_firstTimeLoad && !_isDataGridLoaded)
  //          {
    //            outputDockablePane.ToggleAutoHide();
      //          _forcedToAutoHide = true;
        //    }
        }

        private void btnReciboPage_Click(object sender, RoutedEventArgs e)
        {
            _recibos.frmRecibo fRecibo = new _recibos.frmRecibo();

            fRecibo.ShowDialog();

            
        }

    }
}
