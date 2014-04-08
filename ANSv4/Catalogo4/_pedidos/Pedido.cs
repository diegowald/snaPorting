using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Catalogo._devoluciones
{
    public class Pedido
    {
        private const string m_sMODULENAME_ = "clsPedido";
        private string mvarNroPedido;
        private System.DateTime mvarF_Pedido;
        private int mvarIdCliente;
        private byte mvarNroImpresion;
        private string mvarObservaciones;
        private string mvarTransporte;
        private System.Data.OleDb.OleDbConnection mvarConexion;
        private System.Collections.Generic.Dictionary<string, PedidoItem> DetallePedido;
        
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

        public int CantidadItems
        {
            get { return DetallePedido.Count; }
        }

       public Pedido(System.Data.OleDb.OleDbConnection conexion, string NroUsuario, int IDDeCliente)
        {
            Nuevo(conexion, IDDeCliente);
        }

       protected void Nuevo(System.Data.OleDb.OleDbConnection conexion, int IDdeCliente = 0)
       {
           DetallePedido = new Dictionary<string, PedidoItem>();
           mvarNroPedido = "";
           mvarF_Pedido = System.DateTime.Today;
           mvarConexion = conexion;

           if (IDdeCliente > 0)
               mvarIdCliente = IDdeCliente;
       }

       // FUNDAMENTAL PARA QUE TE DE LOS NOMBRES
       public PedidoItem Renglon(int Numero)
       {
           return DetallePedido["_" + Numero];
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

        public System.DateTime F_Pedido
        {
            get { return mvarF_Pedido; }
        }

        public string NroPedido
        {
            get { return mvarNroPedido; }
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

            if (DetallePedido.Count < 1)
                return;

            if (Origen.ToUpper() == "VER")
            {
                Funciones.oleDbFunciones.ComandoIU(mvarConexion, "DELETE FROM tblPedido_Enc WHERE NroPedido='09999-99999999'");
                mvarNroPedido = "09999-99999999";
            }
            else
            {
                if (Global01.NroDocumentoAbierto.Trim().Length>0)
                {
                    Funciones.oleDbFunciones.ComandoIU(Global01.Conexion, "DELETE FROM tblPedido_Enc WHERE NroPedido='" + Global01.NroDocumentoAbierto + "'");
                    Global01.NroDocumentoAbierto = "";
                }

                rec = Funciones.oleDbFunciones.Comando(mvarConexion, "SELECT TOP 1 right(NroPedido,8) AS NroPedido FROM tblPedido_Enc WHERE left(NroPedido,5)=" + Global01.NroUsuario + " ORDER BY NroPedido DESC");
                if (!rec.HasRows)
                {
                    // ES UN CLIENTE
                    if (Global01.miSABOR == Global01.TiposDeCatalogo.Cliente)
                    {
                        mvarNroPedido = Global01.NroUsuario.Trim() + "-C0000001";
                    }
                    else
                    {
                        mvarNroPedido = Global01.NroUsuario.Trim() + "-00000001";
                    }
                }
                else
                {
                    rec.Read();
                    // ES UN CLIENTE
                    if (Global01.miSABOR == Global01.TiposDeCatalogo.Cliente)
                    {
                        mvarNroPedido = Global01.NroUsuario.Trim() + "-C" + (int.Parse(rec["NroPedido"].ToString().Substring(rec["NroPedido"].ToString().Length - 7)) + 1).ToString().PadLeft(7, '0');
                    }
                    else
                    {
                        mvarNroPedido = Global01.NroUsuario.Trim() + "-" + (int.Parse(rec["NroPedido"].ToString().Substring(rec["NroPedido"].ToString().Length - 8)) + 1).ToString().PadLeft(8,'0');
                    };
                }
                rec = null;

            }

            cmd.Parameters.Add("pNroPedido", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroPedido;
            cmd.Parameters.Add("pF_Pedido", System.Data.OleDb.OleDbType.Date).Value = mvarF_Pedido;
            cmd.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = mvarIdCliente;
            cmd.Parameters.Add("pNroImpresion", System.Data.OleDb.OleDbType.Integer).Value = 0;
            cmd.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 128).Value = " "; // mvarObservaciones;
            cmd.Parameters.Add("pTransporte", System.Data.OleDb.OleDbType.VarChar, 128).Value = " "; // mvarTransporte;

            cmd.Connection = mvarConexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
            if (Origen.ToUpper() == "VER")
            {
               Funciones.oleDbFunciones.ComandoIU(mvarConexion, "EXEC usp_Pedido_Transmicion_Upd '" + mvarNroPedido + "'");
            }
            else
            {
                auditoria.Auditor.instance.guardar(auditoria.Auditor.ObjetosAuditados.Pedido, auditoria.Auditor.AccionesAuditadas.EXITOSO, "cli:" + mvarIdCliente.ToString().Trim().PadLeft(6, '0') + " ped:" + mvarNroPedido + " tot:" + string.Format("{0:N2}", wTotal));
                //Nuevo();
                if (GuardoOK != null)
                {
                    GuardoOK(Global01.NroImprimir);
                }
            } 

        }

        public void ADDItem(string IDCatalogo, float Precio, int cantidad, bool Similar, byte Deposito, bool Oferta, string Observaciones)
        {
            //OJO chkEsOfertaBahia===Bahia

            PedidoItem mvarItem = new PedidoItem();

            mvarItem.IDCatalogo = IDCatalogo;
            mvarItem.Precio = Precio;
            mvarItem.cantidad = cantidad;
            mvarItem.Similar = Similar;
            mvarItem.Deposito = Deposito;
            mvarItem.Oferta = Oferta;
            mvarItem.Observaciones = Observaciones;

            mvarItem.Bahia = false;
            
            DetallePedido["_" + (DetallePedido.Count + 1).ToString()] = mvarItem;

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
            cmd.Parameters.Add("pSimilar", System.Data.OleDb.OleDbType.Boolean).Value = Similar;
            cmd.Parameters.Add("pOferta", System.Data.OleDb.OleDbType.Boolean).Value = Oferta;
            cmd.Parameters.Add("pBahia", System.Data.OleDb.OleDbType.Boolean).Value = Bahia;
            cmd.Parameters.Add("pDeposito", System.Data.OleDb.OleDbType.TinyInt).Value = Deposito;
            cmd.Parameters.Add("pPrecio", System.Data.OleDb.OleDbType.Single ).Value = Precio;
            cmd.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 80).Value = Observaciones;

            cmd.Connection = mvarConexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_Pedido_Det_add";
            cmd.ExecuteNonQuery();

            rec = Funciones.oleDbFunciones.Comando(mvarConexion, "SELECT ID FROM CatalogoBAK WHERE ID='" + IDCatalogo + "'");
            if (!rec.HasRows)
            {
                Funciones.oleDbFunciones.ComandoIU(mvarConexion, "EXEC usp_AnexaItemCatalogoBak '" + IDCatalogo + "'");
            }
            else
            {
                Funciones.oleDbFunciones.ComandoIU(mvarConexion, "EXEC usp_CambiaCodigoCatalogoBak '" + IDCatalogo + "'");
            }
            rec = null;
            cmd = null;
     
        }


    }
}