using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._interdeposito
{
    public class InterDeposito
    {
        private const string m_sMODULENAME_ = "clsInterDeposito";

        private System.Data.OleDb.OleDbConnection mvarConexion;
        private string mvarNroInterDeposito;
        private string mvarBco_Dep_Tipo;
        private System.DateTime mvarBco_Dep_Fecha;
        private int mvarBco_Dep_Numero;
        private float mvarBco_Dep_Monto;
        private byte mvarBco_Dep_Ch_Cantidad;
        private byte mvarBco_Dep_IdCta;

        private int mvarIdCliente;
        private byte mvarNroImpresion;
        private string mvarObservaciones;

        private System.Collections.Generic.Dictionary<string, InterDepositoFacturas> IntDepFacturas;

        public event GuardoOKEventHandler GuardoOK;
        public delegate void GuardoOKEventHandler(string NroInterDeposito);

        public InterDeposito(System.Data.OleDb.OleDbConnection conexion, string NroUsuario, int IDDeCliente)
        {
            Nuevo(conexion, IDDeCliente);
        }

       protected void Nuevo(System.Data.OleDb.OleDbConnection conexion, int IDdeCliente = 0)
       {
           IntDepFacturas = new Dictionary<string, InterDepositoFacturas>();
           mvarNroInterDeposito = "";
           mvarConexion = conexion;

           if (IDdeCliente > 0)
               mvarIdCliente = IDdeCliente;
       }

        public string Observaciones
        {
            get { return mvarObservaciones; }
            set { mvarObservaciones = value; }
        }

        public  int  CantidadItems
        {
            get { return IntDepFacturas.Count; }
        }

        // FUNDAMENTAL PARA QUE TE DE LOS NOMBRES
        public InterDepositoFacturas Renglon(int Numero)
        {
            return IntDepFacturas["_" + Numero];
        }

        public System.Data.OleDb.OleDbConnection Conexion
        {
            set { mvarConexion = value; }
        }

        public byte NroImpresion
        {
            get { return mvarNroImpresion; }
            set { mvarNroImpresion = value; }
        }

        public string NroInterDeposito
        {
            get { return mvarNroInterDeposito; }
            set { mvarNroInterDeposito = value; }
        }

        public string Bco_Dep_Tipo
        {
            get { return mvarBco_Dep_Tipo; }
            set { mvarBco_Dep_Tipo = value; }
        }

        public System.DateTime Bco_Dep_Fecha
        {
            get { return mvarBco_Dep_Fecha; }
            set { mvarBco_Dep_Fecha = value; }
        }

        public int Bco_Dep_Numero
        {
            get { return mvarBco_Dep_Numero; }
            set { mvarBco_Dep_Numero = value; }
        }

        public float Bco_Dep_Monto
        {
            get { return mvarBco_Dep_Monto; }
            set { mvarBco_Dep_Monto = value; }
        }


        public byte Bco_Dep_Ch_Cantidad
        {
            get { return mvarBco_Dep_Ch_Cantidad; }
            set { mvarBco_Dep_Ch_Cantidad = value; }
        }

        public byte Bco_Dep_IdCta
        {
            get { return mvarBco_Dep_IdCta; }
            set { mvarBco_Dep_IdCta = value; }
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
            System.Data.OleDb.OleDbCommand adoCMD = new System.Data.OleDb.OleDbCommand();

            if (IntDepFacturas.Count < 1)
                return;

            if (Origen.ToUpper() == "VER")
            {
                Funciones.oleDbFunciones.ComandoIU(mvarConexion, "DELETE FROM tblInterDeposito WHERE NroInterDeposito='09999-99999999'");
                mvarNroInterDeposito = "09999-99999999";
            }
            else
            {
                rec = Funciones.oleDbFunciones.Comando(mvarConexion, "SELECT TOP 1 right(NroInterdeposito,8) AS NroInterdeposito FROM tblInterDeposito WHERE left(NroInterdeposito,5)=" + Global01.NroUsuario + " ORDER BY NroInterdeposito DESC");

                if (!rec.HasRows)
                {
                    // ES UN CLIENTE
                    if (Global01.miSABOR == Global01.TiposDeCatalogo.Cliente)
                    {
                        mvarNroInterDeposito = Global01.NroUsuario.Trim() + "-C0000001";
                    }
                    else
                    {
                        mvarNroInterDeposito = Global01.NroUsuario.Trim() + "-00000001";
                    }
                }
                else
                {
                    rec.Read();
                    // ES UN CLIENTE
                    if (Global01.miSABOR == Global01.TiposDeCatalogo.Cliente)
                    {
                        mvarNroInterDeposito = Global01.NroUsuario.Trim() + "-C" + (int.Parse(rec["NroInterDeposito"].ToString().Substring(rec["NroInterDeposito"].ToString().Length - 7)) + 1).ToString().PadLeft(7, '0');
                    }
                    else
                    {
                        mvarNroInterDeposito = Global01.NroUsuario.Trim() + "-" + (int.Parse(rec["NroInterDeposito"].ToString().Substring(rec["NroInterDeposito"].ToString().Length - 8)) + 1).ToString().PadLeft(8, '0');
                    }                  
                }
                rec = null;
            }

            adoCMD.Parameters.Add("pNroInterDeposito", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroInterDeposito;
            adoCMD.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = mvarIdCliente;
            adoCMD.Parameters.Add("pBco_Dep_Tipo", System.Data.OleDb.OleDbType.VarChar, 1).Value = mvarBco_Dep_Tipo;
            adoCMD.Parameters.Add("pBco_Dep_Fecha", System.Data.OleDb.OleDbType.Date).Value = mvarBco_Dep_Fecha;
            adoCMD.Parameters.Add("pBco_Dep_Numero", System.Data.OleDb.OleDbType.Integer).Value = mvarBco_Dep_Numero;
            adoCMD.Parameters.Add("pBco_Dep_Monto", System.Data.OleDb.OleDbType.Single).Value = mvarBco_Dep_Monto;
            adoCMD.Parameters.Add("pBco_Dep_Ch_Cantidad", System.Data.OleDb.OleDbType.TinyInt).Value = mvarBco_Dep_Ch_Cantidad;
            adoCMD.Parameters.Add("pBco_Dep_IdCta", System.Data.OleDb.OleDbType.TinyInt).Value = mvarBco_Dep_IdCta;

            adoCMD.Connection = mvarConexion;
            adoCMD.CommandType = System.Data.CommandType.StoredProcedure;
            adoCMD.CommandText = "usp_InterDeposito_add";

            try
            {
                adoCMD.ExecuteNonQuery();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                switch (ex.ErrorCode)
                {
                    case -2147467259:
                        throw new util.errorHandling.RegistroDuplicadoException(ex);
                        //throw (new Exception(ex.Message));   //util.errorHandling.ErrorForm.show();
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw (new Exception(ex.Message));  //util.errorHandling.ErrorForm.show();
            }

            adoCMD = null;

            // Ahora GUARDO los ITEMS de ESTE Devolucion

            int I = 0;

            for (I = 1; I <= IntDepFacturas.Count; I++)
            {
                GuardarItem(Renglon(I).T_Comprobante, Renglon(I).N_Comprobante, Renglon(I).Importe);
            }

            Global01.NroImprimir = mvarNroInterDeposito;

            // Por POLITICA NUESTRA  -- >  SE BORRA
            if (Origen.ToUpper() == "VER")
            {
                // Actualizo Fecha de Transmicion para que no lo mande
                Funciones.oleDbFunciones.ComandoIU(mvarConexion, "EXEC usp_InterDeposito_Transmicion_Upd '" + mvarNroInterDeposito + "'");
            }
            else
            {
                auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.InterDeposito, auditoria.Auditor.AccionesAuditadas.EXITOSO, "cli:" + mvarIdCliente.ToString().Trim().PadLeft(6, '0') + " intD:" + mvarNroInterDeposito + " tot:");
                //Nuevo();
                if (GuardoOK != null)
                {
                    GuardoOK(Global01.NroImprimir);
                }
            }
        }

        public void ADDFacturas(string T_Comprobante, string N_Comprobante, float Importe)
	    {
		    InterDepositoFacturas mvarItem = new InterDepositoFacturas();

		    mvarItem.T_Comprobante = T_Comprobante;
		    mvarItem.N_Comprobante = N_Comprobante;
		    mvarItem.Importe = Importe;

            IntDepFacturas["_" + (IntDepFacturas.Count + 1).ToString()] = mvarItem;

		    mvarItem = null;
	    }

        private void GuardarItem(string pT_Comprobante, string pN_Comprobante, float pImporte)
        {
            if (!(ValidarConexion()))
                return;

            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            cmd.Parameters.Add("pNroInterDeposito", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroInterDeposito;
            cmd.Parameters.Add("pT_Comprobante", System.Data.OleDb.OleDbType.VarChar, 3).Value = pT_Comprobante;
            cmd.Parameters.Add("pN_Comprobante", System.Data.OleDb.OleDbType.VarChar, 12).Value = pN_Comprobante;
            cmd.Parameters.Add("pImporte", System.Data.OleDb.OleDbType.Single).Value = pImporte;

            cmd.Connection = mvarConexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_InterDeposito_Fac_add";
            cmd.ExecuteNonQuery();

            cmd = null;
        }
   
    }
}