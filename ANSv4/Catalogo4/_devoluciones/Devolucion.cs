using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._devoluciones
{
    public class Devolucion
    {

        //    Private Conexion As OleDbConnection
        private System.Data.OleDb.OleDbConnection Conexion1;
        //copia local
        private string mvarNroDevolucion;
        //copia local
        private System.DateTime mvarF_Devolucion;
        //copia local
        private long mvarIdCliente;
        //copia local
        private byte mvarNroImpresion;
        //copia local
        private string mvarObservaciones;


        private System.Collections.Generic.Dictionary<string, DevolucionItem> DetalleDevolucion;
        public event GuardoOKEventHandler GuardoOK;
        public delegate void GuardoOKEventHandler(string NroDevolucion);

        private string _NroUsuario;

        public Devolucion(string NroUsuario, long IDDeCliente)
        {
            Nuevo(IDDeCliente);
        }
        public string Observaciones
        {
            get { return mvarObservaciones; }
            set { mvarObservaciones = value; }
        }

        public long CantidadItems
        {
            get { return DetalleDevolucion.Count; }
        }

        protected void Nuevo(long IDdeCliente = 0)
        {
            DetalleDevolucion = new Dictionary<string, DevolucionItem>();
            mvarNroDevolucion = "";
            mvarF_Devolucion = System.DateTime.Today;
            if (IDdeCliente > 0)
                mvarIdCliente = IDdeCliente;
        }

        // FUNDAMENTAL PARA QUE TE DE LOS NOMBRES
        public DevolucionItem Renglon(int Numero)
        {
            return DetalleDevolucion["_" + Numero];
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

        public System.DateTime F_Devolucion
        {
            get { return mvarF_Devolucion; }
        }

        public string NroDevolucion
        {
            get { return mvarNroDevolucion; }
        }

        private bool ValidarConexion()
        {
            return (Conexion1 != null);
        }


        public void Guardar(string Origen)
        {
            if (!(ValidarConexion()))
                return;


            System.Data.OleDb.OleDbDataReader rec = null;
            System.Data.OleDb.OleDbCommand adoCMD = new System.Data.OleDb.OleDbCommand();

            if (DetalleDevolucion.Count < 1)
                return;

            if (Origen.ToUpper() == "VER")
            {
                Funciones.oleDbFunciones.ComandoIU(Conexion1, "DELETE FROM tblDevolucion_Enc WHERE NroDevolucion='09999-99999999'");
                mvarNroDevolucion = "09999-99999999";
            }
            else
            {
                rec = Funciones.oleDbFunciones.Comando(Conexion1, "SELECT TOP 1 right(NroDevolucion,8) AS NroDevolucion FROM tblDevolucion_Enc WHERE left(NroDevolucion,5)=" + _NroUsuario + " ORDER BY NroDevolucion DESC");
                if (!rec.HasRows)
                {
                    // ES UN CLIENTE
                    if (Global01.miSABOR == Global01.TiposDeCatalogo.Cliente)
                    {
                        mvarNroDevolucion = _NroUsuario.Trim() + "-C0000001";
                    }
                    else
                    {
                        mvarNroDevolucion = _NroUsuario.Trim() + "-00000001";
                    }
                }
                else
                {
                    // ES UN CLIENTE
                    if (Global01.miSABOR == Global01.TiposDeCatalogo.Cliente)
                    {
                        mvarNroDevolucion = _NroUsuario.Trim() + "-C" + String.Format("0000000", int.Parse(rec["NroDevolucion"].ToString().Substring(rec["NroDevolucion"].ToString().Length - 7) + 1));
                    }
                    else
                    {
                        mvarNroDevolucion = _NroUsuario.Trim() + "-" + String.Format("00000000", int.Parse(rec["NroDevolucion"].ToString().Substring(rec["NroDevolucion"].ToString().Length - 7) + 1));
                    }
                }
                rec = null;
            }

            adoCMD.Parameters.Add("pNroDevolucion", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroDevolucion;
            adoCMD.Parameters.Add("pF_Devolucion", System.Data.OleDb.OleDbType.Date).Value = mvarF_Devolucion;
            adoCMD.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = mvarIdCliente;
            adoCMD.Parameters.Add("pNroImpresion", System.Data.OleDb.OleDbType.Integer).Value = 0;
            adoCMD.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 200).Value = mvarObservaciones;

            adoCMD.Connection = Conexion1;
            adoCMD.CommandType = System.Data.CommandType.StoredProcedure;
            adoCMD.CommandText = "usp_Devolucion_Enc_add";
            adoCMD.ExecuteNonQuery();

            adoCMD = null;

            // Ahora GUARDO los ITEMS de ESTE Devolucion

            int I = 0;

            for (I = 1; I <= DetalleDevolucion.Count; I++)
            {
                GuardarItem(Renglon(I).IDCatalogo, Renglon(I).cantidad, Renglon(I).Deposito, Renglon(I).Factura, Renglon(I).TipoDev, Renglon(I).Vehiculo, Renglon(I).Modelo, Renglon(I).Motor, Renglon(I).KM, Renglon(I).Observaciones);
            }

            Global01.NroImprimir = mvarNroDevolucion;

            // Por POLITICA NUESTRA  -- >  SE BORRA
            if (Origen.ToUpper() == "VER")
            {
                // Actualizo Fecha de Transmicion para que no lo mande
                Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_Devolucion_Transmicion_Upd '" + mvarNroDevolucion + "'");
            }
            else
            {
                // TODO: implementar auditoria
                //vg.auditor.Guardar(ObjetosAuditados.Devoluciones, AccionesAuditadas.EXITOSO, "cli:" + Strings.Format(mvarIdCliente, "000000") + " ped:" + mvarNroDevolucion + " tot:");
                Nuevo();
                if (GuardoOK != null)
                {
                    GuardoOK(Global01.NroImprimir);
                }
            }
        }


        public void ADDItem(string IDCatalogo, int cantidad, byte Deposito, string Factura, byte TipoDev, string Vehiculo, string Modelo, string Motor, string KM, string Observaciones)
        {
            DevolucionItem mvarItem = new DevolucionItem();

            mvarItem.IDCatalogo = IDCatalogo;
            mvarItem.cantidad = cantidad;
            mvarItem.Deposito = Deposito;
            mvarItem.Factura = Factura;
            mvarItem.TipoDev = TipoDev;
            mvarItem.Vehiculo = Vehiculo;
            mvarItem.Modelo = Modelo;
            mvarItem.Motor = Motor;
            mvarItem.KM = KM;
            mvarItem.Observaciones = Observaciones;

            DetalleDevolucion["_" + (DetalleDevolucion.Count + 1).ToString()] = mvarItem;

            mvarItem = null;

        }


        private void GuardarItem(string IDCatalogo, int cantidad, byte Deposito, string Factura, byte TipoDev, string Vehiculo, string Modelo, string Motor, string KM, string Observaciones)
        {
            if (!(ValidarConexion()))
                return;

            System.Data.OleDb.OleDbDataReader rec = null;
            System.Data.OleDb.OleDbCommand adoCMD = new System.Data.OleDb.OleDbCommand();

            adoCMD.Parameters.Add("pNroDevolucion", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroDevolucion;
            adoCMD.Parameters.Add("pIdCatalogo", System.Data.OleDb.OleDbType.VarChar, 38).Value = IDCatalogo;
            adoCMD.Parameters.Add("pCantidad", System.Data.OleDb.OleDbType.Integer).Value = cantidad;
            adoCMD.Parameters.Add("pDeposito", System.Data.OleDb.OleDbType.TinyInt).Value = Deposito;
            adoCMD.Parameters.Add("pFactura", System.Data.OleDb.OleDbType.VarChar, 15).Value = Factura;
            adoCMD.Parameters.Add("pTipoDev", System.Data.OleDb.OleDbType.TinyInt).Value = TipoDev;
            adoCMD.Parameters.Add("pVehiculo", System.Data.OleDb.OleDbType.VarChar, 32).Value = Vehiculo;
            adoCMD.Parameters.Add("pModelo", System.Data.OleDb.OleDbType.VarChar, 32).Value = Modelo;
            adoCMD.Parameters.Add("pMotor", System.Data.OleDb.OleDbType.VarChar, 32).Value = Motor;
            adoCMD.Parameters.Add("pKm", System.Data.OleDb.OleDbType.VarChar, 10).Value = KM;
            adoCMD.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 128).Value = Observaciones;

            adoCMD.Connection = Conexion1;
            adoCMD.CommandType = System.Data.CommandType.StoredProcedure;
            adoCMD.CommandText = "usp_Devolucion_Det_add";
            adoCMD.ExecuteNonQuery();

            rec = Funciones.oleDbFunciones.Comando(Conexion1, "SELECT ID FROM CatalogoBAK WHERE ID='" + IDCatalogo + "'");
            if (!rec.HasRows)
            {
                Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_AnexaItemCatalogoBak '" + IDCatalogo + "'");
            }
            else
            {
                Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_CambiaCodigoCatalogoBak '" + IDCatalogo + "'");
            }
        }
    }
}