using System;

namespace Catalogo
{

     static class Global01
    {

       public const string up2014Ad = ";User Id=hpcd-rw;Password=data700mb";
       public const string up2014Us = ";User ID=inVent;Password=video80min";

       public static System.Data.OleDb.OleDbConnection Conexion = null;
       public static System.Data.OleDb.OleDbTransaction TranActiva = null;
       public static string AppPath = Funciones.modINIs.ReadINI("Datos", "Path", System.IO.Directory.GetCurrentDirectory());
       public static string PathAcrobat = Funciones.modINIs.ReadINI("Datos", "PathAcrobat", "");
       public static string FileBak = "CopiaCata.001";
         
       public static string cstring = AppPath + "\\datos\\ans.mdb";        
       public static string dstring = AppPath + "\\datos\\catalogo.mdb";
       public static string sstring = Environment.GetEnvironmentVariable("windir") + "\\Help\\KbAppCat.hlp";

       public static string strConexionUs = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dstring + up2014Us + ";Persist Security Info=True;Jet OLEDB:System database=" + sstring;
       public static string strConexionAd = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + dstring + up2014Ad + ";jet oledb:system database=" + sstring;

       public static string ArchCerradura = "";
       public static string ArchLlave = "";

    }

}
