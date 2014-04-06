using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Threading;

namespace Catalogo
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {

        //SplashScreenForm ssf = new Catalogo.SplashScreenForm();

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();

            Catalogo.MainMod.Main();

            mwClientes wnd = new mwClientes();
            //mwViajantes wnd = new mwViajantes();
            wnd.ShowDialog();

            if (Global01.Conexion != null)
            {
                Global01.Conexion.Close();
                Global01.Conexion = null;
            }
            wnd.Close();


            Shutdown();
        }        

    }
}
