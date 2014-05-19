using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Reflection;
//using System.Deployment;
//using System.Threading;
//using System.Deployment.Application;

namespace Catalogo
{
    static class MainMod
    {

        public static void Main()
        {
            checkEnviroment();

            checkFlashPlayer();

            valida_ubicacionDatos();

            update_mdb();

            //--- actualiza links tables ----------------------------------------------------
            Funciones.oleDbFunciones.CambiarLinks("ans.mdb");

            //--- compactar MDB--------------------------------------------------------------
            Funciones.oleDbFunciones.CompactDatabase("catalogo.mdb");

            //-- Instancia la Conexión y un DataReader Global a la Rutina Main --
            Global01.Conexion = Funciones.oleDbFunciones.GetConn(Catalogo.Global01.strConexionUs);
            Catalogo._auditor.Auditor.instance.Conexion = Global01.Conexion;

            load_header(0);

            valida_appRegistro();

            if (Global01.AppActiva)
            {
                update_productos();
            }

            load_header(1);

            // Carga tabla de Productos en Segundo Plano
            preload.Preloader.instance.refresh();
           
            //chequea comandos y mensajes desde el servidor
            if ((Funciones.modINIs.ReadINI("DATOS", "INFO", "1") == "1") | (Global01.miSABOR > Global01.TiposDeCatalogo.Cliente))
            {
                util.BackgroundTasks.Updater updater = new util.BackgroundTasks.Updater(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico, util.BackgroundTasks.Updater.UpdateType.UpdateAppConfig);
                updater.run();
            }

            if (Global01.miSABOR > Global01.TiposDeCatalogo.Cliente) valida_appLogin();
            
            if (Global01.AppActiva)
            {
                if (Global01.ActualizarClientes)
                {
                    util.BackgroundTasks.Updater updater = new util.BackgroundTasks.Updater(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico, util.BackgroundTasks.Updater.UpdateType.UpdateCuentas);
                    updater.run();
                }

                //update_productos();
            }

            lanzarProcesosSegundoPlano();

            //- ACA ESTA LA PAPA ----------------------
            //- Run mi APP MainWindow -----------------
            //- ** RETORNA AL app.XAML y sigue la EJECUCION NORMAL ** ---
            //- Fin Main ---
        }

        private static void checkEnviroment()
        {
            string sResultado = "";
            //char ctrln = (char)0x0E;

            System.Reflection.Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fileVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(CurrentAssembly.Location);

            string xVersion = Assembly
                 .GetExecutingAssembly()
                 .GetReferencedAssemblies()
                 .Where(x => x.Name == "System.Core").First().Version.ToString();
          
            sResultado += "\r\n" + "1) Framework " + xVersion.Trim();
            sResultado += "\r\n" + "2) Environment.Version " + Environment.Version.ToString().Trim() + " -- " + System.Runtime.InteropServices.RuntimeEnvironment.GetSystemVersion().Trim();
            sResultado += "\r\n" + "3) Application.ProductVersion " + Application.ProductVersion.Trim();
            sResultado += "\r\n" + "4) File Versión " + fileVersionInfo.FileVersion.ToString();
            sResultado += "\r\n" + "5) Application.ExecutablePath " + Application.ExecutablePath.ToString().Trim() + " - Application.StartupPath" + Application.StartupPath.ToString().Trim();
            sResultado += "\r\n";

            //sResultado += "\r\n" + "4) Assembly.GetExecutingAssembly " + Assembly.GetExecutingAssembly().GetName().Version.ToString().Trim() 

            util.errorHandling.ErrorLogger.LogMessage(sResultado);

            if (byte.Parse(xVersion.Trim().Substring(0, 4).Replace(".","")) < 35)
            {
                MessageBox.Show(new Form() { TopMost = true },"El Framework 3.5-SP1 es REQUERIDO para el uso de la Aplicación!, Comuniquese con auto náutica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }

        }

        private static void lanzarProcesosSegundoPlano()
        {
            Catalogo.util.BackgroundTasks.ChequeoNovedades check = new util.BackgroundTasks.ChequeoNovedades(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico);
            check.run();

            //Aca tiene que ir tambien el proceso que envia al server las transacciones.
            Catalogo.util.BackgroundTasks.EnvioMovimientos envioMovs = new util.BackgroundTasks.EnvioMovimientos(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico,0 , util.BackgroundTasks.EnvioMovimientos.MODOS_TRANSMISION.TRANSMITIR_RECORDSET_OCULTO, new System.Collections.Generic.List<util.BackgroundTasks.EnvioMovimientos.MOVIMIENTO_SELECCIONADO>());
            int xMin = Int16.Parse("0" + Funciones.modINIs.ReadINI("DATOS", "DelayedEnv", "2"));
            envioMovs.delayedRun(xMin * 60 * 1000);
        }

        private static void ActivarApplicacion()
        {

            util.BackgroundTasks.Updater updater = new util.BackgroundTasks.Updater(
               util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico,
               util.BackgroundTasks.Updater.UpdateType.ActivarApp);
            updater.run();

            //OleDbDataReader dr = null;
            //dr = Funciones.oleDbFunciones.Comando(Global01.Conexion, "SELECT * FROM v_appConfig2");
            //if (!dr.HasRows)
            //{
            //    MessageBox.Show(new Form() { TopMost = true }, "Aplicación NO inicializada! (error=Version y Tipo), Comuniquese con auto náutica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    miEnd();
            //}
            //else
            //{
            //    dr.Read();

            //    if (dr["appCVersion"].ToString().Substring(1, 3) != Global01.VersionApp.Substring(3, 3))
            //    {
            //        MessageBox.Show(new Form() { TopMost = true }, "INCONSISTENCIA en la versión de la Aplicación!, Comuniquese con auto náutica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //        miEnd();
            //    }
            //    else
            //    {
            //        Global01.URL_ANS = Funciones.modINIs.ReadINI("DATOS", "IP", "0.0.0.0");
            //        if (Global01.URL_ANS == "0.0.0.0")
            //        {
            //            Global01.URL_ANS = DBNull.Value.Equals(dr["url"]) ? "0.0.0.0" : dr["url"].ToString();
            //        }

            //        Global01.URL_ANS2 = Funciones.modINIs.ReadINI("DATOS", "IP2", "0.0.0.0");
            //        if (Global01.URL_ANS2 == "0.0.0.0")
            //        {
            //            Global01.URL_ANS2 = DBNull.Value.Equals(dr["url2"]) ? "0.0.0.0" : dr["url2"].ToString();
            //        }

            //        Global01.proxyServerAddress = Funciones.modINIs.ReadINI("DATOS", "ProxyServer", "0.0.0.0");

            //        util.BackgroundTasks.Updater updater = new util.BackgroundTasks.Updater(
            //           util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico,
            //           util.BackgroundTasks.Updater.UpdateType.ActivarApp);
            //        updater.run();
            //    }
            //}
            //dr = null;
        }

        private static void load_header(byte Paso)
        {
            //- acá sigo con el código de main --
            OleDbDataReader dr = null;
            dr = Funciones.oleDbFunciones.Comando(Global01.Conexion, "SELECT * FROM v_appConfig2");
            if (!dr.HasRows)
            {
                MessageBox.Show(new Form() { TopMost = true },"Aplicación NO inicializada! (error=Version y Tipo), Comuniquese con auto náutica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }
            else
            {
                dr.Read();

                if (dr["appCVersion"].ToString().Substring(1, 3) != Global01.VersionApp.Substring(3, 3))
                {
                    MessageBox.Show(new Form() { TopMost = true },"INCONSISTENCIA en la versión de la Aplicación!, Comuniquese con auto náutica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    miEnd();
                }

                Global01.MiBuild = DBNull.Value.Equals(dr["Build"]) ? (int)(0) : Int32.Parse(dr["Build"].ToString());

                Global01.URL_ANS = Funciones.modINIs.ReadINI("DATOS", "IP", "0.0.0.0");
                if (Global01.URL_ANS == "0.0.0.0")
                {
                    Global01.URL_ANS = DBNull.Value.Equals(dr["url"]) ? "0.0.0.0" : dr["url"].ToString();
                }

                Global01.URL_ANS2 = Funciones.modINIs.ReadINI("DATOS", "IP2", "0.0.0.0");
                if (Global01.URL_ANS2 == "0.0.0.0")
                {
                    Global01.URL_ANS2 = DBNull.Value.Equals(dr["url2"]) ? "0.0.0.0" : dr["url2"].ToString();
                }

                Global01.proxyServerAddress = Funciones.modINIs.ReadINI("DATOS", "ProxyServer", "0.0.0.0");

                if (Paso == 1)
                {
                    Global01.ListaPrecio = DBNull.Value.Equals(dr["appCListaPrecio"]) ? (byte)(0) : (byte)(dr["appCListaPrecio"]);
                    //Global01.MiBuild = DBNull.Value.Equals(dr["Build"]) ? (int)(0) : Int32.Parse(dr["Build"].ToString());
                    Global01.NroUsuario = DBNull.Value.Equals(dr["IDAns"]) ? "00000" : dr["IDAns"].ToString();
                    Global01.Zona = DBNull.Value.Equals(dr["Zona"]) ? "000" : dr["Zona"].ToString();
                    Global01.Cuit = DBNull.Value.Equals(dr["Cuit"]) ? "0" : dr["Cuit"].ToString();
                    Global01.dbCaduca = DBNull.Value.Equals(dr["dbCaduca"]) ? DateTime.Parse("01/01/1900") : DateTime.Parse(dr["dbCaduca"].ToString());
                    Global01.appCaduca = DBNull.Value.Equals(dr["appCaduca"]) ? DateTime.Parse("01/01/1900") : DateTime.Parse(dr["appCaduca"].ToString());
                    Global01.F_ActCatalogo = DBNull.Value.Equals(dr["F_ActCatalogo"]) ? DateTime.Parse("01/01/1900") : DateTime.Parse(dr["F_ActCatalogo"].ToString());
                    Global01.F_ActClientes = DBNull.Value.Equals(dr["F_ActClientes"]) ? DateTime.Parse("01/01/1900") : DateTime.Parse(dr["F_ActClientes"].ToString());
                    Global01.F_UltimoAcceso = DBNull.Value.Equals(dr["FechaUltimoAcceso"]) ? DateTime.Today.Date : DateTime.Parse(dr["FechaUltimoAcceso"].ToString());
                    Global01.EnviarAuditoria = DBNull.Value.Equals(dr["EnviarAuditoria"]) ? (bool)(true) : (bool)(dr["EnviarAuditoria"]);
                    Global01.AuditarProceso = DBNull.Value.Equals(dr["Auditor"]) ? (bool)(true) : (bool)(dr["Auditor"]);

                    Global01.RazonSocial = DBNull.Value.Equals(dr["RazonSocial"]) ? "" : dr["RazonSocial"].ToString();
                    Global01.ApellidoNombre = DBNull.Value.Equals(dr["ApellidoNombre"]) ? "" : dr["ApellidoNombre"].ToString();

                    Global01.pin = DBNull.Value.Equals(dr["PIN"]) ? "" : dr["PIN"].ToString();
                    Global01.EmailTO = DBNull.Value.Equals(dr["Email"]) ? "" : dr["Email"].ToString();

                    //Global01.Domicilio = DBNull.Value.Equals(dr["Domicilio"]) ? "" : dr["Domicilio"];
                    //Global01.Ciudad = DBNull.Value.Equals(dr["Ciudad"]) ? "" : dr["Ciudad"];

                    valida_header();
                }
                else
                {
                    if (Global01.Conexion.State == ConnectionState.Open) { Global01.Conexion.Close(); }
                }
            }
            dr = null;
        }

        private static void valida_header()
        {
            //------ otros chequeos ---------
            //OJO verificar cuit<> 1 or len(cuit) < 11 or idans nulo

            if (DateTime.Today.Date > Global01.appCaduca.Date)
            {
                MessageBox.Show(new Form() { TopMost = true },"El uso de la aplicación EXPIRO!, Comuniquese con auto náutica sur", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }

            if (Global01.F_UltimoAcceso.Date > DateTime.Today.Date)
            {
                MessageBox.Show(new Form() { TopMost = true },"Error con la hora del sistema, Comuniquese con auto náutica sur", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }
            else
            {
                Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_UltimoAcceso_upd");
            }

            if (Int32.Parse(Global01.NroUsuario.ToString()) <= 0 | Int64.Parse(Global01.Cuit.ToString().Replace("-", "")) <= 1)
            {
                MessageBox.Show(new Form() { TopMost = true },"Error en nº de Cuenta ó Cuit, Comuniquese con auto náutica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Global01.IDMaquinaCRC = Catalogo._registro.AppRegistro.ObtenerCRC(Global01.IDMaquina);
                Funciones.modINIs.DeleteKeyINI("DATOS", "MachineId");
                Funciones.modINIs.DeleteKeyINI("DATOS", "RegistrationKey");
                Funciones.modINIs.WriteINI("DATOS", "MachineId", Global01.IDMaquinaCRC);
                miEnd();
            }

            if (DateTime.Today.Date > Global01.dbCaduca.Date)
            {
                MessageBox.Show(new Form() { TopMost = true },"La vigencia del Catálogo EXPIRO!, debe actualizar por internet o comuniquese con su viajante", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (DateTime.Today.Date > Global01.dbCaduca.Date.AddDays(3).Date)
                {
                    MessageBox.Show(new Form() { TopMost = true },"Quedan menos de 3 días para la validez del Catálogo, debe actualizar por internet o comuniquese con su viajante", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        public static void update_productos()
        {
            Catalogo.varios.fDataUpdate fu = new Catalogo.varios.fDataUpdate();
            fu.SoloCatalogo = Convert.ToBoolean(Funciones.modINIs.ReadINI("DATOS", "SoloCatalogo", "false"));
            string ipAddress = Global01.URL_ANS;

        VadeNuevo:
            fu.Url = ipAddress;
            fu.ShowDialog();

            if (Global01.xError)
            {
                Global01.xError = false;

                if (MessageBox.Show(new Form() { TopMost = true },"Error de Conexión al Servidor, ¿quiere intentar de nuevo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ipAddress = Global01.URL_ANS2;
                    goto VadeNuevo;
                }
            }
            fu.Dispose();
            fu = null;
        }

        private static void valida_appLogin()
        {
            Catalogo.varios.fLogin f = new Catalogo.varios.fLogin();
            f.ShowDialog();
            if (!f.TodoBien)
            {
                f.Dispose();
                miEnd();
            }
            f.Dispose();
            f = null;
        }

        internal static void valida_appRegistro()
        {
            //- Registro y activación -------------XX
        AcaRegistro:
            if (!Catalogo._registro.AppRegistro.ValidateRegistration(Global01.IDMaquinaREG))
            //if (false)
            {
                if (Global01.IDMaquinaCRC == "no")
                {
                    // Genera nueva Instalacion ID y va a Registro
                    Global01.IDMaquinaCRC = Catalogo._registro.AppRegistro.ObtenerCRC(Global01.IDMaquina);
                    Funciones.modINIs.DeleteKeyINI("DATOS", "MachineId");
                    Funciones.modINIs.DeleteKeyINI("DATOS", "RegistrationKey");
                    Funciones.modINIs.WriteINI("DATOS", "MachineId", Global01.IDMaquinaCRC);
                    Global01.RecienRegistrado = false;

                    // a registrar
                    Catalogo._registro.fRegistro fRegistro = new Catalogo._registro.fRegistro();
                    fRegistro.ShowDialog();
                    fRegistro = null;

                    if (!Global01.RecienRegistrado)
                    {
                        Funciones.modINIs.DeleteKeyINI("DATOS", "MachineId");
                        Funciones.modINIs.DeleteKeyINI("DATOS", "RegistrationKey");
                        Global01.IDMaquinaCRC = "no";
                        miEnd();
                    }
                    else
                    {
                        MessageBox.Show(new Form() { TopMost = true },"BIENVENIDO A NUESTRO CATALOGO!.", "REGISTRADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        goto AcaRegistro;
                    }
                }
                else
                {
                    if (!Catalogo._registro.AppRegistro.ValidateMachineId(Global01.IDMaquinaCRC))
                    {
                        Funciones.modINIs.DeleteKeyINI("DATOS", "MachineId");
                        Funciones.modINIs.DeleteKeyINI("DATOS", "RegistrationKey");
                        Global01.IDMaquinaCRC = "no";
                        MessageBox.Show(new Form() { TopMost = true },"Código de registro adulterado \r\n Ahora debe registrar la aplicación nuevamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        goto AcaRegistro;
                    }
                    else
                    {
                        // App Registro OK - me fijo si está activada -
                        if (Global01.LLaveViajante == "no")
                        {
                            Funciones.modINIs.DeleteKeyINI("DATOS", "MachineId");
                            Funciones.modINIs.DeleteKeyINI("DATOS", "RegistrationKey");
                            Global01.IDMaquinaCRC = "no";
                            MessageBox.Show(new Form() { TopMost = true },"Código de registro adulterado \r\n Ahora debe registrar la aplicación nuevamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto AcaRegistro;
                        }
                        else
                        {
                            Global01.AppActiva = false;
                            if (MessageBox.Show(new Form() { TopMost = true },"¿ Desea ACTIVAR la aplicación ahora ? \r\n si la aplicación no se activa, NO se pueden realizar actualizaciones \r\n \r\n - DEBE ESTAR CONECTADO A INTERNET -", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                ActivarApplicacion();
                                goto AcaRegistro;
                            }
                        }
                    }
                }
            }
            else
            {
                // registrada y activa
                Global01.AppActiva = true;
                //-------- BORRAR ESTA LINEA!!!!!!! ----------------------
                //Sabor3 = 
                //Global01.IDMaquina = "391887A0B0AC683CDB99E45117855B0CE";
                //Sabor2 = 
                //Global01.IDMaquina = "291887A0B0AC683CDB99E45117855B0CE";

            }
            //--------------------------------------XX
        }

        private static void update_mdb()
        {
            //--- Verifico si hay actualización de Emergencia de la MDB - Update MDB ----------
            if (Funciones.modINIs.ReadINI("UPDATE", "mdb") == "up201406")
            {                
                System.IO.File.Copy(Global01.AppPath + "\\Datos\\Catalogo.mdb", Global01.AppPath + "\\Datos\\" + Global01.FileBak, false);
                Application.DoEvents();

                System.IO.File.Copy(Global01.AppPath + "\\Reportes\\Catalogo.mdb", Global01.AppPath + "\\Datos\\Catalogo.mdb", true);
                Application.DoEvents();

                Funciones.updateMDB.Emergencia(Global01.FileBak);
            }
            //--- termina update de emergencia ----------------------------------------------
        }

        private static void valida_ubicacionDatos()
        {
            //--- Pregunta ¿ Está todo en su lugar ? ----------------------
            if (PrevInstance())
            {
                MessageBox.Show(new Form() { TopMost = true },"Hay otra instancia de la aplicación abierta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }
            
            if (!System.IO.File.Exists(Global01.sstring))
            {
                MessageBox.Show(new Form() { TopMost = true }, "Error en la instalación del archivo de Seguridad", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }

            if (!System.IO.File.Exists(Global01.cstring))
            {
                MessageBox.Show(new Form() { TopMost = true }, "Error en la instalación del archivo de Datos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }

            if (!System.IO.File.Exists(Global01.dstring))
            {
                if (Funciones.modINIs.ReadINI("UPDATE", "mdb") == "new201406")
                {
                    Funciones.modINIs.DeleteKeyINI("UPDATE", "mdb");
                    if (System.IO.File.Exists(Global01.AppPath + "\\Reportes\\Catalogo.mdb"))
                    {                    
                        System.IO.File.Copy(Global01.AppPath + "\\Reportes\\Catalogo.mdb", Global01.AppPath + "\\Datos\\Catalogo.mdb", true);
                        System.IO.File.Delete(Global01.AppPath + "\\Reportes\\Catalogo.mdb");
                        System.IO.File.Delete(Global01.AppPath + "\\up201406.exe");
                    }
                    //Global01.IDMaquinaCRC = Catalogo._registro.AppRegistro.ObtenerCRC(Global01.IDMaquina);
                    Funciones.modINIs.DeleteKeyINI("DATOS", "MachineId");
                    Funciones.modINIs.DeleteKeyINI("DATOS", "RegistrationKey");
                    //Funciones.modINIs.WriteINI("DATOS", "MachineId", Global01.IDMaquinaCRC);
                }
                else
                {
                    MessageBox.Show(new Form() { TopMost = true }, "Error en la instalación del archivo Catalogo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    miEnd();
                }
            }
            //--- Fin de Pregunta -------------------------------

        }

        public static void miEnd(bool Halt=true)
        {
            Catalogo.util.BackgroundTasks.BackgroundTaskCoordinator.instance.shutdownTasks();
            Application.Exit();
            if (Halt)           
            { 
                System.Environment.Exit(0);
            }
            //System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private static bool PrevInstance()
        {
            //get the name of current process, i,e the process 
            //name of this current application

            string currPrsName = Process.GetCurrentProcess().ProcessName;

            //Get the name of all processes having the 
            //same name as this process name 
            Process[] allProcessWithThisName = Process.GetProcessesByName(currPrsName);

            //if more than one process is running return true.
            //which means already previous instance of the application 
            //is running
            if (allProcessWithThisName.Length > 1)
            {
                return true; // Yes Previous Instance Exist
            }
            else
            {
                return false; //No Prev Instance Running
            }

            //Alternate way using Mutex
            //using System.Threading;
            //bool appNewInstance;

            //Mutex m = new Mutex(true, "ApplicationName", out appNewInstance);

            //if (!appNewInstance)
            //{
            //    // Already Once Instance Running
            //    MessageBox.Show(new Form() { TopMost = true },"One Instance Of This App Allowed.");
            //    return;
            //}
            //GC.KeepAlive(m);
        }

        public static void inicializaGlobales()
        {
           //xxxSabor
            Global01.miSABOR = Global01.TiposDeCatalogo.Viajante;
           // Global01.miSABOR = Global01.TiposDeCatalogo.Cliente;
 
            Global01.NoConn = false;
            Global01.VersionApp = (int)(Global01.miSABOR) + "." + Application.ProductVersion.Trim();

            Global01.Conexion = null;
            //Global01.TranActiva_ = null;

            string xLocAns = Environment.GetEnvironmentVariable("windir") + "\\locans" + ((int)(Global01.miSABOR)).ToString() + ".log";
        vadenuevo:
            if (!System.IO.File.Exists(xLocAns))
            {
                Funciones.modINIs.INIWrite(xLocAns, "ans", "path", "C:\\Catalogo ANS");
            }
            Global01.AppPath = Funciones.modINIs.INIRead(xLocAns, "ans", "path", "C:\\Catalogo ANS");

            Global01.PathAcrobat = Funciones.modINIs.ReadINI("Datos", "PathAcrobat", "");
            Global01.FileBak = "CopiaCata_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".mdb";

            Global01.cstring = Global01.AppPath + "\\datos\\ans.mdb";
            Global01.dstring = Global01.AppPath + "\\datos\\catalogo.mdb";

            //Global01.sstring = Environment.GetEnvironmentVariable("windir") + "\\Help\\KbAppCat.hlp";
            Global01.sstring = Application.StartupPath.ToString().Trim() + "\\AvallonDock.resources.dll";

            if (!System.IO.File.Exists(Global01.dstring))
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();

                folderBrowserDialog1.Description = "Seleccione la ubicación donde está instalado el Catálogo de Auto Náutica Sur.";
                folderBrowserDialog1.ShowNewFolderButton = false;
                folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Funciones.modINIs.INIWrite(xLocAns, "ans", "path", folderBrowserDialog1.SelectedPath.ToString());
                }
                else if (result == DialogResult.Cancel)
                {
                    miEnd();
                }
                goto vadenuevo;
            }

            Global01.strConexionUs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Global01.dstring + Global01.up2014Us + ";Persist Security Info=True;Jet OLEDB:System database=" + Global01.sstring;
            Global01.strConexionAd = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Global01.dstring + Global01.up2014Ad + ";Persist Security Info=True;Jet OLEDB:System database=" + Global01.sstring;

            Global01.IDMaquina = Catalogo._registro.AppRegistro.ObtenerIDMaquina();
            Global01.IDMaquinaCRC = Funciones.modINIs.ReadINI("DATOS", "MachineId", "no");
            Global01.LLaveViajante = Funciones.modINIs.ReadINI("DATOS", "LlaveViajante", "no");
            Global01.IDMaquinaREG = Funciones.modINIs.ReadINI("DATOS", "RegistrationKey", "no");
            Global01.RecienRegistrado = false;
            Global01.AppActiva = false;

            Global01.OperacionActivada = "nada";
            Global01.NroDocumentoAbierto = "";
            Global01.NroUsuario = "00000";
            Global01.Zona = "000";
            Global01.Cuit = "0";
            Global01.pin = "";
            Global01.RazonSocial = "";
            Global01.ApellidoNombre = "";

            Global01.dbCaduca = DateTime.Today.Date;
            Global01.appCaduca = DateTime.Today.Date;
            Global01.F_ActCatalogo = DateTime.Today.Date;
            Global01.F_ActClientes = DateTime.Today.Date;
            Global01.F_UltimoAcceso = DateTime.Today.Date;

            Global01.EnviarAuditoria = false;
            Global01.AuditarProceso = false;
            Global01.xError = false;
            Global01.URL_ANS = "0.0.0.0";
            Global01.URL_ANS2 = "0.0.0.0";
            Global01.MainWindowCaption = "Catálogo Dígital de Productos - v4.0";

            Global01.MiBuild = 0;
            Global01.ListaPrecio = 1;
            Global01.Dolar = float.Parse(Funciones.modINIs.INIRead(Global01.AppPath + "\\Cambio.ini", "General", "Dolar", "1"));
            Global01.Euro = float.Parse(Funciones.modINIs.INIRead(Global01.AppPath + "\\Cambio.ini", "General", "Euro", "1"));

            Global01.EmailTO = "";
            Global01.EmailBody = "";
            Global01.EmailAsunto = "";

            Global01.IPPing = Funciones.modINIs.ReadINI("DATOS", "IPPing", "8.8.8.8");
        }

        /// <summary>
        /// <see cref="http://stackoverflow.com/questions/5039636/how-to-check-if-a-particular-version-of-flash-player-is-installed-or-not-in-c"/>
        /// <seealso cref="http://stackoverflow.com/questions/4656814/how-can-i-make-my-application-check-if-adobe-flash-player-is-installed-on-a-pc"/>
        /// </summary>
        internal static void checkFlashPlayer()
        {
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Macromedia\FlashPlayer");
            if (regKey != null)
            {
                string flashVersion = Convert.ToString(regKey.GetValue("CurrentVersion"));
                //                return flashVersion;
                System.Diagnostics.Debug.WriteLine(flashVersion);
            }
            else
            {
                System.Windows.Forms.MessageBox.Show(new Form() { TopMost = true },"Debe tener instalado Flash Player", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                miEnd();
            }
            // otra version que no anduvo en mi maquina.
            /*
            Microsoft.Win32.RegistryKey rk=Microsoft.Win32.Registry.CurrentUser.OpenSubKey("HKEY_LOCAL_MACHINE\\SOFTWARE\\Macromedia\\FlashPlayer");
            if (rk == null)
            {
                System.Windows.Forms.MessageBox.Show(new Form() { TopMost = true },"Debe tener instalado Flash Player", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                miEnd();
            }*/
        }

    }
}