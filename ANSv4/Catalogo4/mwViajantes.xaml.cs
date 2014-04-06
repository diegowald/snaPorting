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
    public partial class mwViajantes : Window
    {
<<<<<<< HEAD:ANSv4/Catalogo4/mwViajantes.xaml.cs
        
        public mwViajantes()
=======

        private Catalogo._productos.SearchFilter sf = null;
        private Catalogo._productos.GridViewFilter2 gv = null;
        private Catalogo._pedidos.ucPedido ped = null;
        private Catalogo._devoluciones.ucDevolucion dev = null;

        public MainWindow()
>>>>>>> 8571054b4f4f12d784631e95c45860d3b0524931:ANSv4/Catalogo4/MainWindow.xaml.cs
        {
            this.Hide();
            InitializeComponent();            
            //System.Windows.Application.Current.Resources["ThemeDictionary"] = new ResourceDictionary();
            //ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString("#CFD1D2"));
            ThemeFactory.ChangeColors((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            this.Closing += MainWindow_Closing;
        }

<<<<<<< HEAD:ANSv4/Catalogo4/mwViajantes.xaml.cs
=======

        private void addFlashPlayer()
        {
            // Create the interop host control.
            System.Windows.Forms.Integration.WindowsFormsHost host = new System.Windows.Forms.Integration.WindowsFormsHost();

            // Create the MaskedTextBox control.
            Catalogo.util.FlashControl flash = new util.FlashControl();
            //ShockwaveFlashObjects.ShockwaveFlashClass flash = new ShockwaveFlashObjects.ShockwaveFlashClass();
            flash.AutoScroll = true;
            flash.Dock = System.Windows.Forms.DockStyle.Top;
            flash.Location = new System.Drawing.Point(0, 0);
            flash.Name = "flash";
            flash.file = "http://samples.mplayerhq.hu/SWF/962_fws.swf";
            //filterControl.Size = new System.Drawing.Size(640, 480);
            //filterControl.TabIndex = 0;
            //gridViewControl.Text = "Lista de Productos";
            flash.play();
            // Assign the MaskedTextBox control as the host control's child.
            host.Child = flash;

            //this.topBanner.Children.Add(host);
        }

>>>>>>> 8571054b4f4f12d784631e95c45860d3b0524931:ANSv4/Catalogo4/MainWindow.xaml.cs
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
            xRecibo = new Catalogo._recibos.ucRecibo() ;
            xRecibo.AutoScroll = false;
            xRecibo.Location = new System.Drawing.Point(0, 0);
            xRecibo.Dock = System.Windows.Forms.DockStyle.Fill;            
            xRecibo.Name = "Recibos";

            host.Child = xRecibo;
            this.xRecibosArea.Children.Add(host);

            return xRecibo;
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

            Catalogo._recibos.ucRecibo rec = addReciboArea();
            Catalogo._movimientos.ucMovimientos mov = addMovimientosArea();
            ////Catalogo._novedades.ucNovedades nov = addNovedadesArea();

            sf = addSearchArea();
            gv = addProductsArea();
            ped = addPedidoArea();
            dev = addDevolucionArea();

            sf.attachReceptor(gv);
            sf.attachReceptor2(gv);

            gv.attachReceptor(productDetalle);
            gv.attachReceptor(ped);
            gv.attachReceptor(dev);
            gv.attachReceptor2(sf);
            gv.attachReceptor3(ped);
            gv.attachReceptor3(dev);

            ped.attachReceptor(rec);
            ped.attachReceptor(dev);
            ped.attachReceptor(mov); 

            rec.attachReceptor(ped);
            rec.attachReceptor(dev);
            rec.attachReceptor(mov); 

            dev.attachReceptor(ped);
            dev.attachReceptor(dev);
            dev.attachReceptor(mov); 

            this.Show();
            SplashScreen.CloseSplashScreen();

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
            Close();
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


        void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            //    On Error GoTo ErrorGuardianLocalHandler

            if (Funciones.modINIs.ReadINI("DATOS", "ConfirmaSalida", "1") == "1")
            {
                if (System.Windows.Forms.MessageBox.Show("Saliendo del Catálogo... ¿Está seguro?", "Cerrando la Aplicación", System.Windows.Forms.MessageBoxButtons.YesNo)
                    == System.Windows.Forms.DialogResult.No)
                {
                    e.Cancel = true;
                    return;
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
                                //if (ProbarConexion())
                                if (true)
                                {
                                    Catalogo.util.BackgroundTasks.EnvioMovimientos movs = new util.BackgroundTasks.EnvioMovimientos(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico,
                                        0, false, util.BackgroundTasks.EnvioMovimientos.MODOS_TRANSMISION.TRANSMITIR_RECORDSET);
                                    movs.run();
                                    //                  Dim dlg As frmConexionEnvio
                                    //                  Set dlg = New frmConexionEnvio
                                    //                  dlg.ModoTransmision = TRANSMITIR_RECORDSET
                                    //                  dlg.IdCliente = 0
                                    //                  dlg.Show vbModal, Me
                                    //                  Set dlg = Nothing
                                }
                                else
                                {
                                    auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Comunicaciones,
                                         auditoria.Auditor.AccionesAuditadas.FALLO, "Probar Conexion, TRANSMITIR_RECORDSET " + ped.IDClienteSeleccionado.ToString());
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
            auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Programa,
                auditoria.Auditor.AccionesAuditadas.TERMINA, "se cierra la aplicacion");
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


    }
}
