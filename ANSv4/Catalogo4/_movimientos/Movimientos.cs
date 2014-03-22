using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._movimientos
{
    class Movimientos
    {

        public enum DATOS_MOSTRAR
        {
            TODOS = 0,
            ENVIADOS,
            NO_ENVIADOS
        };

        private System.Data.OleDb.OleDbConnection Conexion1;
        private string _IDMaquina;

        public Movimientos(System.Data.OleDb.OleDbConnection conexion, string IDMaquina)
        {
            Conexion1 = conexion;
            _IDMaquina = IDMaquina;
        }

        public System.Data.OleDb.OleDbDataReader Leer(DATOS_MOSTRAR queMostrar, long IDCliente)
        {
            if (!validarConexion())
            {
                return null;
            }

            string wCond2 = (IDCliente > 0 ?
                " and IDcliente=" + IDCliente.ToString()
                : "");

            switch (queMostrar)
            {
                case DATOS_MOSTRAR.TODOS:
                    return Funciones.oleDbFunciones.xGetDr(ref Conexion1, "v_Movimientos", "left(Nro,5)='" + IDCliente + "' " + wCond2, "IDcliente, Origen, Fecha DESC");
                    break;
                case DATOS_MOSTRAR.ENVIADOS:
                    return Funciones.oleDbFunciones.xGetDr(ref Conexion1, "v_Movimientos", "left(Nro,5)='" + IDCliente + "' and not (F_Transmicion is null)" + wCond2, "IDcliente, Origen, Fecha DESC");
                    break;
                case DATOS_MOSTRAR.NO_ENVIADOS:
                    return Funciones.oleDbFunciones.xGetDr(ref Conexion1, "v_Movimientos", "left(Nro,5)='" + IDCliente + "' and F_Transmicion is null" + wCond2, "IDcliente, Origen, Fecha DESC");
                    break;
                default:
                    return null;
            }
        }


        public bool preguntoAlSalir()
        {
            // Devuelvo true si es que HAY PENDIENTE de ALGUNOS de los cliente
            if (!validarConexion())
            {
                return false;
            }

            System.Data.OleDb.OleDbDataReader rec = Funciones.oleDbFunciones.Comando(ref Conexion1, "SELECT Count(*) AS Cantidad FROM UnionPedidoRecibo WHERE (F_Transmicion is null) and left(Nro,5)='" + _IDMaquina + "'");

            if (rec.HasRows)
            {
                return ((int)rec["cantidad"] > 0);
            }

            return false;
        }

        public bool preguntoAlCerrarVisista(long IDCLiente)
        {
            // Devuelvo true si es que HAY PENDIENTE para ESTE cliente.

            if (!validarConexion())
            {
                return false;
            }

            System.Data.OleDb.OleDbDataReader rec = Funciones.oleDbFunciones.Comando(ref Conexion1, "SELECT Count(*) AS Cantidad FROM UnionPedidoRecibo WHERE UnionPedidoRecibo.IdCliente=" + IDCLiente.ToString() + " and (f_Transmicion is NULL) and left(Nro,5)='" + _IDMaquina + "'");
            if (rec.HasRows)
            {
                return (int)rec["cantidad"] > 0;
            }
            return false;
        }

        private bool validarConexion()
        {
            return Conexion1 != null;
        }
    }
}