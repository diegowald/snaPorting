using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic;
using System.Data;
using System.Diagnostics;

namespace Catalogo._pedidos
{
    public class Pedido
    {

        private const string m_sMODULENAME_ = "clsPedido";
        private System.Data.OleDb.OleDbConnection Conexion1;

        private string mvarNroPedido;
        private System.DateTime mvarF_Pedido;
        private long mvarIdCliente;
        private byte mvarNroImpresion;
        private string mvarObservaciones;
        private string mvarTransporte;

        private Collection DetallePedido;
        public event GuardoOKEventHandler GuardoOK;
        public delegate void GuardoOKEventHandler(string NroPedido);

        public string Transporte
        {
            get { return mvarTransporte; }
            set { mvarTransporte = value; }
        }

        public string Observaciones
        {
            get { return mvarObservaciones; }
            set { mvarObservaciones = value; }
        }

        public long CantidadItems
        {
            get { return DetallePedido.Count; }
        }

        public void Nuevo(int IDdeCliente = 0)
        {
            DetallePedido = null;
            DetallePedido = new Collection();
            mvarNroPedido = "0";
            mvarF_Pedido = System.DateTime.Today;
            if (IDdeCliente > 0)
                mvarIdCliente = IDdeCliente;
        }

        // FUNDAMENTAL PARA QUE TE DE LOS NOMBRES
        //Pablo
        public PedidoItem Renglon(int Numero)
        {
            return DetallePedido["_" + Numero.ToString()];
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

        public System.DateTime F_Pedido
        {
            get { return mvarF_Pedido; }
        }

        public string NroPedido
        {
            get { return mvarNroPedido; }
        }

        //diego     Private Sub Class_Terminate()
        //diego         DetallePedido = Nothing
        //diego     End Sub

        private bool ValidarConexion()
        {
            return (Conexion1 != null);
        }


        public void Guardar(string Origen = Constants.vbNullString)
        {
            if (!(ValidarConexion()))
                return;

            System.Data.OleDb.OleDbDataReader rec = null;
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            if (DetallePedido.Count < 1)
                return;

            if (Strings.UCase(Origen) == "VER")
            {
                Funciones.oleDbFunciones.ComandoIU(Conexion1, "DELETE FROM tblPedido_Enc WHERE NroPedido='09999-99999999'");
                mvarNroPedido = "09999-99999999";
            }
            else
            {
                if (Strings.Len(Strings.Trim(Global01.NroPDR)) > 0)
                {
                    Funciones.oleDbFunciones.ComandoIU(Conexion1, "DELETE FROM tblPedido_Enc WHERE NroPedido='" + Global01.NroPDR + "'");
                    Global01.NroPDR = "";
                }

                rec = Funciones.oleDbFunciones.Comando(Conexion1, "SELECT TOP 1 right(NroPedido,8) AS NroPedido FROM tblPedido_Enc WHERE left(NroPedido,5)=" + Global01.NroUsuario + " ORDER BY NroPedido DESC");
                if (!rec.HasRows)
                {
                    // ES UN CLIENTE
                    if ((int)Global01.miSABOR == 2)
                    {
                        mvarNroPedido = Strings.Trim(Global01.NroUsuario) + "-C0000001";
                    }
                    else
                    {
                        mvarNroPedido = Strings.Trim(Global01.NroUsuario) + "-00000001";
                    }
                }
                else
                {
                    // ES UN CLIENTE
                    if ((int)Global01.miSABOR==2)
                    {
                        mvarNroPedido = Strings.Trim(Global01.NroUsuario) + "-C" + (Int16.Parse(rec["NroPedido"].ToString().Substring(rec["NroPedido"].ToString().Trim().Length - 7)) + 1).ToString().PadLeft(7,'0');
                    }
                    else
                    {
                        mvarNroPedido = Strings.Trim(Global01.NroUsuario) + "-" + (Int16.Parse(rec["NroPedido"].ToString().Substring(rec["NroPedido"].ToString().Trim().Length - 7)) + 1).ToString().PadLeft(8, '0');
                    }
                }
                rec = null;

            }

            cmd.Parameters.Add("pNroPedido", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroPedido;
            cmd.Parameters.Add("pF_Pedido", System.Data.OleDb.OleDbType.Date).Value = mvarF_Pedido;
            cmd.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = mvarIdCliente;
            cmd.Parameters.Add("pNroImpresion", System.Data.OleDb.OleDbType.Integer).Value = 0;
            cmd.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 128).Value = mvarObservaciones;
            cmd.Parameters.Add("pTransporte", System.Data.OleDb.OleDbType.VarChar, 128).Value = mvarTransporte;

            cmd.Connection = Conexion1;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Pedido_Enc_add";
            cmd.ExecuteNonQuery();

            cmd = null;

            // Ahora GUARDO los ITEMS de ESTE pedido

            int I = 0;
            double wTotal = 0;
            wTotal = 0;

            for (I = 1; I <= DetallePedido.Count; I++)
            {
                GuardarItem(Renglon(I).IDCatalogo, Renglon(I).cantidad, Renglon(I).Similar, Renglon(I).Bahia, Renglon(I).Oferta, Renglon(I).Deposito, Renglon(I).Precio, Renglon(I).Observaciones);
                wTotal = wTotal + (Renglon(I).cantidad * Renglon(I).Precio);
            }

            Global01.NroImprimir = mvarNroPedido;

            // Por POLITICA NUESTRA  -- >  SE BORRA
            if (Strings.UCase(Origen) == "VER")
            {
               Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_Pedido_Transmicion_Upd '" + mvarNroPedido + "'");
            }
            else
            {
                //vg.auditor.Guardar(ObjetosAuditados.Pedido, AccionesAuditadas.EXITOSO, "cli:" + Strings.Format(mvarIdCliente, "000000") + " ped:" + mvarNroPedido + " tot:" + Strings.Format(wTotal, "fixed"));
                Nuevo();
                if (GuardoOK != null)
                {
                    GuardoOK(Global01.NroImprimir);
                }
            } 

        }

        public void ADDItem(string IDCatalogo, int cantidad, bool Similar, bool Bahia, bool Oferta, byte Deposito, float Precio, string Observaciones)
        {
            //OJO chkEsOfertaBahia===Bahia

            PedidoItem mvarItem = new PedidoItem();

            mvarItem.IDCatalogo = IDCatalogo;
            mvarItem.cantidad = cantidad;
            mvarItem.Similar = Similar;
            mvarItem.Bahia = Bahia;
            mvarItem.Oferta = Oferta;
            mvarItem.Deposito = Deposito;
            mvarItem.Precio = Precio;
            mvarItem.Observaciones = Observaciones;

            DetallePedido.Add(mvarItem, "_" + DetallePedido.Count + 1);

            mvarItem = null;

        }


        private void GuardarItem(string IDCatalogo, int cantidad, bool Similar, bool Bahia, bool Oferta, byte Deposito, float Precio, string Observaciones)
        {
            if (!(ValidarConexion()))
                return;

            System.Data.OleDb.OleDbDataReader rec = null;
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            cmd.Parameters.Add("pNroPedido", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroPedido;
            cmd.Parameters.Add("pIdCatalogo", System.Data.OleDb.OleDbType.VarChar, 38).Value = IDCatalogo;
            cmd.Parameters.Add("pCantidad", System.Data.OleDb.OleDbType.Integer).Value = cantidad;
            cmd.Parameters.Add("pSimilar", SqlDbType.Bit).Value = Similar;
            cmd.Parameters.Add("pOferta", SqlDbType.Bit).Value = Oferta;
            cmd.Parameters.Add("pBahia", SqlDbType.Bit).Value = Bahia;
            cmd.Parameters.Add("pDeposito", System.Data.OleDb.OleDbType.TinyInt).Value = Deposito;
            cmd.Parameters.Add("pPrecio", System.Data.OleDb.OleDbType.Single ).Value = Precio;
            cmd.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 80).Value = Observaciones;

            cmd.Connection = Conexion1;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "usp_Pedido_Det_add";
            cmd.ExecuteNonQuery();

            rec = Funciones.oleDbFunciones.Comando(Conexion1, "SELECT ID FROM CatalogoBAK WHERE ID='" + IDCatalogo + "'");
            if (!rec.HasRows)
            {
                Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_AnexaItemCatalogoBak '" + IDCatalogo + "'");
            }
            else
            {
                Funciones.oleDbFunciones.ComandoIU(Conexion1, "EXEC usp_CambiaCodigoCatalogoBak '" + IDCatalogo + "'");
            }
            rec = null;
            cmd = null;
     
        }


    }
}