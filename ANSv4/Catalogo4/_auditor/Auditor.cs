using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._auditor
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
                case ObjetosAuditados.Logueo:
                    return "Logueo";
                case ObjetosAuditados.ActualizacionCliente:
                    return "ActualizacionCliente";
                case ObjetosAuditados.ActualizacionCatalogo:
                    return "ActualizacionCatalogo";
                case  ObjetosAuditados.Programa:
                    return"Programa";
                case ObjetosAuditados.Visita:
                    return "Visita";
                case ObjetosAuditados.Pedido:
                    return "Pedido";
                case  ObjetosAuditados.ImpresionPedido:
                    return "ImpresionPedido";
                case ObjetosAuditados.Recibo:
                    return "Recibo";
                case ObjetosAuditados.ImpresionRecibo:
                    return "ImpresionRecibo";
                case ObjetosAuditados.Seguridad:
                    return "Seguridad";
                case ObjetosAuditados.AppConfig:
                    return "AppConfig";
                case ObjetosAuditados.Comandos:
                    return"Comandos";
                case ObjetosAuditados.WebServices:
                    return "WebServices";
                case ObjetosAuditados.Comunicaciones:
                    return "Comunicaciones";
                case ObjetosAuditados.TiempoOperacion:
                    return "TiempoOperacion";
                case ObjetosAuditados.SubRutina:
                    return "SubRutina";
                case ObjetosAuditados.Devoluciones:
                    return "Devoluciones";
                case ObjetosAuditados.ImpresionDevolucion:
                    return "ImpresionDevolucion";
                case ObjetosAuditados.ActualizacionGeneral:
                    return "ActualizacionGeneral";
                case ObjetosAuditados.InterDeposito:
                    return "InterDeposito";
                case ObjetosAuditados.ImpresionInterDeposito:
                    return "ImpresionInterDeposito";
                case ObjetosAuditados.Rendicion:
                    return"Rendicion";
                case  ObjetosAuditados.ImpresionRendicion:
                    return "ImpresionRendicion";
                case ObjetosAuditados.ErrordePrograma:
                    return "ErrordePrograma";
                case ObjetosAuditados.Rutina:
                    return "Rutina";
                default:
                    return "";
            }
        }


        private string AccionesAuditadasToString(AccionesAuditadas accion)
        {
            switch(accion)
            {
                case AccionesAuditadas.INICIA:
                    return "INICIA";
                case AccionesAuditadas.EXITOSO:
                    return "EXITOSO";
                case AccionesAuditadas.FALLO:
                    return "FALLO";
                case AccionesAuditadas.CANCELA:
                    return "CANCELA";
                case  AccionesAuditadas.GUARDA:
                    return "GUARDA";
                case AccionesAuditadas.TRANSMITE:
                    return "TRANSMITE";
                case AccionesAuditadas.IMPRIME:
                    return "IMPRIME";
                case AccionesAuditadas.INFORMA:
                    return "INFORMA";
                case AccionesAuditadas.TERMINA:
                    return "TERMINA";
                case AccionesAuditadas.DESCONOCIDO:
                    return "DESCONOCIDO";
                default:
                    return "";
            }
        }

        private System.Data.OleDb.OleDbConnection Conexion1;

        public Auditor()
        {
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
                {
                    util.errorHandling.ErrorLogger.LogMessage("No se puede auditar. Conexion no valida");
                    return;
                }
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
            if (Conexion1 == null)
            {
                throw new InvalidOperationException("No esta correctamente configurada la base de datos.");
            }
            if (Conexion1.State!= System.Data.ConnectionState.Open)
            {
                throw new InvalidOperationException("La conexion no esta abierta.");
            }
            return Conexion1 != null;
        }
    }
}