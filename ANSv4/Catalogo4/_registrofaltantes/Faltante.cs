using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._registrofaltantes
{
    public class Faltante
    {
        // Define como se llama este modulo para el control de errores

        private System.Data.OleDb.OleDbConnection mvarConexion;
        private int mvarIdCliente;
        private int mvarIdViajante;
        private string mvarNroFaltante;
        private DateTime mvarF_Faltante;        
        private DateTime mvarF_Transmicion;
        private string mvarObservaciones;

        public event GuardoOKEventHandler GuardoOK;
        public delegate void GuardoOKEventHandler(string nroFaltante);

        public Faltante(System.Data.OleDb.OleDbConnection conexion, string NroUsuario, int IDDeCliente)
        {
            Nuevo(conexion, Int16.Parse(NroUsuario), IDDeCliente);
        }

        protected void Nuevo(System.Data.OleDb.OleDbConnection conexion, int IdViajante, int IDdeCliente = 0)
        {
            mvarNroFaltante = "";
            mvarF_Faltante = DateTime.Today;
            mvarConexion = conexion;
            mvarIdViajante = IdViajante;
            if (IDdeCliente > 0)
            {
                mvarIdCliente = IDdeCliente;
                //Global01.EmailTO = (Funciones.oleDbFunciones.Comando(conexion, "SELECT Email FROM tblClientes WHERE ID=" + IDdeCliente, "Email")).ToLower();
            }
        }

        public string Observaciones
        {
            get { return mvarObservaciones; }
            set { mvarObservaciones = value; }
        }

        public System.DateTime F_Transmision
        {
            get { return mvarF_Transmicion; }
            set { mvarF_Transmicion = value; }
        }

        public void Leer(string NrodeFaltante)
        {
            if (!(ValidarConexion()))
                return;

            System.Data.OleDb.OleDbDataReader rec = null;
            rec = Funciones.oleDbFunciones.Comando(mvarConexion, "EXECUTE v_Faltante1 " + NrodeFaltante);
            if (rec.HasRows)
            {
                rec.Read();
                mvarNroFaltante = NrodeFaltante.ToString();
                mvarF_Faltante = (DateTime) rec["F_Faltante"];
                mvarIdCliente = (int) rec["IdCliente"];
                mvarIdCliente = (int) rec["IdViajante"];
                mvarF_Transmicion = (DateTime) rec["F_Transmicion"];
                mvarObservaciones = rec["Observaciones"].ToString();
            }
        }

        public System.Data.OleDb.OleDbConnection Conexion
        {
            set { mvarConexion = value; }
        }

        public System.DateTime F_Faltante
        {
            get { return mvarF_Faltante; }
        }

        public string nroFaltante
        {
            get { return mvarNroFaltante; }
        }

        private bool ValidarConexion()
        {
            return (mvarConexion != null);
        }

        public void Guardar(string Origen)
        {
            if (!(ValidarConexion()))
                return;

            System.Data.OleDb.OleDbDataReader rec = null;
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            if (Origen.ToUpper() == "VER")
            {
                Funciones.oleDbFunciones.ComandoIU(mvarConexion, "DELETE FROM tblFaltante WHERE NroFaltante='09999-99999999'");
                mvarNroFaltante = "09999-99999999";
            }
            else
            {
                rec = Funciones.oleDbFunciones.Comando(mvarConexion, "SELECT TOP 1 right(NroFaltante,8) AS NroFaltante FROM tblFaltante WHERE left(NroFaltante,5)=" + Global01.NroUsuario.Trim() + " ORDER BY NroFaltante DESC");
                if (!rec.HasRows)
                {
                    mvarNroFaltante = Global01.NroUsuario.Trim() + "-00000001";
                }
                else
                {
                    rec.Read();
                    mvarNroFaltante = Global01.NroUsuario.Trim() + "-" + (int.Parse(rec["NroFaltante"].ToString().Substring(rec["NroFaltante"].ToString().Length - 8)) + 1).ToString().PadLeft(8, '0');
                }
                rec = null;
            }

            cmd.Parameters.Add("pNroFaltante", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroFaltante;
            cmd.Parameters.Add("pF_Faltante", System.Data.OleDb.OleDbType.Date).Value = mvarF_Faltante;
            cmd.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = mvarIdCliente;
            cmd.Parameters.Add("pIdViajante", System.Data.OleDb.OleDbType.Integer).Value = mvarIdViajante;
            cmd.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 255).Value = mvarObservaciones;

            cmd.Connection = mvarConexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_Faltante_add";
            cmd.ExecuteNonQuery();

            cmd = null;

            Global01.NroImprimir = mvarNroFaltante;

            // Por POLITICA NUESTRA  -- >  SE BORRA
            if (Origen.ToUpper() == "VER")
            {
                // Actualizo Fecha de Transmicion para que no lo mande
                Funciones.oleDbFunciones.ComandoIU(mvarConexion, "EXEC usp_Faltante_Transmicion_Upd '" + mvarNroFaltante + "'");
            }
            else
            {
                //_auditor.Auditor.instance.guardar(_auditor.Auditor.ObjetosAuditados.Faltante,
                //     _auditor.Auditor.AccionesAuditadas.EXITOSO, "cli:" + mvarIdCliente.ToString().Trim().PadLeft(6, '0') + " rec:" + mvarNroFaltante + " tot:" + string.Format("{0:N2}", mvarTotal));
                ////Nuevo();
                if (GuardoOK != null)
                {
                    GuardoOK(Global01.NroImprimir);
                }
            }

        }

    }
}
