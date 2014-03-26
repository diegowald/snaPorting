using System;

namespace Catalogo
{

    static class Global01
    {

       public enum TiposDeCatalogo
        {
            Invitado = 1,
            Cliente = 2,
            Viajante = 3,
            Supervisor = 4
        }

       public static string VersionApp;
       public static TiposDeCatalogo miSABOR;

       public const string up2014Ad = ";User Id=hpcd-rw;Password=data700mb";
       public const string up2014Us = ";User ID=inVent;Password=video80min";

       public static System.Data.OleDb.OleDbConnection Conexion;
       public static System.Data.OleDb.OleDbTransaction TranActiva;
       public static string AppPath;
       public static string PathAcrobat;
       public static string FileBak;
         
       public static string cstring;        
       public static string dstring;
       public static string sstring;

       public static string strConexionUs;
       public static string strConexionAd;

       public static string IDMaquina;
       public static string IDMaquinaCRC;
       public static string IDMaquinaREG;
       public static string LLaveViajante;
       public static bool RecienRegistrado;
       public static bool AppActiva;

       public static string NroUsuario;
       public static System.DateTime dbCaduca;
       public static System.DateTime appCaduca;
       public static System.DateTime F_ActCatalogo;
       public static System.DateTime F_ActClientes;
       public static System.DateTime F_UltimoAcceso;
       public static bool ActualizarClientes;

       public static bool EnviarAuditoria;
       public static bool AuditarProceso;
       public static bool xError;
       public static string URL_ANS;
       public static string URL_ANS2;
       public static string MainWindowCaption;
       public static bool NoConn;
       public static int MiBuild;
       public static byte ListaPrecio;
       public static float Dolar;
       public static float Euro;

       public static string Cuit;
       public static string pin;
       public static string RazonSocial;
       public static string ApellidoNombre;

       public static string NroImprimir;

    }
}
