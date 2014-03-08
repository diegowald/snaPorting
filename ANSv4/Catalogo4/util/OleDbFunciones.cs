using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo.Funciones
{
    class oleDbFunciones
    {

        public static System.Data.OleDb.OleDbConnection GetConn(string strConexion)
        {

            System.Data.OleDb.OleDbConnection conn = default(System.Data.OleDb.OleDbConnection);

            conn = new System.Data.OleDb.OleDbConnection(strConexion);

            return conn;

        }

    }
}
