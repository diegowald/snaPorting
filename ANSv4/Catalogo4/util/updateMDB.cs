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

            Global01.Conexion = Funciones.oleDbFunciones.GetConn(Catalogo.Global01.strConexionUs);

            if (!(Global01.Conexion.State == System.Data.ConnectionState.Open)) { Global01.Conexion.Open(); };

            if (Global01.TranActiva==null)
            {
                Global01.TranActiva = Global01.Conexion.BeginTransaction();
            }

            try
            {
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaAppConfig");
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaPedidoEnc");
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaPedidoDet");
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaDevolucionEnc");
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaDevolucionDet");
                Application.DoEvents();

                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaReciboEnc");
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaReciboDet");
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaReciboApp");
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaReciboDed");
                Application.DoEvents();

                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexatblLineasPorcentaje");
                Application.DoEvents();

                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaClientes");
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaCtaCte");
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaClientesNovedades");

                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xIDsCatalogoBAK_Pedidos_Anexa");
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xIDsCatalogoBAK_Devolucion_Anexa");

                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaCatalogoBAK");

                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaAuditor");
                Application.DoEvents();

                //If TableExists("bkpTblInterDeposito1", adoCN) Then
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaInterDeposito");
                //If TableExists("bkpTblInterDeposito_Fac1", adoCN) Then
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaInterDeposito_Fac");
                //If TableExists("bkptblRendicion1", adoCN) Then
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaRendicion");
                //If TableExists("bkpbkptblRendicionValores1", adoCN) Then
                oleDbFunciones.ComandoIU(ref Global01.Conexion, "EXEC xAnexaRendicionValores");

                Application.DoEvents();

                if (Global01.TranActiva != null)
                {
                    Global01.TranActiva.Commit();
                    Global01.TranActiva = null;
                };
            }
            catch (Exception e)
            {
                if (Global01.TranActiva != null)
                {
                    Global01.TranActiva.Rollback();
                    Global01.TranActiva = null;
                }
                throw new Exception(e.Message.ToString() + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
            }
            finally
            {

            }
      

            Funciones.modINIs.DeleteKeyINI("UPDATE", "mdb");

            System.IO.File.Delete(Global01.AppPath + "\\Reportes\\Catalogo.mdb");
            System.IO.File.Delete(Global01.AppPath + "\\up201406.exe");

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
