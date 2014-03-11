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

            //Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            //splashthread.IsBackground = true;
            //splashthread.Start();

            Catalogo.Login f = new Catalogo.Login();

            Catalogo.MainMod.Main();

            if (true) //(f.ShowDialog() == true)
            {
                MainWindow wnd = new MainWindow();
                wnd.ShowDialog();
                wnd.Close();

                //_recibos.fRecibo wnd = new _recibos.fRecibo();
                //wnd.ShowDialog();
                //wnd.Close();
                //wnd.Dispose();
            }
  
            Shutdown();
        }        

    }
}
