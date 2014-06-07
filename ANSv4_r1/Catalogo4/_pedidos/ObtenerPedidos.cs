using System;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo._pedidos
{
    class ObtenerPedidos
    {
        // Define como se llama este modulo para el control de errores
        
        private System.Data.OleDb.OleDbTransaction _TranActiva = null;

        private PedidosWS.Pedidos cliente;
        private bool webServiceInicializado;
        private string _MacAddress;
        private string _ipAddress;

        //    Public Event SincronizarNovedadesProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)
        //    Public Event SincronizarNovedadesProgresoParcial(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)

        private System.Data.OleDb.OleDbConnection conexion;

        public ObtenerPedidos(string MacAddress, string ipAddress)
        {
            inicializar(MacAddress, ipAddress);
            conexion = Global01.Conexion;
        }

        public bool inicializado
        {
            get
            {
                return webServiceInicializado;
            }
        }

        private void inicializar(string MacAddress, string ipAddress)
        {
            bool conectado = util.network.IPCache.instance.conectado;

            if (!webServiceInicializado)
            {
                if (conectado)
                {
                    cliente = new PedidosWS.Pedidos();
                    cliente.Url = "http://" + ipAddress + "/wsCatalogo4/Pedidos.asmx?wsdl";
                    if (Global01.proxyServerAddress != "0.0.0.0")
                    {
                        cliente.Proxy = new System.Net.WebProxy(Global01.proxyServerAddress);
                    }
                    _MacAddress = MacAddress;
                    _ipAddress = ipAddress;
                    webServiceInicializado = true;
                }
                else
                {
                    webServiceInicializado = false;
                }
            }
        }

        private void sincroPedidosCompletada(ref bool cancel)
        {

            //if (util.network.IPCache.instance.conectado)
            //{
            //    // conexion no valida.
            //    cancel = true;
            //    return;
            //}

            //cliente.SincronizacionNovedadesCompletada(_MacAddress);

            return;
        }

        internal void sincronizarPedidos()
        {
            try
            {
                bool cancel = false;

                if (_TranActiva== null)
                {
                    //@ _TranActiva = conexion.BeginTransaction();
                }

                Catalogo.varios.complexMessage msg;
                msg.progress1 = new util.Pair<string, float>("Sincronizando Pedidos OdN...", 0);
                msg.progress2 = new util.Pair<string, float>("", 0);

                if (!webServiceInicializado)
                {
                    cancel = true;
                }

                if (!cancel)
                {
                    msg.progress1.second = 30;
                    this.requestCancel(ref cancel);
                }

                if (!cancel)
                {
                    //rec = Funciones.oleDbFunciones.Comando(mvarConexion, "SELECT TOP 1 right(NroPedido,8) AS NroPedido FROM tblPedido_Enc WHERE left(NroPedido,5)=" + Global01.NroUsuario + " ORDER BY NroPedido DESC");
                    string IdUltimoPedido =  Funciones.oleDbFunciones.Comando(conexion, "SELECT TOP 1 NroPedido FROM tblPedido_OdN ORDER BY NroPedido DESC", "NroPedido", false, _TranActiva);

                    sincronizarTodosLosPedidos(IdUltimoPedido, ref cancel, msg);
                }

                if (!cancel)
                {
                    msg.progress1.first = "Finalizando Sincronización de Pedidos OdN";
                    msg.progress1.second = 90;
                    this.requestCancel(ref cancel);
                }

                if (!cancel)
                {
                    sincroPedidosCompletada(ref cancel);
                    this.requestCancel(ref cancel);
                }

                if (cancel)
                {
                    if (_TranActiva!= null)
                    {
                        _TranActiva.Rollback();
                        _TranActiva = null;
                    }
                    msg.progress1.first="Sincronización de Pedidos OdN con Errores";
                    msg.progress1.second=100;
                    this.requestCancel(ref cancel);
                }
                else
                {
                    if (_TranActiva!= null)
                    {
                        _TranActiva.Commit();
                        _TranActiva = null;
                    }

                    Catalogo.Funciones.oleDbFunciones.ComandoIU(conexion, "EXEC usp_Pedidos_Anexar");
                    msg.progress1.first = "Sincronización de Pedidos Finalizada";
                    msg.progress1.second = 100;
                }
            }
            catch (Exception ex)
            {
                //.ErrorForm.show():
                if (_TranActiva!= null)
                {
                    _TranActiva.Rollback();
                    _TranActiva = null;
                }
                util.errorHandling.ErrorLogger.LogMessage(ex);

                throw ex;  //util.errorHandling.ErrorForm.show();
            }
            finally
            {
                _TranActiva = null;
            }
        }

        private void Pedidos_OdN_Add(System.Data.OleDb.OleDbConnection Conexion,
                                   int ID, DateTime Fecha, string Descripcion, string Destino, 
                                   string Origen, string Tipo, DateTime F_Inicio, DateTime F_Fin, 
                                   string N_Archivo, string url, string zonas, byte activo)
        {
            try
            {

                //if (N_Archivo == "banner.swf") { activo = 0; }

                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

                cmd.Parameters.Add("pID", System.Data.OleDb.OleDbType.Integer).Value = ID;
                cmd.Parameters.Add("pFecha", System.Data.OleDb.OleDbType.Date).Value = Fecha;
                cmd.Parameters.Add("pDescripcion", System.Data.OleDb.OleDbType.VarChar, 128).Value = Descripcion;
                cmd.Parameters.Add("pDestino", System.Data.OleDb.OleDbType.VarChar, 10).Value = Destino;
                cmd.Parameters.Add("pOrigen", System.Data.OleDb.OleDbType.VarChar, 15).Value = Origen;
                cmd.Parameters.Add("pTipo", System.Data.OleDb.OleDbType.VarChar, 15).Value = Tipo;
                cmd.Parameters.Add("pF_Inicio", System.Data.OleDb.OleDbType.Date).Value = F_Inicio;
                cmd.Parameters.Add("pF_Fin", System.Data.OleDb.OleDbType.Date).Value = F_Fin;
                cmd.Parameters.Add("pN_Archivo", System.Data.OleDb.OleDbType.VarChar, 64).Value = N_Archivo;
                cmd.Parameters.Add("pUrl", System.Data.OleDb.OleDbType.VarChar, 128).Value = url;
                cmd.Parameters.Add("pZonas", System.Data.OleDb.OleDbType.VarChar, 128).Value = zonas;
                cmd.Parameters.Add("pActivo", System.Data.OleDb.OleDbType.TinyInt).Value = activo;

                cmd.Connection = Conexion;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_Pedidos_OdN_add";

                cmd.ExecuteNonQuery();

                cmd = null;
            }

            catch (System.Data.OleDb.OleDbException ex)
            {

                if (ex.ErrorCode ==-2147467259)
                {
                    //        If Err.Number = -2147467259 Then
                    //            ' El registro está duplicado... debo borrar el registro e intentar nuevamente
                    //Funciones.oleDbFunciones.ComandoIU(Conexion, "DELETE FROM ansNovedades WHERE ID = " + ID.ToString());
                    Pedidos_OdN_Add(Conexion, ID, Fecha, Descripcion, Destino, Origen, Tipo, F_Inicio, F_Fin, N_Archivo, url, zonas, activo);
                }
                else
                {
                    util.errorHandling.ErrorLogger.LogMessage(ex);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                throw ex;  //util.errorHandling.ErrorForm.show();
            }
        }

        private void sincronizarTodosLosPedidos(string IdUltimoPedido, ref bool cancel, Catalogo.varios.complexMessage msg)
        {
            if (!util.network.IPCache.instance.conectado)
            {
                cancel = true;
                return;
            }

            msg.progress1.first = "Sincronizando Pedidos OdN ...";
            msg.progress1.second = 40;
            msg.progress2.first = "Importando Mis Pedidos";
            msg.progress2.second = 0;
            this.requestCancel(ref cancel);
            if (cancel)
            {
                return;
            }
            try
            {
                // Obtengo la cantidad de modificaciones a importar
                long cantidadAImportar = cliente.GetTodosLosPedidos_Cantidad(_MacAddress, IdUltimoPedido);
                long restanImportar = cantidadAImportar;

                string lastID = IdUltimoPedido;
                long cantImportada = 0;

                while (restanImportar > 0)
                {
                    msg.progress2.first = "Sincronizando Pedidos ...";
                    msg.progress2.second = ((float)cantidadAImportar - restanImportar) / cantidadAImportar * 100;
              
                    System.Data.DataSet ds = cliente.GetTodosLosPedidos_Datos_Registros(_MacAddress, lastID);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        restanImportar -= ds.Tables[0].Rows.Count;

                        //    'diego          On Error GoTo Proximo
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            System.Data.DataRow row = ds.Tables[0].Rows[i];
                            Pedidos_OdN_Add(conexion,
                                        row["NroPedido"].ToString(),
                                        (DateTime)row["Fecha"],
                                        row["Descripcion"].ToString(),
                                        row["Destino"].ToString(),
                                        row["Origen"].ToString(),
                                        row["Tipo"].ToString(),
                                        (DateTime)row["F_Inicio"],
                                        (DateTime)row["F_Fin"],
                                        row["N_Archivo"].ToString(),
                                        row["url"].ToString(),
                                        row["zonas"].ToString(),
                                        (byte)row["Activo"]);

                            cantImportada++;
                            if (cantImportada % 31 == 0)
                            {
                                msg.progress2.second = ((float)cantidadAImportar - restanImportar) / cantidadAImportar * 100;
                                this.requestCancel(ref cancel);
                                if (cancel)
                                {
                                    return;
                                }
                            }
                            lastID = row["NroPedido"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
            }
        }
    
        public emisorHandler<Catalogo.varios.complexMessage> emisor
        {
            get;
            set;
        }

        public Funciones.emitter_receiver.onRequestCancel requestCancel
        {
            get;
            set;
        }


        public emisorHandler<string> emisor2
        {
            get;
            set;
        }
    }
}
