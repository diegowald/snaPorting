using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Catalogo.Funciones
{

    public class modINIs
    {
        [DllImport("kernel32", EntryPoint = "WritePrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]

        #region "API Calls"
        // standard API declarations for INI access
        // changing only "As Long" to "As Int32" (As Integer would work also)
        private static extern Int32 WritePrivateProfileString(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);
        [DllImport("kernel32", EntryPoint = "GetPrivateProfileStringW", CharSet = CharSet.Unicode, SetLastError = true, ExactSpelling = true)]

        private static extern Int32 GetPrivateProfileString(string lpApplicationName, string lpKeyName, string lpDefault, string lpReturnedString, Int32 nSize, string lpFileName);
        #endregion


        public static string INIRead(string INIPath, string SectionName, string KeyName, string DefaultValue)
        {
            string functionReturnValue = null;

            // primary version of call gets single value given all parameters
            Int32 n = default(Int32);
            string sData = null;
            sData = new String(' ', 1024);
            // allocate some room 
            n = GetPrivateProfileString(SectionName, KeyName, DefaultValue, sData, sData.Length, INIPath);
            // return whatever it gave us
            if (n > 0)
            {
                functionReturnValue = sData.Substring(0, n);
            }
            else
            {
                functionReturnValue = "";
            }
            return functionReturnValue;

        }

        public static string ReadINI(string SectionName, string KeyName, string DefaultValue = null)
        {

            return INIRead(System.IO.Directory.GetCurrentDirectory() + "\\settings.ini", SectionName, KeyName, DefaultValue);

        }

        public static string INIRead(string INIPath, string SectionName, string KeyName)
        {

            // overload 1 assumes zero-length default
            return INIRead(INIPath, SectionName, KeyName, "");

        }

        public static string INIRead(string INIPath, string SectionName)
        {

            // overload 2 returns all keys in a given section of the given file
            return INIRead(INIPath, SectionName, null, "");

        }

        public static string INIRead(string INIPath)
        {

            // overload 3 returns all section names given just path
            return INIRead(INIPath, null, null, "");

        }


        public static void WriteINI(string SectionName, string KeyName, string TheValue)
        {
            WritePrivateProfileString(SectionName, KeyName, TheValue, System.IO.Directory.GetCurrentDirectory() + "\\settings.ini");

        }


        public static void INIWrite(string INIPath, string SectionName, string KeyName, string TheValue)
        {
            WritePrivateProfileString(SectionName, KeyName, TheValue, INIPath);

        }

        // delete single line from section
        public static void DeleteKeyINI(string SectionName, string KeyName)
        {

            WritePrivateProfileString(SectionName, KeyName, null, System.IO.Directory.GetCurrentDirectory() + "\\settings.ini");

        }

        // delete single line from section
        public static void INIDelete(string INIPath, string SectionName, string KeyName)
        {

            WritePrivateProfileString(SectionName, KeyName, null, INIPath);

        }


        public static void INIDelete(string INIPath, string SectionName)
        {
            // delete section from INI file
            WritePrivateProfileString(SectionName, null, null, INIPath);

        }

    }

}