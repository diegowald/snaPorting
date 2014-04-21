using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
//using System.Threading;

namespace Catalogo
{
    static class MainMod
    {

         public static void Main()
        {
            valida_ubicacionDatos();

            update_mdb();

            //--- actualiza links tables ----------------------------------------------------
            Funciones.oleDbFunciones.CambiarLinks("ans.mdb");

            //--- compactar MDB--------------------------------------------------------------
            Funciones.oleDbFunciones.CompactDatabase("catalogo.mdb");

            //-- Instancia la Conexi�n y un DataReader Global a la Rutina Main --
            Global01.Conexion = Funciones.oleDbFunciones.GetConn(Catalogo.Global01.strConexionUs);

            valida_appRegistro();

            // Carga tabla de Productos en Segundo Plano
            preload.Preloader.instance.refresh();

            load_header(); 
     
            //chequea comandos y mensajes desde el servidor
            if (Funciones.modINIs.ReadINI("DATOS", "INFO", "0") == "1") //Or vg.RecienRegistrado Or vg.NoConn
            {
                util.BackgroundTasks.Updater updater = new util.BackgroundTasks.Updater(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Asincronico, util.BackgroundTasks.Updater.UpdateType.UpdateAppConfig);
                updater.run();
            }

            valida_appLogin();

            if (Global01.AppActiva)
            {
                if (Global01.ActualizarClientes)
                {   
                    util.BackgroundTasks.Updater updater = new util.BackgroundTasks.Updater(util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico, util.BackgroundTasks.Updater.UpdateType.UpdateCuentas);
                    updater.run();
                }

                update_productos();
            }

            //- ACA ESTA LA PAPA ----------------------
            //- Run mi APP MainWindow -----------------
            //- ** RETORNA AL app.XAML y sigue la EJECUCION NORMAL ** ---
            //- Fin Main ---
        }

        private static void ActivarApplicacion()
        {
            OleDbDataReader dr = null;
            dr = Funciones.oleDbFunciones.Comando(Global01.Conexion, "SELECT * FROM v_appConfig2");
            if (!dr.HasRows)
            {
                MessageBox.Show("Aplicaci�n NO inicializada! (error=Version y Tipo), Comuniquese con auto n�utica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }
            else
            {
                dr.Read();

                if (dr["appCVersion"].ToString().Substring(1, 3) != Global01.VersionApp.Substring(3, 3))
                {
                    MessageBox.Show("INCONSISTENCIA en la versi�n de la Aplicaci�n!, Comuniquese con auto n�utica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    miEnd();
                }
                else
                {
                    Global01.URL_ANS = Funciones.modINIs.ReadINI("DATOS", "IP", "0.0.0.0");
                    if (Global01.URL_ANS != "0.0.0.0")
                    {
                        Global01.ipSettingIni = true;
                    }
                    else
                    { 
                        Global01.URL_ANS = DBNull.Value.Equals(dr["url"]) ? "0.0.0.0" : dr["url"].ToString(); 
                    }
                    Global01.URL_ANS2 = DBNull.Value.Equals(dr["url2"]) ? "0.0.0.0" : dr["url2"].ToString();
                    Global01.proxyServerAddress = Funciones.modINIs.ReadINI("DATOS", "ProxyServer", "0.0.0.0");
                }
            }

            util.BackgroundTasks.Updater updater = new util.BackgroundTasks.Updater(
               util.BackgroundTasks.BackgroundTaskBase.JOB_TYPE.Sincronico,
               util.BackgroundTasks.Updater.UpdateType.ActivarApp);
            updater.run();
        }

        private static void load_header()
        {
            //- ac� sigo con el c�digo de main --
            OleDbDataReader dr = null;
            dr = Funciones.oleDbFunciones.Comando(Global01.Conexion, "SELECT * FROM v_appConfig2");
            if (!dr.HasRows)
            {
                MessageBox.Show("Aplicaci�n NO inicializada! (error=Version y Tipo), Comuniquese con auto n�utica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }
            else
            {
                dr.Read();

                if (dr["appCVersion"].ToString().Substring(1,3)!=Global01.VersionApp.Substring(3,3))
                {
                    MessageBox.Show("INCONSISTENCIA en la versi�n de la Aplicaci�n!, Comuniquese con auto n�utica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    miEnd();
                }

                Global01.URL_ANS = Funciones.modINIs.ReadINI("DATOS", "IP", "0.0.0.0");
                if (Global01.URL_ANS != "0.0.0.0")
                {
                    Global01.ipSettingIni = true;
                }
                else
                {
                    Global01.URL_ANS = DBNull.Value.Equals(dr["url"]) ? "0.0.0.0" : dr["url"].ToString();
                }


                Global01.proxyServerAddress = Funciones.modINIs.ReadINI("DATOS", "ProxyServer", "0.0.0.0");

                Global01.ListaPrecio = DBNull.Value.Equals(dr["appCListaPrecio"]) ? (byte)(0) : (byte)(dr["appCListaPrecio"]);
                Global01.MiBuild = DBNull.Value.Equals(dr["Build"]) ? (int)(0) : Int32.Parse(dr["Build"].ToString());
                Global01.URL_ANS2 = DBNull.Value.Equals(dr["url2"]) ? "0.0.0.0" : dr["url2"].ToString();
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
                Global01.pin  = DBNull.Value.Equals(dr["PIN"]) ? "" : dr["PIN"].ToString();

                //Global01.Domicilio = DBNull.Value.Equals(dr["Domicilio"]) ? "" : dr["Domicilio"];
                //Global01.Ciudad = DBNull.Value.Equals(dr["Ciudad"]) ? "" : dr["Ciudad"];
                //Global01.Email = DBNull.Value.Equals(dr["Email"]) ? "" : dr["Email"];

                valida_header();

            }
            dr = null;
            //if (Global01.Conexion.State == ConnectionState.Open) { Global01.Conexion.Close(); }

        }

        private static void valida_header()
        {
            //------ otros chequeos ---------
            //OJO verificar cuit<> 1 or len(cuit) < 11 or idans nulo

            if (DateTime.Today.Date > Global01.appCaduca.Date)
            {
                MessageBox.Show("El uso de la aplicaci�n EXPIRO!, Comuniquese con auto n�utica sur", "Atenci�n", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }

            if (Global01.F_UltimoAcceso.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Error con la hora del sistema, Comuniquese con auto n�utica sur", "Atenci�n", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }
            else
            {
                Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_UltimoAcceso_upd");
            }

            if (Int32.Parse(Global01.NroUsuario.ToString()) <= 0 | Int64.Parse(Global01.Cuit.ToString().Replace("-","")) <= 1)
            {
                MessageBox.Show("Error en n� de Cuenta � Cuit, Comuniquese con auto n�utica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }

            if (DateTime.Today.Date > Global01.dbCaduca.Date)
            {
                MessageBox.Show("La vigencia del Cat�logo EXPIRO!, debe actualizar por internet o comuniquese con su viajante", "Atenci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (DateTime.Today.Date > Global01.dbCaduca.Date.AddDays(3).Date)
                {
                    MessageBox.Show("Quedan menos de 3 d�as para la validez del Cat�logo, debe actualizar por internet o comuniquese con su viajante", "Atenci�n", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        public static void update_productos()
        {
            Catalogo.varios.fDataUpdate fu = new Catalogo.varios.fDataUpdate();

        VadeNuevo:
            fu.SoloCatalogo = Convert.ToBoolean(Funciones.modINIs.ReadINI("DATOS", "SoloCatalogo", "false"));
            fu.Url = Global01.URL_ANS;
            fu.ShowDialog();
            fu.Dispose();

            if (Global01.xError)
            {
                Global01.xError = false;

                if (MessageBox.Show("Error de Conexi�n al Servidor, �quiere intentar de nuevo?", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Global01.URL_ANS = Global01.URL_ANS2;
                    goto VadeNuevo;
                }
            }
            fu = null;
        }

        private static void valida_appLogin()
        {
            Catalogo.varios.fLogin f = new Catalogo.varios.fLogin();
            f.ShowDialog();

            if (!f.TodoBien)
            {
                miEnd();
            }
        }

        private static void valida_appRegistro()
        {
 
            //- Registro y activaci�n -------------XX
        AcaRegistro:
            //if (!Catalogo._registro.AppRegistro.ValidateRegistration(Global01.IDMaquinaREG))
            if (false)
            {
                if (Global01.IDMaquinaCRC == "no")
                {
                    // Genera nueva Instalacion ID y va a Registro
                    Global01.IDMaquinaCRC = Catalogo._registro.AppRegistro.ObtenerCRC(Global01.IDMaquina);
                    Funciones.modINIs.DeleteKeyINI("DATOS", "MachineId");
                    Funciones.modINIs.DeleteKeyINI("DATOS", "RegistrationKey");
                    Funciones.modINIs.WriteINI("DATOS", "MachineId", Global01.IDMaquinaCRC);

                    // a registrar
                    Catalogo._registro.fRegistro  fRegistro = new Catalogo._registro.fRegistro() ;
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
                        MessageBox.Show("BIENVENIDO A NUESTRO CATALOGO!.", "REGISTRADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("C�digo de registro adulterado \r\n Ahora debe registrar la aplicaci�n nuevamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        goto AcaRegistro;
                    }
                    else
                    {
                        // App Registro OK - me fijo si est� activada -
                        if (Global01.LLaveViajante == "no")
                        {
                            Funciones.modINIs.DeleteKeyINI("DATOS", "MachineId");
                            Funciones.modINIs.DeleteKeyINI("DATOS", "RegistrationKey");
                            Global01.IDMaquinaCRC = "no";
                            MessageBox.Show("C�digo de registro adulterado \r\n Ahora debe registrar la aplicaci�n nuevamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto AcaRegistro;
                        }
                        else
                        {
                            Global01.AppActiva = false;          
                            if (MessageBox.Show("� Desea ACTIVAR la aplicaci�n ahora ? \r\n si la aplicaci�n no se activa, NO se pueden realizar actualizaciones \r\n \r\n - DEBE ESTAR CONECTADO A INTERNET -", "Atenci�n", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                ///// BORRAR ESTA LINEA!!!!!!!
                Global01.IDMaquina = "391887A0B0AC683CDB99E45117855B0CE";
            }
            //--------------------------------------XX
        }

        private static void update_mdb()
        {
            //--- Verifico si hay actualizaci�n de Emergencia de la MDB - Update MDB ----------
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
            //--- Pregunta � Est� todo en su lugar ? ----------------------
            if (PrevInstance())
            {
                MessageBox.Show("Hay otra instancia de la aplicaci�n abierta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }

            if (!System.IO.File.Exists(Global01.dstring))
            {
                //Cursor.Current = Cursors.Default;
                MessageBox.Show("Error en la instalaci�n del archivo Catalogo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }


            if (!System.IO.File.Exists(Global01.cstring))
            {
                MessageBox.Show("Error en la instalaci�n del archivo de Datos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }

            if (!System.IO.File.Exists(Global01.sstring))
            {
                MessageBox.Show("Error en la instalaci�n del archivo de Seguridad", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }
            //--- Fin de Pregunta -------------------------------

        }

        public static void inicializaGlobales()
        {

            //#if SaborViajante
            Global01.miSABOR = Global01.TiposDeCatalogo.Viajante;
            //#else
            //Global01.miSABOR = Global01.TiposDeCatalogo.Cliente;
            //#endif            
           

            Global01.NoConn = false;
            Global01.VersionApp = (int)(Global01.miSABOR) + ".3.2.0";

            Global01.Conexion = null;
            Global01.TranActiva = null;

        vadenuevo:
            if (!System.IO.File.Exists(Environment.GetEnvironmentVariable("windir") + "\\locans.log"))
            {
                Funciones.modINIs.INIWrite(Environment.GetEnvironmentVariable("windir") + "\\locans.log", "ans", "path", "C:\\Catalogo ANS"); 
            }
            Global01.AppPath = Funciones.modINIs.INIRead(Environment.GetEnvironmentVariable("windir") + "\\locans.log", "ans", "path", "C:\\Catalogo ANS");
          
            Global01.PathAcrobat = Funciones.modINIs.ReadINI("Datos", "PathAcrobat", "");
            Global01.FileBak = "CopiaCata_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".mdb";

            Global01.cstring = Global01.AppPath + "\\datos\\ans.mdb";
            Global01.dstring = Global01.AppPath + "\\datos\\catalogo.mdb";
            Global01.sstring = Environment.GetEnvironmentVariable("windir") + "\\Help\\KbAppCat.hlp";
   
            if (!System.IO.File.Exists(Global01.dstring))
            {             
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
                
                folderBrowserDialog1.Description = "Seleccione la ubicaci�n donde est� instalado el Cat�logo de Auto N�utica Sur.";
                folderBrowserDialog1.ShowNewFolderButton = false;
                folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyComputer;
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Funciones.modINIs.INIWrite(Environment.GetEnvironmentVariable("windir") + "\\locans.log", "ans", "path", folderBrowserDialog1.SelectedPath.ToString()); 
                }
                else if (result == DialogResult.Cancel)
                {
                   miEnd();
                }
                goto vadenuevo;
            }

            Global01.strConexionUs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Global01.dstring + Global01.up2014Us + ";Persist Security Info=True;Jet OLEDB:System database=" + Global01.sstring;
            Global01.strConexionAd = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Global01.dstring + Global01.up2014Ad + ";Persist Security Info=True;Jet OLEDB:System database=" + Global01.sstring;

            Global01.ipSettingIni = false;
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
            Global01.MainWindowCaption = "Cat�logo D�gital de Productos - v4.0";

            Global01.MiBuild = 0;
            Global01.ListaPrecio = 1;
            Global01.Dolar = float.Parse(Funciones.modINIs.INIRead(Global01.AppPath + "\\Cambio.ini", "General", "Dolar", "1"));
            Global01.Euro = float.Parse(Funciones.modINIs.INIRead(Global01.AppPath + "\\Cambio.ini", "General", "Euro", "1"));

            Global01.EmailTO = "";
            Global01.EmailBody = "";
            Global01.EmailAsunto = "";
        }

        public static void miEnd()
        {
            System.Environment.Exit(0);
            Application.Exit();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
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
            //    MessageBox.Show("One Instance Of This App Allowed.");
            //    return;
            //}
            //GC.KeepAlive(m);
        }

    }
}