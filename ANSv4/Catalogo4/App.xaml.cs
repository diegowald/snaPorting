using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Catalogo
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Create the startup window
            MainWindow wnd = new MainWindow();

         //   _recibos.frmRecibo wnd = new _recibos.frmRecibo();

            // Do stuff here, e.g. to the window
            wnd.Title = "Recibo para el cliente...";

            // Show the window
            wnd.Show();
        }

    }
}
