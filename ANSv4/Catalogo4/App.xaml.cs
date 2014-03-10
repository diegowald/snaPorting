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

            Catalogo.MainMod.Main();

            // Create the startup window
            MainWindow wnd = new MainWindow();
            wnd.ShowDialog();
            wnd.Close();
            Shutdown();

            //_recibos.fRecibo wnd = new _recibos.fRecibo();
            //wnd.ShowDialog();
            //wnd.Close();
            //wnd.Dispose();
            //Shutdown();

        }        

    }
}
