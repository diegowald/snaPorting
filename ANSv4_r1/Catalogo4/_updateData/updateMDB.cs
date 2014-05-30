﻿using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Catalogo.Funciones
{
    class updateMDB
    {
        //const string m_sMODULENAME_ = "updateMDB";
        //private System.Data.OleDb.OleDbTransaction _TranActiva = null;

        internal static void Emergencia(string db)
        {
            //const string PROCNAME_ = "Emergencia";

            using (new varios.WaitCursor())
            {

                Funciones.modINIs.DeleteKeyINI("UPDATE", "mdb");

                oleDbFunciones.CambiarLinks(db);

                Global01.Conexion = Funciones.oleDbFunciones.GetConn(Catalogo.Global01.strConexionUs);
                Catalogo._auditor.Auditor.instance.Conexion = Global01.Conexion;

                if (!(Global01.Conexion.State == System.Data.ConnectionState.Open))
                {
                    Global01.Conexion.Open();
                }

                //if (Global01.TranActiva==null)
                //{
                //    Global01.TranActiva = Global01.Conexion.BeginTransaction();
                //    Catalogo.util.errorHandling.ErrorLogger.LogMessage("4");
                //}

                try
                {
                    oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaAppConfig");

                    oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaPedidoEnc");
                    oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaPedidoDet");

                    oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexatblLineasPorcentaje");


                    if (Global01.miSABOR == Global01.TiposDeCatalogo.Cliente)
                    {
                        oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaAppConfigCliente_add");
                    }
                    else
                    {
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

                        oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaClientes");
                        oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaCtaCte");
                        oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaClientesNovedades");
                        Application.DoEvents();
                    }

                    oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaInterDeposito");
                    oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaInterDeposito_Fac");
                    Application.DoEvents();

                    oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaCatalogoBAK");
                    oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xIDsCatalogoBAK_Pedidos_Anexa");
                    oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xIDsCatalogoBAK_Devolucion_Anexa");
                    Application.DoEvents();

                    //oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC xAnexaAuditor");
                    //oleDbFunciones.ComandoIU(Global01.Conexion, "EXEC usp_Novedades_Anexar");                

                    //Application.DoEvents();

                    //if (Global01.TranActiva != null)
                    //{
                    //    Global01.TranActiva.Commit();
                    //}
                }
                catch (Exception ex)
                {

                    //if (Global01.TranActiva != null)
                    //{
                    //    Global01.TranActiva.Rollback();
                    //}
                    Catalogo.util.errorHandling.ErrorLogger.LogMessage(ex);

                    throw ex;
                    //throw new Exception(e.Message.ToString() + ' ' + m_sMODULENAME_ + ' ' + PROCNAME_);
                }
                //finally
                //{
                //    Global01.TranActiva = null;
                //}
                oleDbFunciones.Desconectar(Global01.Conexion);

                System.IO.File.Delete(Global01.AppPath + "\\Reportes\\Catalogo.mdb");
                System.IO.File.Delete(Global01.AppPath + "\\up201406.exe");

            }

        }

    }
}
