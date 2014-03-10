using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Catalogo.Funciones
{
    class updateMDB
    {

        const string m_sMODULENAME_ = "updateMDB";

        internal static void Emergencia(string db)
        {
            const string PROCNAME_ = "Emergencia";

            Cursor.Current = Cursors.WaitCursor;

            oleDbFunciones.CambiarLinks(db);

            //string sCN = My.Settings.catalogoConnectionString.ToString;
            //System.Data.OleDb.OleDbConnection adoCN = new System.Data.OleDb.OleDbConnection();

            //Funciones.adoModulo.adoConectar(adoCN, sCN);


            if (Global01.TranActiva == null)
            {
                Global01.TranActiva = Global01.Conexion.BeginTransaction(); 
            }

            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaAppConfig");
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaPedidoEnc");
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaPedidoDet");
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaDevolucionEnc");
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaDevolucionDet");
            Application.DoEvents();

            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaReciboEnc");
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaReciboDet");
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaReciboApp");
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaReciboDed");
            Application.DoEvents();

            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexatblLineasPorcentaje");
            Application.DoEvents();

            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaClientes");
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaCtaCte");
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaClientesNovedades");

            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xIDsCatalogoBAK_Pedidos_Anexa");
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xIDsCatalogoBAK_Devolucion_Anexa");

            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaCatalogoBAK");

            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaAuditor");
            Application.DoEvents();

            //If TableExists("bkpTblInterDeposito1", adoCN) Then
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaInterDeposito");
            //If TableExists("bkpTblInterDeposito_Fac1", adoCN) Then
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaInterDeposito_Fac");
            //If TableExists("bkptblRendicion1", adoCN) Then
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaRendicion");
            //If TableExists("bkpbkptblRendicionValores1", adoCN) Then
            oleDbFunciones.Comando(ref Global01.Conexion, "EXEC xAnexaRendicionValores");

            Application.DoEvents();

            if (Global01.TranActiva != null)
            {
                Global01.TranActiva.Commit();
                Global01.TranActiva = null;
            }

            Funciones.modINIs.DeleteKeyINI("UPDATE", "mdb");

            System.IO.File.Delete(Global01.AppPath + "\\Reportes\\Catalogo.mdb");
            System.IO.File.Delete(Global01.AppPath + "\\up200706.exe");

            oleDbFunciones.Desconectar(ref Global01.Conexion);

            Cursor.Current = Cursors.Default;

            //Exit Sub

            //-------- ErrorGuardian Begin --------
            //ErrorGuardianLocalHandler:
            //
            //    If Err.Number = -2147467259 Then
            //        Resume Next
            //    Else
            //        DeleteKeyINI "update", "mdb"
            //        Kill vg.Path & "\Reportes\Catalogo.mdb"
            //        adoModulo.adoDesconectar adoCN
            //        Screen.MousePointer = vbDefault
            //
            //        Select Case ErrorGuardianGlobalHandler(m_sMODULENAME_, PROCNAME_)
            //            Case vbRetry
            //                Resume
            //            Case vbIgnore
            //                Resume Next
            //        End Select
            //    End If
            //-------- ErrorGuardian End ----------

        }

    }
}
