using System;

namespace Catalogo
{

     static class Global01
    {           

       public static System.Data.OleDb.OleDbConnection Conexion = null;
    
       public static string AppPath = Funciones.modINIs.ReadINI("Datos", "Path", System.IO.Directory.GetCurrentDirectory());
       public static string PathAcrobat = Funciones.modINIs.ReadINI("Datos", "PathAcrobat", "");
       public static string FileBak = "CopiaCata.001";
         
       public static string cstring = AppPath + "\\datos\\ans.mdb";        
       public static string dstring = AppPath + "\\datos\\catalogo.mdb";
       public static string sstring = Environment.GetEnvironmentVariable("windir") + "\\Help\\KbAppCat.hlp";

       public static string strConexion = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dstring + ";Persist Security Info=True;Password=video80min;User ID=inVent;Jet OLEDB:System database=" + sstring;

       public static string ArchCerradura = "";
       public static string ArchLlave = "";

    }

}
