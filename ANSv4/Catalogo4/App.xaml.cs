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
<<<<<<< HEAD

            mwClientes wnd = new mwClientes();
            //mwViajantes wnd = new mwViajantes();
=======
            Application.Current.Exit += Current_Exit;
            MainWindow wnd = new MainWindow();
>>>>>>> 8571054b4f4f12d784631e95c45860d3b0524931
            wnd.ShowDialog();
            
            if (Global01.Conexion != null)
            {
                Global01.Conexion.Close();
                Global01.Conexion = null;
            }
            MainMod.miEnd();
            Shutdown();
        }

        void Current_Exit(object sender, ExitEventArgs e)
        {
            throw new NotImplementedException();
        }        

    }
}
