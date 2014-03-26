using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._recibos
{
    public class Recibo
    {
        // Define como se llama este modulo para el control de errores

        private System.Data.OleDb.OleDbConnection Conexion1;
        private string mvarNroRecibo;
        private System.DateTime mvarF_Recibo;
        private long mvarIdCliente;
        private bool mvarBahia;
        private float mvarTotal;
        private float mvarPercepciones;
        private byte mvarNroImpresion;
        private System.DateTime mvarF_Transmicion;

        private string mvarObservaciones;
        //BarCode: lo genero dinamicamente en v_Recibo_Enc con el Alias CRC
        //BarCode:   Recortar(CCadena(tblRecibo_Enc!F_Recibo)) & _
        //        Recortar(CCadena(tblRecibo_Enc!NroRecibo)) & _
        //        Recortar(CCadena(tblRecibo_Enc!Total)) & _
        //        Recortar(CCadena(v_Recibo_cItems!Items)) & _
        //        Recortar(CCadena(tblRecibo_Enc!IDCliente)) & _
        //        Recortar(CCadena(tblRecibo_Enc!NroImpresion))

        private System.Collections.Generic.Dictionary<string, ReciboItem> DetalleRecibo;
        private System.Collections.Generic.Dictionary<string, AplicacionItem> AplicacionRecibo;
        private System.Collections.Generic.Dictionary<string, DeducirItem> DeducirRecibo;

        public event GuardoOKEventHandler GuardoOK;
        public delegate void GuardoOKEventHandler(string nroRecibo);

        private string _NroUsuario;
        public Recibo(string NroUsuario, long IDDeCliente = 0)
        {
            _NroUsuario = NroUsuario;
            Nuevo(IDDeCliente);
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

        public bool Bahia
        {

            get { return mvarBahia; }
            set { mvarBahia = value; }
        }

        public float Total
        {
            get { return mvarTotal; }
            set { mvarTotal = value; }
        }

        public float Percepciones
        {
            get { return mvarPercepciones; }
            set { mvarPercepciones = value; }
        }

        public long CantidadItemsDedu
        {
            get { return DeducirRecibo.Count; }
        }

        public long CantidadItemsApli
        {
            get { return AplicacionRecibo.Count; }
        }

        public long CantidadItems
        {
            get { return DetalleRecibo.Count; }
        }


        protected void Nuevo(long IDdeCliente = 0)
        {
            DetalleRecibo = new Dictionary<string, ReciboItem>();
            AplicacionRecibo = new Dictionary<string, AplicacionItem>();
            DeducirRecibo = new Dictionary<string, DeducirItem>();

            mvarNroRecibo = "";
            mvarF_Recibo = System.DateTime.Today;
            if (IDdeCliente > 0)
                mvarIdCliente = IDdeCliente;
        }

        // FUNDAMENTAL PARA QUE TE DE LOS NOMBRES
        public DeducirItem RenglonDedu(int Numero)
        {
            return DeducirRecibo["_" + Numero];
        }

        public AplicacionItem RenglonApli(int Numero)
        {
            return AplicacionRecibo["_" + Numero];
        }

        public ReciboItem Renglon(int Numero)
        {
            return DetalleRecibo["_" + Numero];
        }


        public void Leer(long NrodeRecibo)
        {
            if (!(ValidarConexion()))
                return;

            System.Data.OleDb.OleDbDataReader rec = null;
            rec = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_Recibo_Det " + NrodeRecibo);
            if (rec.HasRows)
            {
                while (rec.Read())
                {
                    ADDItem((byte) rec["TipoValor"], 
                        (float) rec["Importe"],
                        (DateTime) rec["F_EmiCheque"],
                        (DateTime) rec["F_CobroCheque"], 
                        rec["N_Cheque"].ToString(), 
                        rec["NrodeCuenta"].ToString(),
                        (int) rec["Banco"], 
                        rec["Cpa"].ToString(), 
                        (bool) rec["ChequePropio"],
                        (float) rec["T_Cambio"]);
                }
                rec = Funciones.oleDbFunciones.Comando(Conexion1, "EXECUTE v_Recibo_Enc " + NrodeRecibo);
                if (rec.HasRows)
                {
                    rec.Read();
                    mvarNroRecibo = NrodeRecibo.ToString();
                    mvarF_Recibo = (DateTime) rec["F_Recibo"];
                    mvarIdCliente = (long) rec["IdCliente"];
                    mvarBahia = (bool) rec["Bahia"];
                    mvarTotal = (float) rec["Total"];
                    mvarNroImpresion = (byte) rec["NroImpresion"];
                    mvarF_Transmicion = (DateTime) rec["F_Transmicion"];
                    mvarObservaciones = rec["Observaciones"].ToString();
                    mvarPercepciones = (float) rec["Percepciones"];
                }
            }
        }

        public System.Data.OleDb.OleDbConnection Conexion
        {
            set { Conexion1 = value; }
        }

        public byte NroImpresion
        {
            get { return mvarNroImpresion; }
            set { mvarNroImpresion = value; }
        }

        public long IdCliente
        {
            get { return mvarIdCliente; }
        }

        public System.DateTime F_Recibo
        {
            get { return mvarF_Recibo; }
        }

        public string nroRecibo
        {
            get { return mvarNroRecibo; }
        }

        //diego     Private Sub Class_Terminate()
        //diego         DetalleRecibo = Nothing
        //diego     End Sub

        private bool ValidarConexion()
        {
            return (Conexion1 != null);
        }


        public void Guardar(string Origen)
        {
            if (!(ValidarConexion()))
                return;

            System.Data.OleDb.OleDbDataReader rec = null;
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            if (DetalleRecibo.Count < 1)
                return;

            if (Origen.ToUpper() == "VER")
            {
                Funciones.oleDbFunciones.ComandoIU(Conexion1, "DELETE FROM tblRecibo_Enc WHERE NroRecibo='09999-99999999'");
                mvarNroRecibo = "09999-99999999";
            }
            else
            {
                rec = Funciones.oleDbFunciones.Comando(Conexion1, "SELECT TOP 1 right(NroRecibo,8) AS NroRecibo FROM tblRecibo_Enc WHERE left(NroRecibo,5)=" + _NroUsuario + " ORDER BY NroRecibo DESC");
                if (!rec.HasRows)
                {
                    mvarNroRecibo = _NroUsuario.Trim() + "-00000001";
                }
                else
                {
                    mvarNroRecibo = _NroUsuario.Trim() + "-" + String.Format("00000000", (int) rec["nroRecibo"] + 1);
                }
                rec = null;
            }

            cmd.Parameters.Add("pNroRecibo", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroRecibo;
            cmd.Parameters.Add("pF_Recibo", System.Data.OleDb.OleDbType.Date).Value = mvarF_Recibo;
            cmd.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = mvarIdCliente;
            cmd.Parameters.Add("pBahia", System.Data.OleDb.OleDbType.Boolean).Value = mvarBahia;
            cmd.Parameters.Add("pTotal", System.Data.OleDb.OleDbType.Single).Value = mvarTotal;
            cmd.Parameters.Add("pNroImpresion", System.Data.OleDb.OleDbType.Integer).Value = 0;
            cmd.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 200).Value = mvarObservaciones;
            cmd.Parameters.Add("pPercepciones", System.Data.OleDb.OleDbType.Single).Value = mvarPercepciones;

            cmd.Connection = Conexion1;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_Recibo_Enc_add";
            cmd.ExecuteNonQuery();

            cmd = null;

            // Ahora GUARDO los ITEMS de ESTE Recibo
            int I = 0;

            for (I = 1; I <= DetalleRecibo.Count; I++)
            {
                GuardarItem(Renglon(I).TipoValor, Renglon(I).Importe, Renglon(I).F_EmiCheque, Renglon(I).F_CobroCheque, Renglon(I).N_Cheque, Renglon(I).NrodeCuenta, Renglon(I).Banco, Renglon(I).Cpa, Renglon(I).ChequePropio, Renglon(I).T_Cambio);
            }

            for (I = 1; I <= AplicacionRecibo.Count; I++)
            {
                GuardarItemApli(RenglonApli(I).Concepto, RenglonApli(I).Importe);
            }

            for (I = 1; I <= DeducirRecibo.Count; I++)
            {
                GuardarItemDedu(RenglonDedu(I).Concepto, RenglonDedu(I).Importe, RenglonDedu(I).Porcentaje, RenglonDedu(I).DeduAlResto);
            }

            Global01.NroImprimir = mvarNroRecibo;

            // Por POLITICA NUESTRA  -- >  SE BORRA
            if (Origen.ToUpper() == "VER")
            {
                // Actualizo Fecha de Transmicion para que no lo mande
                Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_Recibo_Transmicion_Upd '" + mvarNroRecibo + "'");
            }
            else
            {
                // TODO: completar auditoria
                //vg.auditor.Guardar(ObjetosAuditados.Recibo, AccionesAuditadas.EXITOSO, "cli:" + Strings.Format(mvarIdCliente, "000000") + " ped:" + mvarNroRecibo + " tot:" + Strings.Format(mvarTotal, "fixed"));
                Nuevo();
                if (GuardoOK != null)
                {
                    GuardoOK(Global01.NroImprimir);
                }
            }

        }


        public void ADDItem(byte TipoValor, float Importe, System.DateTime F_EmiCheque, System.DateTime F_CobroCheque, string N_Cheque, string NrodeCuenta, int Banco, string Cpa, bool ChequePropio, float T_Cambio)
        {
            ReciboItem mvarItem = new ReciboItem();

            mvarItem.TipoValor = TipoValor;
            mvarItem.Importe = Importe;
            mvarItem.T_Cambio = T_Cambio;
            mvarItem.F_EmiCheque = F_EmiCheque;
            mvarItem.F_CobroCheque = F_CobroCheque;
            mvarItem.N_Cheque = N_Cheque;
            mvarItem.NrodeCuenta = NrodeCuenta;
            mvarItem.Banco = Banco;
            mvarItem.Cpa = Cpa;
            mvarItem.ChequePropio = ChequePropio;

            DetalleRecibo["_" + (DetalleRecibo.Count + 1).ToString()] = mvarItem;
        }


        public void ADDItemDedu(string Concepto, float Importe, bool Porcentaje, bool DeduAlResto)
        {
            DeducirItem mvarItemDedu = new DeducirItem();

            mvarItemDedu.Concepto = Concepto;
            mvarItemDedu.Importe = Importe;
            mvarItemDedu.Porcentaje = Porcentaje;
            mvarItemDedu.DeduAlResto = DeduAlResto;
            DeducirRecibo["_" + (DeducirRecibo.Count + 1).ToString()] = mvarItemDedu;
        }


        public void ADDItemApli(string Concepto, float Importe)
        {
            AplicacionItem mvarItemApli = new AplicacionItem();

            mvarItemApli.Concepto = Concepto;
            mvarItemApli.Importe = Importe;

            AplicacionRecibo["_" + (AplicacionRecibo.Count + 1).ToString()] = mvarItemApli;
        }


        private void GuardarItem(byte TipoValor, float Importe, System.DateTime F_EmiCheque, System.DateTime F_CobroCheque, string N_Cheque, string NrodeCuenta, int Banco, string Cpa, bool ChequePropio, float T_Cambio)
        {
            if (!(ValidarConexion()))
                return;

            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            cmd.Parameters.Add("pNroRecibo", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroRecibo;
            cmd.Parameters.Add("pTipoValor", System.Data.OleDb.OleDbType.TinyInt).Value = TipoValor;
            cmd.Parameters.Add("pImporte", System.Data.OleDb.OleDbType.Single).Value = Importe;
            cmd.Parameters.Add("pF_EmiCheque", System.Data.OleDb.OleDbType.Date).Value = F_EmiCheque;
            cmd.Parameters.Add("pF_CobroCheque", System.Data.OleDb.OleDbType.Date).Value = F_CobroCheque;
            cmd.Parameters.Add("pN_Cheque", System.Data.OleDb.OleDbType.VarChar, 50).Value = N_Cheque;
            cmd.Parameters.Add("pN_Cuenta", System.Data.OleDb.OleDbType.VarChar, 50).Value = NrodeCuenta;
            cmd.Parameters.Add("pIdBanco", System.Data.OleDb.OleDbType.Integer).Value = Banco;
            cmd.Parameters.Add("pCpa", System.Data.OleDb.OleDbType.VarChar, 10).Value = Cpa;
            cmd.Parameters.Add("pChequePropio", System.Data.OleDb.OleDbType.Boolean).Value = ChequePropio;
            cmd.Parameters.Add("pT_Cambio", System.Data.OleDb.OleDbType.Single).Value = T_Cambio;

            cmd.Connection = Conexion1;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_Recibo_Det_add";
            cmd.ExecuteNonQuery();
        }


        private void GuardarItemApli(string Concepto, float Importe)
        {
            if (!(ValidarConexion()))
                return;

            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            cmd.Parameters.Add("pNroRecibo", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroRecibo;
            cmd.Parameters.Add("pConcepto", System.Data.OleDb.OleDbType.VarChar, 50).Value = Concepto;
            cmd.Parameters.Add("pImporte", System.Data.OleDb.OleDbType.Single).Value = Importe;

            cmd.Connection = Conexion1;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_Recibo_App_add";
            cmd.ExecuteNonQuery();
        }


        private void GuardarItemDedu(string Concepto, float Importe, bool Porcentaje, bool DeduAlResto)
        {
            if (!(ValidarConexion()))
                return;

            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            cmd.Parameters.Add("pNroRecibo", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroRecibo;
            cmd.Parameters.Add("pConcepto", System.Data.OleDb.OleDbType.VarChar, 50).Value = Concepto;
            cmd.Parameters.Add("pImporte", System.Data.OleDb.OleDbType.Single).Value = Importe;
            cmd.Parameters.Add("pPorcentaje", System.Data.OleDb.OleDbType.Boolean).Value = Porcentaje;
            cmd.Parameters.Add("pDeduAlResto", System.Data.OleDb.OleDbType.Boolean).Value = DeduAlResto;

            cmd.Connection = Conexion1;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_Recibo_Deducir_add";
            cmd.ExecuteNonQuery();
        }
    }
}
