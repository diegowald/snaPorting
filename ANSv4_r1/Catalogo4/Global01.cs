using System;

namespace Catalogo
{

    public static class Global01
    {

       public enum TiposDeCatalogo
        {
            Invitado = 1,
            Cliente = 2,
            Viajante = 3,
            Supervisor = 4
        }

       public enum TiposDePing
       {
           ICMP =1,
           HTTP,
           FILE
       }

       public static string VersionApp;
       public static TiposDeCatalogo miSABOR;

       public const string up2014Ad = ";User Id=hpcd-rw;Password=data700mb";
       public const string up2014Us = ";User ID=inVent;Password=video80min";

       public static System.Data.OleDb.OleDbConnection Conexion;
       //public static System.Data.OleDb.OleDbTransaction TranActiva_;
       public static string AppPath;
       //public static string PathAcrobat;
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

       public static string OperacionActivada;
       public static string NroDocumentoAbierto;
       public static string NroUsuario;
       public static string Zona;
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
       public static string proxyServerAddress;
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

       public static string EmailTO;
       public static string EmailBody;
       public static string EmailAsunto;

       public static string NroImprimir;
       public static string IPPing;

       // -- DEFAULT de Setting ------
       public static string setDef_INFO = "0";                  //control catalogo online
       public static string setDef_DEP  = "0";                  //deposito
       public static string setDef_DEV_abierta_ne = "1";        //Devolucion Abierta no enviada
       public static string setDef_PED_abierto_ne = "1";        //Pedido Abierto no enviado
       public static string setDef_checkNovedades = "10";       //tiempo de chequeo de novedades
       public static string setDef_checkConectadoMinutos = "1"; //tiempo de chequeo de conectado
       public static string setDef_checkImagenUpdate = "0";     //Imagen Existe PERO busca nueva
       public static string setDef_checkImagenNueva = "1";      //Imagen NO Existe
       public static string setDef_ICC = "0";                   //Imprime Cta. Cte.
       public static string setDef_CCC = "0";                   //Combo Cliente x Nro. de Cuenta
       public static string setDef_DelayedEnviar = "2";         //tiempo de demora para envios
       public static string setDef_SiempreEnviar = "0";         //enviar siempre
       public static string setDef_IP =  "0.0.0.0";             //
       public static string setDef_IP2 = "0.0.0.0";             //
       public static string setDef_ProxyServer = "0.0.0.0";     //
       public static string setDef_IPPing = "8.8.8.8";          //
       public static string setDef_ConfirmaSalida = "1";
       public static string setDef_EsGerente = "0";

    }
}
