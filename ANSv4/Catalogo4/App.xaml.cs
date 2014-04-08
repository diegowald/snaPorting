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

        //SplashScreenForm ssf = new Catalogo.SplashScreenForm();

        private void Application_Startup(object sender, StartupEventArgs e)
        {

            Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();

            Catalogo.MainMod.Main();

           // Catalogo._pruebas.fPruebas wnd = new Catalogo._pruebas.fPruebas();
            //mwClientes wnd = new mwClientes();
            mwViajantes wnd = new mwViajantes();

            Application.Current.Exit += Current_Exit;

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
