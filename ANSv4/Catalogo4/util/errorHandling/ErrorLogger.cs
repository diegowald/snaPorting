using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.util.errorHandling
{
    public class ErrorLogger
    {
        public static String AppErrorLogFileName = "./Reportes/ans.log";

        /// <summary>
        /// Log any messages from the Application
        /// </summary>
        /// <param name="message"></param>
        public static void LogMessage(String pMessage)
        {
            bool Successful = false;

            for (int idx = 0; idx < 10; idx++)
            {
                try
                {
                    // Log message to default log file.
                    System.IO.StreamWriter str = new System.IO.StreamWriter(AppErrorLogFileName);

                    str.AutoFlush = true;   // Wri9te text with no buffering
                    str.WriteLine(new string('=', 40));
                    System.Diagnostics.Debug.WriteLine(new string('=', 40));
                    str.WriteLine("Time: " + DateTime.Now.ToString() +
                    Environment.NewLine
                        + "Message: " + pMessage);
                    System.Diagnostics.Debug.WriteLine("Time: " + DateTime.Now.ToString() +
                    Environment.NewLine
                        + "Message: " + pMessage);
                    str.Close();

                    Successful = true;
                }
                catch (Exception)
                {
                }

                if (Successful == true)     // Logging successful
                {
                    break;
                }
                else                        // Logging failed, retry in 100 milliseconds
                {
                    System.Threading.Thread.Sleep(10);
                }
            }
        }

        public static void LogMessage(Exception pExeption)
        {
            String str_inner = "";

            try
            {
                str_inner = Environment.NewLine +
                "Inner Exception Msg: " + pExeption.InnerException.Message +
                Environment.NewLine +
                "Inner Exception Stack: " + pExeption.InnerException.StackTrace +
                Environment.NewLine +
                "Object: " + pExeption.Source + Environment.NewLine +
                "Function Call: " + pExeption.TargetSite.Name;
            }
            catch (Exception)
            {
            }

            LogMessage("Exception: " + pExeption.Message + Environment.NewLine +
                "Stack: " + str_inner);
        }
    }
}
