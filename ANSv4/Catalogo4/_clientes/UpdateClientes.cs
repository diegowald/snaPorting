using System;

namespace Catalogo._clientes
{
    class UpdateClientes
    {
        // Define como se llama este modulo para el control de errores

        private UpdateClientesWS.UpdateClientes cliente;
        private bool webServiceInicializado;
//    Public Event SincronizarClientesProgress(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)
//    Public Event SincronizarClientesProgresoParcial(ByVal Detalle As String, ByVal Avance As Single, ByRef Cancel As Boolean)
        private string _MacAddress;
        private string _ipAddress;

        private System.Data.OleDb.OleDbConnection conexion;

        public UpdateClientes(System.Data.OleDb.OleDbConnection Conexion,
            string MacAddress,
            string ipAddress, string ipAddressIntranet, 
            bool usaProxy, string proxyServerAddress)
        {
            inicializar(MacAddress, ipAddress, ipAddressIntranet, usaProxy, proxyServerAddress);
            conexion = Conexion;
        }

        public bool inicializado
        {
            get
            {
                return webServiceInicializado;
            }
        }

        private void inicializar(string MacAddress,
            string ipAddress, string ipAddressIntranet, 
            bool usaProxy, string proxyServerAddress)
        {
            bool conectado = util.SimplePing.ping(ipAddress, 5000);

            if (!conectado)
            {
                conectado=util.SimplePing.ping(ipAddressIntranet, 5000);
            }

//        On Error GoTo errhandler
            if (!webServiceInicializado)
            {
                if (conectado)
                {
                    cliente = new UpdateClientesWS.UpdateClientes();
                    cliente.Url = "http://" + ipAddress + "/wsCatalogo4/UpdateClientes.asmx?wsdl";
                    if (usaProxy)
                    {
                        cliente.Proxy = new System.Net.WebProxy(proxyServerAddress);
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
//        Exit Sub
//errhandler:
//        If Err.Number = -2147024809 Then
//            ' Intento con el ip interno
//            Cliente = New WSUpdateClientes.UpdateClientesSoapClient("", "http://" & ipAddressIntranet & "/wsCatalogo4/UpdateClientes.asmx?wsdl")
//            If Trim(Funciones.modINIs.ReadINI("datos", "proxy")) = "1" Then
//                '                    Cliente.ConnectorProperty("ProxyServer") = Funciones.modINIs.ReadINI("datos", "proxyserver")
//            End If

//            m_MacAddress = MacAddress
//            WebServiceInicializado = True
//            m_ipAddress = ipAddressIntranet
//            Err.Clear()
//        Else
//            Err.Raise(Err.Number, Err.Source, Err.Description)
//        End If
//    End Sub
        }


        private void sincroClientesCompletada(ref bool cancel)
        {
            //        On Error GoTo ErrorHandler

            if (util.SimplePing.ping(_ipAddress, 5000))
            {
                // conexion no valida.
                cancel = true;
                return;
            }

            cliente.SincronizacionClientesCompletada(_MacAddress);
            cliente.SincronizacionCtasCtesCompletada(_MacAddress);
            return;

            //ErrorHandler:
            //        Err.Raise(Err.Number, Err.Source, Err.Description)

            //    End Sub
        }


        public void sincronizarClientes()
        {
            //        On Error GoTo ErrorHandler

            bool cancel = false;

            //        If vg.TranActiva Is Nothing Then
            //            vg.TranActiva = vg.Conexion.BeginTransaction
            //        End If

            //        RaiseEvent SincronizarClientesProgress("Sincronizando Clientes ...", 0, Cancel)

            if (!webServiceInicializado)
            {
                cancel = true;
            }

            if (!cancel)
            {
                //            RaiseEvent SincronizarClientesProgress("Sincronizando Clientes ...", 30, Cancel)
            }

            Catalogo.Funciones.oleDbFunciones.ComandoIU(conexion, "DELETE FROM tblClientes");

            if (!cancel)
            {
                sincronizarTodosLosClientes(ref cancel);
            }

            if (!cancel)
            {
                //RaiseEvent SincronizarClientesProgress("Sincronizando Cuentas Corrientes", 60, Cancel)
            }

            if (!cancel)
            {
                SincronizarTodasLasCtasCtes(ref cancel);
            }

            if (!cancel)
            {
                //            RaiseEvent SincronizarClientesProgress("Finalizando Sincronización de Clientes", 90, Cancel)
            }

            if (!cancel)
            {
                sincroClientesCompletada(ref cancel);
            }

            if (cancel)
            {
                //            If Not (vg.TranActiva Is Nothing) Then
                //                vg.TranActiva.Rollback()
                //                vg.TranActiva = Nothing
                //            End If
                //            RaiseEvent SincronizarClientesProgress("Sincronización de Clientes con Errores", 100, Cancel)
            }
            else
            {
                //            If Not (vg.TranActiva Is Nothing) Then
                //                vg.TranActiva.Commit()
                //                vg.TranActiva = Nothing
                //            End If

                Catalogo.Funciones.oleDbFunciones.ComandoIU(conexion, "EXEC usp_appConfig_FActClientes_Upd");
                //            RaiseEvent SincronizarClientesProgress("Sincronización de Clientes Finalizada", 100, Cancel)
            }
            return;

            //ErrorHandler:
            //        If Not (vg.TranActiva Is Nothing) Then
            //            vg.TranActiva.Rollback()
            //            vg.TranActiva = Nothing
            //        End If

            //        Err.Raise(Err.Number, Err.Source, Err.Description)

            //    End Sub
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
                cmd.ExecuteNonQuery();

                cmd = null;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                //ErrorHandler:

                //        If Err.Number = -2147467259 Then
                //            ' El registro está duplicado... debo borrar el registro e intentar nuevamente
                //            ' El error dice así:
                //            ' Los cambios solicitados en la tabla no se realizaron correctamente
                //            '  porque crearían valores duplicados en el índice, clave principal o relación.
                //            ' Cambie los datos en el campo o los campos que contienen datos duplicados,
                //            ' quite el índice o vuelva a definir el índice para permitir entradas duplicadas e inténtelo de nuevo.
                //            'diego            adoModulo.adoComandoIU(vg.Conexion, "DELETE FROM tblClientes WHERE ID = " & CStr(ID))
                //            Err.Clear()
                //            Resume
                //        End If

                //        cmd = Nothing
                //        Err.Raise(Err.Number, Err.Source, Err.Description)
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
            cmd.ExecuteNonQuery();

            cmd = null;
        }


        private void sincronizarTodosLosClientes(ref bool cancel)
        {
            if (!util.SimplePing.ping(_ipAddress, 5000))
            {
                cancel = true;
                return;
            }

            //    'diego    'RaiseEvent SincronizarClientesProgress("Sincronizando Clientes ...", 40, Cancel)

            //    'diego        RaiseEvent SincronizarClientesProgresoParcial("Importando Mis Clientes", 0, Cancel)

            //    'diego        If Cancel Then
            //    'diego            Exit Sub
            //    'diego End If


            // Obtengo la cantidad de modificaciones a importar
            long cantidadAImportar = cliente.GetTodosLosClientes_Cantidad(_MacAddress);
            long restanImportar = cantidadAImportar;
            
            long lastID = 0;
            long cantImportada = 0;


            while (restanImportar>0)
            {
            //    'diego     'RaiseEvent SincronizarClientesProgress("Sincronizando Clientes ...", (CantidadAImportar - RestanImportar) / CantidadAImportar * 100, Cancel)
                System.Data.DataSet ds = cliente.GetTodosLosClientes_Datos_Registros(_MacAddress, lastID);
  
                if (ds.Tables[0].Rows.Count > 0)
                {
                    restanImportar-=ds.Tables[0].Rows.Count;

            //    'diego          On Error GoTo Proximo
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        System.Data.DataRow row = ds.Tables[0].Rows[i];
                        try
                        {
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
                                //    'diego                  RaiseEvent SincronizarClientesProgresoParcial("Importando Mis Clientes", I / cantImportada * 100, Cancel)
                                if (cancel)
                                {
                                    return;
                                }
                            }
                        }
                        catch
                        {
                        }
                        lastID = (int)row["ID"];
                    }
                }
            }
        }

        private void SincronizarTodasLasCtasCtes(ref bool cancel)
        {
            if (!util.SimplePing.ping(_ipAddress, 5000))
            {
                cancel = true;
                return;
            }
            //    '        'RaiseEvent SincronizarClientesProgress("Sincronizando de Clientes ...", 60, Cancel)

            //    '        RaiseEvent SincronizarClientesProgresoParcial("Importando Cuentas Corrientes", 0, Cancel)

            if (cancel)
            {
                return;
            }
            
            // Obtengo la cantidad de modificaciones a importar
            long CantidadAImportar = cliente.GetTodasLasCtasCtes_Cantidad(_MacAddress);
            long RestanImportar = CantidadAImportar;
            long CantidadImportada = 0;
            long lastId = 0;

            while (RestanImportar>0)
            {
            //    '            ' RaiseEvent SincronizarClientesProgress("Sincronizando Clientes ...", (CantidadAImportar - RestanImportar) / CantidadAImportar * 100, Cancel)
                System.Data.DataSet ds = cliente.GetTodasLasCtasCtes_Datos_Registros(_MacAddress, lastId);
  
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RestanImportar-=ds.Tables[0].Rows.Count;

            //    '                On Error GoTo Proximo

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        System.Data.DataRow row = ds.Tables[0].Rows[i];
                        try
                        {
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
                                //    '                        RaiseEvent SincronizarClientesProgresoParcial("Importando Cuentas Corrientes", CantidadImportada / CantidadAImportar * 100, Cancel)
                                if (cancel)
                                {
                                    return;
                                }
                            }
                        }
                        catch
                        {
                            // catch'em all. Ver la manera de corregir esto...
                        }
                        lastId = long.Parse(row["ID"].ToString());
                    }
                }
            }
        }
    }
}
