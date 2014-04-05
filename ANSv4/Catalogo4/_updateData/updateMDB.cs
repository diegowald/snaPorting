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

            Funciones.modINIs.DeleteKeyINI("UPDATE", "mdb");

            oleDbFunciones.CambiarLinks(db);

            Global01.Conexion = Funciones.oleDbFunciones.GetConn(Catalogo.Global01.strConexionUs);

            if (!(Global01.Conexion.State == System.Data.ConnectionState.Open)) { Global01.Conexion.Open(); };

            if (Global01.TranActiva==null)
            {
                Global01.TranActiva = Global01.Conexion.BeginTransaction();
            }

            try
            {
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaAppConfig");

                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaPedidoEnc");
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaPedidoDet");

                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaDevolucionEnc");
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaDevolucionDet");
                Application.DoEvents();

                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaReciboEnc");
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaReciboDet");
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaReciboApp");
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaReciboDed");
                Application.DoEvents();
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaRendicion");
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaRendicionValores");
                Application.DoEvents();

                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexatblLineasPorcentaje");


                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaClientes");
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaCtaCte");
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaClientesNovedades");
                Application.DoEvents();

                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaInterDeposito");
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaInterDeposito_Fac");
                Application.DoEvents();

                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xIDsCatalogoBAK_Pedidos_Anexa");
                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xIDsCatalogoBAK_Devolucion_Anexa");
                Application.DoEvents();

                oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaAuditor");
                Application.DoEvents();

                //oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaCatalogoBAK");

                if (Global01.TranActiva != null)
                {
                    Global01.TranActiva.Commit();
                 };
            }
            catch (Exception e)
            {

                if (Global01.TranActiva != null)
                {
                    Global01.TranActiva.Rollback();
                }
                throw e;
                //throw new Exception(e.Message.ToString() + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
            }
            finally
            {
                Global01.TranActiva = null;
            }
      
            System.IO.File.Delete(Global01.AppPath + "\\Reportes\\Catalogo.mdb");
            System.IO.File.Delete(Global01.AppPath + "\\up201406.exe");

            oleDbFunciones.Desconectar(Global01.Conexion);

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
