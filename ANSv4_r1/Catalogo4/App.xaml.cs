﻿using System;
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
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Catalogo.MainMod.inicializaGlobales();
 
            Thread splashthread = new Thread(new ThreadStart(Catalogo.varios.SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();

            Catalogo.MainMod.Main();

            mwViajantes wnd = new mwViajantes();

            Application.Current.Exit += Current_Exit;

            wnd.ShowDialog();
            
            if (Global01.Conexion != null)
            {
                Global01.Conexion.Close();
                Global01.Conexion = null;
            }
            
            System.Diagnostics.Debug.WriteLine("saliendo por acá....");

            MainMod.miEnd(false);
            Application.Current.Shutdown();
        }

        void Current_Exit(object sender, ExitEventArgs e)
        {
            //throw new NotImplementedException();
        }        

    }
}
