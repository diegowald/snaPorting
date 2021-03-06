﻿using System;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo._novedades
{
    class UpdateNovedades : Funciones.emitter_receiver.IEmisor<Catalogo.varios.complexMessage>, 
        Funciones.emitter_receiver.IEmisor2<string>, // Para emitir un refresh de las novedades del sistema.
        Funciones.emitter_receiver.ICancellableEmitter
    {
        // Define como se llama este modulo para el control de errores
        
        private System.Data.OleDb.OleDbTransaction _TranActiva = null;

        private CatalogoNovedadesWS.CatalogoNovedades cliente;
        private bool webServiceInicializado;
        private string _MacAddress;
        private string _ipAddress;

        private bool hacerRefresh = false;
        //    Public Event SincronizarNovedadesProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)
        //    Public Event SincronizarNovedadesProgresoParcial(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)

        private System.Data.OleDb.OleDbConnection conexion;

        public UpdateNovedades(string MacAddress, string ipAddress)
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
                    cliente = new CatalogoNovedadesWS.CatalogoNovedades();
                    cliente.Url = "http://" + ipAddress + "/wsCatalogo4/CatalogoNovedades.asmx?wsdl";
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


        private void sincroNovedadesCompletada(ref bool cancel)
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

        internal void sincronizarNovedades()
        {
            try
            {
                hacerRefresh = false;
                bool cancel = false;

                if (_TranActiva== null)
                {
                    //@ _TranActiva = conexion.BeginTransaction();
                }

                Catalogo.varios.complexMessage msg;
                msg.progress1 = new util.Pair<string, float>("Sincronizando Novedades ...", 0);
                msg.progress2 = new util.Pair<string, float>("", 0);
                this.emitir(msg);///, Cancel)

                if (!webServiceInicializado)
                {
                    cancel = true;
                }

                if (!cancel)
                {
                    msg.progress1.second = 30;
                    this.emitir(msg);
                    this.requestCancel(ref cancel);
                }

                //Catalogo.Funciones.oleDbFunciones.ComandoIU(conexion, "DELETE FROM tblNovedades");

                if (!cancel)
                {
                    int IdUltimaNovedad = Int16.Parse("0" + Funciones.oleDbFunciones.Comando(conexion, "SELECT TOP 1 ID FROM ansNovedades ORDER BY ID DESC", "ID", false, _TranActiva));

                    sincronizarTodasLasNovedades(IdUltimaNovedad, ref cancel, msg);
                }

                if (!cancel)
                {
                    msg.progress1.first = "Finalizando Sincronización de Novedades";
                    msg.progress1.second = 90;
                    this.emitir(msg);
                    this.requestCancel(ref cancel);
                }

                if (!cancel)
                {
                    sincroNovedadesCompletada(ref cancel);
                    this.requestCancel(ref cancel);
                }

                if (cancel)
                {
                    if (_TranActiva!= null)
                    {
                        _TranActiva.Rollback();
                        _TranActiva = null;
                    }
                    msg.progress1.first="Sincronización de Novedades con Errores";
                    msg.progress1.second=100;
                    this.emitir(msg);
                    this.requestCancel(ref cancel);
                }
                else
                {
                    if (_TranActiva!= null)
                    {
                        _TranActiva.Commit();
                        _TranActiva = null;
                    }

                    Catalogo.Funciones.oleDbFunciones.ComandoIU(conexion, "EXEC usp_Novedades_Anexar");
                    msg.progress1.first = "Sincronización de Novedades Finalizada";
                    msg.progress1.second = 100;
                    this.emitir(msg);
                    if (hacerRefresh)
                    {
                        this.emitir2("Hacer Refresh novedades");
                    }
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

        private void Novedades_Add(System.Data.OleDb.OleDbConnection Conexion,
                                   int ID, DateTime Fecha, string Descripcion, string Destino, 
                                   string Origen, string Tipo, DateTime F_Inicio, DateTime F_Fin, 
                                   string N_Archivo, string url, string zonas, byte activo)
        {
            try
            {
                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();


                cmd.Parameters.Add("pNroPedido", System.Data.OleDb.OleDbType.VarChar, 14).Value = mvarNroPedido;
                cmd.Parameters.Add("pF_Pedido", System.Data.OleDb.OleDbType.Date).Value = mvarF_Pedido;
                cmd.Parameters.Add("pIdCliente", System.Data.OleDb.OleDbType.Integer).Value = mvarIdCliente;
                cmd.Parameters.Add("pNroImpresion", System.Data.OleDb.OleDbType.Integer).Value = 0;
                cmd.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 128).Value = mvarObservaciones;
                cmd.Parameters.Add("pTransporte", System.Data.OleDb.OleDbType.VarChar, 128).Value = mvarTransporte;

                cmd.Connection = Conexion;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_Pedido_OdN_add";

                cmd.ExecuteNonQuery();

                cmd = null;
            }

            catch (System.Data.OleDb.OleDbException ex)
            {

                if (ex.ErrorCode ==-2147467259)
                {
                    //        If Err.Number = -2147467259 Then
                    //            ' El registro está duplicado... debo borrar el registro e intentar nuevamente
                    Funciones.oleDbFunciones.ComandoIU(Conexion, "DELETE FROM ansNovedades WHERE ID = " + ID.ToString());
                    Novedades_Add(Conexion, ID, Fecha, Descripcion, Destino, Origen, Tipo, F_Inicio, F_Fin, N_Archivo, url, zonas, activo);
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

        private void sincronizarTodasLasNovedades(int IdUltimaNovedad, ref bool cancel, Catalogo.varios.complexMessage msg)
        {
            if (!util.network.IPCache.instance.conectado)
            {
                cancel = true;
                return;
            }

            msg.progress1.first = "Sincronizando Novedades ...";
            msg.progress1.second = 40;
            msg.progress2.first = "Importando Mis Novedades";
            msg.progress2.second = 0;
            this.emitir(msg);
            this.requestCancel(ref cancel);
            if (cancel)
            {
                return;
            }
            try
            {
                // Obtengo la cantidad de modificaciones a importar
                long cantidadAImportar = cliente.GetTodasLasNovedades_Cantidad(_MacAddress, IdUltimaNovedad);
                long restanImportar = cantidadAImportar;

                long lastID = IdUltimaNovedad;
                long cantImportada = 0;
                hacerRefresh = (cantidadAImportar > 0);

                while (restanImportar > 0)
                {
                    msg.progress2.first = "Sincronizando Novedades ...";
                    msg.progress2.second = ((float)cantidadAImportar - restanImportar) / cantidadAImportar * 100;
                    this.emitir(msg);
                    System.Data.DataSet ds = cliente.GetTodasLasNovedades_Datos_Registros(_MacAddress, lastID);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        restanImportar -= ds.Tables[0].Rows.Count;

                        //    'diego          On Error GoTo Proximo
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            System.Data.DataRow row = ds.Tables[0].Rows[i];
                            Novedades_Add(conexion,
                                        (int)row["ID"],
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
                                this.emitir(msg);
                                this.requestCancel(ref cancel);
                                if (cancel)
                                {
                                    return;
                                }
                            }
                            lastID = (int)row["ID"];
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
