using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace Catalogo
{
    static class MainMod
    {

        private const string m_sMODULENAME_ = "MainMod";

        public static void Main()
        {
            const string PROCNAME_ = "Main";

            Global01.miSABOR = Global01.TiposDeCatalogo.Viajante;
            Global01.NoConn = false;

            Global01.Conexion = null;
            Global01.TranActiva = null;

            Global01.AppPath = Funciones.modINIs.ReadINI("Datos", "Path", System.IO.Directory.GetCurrentDirectory());
            Global01.PathAcrobat = Funciones.modINIs.ReadINI("Datos", "PathAcrobat", "");
            Global01.FileBak = "CopiaCata_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".mdb";

            Global01.cstring = Global01.AppPath + "\\datos\\ans.mdb";
            Global01.dstring = Global01.AppPath + "\\datos\\catalogo.mdb";
            Global01.sstring = Environment.GetEnvironmentVariable("windir") + "\\Help\\KbAppCat.hlp";

            Global01.strConexionUs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Global01.dstring + Global01.up2014Us + ";Persist Security Info=True;Jet OLEDB:System database=" + Global01.sstring;
            Global01.strConexionAd = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Global01.dstring + Global01.up2014Ad + ";jet oledb:system database=" + Global01.sstring;

            Global01.IDMaquina = Catalogo._registro.AppRegistro.ObtenerIDMaquina();
            Global01.IDMaquinaCRC = Funciones.modINIs.ReadINI("DATOS", "MachineId", "no");
            Global01.LLaveViajante = Funciones.modINIs.ReadINI("DATOS", "LlaveViajante", "no");
            Global01.IDMaquinaREG = Funciones.modINIs.ReadINI("DATOS", "RegistrationKey", "no");
            Global01.RecienRegistrado = false;
            Global01.AppActiva = false;

            Global01.NroUsuario = "0";
            Global01.Cuit = "0";

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

            //--- Pregunta ¿ Está todo en su lugar ? ----------------------
            if (PrevInstance())
            {
                MessageBox.Show("Hay otra instancia de la aplicación abierta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            };

            if (!System.IO.File.Exists(Global01.dstring))
            {
                //Cursor.Current = Cursors.Default;
                MessageBox.Show("Error en la instalación del archivo Catalogo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }

            if (!System.IO.File.Exists(Global01.cstring))
            {
                MessageBox.Show("Error en la instalación del archivo de Datos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }


            if (!System.IO.File.Exists(Global01.sstring))
            {
                MessageBox.Show("Error en la instalación del archivo de Seguridad", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }
            //--- Fin de Pregunta -------------------------------

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

            //--- actualiza links tables ----------------------------------------------------
            Funciones.oleDbFunciones.CambiarLinks("ans.mdb");

            //--- compactar MDB--------------------------------------------------------------
            // -- OJO! Funciones.oleDbFunciones.CompactDatabase("catalogo.mdb");



            //-- Instancia la Conexión y un DataReader Global a la Rutina Main --
            Global01.Conexion = Funciones.oleDbFunciones.GetConn(Catalogo.Global01.strConexionUs);

            OleDbDataReader dr = null;

            //--- Verifico que la version de la MDB y del Ejecutable sean compatible --------
            dr = Funciones.oleDbFunciones.Comando(ref Global01.Conexion, "SELECT * FROM v_appConfig2");
            if (!dr.HasRows)
            {
                MessageBox.Show("Aplicación NO inicializada! (error=Version y Tipo), Comuniquese con auto náutica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }
            else
            {
                dr.Read();


                //If Mid(dr[appCVersion, 1, 3) <> Application.ProductVersion.Substring(1, 3) Then
                //acá pablo
                if (false)
                {
                    MessageBox.Show("INCONSISTENCIA en la versión de la Aplicación!, Comuniquese con auto náutica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    miEnd();
                }
                Global01.URL_ANS = Funciones.modINIs.ReadINI("DATOS", "IP");
                if (Global01.URL_ANS != "0.0.0.0") { Global01.URL_ANS = DBNull.Value.Equals(dr["url"]) ? "0.0.0.0" : dr["url"].ToString(); }

                Global01.ListaPrecio = DBNull.Value.Equals(dr["appCListaPrecio"]) ? (byte)(0) : (byte)(dr["appCListaPrecio"]);
                Global01.MiBuild = DBNull.Value.Equals(dr["Build"]) ? (int)(0) : Int32.Parse(dr["Build"].ToString());
                Global01.URL_ANS2 = DBNull.Value.Equals(dr["url2"]) ? "0.0.0.0" : dr["url2"].ToString();

            }
            dr = null;
            if (Global01.Conexion.State == ConnectionState.Open) { Global01.Conexion.Close(); };
        //--- Fin chequeo de Version --------

        
            //- Registro y activación -------------XX
        AcaRegistro:
            if (!Catalogo._registro.AppRegistro.ValidateRegistration(Global01.IDMaquinaREG))
            {
                if (Global01.IDMaquinaCRC == "no")
                {
                    // Genera nueva Instalacion ID y va a Registro
                    Global01.IDMaquinaCRC = Catalogo._registro.AppRegistro.ObtenerCRC(ref Global01.IDMaquina);
                    Funciones.modINIs.DeleteKeyINI("DATOS", "MachineId");
                    Funciones.modINIs.DeleteKeyINI("DATOS", "RegistrationKey");
                    Funciones.modINIs.WriteINI("DATOS", "MachineId", Global01.IDMaquinaCRC);

                    // a registrar
                    Registration fRegistro = new Registration();
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
                        MessageBox.Show("BIENVENDO A NUESTRO CATALOGO!.", "REGISTRADO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        goto AcaRegistro;
                    };
                }
                else
                {
                    if (!Catalogo._registro.AppRegistro.ValidateMachineId(Global01.IDMaquinaCRC))
                    {
                        Funciones.modINIs.DeleteKeyINI("DATOS", "MachineId");
                        Funciones.modINIs.DeleteKeyINI("DATOS", "RegistrationKey");
                        Global01.IDMaquinaCRC = "no";
                        MessageBox.Show("Código de registro adulterado \r\n Ahora debe registrar la aplicación nuevamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            MessageBox.Show("Código de registro adulterado \r\n Ahora debe registrar la aplicación nuevamente", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            goto AcaRegistro;
                        }
                        else
                        {
                            if (MessageBox.Show("¿ Desea ACTIVAR la aplicación ahora ? \r\n si la aplicación no se activa, NO se pueden realizar actualizaciones \r\n \r\n - DEBE ESTAR CONECTADO A INTERNET -", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                //ActivarApplicacion();
                                //Dim dlg As New frmConexionUpdate

                                //dlg.modoUpdate = ActivarApp
                                //dlg.Show vbModal
                            };
                        };
                    };
                };
            }
            else
            {
                // registrada y activa
                Global01.AppActiva = true;
            };
            //--------------------------------------XX

            if (Global01.AppActiva)
            {
                Catalogo.util.fDataUpdate fu = new Catalogo.util.fDataUpdate();

            VadeNuevo:
                fu.SoloCatalogo = Convert.ToBoolean(Funciones.modINIs.ReadINI("DATOS", "SoloCatalogo", "false"));
                fu.Url = Global01.URL_ANS;
                fu.ShowDialog();
                fu.Dispose();

                if (Global01.xError)
                {
                    Global01.xError = false;

                    if (MessageBox.Show("Error de Conexión al Servidor, ¿quiere intentar de nuevo?", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Global01.URL_ANS = Global01.URL_ANS2;
                        goto VadeNuevo;
                    }
                }
                fu = null;
            };

            //- acá sigo con el código de main --
            dr = Funciones.oleDbFunciones.Comando(ref Global01.Conexion, "SELECT * FROM v_appConfig2");
            if (!dr.HasRows)
            {
                MessageBox.Show("Aplicación NO inicializada! (error=Version y Tipo), Comuniquese con auto náutica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }
            else
            {
                dr.Read();

                Global01.URL_ANS = Funciones.modINIs.ReadINI("DATOS", "IP");
                if (Global01.URL_ANS != "0.0.0.0") { Global01.URL_ANS = DBNull.Value.Equals(dr["url"]) ? "0.0.0.0" : dr["url"].ToString(); }

                Global01.ListaPrecio = DBNull.Value.Equals(dr["appCListaPrecio"]) ? (byte)(0) : (byte)(dr["appCListaPrecio"]);
                Global01.MiBuild = DBNull.Value.Equals(dr["Build"]) ? (int)(0) : Int32.Parse(dr["Build"].ToString());
                Global01.URL_ANS2 = DBNull.Value.Equals(dr["url2"]) ? "0.0.0.0" : dr["url2"].ToString();
                Global01.NroUsuario = DBNull.Value.Equals(dr["IDAns"]) ? "00000" : dr["IDAns"].ToString();
                Global01.Cuit = DBNull.Value.Equals(dr["Cuit"]) ? "0" : dr["Cuit"].ToString();
                Global01.dbCaduca = DBNull.Value.Equals(dr["dbCaduca"]) ? DateTime.Parse("01/01/1900") : DateTime.Parse(dr["dbCaduca"].ToString());
                Global01.appCaduca = DBNull.Value.Equals(dr["appCaduca"]) ? DateTime.Parse("01/01/1900") : DateTime.Parse(dr["appCaduca"].ToString());
                Global01.F_ActCatalogo = DBNull.Value.Equals(dr["F_ActCatalogo"]) ? DateTime.Parse("01/01/1900") : DateTime.Parse(dr["F_ActCatalogo"].ToString());
                Global01.F_ActClientes = DBNull.Value.Equals(dr["F_ActClientes"]) ? DateTime.Parse("01/01/1900") : DateTime.Parse(dr["F_ActClientes"].ToString());
                Global01.F_UltimoAcceso = DBNull.Value.Equals(dr["FechaUltimoAcceso"]) ? DateTime.Today.Date : DateTime.Parse(dr["FechaUltimoAcceso"].ToString());
                Global01.EnviarAuditoria = DBNull.Value.Equals(dr["EnviarAuditoria"]) ? (bool)(true) : (bool)(dr["EnviarAuditoria"]);
                Global01.AuditarProceso = DBNull.Value.Equals(dr["Auditor"]) ? (bool)(true) : (bool)(dr["Auditor"]);

                //Global01.RazonSocial = dr.IsDBNull(0) ? "" : dr["RazonSocial"];
                //Global01.ApellidoNombre = DBNull.Value.Equals(dr["ApellidoNombre"]) ? "" : dr["ApellidoNombre"];
                //Global01.Domicilio = DBNull.Value.Equals(dr["Domicilio"]) ? "" : dr["Domicilio"];
                //Global01.Ciudad = DBNull.Value.Equals(dr["Ciudad"]) ? "" : dr["Ciudad"];
                //Global01.Email = DBNull.Value.Equals(dr["Email"]) ? "" : dr["Email"];

            }
            dr = null;
            if (Global01.Conexion.State == ConnectionState.Open) { Global01.Conexion.Close(); };

            //------ otros chequeos ---------
            //OJO verificar cuit<> 1 or len(cuit) < 11 or idans nulo

            if (Int32.Parse(Global01.NroUsuario.ToString()) <= 0 || Int64.Parse(Global01.Cuit.ToString()) <= 1)
            {
                MessageBox.Show("Error en nº de Cuenta ó Cuit, Comuniquese con auto náutica sur", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            };

            if (Global01.F_UltimoAcceso.Date > DateTime.Today.Date)
            {
                MessageBox.Show("Error con la hora del sistema, Comuniquese con auto náutica sur", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            }
            else
            {
                Funciones.oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC usp_UltimoAcceso_upd");
            };

            if (DateTime.Today.Date > Global01.appCaduca.Date)
            {
                MessageBox.Show("El uso de la aplicación EXPIRO!, Comuniquese con auto náutica sur", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                miEnd();
            };

            if (DateTime.Today.Date > Global01.dbCaduca.Date)
            {
                MessageBox.Show("La vigencia del Catálogo EXPIRO!, debe actualizar por internet o comuniquese con su viajante", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (DateTime.Today.Date > Global01.dbCaduca.Date.AddDays(3).Date)
                {
                    MessageBox.Show("Quedan menos de 3 días para la validez del Catálogo, debe actualizar por internet o comuniquese con su viajante", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                };
            };

            //- fin código main -----------------
            preload.Preloader.instance.refresh();
            System.Windows.Forms.MessageBox.Show("Aca estoy cargando los datos en segundo plano");
        }
        // - Fin Main ---

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
            Process[] allProcessWithThisName
                            = Process.GetProcessesByName(currPrsName);

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