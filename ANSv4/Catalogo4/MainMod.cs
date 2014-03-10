using System;
using System.Diagnostics;
using System.Windows.Forms;


            //string message = "You did not enter a server name. Cancel this operation?";
            //            string caption = "Error Detected in Input";
            //MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            //DialogResult result;

            //// Displays the MessageBox.

            //result = MessageBox.Show(message, caption, buttons);

            //if (result == System.Windows.Forms.DialogResult.Yes)
            //{

            //    // Closes the parent form.

            //    this.Close();

            //}

namespace Catalogo
{
    static class MainMod
    {

       private const string m_sMODULENAME_ = "MainMod";
         
        public static void Main()
        {
            const string PROCNAME_ = "Main";

            //--- Pregunta ¿ Está todo en su lugar ? ----------------------
            if (PrevInstance()) 
            {
                MessageBox.Show("Hay otra instancia de la aplicación abierta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);                
                miEnd();
            };

            if (!System.IO.File.Exists(Global01.dstring))
            {
                //Cursor.Current = Cursors.Default;
                MessageBox.Show("Error en la instalación del archivo Catalogo","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                miEnd();
            }

            if (!System.IO.File.Exists(Global01.cstring))
            {
                MessageBox.Show("Error en la instalación del archivo de Datos","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                miEnd();
            }
            

            if (!System.IO.File.Exists(Global01.sstring))
            {
                MessageBox.Show("Error en la instalación del archivo de Seguridad","ERROR",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                miEnd();
            }
            //--- Fin de Pregunta -------------------------------

            //--- Verifico si hay actualización de Emergencia de la MDB - Update MDB ----------
                if (Funciones.modINIs.ReadINI("UPDATE", "mdb")=="up201406")
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
            Funciones.oleDbFunciones.CompactDatabase("catalogo.mdb");

            

            Global01.Conexion = Funciones.oleDbFunciones.GetConn(Catalogo.Global01.strConexionUs);

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