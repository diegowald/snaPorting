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

        private System.Data.OleDb.OleDbConnection _conexion;
        private string _idCliente;

        public Movimientos(System.Data.OleDb.OleDbConnection conexion, int IdCliente)
        {
            _conexion = conexion;
            _idCliente = IdCliente.ToString().PadLeft(5,'0');
        }

        public System.Data.OleDb.OleDbDataReader Leer(DATOS_MOSTRAR queMostrar, string Origen)
        {
            
            if (!validarConexion())
            {
                return null;
            }

            System.Data.OleDb.OleDbDataReader dr = null;
            string wCond2 = (int.Parse(_idCliente) > 0 ? " and IdCliente=" + _idCliente : " ");
            string wCond3 = ((Origen.Trim()!="(todos)") ? " and UCase(mid(Origen,1,4))='" + Origen.Substring(0,4).ToUpper() + "' " : " ");

            switch (queMostrar)
            {
                case DATOS_MOSTRAR.TODOS:
                    dr =  Funciones.oleDbFunciones.xGetDr(_conexion, "v_Movimientos", "left(Nro,5)='" + Global01.NroUsuario  + "' " + wCond2 + wCond3, "IDcliente, Origen, Fecha DESC");
                    break;
                case DATOS_MOSTRAR.ENVIADOS:
                    dr = Funciones.oleDbFunciones.xGetDr(_conexion, "v_Movimientos", "left(Nro,5)='" + Global01.NroUsuario + "' and not (F_Transmicion is null)" + wCond2 + wCond3, "IDcliente, Origen, Fecha DESC");
                    break;
                case DATOS_MOSTRAR.NO_ENVIADOS:
                    dr = Funciones.oleDbFunciones.xGetDr(_conexion, "v_Movimientos", "left(Nro,5)='" + Global01.NroUsuario + "' and F_Transmicion is null" + wCond2 + wCond3, "IDcliente, Origen, Fecha DESC");
                    break;
            }
            
            return dr;

        }

        public bool preguntoAlSalir()
        {
            // Devuelvo true si es que HAY PENDIENTE de ALGUNOS de los cliente
            if (!validarConexion())
            {
                return false;
            }

            System.Data.OleDb.OleDbDataReader rec = Funciones.oleDbFunciones.Comando(_conexion, "SELECT Count(*) AS Cantidad FROM UnionPedidoRecibo WHERE (F_Transmicion is null) and left(Nro,5)='" + _idCliente + "'");

            if (rec.HasRows)
            {
                return ((int)rec["cantidad"] > 0);
            }

            return false;
        }

        public bool preguntoAlCerrarVisista()
        {
            // Devuelvo true si es que HAY PENDIENTE para ESTE cliente.

            if (!validarConexion())
            {
                return false;
            }

            System.Data.OleDb.OleDbDataReader rec = Funciones.oleDbFunciones.Comando(_conexion, "SELECT Count(*) AS Cantidad FROM UnionPedidoRecibo WHERE UnionPedidoRecibo.IdCliente=" + _idCliente + " and (f_Transmicion is NULL) and left(Nro,5)='" + _idCliente + "'");
            if (rec.HasRows)
            {
                return (int)rec["cantidad"] > 0;
            }
            return false;
        }

        private bool validarConexion()
        {
            return _conexion != null;
        }
    }
}