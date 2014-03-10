using System;
using System.Diagnostics;

namespace Catalogo
{
    static class MainMod
    {

       private const string m_sMODULENAME_ = "MainMod";
         
        public static void Main()
        {
            const string PROCNAME_ = "Main";
       

        }

        public static bool PrevInstance()
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