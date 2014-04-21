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
using System.Threading;

namespace Catalogo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class mwClientes : Window
    {

        private Catalogo._productos.SearchFilter sf = null;
        private Catalogo._productos.GridViewFilter2 gv = null;
        private Catalogo._pedidos.ucPedido ped = null;

        private Catalogo._interdeposito.ucInterDeposito IntDep = null;
        private Catalogo._movimientos.ucMovimientos mov = null;
        private Catalogo._novedades.ucNovedades nov = null;

        public mwClientes()
        {
            try
            {
                this.Hide();
                InitializeComponent();
                //System.Windows.Application.Current.Resources["ThemeDictionary"] = new ResourceDictionary();
                //ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString("#CFD1D2"));
                ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                this.Closing += mwClientes_Closing;

#if SaborViajante
    this.header.Visibility = System.Windows.Visibility.Hidden;
#endif

            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                //throw ex;
            }
        }

        private void addFlashPlayer()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo.varios.FlashControl flash = new varios.FlashControl();
            flash.AutoScroll = true;
            flash.Dock = System.Windows.Forms.DockStyle.Top;
            flash.Location = new System.Drawing.Point(0, 0);
            flash.Name = "flash";
            flash.file = @"D:\Desarrollos\GitHub\snaPorting\ANSv4\Catalogo4\recursos\autonatica.swf";
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

        private Catalogo._productos.GridViewFilter2  addProductsArea()
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

            host.Child = novedadesControl;
            this.grNovedades.Children.Add(host);

            return novedadesControl;
        }

        private void DocumentPane_Loaded_1(object sender, RoutedEventArgs e)
        {
            addFlashPlayer();
            /*
           IntDep = addInterDepositoArea();  
           mov = addMovimientosArea();
           //nov = addNovedadesArea();

            sf = addSearchArea();
            gv = addProductsArea();
            ped = addPedidoArea();

            sf.attachReceptor(gv);
            sf.attachReceptor2(gv);

            gv.attachReceptor(productDetalle);
            gv.attachReceptor(ped);
            gv.attachReceptor2(sf);
            gv.attachReceptor3(ped);

            ped.attachReceptor(mov);
*/
            this.Show();
            Catalogo.varios.SplashScreen.CloseSplashScreen();

        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            this.ContentMenu.ToggleAutoHide();
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

            if (Global01.Conexion != null)
            {
                Global01.Conexion.Close();
                Global01.Conexion = null;
            }

            MainMod.miEnd();
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

        void mwClientes_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            //    On Error GoTo ErrorGuardianLocalHandler

            if (Funciones.modINIs.ReadINI("DATOS", "ConfirmaSalida", "1") == "1")
            {
                try
                {
                    if (System.Windows.Forms.MessageBox.Show("Saliendo del Catálogo... ¿Está seguro?", "Cerrando la Aplicación", System.Windows.Forms.MessageBoxButtons.YesNo)
                        == System.Windows.Forms.DialogResult.No)
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
                System.Windows.Forms.MessageBox.Show("Antes de salir debe cerrar VISITA", "Atención",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                e.Cancel = true;
                return;
            }

            if (ped.IDClienteSeleccionado != -1)
            {
                _movimientos.Movimientos movimientos = new _movimientos.Movimientos(Global01.Conexion, ped.IDClienteSeleccionado);
                if (movimientos.preguntoAlSalir())
                {
                    //          ' Si hay movimientos pendientes pregunto si quiere enviarlos
                    System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Tiene movimientos que aun no ha enviado. ¿QUIERE ENVIARLOS AHORA?", "ENVIO DE MOVIMIENTOS", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                    switch (result)
                    {
                        case System.Windows.Forms.DialogResult.Yes:
                            {

                                bool Conectado = util.SimplePing.ping(Global01.URL_ANS, 5000, 0);
                                if (!Conectado)
                                {
                                    Conectado = util.SimplePing.ping(Global01.URL_ANS2, 5000, 0);
                                }

                                if (Conectado)
                                {
                                    Catalogo.util.BackgroundTasks.EnvioMovimientos movs = new util.BackgroundTasks.EnvioMovimientos(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico,
                                        0, util.BackgroundTasks.EnvioMovimientos.MODOS_TRANSMISION.TRANSMITIR_RECORDSET, null);
                                    movs.run();  
                                }
                                else
                                {
                                    _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Comunicaciones,
                                         _auditor.Auditor.AccionesAuditadas.FALLO, "Probar Conexion, TRANSMITIR_RECORDSET " + ped.IDClienteSeleccionado.ToString());
                                }
                            }
                            break;
                        case System.Windows.Forms.DialogResult.No:
                            {
                                if (Global01.miSABOR != Global01.TiposDeCatalogo.Invitado &&
                                    Global01.miSABOR != Global01.TiposDeCatalogo.Cliente &&
                                    Funciones.modINIs.ReadINI("DATOS", "EEA", "1") == "1")
                                {

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

            //  With Me
            //    If .WindowState <> vbMinimized Then
            //      GuardarFormatoForm Me
            //      WriteINI "Preferencias", "picHorizontal", picHorizontal.Top
            //                    
            //      GrabarLVColumnas Me.lvBuscar
            //      GrabarLVColumnas Me.lvEstado
            //      GrabarLVColumnas Me.lvMovimientos
            //      GrabarLVColumnas Me.lvPedido
            //      GrabarLVColumnas Me.lvDevolucion1
            //      GrabarLVColumnas Me.lvDevolucion2
            //      GrabarLVColumnas Me.lvValores
            //      GrabarLVColumnas Me.lvAplicacion
            //      GrabarLVColumnas Me.lvADeducir
            //    End If
            //  End With 'Me

            //  Set fExistencia = Nothing
            _auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Programa,
                _auditor.Auditor.AccionesAuditadas.TERMINA, "se cierra la aplicacion");
            //  Select Case UnloadMode
            //    Case vbFormControlMenu
            //        '0 El usuario eligió el comando Cerrar del menú Control del formulario.
            //        vg.auditor.Guardar Programa, TERMINA, "eligió el comando Cerrar"
            //    Case vbFormCode
            //        '1 Se invocó la instrucción Unload desde el código.
            //        vg.auditor.Guardar Programa, TERMINA, "instrucción Unload"
            //    Case vbAppWindows
            //        '2 La sesión actual del entorno operativo Microsoft Windows está finalizando.
            //        vg.auditor.Guardar Programa, TERMINA, "Microsoft Windows está finalizando"
            //    Case vbAppTaskManager
            //        '3 El Administrador de tareas de Microsoft Windows está cerrando la aplicación.
            //        vg.auditor.Guardar Programa, TERMINA, "vbAppTaskManager"
            //    Case vbFormMDIForm
            //        '4 Un formulario MDI secundario se está cerrando porque el formulario MDI también se está cerrando.
            //        vg.auditor.Guardar Programa, TERMINA, "vbFormMDIForm"
            //    Case vbFormOwner
            //        '5 Un formulario se está cerrando por que su formulario propietario se está cerrando
            //        vg.auditor.Guardar Programa, TERMINA, "vbFormOwner"
            // End Select

            //'-------- ErrorGuardian Begin --------
            //Exit Sub
            //
            //ErrorGuardianLocalHandler:
            //    If Err.Number = 53 Then ' el archivo de VersionAnterior NO EXISTE
            //        Resume Next
            //    Else
            //        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
            //            Case vbRetry
            //                Resume
            //            Case vbIgnore
            //                Resume Next
            //        End Select
            //    End If
            //'-------- ErrorGuardian End ----------
        }

        private void crearControlesProductos()
        {
            if (sf == null)
            {
                sf = addSearchArea();
                gv = addProductsArea();
                ped = addPedidoArea();

                sf.attachReceptor(gv);
                sf.attachReceptor2(gv);

                gv.attachReceptor(productDetalle);
                gv.attachReceptor(ped);
                gv.attachReceptor2(sf);
                gv.attachReceptor3(ped);

                if (mov != null)
                {
                    ped.attachReceptor(mov);
                }
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

                if (ped != null)
                {
                    ped.attachReceptor(mov);
                }
            }
        }

        private void crearControlesNovedades()
        {
            if (nov == null)
            {
                nov = addNovedadesArea();
            }
        }

        private void DocumentPane_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                foreach (object obj in e.AddedItems)
                {
                    if (obj is AvalonDock.DocumentContent)
                    {
                        AvalonDock.DocumentContent content = obj as AvalonDock.DocumentContent;
                        switch (content.Title)
                        {
                            case "Productos":
                                crearControlesProductos();
                                break;
                            case "Inter-Depósitos":
                                crearControlesInterDepositos();
                                break;
                            case "Bandeja de Enviados":
                                crearControlesBandejaEnviados();
                                break;
                            case "Novedades en Línea":
                                crearControlesNovedades();
                                break;
                            default:
                                // Nothing
                                break;
                        }
                    }
                }
            }
        }

        private void xMenu1_web(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.autonauticasur.com.ar");
        }

        private void xMenu1_revista(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.autonauticasur-r.com.ar/revista/vigente/index2.html");
        }

        private void xMenu1_porcentajeL(object sender, RoutedEventArgs e)
        {

        }

        private void xMenu1_AppConfig(object sender, RoutedEventArgs e)
        {
            _preferencias.PreferenciasFrm pref = new _preferencias.PreferenciasFrm();
            pref.ShowDialog();
        }

    }
}
