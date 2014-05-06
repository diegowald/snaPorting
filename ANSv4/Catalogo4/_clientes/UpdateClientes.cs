using System;
using Catalogo.Funciones.emitter_receiver;

namespace Catalogo._clientes
{
    class UpdateClientes : Funciones.emitter_receiver.IEmisor<Catalogo.varios.complexMessage>, 
        Funciones.emitter_receiver.ICancellableEmitter
    {
        // Define como se llama este modulo para el control de errores

        private UpdateClientesWS.UpdateClientes cliente;
        private bool webServiceInicializado;
        private string _MacAddress;
        private string _ipAddress;

        //    Public Event SincronizarClientesProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)
        //    Public Event SincronizarClientesProgresoParcial(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)

        private System.Data.OleDb.OleDbConnection conexion;

        public UpdateClientes(string MacAddress, string ipAddress)
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
                    cliente = new UpdateClientesWS.UpdateClientes();
                    cliente.Url = "http://" + ipAddress + "/wsCatalogo4/UpdateClientes.asmx?wsdl";
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

        private void sincroClientesCompletada(ref bool cancel)
        {
            //        On Error GoTo .ErrorForm.show()
            if (!util.network.IPCache.instance.conectado)
            {
                // conexion no valida.
                cancel = true;
                return;
            }

            cliente.SincronizacionClientesCompletada(_MacAddress);
            cliente.SincronizacionCtasCtesCompletada(_MacAddress);
            return;
        }


        public void sincronizarClientes()
        {
            try
            {

                bool cancel = false;

                if (Global01.TranActiva == null)
                {
                    Global01.TranActiva = conexion.BeginTransaction();
                }

                Catalogo.varios.complexMessage msg;
                msg.progress1 = new util.Pair<string, float>("Sincronizando Clientes ...", 0);
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

                Catalogo.Funciones.oleDbFunciones.ComandoIU(conexion, "DELETE FROM tblClientes");

                if (!cancel)
                {
                    sincronizarTodosLosClientes(ref cancel, msg);
                }

                if (!cancel)
                {
                    msg.progress1.first = "Sincronizando Cuentas Corrientes";
                    msg.progress1.second = 60;
                    this.emitir(msg);///, Cancel)
                    this.requestCancel(ref cancel);                                                                                                      ///this.requestCancel(ref cancel);
                }

                if (!cancel)
                {
                    SincronizarTodasLasCtasCtes(ref cancel, msg);
                    this.requestCancel(ref cancel);
                }

                if (!cancel)
                {
                    msg.progress1.first = "Finalizando Sincronización de Clientes";
                    msg.progress1.second = 90;
                    this.emitir(msg);
                    this.requestCancel(ref cancel);
                }

                if (!cancel)
                {
                    sincroClientesCompletada(ref cancel);
                    this.requestCancel(ref cancel);
                }

                if (cancel)
                {
                    if (Global01.TranActiva != null)
                    {
                        Global01.TranActiva.Rollback();
                        Global01.TranActiva = null;
                        util.errorHandling.ErrorLogger.LogMessage("11 rollback");
                    }
                    msg.progress1.first="Sincronización de Clientes con Errores";
                    msg.progress1.second=100;
                    this.emitir(msg);
                    this.requestCancel(ref cancel);
                }
                else
                {
                    if (Global01.TranActiva != null)
                    {
                        Global01.TranActiva.Commit();
                        Global01.TranActiva = null;
                    }

                    Catalogo.Funciones.oleDbFunciones.ComandoIU(conexion, "EXEC usp_appConfig_FActClientes_Upd");
                    msg.progress1.first = "Sincronización de Clientes Finalizada";
                    msg.progress1.second = 100;
                    this.emitir(msg);
                }
            }
            catch (Exception ex)
            {
                util.errorHandling.ErrorLogger.LogMessage(ex);
                //.ErrorForm.show():
                if (Global01.TranActiva != null)
                {
                    Global01.TranActiva.Rollback();
                    Global01.TranActiva = null;
                    util.errorHandling.ErrorLogger.LogMessage("11 rollback");
                }

                throw ex;  //util.errorHandling.ErrorForm.show();
            }
            finally
            {
                Global01.TranActiva = null;
            }
        }

        private void Clientes_Add(System.Data.OleDb.OleDbConnection Conexion,
            int ID, string RazonSocial, string Cuit,
            string Email, int IDViajante,
            string Domicilio, string Ciudad,
            string Telefono, string Observaciones,
            byte Activo, DateTime F_Actualizacion,
            string Cascara)
        {
            try
            {

                System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

                cmd.Parameters.Add("pID", System.Data.OleDb.OleDbType.Integer).Value = ID;
                cmd.Parameters.Add("pRazonSocial", System.Data.OleDb.OleDbType.VarChar, 40).Value = RazonSocial;
                cmd.Parameters.Add("pCuit", System.Data.OleDb.OleDbType.VarChar, 13).Value = Cuit;
                cmd.Parameters.Add("pEmail", System.Data.OleDb.OleDbType.VarChar, 40).Value = Email;
                cmd.Parameters.Add("pIDViajante", System.Data.OleDb.OleDbType.Integer).Value = IDViajante;
                cmd.Parameters.Add("pDomicilio", System.Data.OleDb.OleDbType.VarChar, 40).Value = Domicilio;
                cmd.Parameters.Add("pCiudad", System.Data.OleDb.OleDbType.VarChar, 40).Value = Ciudad;
                cmd.Parameters.Add("pTelefono", System.Data.OleDb.OleDbType.VarChar, 40).Value = Telefono;
                cmd.Parameters.Add("pObservaciones", System.Data.OleDb.OleDbType.VarChar, 200).Value = Observaciones;
                cmd.Parameters.Add("pActivo", System.Data.OleDb.OleDbType.TinyInt).Value = Activo;
                cmd.Parameters.Add("pCascara", System.Data.OleDb.OleDbType.VarChar, 3).Value = Cascara;
                cmd.Parameters.Add("pF_Actualizacion", System.Data.OleDb.OleDbType.Date).Value = F_Actualizacion;

                cmd.Connection = Conexion;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "usp_Clientes_add";
                if (Global01.TranActiva != null)
                {
                    cmd.Transaction = Global01.TranActiva;
                }
                cmd.ExecuteNonQuery();

                cmd = null;
            }

            catch (System.Data.OleDb.OleDbException ex)
            {

                if (ex.ErrorCode ==-2147467259)
                {
                    //        If Err.Number = -2147467259 Then
                    //            ' El registro está duplicado... debo borrar el registro e intentar nuevamente
                    //            ' El error dice así:
                    //            ' Los cambios solicitados en la tabla no se realizaron correctamente
                    //            '  porque crearían valores duplicados en el índice, clave principal o relación.
                    //            ' Cambie los datos en el campo o los campos que contienen datos duplicados,
                    //            ' quite el índice o vuelva a definir el índice para permitir entradas duplicadas e inténtelo de nuevo.
                    Funciones.oleDbFunciones.ComandoIU(Conexion, "DELETE FROM tblClientes WHERE ID = " + ID.ToString());
                    Clientes_Add(Conexion, ID, RazonSocial, Cuit, Email, IDViajante, Domicilio,
                        Ciudad, Telefono, Observaciones, Activo, F_Actualizacion, Cascara);
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

        private void CtaCte_Add(System.Data.OleDb.OleDbConnection Conexion,
            int IdCliente, DateTime F_Comprobante,
            string T_Comprobante, string N_Comprobante,
            string Det_Comprobante, float Importe,
            float Saldo, float ImpOferta,
            string TextoOferta, byte Vencida,
            float ImpPercep, byte EsContado)
        {
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand();

            cmd.Parameters.Add("pIDcliente", System.Data.OleDb.OleDbType.Integer).Value = IdCliente;
            cmd.Parameters.Add("pF_Comprobante", System.Data.OleDb.OleDbType.Date).Value = F_Comprobante;
            cmd.Parameters.Add("pT_Comprobante", System.Data.OleDb.OleDbType.VarChar, 3).Value = T_Comprobante;
            cmd.Parameters.Add("pN_Comprobante", System.Data.OleDb.OleDbType.VarChar, 12).Value = N_Comprobante;
            cmd.Parameters.Add("pDet_Comprobante", System.Data.OleDb.OleDbType.VarChar, 100).Value = Det_Comprobante;
            cmd.Parameters.Add("pImporte", System.Data.OleDb.OleDbType.Single).Value = Importe;
            cmd.Parameters.Add("pSaldo", System.Data.OleDb.OleDbType.Single).Value = Saldo;
            cmd.Parameters.Add("pImpOferta", System.Data.OleDb.OleDbType.Single).Value = ImpOferta;
            cmd.Parameters.Add("pTextoOferta", System.Data.OleDb.OleDbType.VarChar, 100).Value = TextoOferta;
            cmd.Parameters.Add("pVencida", System.Data.OleDb.OleDbType.TinyInt).Value = Vencida;
            cmd.Parameters.Add("pImpPercep", System.Data.OleDb.OleDbType.Single).Value = ImpPercep;
            cmd.Parameters.Add("pEsContado", System.Data.OleDb.OleDbType.TinyInt).Value = EsContado;

            cmd.Connection = Conexion;
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "usp_CtaCte_add";
            if (Global01.TranActiva != null)
            {
                cmd.Transaction = Global01.TranActiva;
            }
            cmd.ExecuteNonQuery();

            cmd = null;
        }


        private void sincronizarTodosLosClientes(ref bool cancel, Catalogo.varios.complexMessage msg)
        {
            if (!util.network.IPCache.instance.conectado)
            {
                cancel = true;
                return;
            }
            try
            {
                msg.progress1.first = "Sincronizando Clientes ...";
                msg.progress1.second = 40;
                msg.progress2.first = "Importando Mis Clientes";
                msg.progress2.second = 0;
                this.emitir(msg);
                this.requestCancel(ref cancel);
                if (cancel)
                {
                    return;
                }

                // Obtengo la cantidad de modificaciones a importar
                long cantidadAImportar = cliente.GetTodosLosClientes_Cantidad(_MacAddress);
                long restanImportar = cantidadAImportar;

                long lastID = 0;
                long cantImportada = 0;


                while (restanImportar > 0)
                {
                    msg.progress2.first = "Sincronizando Clientes ...";
                    msg.progress2.second = ((float)cantidadAImportar - restanImportar) / cantidadAImportar * 100;
                    this.emitir(msg);
                    System.Data.DataSet ds = cliente.GetTodosLosClientes_Datos_Registros(_MacAddress, lastID);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        restanImportar -= ds.Tables[0].Rows.Count;

                        //    'diego          On Error GoTo Proximo
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            System.Data.DataRow row = ds.Tables[0].Rows[i];
                            Clientes_Add(conexion, (int)row["ID"],
                                      row["RazonSocial"].ToString(),
                                      row["Cuit"].ToString(),
                                      row["Email"].ToString(), (int)row["IDViajante"],
                                      row["Domicilio"].ToString(), row["Ciudad"].ToString(),
                                      row["Telefono"].ToString(), row["Observaciones"].ToString(),
                                      (byte)row["Activo"], (DateTime)row["F_ActCliente"], row["Cascara"].ToString());
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

        private void SincronizarTodasLasCtasCtes(ref bool cancel, Catalogo.varios.complexMessage msg)
        {
            if (!util.network.IPCache.instance.conectado)
            {
                cancel = true;
                return;
            }

            try
            {
                msg.progress1.first = "Sincronizando de Clientes ...";
                msg.progress1.second = 60;
                msg.progress2.first = "Importando Cuentas Corrientes";
                msg.progress2.second = 0;
                this.emitir(msg);
                this.requestCancel(ref cancel);
                if (cancel)
                {
                    return;
                }

                // Obtengo la cantidad de modificaciones a importar
                long CantidadAImportar = cliente.GetTodasLasCtasCtes_Cantidad(_MacAddress);
                long RestanImportar = CantidadAImportar;
                long CantidadImportada = 0;
                long lastId = 0;

                while (RestanImportar > 0)
                {
                    msg.progress2.first = "Sincronizando Cuentas Corrientes ...";
                    msg.progress2.second = ((float)CantidadAImportar - RestanImportar) / CantidadAImportar * 100;
                    this.emitir(msg);
                    System.Data.DataSet ds = cliente.GetTodasLasCtasCtes_Datos_Registros(_MacAddress, lastId);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        RestanImportar -= ds.Tables[0].Rows.Count;

                        //    '                On Error GoTo Proximo

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            System.Data.DataRow row = ds.Tables[0].Rows[i];
                            CtaCte_Add(conexion,
                                int.Parse(row["IdCliente"].ToString()),
                                (DateTime)row["F_Comprobante"],
                                row["T_Comprobante"].ToString(),
                                row["N_Comprobante"].ToString(),
                                DBNull.Value.Equals(row["Det_Comprobante"]) ? "" : row["Det_Comprobante"].ToString(),
                                int.Parse(row["Importe"].ToString()) / 100,
                                int.Parse(row["Saldo"].ToString()) / 100,
                                int.Parse(row["ImpOferta"].ToString()) / 100,
                                row["TextoOferta"].ToString(),
                                (byte)row["Vencida"],
                                int.Parse(row["ImpPercep"].ToString()) / 100,
                                (byte)row["EsContado"]);
                            CantidadImportada++;
                            if (CantidadImportada % 31 == 0)
                            {
                                msg.progress2.second = (float)CantidadImportada / CantidadAImportar * 100;
                                this.emitir(msg);
                                this.requestCancel(ref cancel);
                                if (cancel)
                                {
                                    return;
                                }
                            }
                            lastId = long.Parse(row["ID"].ToString());
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

    }
}
