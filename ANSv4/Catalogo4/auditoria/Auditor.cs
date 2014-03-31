using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.auditoria
{

    public class Auditor : Catalogo.util.singleton<Auditor>
    {

        public enum ObjetosAuditados
        {
        RegistroPrograma = 1,
        Logueo = 2,
        ActualizacionCliente = 3,
        ActualizacionCatalogo = 4,
        Programa = 5,
        Visita = 6,
        Pedido = 7,
        ImpresionPedido = 8,
        Recibo = 9,
        ImpresionRecibo = 10,
        Seguridad = 11,
        AppConfig = 12,
        Comandos = 13,
        WebServices = 14,
        Comunicaciones = 15,
        TiempoOperacion = 16,
        SubRutina = 17,
        Devoluciones = 18,
        ImpresionDevolucion = 19,
        ActualizacionGeneral = 20,
        InterDeposito = 21,
        ImpresionInterDeposito = 22,
        Rendicion = 23,
        ImpresionRendicion = 24,
        ErrordePrograma = 99,
        Rutina = 100
        }

        public enum AccionesAuditadas
        {
        INICIA = 1,
        EXITOSO = 2,
        FALLO = 3,
        CANCELA = 4,
        GUARDA = 5,
        TRANSMITE = 6,
        IMPRIME = 7,
        INFORMA = 8,
        TERMINA = 10,
        DESCONOCIDO = 100
        }

        private string ObjetoToString(ObjetosAuditados objeto)
        {
            switch(objeto)
            {
                case ObjetosAuditados.RegistroPrograma:
                    return "RegistroPrograma";
                    break;
                case ObjetosAuditados.Logueo:
                    return "Logueo";
                    break;
                case ObjetosAuditados.ActualizacionCliente:
                    return "ActualizacionCliente";
                    break;
                case ObjetosAuditados.ActualizacionCatalogo:
                    return "ActualizacionCatalogo";
                    break;
                case  ObjetosAuditados.Programa:
                    return"Programa";
                    break;
                case ObjetosAuditados.Visita:
                    return "Visita";
                    break;
                case ObjetosAuditados.Pedido:
                    return "Pedido";
                    break;
                case  ObjetosAuditados.ImpresionPedido:
                    return "ImpresionPedido";
                    break;
                case ObjetosAuditados.Recibo:
                    return "Recibo";
                    break;
                case ObjetosAuditados.ImpresionRecibo:
                    return "ImpresionRecibo";
                    break;
                case ObjetosAuditados.Seguridad:
                    return "Seguridad";
                    break;
                case ObjetosAuditados.AppConfig:
                    return "AppConfig";
                    break;
                case ObjetosAuditados.Comandos:
                    return"Comandos";
                    break;
                case ObjetosAuditados.WebServices:
                    return "WebServices";
                    break;
                case ObjetosAuditados.Comunicaciones:
                    return "Comunicaciones";
                    break;
                case ObjetosAuditados.TiempoOperacion:
                    return "TiempoOperacion";
                    break;
                case ObjetosAuditados.SubRutina:
                    return "SubRutina";
                    break;
                case ObjetosAuditados.Devoluciones:
                    return "Devoluciones";
                    break;
                case ObjetosAuditados.ImpresionDevolucion:
                    return "ImpresionDevolucion";
                    break;
                case ObjetosAuditados.ActualizacionGeneral:
                    return "ActualizacionGeneral";
                    break;
                case ObjetosAuditados.InterDeposito:
                    return "InterDeposito";
                    break;
                case ObjetosAuditados.ImpresionInterDeposito:
                    return "ImpresionInterDeposito";
                    break;
                case ObjetosAuditados.Rendicion:
                    return"Rendicion";
                    break;
                case  ObjetosAuditados.ImpresionRendicion:
                    return "ImpresionRendicion";
                    break;
                case ObjetosAuditados.ErrordePrograma:
                    return "ErrordePrograma";
                    break;
                case ObjetosAuditados.Rutina:
                    return "Rutina";
                    break;
                default:
                    return "";
                    break;
            }
        }


        private string AccionesAuditadasToString(AccionesAuditadas accion)
        {
            switch(accion)
            {
                case AccionesAuditadas.INICIA:
                    return "INICIA";
                    break;
                case AccionesAuditadas.EXITOSO:
                    return "EXITOSO";
                    break;
                case AccionesAuditadas.FALLO:
                    return "FALLO";
                    break;
                case AccionesAuditadas.CANCELA:
                    return "CANCELA";
                        break;
                case  AccionesAuditadas.GUARDA:
                    return "GUARDA";
                    break;
                case AccionesAuditadas.TRANSMITE:
                    return "TRANSMITE";
                    break;
                case AccionesAuditadas.IMPRIME:
                    return "IMPRIME";
                    break;
                case AccionesAuditadas.INFORMA:
                    return "INFORMA";
                    break;
                case AccionesAuditadas.TERMINA:
                    return "TERMINA";
                    break;
                case AccionesAuditadas.DESCONOCIDO:
                    return "DESCONOCIDO";
                    break;
                default:
                    return "";
                    break;
            }
        }

//            ' Define como se llama este modulo para el control de errores
//    Private Const m_sMODULENAME_ As String = "clsAuditor"
        private System.Data.OleDb.OleDbConnection Conexion1;

        public Auditor()
        {
            Conexion1=null;
        }

        public void guardar(ObjetosAuditados objeto, AccionesAuditadas accion)
        {
            guardar(objeto, accion, "");
        }

        public void guardar(ObjetosAuditados objeto,
            AccionesAuditadas accion, string detalle)
        {
            if (Global01.AuditarProceso)
            {
                if (!validarConexion())
                    return;
                System.Data.OleDb.OleDbCommand adoCmd = new System.Data.OleDb.OleDbCommand();
                
                adoCmd.Parameters.Add("pDetalle",  System.Data.OleDb.OleDbType.VarChar, 128).Value = 
                    ObjetoToString(objeto) + "|" + AccionesAuditadasToString(accion) + "|" + detalle;
                
                adoCmd.Connection = Conexion1;
                adoCmd.CommandType = global::System.Data.CommandType.StoredProcedure;
                adoCmd.CommandText = "usp_Auditor_add";
                adoCmd.ExecuteNonQuery();
            }
        }

        public System.Data.OleDb.OleDbConnection Conexion
        {
            set
            {
                Conexion1 = value;
            }
        }

        private bool validarConexion()
        {
            return Conexion1!=null;
        }
    }
}