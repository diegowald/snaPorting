﻿using System;
using AvalonDock;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Catalogo.Funciones.emitter_receiver;
using System.Windows.Media;

namespace Catalogo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class mwViajantes : Window
    {
        private Catalogo._productos.SearchFilter sf = null;
        private Catalogo._productos.GridViewFilter2 gv = null;
        private Catalogo._pedidos.ucPedido ped = null;
        private Catalogo._devoluciones.ucDevolucion dev = null;
        private Catalogo._recibos.ucRecibo rec = null;
        private Catalogo._interdeposito.ucInterDeposito IntDep = null;
        private Catalogo._rendiciones.ucRendiciones RenD = null;
        private Catalogo._movimientos.ucMovimientos mov = null;
        private Catalogo._novedades.ucNovedades nov = null;
        private Catalogo.varios.FlashControl flash = null;
        private Catalogo._registrofaltantes.ucFaltante faltantes = null;

        private bool _forcedToAutoHide;

        public mwViajantes()
        {
            this.Hide();
            InitializeComponent();

            setHotKeys();
            this.iSB2.Content = "Catálogo Dígital de Productos - " + Global01.VersionApp + " - " + Global01.ApellidoNombre + " (" + Global01.NroUsuario + ") - " + string.Format("{0:dd/MM/yyyy HH:mm}", Global01.F_ActCatalogo) + " - (" + Global01.MiBuild + "." + Global01.ListaPrecio + ")";

            //System.Windows.Application.Current.Resources["ThemeDictionary"] = new ResourceDictionary();
            //ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString("#CFD1D2"));
            ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString("#FFFFFF"));

            if (Global01.miSABOR <= Global01.TiposDeCatalogo.Cliente | !Global01.AppActiva)
            {
                this.dcRecibos.IsCloseable = true;
                this.dcRendiciones.IsCloseable = true;
                this.xDevolucionesAreaDockC.IsCloseable = true;
                this.xRegFaltantesAreaDockC.IsCloseable = true;

                this.dcRecibos.Close();
                this.dcRendiciones.Close();
                this.xDevolucionesAreaDockC.Close();
                this.xRegFaltantesAreaDockC.Close();
            }

            if (!Global01.AppActiva)
            {
                this.xVtaDevDockP.Visibility = System.Windows.Visibility.Collapsed;
                this.grSpliter1.Visibility = System.Windows.Visibility.Collapsed;
                this.grPedidosDevolucionesArea.Visibility = System.Windows.Visibility.Collapsed;

                this.dcEnviados.IsCloseable = true;
                this.dcEnviados.Close();
                this.dcInterDepositos.IsCloseable = true;
                this.dcInterDepositos.Close();

                this.appMenu.Visibility = System.Windows.Visibility.Collapsed;

                //this.grProductosArea.Height = 400;
                this.grProductsArea.Height = 310;
                this.grProductsDetalle.Height = 90;
                //this.productDetalle.Height = 90;

            }
            this.Closing += mwViajantes_Closing;
            Catalogo.varios.NotificationCenter.instance.updateBanner += updateBanner;
            crearControlesProductos();
            Catalogo.varios.NotificationCenter.instance.refreshNovedades += refreshNovedades;
        }

        public void updateBanner(string filename)
        {
            if (Global01.miSABOR < Global01.TiposDeCatalogo.Viajante)
            {
                this.topBanner.Children.Clear();
            }

            flash.Dispose();
            flash = null;
                       
            System.IO.File.Copy(filename, Global01.AppPath + "\\imagenes\\banner.swf", true);

            if (Global01.miSABOR < Global01.TiposDeCatalogo.Viajante) 
            {
                addFlashPlayer(); 
            }

        }

        private void addFlashPlayer()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            flash = new varios.FlashControl();
            flash.AutoScroll = true;
            flash.Dock = System.Windows.Forms.DockStyle.Top;
            flash.Location = new System.Drawing.Point(0, 0);
            flash.Name = "flash";

            flash.file = Global01.AppPath + "\\imagenes\\banner.swf";

            flash.play();

            // Assign the MaskedTextBox control as the host control's child.
            host.Child = flash;

            this.topBanner.Children.Add(host);
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

        private Catalogo._rendiciones.ucRendiciones addRendicionArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo._rendiciones.ucRendiciones xRendicion;
            xRendicion = new Catalogo._rendiciones.ucRendiciones();
            //xDevolucion.AutoScroll = false;
            //xDevolucion.Location = new System.Drawing.Point(0, 0);
            //xDevolucion.Dock = System.Windows.Forms.DockStyle.Fill;
            //xDevolucion.Name = "Devoluciones";

            host.Child = xRendicion;
            this.xRendicionArea.Children.Add(host);

            return xRendicion;
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
            xRecibo = new Catalogo._recibos.ucRecibo();
            xRecibo.AutoScroll = false;
            xRecibo.Location = new System.Drawing.Point(0, 0);
            xRecibo.Dock = System.Windows.Forms.DockStyle.Fill;
            xRecibo.Name = "Recibos";

            host.Child = xRecibo;
            this.xRecibosArea.Children.Add(host);

            return xRecibo;
        }

        private Catalogo._interdeposito.ucInterDeposito addInterDepositoArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            Catalogo._interdeposito.ucInterDeposito xInterDeposito;
            xInterDeposito = new Catalogo._interdeposito.ucInterDeposito();
            xInterDeposito.AutoScroll = false;
            xInterDeposito.Location = new System.Drawing.Point(0, 0);
            xInterDeposito.Dock = System.Windows.Forms.DockStyle.Fill;
            xInterDeposito.Name = "InterDeposito";

            host.Child = xInterDeposito;
            this.xInterDepositoArea.Children.Add(host);

            return xInterDeposito;
        }

        private Catalogo._productos.GridViewFilter2 addProductsArea()
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
            this.grProductsArea.Children.Add(host);

            return gridViewControl;
        }

        private _registrofaltantes.ucFaltante addFaltantesArea()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            Catalogo._registrofaltantes.ucFaltante f = new _registrofaltantes.ucFaltante();
            f.AutoScroll = false;
            f.Location = new System.Drawing.Point(0, 0);
            f.Dock = System.Windows.Forms.DockStyle.Fill;
            f.Name = "Faltantes";

            host.Child = f;
            this.xRegFaltantesArea.Children.Add(host);

            return f;
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

            if (Global01.miSABOR >= Global01.TiposDeCatalogo.Viajante)
            {
                this.header.Height = 26;
                topRedesSociales.Visibility = System.Windows.Visibility.Hidden;
                topBanner.Visibility = System.Windows.Visibility.Hidden;
                topLogo.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                this.header.Height = 90;
                topRedesSociales.Visibility = System.Windows.Visibility.Visible;
                topBanner.Visibility = System.Windows.Visibility.Visible;
                topLogo.Visibility = System.Windows.Visibility.Visible;
                addFlashPlayer();
            }

            this.Show();
            Catalogo.varios.SplashScreen.CloseSplashScreen();
        }

        private void dockManager_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!_forcedToAutoHide)
                return;
            _forcedToAutoHide = false;
            this.xContentMenu.Activate();
            this.xContentMenu.ToggleAutoHide();
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.xContentMenu.ToggleAutoHide();
            _forcedToAutoHide = true;
        }

        private void Title_MouseLeftButtonDown(object sender, MouseButtonEventArgs eventArgs)
        {
            if (eventArgs.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            //this.Close();
            System.Windows.Application.Current.MainWindow.Close();
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

        void mwViajantes_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (Funciones.modINIs.ReadINI("DATOS", "ConfirmaSalida", "1") == "1")
            {
                try
                {
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Saliendo del Catálogo... ¿Está seguro?", "Cerrando la Aplicación", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Information, System.Windows.Forms.MessageBoxDefaultButton.Button1);

                    if (result == System.Windows.Forms.DialogResult.No)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);
                    throw ex;
                }
            }

            //  #If Sabor = 3 Or Sabor = 4 Then

            if (Global01.OperacionActivada == "PEDIDO")
            {
                //      If cmdVISITA.Tag <> "INICIAR Visita" Then
                System.Windows.Forms.MessageBox.Show("Antes de salir debe cerrar el PEDIDO", "Atención",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                e.Cancel = true;
                return;
            }

            if (Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado > 0 )
            {
                _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado);
                if (movimientos.preguntoAlSalir())
                {
                    // Si hay movimientos pendientes pregunto si quiere enviarlos
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Tiene movimientos que aun no ha enviado. ¿QUIERE ENVIARLOS AHORA?", "ENVIO DE MOVIMIENTOS", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                    switch (result)
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                            {
                                bool Conectado = util.network.IPCache.instance.conectado;

                                if (Conectado)
                                {
                                    Catalogo.util.BackgroundTasks.EnvioMovimientos movs = new util.BackgroundTasks.EnvioMovimientos(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico,
                                        0, util.BackgroundTasks.EnvioMovimientos.MODOS_TRANSMISION.TRANSMITIR_RECORDSET, null);
                                    movs.run();
                                }
                                else
                                {
                                    _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Comunicaciones,
                                         _auditor.Auditor.AccionesAuditadas.FALLO, "Probar Conexion, TRANSMITIR_RECORDSET " + Catalogo.varios.NotificationCenter.instance.ClienteSeleccionado.ToString());
                                }
                            }
                            break;
                        case System.Windows.Forms.DialogResult.No:
                            {
                                if (Global01.miSABOR != Global01.TiposDeCatalogo.Invitado &&
                                    Global01.miSABOR != Global01.TiposDeCatalogo.Cliente &&
                                    Funciones.modINIs.ReadINI("DATOS", "EEA", "1") == "1")
                                {
                                    //Aca tiene que ir tambien el proceso que envia al server las transacciones.
                                    Catalogo.util.BackgroundTasks.EnvioMovimientos envioMovs = new util.BackgroundTasks.EnvioMovimientos(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico, 0, util.BackgroundTasks.EnvioMovimientos.MODOS_TRANSMISION.TRANSMITIR_RECORDSET_OCULTO, new System.Collections.Generic.List<util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO>());
                                    envioMovs.run();
                                    //                    If Len(Trim(Dir(vg.Path & "\monitorE.exe"))) > 0 Then
                                    //                        Shell vg.Path & "\monitorE.exe", vbHide
                                    //                    End If
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Programa, _auditor.Auditor.AccionesAuditadas.TERMINA, "se cierra la aplicacion");
        }

        private void xMenu1_web(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.autonauticasur.com.ar");
        }

        private void xMenu1_revista(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.autonauticasur-r.com.ar/revista/vigente/index2.html");
        }

        private void xMenu1_CtaCte(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.autonauticasur-r.com.ar/portals/0/w3/acc.html");
        }

        private void xMenu1_porcentajeL(object sender, RoutedEventArgs e)
        {
            cambioPctEvt(null, null);

        }

        private void xMenu1_AppConfig(object sender, RoutedEventArgs e)
        {
            try
            {
                _preferencias.PreferenciasFrm pref = new _preferencias.PreferenciasFrm();
                pref.ShowDialog();
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                util.errorHandling.ErrorForm.show();
            }
        }

        private void DocumentPane_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach (object obj in e.AddedItems)
                {
                    if (obj is AvalonDock.DocumentContent)
                    {
                        AvalonDock.DocumentContent content = obj as AvalonDock.DocumentContent;
                        //content.Foreground.SetValue = 
                        switch (content.Title)
                        {
                            case "Productos":
                                crearControlesProductos();
                                break;
                            case "Recibos":
                                crearControlesRecibos();
                                break;
                            case "Rendición de Recibos":
                                crearcontrolesRendicionRecibos();
                                break;
                            case "Inter-Depósitos":
                                crearControlesInterDepositos();
                                break;
                            case "Bandeja de Enviados":
                                crearControlesBandejaEnviados();
                                break;
                            case "Novedades en Línea":
                                crearControlesNovedades();
                                dcNovedades.SetValue(TextBlock.FontStyleProperty, FontStyles.Normal);
                                break;
                            default:
                                // Nothing
                                break;
                        }
                    }
                }
            }
        }

        private void crearcontrolesRendicionRecibos()
        {
            if (RenD == null)
            {
                RenD = addRendicionArea();
            }
        }

        private void crearControlesRecibos()
        {
            if (rec == null)
            {
                rec = addReciboArea();
            }
        }

        private void crearControlesProductos()
        {
            if (sf == null)
            {
                sf = addSearchArea();
                gv = addProductsArea();
                ped = addPedidoArea();
                dev = addDevolucionArea();
                faltantes = addFaltantesArea();

                sf.attachReceptor(gv);
                sf.attachReceptor2(gv);
                sf.attachReceptor3(gv);

                gv.attachReceptor(productDetalle);
                gv.attachReceptor(ped);
                gv.attachReceptor(dev);
                gv.attachReceptor2(sf);
                gv.attachReceptor3(ped);
                gv.attachReceptor3(dev);
            }
        }

        private void crearControlesInterDepositos()
        {
            if (IntDep == null)
            {
                IntDep = addInterDepositoArea();
            }
        }

        private void crearControlesBandejaEnviados()
        {
            if (mov == null)
            {
                mov = addMovimientosArea();
            }
        }

        private void crearControlesNovedades()
        {
            if (nov == null)
            {
                nov = addNovedadesArea();
            }
        }

        private void btnYoutube_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.youtube.com/channel/UC32Fr7Or2ZZhPnkZLn1n4-Q");
        }

        private void btnTwitter_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://twitter.com/AutoNauticaSur");
        }

        private void btnFacebook_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.facebook.com/autonauticasur");
        }

        private void btnLinkedin_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.linkedin.com/company/3667540");
        }

        private void setHotKeys()
        {
            createHotKey(Key.B, ModifierKeys.Control, buscarEvt);
            createHotKey(Key.D, ModifierKeys.Control, verResumenEvt);
            createHotKey(Key.P, ModifierKeys.Control, cambioPctEvt);
            createHotKey(Key.Z, ModifierKeys.Control, checkRregAppEvt);
        }

        private void createHotKey(System.Windows.Input.Key key, System.Windows.Input.ModifierKeys modifier, ExecutedRoutedEventHandler handler)
        {
            System.Windows.Input.RoutedCommand routedCmd = new RoutedCommand();
            routedCmd.InputGestures.Add(new KeyGesture(key, modifier));
            CommandBindings.Add(new CommandBinding(routedCmd, handler));
        }

        private void buscarEvt(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                this.dockPane.SelectedIndex = 0;
                doEvents();
                sf.Focus();
                this.sf.FocusOnSearch();
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                util.errorHandling.ErrorForm.show();
            }
        }

        private void verResumenEvt(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (dockPane.SelectedIndex == 1)
                {
                    rec.verResumen();
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                util.errorHandling.ErrorForm.show();
            }
        }

        private void cambioPctEvt(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                _productos.PorcentajeLinea f = new _productos.PorcentajeLinea();
                f.ShowDialog();
                f.Dispose();
                f = null;                                
                preload.Preloader.instance.refresh(true);
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                util.errorHandling.ErrorForm.show();
            }
        }

        private void checkRregAppEvt(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                System.Windows.Forms.MessageBox.Show("Implementar Check Registro app");
                MainMod.valida_appRegistro();
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                util.errorHandling.ErrorForm.show();
            }
        }

        public void doEvents()
        {
            Application.Current.Dispatcher.Invoke(new System.Threading.ThreadStart(delegate { }),
                System.Windows.Threading.DispatcherPriority.Background);
        }

        private delegate void refreshNovedadesDelegate();
        private void refreshNovedades()
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(new refreshNovedadesDelegate(refreshNovedades), null);
                return;
            }
            if (dockPane.SelectedIndex != 5)
            {
                dcNovedades.SetValue(TextBlock.FontStyleProperty, FontStyles.Italic);
                dcNovedades.Title = "Hay Novedades ... (atención!!)";
                dcNovedades.FontSize= 14;
                //dcNovedades.SetValue(TextBlock.ForegroundProperty, "Red");
            }
        }         
    }
}